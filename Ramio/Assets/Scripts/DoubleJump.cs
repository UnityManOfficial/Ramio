using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    private bool canDoubleJump;
    public float jumpVelocity = 15f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            canDoubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                rigidbody2d.velocity = Vector2.up * jumpVelocity;
            }
            else
            {
                if (canDoubleJump)
                {
                    rigidbody2d.velocity = Vector2.up * jumpVelocity;
                    canDoubleJump = false;
                }
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        Debug.Log(raycastHit2d.collider);
        return raycastHit2d.collider != null;
    }
}
