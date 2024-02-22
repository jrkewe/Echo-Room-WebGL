using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorLight : MonoBehaviour
{

    private GameObject floor; 
    
    void Start()
    {
        if (floor == null)
        {
            floor = GameObject.FindWithTag("Floor");
        }
        if (floor != null)
        {
            transform.position = new Vector3(floor.transform.position.x, 1.5f, floor.transform.position.z);
        }
    }
}
