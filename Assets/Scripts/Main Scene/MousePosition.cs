using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousePosition : MonoBehaviour
{
    //Debuger
    public static ILogger debugMousePosition = new Logger(Debug.unityLogger.logHandler);

    //mouse position in 3D
    public Camera mainCamera;
    public Vector3 mousePosition;

    //selected object
    public GameObject selectedObject;
    public int objectID;

    //Mouse drags an object
    public bool mouseDragsObject = false;
    public bool mouseIsDraged;

    //Terain
    public LayerMask layerMask;
    public bool terrainHItted = false;

    private void Start() 
    {
        //Debugger
        debugMousePosition.logEnabled = false;
    }

    private void Update()
    {
        terrainHItted = false;
        GetMousePosition();
    }

    public bool DetectObject()
    {
        if (!EventSystem.current.IsPointerOverGameObject(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {

                if (raycastHit.collider.gameObject.tag == "WallX")
                {
                    selectedObject = raycastHit.collider.gameObject;
                    objectID = 0;
                    debugMousePosition.Log("Wall X");
                    debugMousePosition.Log(selectedObject.transform.localScale);
                    debugMousePosition.Log("Position: " + selectedObject.transform.position);
                    return true;
                }
                else if (raycastHit.collider.gameObject.tag == "WallZ")
                {
                    selectedObject = raycastHit.collider.gameObject;
                    objectID = 1;
                    debugMousePosition.Log("Wall Z");
                    debugMousePosition.Log(selectedObject.transform.localScale);
                    return true;
                }
                else if (raycastHit.collider.gameObject.tag == "Floor")
                {
                    selectedObject = raycastHit.collider.gameObject;
                    objectID = 2;
                    debugMousePosition.Log("Floor");
                    debugMousePosition.Log(selectedObject.transform.localScale);
                    return true;
                }
                else if (raycastHit.collider.gameObject.tag == "DetectorObstacle")
                {
                    selectedObject = raycastHit.collider.gameObject;
                    objectID = 3;
                    debugMousePosition.Log("DetectorObstacle");
                    debugMousePosition.Log(selectedObject.transform.localScale);
                    return true;
                }
                else if (raycastHit.collider.gameObject.tag == "DoorObstacle")
                {
                    selectedObject = raycastHit.collider.gameObject;
                    objectID = 4;
                    debugMousePosition.Log("DoorObstacle");
                    debugMousePosition.Log(selectedObject.transform.localScale);
                    return true;
                }
                else if (raycastHit.collider.gameObject.tag == "VentilationObstacle")
                {
                    selectedObject = raycastHit.collider.gameObject;
                    objectID = 5;
                    debugMousePosition.Log("VentilationObstacle");
                    debugMousePosition.Log(selectedObject.transform.localScale);
                    return true;
                }
                else if (raycastHit.collider.gameObject.tag == "LightingObstacle")
                {
                    selectedObject = raycastHit.collider.gameObject;
                    objectID = 6;
                    debugMousePosition.Log("LightingObstacle");
                    debugMousePosition.Log(selectedObject.transform.localScale);
                    return true;
                }
                else if (raycastHit.collider.gameObject.tag == "WindowObstacle")
                {
                    selectedObject = raycastHit.collider.gameObject;
                    objectID = 7;
                    debugMousePosition.Log("WindowObstacle");
                    debugMousePosition.Log(selectedObject.transform.localScale);
                    return true;
                }
                else if (raycastHit.collider.gameObject.tag == "Panel")
                {
                    selectedObject = raycastHit.collider.gameObject;
                    objectID = 8;
                    debugMousePosition.Log("Panel");
                    debugMousePosition.Log(selectedObject.transform.localScale);
                    return true;
                }
                else if (raycastHit.collider.gameObject.tag == "Terrain")
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        terrainHItted = true;
                        
                    }
                  
                    
                        return false;
                    
                }

                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        else
        {
            return false;
        }

    }

    public void GetMousePosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit/*, float.MaxValue, layerMask*/))
        {
            transform.position = raycastHit.point;
            mousePosition = transform.position;
            //mousePosition.y = 0.0f;
        }
    }

   
}
