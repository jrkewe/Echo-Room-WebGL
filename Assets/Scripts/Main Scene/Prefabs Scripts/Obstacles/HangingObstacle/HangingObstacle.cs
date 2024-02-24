using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HangingObstacle : MonoBehaviour
{

    //Debuger
    public static ILogger debugCeilinglObstacle = new Logger(Debug.unityLogger.logHandler);

    //Prefab parameters
    private float height;
    private float thickness = 0.16f;

    //Mouse parameters
    private MousePosition mousePositionScript;
    private CameraMove cameraMoveScript;
    private Vector3 offset;
    private float rightMouseButtonDraggingSpeed = 14;

    //Collision with the floor
    private Vector3 position;
    private float positionX;
    private float positionZ;
    private GameObject wallObstacle;

    //Ceiling obstacle state
    public bool isFrozeen = false;
    private bool canGoBack = true;
    private bool hangingObstacleCollided = false;
    private bool ceilingObstacleCollidedWithLeftOrRightSide = false;
    private bool ceilingObstacleCollidedWithFrontOrBackSide = false;
    public bool isDraggedRightMouseButton = false;

    //Position relative to the floor
    private PointZero pointZeroScript;
    private Vector2 position2D;
    public Vector2 relativePosition;
    public float relativeHeight;

    //Position relative to the ceiling
    private float ceilingRelativeHeight;

    //Canvas InputField object
    private GameObject canvasIF;
    private float inputFieldFadesDelay = 2.0f;

    private void Start()
    {
        //Debugger
        debugCeilinglObstacle.logEnabled = false;

        SetDimensions();
        SetPosition();

        canvasIF = transform.GetChild(0).gameObject;
        canvasIF.SetActive(false);

        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
        cameraMoveScript = GameObject.Find("Main Camera").GetComponent<CameraMove>();
        pointZeroScript = GameObject.Find("User Input Manager").GetComponent<PointZero>();
    }


    public void SetDimensions()
    {
        transform.localScale = new Vector3(1.0f, 0.16f, 1.0f);
    }

    public void SetPosition()
    {
        if (GameObject.FindWithTag("WallX"))
        {
            height = GameObject.FindWithTag("WallX").transform.localScale.y - thickness/2;
        }
        else if (GameObject.FindWithTag("WallZ"))
        {
            height = GameObject.FindWithTag("WallZ").transform.localScale.y - thickness / 2;
        }
        else
        {
            height = transform.localScale.y / 2;
        }
        ceilingRelativeHeight = height;
        position = new Vector3(transform.position.x, height, transform.position.z);
        transform.position = position;
    }

    public void Reposition()
    {
        transform.position = new Vector3(pointZeroScript.pointZero.x + relativePosition.x + transform.localScale.x / 2, ceilingRelativeHeight - relativeHeight, pointZeroScript.pointZero.y + relativePosition.y + transform.localScale.z / 2);
    }

    private void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            isFrozeen = true;
        }

        hangingObstacleCollided = false;
        ceilingObstacleCollidedWithLeftOrRightSide = false;
        ceilingObstacleCollidedWithFrontOrBackSide = false;

        offset = transform.position - mousePositionScript.mousePosition;

        if (pointZeroScript.pointZero != Vector2.zero)
        {
            float leftSide = transform.position.z - transform.localScale.z / 2;
            float backSide = transform.position.x - transform.localScale.x / 2;
            position2D = new Vector2(backSide, leftSide);
            relativePosition = position2D - pointZeroScript.pointZero;
            relativeHeight = ceilingRelativeHeight - transform.position.y;
        }

    }

    //Dragging with left mouse button
    private void OnMouseDrag()
    {
        //Floor exists
        if (pointZeroScript.pointZero != Vector2.zero)
        {
            float leftSideOfCeilingObstacle = transform.position.z - transform.localScale.z / 2;
            float backSideOfCeilingObstacle = transform.position.x - transform.localScale.x / 2;
            position2D = new Vector2(backSideOfCeilingObstacle, leftSideOfCeilingObstacle);
            relativePosition = position2D - pointZeroScript.pointZero;
            relativeHeight = ceilingRelativeHeight - transform.position.y;
        }

        //Is not lock
        if (!isFrozeen)
        {
            mousePositionScript.mouseDragsObject = true;

            //Ceiling obstacle range of movement above floor
            float frontBound = pointZeroScript.frontBound - (transform.localScale.x / 2);
            float floorRightBound = pointZeroScript.rightBound - (transform.localScale.z / 2);
            float backBound = pointZeroScript.pointZero.x + (transform.localScale.x / 2);
            float floorLeftBound = pointZeroScript.pointZero.y + (transform.localScale.z / 2);

            //Floor exists
            if (pointZeroScript.pointZero != Vector2.zero) {
               
                //Limit movement to floor surface
                if ((transform.position.x <= frontBound) && (transform.position.x >= backBound) && (transform.position.z <= floorRightBound) && (transform.position.z >= floorLeftBound))
                {
                    canGoBack = false;

                    mousePositionScript.mousePosition.x = Mathf.Clamp(mousePositionScript.mousePosition.x, backBound, frontBound);
                    mousePositionScript.mousePosition.z = Mathf.Clamp(mousePositionScript.mousePosition.z, floorLeftBound, floorRightBound);
                    
                    transform.position = new Vector3(mousePositionScript.mousePosition.x, transform.position.y, mousePositionScript.mousePosition.z);
                }

                else if(canGoBack)
                { 
                    transform.position = new Vector3(mousePositionScript.mousePosition.x, transform.position.y, mousePositionScript.mousePosition.z);
                }
            }

            //Floor doesnt exist
            else if (pointZeroScript.pointZero == Vector2.zero)
            {
                transform.position = new Vector3(mousePositionScript.mousePosition.x, transform.position.y, mousePositionScript.mousePosition.z);
            }

            //Ceiling obstacle hited wall obstacle above floor surface
            if (wallObstacle != null && hangingObstacleCollided)
            {
                //move on x axis
                if (ceilingObstacleCollidedWithLeftOrRightSide)
                {
                    mousePositionScript.mousePosition.x = Mathf.Clamp(mousePositionScript.mousePosition.x, backBound, frontBound);
                    transform.position = new Vector3(mousePositionScript.mousePosition.x, transform.position.y, positionZ);

                }
                //move on z axis
                else if (ceilingObstacleCollidedWithFrontOrBackSide)
                {
                    mousePositionScript.mousePosition.z = Mathf.Clamp(mousePositionScript.mousePosition.z, floorLeftBound, floorRightBound);
                    transform.position = new Vector3(positionX, transform.position.y, mousePositionScript.mousePosition.z);
                }

            }
        }
    }

    //Dragging with right - only on Y axis 
    void OnMouseOver()
    {
        StopCoroutine(WaitForSeconds());
        //Canvas Input Field is turn on
        canvasIF.SetActive(true);

        if (Input.GetMouseButton(1) && !isFrozeen)
        {
            relativeHeight = ceilingRelativeHeight - transform.position.y;
            isDraggedRightMouseButton = true;
            cameraMoveScript.canRotate = false;

            float verticalInput = Input.GetAxis("Mouse Y") * Time.deltaTime * rightMouseButtonDraggingSpeed;
            transform.position = new Vector3(transform.position.x, transform.position.y + verticalInput, transform.position.z);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isDraggedRightMouseButton = false;
            cameraMoveScript.canRotate = true;
        }
    }



    //Collision
    private void OnTriggerEnter(Collider other)
    {
        //Ceiling obstacle collide with WallObstacle
        if (other.transform.CompareTag("WindowObstacle") || other.transform.CompareTag("DoorObstacle"))
        {
            hangingObstacleCollided = true;

            wallObstacle = other.gameObject;
            float wallObstacleSizeZ = wallObstacle.transform.localScale.z;
            float wallObstaclePositionZ = wallObstacle.transform.position.z;
            float rightBound = wallObstaclePositionZ + wallObstacleSizeZ / 2 + transform.localScale.z / 2;
            float leftBound = wallObstaclePositionZ - wallObstacleSizeZ / 2 - transform.localScale.z / 2;

            float distanceBetweenPositionsZ = Mathf.Round(Mathf.Abs(transform.position.z - wallObstaclePositionZ));
            float distanceRegardingSizeZ = ((gameObject.transform.localScale.z + wallObstacleSizeZ) / 2);


            float wallObstacleSizeX = wallObstacle.transform.localScale.x;
            float wallObstaclePositionX = wallObstacle.transform.position.x;
            float frontBound = wallObstaclePositionX + wallObstacleSizeX / 2 + transform.localScale.x / 2;
            float backBound = wallObstaclePositionX - wallObstacleSizeX / 2 - transform.localScale.x / 2;

            float distanceBetweenPositionsX = Mathf.Round(Mathf.Abs(transform.position.x - wallObstaclePositionX));
            float distanceRegardingSizeX = ((transform.localScale.x + wallObstacleSizeX) / 2);


            if (distanceBetweenPositionsZ >= distanceRegardingSizeZ)
            {
                ceilingObstacleCollidedWithLeftOrRightSide = true;

                //Collides with wallObstacle from right side
                if (transform.position.z > wallObstaclePositionZ)
                {
                    debugCeilinglObstacle.Log("right side");
                    mousePositionScript.mousePosition.z = rightBound;
                    positionZ = rightBound;
                    transform.position = new Vector3(transform.position.x, transform.localScale.y / 2, rightBound);
                }
                //Collides with wallObstacle from left side
                else if (transform.position.z < wallObstaclePositionZ)
                {
                    debugCeilinglObstacle.Log("left side");
                    mousePositionScript.mousePosition.z = leftBound;
                    positionZ = leftBound;
                    transform.position = new Vector3(transform.position.x, transform.localScale.y / 2, leftBound);
                }
            }
            else if (distanceBetweenPositionsX >= distanceRegardingSizeX)
            {
                ceilingObstacleCollidedWithFrontOrBackSide = true;

                //Collides from back of wallObstacle
                if (transform.position.x < wallObstaclePositionX)
                {
                    debugCeilinglObstacle.Log("back of floor");
                    mousePositionScript.mousePosition.x = backBound;
                    positionX = backBound;
                    transform.position = new Vector3(backBound, transform.localScale.y / 2, transform.position.z);
                }
                //Collides from front of wallObstacle
                else if (transform.position.x > wallObstaclePositionX)
                {
                    debugCeilinglObstacle.Log("front of wallObstacle");
                    mousePositionScript.mousePosition.x = frontBound;
                    positionX = frontBound;
                    transform.position = new Vector3(frontBound, transform.localScale.y / 2, transform.position.z);
                }
            }
            else if (distanceBetweenPositionsX < distanceRegardingSizeX || distanceBetweenPositionsZ < distanceRegardingSizeZ)
            {
                Debug.Log("Entered wallObstacle");

            }

        }
    }


    private void OnTriggerExit(Collider other)
    {
        hangingObstacleCollided = false;

        ceilingObstacleCollidedWithLeftOrRightSide = false;
        ceilingObstacleCollidedWithFrontOrBackSide = false;
    }

    private void OnMouseUp()
    {
        hangingObstacleCollided = false;
        ceilingObstacleCollidedWithLeftOrRightSide = false;
        ceilingObstacleCollidedWithFrontOrBackSide = false;
        mousePositionScript.mouseDragsObject = false;
    }


    private void OnMouseExit()
    {
        cameraMoveScript.canRotate = true;

        //Canvas Input Field is turn on
        if (mousePositionScript.mouseDragsObject == false)
        {
            StartCoroutine(WaitForSeconds());
        }
    } 
    
    //Input fields fades delay
    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(inputFieldFadesDelay);
        canvasIF.SetActive(false);
    }


}