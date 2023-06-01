using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            
                BunnyMovement.activeMoveSpeed = 5;
                BunnyMovement.moveSpeed = 5;
                BunnyMovement.onWater = true;
                BunnyMovement.canDash = false;
            
        }

    }
   
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            BunnyMovement.activeMoveSpeed = 10;
            BunnyMovement.moveSpeed = 10;
            BunnyMovement.onWater = false;
            BunnyMovement.canDash = true;
        }
    }
}
