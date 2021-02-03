using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeButton : ClickableButton
{
    public Escapee player;

    protected override void btn_onClick()
    {
        player.freePlayer();
    }
}
