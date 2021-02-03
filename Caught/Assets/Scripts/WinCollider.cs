using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCollider : MonoBehaviour
{
    Boolean winText = false;
    private GUIStyle guiStyle = new GUIStyle();

    private void OnTriggerEnter(Collider collision)
    {
        if(RequiredTasks.completedTasks >= RequiredTasks.totalTasks)
        {
            Debug.Log("Escapee has reached the win zone");
            winText = true;
            collision.gameObject.GetComponent<ControllablePlayer>().endGame();
            
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        winText = false;
    }

    void OnGUI()
    {
        guiStyle.fontSize = 40;
        if (winText)
        {
            GUI.Label(new Rect(15, 250, 100, 100), "An Escapee has reached the win zone!", guiStyle);
        }
    }
}