using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldInputLookAtCamera : MonoBehaviour
{
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCamera.GetComponent<Transform>(), Vector3.up);
    }
}
