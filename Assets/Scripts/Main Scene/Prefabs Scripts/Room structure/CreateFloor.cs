using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateFloor : MonoBehaviour
{
    //Debuger
    public static ILogger debugFloor = new Logger(Debug.unityLogger.logHandler);

    //Prefab parameters
    private float fixedHeight = 0.08f;

    //Mouse parameters
    private MousePosition mousePositionScript;
    private Vector3 offset;

    //Floor state
    private bool floorIsDraged = false;
    private bool floorIsAtached = false;
    private bool isAtachedToFloorXSide = false;
    private bool isAtachedToFloorZSide = false;
    public bool isFrozeen = false;

    //Fixed positions
    private Vector3 position;
    private float positionX;
    private float positionZ;

    private void Start()
    {
        //Debugger
        debugFloor.logEnabled = false;

        SetPosition();

        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
    }

    public void SetPosition()
    {
        transform.position = new Vector3(transform.position.x, fixedHeight, transform.position.z);
    }

    private void OnMouseDown()
    {
        floorIsAtached = false;
        isAtachedToFloorXSide = false;
        isAtachedToFloorZSide = false;
        offset = transform.position - mousePositionScript.mousePosition;
    }

    private void OnMouseDrag()
    {
        if (!isFrozeen)
        {
            floorIsDraged = true;
            mousePositionScript.mouseDragsObject = true;

            //move anywhere
            if (!floorIsAtached)
            {
                transform.position = new Vector3(mousePositionScript.mousePosition.x + offset.x, fixedHeight, mousePositionScript.mousePosition.z + offset.z) ;
            }
            else if (floorIsAtached)
            {
                //move only on x axis
                if (isAtachedToFloorXSide)
                {
                    position = new Vector3(transform.position.x, gameObject.transform.localScale.y / 2, positionZ);
                    transform.position = position;
                }
                //move only on z axis
                else if (isAtachedToFloorZSide)
                {
                    position = new Vector3(positionX, gameObject.transform.localScale.y / 2, transform.position.z);
                    transform.position = position;
                }
            }
        }
    }

    private void OnMouseUp()
    {
        floorIsDraged = false;
        mousePositionScript.mouseDragsObject = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Floor collide with floor
        if (other.transform.CompareTag("Floor") && floorIsDraged)
        {
            floorIsAtached = true;

            GameObject floor = other.gameObject;

            //X values
            float floorSizeX = floor.transform.localScale.x;
            float floorPositionX = floor.transform.position.x;
            float rightFloorBound = floorPositionX - floorSizeX / 2 - gameObject.transform.localScale.x / 2;
            float leftFloorBound = floorPositionX + floorSizeX / 2 + gameObject.transform.localScale.x / 2;

            float distanceBetweenPositionsX = Mathf.Round(Mathf.Abs(gameObject.transform.position.x - floorPositionX));
            float distanceRegardingSizeX = ((gameObject.transform.localScale.x + floorSizeX) / 2);

            //Z values
            float floorSizeZ = floor.transform.localScale.z;
            float floorPositionZ = floor.transform.position.z;
            float backFloorBound = floorPositionZ - floorSizeZ / 2 - gameObject.transform.localScale.z / 2;
            float frontFloorBound = floorPositionZ + floorSizeZ / 2 + gameObject.transform.localScale.z / 2;

            float distanceBetweenPositionsZ = Mathf.Round(Mathf.Abs(gameObject.transform.position.z - floorPositionZ));
            float distanceRegardingSizeZ = ((gameObject.transform.localScale.z + floorSizeZ) / 2);

            if (distanceBetweenPositionsX >= distanceRegardingSizeX)
            {
                isAtachedToFloorZSide = true;
                //Collides from right side of floor
                if (gameObject.transform.position.x < floorPositionX)
                {
                    positionX = rightFloorBound;
                    mousePositionScript.mousePosition.x = rightFloorBound;
                    position = new Vector3(rightFloorBound, gameObject.transform.localScale.y / 2, transform.position.z);
                    gameObject.transform.position = position;
                }
                //Collides from left side of floor
                else if (gameObject.transform.position.x > floorPositionX)
                {
                    positionX = leftFloorBound;
                    mousePositionScript.mousePosition.x = leftFloorBound;
                    position = new Vector3(leftFloorBound, gameObject.transform.localScale.y / 2, transform.position.z);
                    gameObject.transform.position = position;
                }
            }

            else if (distanceBetweenPositionsZ >= distanceRegardingSizeZ)
            {
                isAtachedToFloorXSide = true;
                //Collides with floor from behind
                if (gameObject.transform.position.z < floorPositionZ)
                {
                    positionZ = backFloorBound;
                    position = new Vector3(transform.position.x, gameObject.transform.localScale.y / 2, backFloorBound);
                    mousePositionScript.mousePosition.z = backFloorBound;
                    gameObject.transform.position = new Vector3(transform.position.x, gameObject.transform.localScale.y / 2, backFloorBound);
                }
                //Collides with floor from front
                else if (gameObject.transform.position.z > floorPositionZ)
                {
                    positionZ = frontFloorBound;
                    position = new Vector3(transform.position.x, gameObject.transform.localScale.y / 2, frontFloorBound);
                    mousePositionScript.mousePosition.z = frontFloorBound;
                    gameObject.transform.position = new Vector3(transform.position.x, gameObject.transform.localScale.y / 2, frontFloorBound);
                }
            }
        }
    }

}
