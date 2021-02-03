using System.Collections;
using System.Collections.Generic;
using System;
using Mirror;
using UnityEngine;

public class GrimReaper : ControllablePlayer
{
    private bool carryingPlayer = false;
    private DateTime carryCoolDownActivationTime;
    private int carryCoolDownTime = 5; //(5 seconds)
    private bool carryCoolDown = false;

    [SerializeField] private GameObject powerUpBtn;
    [SerializeField] private GameObject captureBtn;

    //private List<Escapee> playersInRange = new List<Escapee>();

    public override void Update()
    {
        //movement.ProcessInputs();
        if (!hasAuthority)
        {
            return;
        }

        powerUp();
        //trap();

        if(carryingPlayer)
        {
            //CmdCarry();
            //changePos(transform.position.x, transform.position.y);
            if (!(playersInRange[0].isCaptured()))
            {
                dropPlayer();
            }          
        }

        if (carryCoolDown)
        {
            if(carryCoolDownActivationTime.Second + carryCoolDownTime <= DateTime.Now.Second)
            {
                carryCoolDown = false;
            }
        }

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

    void OnTriggerEnter(Collider collision)
    {
        if (!carryCoolDown)
        {
            if (collision.gameObject.tag == "Escapee")
            {
                Debug.Log("Player in range");
                //playerInRange = collision.gameObject.GetComponent<Escapee>();
                playersInRange.Add(collision.gameObject.GetComponent<Escapee>());
                captureBtn.GetComponent<CaptureButton>().activateTaskBtn();
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (!carryingPlayer && collision.gameObject.tag == "Escapee")
        {
            //playerInRange = null;
            if(playersInRange.Count > 0)
            {
                playersInRange.Remove(collision.gameObject.GetComponent<Escapee>());
            }
            
            if(playersInRange.Count == 0)
            {
                captureBtn.GetComponent<CaptureButton>().deActivateTaskBtn();
            }        
        }      
    }

    public void carry()
    {
        //disables escapee
        //playersInRange[0].captured(this);
        if (playersInRange[0].isCaptured())
        {
            playersInRange.RemoveAt(0);
            return;
        }
        CmdCarry();
        //playersInRange[0].GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
        captureBtn.GetComponent<CaptureButton>().deActivateTaskBtn();
        carryingPlayer = true;
    }

    public bool isCarryingPlayer()
    {
        return carryingPlayer;
    }

    public void dropPlayer()
    {
        carryingPlayer = false;
        //playersInRange[0].GetComponent<NetworkIdentity>().RemoveClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
        carryCoolDown = true;
        //reset playersInRange list and start a cool down
        playersInRange.Clear();
        carryCoolDownActivationTime = DateTime.Now;
    }

    public void froceImprisonment()
    {
        playersInRange[0].setImprisoned();
    }

    public override void Start()
    {
        if (isLocalPlayer)
        {
            base.setName("Grim Reaper");
            base.setSpeed(5);

            canvas.gameObject.SetActive(true);
            captureBtn.GetComponent<CaptureButton>().reaper = this;
        }
    }

    public override void generatorCompleted()
    {
        setPowerUp(Task.GetPowerUp());
    }

    [Command]
    void CmdCarry()
    {
        playersInRange[0].captured(gameObject);
    }
}
