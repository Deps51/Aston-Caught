using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenControlScript : MonoBehaviour
{

    public GameObject loadingScreenObj;
    public GameObject loadingBar;
    public TaskButton taskBtn;
    public Slider slider;
    public float count;
    public float countIncrease;

    private ControllablePlayer player;

    private bool isActive = false;

    public void Start()
    {
        countIncrease = 0.001f;
    }

    public void FixedUpdate()
    {
        if (isActive)
        {
            slider.value += countIncrease;
            if(slider.value >= 1f)
            {
                isActive = false;
                Debug.Log("finished in update");
                //player.setPowerUp(Task.GetPowerUp());
                player.generatorCompleted();
                taskBtn.exitTask();
            }
        }
    }



    public void setPlayer(ControllablePlayer player)
    {
        this.player = player;
    }

    

    AsyncOperation async;

    //This is the function that loads the bar
    IEnumerator LoadingScreen()
    {
        loadingScreenObj.SetActive(true);

        count = 0;
        while (count<1f)
        {
            slider.value = count;
            //To increase the speed, increase this number
            count = count + countIncrease;
                
            yield return null;
        }

        //loading bar has finished
        Debug.Log("finished");
        player.setPowerUp(Task.GetPowerUp());
    }

    public void reset()
    {
        count = 0;
        slider.value = 0;
        isActive = false;
        Debug.Log("RESETT");
        //OnDisable();
        //loadingBar.active = false;
        taskBtn.exitTask();

    }

    private void OnEnable()
    {
        Debug.Log("ENABLEDD");
        //StartCoroutine(LoadingScreen());
        loadingScreenObj.SetActive(true);
        slider.value = 0;
        isActive = true;
    }

    private void OnDisable()
    {
        Debug.Log("DISBALED");
    }
}
