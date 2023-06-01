using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathScreenScript : MonoBehaviour
{
    public TextMeshProUGUI playerScore;

    void Start()
    {
        Time.timeScale = 0f;
    }
    public void Set(int score)
    {
        
        this.gameObject.SetActive(true);
        playerScore.text = score.ToString() + " POINTS";
    }
    public void Redeem()
    {

        Time.timeScale = 1f;
        if(Time.timeScale == 1f)
        {
            SceneManager.LoadScene("SampleScene");
        }
        
    }

    public void Menu() {
        SceneManager.LoadScene("Menu");
    }
}
