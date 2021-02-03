using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : Generator
{
    private static List<Task> tasks = new List<Task>();
    private static List<PowerUp> powerUps = new List<PowerUp>();


    private static System.Random rand = new System.Random();

    public static void addPowerUp(PowerUp pu)
    {
        powerUps.Add(pu);
    }

    public static PowerUp GetPowerUp()
    {
        int index = rand.Next(powerUps.Count);
        Debug.Log("index: " + index);
        Debug.Log(powerUps[index]);
        return powerUps[index];
    }

    public static Task getTask()
    {
        Task task = tasks[tasks.Count - 1];
        tasks.RemoveAt(tasks.Count - 1);
        return task;
    }

    // Start is called before the first frame update
    void Start()
    {
        tasks.Add(this);
    }
}
