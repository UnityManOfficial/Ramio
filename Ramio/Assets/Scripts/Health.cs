using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public AudioClip BreakSound;

    Animator Warning;

    Coroutine ResetDie;

    private void Start()
    {
        Warning = GetComponent<Animator>();
    }

    public void SetMaxHealth(int HP)
    {
        slider.maxValue = HP;
        slider.value = HP;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int HP)
    {
        slider.value = HP;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if(HP <= 3)
        {
            Warning.SetBool("LowHealth", true);
            Warning.SetFloat("ACK", 1f);
            if (HP <= 2)
            {
                Warning.SetFloat("ACK", 5f);
                if(HP <= 0)
                {
                    Warning.SetBool("Died", true);
                    Warning.SetFloat("ACK", 1f);
                    ResetDie = StartCoroutine(RestartD());
                    AudioSource.PlayClipAtPoint(BreakSound, Camera.main.transform.position, 0.03f);
                }
            }
        }
        if (HP > 3)
        {
            Warning.SetBool("LowHealth", false);
        }
    }

    IEnumerator RestartD()
    {
        yield return new WaitForSeconds(2);
        Warning.SetBool("Died", false);
    }

}
