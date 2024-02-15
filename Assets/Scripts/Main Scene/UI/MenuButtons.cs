using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    //Buttons
    private Button menuButton;
    public int buttonID;
    private MenuManager canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<MenuManager>();
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(ActivateMenu);
    }

    // Update is called once per frame
    private void ActivateMenu() 
    {
        canvas.SetMenuActive(buttonID);
    }
}
