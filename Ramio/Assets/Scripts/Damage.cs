using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    public int DamageDealt = 1;
    public bool Invincible = false;

    public int GetDamage()
    {
       return DamageDealt;
    }

}
