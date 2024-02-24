using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class InstantiationMenu : MonoBehaviour
{
    public TMP_InputField xInstantiationX;
    public TMP_InputField xInstantiationD;
    public TMP_InputField zInstantiationZ;
    public TMP_InputField zInstantiationD;
    public TMP_InputField mInstantiationX;
    public TMP_InputField mInstantiationZ;
    public TMP_InputField mInstantiationDX;
    public TMP_InputField mInstantiationDZ;

    //Choosen button and panel
    private int choosenButton;
    private GameObject choosenPanel;

    //State of menu
    private bool buttonWasChoosen = false;
    private bool panelWasChoosen = false;

    int numberOfMenus = 3;

    public void DisableOtherMenu(int menu)
    {
        choosenButton = menu;
        buttonWasChoosen = true; 
        
        if (panelWasChoosen)
        {
            //Coroutine - instantiation - wait for enter
            StartCoroutine(WaitForKeyEnter());
        }

        for (int i = 0; i<numberOfMenus ;i++) 
        {
            if (i==menu)
            {
                gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (i!=menu) 
            {
                gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void PanelInstantiate(GameObject panel)
    {
        Debug.Log("Wybrano panel");
        panelWasChoosen = true;
        choosenPanel = panel;
        //Input fields reset

       
    } 

    IEnumerator WaitForKeyEnter()
    {
        //Insert Instantiation data
        Debug.Log("Czeka na enter");
        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }

        switch (choosenButton) 
        {
            case 0:
                Debug.Log("Przycisk 0");
                float quantityX;
                float distanceX;
                bool successQX = float.TryParse(xInstantiationX.text, out quantityX);
                bool successDX = float.TryParse(xInstantiationD.text, out distanceX);
                if (successQX && successDX)
                {
                    //Position of instantiated object
                    float realDistance = choosenPanel.transform.localScale.x + distanceX;
                    for (int i = 0;i < quantityX - 1; i++) {
                        float positionX = choosenPanel.transform.position.x + realDistance * (i + 1);
                        Vector3 position = new Vector3(positionX, choosenPanel.transform.position.y, choosenPanel.transform.position.z);
                        Instantiate(choosenPanel, position, Quaternion.identity);
                    }
                }
                break;

            case 1:
                Debug.Log("Przycisk 1");
                float quantityZ;
                float distanceZ;
                bool successQZ = float.TryParse(zInstantiationZ.text, out quantityZ);
                bool successDZ = float.TryParse(zInstantiationD.text, out distanceZ);
                if (successQZ && successDZ)
                {
                    //Position of instantiated object
                    float realDistance = choosenPanel.transform.localScale.z + distanceZ;
                    for (int i = 0; i < quantityZ - 1; i++)
                    {
                        float positionZ = choosenPanel.transform.position.z + realDistance * (i + 1);
                        Vector3 position = new Vector3(choosenPanel.transform.position.x, choosenPanel.transform.position.y, positionZ );
                        Instantiate(choosenPanel, position, Quaternion.identity);
                    }
                }
                break;

            case 2:
                Debug.Log("Przycisk 2");
                float mQuantityX;
                float mDistanceX;
                float mQuantityZ;
                float mDistanceZ;
                bool successMQX = float.TryParse(mInstantiationX.text, out mQuantityX);
                bool successMDX = float.TryParse(mInstantiationDX.text, out mDistanceX);
                bool successMQZ = float.TryParse(mInstantiationZ.text, out mQuantityZ);
                bool successMDZ = float.TryParse(mInstantiationDZ.text, out mDistanceZ);
                if (successMQX && successMDX && successMQZ && successMDZ)
                {
                    float positionXOfRawZ = choosenPanel.transform.position.x;
                    for (int i = 0 ; i < mQuantityX - 1 ; i++) //dla x rzedow
                    {
                        float realDistanceZ = choosenPanel.transform.localScale.z + mDistanceZ;
                        float realDistanceX = choosenPanel.transform.localScale.x + mDistanceX;
                        //First raw
                        if (i==0) {
                            for (int j = 0; j < mQuantityZ - 1; j++)
                            {
                                Debug.Log("Panele Z: " + j);
                                float positionZ = choosenPanel.transform.position.z + realDistanceZ * (j + 1);
                                Vector3 mPositionZ = new Vector3(positionXOfRawZ, choosenPanel.transform.position.y, positionZ);
                                Instantiate(choosenPanel, mPositionZ, Quaternion.identity);
                            } 
                        }

                        //Next raws
                        float positionX = choosenPanel.transform.position.x + realDistanceX * (i + 1);
                        Vector3 mPositionX = new Vector3(positionX, choosenPanel.transform.position.y, choosenPanel.transform.position.z);
                        Instantiate(choosenPanel, mPositionX, Quaternion.identity);
                        positionXOfRawZ = positionX;

                        for (int j = 0; j < mQuantityZ - 1; j++)
                        {
                            Debug.Log("Panele Z: " + j);
                            float positionZ = choosenPanel.transform.position.z + realDistanceZ * (j + 1);
                            Vector3 mPositionZ = new Vector3(positionXOfRawZ, choosenPanel.transform.position.y, positionZ);
                            Instantiate(choosenPanel, mPositionZ, Quaternion.identity);
                        }
                    } 
                }
                break;

            default:
                break;

        }


        buttonWasChoosen = false;
        panelWasChoosen = false;

    }
}
