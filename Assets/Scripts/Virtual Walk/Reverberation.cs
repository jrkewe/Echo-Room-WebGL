using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reverberation : MonoBehaviour
{
    //Buttons
    private Button button;
    public Sprite imageTurnOnReverberation;
    public Sprite imageTurnOffReverberation;

    //Reverberation state
    private bool reverberationIsOff = true;

    // Start is called before the first frame update
    void Start()
    {

        button = GetComponent<Button>();
        button.image.sprite = imageTurnOffReverberation;
        button.onClick.AddListener(ToggleReverbertion);
    }

    private void ToggleReverbertion() 
    {
        //Change lock button image
        if (reverberationIsOff == true)
        {
            button.image.sprite = imageTurnOnReverberation;
            reverberationIsOff = false;
        }
        else
        {
            button.image.sprite = imageTurnOffReverberation;
            reverberationIsOff = true;
        }
    }
}
