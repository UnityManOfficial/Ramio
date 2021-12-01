using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{

    public bool Speed = false;
    public bool Jump = false;
    public bool Rock = false;

    [Range(1, 10)] public int SpeedV = 4;
    [Range(1, 10)] public int JumpV = 4;
    [Range(1, 10)] public int RockCollect = 4;
    public AudioClip PickupSound;


    public int Powerup()
    {
        if(Speed)
        {
            return SpeedV;
        }
        if(Jump)
        {
            return JumpV;
        }
        if(Rock)
        {
            return RockCollect;
        }
        else
        {
            return 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            AudioSource.PlayClipAtPoint(PickupSound, Camera.main.transform.position, 0.5f);
            Destroy(gameObject);
        }
    }
}
