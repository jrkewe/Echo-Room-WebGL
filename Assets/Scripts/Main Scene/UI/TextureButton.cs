using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureButton : MonoBehaviour
{
    //Buttons
    private Button menuButton;
    public int buttonID;
    public Texture texture;
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
                case 0: mousePositionScript.selectedObject.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
                    break;
                case 1: mousePositionScript.selectedObject.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
                    break;
                case 2:
                    mousePositionScript.selectedObject.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
                    break;
                default: break;
            }
               
        } 
    }
}
