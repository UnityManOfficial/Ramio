using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed = 1.0f;
    public float jumpSpeed = 1.0f;
    public bool grounded = false;
    public AudioClip JumpSound;
    public AudioClip DeathSound;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2D;

    void Start()
    {

        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        Run();
        Jump();
        //FlipCharacter();
    }

    private void Run()
    {
        float moveX = Input.GetAxis("Horizontal");
        var velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = moveX * moveSpeed;
        myRigidBody.velocity = velocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            myRigidBody.AddForce(new Vector2(0, 100 * jumpSpeed));
            myAnimator.SetBool("Jumping", true);
            AudioSource.PlayClipAtPoint(JumpSound, Camera.main.transform.position, 0.5f);
        }
    }

    //private void FlipCharacter()
    //{
        //bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        //if (playerHasHorizontalSpeed)
       // {
           // transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
       // }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            grounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            grounded = true;
        }
    }

}
