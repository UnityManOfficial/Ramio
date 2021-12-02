using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void OpenPlease()
    {
        myAnimator.SetBool("DoorOpen", true);
    }

    public void ClosePlease()
    {
        myAnimator.SetBool("DoorOpen", false);
    }

}
