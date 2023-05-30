using System;
using System.Collections;
using System.Collections.Generic;
using IAPlayer;
using UnityEngine;

public class WhenAttack : MonoBehaviour
{
    public IAplayer IaplayeAplayer;
    public void Awake()
    {
        IaplayeAplayer = GetComponentInParent<IAplayer>();
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag(IaplayeAplayer.AIsetter.target.tag))
        {
            
            IaplayeAplayer.attacking = true;
            IaplayeAplayer.animator.SetBool("Attack",true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(IaplayeAplayer.AIsetter.target.tag))
        {
            
            IaplayeAplayer.attacking = false;
            IaplayeAplayer.animator.SetBool("Attack",false);
        }
    }
}
