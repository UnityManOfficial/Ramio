using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveButons : MonoBehaviour
{
    [Header ("Settings")]
    [Tooltip("Is it a pressueplate?")] public bool IsPressurePlate = false;
    [Tooltip("Is It a lever?")] public bool IsLever = false;
    public bool HasBeenTurnedOn = false;
    [SerializeField] [Tooltip("Insert Door Here")] GameObject Door;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPressurePlate && collision.gameObject.layer == 3)
        {
            HasBeenTurnedOn = true;
        }

        if(IsLever && collision.gameObject.layer == 3)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                HasBeenTurnedOn = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsPressurePlate)
        {
            HasBeenTurnedOn = false;
        }
    }
}
