using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavatorMovement : MonoBehaviour
{
    public float speedDampTime = 399f;
    public float sensitivityX = 1.0f;
    public float animationSpeed = 1.5f;
    public float elapsedTime = 0;
    private bool noForwardMov = true;
    private bool noBackMov = true;
    private float desiredDuration = 0.5f;
    private bool bucketWheelMov = false;
    private bool armMovLeft = false;
    private bool armMovRight = false;
    private float armFrame = 0.0f;
    public bool is_playing = false;
    public bool engine_start = false;
    public bool revved = false;
    public float left_track_speed = 0.0f;
    public float right_track_speed = 0.0f;
    public AudioClip engine_start_clip;
    public AudioClip rev_clip;

    private Animator anim;
    private HashIDs hash;
    private Rigidbody ourBody;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        ourBody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (is_playing)
        {
            float drive = Input.GetAxis("Drive");
            float steer = Input.GetAxis("Steer");
            MovementManagement(drive);
            Rotating(steer);
            BucketWheelManagement();
            ArmManagement();
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

            // track speeds
            if (left_track_speed < 1)
            {
                left_track_speed += 1.0f;
            }

            if (right_track_speed < 1)
            {
                right_track_speed += 1.0f;
            }


            // start animations
            //anim.SetFloat(hash.leftTrackSpeedFloat, left_track_speed, speedDampTime, Time.deltaTime);
            //anim.SetFloat(hash.rightTrackSpeedFloat, right_track_speed, speedDampTime, Time.deltaTime);
            //anim.SetBool(hash.movingBool, true);
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

            // track speeds
            if (left_track_speed > -1)
            {
                left_track_speed -= 1.0f;
            }

            if (right_track_speed > -1)
            {
                right_track_speed -= 1.0f;
            }

            // start animations
            //anim.SetFloat(hash.leftTrackSpeedFloat, left_track_speed, speedDampTime, Time.deltaTime);
            //anim.SetFloat(hash.rightTrackSpeedFloat, right_track_speed, speedDampTime, Time.deltaTime);
            //anim.SetBool(hash.movingBool, true);
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
            //anim.SetFloat(hash.leftTrackSpeedFloat, 0);
            //anim.SetFloat(hash.rightTrackSpeedFloat, 0);
            //anim.SetBool(hash.movingBool, false);
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
            anim.SetBool(hash.movingBool, true);
            Quaternion deltaRotation = Quaternion.Euler(0f, steer * sensitivityX, 0f);
            // this value is applied to turn the body via the rididbody
            ourBody.MoveRotation(ourBody.rotation * deltaRotation);
            
            if (steer > 0)
            {
                // track speeds
                if (left_track_speed < 1)
                {
                    left_track_speed += 1.0f;
                }

                if (right_track_speed > -1)
                {
                    right_track_speed -= 1.0f;
                }

                // animation
                anim.SetFloat(hash.leftTrackSpeedFloat, left_track_speed);
                anim.SetFloat(hash.rightTrackSpeedFloat, right_track_speed);
            }
            else
            {
                // track speeds
                if (left_track_speed > -1)
                {
                    left_track_speed -= 1.0f;
                }

                if (right_track_speed < 1)
                {
                    right_track_speed += 1.0f;
                }

                // animation
                anim.SetFloat(hash.leftTrackSpeedFloat, left_track_speed);
                anim.SetFloat(hash.rightTrackSpeedFloat, right_track_speed);
            }
        }
        else
        {

        }

        Debug.Log(left_track_speed);
        Debug.Log(right_track_speed);
    }

    void BucketWheelManagement()
    {
        if (Input.GetKeyUp("f") && bucketWheelMov)
        {
            bucketWheelMov = false;
            anim.SetFloat(hash.bucketWheelSpeedFloat, 0);
        }
        else if(Input.GetKeyUp("f") && !bucketWheelMov)
        {
            bucketWheelMov = true;
            anim.SetFloat(hash.bucketWheelSpeedFloat, 1);
        }
    }

    void ArmManagement()
    {
        if (Input.GetKeyDown("q") && !armMovLeft &&!armMovRight)
        {
            armMovLeft = true;
            anim.SetFloat(hash.armSpeedFloat, 1);
        }
        else if (Input.GetKeyUp("q") && armMovLeft)
        {
            armMovLeft = false;
            anim.SetFloat(hash.armSpeedFloat, 0);
        }

        if (Input.GetKeyDown("e") && !armMovLeft && !armMovRight)
        {
            armMovRight = true;
            anim.SetFloat(hash.armSpeedFloat, -1);
        }
        else if (Input.GetKeyUp("e") && armMovRight)
        {
            armMovRight = false;
            anim.SetFloat(hash.armSpeedFloat, 0);
        }

        if (armMovLeft)
        {
            armFrame += 1 * Time.deltaTime;

            if (armFrame > 3)
            {
                armMovLeft = false;
                anim.SetFloat(hash.armSpeedFloat, 0);
            }
        }
        else if (armMovRight)
        {
            armFrame -= 1 * Time.deltaTime;

            if (armFrame < -3)
            {
                armMovRight = false;
                anim.SetFloat(hash.armSpeedFloat, 0);
            }
        }
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
        if (drive > 0 && steer == 0)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 1.0f, speedDampTime, Time.deltaTime);
            anim.SetFloat(hash.rightTrackSpeedFloat, 1.0f, speedDampTime, Time.deltaTime);
            anim.SetBool(hash.movingBool, true);
        }
        else if (drive < 0 && steer == 0)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -1.0f, speedDampTime, Time.deltaTime);
            anim.SetFloat(hash.rightTrackSpeedFloat, -1.0f, speedDampTime, Time.deltaTime);
            anim.SetBool(hash.movingBool, true);
        }
        else if (drive == 0 && steer > 0)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 1.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, -1.0f);
            anim.SetBool(hash.movingBool, true);
        }
        else if (drive == 0 && steer < 0)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -1.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, 1.0f);
            anim.SetBool(hash.movingBool, true);
        }
        else if (drive > 0 && steer > 0)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 1.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, 0.5f);
            anim.SetBool(hash.movingBool, true);
        }
        else if (drive > 0 && steer < 0)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 0.5f);
            anim.SetFloat(hash.rightTrackSpeedFloat, 1.0f);
            anim.SetBool(hash.movingBool, true);
        }
        else if (drive < 0 && steer > 0)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -0.5f);
            anim.SetFloat(hash.rightTrackSpeedFloat, -1.0f);
            anim.SetBool(hash.movingBool, true);
        }
        else if (drive < 0 && steer < 0)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -0.5f);
            anim.SetFloat(hash.rightTrackSpeedFloat, -1.0f);
            anim.SetBool(hash.movingBool, true);
        }
        else if (drive == 0 && steer == 0)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 0.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, 0.0f);
            anim.SetBool(hash.movingBool, false);
        }
    }
}
