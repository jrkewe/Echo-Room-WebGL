using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour
{
    //Mouse input
    public float horizontalInput;
    public float verticalInput;

    //Zoom camera
    private Camera ZoomCamera;
    private float scrollSpeed = 15f;

    //Drag camera
    private Vector3 screenStartPoint;
    private Vector3 screenEndPoint;
    private Vector3 screenLenghtOfDrag;
    private MousePosition mousePositionScript;
    private float dragSpeed = 8.0f;

    //Rotate camera around
    public Vector3 pointCameraOrbitsAround;
    public float rotationSpeed = 25;
    public bool canRotate = true;

    //Reposition camera
    private Vector3 startPosition = new Vector3(17,11,-6);
    private Vector3 startAngle = new Vector3(35,-39,0);
    private float fieldOfViev = 46;
    public bool cameraIsRestarted = false;


    private void Start()
    {
        ZoomCamera = Camera.main;
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
        RepositionCamera();
    }

    void Update()
    {

        cameraIsRestarted = false; 

        //R key - repositioning
        if (Input.GetKey(KeyCode.R))
        {
            RepositionCamera();
            cameraIsRestarted = true;
        }

        //Left click - dragging
        if (!mousePositionScript.mouseDragsObject && !PointerIsOverUI())
        {
            DragCamera();
        }

        //Scroll
        if (Input.GetAxis("Mouse ScrollWheel")!=0) {
            Zoom();
        }

        //Right click - rotation
        if (Input.GetMouseButton(1) && canRotate)
        {
            FindTargret();
            RotateCamera();
        }

        CheckCameraPosition();
    }

    void FindTargret()
    {
        //Point that camera is looking at
        Ray ray = Camera.main.ScreenPointToRay(new Vector3((Screen.width / 2), Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            pointCameraOrbitsAround = hit.point;
        }
    }
    void RotateCamera()
    {
        verticalInput = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        transform.RotateAround(pointCameraOrbitsAround, Vector3.up, horizontalInput);
        transform.RotateAround(pointCameraOrbitsAround, Vector3.right, -verticalInput * 1.5f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }

    void Zoom()
    {
        ZoomCamera.transform.position -= transform.forward * -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
    }

    void DragCamera()
    {

        if (Input.GetMouseButtonDown(0))
        {
            mousePositionScript.mouseIsDraged = true;
            screenStartPoint = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            screenEndPoint = Input.mousePosition;
            screenLenghtOfDrag = screenStartPoint - screenEndPoint;
            transform.Translate(new Vector3(screenLenghtOfDrag.x, screenLenghtOfDrag.y, 0).normalized * Time.deltaTime * dragSpeed);
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePositionScript.mouseIsDraged = false;
        }

    }

    private bool PointerIsOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void CheckCameraPosition()
    {
        float xRange = 35.0f;
        float zRange = 35.0f;
        float yMinHeight = 1.0f;
        float yMaxHeight = 35.0f;

        //Minimal and maximal x value
        if (transform.position.x <= -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x >= xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z); ;
        }
        //Minimal and maximal y value
        if (transform.position.y <= yMinHeight)
        {
            transform.position = new Vector3(transform.position.x, yMinHeight, transform.position.z); ;
        }
        if (transform.position.y >= yMaxHeight)
        {
            transform.position = new Vector3(transform.position.x, yMaxHeight, transform.position.z);
        }
        //Minimal and maximal z value
        if (transform.position.z <= -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
        if (transform.position.z >= zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
    }

    private void RepositionCamera()
    {
        transform.position = startPosition;
        transform.eulerAngles = startAngle;
        ZoomCamera.fieldOfView = fieldOfViev;
    }

}

