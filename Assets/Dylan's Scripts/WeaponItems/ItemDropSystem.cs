using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// System to drop appropriate item drops that stre the actual game objects for memory!
public class ItemDropSystem : MonoBehaviour
{
    private GameObject player;
    public GameObject[] items;  // array of all items in the game
    
    private void Start() {  
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void DropItem(int i, GameObject item) {
        Vector3 playerPos = player.transform.position;
        GameObject ItemToDrop = Instantiate(items[i], new Vector2(playerPos.x, playerPos.y + 4), Quaternion.identity);
        ItemToDrop.GetComponent<Item>().ExistingWeapon = item;
    }
}
