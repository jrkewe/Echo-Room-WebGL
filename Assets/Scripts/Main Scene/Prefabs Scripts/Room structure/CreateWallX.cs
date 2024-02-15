using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateWallX : MonoBehaviour
{
    //Debuger
    public static ILogger debugWallZ = new Logger(Debug.unityLogger.logHandler);

    //Prefab parameters
    private float fixedHeight = 0.5f;

    //Mouse parameters
    private MousePosition mousePositionScript;
    private Vector3 offset;

    //Collision with the floor
    private bool wallCollided = false;
    private GameObject floor;
    private Vector3 position;

    //Wall state
    public bool isFrozeen = false;

    ////Position relative to the floor
    //private PointZero pointZeroScript;
    //private Vector2 position2D;
    //private Vector2 relativePosition;


    private void Start()
    {
        //Debugger
        debugWallZ.logEnabled = false;

        SetPosition();
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
        //pointZeroScript = GameObject.Find("User Input Manager").GetComponent<PointZero>();
    }

    public void SetPosition()
    {
        transform.position = new Vector3(transform.position.x, fixedHeight, transform.position.z);
    }

    private void OnMouseDown()
    {
        offset = transform.position - mousePositionScript.mousePosition;
        wallCollided = false;
        //if (pointZeroScript.pointZero!=Vector2.zero) {
        //    position2D = new Vector2(transform.position.x, transform.position.z);
        //    relativePosition = position2D - pointZeroScript.pointZero;
        //    Debug.Log("Relative position: " + relativePosition);
        //}
    }

    private void OnMouseDrag()
    {
        if (!isFrozeen)
        {
            mousePositionScript.mouseDragsObject = true;

            fixedHeight = transform.localScale.y / 2;

            //WallX draged anywhere
            if (!wallCollided)
            {
                transform.position = new Vector3(mousePositionScript.mousePosition.x + offset.x, fixedHeight, mousePositionScript.mousePosition.z + offset.z);
            }

            //Drag wall on the edge of the floor 
            //z coordinate is fixed
            else if (floor != null && wallCollided)
            {
                float floorSizeX = floor.transform.localScale.x;
                float floorPositionX = floor.transform.position.x;
                float backBound = floorPositionX - (floorSizeX / 2) + (transform.localScale.x / 2);
                float frontBound = floorPositionX + (floorSizeX / 2) - (transform.localScale.x / 2);

                //WallX hits back side of floor edge
                if (transform.position.x < backBound)
                {
                    transform.position = new Vector3(backBound, fixedHeight, transform.position.z);
                }

                //WallX hits front side of floor edge
                else if (transform.position.x > frontBound)
                {
                    transform.position = new Vector3(frontBound, fixedHeight, transform.position.z);
                }

                //Drag WallX while its on the edge of the floor
                else if (transform.position.x <= frontBound && transform.position.x >= backBound)
                {
                    mousePositionScript.mousePosition.x = Mathf.Clamp(mousePositionScript.mousePosition.x, backBound, frontBound);
                    transform.position = new Vector3(mousePositionScript.mousePosition.x, fixedHeight, transform.position.z);
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
        //WallZ collide with floor - fix z coordinate
        if (other.transform.CompareTag("Floor"))
        {
            wallCollided = true;
            floor = other.gameObject;

            float floorSizeZ = floor.transform.localScale.z;
            float floorPositionZ = floor.transform.position.z;
            float rightBound = floorPositionZ + floorSizeZ / 2 - 0.125f;
            float leftBound = floorPositionZ - floorSizeZ / 2 + 0.125f;

            //Collides with floor from right side
            if (transform.position.z > floorPositionZ)
            {
                mousePositionScript.mousePosition.z = rightBound;
                transform.position = new Vector3(transform.position.x, transform.localScale.y / 2, rightBound);
            }
            //Collides with floor from left side
            else if (transform.position.z < floorPositionZ)
            {
                mousePositionScript.mousePosition.z = leftBound;
                transform.position = new Vector3(transform.position.x, transform.localScale.y / 2, leftBound);
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        wallCollided = false;
    }

}
