using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorControler : MonoBehaviour
{
    public Material obstacleMaterial;
    public Material panelMaterial;
    public Material transparentMaterial;

    //Obstacles
    public GameObject[] detectorPrefabs;
    public GameObject[] doorPrefabs;
    public GameObject[] lightingPrefabs;
    public GameObject[] ventilationPrefabs;
    public GameObject[] windowPrefabs;

    //Panels
    public GameObject[] panelPrefabs;

    public void Start()
    {
        //Obstacles
        detectorPrefabs = GameObject.FindGameObjectsWithTag("DetectorObstacle");
        doorPrefabs = GameObject.FindGameObjectsWithTag("DoorObstacle");
        lightingPrefabs = GameObject.FindGameObjectsWithTag("LightingObstacle");
        ventilationPrefabs = GameObject.FindGameObjectsWithTag("VentilationObstacle");
        windowPrefabs = GameObject.FindGameObjectsWithTag("WindowObstacle");

        //Panels
        panelPrefabs = GameObject.FindGameObjectsWithTag("Panel");

        //Change color
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {   
            //Obstacles
            for (int i = 0; i < detectorPrefabs.Length; i++)
            {
                detectorPrefabs[i].GetComponent<Renderer>().material.SetColor("_Color", obstacleMaterial.GetColor("_Color"));
                detectorPrefabs[i].GetComponent<Renderer>().material.SetFloat("_Metallic", obstacleMaterial.GetFloat("_Metallic"));
            }
            for (int i = 0; i < doorPrefabs.Length; i++)
            {
                doorPrefabs[i].GetComponent<Renderer>().material.SetColor("_Color", obstacleMaterial.GetColor("_Color"));
                doorPrefabs[i].GetComponent<Renderer>().material.SetFloat("_Metallic", obstacleMaterial.GetFloat("_Metallic"));
            }
            for (int i = 0; i < lightingPrefabs.Length; i++)
            {
                lightingPrefabs[i].GetComponent<Renderer>().material.SetColor("_Color", obstacleMaterial.GetColor("_Color"));
                lightingPrefabs[i].GetComponent<Renderer>().material.SetFloat("_Metallic", obstacleMaterial.GetFloat("_Metallic"));
            }
            for (int i = 0; i < ventilationPrefabs.Length; i++)
            {
                ventilationPrefabs[i].GetComponent<Renderer>().material.SetColor("_Color", obstacleMaterial.GetColor("_Color"));
                ventilationPrefabs[i].GetComponent<Renderer>().material.SetFloat("_Metallic", obstacleMaterial.GetFloat("_Metallic"));
            }
            for (int i = 0; i < windowPrefabs.Length; i++)
            {
                windowPrefabs[i].GetComponent<Renderer>().material.SetColor("_Color", obstacleMaterial.GetColor("_Color"));
                windowPrefabs[i].GetComponent<Renderer>().material.SetFloat("_Metallic", obstacleMaterial.GetFloat("_Metallic"));
            } 

            //Panels
            for (int i = 0; i < panelPrefabs.Length; i++)
            {
                panelPrefabs[i].GetComponent<Renderer>().material.SetColor("_Color", panelMaterial.GetColor("_Color"));
                panelPrefabs[i].GetComponent<Renderer>().material.SetFloat("_Metallic", panelMaterial.GetFloat("_Metallic"));
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        { 
            //Obstacles
            for (int i = 0; i < detectorPrefabs.Length; i++)
            {
                detectorPrefabs[i].GetComponent<Renderer>().material.SetColor("_Color", transparentMaterial.GetColor("_Color"));
                detectorPrefabs[i].GetComponent<Renderer>().material.SetFloat("_Metallic", transparentMaterial.GetFloat("_Metallic"));
            }
            for (int i = 0; i < doorPrefabs.Length; i++)
            {
                doorPrefabs[i].GetComponent<Renderer>().material.SetColor("_Color", transparentMaterial.GetColor("_Color"));
                doorPrefabs[i].GetComponent<Renderer>().material.SetFloat("_Metallic", transparentMaterial.GetFloat("_Metallic"));
            }
            for (int i = 0; i < lightingPrefabs.Length; i++)
            {
                lightingPrefabs[i].GetComponent<Renderer>().material.SetColor("_Color", transparentMaterial.GetColor("_Color"));
                lightingPrefabs[i].GetComponent<Renderer>().material.SetFloat("_Metallic", transparentMaterial.GetFloat("_Metallic"));
            }
            for (int i = 0; i < ventilationPrefabs.Length; i++)
            {
                ventilationPrefabs[i].GetComponent<Renderer>().material.SetColor("_Color", transparentMaterial.GetColor("_Color"));
                ventilationPrefabs[i].GetComponent<Renderer>().material.SetFloat("_Metallic", transparentMaterial.GetFloat("_Metallic"));
            }
            for (int i = 0; i < windowPrefabs.Length; i++)
            {
                windowPrefabs[i].GetComponent<Renderer>().material.SetColor("_Color", transparentMaterial.GetColor("_Color"));
                windowPrefabs[i].GetComponent<Renderer>().material.SetFloat("_Metallic", transparentMaterial.GetFloat("_Metallic"));
            }

            //Panels
            for (int i = 0; i < panelPrefabs.Length; i++)
            {
                panelPrefabs[i].GetComponent<Renderer>().material.SetColor("_Color", transparentMaterial.GetColor("_Color"));
                panelPrefabs[i].GetComponent<Renderer>().material.SetFloat("_Metallic", transparentMaterial.GetFloat("_Metallic"));
            }
        }
    }
}
