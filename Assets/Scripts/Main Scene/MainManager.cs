using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    //Room structure
    public GameObject[] floorPrefabs;
    public GameObject[] wallXPrefabs;
    public GameObject[] wallZPrefabs;

    //Obstacles
    public GameObject[] detectorPrefabs;
    public GameObject[] doorPrefabs;
    public GameObject[] lightingPrefabs;
    public GameObject[] ventilationPrefabs;
    public GameObject[] windowPrefabs;

    public void CreateInstance()
    {
        //Room structure
        floorPrefabs = GameObject.FindGameObjectsWithTag("Floor");
        wallXPrefabs = GameObject.FindGameObjectsWithTag("WallX");
        wallZPrefabs = GameObject.FindGameObjectsWithTag("WallZ");
        //Obstacles
        detectorPrefabs = GameObject.FindGameObjectsWithTag("DetectorObstacle");
        doorPrefabs = GameObject.FindGameObjectsWithTag("DoorObstacle");
        lightingPrefabs = GameObject.FindGameObjectsWithTag("LightingObstacle");
        ventilationPrefabs = GameObject.FindGameObjectsWithTag("VentilationObstacle");
        windowPrefabs = GameObject.FindGameObjectsWithTag("WindowObstacle");

        if (Instance != null)
        {
            //Room Structure
            for (int i = 0; i < floorPrefabs.Length;) {
                Destroy(floorPrefabs[i]);
                return;
            }
            for (int i = 0; i < wallXPrefabs.Length;)
            {
                Destroy(wallXPrefabs[i]);
                return;
            }
            for (int i = 0; i < wallZPrefabs.Length;)
            {
                Destroy(wallZPrefabs[i]);
                return;
            }

            //Obstacles
            for (int i = 0; i < detectorPrefabs.Length;)
            {
                Destroy(detectorPrefabs[i]);
                return;
            }
            for (int i = 0; i < doorPrefabs.Length;)
            {
                Destroy(doorPrefabs[i]);
                return;
            }
            for (int i = 0; i < lightingPrefabs.Length;)
            {
                Destroy(lightingPrefabs[i]);
                return;
            }
            for (int i = 0; i < ventilationPrefabs.Length;)
            {
                Destroy(ventilationPrefabs[i]);
                return;
            }
            for (int i = 0; i < windowPrefabs.Length;)
            {
                Destroy(windowPrefabs[i]);
                return;
            }
        }
        Instance = this;
        //Room Structure
        for (int i = 0; i < floorPrefabs.Length; i++)
        {
            DontDestroyOnLoad(floorPrefabs[i]);
        }
        for (int i = 0; i < wallXPrefabs.Length; i++)
        {
            DontDestroyOnLoad(wallXPrefabs[i]);
        }
        for (int i = 0; i < wallZPrefabs.Length; i++)
        {
            DontDestroyOnLoad(wallZPrefabs[i]);
        }

        //Obstacles
        for (int i = 0; i < detectorPrefabs.Length; i++)
        {
            DontDestroyOnLoad(detectorPrefabs[i]);
        }
        for (int i = 0; i < doorPrefabs.Length; i++)
        {
            DontDestroyOnLoad(doorPrefabs[i]);
        }
        for (int i = 0; i < lightingPrefabs.Length; i++)
        {
            DontDestroyOnLoad(lightingPrefabs[i]);
        }
        for (int i = 0; i < ventilationPrefabs.Length; i++)
        {
            DontDestroyOnLoad(ventilationPrefabs[i]);
        }
        for (int i = 0; i < windowPrefabs.Length; i++)
        {
            DontDestroyOnLoad(windowPrefabs[i]);
        }

    }

}
