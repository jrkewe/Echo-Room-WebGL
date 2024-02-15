using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserInputManager : MonoBehaviour
{
    //Debuger
    public static ILogger debugUserInpurManager = new Logger(Debug.unityLogger.logHandler);
    
    //Prefabs objects
    public GameObject[] objectsPrefabs;
    public bool canInstantiatePrefab = false;
    public int prefabIndex;
    
    //Mouse position
    private MousePosition mousePositionScript;

    //Resizing
    public bool objectDetected= false;
    public Vector3 newSize;

    //ReadInputScript
    private NewDimensions newDimensionsScript;

    public void Start()
    {
        //Debugger
        debugUserInpurManager.logEnabled = false;

        mousePositionScript = GetComponent<MousePosition>();
        newDimensionsScript = GameObject.Find("SetDimensions").GetComponent<NewDimensions>();
    }

    private void Update()
    {
            if (Input.GetMouseButtonDown(0))
            {
                objectDetected = false;

                //if its object - DetectObject
                if (mousePositionScript.DetectObject())
                {
                    StopAllCoroutines();

                    objectDetected = true;

                //Pobierz state przycisku
                    StartCoroutine(WaitForResize());
                    StartCoroutine(WaitForDelete());
                //Czekaj czy przycisk sie zmieni
                }

                //if its terrain - InstantiateObject
                else
                {
                    InstantiateObjects();
                }
            }
    }

    //Create Prefabs
    public void InstantiateObjects()
    {
        if (Input.GetMouseButtonDown(0) && canInstantiatePrefab)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else
            {
                Instantiate(objectsPrefabs[prefabIndex], mousePositionScript.mousePosition, transform.rotation);
                canInstantiatePrefab=false;
            }

        }
    }

    //Wait till Delete is pressed
    IEnumerator WaitForDelete()
    {
        while (!Input.GetKeyDown(KeyCode.Delete))
        { 
            yield return null;
        }
        Destroy(mousePositionScript.selectedObject);
    }

    //Wait till change size 
    IEnumerator WaitForResize()
    {
        while (!newDimensionsScript.enterWasClicked)
        {
            yield return null;
        }

        if (newSize!=Vector3.zero) {
            if (mousePositionScript.objectID == 0) //WallX
            {
                float key = newSize.z;
                newSize.z = 0.25f;
                mousePositionScript.selectedObject.transform.localScale = newSize;
                mousePositionScript.selectedObject.transform.position = new Vector3(mousePositionScript.selectedObject.transform.position.x, newSize.y / 2, mousePositionScript.selectedObject.transform.position.z);
                newSize.z = key;
            }
            else if (mousePositionScript.objectID == 1) //WallZ
            {
                float key = newSize.x;
                newSize.x = 0.25f;
                mousePositionScript.selectedObject.transform.localScale = newSize;
                mousePositionScript.selectedObject.transform.position = new Vector3(mousePositionScript.selectedObject.transform.position.x, newSize.y / 2, mousePositionScript.selectedObject.transform.position.z);
                newSize.x = key;
            }
            else if (mousePositionScript.objectID == 2) //Floor
            {
                float key = newSize.y;
                newSize.y = 0.16f;
                mousePositionScript.selectedObject.transform.localScale = newSize;
                mousePositionScript.selectedObject.transform.position = new Vector3(mousePositionScript.selectedObject.transform.position.x , 0.08f, mousePositionScript.selectedObject.transform.position.z + 0.5f);
                newSize.y = key;
                
            }
            else if (mousePositionScript.objectID == 3) //Detector/horizontal 
            {
                float key = newSize.y;
                newSize.y = 0.16f;
                mousePositionScript.selectedObject.transform.localScale = newSize;
                newSize.y = key;
            }
            else if (mousePositionScript.objectID == 4) //Door obstacle
            {
                float key = newSize.y;
                newSize.y = mousePositionScript.selectedObject.transform.localScale.y;
                mousePositionScript.selectedObject.transform.localScale = newSize;
                newSize.y = key;
            }
            else if (mousePositionScript.objectID == 5) //Ventilation/horizontal 
            {
                float key = newSize.y;
                newSize.y = 0.16f;
                mousePositionScript.selectedObject.transform.localScale = newSize;
                newSize.y = key;
            }
            else if (mousePositionScript.objectID == 6) //Lighting/horizontal 
            {
                mousePositionScript.selectedObject.transform.localScale = newSize;
            }
            else if (mousePositionScript.objectID == 7) //Window obstacle
            {
                float key = newSize.y;
                newSize.y = mousePositionScript.selectedObject.transform.localScale.y;
                mousePositionScript.selectedObject.transform.localScale = newSize;
                newSize.y = key;
            }
            else if (mousePositionScript.objectID == 8) //Panel 
            {
                mousePositionScript.selectedObject.transform.localScale = newSize;
            }

        }
        else 
        {
            debugUserInpurManager.Log("Enter size again");
        }
    }

}
