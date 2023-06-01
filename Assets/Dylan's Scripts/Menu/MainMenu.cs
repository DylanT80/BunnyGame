using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject title;
    void Update()
    {
        Time. timeScale = 1;
    }
    public void PlayGame() {
        Time.timeScale = 1f;
        if(Time.timeScale == 1f)
        {
            
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void QuitGame() {
        Application.Quit();
    }
}
