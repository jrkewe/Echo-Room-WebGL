using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainPanelManager : MonoBehaviour
{
    public static MainPanelManager Instance;

    //Panels
    public GameObject[] panelPrefabs;


    public void CreateInstance()
    {
        //Panels
        panelPrefabs = GameObject.FindGameObjectsWithTag("Panel");

        if (Instance != null)
        {
            for (int i = 0; i < panelPrefabs.Length;)
            {
                Destroy(panelPrefabs[i]);
                return;
            }
        }
        Instance = this;
        
        //Panel
        for (int i = 0; i < panelPrefabs.Length; i++)
        {
            DontDestroyOnLoad(panelPrefabs[i]);
        }

    }
}
