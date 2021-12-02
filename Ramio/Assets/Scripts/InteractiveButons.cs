using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveButons : MonoBehaviour
{

    [Header("Settings")]
    public bool Timer = false;
    public float TimeCount = 1.0f;

    Coroutine Countdown;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            if(Timer)
            {
                FindObjectOfType<Door>().OpenPlease();
                Countdown = StartCoroutine(ClosingIn());
            }
            else if (!Timer)
            {
                FindObjectOfType<Door>().OpenPlease();
            }
        }
    }

    IEnumerator ClosingIn()
    {
        yield return new WaitForSeconds(TimeCount);
        FindObjectOfType<Door>().ClosePlease();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!Timer)
        {
            FindObjectOfType<Door>().ClosePlease();
        }
    }
}
