using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PanelsInstantiation : MonoBehaviour
{
    private InstantiationMenu panelInstantiationMenu;
    private MousePosition mousePositionScript;

    // Start is called before the first frame update
    void Start()
    {
        panelInstantiationMenu = GameObject.Find("Panel Instantiation Menu").GetComponent<InstantiationMenu>();
        mousePositionScript = GameObject.Find("User Input Manager").GetComponent<MousePosition>();
    }

    private void OnMouseDown()
    {
        panelInstantiationMenu.PanelInstantiate(gameObject);
    }


}
