using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public int numberOfPlatforms = 8;
    public float levelHeight = 3f;
    public float minY = -5f;
    public float maxY = 1.5f;
    public float minX = 2f;
    public float maxX = 4f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = new Vector3(-14, -3);
        for(int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(-levelHeight, levelHeight);
            spawnPosition.x += Random.Range(minX, maxX);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
