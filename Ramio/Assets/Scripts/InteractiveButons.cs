using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveButons : MonoBehaviour
{
    [Header ("Settings")]
    [Tooltip("Is it a pressueplate?")] public bool IsPressurePlate = false;
    public bool HasBeenTurnedOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPressurePlate && collision.gameObject.layer == 3)
        {
            HasBeenTurnedOn = true;
            FindObjectOfType<Door>().OpenPlease();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsPressurePlate)
        {
            HasBeenTurnedOn = false;
            FindObjectOfType<Door>().ClosePlease();
        }
    }
}
