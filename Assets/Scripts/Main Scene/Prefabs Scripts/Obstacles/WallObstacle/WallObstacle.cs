using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObstacle : MonoBehaviour
{
    //Debuger
    public static ILogger debugVerticalObstacle = new Logger(Debug.unityLogger.logHandler);

    //Prefab parameters
    private float height = 1.0f;
    public GameObject Plane;

    //Mouse parameters
    private MousePosition mousePositionScript;
    private Vector3 offset;

    //Collision with the floor - ? collision with wall - ?
    private bool obstacleCollided = false;
    private Vector3 position;
    private GameObject floor;
    private GameObject wall;

    //Wall state
    public bool isFrozeen = false;

    //Position relative to the floor
    private PointZero pointZeroScript;
    private Vector2 position2D;
    public Vector2 relativePosition;

    //Bounds
    public float rightBound;
    public float leftBound;
    public float frontBound;
    public float backBound;


    //Canvas InputField object
    private GameObject canvasIF;
    private float inputFieldFadesDelay = 2.0f;


    private void Start()
    {
        //Debugger
        debugVerticalObstacle.logEnabled = false;

        SetDimensions();
        SetPosition();

        canvasIF = transform.GetChild(0).gameObject;
        canvasIF.SetActive(false);

        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
        pointZeroScript = GameObject.Find("User Input Manager").GetComponent<PointZero>();

    }

    public void SetDimensions()
    {
        if (GameObject.FindWithTag("WallX"))
        {
            height = GameObject.FindWithTag("WallX").transform.localScale.y;
        }
        else if (GameObject.FindWithTag("WallZ"))
        {
            height = GameObject.FindWithTag("WallZ").transform.localScale.y;
        }
        transform.localScale = new Vector3(1.00f, height, 1.00f);
    }

    public void SetPosition()
    {
        height = transform.localScale.y / 2;
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }
    public void Reposition()
    {
        transform.position = new Vector3(pointZeroScript.pointZero.x + relativePosition.x + transform.localScale.x / 2, height, pointZeroScript.pointZero.y + relativePosition.y + transform.localScale.z / 2);
    }

    private void OnMouseDown()
    {
        offset = transform.position - mousePositionScript.mousePosition;
        obstacleCollided = false;
        if (pointZeroScript.pointZero != Vector2.zero)
        {
            float leftSide = transform.position.z - transform.localScale.z / 2;
            float backSide = transform.position.x - transform.localScale.x / 2;
            position2D = new Vector2(backSide, leftSide);
            relativePosition = position2D - pointZeroScript.pointZero;
        }

    }

    private void OnMouseDrag()
    {
        if (pointZeroScript.pointZero != Vector2.zero)
        {
            float leftSide = transform.position.z - (transform.localScale.z / 2);
            float backSide = transform.position.x - (transform.localScale.x / 2);
            position2D = new Vector2(backSide, leftSide);
            relativePosition = position2D - pointZeroScript.pointZero;
        }

        if (!isFrozeen)
        {
            mousePositionScript.mouseDragsObject = true;
            height = transform.localScale.y / 2;

            //Wall obstacle draged anywhere
            if (!obstacleCollided)
            {
                transform.position = new Vector3(mousePositionScript.mousePosition.x + offset.x, height, mousePositionScript.mousePosition.z + offset.z);
            }
            //Drag Wall obstacle around the floor
            else if (floor != null && obstacleCollided)
            {
                float floorSizeX = floor.transform.localScale.x;
                float floorPositionX = floor.transform.position.x;
                backBound = floorPositionX - (floorSizeX / 2) + (transform.localScale.x / 2) + 0.25f;
                frontBound = floorPositionX + (floorSizeX / 2) - (transform.localScale.x / 2) - 0.25f;

                float floorSizeZ = floor.transform.localScale.z;
                float floorPositionZ = floor.transform.position.z;
                rightBound = floorPositionZ + floorSizeZ / 2 - transform.localScale.z / 2 - 0.25f;
                leftBound = floorPositionZ - floorSizeZ / 2 + transform.localScale.z / 2 + 0.25f;

                //Edges
                //Obstacle moves on right or left edge of the floor but doesnt touch front or back edge
                if ((transform.position.z == rightBound || transform.position.z == leftBound) && !(transform.position.x == frontBound|| transform.position.x == backBound))
                {
                    mousePositionScript.mousePosition.x = Mathf.Clamp(mousePositionScript.mousePosition.x, backBound, frontBound);
                    transform.position = new Vector3(mousePositionScript.mousePosition.x, height, transform.position.z);
                    if (transform.position.z == rightBound)
                    {
                        Plane.transform.eulerAngles = new Vector3(90, 180, 0);
                        Plane.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0.49f);
                    }
                    else if (transform.position.z == leftBound)
                    {
                        Plane.transform.eulerAngles = new Vector3(90, 0, 0);
                        Plane.GetComponent<RectTransform>().localPosition = new Vector3(0, 0,-0.49f);
                    }
                }
                //Obstacle moves on front or back edge of the floor but doesnt touch right or left edge
                else if ((transform.position.x == frontBound || transform.position.x == backBound) && !(transform.position.z == rightBound || transform.position.z == leftBound))
                {
                    mousePositionScript.mousePosition.z = Mathf.Clamp(mousePositionScript.mousePosition.z, leftBound , rightBound );
                    transform.position = new Vector3(transform.position.x, height, mousePositionScript.mousePosition.z);

                    if (transform.position.x == frontBound)
                    {
                        Plane.transform.eulerAngles = new Vector3(90, 270, 0);
                        Plane.GetComponent<RectTransform>().localPosition = new Vector3(0.49f, 0, 0);
                    }
                    else if (transform.position.x == backBound)
                    {
                        Plane.transform.eulerAngles = new Vector3(90, 90, 0);
                        Plane.GetComponent<RectTransform>().localPosition = new Vector3(-0.49f, 0,0);
                    }
                }

                //Corners
                else
                {
                    //Front/left corner
                    if (transform.position.x == frontBound && transform.position.z == leftBound)
                    {
                        mousePositionScript.mousePosition.x = Mathf.Clamp(mousePositionScript.mousePosition.x, backBound , frontBound );
                        mousePositionScript.mousePosition.z = Mathf.Clamp(mousePositionScript.mousePosition.z, leftBound , rightBound);
                        transform.position = new Vector3(mousePositionScript.mousePosition.x, height, mousePositionScript.mousePosition.z);
                    }
                    //Left/back corner
                    else if (transform.position.z == leftBound && transform.position.x == backBound)
                    {
                        mousePositionScript.mousePosition.x = Mathf.Clamp(mousePositionScript.mousePosition.x, backBound, frontBound);
                        mousePositionScript.mousePosition.z = Mathf.Clamp(mousePositionScript.mousePosition.z, leftBound, rightBound);
                        transform.position = new Vector3(mousePositionScript.mousePosition.x, height, mousePositionScript.mousePosition.z);
                    }
                    //Back/right corner
                    else if (transform.position.x == backBound && transform.position.z == rightBound)
                    {
                        mousePositionScript.mousePosition.x = Mathf.Clamp(mousePositionScript.mousePosition.x, backBound, frontBound);
                        mousePositionScript.mousePosition.z = Mathf.Clamp(mousePositionScript.mousePosition.z, leftBound, rightBound);
                        transform.position = new Vector3(mousePositionScript.mousePosition.x, height, mousePositionScript.mousePosition.z);
                    }
                    //Right/front corner
                    else if (transform.position.z == rightBound && transform.position.x == frontBound)
                    {
                        mousePositionScript.mousePosition.x = Mathf.Clamp(mousePositionScript.mousePosition.x, backBound, frontBound);
                        mousePositionScript.mousePosition.z = Mathf.Clamp(mousePositionScript.mousePosition.z, leftBound, rightBound);
                        transform.position = new Vector3(mousePositionScript.mousePosition.x, height, mousePositionScript.mousePosition.z);
                    }
                }
            }
        }
    }

    private void OnMouseUp()
    {
        mousePositionScript.mouseDragsObject = false;
    }

    private void OnMouseOver()
    {
        StopCoroutine(WaitForSeconds());
        //Canvas Input Field is turn on
        canvasIF.SetActive(true);
    }
    private void OnMouseExit()
    {
        //Canvas Input Field is turn on
        if (mousePositionScript.mouseDragsObject == false)
        {
            StartCoroutine(WaitForSeconds());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Vertical obstacle collide with floor
        if (other.transform.CompareTag("Floor"))
        {
            obstacleCollided = true;
            floor = other.gameObject;
            float floorSizeZ = floor.transform.localScale.z;
            float floorPositionZ = floor.transform.position.z;
            rightBound = floorPositionZ + floorSizeZ / 2 - transform.localScale.z / 2 - 0.25f;
            leftBound = floorPositionZ - floorSizeZ / 2 + transform.localScale.z / 2 + 0.25f;


            float floorSizeX = floor.transform.localScale.x;
            float floorPositionX = floor.transform.position.x;
            frontBound = floorPositionX + floorSizeX / 2 - transform.localScale.x / 2 - 0.25f;
            backBound = floorPositionX - floorSizeX / 2 + transform.localScale.x / 2 + 0.25f;

            //Collides with floor from right side
            if (transform.position.z > rightBound)
            {
                mousePositionScript.mousePosition.z = rightBound;
                transform.position = new Vector3(transform.position.x, transform.localScale.y / 2, rightBound);
            }
            //Collides with floor from left side
            else if (transform.position.z < leftBound)
            {
                mousePositionScript.mousePosition.z = leftBound;
                transform.position = new Vector3(transform.position.x, transform.localScale.y / 2, leftBound);
            }
            //Collides from back of floor
            else if (transform.position.x < backBound)
            {
                mousePositionScript.mousePosition.x = backBound;
                transform.position = new Vector3(backBound, transform.localScale.y / 2, transform.position.z);
            }
            //Collides from front of floor
            else if (transform.position.x > frontBound)
            {
                mousePositionScript.mousePosition.x = frontBound;
                transform.position = new Vector3(frontBound, transform.localScale.y / 2, transform.position.z);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        obstacleCollided = false;
    }

    //Input fields fades delay
    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(inputFieldFadesDelay);
        canvasIF.SetActive(false);
    }

}