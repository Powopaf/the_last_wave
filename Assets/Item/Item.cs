using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected int _durability { get; set; }

    public Item() { }

    public delegate void Update();

    protected abstract void UpdateMe(int a);
}
