using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpButton : ClickableButton
{
    
    public ControllablePlayer player;

    public PowerUp powerUp;
   

    //Handle the onClick event
    protected override void btn_onClick()
    {
        //Debug.Log("button onclick. Player: " + player.name );
        
        Debug.Log(player.getSpeed());
        //player.setPowerUp(powerUp);
        player.usePowerUp();

        //text.text = player.name + ". speed: " + player.getSpeed();

    }
}
