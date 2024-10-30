using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public void CharacterDeath(int health)
    {
        if (health < 0) Die();
    }

    public void Die()
    {
        Console.WriteLine("Die");
    }
}
