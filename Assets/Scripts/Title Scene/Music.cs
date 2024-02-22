using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public AudioClip music;
    public AudioClip officeAmbiance;

    public static Music Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SetAudioClip(int sceneID) 
    {

        if (sceneID != 3 )
        {
            gameObject.GetComponent<AudioSource>().clip = music;
            gameObject.GetComponent<AudioSource>().Play();
        }
        else if (sceneID == 3)
        {
            gameObject.GetComponent<AudioSource>().clip = officeAmbiance;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
