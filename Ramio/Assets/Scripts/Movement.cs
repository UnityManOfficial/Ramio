using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Health health;

    [Header("Movement Speed")]
    public float moveSpeed = 1.0f;
    public float jumpSpeed = 1.0f;
    private bool grounded = false;

    [Header("Player")]
    public int HP;
    public int MaxHP = 5;

    [Header("Settings")]
    public float PowerUpsCountdown = 1.0f;
    public float ReturnAfterPUSpeed = 5.0f;
    public float ReturnAfterPUJump = 8.0f;

    [Header("Audio Clips")]
    public AudioClip JumpSound;
    public AudioClip DeathSound;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2D;

    Coroutine CountDownPower;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
        HP = MaxHP;
        health.SetMaxHealth(MaxHP);
    }

    void Update()
    {
        Run();
        Jump();
        FlipCharacter();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        HP -= damage;
        health.SetHealth(HP);
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

    private void FlipCharacter()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            grounded = true;
        }
        if (other.gameObject.layer == 10)
        {
            PickUps pickups = other.gameObject.GetComponent<PickUps>();
            PowerUpAdd(pickups);
        }
    }

    private void PowerUpAdd(PickUps pickups)
    {
        if(pickups.IsJump() == true)
        {
            jumpSpeed += pickups.GetPowerup();
        }
        if(pickups.IsSpeed() == true)
        {
            moveSpeed += pickups.GetPowerup();
        }
        if(pickups.IsHealth() == true)
        {
            HP += pickups.GetPowerup();
            health.SetHealth(HP);
        }
        CountDownPower = StartCoroutine(CountDownPowerUp());
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

    IEnumerator CountDownPowerUp()
    {
        yield return new WaitForSeconds(PowerUpsCountdown);
        jumpSpeed = ReturnAfterPUJump;
        moveSpeed = ReturnAfterPUSpeed;
    }

}
