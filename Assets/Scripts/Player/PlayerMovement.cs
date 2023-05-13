using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public AudioClip shoutingClip;
    public float speedDampTime = 0.01f;
    public float sensitivityX = 1.0f;
    public float animationSpeed = 1.5f;
    private float elapsedTime = 0;
    public bool noBackMov = true;
    private float desiredDuration = 0.5f;
    public bool is_playing = true;
    public bool can_move = true;

    public Rigidbody our_body;

    private Animator anim;
    private HashIDs hash;

    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(1, 1f);
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();

        GetComponent<AudioSource>().pitch = 0.5f;
        GetComponent<AudioSource>().volume = 1;
    }

    void FixedUpdate()
    {
        if (is_playing && can_move)
        {
            float v = Input.GetAxis("Vertical");
            bool sneak = Input.GetButton("Sneak");
            float turn = Input.GetAxis("Turn");
            Rotating(turn);
            MovementManagement(v, sneak);
            elapsedTime += Time.deltaTime;
        }
    }

    private void Update()
    {
        if (is_playing && can_move && noBackMov)
        {
            bool shout = Input.GetButtonDown("Attract");
            anim.SetBool(hash.shoutingBool, shout);
            AudioManagement(shout);
        }
    }

    void Rotating(float turn)
    {
        if (turn != 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, turn * sensitivityX, 0f);
            our_body.MoveRotation(our_body.rotation * deltaRotation);
        }
    }

    void MovementManagement(float vertical, bool sneaking)
    {
        anim.SetBool(hash.sneakingBool, sneaking);

        if (vertical > 0)
        {
            anim.SetFloat(hash.speedFloat, 1.5f, speedDampTime, Time.deltaTime);
            anim.SetBool("Backwards", false);
            noBackMov = true;
        }

        if (vertical < 0)
        {
            if (noBackMov == true)
            {
                elapsedTime = 0;
                noBackMov = false;
            }

            anim.SetFloat(hash.speedFloat, -1.5f, speedDampTime, Time.deltaTime);
            anim.SetBool("Backwards", true);

            float percentageComplete = elapsedTime / desiredDuration;
            Rigidbody ourBody = this.GetComponent<Rigidbody>();
            float movement = Mathf.Lerp(0f, -0.025f, percentageComplete);
            Vector3 moveBack = new Vector3(0f, 0f, movement);
            moveBack = ourBody.transform.TransformDirection(moveBack);
            ourBody.transform.position += moveBack;

            if (GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Stop();
            }
        }

        if (vertical == 0)
        {
            anim.SetFloat(hash.speedFloat, 0);
            anim.SetBool(hash.backwardsBool, false);
            noBackMov = true;
        }
    }

    void AudioManagement (bool shout)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            if (GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Stop();
            }
        }

        if (shout)
        {
            AudioSource.PlayClipAtPoint(shoutingClip, transform.position);

            GameObject thisAudio = GameObject.Find("One shot audio");

            if (true)
            {
                thisAudio.GetComponent<AudioSource>().pitch = 2.0f;
            }
        }
    }

    public void stopWalkAudio()
    {
        if (GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}


