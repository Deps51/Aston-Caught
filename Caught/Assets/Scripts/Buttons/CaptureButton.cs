using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureButton : ClickableButton
{

    public GrimReaper reaper;

    protected override void btn_onClick()
    {
        reaper.carry();
    }

}
