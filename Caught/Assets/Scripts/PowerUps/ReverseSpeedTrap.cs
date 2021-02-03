using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseSpeedTrap : Trap
{
    
    public override void Start()
    {
        setName("Reverse Controls");
        setTime(5); //lasts for 5 seconds
        type = "Trap";

        //COMMENT OUT THIS IF NOT TESTING:
        //GameObject powerUpBtn = GameObject.FindGameObjectWithTag("Button");
        //powerUpBtn.GetComponent<ClickableButton>().powerUp = this;

        Task.addPowerUp(this);
        trapIndex = Trap.addTrap(this);
    }

    public override void usePowerUp(ControllablePlayer p)
    {
        //getPlayer().placeTrap(trapIndex);
        p.placeTrap(trapIndex);
    }

    public override void activateTrap()
    {
        //maths here:
        float new_speed = getTargetPlayer().getSpeed() * -1;
        getTargetPlayer().setSpeed(new_speed);

        //don't change:
        setActive(true);
        getTargetPlayer().setTrap(this);


        
    }

    public override void reset(ControllablePlayer p = null)
    {
        Debug.Log("reversedSpeedTrap reset");
        getTargetPlayer().setSpeed(ControllablePlayer.DEFAULT_SPEED);
        getTargetPlayer().setTrap(null);


        //Destroy(placedTrap);
    }

}
