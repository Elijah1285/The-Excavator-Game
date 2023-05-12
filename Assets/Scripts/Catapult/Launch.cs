using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Launch : MonoBehaviour
{
    public GameObject projectile;
    public float projectileForce = 800.0f;
    public float catapult_cooldown = 0;
    GameObject missile;
    public bool missile_is_live = false;
    public bool player_in_range = false;

    public Transform projectile_cam_pos;

    public TMP_Text catapult_instruction;
    public TMP_Text minimap_toggle_instruction;

    public Camera projectile_cam;
    public Camera player_cam;

    public AudioSource load_audio_source;
    public AudioSource launch_audio_source;

    public Animator anim;

    public DirtDump dirt_dump;
    public CatapultTrigger catapult_trigger;
    public HashIDs hash;

    void FixedUpdate()
    {
        bool fire = Input.GetButton("Fire");
        bool instantiate = Input.GetButton("Instantiate");

        if (instantiate && (!missile_is_live) && player_in_range && dirt_dump.money >= 50 && catapult_cooldown <= 0)
        {
            missile = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
            missile.GetComponent<Rigidbody>().useGravity = false;
            missile_is_live = true;

            dirt_dump.money -= 50;
            dirt_dump.money_counter.text = dirt_dump.money.ToString();

            load_audio_source.Play();
        }
        if (fire && (missile_is_live) && player_in_range)
        {
            projectile_cam.transform.position = projectile_cam_pos.position;
            projectile_cam.transform.rotation = projectile_cam_pos.rotation;
            projectile_cam.transform.parent = missile.transform;

            player_cam.enabled = false;
            projectile_cam.enabled = true;

            missile.GetComponent<Rigidbody>().AddForce(missile.transform.right * projectileForce, ForceMode.Acceleration);
            missile.GetComponent<Rigidbody>().useGravity = true;
            missile_is_live = false;

            launch_audio_source.Play();

            anim.SetBool(hash.launchingBool, true);

            if (catapult_instruction.enabled)
            {
                catapult_instruction.enabled = false;
            }

            if (!catapult_trigger.shown_catapult_instruction)
            {
                catapult_trigger.shown_catapult_instruction = true;
            }

            if (minimap_toggle_instruction.enabled)
            {
                minimap_toggle_instruction.enabled = false;
            }

            catapult_cooldown = 8.0f;
        }

        if (catapult_cooldown > 0)
        {
            catapult_cooldown -= Time.deltaTime;
        }
    }
}
