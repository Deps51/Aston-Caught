using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    private string name; //name of the powerup
    private int time; //how long the effect lasts for in seconds
    [SerializeField]  private ControllablePlayer player; //player who currenty has this powerup
    private bool active = false;
    protected string type = "PowerUp";
    public abstract void Start();

    public abstract void usePowerUp(ControllablePlayer p);

    public abstract void reset(ControllablePlayer p = null);


    protected void setName(string name)
    {
        this.name = name;
    }

    public string getName()
    {
        return name;
    }

    public int getTime()
    {
        return time;
    }

    protected void setTime(int time)
    {
        this.time = time;
    }

    public string getType()
    {
        return type;
    }

    public bool isActive()
    {
        return active;
    }

    public void setActive(bool active)
    {
        this.active = active;
    }


    public void setPlayer(ControllablePlayer player)
    {
        this.player = player;
    }

    public ControllablePlayer getPlayer()
    {
        return player;
    }
}
