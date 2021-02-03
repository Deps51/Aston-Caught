using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prison : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        if (collision.gameObject.tag == "Escapee")
        {
            Debug.Log("player is escapee");
            Escapee player = collision.gameObject.GetComponent<Escapee>();
            if(player.isCaptured())
            //if(true)
            {
                Debug.Log("prsion player.iscaptured()");
                //player is now out of the game
                player.setImprisoned();
            }
        }
        else if(collision.gameObject.tag == "GrimReaper")
        {
            GrimReaper reaper = collision.gameObject.GetComponent<GrimReaper>();
            if (reaper.isCarryingPlayer())
            {
                Debug.Log("prsion player.iscaptured() ELSE");
                reaper.froceImprisonment();
            }
        }
        
    }
}
