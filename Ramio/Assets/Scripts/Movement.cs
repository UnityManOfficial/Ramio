using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Movement : MonoBehaviour
{

    public Health health;

    [Header("Movement Speed")]
    public float moveSpeed = 1.0f;
    public float jumpSpeed = 1.0f;
    public float DoubleJumpSpeed = 1.0f;
    private bool grounded = false;
    public bool DoubleJumpYes = false;
    public bool DJEnable = true;

    [Header("Player")]
    public int HP;
    public int MaxHP = 5;
    public int lives = 3;

    [Header("Settings")]
    public float PowerUpsCountdown = 1.0f;
    public float ReturnAfterPUSpeed = 5.0f;
    public float ReturnAfterPUJump = 8.0f;
    public GameObject EndTransition;
    public TextMeshProUGUI LivesCounter;

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
        LivesCounter.text = lives.ToString();
    }

    void Update()
    {
        Run();
        Jump();
        if(DoubleJumpYes && DJEnable)
        {
            DoubleJump();
        }
        FlipCharacter();
        PlayerDeath();
        GameOver();
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

    public void GameOver()
    {
        if(lives == 0)
        {
            EndTransition.SetActive(true);
            StartCoroutine(LoadOver());
        }
    }


    public void PlayerDeath()
    {
        if(HP <= 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(25f, 25f);
            StartCoroutine(Oops());
        }
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
            DoubleJumpYes = true;
        }
    }

    private void DoubleJump()
    {
        if (Input.GetButtonDown("Jump") && DoubleJumpYes == true && !grounded)
        {
            myRigidBody.AddForce(new Vector2(0, 100 * jumpSpeed));
            DoubleJumpYes = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            grounded = true;
            DoubleJumpYes = false;
        }
        else if (collision.gameObject.layer == 10)
        {
            PickUps pickups = collision.gameObject.GetComponent<PickUps>();
            PowerUpAdd(pickups);
        }
        else if(collision.tag == "NextLevel")
        {
            EndTransition.SetActive(true);
            StartCoroutine(NextLevel());
        }
        else if (collision.tag == "PrevLevel")
        {
            EndTransition.SetActive(true);
            StartCoroutine(PrevLevel());
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

    IEnumerator Oops()
    {
        lives -= 1;
        HP = MaxHP;
        yield return new WaitForSeconds(1);
        LivesCounter.text = lives.ToString();
        health.SetHealth(HP);
    }

    IEnumerator CountDownPowerUp()
    {
        yield return new WaitForSeconds(PowerUpsCountdown);
        jumpSpeed = ReturnAfterPUJump;
        moveSpeed = ReturnAfterPUSpeed;
    }
    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator PrevLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    IEnumerator LoadOver()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOver");
    }

}
