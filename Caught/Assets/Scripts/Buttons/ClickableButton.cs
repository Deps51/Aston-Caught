using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public abstract class ClickableButton : MonoBehaviour
{
    public UnityEngine.UI.Button btn;
    protected abstract void btn_onClick();

    void Start()
    {
        btn.onClick.AddListener(btn_onClick); //subscribe to the onClick event

    }

    public void activateTaskBtn()
    {    
        GetComponent<UnityEngine.UI.Button>().interactable = true;
         
    }

    public void deActivateTaskBtn()
    {    
        GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
}
