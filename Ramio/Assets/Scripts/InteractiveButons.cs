using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveButons : MonoBehaviour
{

    [Header("Settings")]
    public bool Timer = false;
    public float TimeCount = 1.0f;

    Animator LeverAnimation;

    Coroutine Countdown;

    private void Start()
    {
        LeverAnimation = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            if(Timer)
            {
                LeverAnimation.SetBool("PullTheLeverKronk", true);
                FindObjectOfType<Door>().OpenPlease();
                Countdown = StartCoroutine(ClosingIn());
            }
            else if (!Timer)
            {
                LeverAnimation.SetBool("PullTheLeverKronk", true);
                FindObjectOfType<Door>().OpenPlease();
            }
        }
    }

    IEnumerator ClosingIn()
    {
        yield return new WaitForSeconds(TimeCount);
        LeverAnimation.SetBool("PullTheLeverKronk", false);
        FindObjectOfType<Door>().ClosePlease();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!Timer)
        {
            LeverAnimation.SetBool("PullTheLeverKronk", false);
            FindObjectOfType<Door>().ClosePlease();
        }
    }
}
