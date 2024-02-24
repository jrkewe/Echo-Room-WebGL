using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiationButtons : MonoBehaviour
{
    //Buttons
    private Button menuButton;
    public int buttonID;

    private GameObject instantiationMenu;
    private InstantiationMenu instantiationMenuScript;


    // Start is called before the first frame update
    void Start()
    {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(ActivateInputFieldMenu);
    }

    // Update is called once per frame
    void ActivateInputFieldMenu()
    {
        //Set Menu Active 
        instantiationMenuScript = gameObject.transform.parent.GetComponent<InstantiationMenu>();
        instantiationMenuScript.DisableOtherMenu(buttonID);

    }
}
