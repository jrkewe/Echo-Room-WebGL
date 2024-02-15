using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallZ : MonoBehaviour
{
    //Debuger
    private static ILogger debugWallX = new Logger(Debug.unityLogger.logHandler);

    //Prefab parameters
    private float fixedHeight = 0.5f;

    //Mouse parameters
    private MousePosition mousePositionScript;
    private Vector3 offset;

    //Collision with the floor
    private GameObject floor;
    private bool wallCollided = false;

    //Wall state
    public bool isFrozeen = false;

    private void Start()
    {
        //Debugger
        debugWallX.logEnabled = false;

        SetPosition();
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
    }

    public void SetPosition()
    {
        transform.position = new Vector3(transform.position.x, fixedHeight, transform.position.z);
    }

    private void OnMouseDown() 
    {
        offset = transform.position - mousePositionScript.mousePosition;
        wallCollided = false;
    }

    private void OnMouseDrag()
    {
        if (!isFrozeen)
        {
            mousePositionScript.mouseDragsObject = true;
            fixedHeight = transform.localScale.y / 2;

            //WallZ draged anywhere
            if (!wallCollided)
            {
                transform.position = new Vector3(mousePositionScript.mousePosition.x + offset.x, fixedHeight, mousePositionScript.mousePosition.z + offset.z);
            }

            //Drag wall on the edge of the floor 
            //x coordinate is fixed
            else if (floor != null && wallCollided)
            {
                float floorSizeZ = floor.transform.localScale.z;
                float floorPositionZ = floor.transform.position.z;
                float backFloorBound = floorPositionZ - (floorSizeZ / 2) + (transform.localScale.z / 2);
                float frontFloorBound = floorPositionZ + (floorSizeZ / 2) - (transform.localScale.z / 2);

                //WallZ hits back of floor
                if (transform.position.z < backFloorBound)
                {
                    transform.position = new Vector3(transform.position.x, fixedHeight, backFloorBound);
                }

                //WallZ hits front of floor
                else if (transform.position.z > frontFloorBound)
                {
                    transform.position = new Vector3(transform.position.x, fixedHeight, frontFloorBound);
                }

                //Drag WallZ while its on the edge of the floor
                else if (transform.position.z <= frontFloorBound && transform.position.z >= backFloorBound)
                {
                    mousePositionScript.mousePosition.z = Mathf.Clamp(mousePositionScript.mousePosition.z, backFloorBound, frontFloorBound);
                    transform.position = new Vector3(transform.position.x, fixedHeight, mousePositionScript.mousePosition.z);
                }
            }
        }
    }

    private void OnMouseUp()
    {
        mousePositionScript.mouseDragsObject = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //WallX collide with floor - fix x coordinate
        if (other.transform.CompareTag("Floor"))
        {
            wallCollided = true;
            floor = other.gameObject;

            float floorSizeX = floor.transform.localScale.x;
            float floorPositionX = floor.transform.position.x;
            float backBound = floorPositionX - floorSizeX / 2 + 0.125f;
            float frontBound = floorPositionX + floorSizeX / 2 - 0.125f;

            //Collides from back of floor
            if (transform.position.x < floorPositionX)
            {
                mousePositionScript.mousePosition.x = backBound;
                transform.position = new Vector3(backBound, transform.localScale.y / 2, transform.position.z);
            }
            //Collides from front of floor
            else if (transform.position.x > floorPositionX)
            {
                mousePositionScript.mousePosition.x = frontBound;
                transform.position = new Vector3(frontBound, transform.localScale.y / 2, transform.position.z);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        wallCollided = false;
    }
}

