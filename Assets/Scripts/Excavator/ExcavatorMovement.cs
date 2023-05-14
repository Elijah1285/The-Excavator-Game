using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavatorMovement : MonoBehaviour
{
    enum Movement {FORWARD, BACKWARD, LEFT, RIGHT, FORWARDLEFT, FORWARDRIGHT, BACKWARDLEFT, BACKWARDRIGHT, STOP, AIR};

    public float speed = 1.0f;
    public float rotate_speed = 1.0f;
    public float wheel_speed = 1.0f;
    public float speedDampTime = 399f;
    public float sensitivityX = 0.5f;
    public float animationSpeed = 1.5f;
    public float elapsedTime = 0;
    private bool noForwardMov = true;
    private bool noBackMov = true;
    private float desiredDuration = 0.5f;
    private float armFrame = 0.0f;
    public bool is_playing = false;
    public bool can_move = true;
    public bool cutscene_playing = false;
    public bool engine_start = false;
    public bool revved = false;
    public float bucket_wheel_speed = 0.0f;
    public float arm_speed = 0.0f;
    Movement movement;
    public AudioClip engine_start_clip;
    public AudioClip rev_clip;

    public AudioSource engine_audio_source;
    public AudioSource bucket_wheel_audio_source;
    public AudioSource arm_audio_source;
    public AudioSource turn_audio_source;
    public AudioSource drive_audio_source;

    public GameObject scooper;

    public ParticleSystem pipe_1_particles;
    public ParticleSystem pipe_2_particles;
    public ParticleSystem pipe_3_particles;

    public Animator anim;
    private HashIDs hash;
    public Rigidbody ourBody;

    private void Awake()
    {
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        movement = Movement.STOP;
        engine_audio_source.volume = 0.2f;
        bucket_wheel_audio_source.volume = 0.2f;
        arm_audio_source.volume = 0.2f;
        turn_audio_source.volume = 0.05f;
        drive_audio_source.volume = 0.2f;
    }

    private void Update()
    {
        if (is_playing && can_move && !cutscene_playing)
        {
            float drive = Input.GetAxis("Drive");
            float steer = Input.GetAxis("Steer");
            float arm = Input.GetAxis("Arm");
            float wheel = Input.GetAxis("Wheel");

            MovementManagement(drive);
            Rotating(steer);
            BucketWheelManagement(wheel);
            ArmManagement(arm);
            AudioManagement();
            AnimationAndParticleManagement(drive, steer);
            elapsedTime += Time.deltaTime;
        }
    }

    void MovementManagement(float drive)
    {
        if (drive > 0)
        {
            if (noForwardMov == true)
            {
                elapsedTime = 0;
                noForwardMov = false;
            }

            noBackMov = true;

            // do movement
            float percentageComplete = elapsedTime / desiredDuration;
            float movement = Mathf.Lerp(0f, -0.030f, percentageComplete);
            Vector3 moveForward = new Vector3(movement * speed, 0f, 0f);
            moveForward = ourBody.transform.TransformDirection(moveForward);
            ourBody.transform.position += moveForward;

            if (!revved)
            {
                AudioSource.PlayClipAtPoint(rev_clip, transform.position);
                revved = true;
            }

            if (!drive_audio_source.isPlaying)
            {
                drive_audio_source.Play();
            }
        }

        if (drive < 0)
        {
            if (noBackMov == true)
            {
                elapsedTime = 0;
                noBackMov = false;
            }

            noForwardMov = true;

            // do movement
            float percentageComplete = elapsedTime / desiredDuration;
            float movement = Mathf.Lerp(0f, 0.030f, percentageComplete);
            Vector3 moveBack = new Vector3(movement * speed, 0f, 0f);
            moveBack = ourBody.transform.TransformDirection(moveBack);
            ourBody.transform.position += moveBack;

            if (!revved)
            {
                AudioSource.PlayClipAtPoint(rev_clip, transform.position);
                revved = true;
            }

            if (!drive_audio_source.isPlaying)
            {
                drive_audio_source.Play();
            }
        }
        if (drive == 0)
        {
            noForwardMov = true;
            noBackMov = true;
            revved = false;

            if (drive_audio_source.isPlaying)
            {
                drive_audio_source.Stop();
            }
        }
    }
    void Rotating(float steer)
    {
        if (steer != 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, steer * sensitivityX * rotate_speed, 0f);
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);

            if (!turn_audio_source.isPlaying)
            {
                turn_audio_source.Play();
            }
        }
        else
        {
            if (turn_audio_source.isPlaying)
            {
                turn_audio_source.Stop();
            }
        }
    }

    void BucketWheelManagement(float wheel)
    {
        if (wheel <= 0)
        {
            if (bucket_wheel_speed > 0)
            {
                bucket_wheel_speed += wheel;
            }
            else if (bucket_wheel_speed <= 0 && scooper.GetComponent<DirtScooper>().bucket_wheel_turning)
            {
                bucket_wheel_speed = 0;
                bucket_wheel_audio_source.Stop();
                scooper.GetComponent<DirtScooper>().bucket_wheel_turning = false;

                pipe_2_particles.Stop();
            }

            anim.SetFloat(hash.bucketWheelSpeedFloat, bucket_wheel_speed);
        }
        else if (wheel > 0)
        {
            bucket_wheel_speed = wheel * wheel_speed * 5.0f;
            anim.SetFloat(hash.bucketWheelSpeedFloat, bucket_wheel_speed);

            if (!scooper.GetComponent<DirtScooper>().bucket_wheel_turning)
            {
                scooper.GetComponent<DirtScooper>().bucket_wheel_turning = true;
            }

            if (!bucket_wheel_audio_source.isPlaying)
            {
                bucket_wheel_audio_source.Play();
            }

            if (!pipe_2_particles.isEmitting)
            {
                pipe_2_particles.Play();
            }
        }
    }

    void ArmManagement(float arm)
    {
        if (arm < 0)
        {
            if (armFrame < -3.75)
            {
                arm_speed = 0;
            }
            else
            {
                arm_speed = arm * 1.5f;
                armFrame += arm_speed * Time.deltaTime;
            }

            if (!arm_audio_source.isPlaying)
            {
                arm_audio_source.Play();
            }
        }
        else if (arm > 0)
        {
            if (armFrame > 3.75)
            {
                arm_speed = 0;
            }
            else
            {
                arm_speed = arm * 1.5f;
                armFrame += arm_speed * Time.deltaTime;
            }

            if (!arm_audio_source.isPlaying)
            {
                arm_audio_source.Play();
            }
        }
        else
        {
            arm_speed = arm;

            if (arm_audio_source.isPlaying)
            {
                arm_audio_source.Stop();
            }
        }

        anim.SetFloat(hash.armSpeedFloat, arm_speed);
    }

    void AudioManagement()
    {
        if (engine_start)
        {
            AudioSource.PlayClipAtPoint(engine_start_clip, transform.position);
            engine_start = false;
        }
    }

    void AnimationAndParticleManagement(float drive, float steer)
    {
        var pipe_1_particles_main = pipe_1_particles.main;
        var pipe_2_particles_main = pipe_2_particles.main;
        var pipe_3_particles_main = pipe_3_particles.main;

        if (drive > 0 && steer == 0 && can_move && movement != Movement.FORWARD)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 1.0f * (speed * 0.8f));
            anim.SetFloat(hash.rightTrackSpeedFloat, 1.0f * (speed * 0.8f));

            if (!pipe_1_particles.isEmitting)
            {
                pipe_1_particles.Play();
            }

            if (!pipe_3_particles.isEmitting)
            {
                pipe_3_particles.Play();
            }

            pipe_1_particles_main.startSize = 300;
            pipe_3_particles_main.startSize = 300;
            movement = Movement.FORWARD;
        }
        else if (drive < 0 && steer == 0 && can_move && movement != Movement.BACKWARD)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -1.0f * (speed * 0.8f));
            anim.SetFloat(hash.rightTrackSpeedFloat, -1.0f * (speed * 0.8f));

            if (!pipe_1_particles.isEmitting)
            {
                pipe_1_particles.Play();
            }

            if (!pipe_3_particles.isEmitting)
            {
                pipe_3_particles.Play();
            }

            pipe_1_particles_main.startSize = 300;
            pipe_3_particles_main.startSize = 300;
            movement = Movement.BACKWARD;
        }
        else if (drive == 0 && steer > 0 && can_move && movement != Movement.RIGHT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 1.0f * (speed * 0.8f));
            anim.SetFloat(hash.rightTrackSpeedFloat, -1.0f * (speed * 0.8f));

            if (!pipe_1_particles.isEmitting)
            {
                pipe_1_particles.Play();
            }

            if (!pipe_3_particles.isEmitting)
            {
                pipe_3_particles.Play();
            }

            pipe_1_particles_main.startSize = 300;
            pipe_3_particles_main.startSize = 300;
            movement = Movement.RIGHT;
        }
        else if (drive == 0 && steer < 0 && can_move && movement != Movement.LEFT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -1.0f * (speed * 0.8f));
            anim.SetFloat(hash.rightTrackSpeedFloat, 1.0f * (speed * 0.8f));

            if (!pipe_1_particles.isEmitting)
            {
                pipe_1_particles.Play();
            }

            if (!pipe_3_particles.isEmitting)
            {
                pipe_3_particles.Play();
            }

            pipe_1_particles_main.startSize = 300;
            pipe_3_particles_main.startSize = 300;
            movement = Movement.LEFT;
        }
        else if (drive > 0 && steer > 0 && can_move && movement != Movement.FORWARDRIGHT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 1.0f * (speed * 0.8f));
            anim.SetFloat(hash.rightTrackSpeedFloat, 0.6f * (speed * 0.8f));

            if (!pipe_1_particles.isEmitting)
            {
                pipe_1_particles.Play();
            }

            if (!pipe_3_particles.isEmitting)
            {
                pipe_3_particles.Play();
            }

            pipe_1_particles_main.startSize = 300;
            pipe_3_particles_main.startSize = 100;
            movement = Movement.FORWARDRIGHT;
        }
        else if (drive > 0 && steer < 0 && can_move && movement != Movement.FORWARDLEFT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 0.6f * (speed * 0.8f));
            anim.SetFloat(hash.rightTrackSpeedFloat, 1.0f * (speed * 0.8f));

            if (!pipe_1_particles.isEmitting)
            {
                pipe_1_particles.Play();
            }

            if (!pipe_3_particles.isEmitting)
            {
                pipe_3_particles.Play();
            }

            pipe_1_particles_main.startSize = 100;
            pipe_3_particles_main.startSize = 300;
            movement = Movement.FORWARDLEFT;
        }
        else if (drive < 0 && steer > 0 && can_move && movement != Movement.BACKWARDLEFT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -0.6f * (speed * 0.8f));
            anim.SetFloat(hash.rightTrackSpeedFloat, -1.0f * (speed * 0.8f));

            if (!pipe_1_particles.isEmitting)
            {
                pipe_1_particles.Play();
            }

            if (!pipe_3_particles.isEmitting)
            {
                pipe_3_particles.Play();
            }

            pipe_1_particles_main.startSize = 100;
            pipe_3_particles_main.startSize = 300;
            movement = Movement.BACKWARDLEFT;
        }
        else if (drive < 0 && steer < 0 && can_move && movement != Movement.BACKWARDRIGHT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -1.0f * (speed * 0.8f));
            anim.SetFloat(hash.rightTrackSpeedFloat, -0.6f * (speed * 0.8f));

            if (!pipe_1_particles.isEmitting)
            {
                pipe_1_particles.Play();
            }

            if (!pipe_3_particles.isEmitting)
            {
                pipe_3_particles.Play();
            }

            pipe_1_particles_main.startSize = 300;
            pipe_3_particles_main.startSize = 100;
            movement = Movement.BACKWARDRIGHT;
        }
        else if (drive == 0 && steer == 0 && can_move && movement != Movement.STOP)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 0.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, 0.0f);

            if (pipe_1_particles.isEmitting)
            {
                pipe_1_particles.Stop();
            }

            if (pipe_3_particles.isEmitting)
            {
                pipe_3_particles.Stop();
            }

            pipe_1_particles_main.startSize = 300;
            pipe_3_particles_main.startSize = 300;
            movement = Movement.STOP;
        }
    }

    public void stopMovementAudio()
    {
        if (drive_audio_source.isPlaying)
        {
            drive_audio_source.Stop();
        }

        if (turn_audio_source.isPlaying)
        {
            turn_audio_source.Stop();
        }
    }

    public void stopArmAudio()
    {
        if (arm_audio_source.isPlaying)
        {
            arm_audio_source.Stop();
        }
    }

    public void setMovementToAir()
    {
        movement = Movement.AIR;
    }
}
