/*using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GrimReaper : ControllablePlayer
{
    private bool carryingPlayer = false;
    private Escapee currentCapturedPlyer = null;

   

    public override void Update()
    {
        powerUp();
        trap();
    }

    private void powerUp()
    {
        if (getPowerUp() != null && getPowerUp().isActive())
        {
            if (powerUpActivationTime.Second + getPowerUp().getTime() <= DateTime.Now.Second)
            {
                getPowerUp().setActive(false);
                getPowerUp().reset();
            }
        }
    }

    private void trap()
    {
        if (trapActive)
        {
            if (trapActivationTime.Second + getTrap().getTime() <= DateTime.Now.Second)
            {
                getTrap().setActive(false);
                getTrap().reset();
            }
        }
    }

    public bool playerInRange()
    {
        //checks if a player is in range to capture
        return false;
    }

    public void capture()
    {
        //disables escapee
    }

    public override void Start()
    {
        base.setName("Grim Reaper");
        base.setSpeed(5);
    }


}
*/