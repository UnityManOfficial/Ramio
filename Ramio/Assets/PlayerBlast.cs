using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlast : MonoBehaviour
{
    CapsuleCollider2D blastCollider;
    public float lifetime = 3f;
    public float blastSpeed = 10f;
    public int damage = 10;
    public Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        blastCollider = GetComponent<CapsuleCollider2D>();
        rigidbody2d.velocity = transform.right * blastSpeed;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (blastCollider.IsTouchingLayers(LayerMask.GetMask("Hazards", "Foreground", "Buttons", "Boss")))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Boss boss = hitInfo.GetComponent<Boss>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
        }
    }
}
