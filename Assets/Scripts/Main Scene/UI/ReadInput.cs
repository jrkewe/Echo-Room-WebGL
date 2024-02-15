using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReadInput : MonoBehaviour
{
    //Debuger
    public static ILogger debugReadInput = new Logger(Debug.unityLogger.logHandler);

    public TMP_InputField inputFieldX;
    public TMP_InputField inputFieldY;
    public TMP_InputField inputFieldZ;

    private UserInputManager userInputManagerScript;
    private MousePosition mousePositionScript;

    //New Dimensions script
    private NewDimensions newDimensionsScript;

    //Parameters
    private float wallThicknes = 0.25f;
    private float floorThicknes = 0.16f;

    private void Start()
    {
        //Debugger
        debugReadInput.logEnabled = false;

        RestartInputFields();

        DisabelAllInputFields();

        userInputManagerScript = GameObject.Find("User Input Manager").GetComponent<UserInputManager> ();
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
        newDimensionsScript = GameObject.Find("SetDimensions").GetComponent<NewDimensions>();
    }

    private void Update()
    {

        newDimensionsScript.enterWasClicked = false;

        if (userInputManagerScript.objectDetected && mousePositionScript.selectedObject != null && !mousePositionScript.mouseDragsObject)
        {
            DisplayDimensionsOfSelectedObject();

            EnableTwoInputFields();

            StartCoroutine(WaitForEnter());
        }
    }

    IEnumerator WaitForEnter()
    {

        //wprowadzaj wymiary
        while (!Input.GetKey(KeyCode.Return) )
        {
            if (Input.GetMouseButton(0) && (mousePositionScript.DetectObject() || mousePositionScript.terrainHItted))
            {
                RestartInputFields();
                yield return null;
                StopCoroutine(WaitForEnter());
            }
            yield return null;
        }
        newDimensionsScript.enterWasClicked = true;

        //przeslij wymiary 
        if (mousePositionScript.objectID == 0) //WallX
        {
            float x;
            float y;
            float z = 0.0f;
            bool successX = float.TryParse(inputFieldX.text, out x);
            bool successY = float.TryParse(inputFieldY.text, out y);
            if (successX && successY)
            {
                userInputManagerScript.newSize = new Vector3(x + wallThicknes * 2, y + floorThicknes , z);
            }
        }
        else if (mousePositionScript.objectID == 1) //WallZ
        {
            float x = 0.0f;
            float y;
            float z;
            bool successY = float.TryParse(inputFieldY.text, out y);
            bool successZ = float.TryParse(inputFieldZ.text, out z);
            if (successY && successZ)
            {
                userInputManagerScript.newSize = new Vector3(x, y + floorThicknes, z + wallThicknes * 2);
            }
        }
        else if (mousePositionScript.objectID == 2) //Floor
        {
            float x;
            float y = 0.0f;
            float z;
            bool successX = float.TryParse(inputFieldX.text, out x);
            bool successZ = float.TryParse(inputFieldZ.text, out z);
            if (successX && successZ)
            {
                userInputManagerScript.newSize = new Vector3(x + wallThicknes * 2, y, z + wallThicknes * 2);
            }
        }
        else if (mousePositionScript.objectID == 3) //Detector/horizontal obstacle
        {
            float x;
            float y = 0.0f;
            float z;
            bool successX = float.TryParse(inputFieldX.text, out x);
            bool successZ = float.TryParse(inputFieldZ.text, out z);
            if (successX && successZ)
            {
                userInputManagerScript.newSize = new Vector3(x, y, z);
            }
        }
        else if (mousePositionScript.objectID == 4) //Door obstacle
        {
            float x;
            float y = 0.0f;
            float z;
            bool successX = float.TryParse(inputFieldX.text, out x);
            bool successZ = float.TryParse(inputFieldZ.text, out z);
            if (successX && successZ)
            {
                userInputManagerScript.newSize = new Vector3(x, y, z);
            }
        }
        else if (mousePositionScript.objectID == 5) //Ventilation/horizontal obstacle
        {
            float x;
            float y = 0.0f;
            float z;
            bool successX = float.TryParse(inputFieldX.text, out x);
            bool successZ = float.TryParse(inputFieldZ.text, out z);
            if (successX && successZ)
            {
                userInputManagerScript.newSize = new Vector3(x, y, z);
            }
        }
        else if (mousePositionScript.objectID == 6) //Lighting/horizontal obstacle
        {
            float x;
            float y;
            float z;
            bool successX = float.TryParse(inputFieldX.text, out x);
            bool successY = float.TryParse(inputFieldY.text, out y);
            bool successZ = float.TryParse(inputFieldZ.text, out z);
            if (successX && successY && successZ)
            {
                userInputManagerScript.newSize = new Vector3(x, y, z);
            }
        }
        else if (mousePositionScript.objectID == 7) //Window obstacle
        {
            float x;
            float y = 0.0f;
            float z;
            bool successX = float.TryParse(inputFieldX.text, out x);
            bool successZ = float.TryParse(inputFieldZ.text, out z);
            if (successX && successZ)
            {
                userInputManagerScript.newSize = new Vector3(x, y, z);
            }
        }
        else if (mousePositionScript.objectID == 8) //Panel
        {
            float x;
            float y;
            float z;
            bool successX = float.TryParse(inputFieldX.text, out x);
            bool successY = float.TryParse(inputFieldY.text, out y);
            bool successZ = float.TryParse(inputFieldZ.text, out z);
            if (successX && successY && successZ)
            {
                userInputManagerScript.newSize = new Vector3(x, y, z);
            }
        }


        RestartInputFields();
        DisabelAllInputFields();
    }

    private void EnableTwoInputFields() 
    {
        if (mousePositionScript.objectID == 0) //WallX
        {
            inputFieldX.enabled = true;
            inputFieldY.enabled = true;
        }
        else if (mousePositionScript.objectID == 1) //WallZ
        {
            inputFieldY.enabled = true;
            inputFieldZ.enabled = true;
        }
        else if (mousePositionScript.objectID == 2) //Floor
        {
         
            inputFieldX.enabled = true;
            inputFieldZ.enabled = true;
        }
        else if (mousePositionScript.objectID == 3) //Detector/horizontal obstacle
        {
            inputFieldX.enabled = true;
            inputFieldZ.enabled = true;
        }
        else if (mousePositionScript.objectID == 4) //Door obstacle
        {
            inputFieldX.enabled = true;
            inputFieldZ.enabled = true;
        }
        else if (mousePositionScript.objectID == 5) //Ventilation/horizontal obstacle
        {
            inputFieldX.enabled = true;
            inputFieldZ.enabled = true;
        }
        else if (mousePositionScript.objectID == 6) //Lighting/horizontal obstacle
        {
            inputFieldX.enabled = true;
            inputFieldY.enabled = true;
            inputFieldZ.enabled = true;
        }
        else if (mousePositionScript.objectID == 7) //Window obstacle
        {
            inputFieldX.enabled = true;
            inputFieldZ.enabled = true;
        }
        else if (mousePositionScript.objectID == 8) //Panel
        {
            inputFieldX.enabled = true;
            inputFieldY.enabled = true;
            inputFieldZ.enabled = true;
        }
    }

    private void DisabelAllInputFields()
    {
        inputFieldX.enabled = false;
        inputFieldY.enabled = false;
        inputFieldZ.enabled = false;
    }

    private void RestartInputFields()
    {
        inputFieldX.text = "Enter X";
        inputFieldY.text = "Enter Y";
        inputFieldZ.text = "Enter Z";
        inputFieldX.image.color = Color.white;
        inputFieldY.image.color = Color.white;
        inputFieldZ.image.color = Color.white;
    }

    private void DisplayDimensionsOfSelectedObject()
    {
        if (mousePositionScript.objectID == 0) //WallX
        {   
            //X
            if (mousePositionScript.selectedObject.transform.localScale.x - wallThicknes > 1)
            {
                inputFieldX.text = (mousePositionScript.selectedObject.transform.localScale.x - wallThicknes * 2).ToString();
            }
            else { inputFieldX.text = "1"; }

            //Y
            if (mousePositionScript.selectedObject.transform.localScale.y - floorThicknes > 1)
            {
                inputFieldY.text = (mousePositionScript.selectedObject.transform.localScale.y - floorThicknes).ToString();
            }
            else { inputFieldY.text = "1"; }

            inputFieldZ.image.color = Color.red;
            inputFieldZ.text = "Fixed";
        }
        if (mousePositionScript.objectID == 1) //WallZ
        {
            inputFieldX.image.color = Color.red;
            inputFieldX.text = "Fixed";

            //Y
            if (mousePositionScript.selectedObject.transform.localScale.y - floorThicknes > 1)
            {
                inputFieldY.text = (mousePositionScript.selectedObject.transform.localScale.y - floorThicknes).ToString();
            }
            else { inputFieldY.text = "1"; }

            //Z
            if (mousePositionScript.selectedObject.transform.localScale.z - wallThicknes > 1)
            {
                inputFieldZ.text = (mousePositionScript.selectedObject.transform.localScale.z - wallThicknes * 2).ToString();
            }
            else { inputFieldZ.text = "1"; }
        }
        if (mousePositionScript.objectID == 2) //Floor 
        {
            //X
            if (mousePositionScript.selectedObject.transform.localScale.x - wallThicknes > 1)
            {
                inputFieldX.text = (mousePositionScript.selectedObject.transform.localScale.x - wallThicknes * 2).ToString();
            }
            else { inputFieldX.text = "1"; }

            inputFieldY.image.color = Color.red;
            inputFieldY.text = "Fixed";

            //Z
            if (mousePositionScript.selectedObject.transform.localScale.z - wallThicknes > 1)
            {
                inputFieldZ.text = (mousePositionScript.selectedObject.transform.localScale.z - wallThicknes * 2).ToString();
            }
            else { inputFieldZ.text = "1"; }
        }
        if (mousePositionScript.objectID == 3) //Detector/horizontal obstacle
        {
            inputFieldX.text = mousePositionScript.selectedObject.transform.localScale.x.ToString();
            inputFieldY.image.color = Color.red;
            inputFieldY.text = "Fixed";
            inputFieldZ.text = mousePositionScript.selectedObject.transform.localScale.z.ToString();
        }
        if (mousePositionScript.objectID == 4) //Door obstacle
        {
            inputFieldX.text = mousePositionScript.selectedObject.transform.localScale.x.ToString();
            inputFieldY.image.color = Color.red;
            inputFieldY.text = "Fixed";
            inputFieldZ.text = mousePositionScript.selectedObject.transform.localScale.z.ToString();
        }
        if (mousePositionScript.objectID == 5) //Ventilation/horizontal obstacle
        {
            inputFieldX.text = mousePositionScript.selectedObject.transform.localScale.x.ToString();
            inputFieldY.image.color = Color.red;
            inputFieldY.text = "Fixed";
            inputFieldZ.text = mousePositionScript.selectedObject.transform.localScale.z.ToString();
        }
        if (mousePositionScript.objectID == 6) //Lighting/horizontal obstacle
        {
            inputFieldX.text = mousePositionScript.selectedObject.transform.localScale.x.ToString();
            inputFieldY.text = mousePositionScript.selectedObject.transform.localScale.y.ToString();
            inputFieldZ.text = mousePositionScript.selectedObject.transform.localScale.z.ToString();
        }
        if (mousePositionScript.objectID == 7) //Window obstacle
        {
            inputFieldX.text = mousePositionScript.selectedObject.transform.localScale.x.ToString();
            inputFieldY.image.color = Color.red;
            inputFieldY.text = "Fixed";
            inputFieldZ.text = mousePositionScript.selectedObject.transform.localScale.z.ToString();
        }
        if (mousePositionScript.objectID == 8) //Panel
        {
            inputFieldX.text = mousePositionScript.selectedObject.transform.localScale.x.ToString();
            inputFieldY.text = mousePositionScript.selectedObject.transform.localScale.y.ToString();
            inputFieldZ.text = mousePositionScript.selectedObject.transform.localScale.z.ToString();
        }

    }

}
