using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOrb : MonoBehaviour
{
    private bool JumpOrbActive;
    private float enableTimer;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        JumpOrbActive = true;
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!JumpOrbActive)
        {
            enableTimer -= Time.deltaTime;

            if (enableTimer < 0)
            {
                JumpOrbActive = true;
                spriteRenderer.color = new Color(1, 1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Movement player = collision.gameObject.GetComponent<Movement>();
        if (JumpOrbActive && player != null)
        {
            JumpOrbActive = false;
            enableTimer = 5f;
            spriteRenderer.color = new Color(1, 1, 1, .5f);
            player.TouchedJumpOrb();
        }
    }
}
