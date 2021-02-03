using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : PowerUp
{
    public static Dictionary<int, Trap> traps = new Dictionary<int, Trap>();
    public GameObject trapPrefab;
    public ControllablePlayer targetPlayer = null;
    protected GameObject placedTrap;

    protected int trapIndex;

    public void placeTrap()
    {
        //placedTrap = Instantiate(trapPrefab, getPlayer().transform.position, Quaternion.identity);
        //placedTrap.AddComponent<this>();
    }

    public static int addTrap(Trap trap)
    {
        traps.Add(traps.Count, trap);
        return traps.Count - 1;
    }

    public int getTrapIndex()
    {
        return trapIndex;
    }

    public static Trap getTrap(int index)
    {
        return traps[index];
    }

    public ControllablePlayer getTargetPlayer()
    {
        return targetPlayer;
    }

    public void setTargetPlayer(ControllablePlayer targetPlayer)
    {
        this.targetPlayer = targetPlayer;
    } 

    public abstract void activateTrap();

}
