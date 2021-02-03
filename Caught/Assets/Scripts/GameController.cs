using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class GameController : NetworkManager
{
    //ec2-18-218-176-113.us-east-2.compute.amazonaws.com

    //ControllablePlayer[] total_players = new ControllablePlayer[10];
    //List<GameObject> players = new List<GameObject>();
    //GameObject[] players = new GameObject[10];
    public Transform escpaeeSpawn;
    public Transform reaperSpawn;
    int totalPlayers = 0;

    int totalEscapee = 0;
    int totalReaper = 0;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        //base.OnServerAddPlayer(conn);
        Debug.Log("Client player added Connected");
        Debug.Log("ID: " + conn.connectionId);

        GameObject player;

        if(totalPlayers == 1 || totalPlayers == 5)
        {
            totalReaper ++;
            player = Instantiate(Resources.Load("TempPlayer 2"), reaperSpawn.position, Quaternion.identity) as GameObject;
            player.name += totalPlayers;
            //var sprite = Resources.Load<Sprite>("Sprites/sprite01");
            Sprite s = Resources.Load<Sprite>("Players/Reapers/Reaper" + totalReaper);
            //player.GetComponent<SpriteRenderer>().sprite = s;
            player.GetComponent<GrimReaper>().setSprite("Players/Reapers/Reaper" + totalReaper);
        }
        else
        {
            totalEscapee ++;
            player = Instantiate(Resources.Load("TempPlayer"), escpaeeSpawn.position, Quaternion.identity) as GameObject;
            player.name += totalPlayers;
            Sprite s = Resources.Load<Sprite>("Players/Escapees/Escapee" + totalEscapee);
            //player.GetComponent<SpriteRenderer>().sprite = s;
            player.GetComponent<Escapee>().setSprite("Players/Escapees/Escapee" + totalEscapee);
        }
        
        totalPlayers ++;
        NetworkServer.AddPlayerForConnection(conn, player);

        
    }


    public override void OnServerDisconnect(NetworkConnection conn)
    {
        totalPlayers--;
        base.OnServerDisconnect(conn);
    }

    public override void OnStartServer()
    {
        
        base.OnStartServer();
        //Physics2D.IgnoreLayerCollision(8, 9);
    }

}
