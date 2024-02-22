
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldInputLookAtCamera : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    private void Start()
    {
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera != null) {
            transform.LookAt(mainCamera.GetComponent<Transform>(), Vector3.up);
        }
        else if (mainCamera == null) 
        {
            FindCamera();
        }
    }

    void FindCamera() 
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
}
