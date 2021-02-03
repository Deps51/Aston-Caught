using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Generator : NetworkBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;
        Debug.Log("collision player: " + player + " name: " + player.name);

        if (player.GetComponent<Escapee>() != null)
        {
            //activate task button
            player.GetComponent<Escapee>().activateTaskBtn();
            player.GetComponent<Escapee>().setGenerator(this);
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        GameObject player = collision.gameObject;
        if (player.GetComponent<Escapee>() != null)
        {
            //deActivate task button
            player.GetComponent<Escapee>().deActivateTaskBtn();
        }
    }
}
