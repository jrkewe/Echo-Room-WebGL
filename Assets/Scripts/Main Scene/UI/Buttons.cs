using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    //Buttons
    private Button button;

    //Main script
    private UserInputManager userInputManagerScript;

    //tells me which type of object it is
    public int buttonNumber;

    // Start is called before the first frame update
    void Start()
    {
        userInputManagerScript = GameObject.Find("User Input Manager").GetComponent<UserInputManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadObject);
    }
     
    public void LoadObject()
    {
        userInputManagerScript.canInstantiatePrefab = true;
        userInputManagerScript.prefabIndex = buttonNumber;
    }
}
