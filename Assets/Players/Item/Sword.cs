using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int Damage { get; }
    public int Protection { get; }
    public (int, int) Potion { get; }
    public void Reset() { }

    public int Upgrade(int money)
    {
        throw new NotImplementedException();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Zombie"))
        {
            Debug.Log("OOOOOUCH");
        }
    }
}
