using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneButtons : MonoBehaviour
{
    //Buttons
    private Button sceneButton;
    public int buttonID;

    // Start is called before the first frame update
    void Start()
    {
        sceneButton = GetComponent<Button>();
        sceneButton.onClick.AddListener(LoadScene);
    }

    // Update is called once per frame
    private void LoadScene()
    {
        if (buttonID == 0) 
        {
            int currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneNumber - 1);
        }
        else if (buttonID == 1)
        {
            int currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneNumber + 1);
        }
    }
}
