using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    public int dyingState;
    public int deadBool;
    public int walkState;
    public int shoutState;
    public int speedFloat;
    public int sneakingBool;
    public int shoutingBool;
    public int backwardsBool;
    public int leftTrackSpeedFloat;
    public int rightTrackSpeedFloat;
    public int bucketWheelSpeedFloat;
    public int armSpeedFloat;
    public int reverseBool;
    public int launchingBool;
    public int reloadingBool;
    public int wheelSpeedFloat;

    private void Awake()
    {
        dyingState = Animator.StringToHash("BaseLayer.Dying");
        deadBool = Animator.StringToHash("Dead");
        walkState = Animator.StringToHash("Walk");
        shoutState = Animator.StringToHash("Shouting.Shout");
        speedFloat = Animator.StringToHash("Speed");
        sneakingBool = Animator.StringToHash("Sneaking");
        shoutingBool = Animator.StringToHash("Shouting");
        backwardsBool = Animator.StringToHash("Backwards");
        leftTrackSpeedFloat = Animator.StringToHash("left track speed");
        rightTrackSpeedFloat = Animator.StringToHash("right track speed");
        bucketWheelSpeedFloat = Animator.StringToHash("bucket wheel speed");
        armSpeedFloat = Animator.StringToHash("arm speed");
        launchingBool = Animator.StringToHash("Launching");
        reloadingBool = Animator.StringToHash("Reloading");
        wheelSpeedFloat = Animator.StringToHash("wheel_speed");
    }
}
