using System.Collections;
using System.Collections.Generic;
using System;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escapee : ControllablePlayer 
{
    [SyncVar] private bool currentlyCaptured = false;
    private bool imprisoned = false;
    private Task[] tasks;
    [SerializeField] private GameObject powerUpBtn;
    [SerializeField] private GameObject taskBtn;
    [SerializeField] private GameObject freeBtn;

    private GrimReaper reaperCarry = null;
    private Generator currentGenerator = null;


    //[SyncVar (hook = nameof(OnCompletedTaskChange))] int completedTasks = 0;


    //public Escapee(string name, int speed) : base(name, speed) { Debug.Log("Controllable player cons"); }

    public override void Update()
    {
        if (!hasAuthority)
        {
            return;
        }

        if (imprisoned)
        {
            Debug.Log("In Prison");
            return; //stops player being controlled if they're out
        }
        else
        {
            if (currentlyCaptured)
            {
                //changePos(reaperCarry.transform.position.x -0.3f, reaperCarry.transform.position.y);
                transform.position = new Vector3(reaperCarry.transform.position.x - 0.3f, reaperCarry.transform.position.y, 0);
                //this.transform.position = new Vector3(10, 10, 0);
  
                //return; //stops player being controlled if they're captured
            }
            else
            {
                //rest of code goes here
                powerUp();
                //trap();
                //movement.ProcessInputs();

            //move pos if captured
            
            }
        }
    }

    private void powerUp()
    {
        if (getPowerUp() != null && getPowerUp().isActive() && !(getPowerUp() is Trap))
        {
            if (powerUpActivationTime.Second + getPowerUp().getTime() <= DateTime.Now.Second)
            {
                getPowerUp().setActive(false);
                getPowerUp().reset(this);
                setPowerUp(null);
            }
        }
    }

    public void changePos(float x, float y) {
        base.changePos(x, y);
    }

    [Command]
    void CmdSetCaptured(bool state)
    {
        currentlyCaptured = state;
        RpcSetCaptured(state);  
    }

    [ClientRpc]
    void RpcSetCaptured(bool state)
    {
        Debug.Log("RpcSetCaptured set to " + state);
        //currentlyCaptured = state;
        //GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Players/Escapees/EscapeeCaught");
    }

    [TargetRpc]
    public void captured(GameObject reaper)
    {
        movement.enabled = false;
        //currentlyCaptured = true;
        CmdSetCaptured(true);
        Debug.Log(currentlyCaptured);   
        reaperCarry = reaper.GetComponent<GrimReaper>();
        setSpeed(0);
        taskBtn.GetComponent<TaskButton>().deActivateTaskBtn();
        powerUpBtn.GetComponent<PowerUpButton>().deActivateTaskBtn();
        //deactive buttons
        //message on screen
    }

    public bool isCaptured()
    {
        return currentlyCaptured;
    }

    public void setImprisoned()
    {
        //player is now out of the game
        imprisoned = true;
        base.setImprisoned();
        CmdSetCaptured(false);
        reaperCarry = null;
        movement.enabled = true;
        setSpeed(DEFAULT_SPEED);
        Debug.Log(gameObject.name + " is out of the game");
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Escapee trigger enter");
        if(!currentlyCaptured && collision.gameObject.tag == "Escapee")
        {
            Debug.Log("triger is escapee");
            if (collision.gameObject.GetComponent<Escapee>().isCaptured())
            {
                Debug.Log("escape is caught");
                playersInRange.Add(collision.gameObject.GetComponent<Escapee>());
                freeBtn.GetComponent<FreeButton>().activateTaskBtn();
            }     
        }/*else if(collision.gameObject.tag == "Morph"){
            //morph here
            morphObject = collision.gameObject;
        }*/
    }

    private void OnTriggerExit(Collider collision)
    {
        if(!currentlyCaptured && collision.gameObject.tag == "Escapee" && playersInRange.Count > 0)
        {
            if (playersInRange.Contains(collision.gameObject.GetComponent<Escapee>()))
            {
                playersInRange.Remove(collision.gameObject.GetComponent<Escapee>());
            }
            
        }/*else if(collision.gameObject.tag == "Morph"){
            //morph here
            morphObject = null;
        }*/

        if(playersInRange.Count == 0)
        {
            freeBtn.GetComponent<FreeButton>().deActivateTaskBtn();
        }
    }

    

    [Command]
    public void CmdFree()
    {
        playersInRange[0].RpcFree();
    }

    [TargetRpc]
    void RpcFree()
    {
        //currentlyCaptured = false;
        movement.enabled = true;
        //currentlyCaptured = false;
        //RpcSetCaptured(false);
        CmdSetCaptured(false);
        reaperCarry = null;
        setSpeed(ControllablePlayer.DEFAULT_SPEED);
        powerUpBtn.GetComponent<PowerUpButton>().activateTaskBtn();
        freeBtn.GetComponent<FreeButton>().deActivateTaskBtn();
        //freeClientRpc(n);
    }

    [ClientRpc]
    void freeClientRpc()
    {
        //GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(n);
    }

    public void freePlayer()
    {
        CmdFree();
    }

    public void setGenerator(Generator g)
    {
        currentGenerator = g;
    }

    public override void generatorCompleted()
    {
        if(currentGenerator is Task)
        {
            setPowerUp(Task.GetPowerUp());
        }
        else
        {
            Debug.Log("Gen is required");

            RequiredTasks rt = (RequiredTasks)currentGenerator;
            int i = rt.completeTask();
            
            cmdUpdateTaskCount(i);
        }
    }

    [Command]
    void cmdUpdateTaskCount(int i)
    {
        Debug.Log("Youve won, now go to RPC");
        completedTasks = i;
        /*if (i >= RequiredTasks.totalTasks)
        {
            //SceneManager.LoadScene("MenuScene");
            RpcWin(i);
        }*/
    }

    [ClientRpc]
    void RpcWin(int i)
    {
        Debug.Log("Youve won, now go");
        completedTasks = i;
        //RequiredTasks.win();
        //SceneManager.LoadScene("MenuScene");
    }



    public override void Start()
    {
        if (isLocalPlayer)
        {
            Debug.Log("Escapee start");
            setName("Escapee");
            setSpeed(5);

            canvas.gameObject.SetActive(true);

            Debug.Log("Escapee Local Player");;
            powerUpBtn.GetComponent<PowerUpButton>().player = this;
            taskBtn.GetComponent<TaskButton>().player = this;
        }  
    }

    public void activateTaskBtn()
    {
        Debug.Log("activate task btn");
        taskBtn.GetComponent<TaskButton>().activateTaskBtn(this);
    }

    public void deActivateTaskBtn()
    {
        Debug.Log("deActivate task btn");
        taskBtn.GetComponent<TaskButton>().deActivateTaskBtn();
    }
}