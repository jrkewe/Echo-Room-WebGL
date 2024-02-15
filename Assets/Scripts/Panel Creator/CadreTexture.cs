using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CadreTexture : MonoBehaviour
{
    //Buttons
    private Button menuButton;
    public int buttonID;
    public Material material;
    private MousePosition mousePositionScript;

    // Start is called before the first frame update
    void Start()
    {
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(ApplyTexture);
    }

    private void ApplyTexture()
    {
        if (mousePositionScript.selectedObject != null)
        {
            switch (buttonID)
            {
                case 0:
                    mousePositionScript.selectedObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = material;
                    break;
                case 1:
                    mousePositionScript.selectedObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = material;
                    break;
                case 2:
                    mousePositionScript.selectedObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = material;
                    break;
                default: break;
            }

        }
    }
}
