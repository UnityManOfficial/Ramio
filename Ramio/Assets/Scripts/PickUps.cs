using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{

    public bool Speed = false;
    public bool Jump = false;
    public bool Health = false;

    [Range(1, 10)] public int SpeedV = 4;
    [Range(1, 10)] public int JumpV = 4;
    [Range(1, 4)] public int HealthV = 1;
    public AudioClip PickupSound;

    Coroutine DeathCounter;

    public int GetPowerup()
    {
        if(Speed)
        {
            return SpeedV;
        }
        if(Jump)
        {
            return JumpV;
        }
        if(Health)
        {
            return HealthV;
        }
        else
        {
            return 0;
        }
    }

    public bool IsJump()
    {
        return Jump;
    }

    public bool IsSpeed()
    {
        return Speed;
    }

    public bool IsHealth()
    {
        return Health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            //AudioSource.PlayClipAtPoint(PickupSound, Camera.main.transform.position, 0.5f);
            DeathCounter = StartCoroutine(DisappearLikeMyFather());
        }
    }

    IEnumerator DisappearLikeMyFather()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
