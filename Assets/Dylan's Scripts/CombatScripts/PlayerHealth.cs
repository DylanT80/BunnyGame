using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float numOfHearts;  // max number of hearts
    public Image[] hearts;  // Will connect with the Canvas UI images
    public Sprite fullHeart;
    public Sprite emptyHeart;


    private void Start() {
        
    }
    // Update is called once per frame
    void Update()
    {
        health = GetComponent<PlayerInfo>().health;
        numOfHearts = GetComponent<PlayerInfo>().maxHealth;

        // Avoid going over max hearts
        if (health > numOfHearts) {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            // Displaying current health
            if (i < health) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }

            // Only reveal the max number of hearts
            if (i < numOfHearts) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }

    }
}
