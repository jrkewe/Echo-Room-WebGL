using System.Collections;
using TMPro;
using UnityEngine;

public class ReadInputPositionXCeilingObstacle : MonoBehaviour
{
    //Debuger
    public static ILogger debugReadInputPositionXceilingObstacle = new Logger(Debug.unityLogger.logHandler);

    public TMP_InputField inputField;

    private MousePosition mousePositionScript;
    private UserInputManager userInputManagerScript;

    //Parents
    private GameObject objectParent;
    private CeilingObstacle objectParentScript;

    private void Start()
    {
        //Debugger
        debugReadInputPositionXceilingObstacle.logEnabled = false;

        //Parent - obstacle
        objectParent = gameObject.transform.parent.gameObject.transform.parent.gameObject;
        objectParentScript = objectParent.GetComponent<CeilingObstacle>();

        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
        userInputManagerScript = GameObject.Find("User Input Manager").GetComponent<UserInputManager>();

        DisplayPositionOfSelectedObject();
    }

    private void Update()
    {
        if (mousePositionScript.mouseDragsObject) {
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
        float x;
        bool successX = float.TryParse(inputField.text, out x);
        if (successX)
        {
            objectParentScript.relativePosition.x = x;
            objectParentScript.Reposition();
        }
    }

    private void DisplayPositionOfSelectedObject()
    {
            float relativePositionX = objectParentScript.relativePosition.x;
           inputField.text = relativePositionX.ToString("0.00");
    }
}
