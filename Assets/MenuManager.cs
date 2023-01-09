using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("SoundManager").GetComponent<MusicManager>().PlayMenu();
    }

    public void Play()
    {
        SceneManager.LoadScene("Main");
    }
}
