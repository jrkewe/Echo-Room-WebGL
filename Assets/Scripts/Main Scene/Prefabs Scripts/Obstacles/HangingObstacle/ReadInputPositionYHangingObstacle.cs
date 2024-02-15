using System.Collections;
using TMPro;
using UnityEngine;

public class ReadInputPositionYHangingObstacle : MonoBehaviour
{
    //Debuger
    public static ILogger debugReadInputPositionYhangingObstacle = new Logger(Debug.unityLogger.logHandler);

    public TMP_InputField inputField;

    private MousePosition mousePositionScript;
    private UserInputManager userInputManagerScript;

    //Parents
    private GameObject objectParent;
    private HangingObstacle objectParentScript;

    private void Start()
    {
        //Debugger
        debugReadInputPositionYhangingObstacle.logEnabled = false;

        //Parent - obstacle
        objectParent = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        objectParentScript = objectParent.GetComponent<HangingObstacle>();

        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
        userInputManagerScript = GameObject.Find("User Input Manager").GetComponent<UserInputManager>();

        DisplayPositionOfSelectedObject();
    }

    private void Update()
    {
        if (mousePositionScript.mouseDragsObject || objectParentScript.isDraggedRightMouseButton)
        {
            DisplayPositionOfSelectedObject();
        }
        //if its object - DetectObject
        if (userInputManagerScript.objectDetected && mousePositionScript.selectedObject != null && !mousePositionScript.mouseDragsObject)
        {
            StopAllCoroutines();

            StartCoroutine(WaitForReposition());
        }

    }

    IEnumerator WaitForReposition()
    {

        //wprowadzaj wymiary
        while (!Input.GetKey(KeyCode.Return))
        {
            //gdy cos innego klikniete
            if (Input.GetMouseButton(0) && (mousePositionScript.DetectObject() || mousePositionScript.terrainHItted))
            {
                yield return null;
                StopCoroutine(WaitForReposition());
            }
            yield return null;
        }

        //przeslij pozycje
        float y;
        bool successY = float.TryParse(inputField.text, out y);
        if (successY)
        {
            //zaimplementowac wysokosc
            objectParentScript.relativeHeight = y;

            objectParentScript.Reposition();
        }
    }

    private void DisplayPositionOfSelectedObject()
    {
        //zaimplementowac wysokosc
        float relativeHeight = objectParentScript.relativeHeight;
        inputField.text = relativeHeight.ToString("0.00");
    }
}

