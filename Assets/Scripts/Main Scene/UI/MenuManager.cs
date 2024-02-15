using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menuBackground;

    public void SetMenuActive(int buttonID) 
    {
        for (int i =0;i<menuBackground.Length ;i++) {
            if (i != buttonID)
            { 
                menuBackground[i].SetActive(false);

            } 
            else if (i==buttonID) 
            {
                menuBackground[i].SetActive(true);
            }
        }
    }
}
