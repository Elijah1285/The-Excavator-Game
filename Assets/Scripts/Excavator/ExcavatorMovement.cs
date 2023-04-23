using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavatorMovement : MonoBehaviour
{
    enum Movement {FORWARD, BACKWARD, LEFT, RIGHT, FORWARDLEFT, FORWARDRIGHT, BACKWARDLEFT, BACKWARDRIGHT, STOP};

    public float speedDampTime = 399f;
    public float sensitivityX = 1.0f;
    public float animationSpeed = 1.5f;
    public float elapsedTime = 0;
    private bool noForwardMov = true;
    private bool noBackMov = true;
    private float desiredDuration = 0.5f;
    private bool bucketWheelMov = false;
    private float armFrame = 0.0f;
    public bool is_playing = false;
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

    private Animator anim;
    private HashIDs hash;
    private Rigidbody ourBody;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        ourBody = this.GetComponent<Rigidbody>();
        movement = Movement.STOP;
        engine_audio_source.volume = 0.3f;
        bucket_wheel_audio_source.volume = 0.3f;
        arm_audio_source.volume = 0.3f;
    }

    private void Update()
    {
        

        if (is_playing)
        {
            float drive = Input.GetAxis("Drive");
            //Debug.Log(drive);
            float steer = Input.GetAxis("Steer");
            float arm = Input.GetAxis("Arm");
            float wheel = Input.GetAxis("Wheel");
            MovementManagement(drive);
            Rotating(steer);
            BucketWheelManagement(wheel);
            ArmManagement(arm);
            AudioManagement();
            AnimationManagement(drive, steer);
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
            float movement = Mathf.Lerp(0f, -0.025f, percentageComplete);
            Vector3 moveForward = new Vector3(movement, 0f, 0f);
            moveForward = ourBody.transform.TransformDirection(moveForward);
            ourBody.transform.position += moveForward;

            if (!revved)
            {
                AudioSource.PlayClipAtPoint(rev_clip, transform.position);
                revved = true;
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
            float movement = Mathf.Lerp(0f, 0.025f, percentageComplete);
            Vector3 moveBack = new Vector3(movement, 0f, 0f);
            moveBack = ourBody.transform.TransformDirection(moveBack);
            ourBody.transform.position += moveBack;

            if (!revved)
            {
                AudioSource.PlayClipAtPoint(rev_clip, transform.position);
                revved = true;
            }
        }
        if (drive == 0)
        {
            noForwardMov = true;
            noBackMov = true;
            revved = false;
        }
    }
    void Rotating(float steer)
    {
        // access the avatar's rigidbody
        Rigidbody ourBody = this.GetComponent<Rigidbody>();

        // check whether we have rotation data to apply
        if (steer != 0)
        {
            // use mouse input to create a Euler ange which provides rotation in the Y axis
            // this value is then turned into a Quarternion
            Quaternion deltaRotation = Quaternion.Euler(0f, steer * sensitivityX, 0f);
            // this value is applied to turn the body via the rididbody
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
        }
    }

    void BucketWheelManagement(float wheel)
    {
        if (wheel <= 0)
        {
            bucketWheelMov = false;

            if (bucket_wheel_speed > 0)
            {
                bucket_wheel_speed += wheel;
            }
            else if (bucket_wheel_speed < 0)
            {
                bucket_wheel_speed = 0;
                bucket_wheel_audio_source.Stop();
            }

            anim.SetFloat(hash.bucketWheelSpeedFloat, bucket_wheel_speed);
        }
        else if (wheel > 0)
        {
            bucketWheelMov = true;
            bucket_wheel_speed = wheel * 5.0f;
            anim.SetFloat(hash.bucketWheelSpeedFloat, bucket_wheel_speed);

            if (!bucket_wheel_audio_source.isPlaying)
            {
                bucket_wheel_audio_source.Play();
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

    void AnimationManagement(float drive, float steer)
    {
        if (drive > 0 && steer == 0 && movement != Movement.FORWARD)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat,1.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, 1.0f);
            movement = Movement.FORWARD;
        }
        else if (drive < 0 && steer == 0 && movement != Movement.BACKWARD)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -1.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, -1.0f);
            movement = Movement.BACKWARD;
        }
        else if (drive == 0 && steer > 0 && movement != Movement.RIGHT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 1.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, -1.0f);
            movement = Movement.RIGHT;
        }
        else if (drive == 0 && steer < 0 && movement != Movement.LEFT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -1.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, 1.0f);
            movement = Movement.LEFT;
        }
        else if (drive > 0 && steer > 0 && movement != Movement.FORWARDRIGHT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 1.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, 0.6f);
            movement = Movement.FORWARDRIGHT;
        }
        else if (drive > 0 && steer < 0 && movement != Movement.FORWARDLEFT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 0.6f);
            anim.SetFloat(hash.rightTrackSpeedFloat, 1.0f);
            movement = Movement.FORWARDLEFT;
        }
        else if (drive < 0 && steer > 0 && movement != Movement.BACKWARDLEFT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -0.6f);
            anim.SetFloat(hash.rightTrackSpeedFloat, -1.0f);
            movement = Movement.BACKWARDLEFT;
        }
        else if (drive < 0 && steer < 0 && movement != Movement.BACKWARDRIGHT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -1.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, -0.6f);
            movement = Movement.BACKWARDRIGHT;
        }
        else if (drive == 0 && steer == 0 && movement != Movement.STOP)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 0.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, 0.0f);
            movement = Movement.STOP;
        }
    }
}
