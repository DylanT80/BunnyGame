using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructScript : MonoBehaviour
{
    public static bool active = true;
    public GameObject Message;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Message.SetActive(true);
        }

    }
    void Update()
    {
        this.gameObject.SetActive(active);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Message.SetActive(false);
        }
    }
}
