using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class ControllablePlayer : NetworkBehaviour
{
    private string name;
    private float speed;
    private PowerUp currentPowerUp = null;
    private Trap currentTrap = null;
    private PlayerMovement playerMovement;
    public static readonly int DEFAULT_SPEED = 6;
    protected DateTime powerUpActivationTime;
    public Canvas canvas;

    [SyncVar(hook = nameof(OnCompletedTaskChange))] public int completedTasks = 0;
    //[SyncVar] protected Sprite sprite;
    [SyncVar (hook = nameof(OnSpriteChange))] protected string spriteName;
    //public UnityEngine.UI.Text taskCompletedText;

    public PlayerMovement movement;
    protected List<Escapee> playersInRange = new List<Escapee>();
    //protected GameObject morphObject;

    public void OnCompletedTaskChange(int old, int new_)
    {
        RequiredTasks.completedTasks = new_;
        //CmdUpdateText(new_);
    }


    public void endGame()
    {
        CmdEndGame();
    }

    [Command]
    void CmdEndGame()
    {
        RpcEndGame();
    }

    [ClientRpc]
    void RpcEndGame()
    {
        SceneManager.LoadScene("MenuScene 1"); //aka end screen
    }


    public PowerUp getPowerUp()
    {
        return this.currentPowerUp;
    }

    public void setPowerUp(PowerUp powerUp)
    {
        currentPowerUp = powerUp;
        //currentPowerUp.setPlayer(this);
    }

    public PowerUp getTrap()
    {
        return this.currentTrap;
    }

    public void setTrap(Trap trap)
    {
        currentTrap = trap;
    }


    public float getSpeed()
    {
        return speed;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;

        //now need to change it in player movement
        movement.setSpeed(speed);
    }

    public void setName(string newName)
    {
        name = newName;
    }

    public void usePowerUp()
    {
        if (currentPowerUp.isActive() && currentPowerUp.getType() == "PowerUp")
        {
            return;
        }

        currentPowerUp.usePowerUp(this);
        //if (!(currentPowerUp is Trap)  )
        if (currentPowerUp.getType() == "PowerUp")
        {
            Debug.Log("setting activation time");
            powerUpActivationTime = DateTime.Now;
        }
        else
        {
            currentPowerUp = null;
        }

    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        setSprite(spriteName);

    }

    public abstract void Start();

    public abstract void Update();

    public abstract void generatorCompleted();

    public void placeTrap(int trap)
    {
        CmdPlaceTrap(trap);
    }

    public void getMorph()
    {
        //return morphObject;
    }

    public void setMorphNull()
    {
        //morphObject = null;
    }

    //[TargetRpc]
    public void setSprite(string name)
    {
        //sprite = s;
        //spriteName = name;

        spriteName = name;
        //CmdSetSprite(name);
    }

    public void OnSpriteChange(string old, string new_)
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(new_);
    }

    [Command]
    void CmdSetSprite(string name)
    {
        //sprite = Resources.Load<Sprite>(name);
        //GetComponent<SpriteRenderer>().sprite = sprite;
        RpcSetSprite(name);
    }

    [ClientRpc]
    void RpcSetSprite(string name)
    {
        Sprite s = Resources.Load<Sprite>(name);
        GetComponent<SpriteRenderer>().sprite = s;
    }

    [Command]
    void CmdPlaceTrap(int trap)//GameObject trapPrefab
    {
        Debug.Log("place trap on server [command]");
        //GameObject trap = Instantiate(Resources.Load("Trap"), transform.position, Quaternion.identity) as GameObject;
        RpcPlaceTrap(trap);
    }


    [ClientRpc]
    void RpcPlaceTrap(int trap)//GameObject trapPrefab
    {
        if (hasAuthority)
        {
            Debug.Log("player does: " + gameObject.name);
        }

        Debug.Log("place trap on server [ClientRpc]");
        GameObject placedTrap = Instantiate(Resources.Load("Trap"), transform.position, Quaternion.identity) as GameObject;
        placedTrap.GetComponent<ActivateTrap>().setTrap(Trap.getTrap(trap), this);
    }

    public void setImprisoned()
    {
        //player is now out of the game
 
        Debug.Log("player has authority to setImprisoned");
        //RpcSetImprisoned();
        CmdSetImprisoned();
    }

    [Command]
    void CmdSetImprisoned()
    {
        RpcSetImprisoned();
    }

    [ClientRpc]
    void RpcSetImprisoned()
    {
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }


    //[Server]
    public void changePos(float x, float y)
    {
        //this.transform.position = new Vector2(x, y);
        CmdChangePos(x, y);
        //RpcChangePos(x, y);
    }

    [Command]
    void CmdChangePos(float x, float y)
    {
        RpcChangePos(x, y);
    }
    
    /*[Command(ignoreAuthority = true)]
    void CmdChangePos(float x, float y)
    {
        RpcChangePos(x, y);
    }*/

    [ClientRpc]
    void RpcChangePos(float x, float y)
    {
        this.transform.position = new Vector2(x, y);
    }
}