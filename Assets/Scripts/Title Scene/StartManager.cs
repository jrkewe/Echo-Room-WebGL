using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class StartManager : MonoBehaviour
{
    public int time = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene() 
    {
        while (time>0) 
        {
            yield return new WaitForSeconds(time);
            time--;
        }
        SceneManager.LoadScene(1); 
    }
}
