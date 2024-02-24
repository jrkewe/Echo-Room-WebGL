using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class SceneButtons : MonoBehaviour
{
    //Buttons
    private Button sceneButton;
    public int buttonID;

    //Data persistance between scenes
    private MainManager mainManagerScript;
    private MainPanelManager mainPanelManagerScript;
    private Music audioSourceScript;

    //Stop instantiation
    private UserInputManager userInputManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 3) {
            userInputManagerScript = GameObject.Find("User Input Manager").GetComponent<UserInputManager>();
            userInputManagerScript.canInstantiatePrefab = false;
        }

        audioSourceScript = GameObject.Find("Audio Source").GetComponent<Music>();
        if (audioSourceScript!=null) {
            audioSourceScript.SetAudioClip(SceneManager.GetActiveScene().buildIndex);
        }

        sceneButton = GetComponent<Button>();
        sceneButton.onClick.AddListener(LoadScene);
    }

    // Update is called once per frame
    private void LoadScene()
    {

        if (buttonID == 0) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else if (buttonID == 1)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                mainManagerScript = GameObject.Find("MainManager").GetComponent<MainManager>();
                mainManagerScript.CreateInstance();
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                mainPanelManagerScript = GameObject.Find("Main Panel Manager").GetComponent<MainPanelManager>();
                mainPanelManagerScript.CreateInstance();
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}
