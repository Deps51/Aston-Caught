using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskButton : ClickableButton
{
    public GameObject loading_bar;
    public Escapee player;

    protected override void btn_onClick()
    {
        loading_bar.active = true;
        player.setSpeed(0);
        
    }

    public void exitTask()
    {
        loading_bar.active = false;
        player.setSpeed(ControllablePlayer.DEFAULT_SPEED);
    }

    public void activateTaskBtn(ControllablePlayer player)
    {
        base.activateTaskBtn();
        loading_bar.GetComponentInChildren<LoadingScreenControlScript>().setPlayer(player);
    }

}
