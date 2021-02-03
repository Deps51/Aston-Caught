using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasedSpeed : PowerUp
{
    private GameObject powerUpBtn;
    public override void Start()
    {
        setName("Speed Increase");
        setTime(5); //lasts for 5 seconds

        //COMMENT OUT THIS IF NOT TESTING:

        //powerUpBtn = GameObject.FindGameObjectsWithTag("Button")[1];
        //powerUpBtn.GetComponent<PowerUpButton>().powerUp = this;
        Task.addPowerUp(this);

    }

    public override void usePowerUp(ControllablePlayer p)
    {
        //maths here:
        //float new_speed = getPlayer().getSpeed() * 2;
        float new_speed = p.getSpeed() * 2;
        //getPlayer().setSpeed(new_speed);
        p.setSpeed(new_speed);

        //don't change:
        setActive(true);
    }

    public override void reset(ControllablePlayer p)
    {
        //getPlayer().setSpeed(ControllablePlayer.DEFAULT_SPEED);
        p.setSpeed(ControllablePlayer.DEFAULT_SPEED);
    }
}
