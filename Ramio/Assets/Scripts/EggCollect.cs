using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EggCollect : MonoBehaviour
{

    public bool IsCollected = false;

    Animator CollectedEgg;

    // Start is called before the first frame update
    void Start()
    {
        CollectedEgg = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            IsCollected = true;
            CollectedEgg.SetBool("Collect", true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
