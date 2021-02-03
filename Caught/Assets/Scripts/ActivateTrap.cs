using Mirror;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ActivateTrap : NetworkBehaviour
{
    public GameObject prefab;
    Trap trap;
    private DateTime trapActivationTime;
    public string target;
    

    void OnTriggerEnter(Collider collision)
    {
        if (!trap.isActive() && collision.gameObject.tag == target)
        {
            if(target == "Escapee")
            {
                trap.setTargetPlayer(collision.gameObject.GetComponent<Escapee>());
                Debug.Log("They are escapee");
            }
            else
            {
                trap.setTargetPlayer(collision.gameObject.GetComponent<GrimReaper>());
                Debug.Log("They are grim reaper");
            }

            Debug.Log("Trap. Player trapped is: " + trap.getTargetPlayer().name);
            trap.activateTrap();
            trapActivationTime = DateTime.Now;
        }
        else
        {
            Debug.Log("Someone else got caught by this trap or got hit by own teammate");
        }
    }

    public void setTrap(Trap trap, ControllablePlayer player)
    {
        this.trap = trap;
        target = player is Escapee ? "GrimReaper" : "Escapee";
        Debug.Log("Target: " + target);
    }


    void Update()
    {
        if (trap.isActive())
        {
            if (trapActivationTime.Second + trap.getTime() <= DateTime.Now.Second)
            {
                Debug.Log("trap time out");
                trap.setActive(false);
                trap.reset();

                //Destroy(trap);
                Destroy(prefab);
                //Destroy(trap);
                //Destroy(this);
            }
        }
    }
}
