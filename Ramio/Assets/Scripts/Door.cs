using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool DoorOpen = false;

    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void OpenPlease()
    {
        DoorOpen = true;
        myAnimator.SetBool("DoorOpen", true);
    }

    public void ClosePlease()
    {
        DoorOpen = false;
        myAnimator.SetBool("DoorOpen", false);
    }

}
