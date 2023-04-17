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
    private bool armMovLeft = false;
    private bool armMovRight = false;
    private float armFrame = 0.0f;
    public bool is_playing = false;
    public bool engine_start = false;
    public bool revved = false;
    Movement movement;
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
        movement = Movement.STOP;
    }

    private void Update()
    {
        

        if (is_playing)
        {
            float drive = Input.GetAxis("Drive");
            //Debug.Log(drive);
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
        if (drive > 0 && steer == 0 && movement != Movement.FORWARD)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat,1.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, 1.0f);
            movement = Movement.FORWARD;
            Debug.Log("moving forward");
        }
        else if (drive < 0 && steer == 0 && movement != Movement.BACKWARD)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -1.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, -1.0f);
            movement = Movement.BACKWARD;
            Debug.Log("moving backward");
        }
        else if (drive == 0 && steer > 0 && movement != Movement.RIGHT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 1.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, -1.0f);
            movement = Movement.RIGHT;
            Debug.Log("turning right");
        }
        else if (drive == 0 && steer < 0 && movement != Movement.LEFT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -1.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, 1.0f);
            movement = Movement.LEFT;
            Debug.Log("turning left");
        }
        else if (drive > 0 && steer > 0 && movement != Movement.FORWARDRIGHT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 1.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, 0.6f);
            movement = Movement.FORWARDRIGHT;
            Debug.Log("moving forwardright");
        }
        else if (drive > 0 && steer < 0 && movement != Movement.FORWARDLEFT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 0.6f);
            anim.SetFloat(hash.rightTrackSpeedFloat, 1.0f);
            movement = Movement.FORWARDLEFT;
            Debug.Log("moving forwardleft");
        }
        else if (drive < 0 && steer > 0 && movement != Movement.BACKWARDLEFT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -0.6f);
            anim.SetFloat(hash.rightTrackSpeedFloat, -1.0f);
            movement = Movement.BACKWARDLEFT;
            Debug.Log("moving backwardleft");
        }
        else if (drive < 0 && steer < 0 && movement != Movement.BACKWARDRIGHT)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, -1.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, -0.6f);
            movement = Movement.BACKWARDRIGHT;
            Debug.Log("moving backwardright");
        }
        else if (drive == 0 && steer == 0 && movement != Movement.STOP)
        {
            anim.SetFloat(hash.leftTrackSpeedFloat, 0.0f);
            anim.SetFloat(hash.rightTrackSpeedFloat, 0.0f);
            movement = Movement.STOP;
            Debug.Log("stopped movement");
        }
    }
}
