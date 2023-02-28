using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected int _durability { get; set; }

    public Item() { }
    

    protected abstract void UpdateMe(int a);
}
