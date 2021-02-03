using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class RequiredTasks : Generator
{
    public static int totalTasks = 28;
    public static int completedTasks = 0;

    public GameObject winCollider;

    public int completeTask()
    {
        completedTasks ++;
        //cmdCompleteTask();

        return completedTasks;
    }

    public void win()
    {
        GameObject.FindGameObjectWithTag("Win").SetActive(true);
        Debug.Log("escapee now open");
    }

    /*[Command]
    public void cmdCompleteTask()
    {
        completedTasks++;

        if(completedTasks >= totalTasks)
        {
            //escape can now open
            winCollider.SetActive(true);
            Debug.Log("escapee now open");
        }
    }*/
}
