using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RoomSurface : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        float floorSurface = 1;
        //count floor surface 
        text.text = floorSurface.ToString("0.00"); 
    }

}
