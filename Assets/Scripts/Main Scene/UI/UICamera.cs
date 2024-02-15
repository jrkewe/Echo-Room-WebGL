using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    //Mouse input
    private float horizontalInput;
    private float verticalInput;

    private CameraMove mainCamera;
    public GameObject objectCameraOrbitsAround;

    //Reposition camera
    private Vector3 startPosition = new Vector3(999, 141, 103);
    private Vector3 startAngle = new Vector3(10, 46, 0);

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<CameraMove>();
        RepositionCamera();
    }

    void Update()
    {
        //Right click - rotation
        if (Input.GetMouseButton(1)) {
            horizontalInput = mainCamera.horizontalInput;
            verticalInput = mainCamera.verticalInput;

            transform.RotateAround(objectCameraOrbitsAround.transform.position, Vector3.up, horizontalInput);
            transform.RotateAround(objectCameraOrbitsAround.transform.position, Vector3.right, -verticalInput);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }
        //Mouse release - stop moving
        else
        {
            transform.Rotate(0,0,0);
        }

        //R key - repositioning
        if (mainCamera.cameraIsRestarted) 
        {
            RepositionCamera();
        }

    }

    private void RepositionCamera()
    {
        transform.position = startPosition;
        transform.eulerAngles = startAngle;
    }
}
