using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInfo : MonoBehaviour
{
    private Inventory inventory;
    public int i;                   // Index for this slot in array
    public int WeaponNumber;        // Weapon number type it has
    private ItemDropSystem IDS;
    private GameObject player;
    public bool selected = false;
    private WeaponUI WUI;
    private BunnyCamScript BCS;
    private GameObject IS;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        IDS = GameObject.Find("InventorySystem").GetComponent<ItemDropSystem>();
        player = GameObject.FindGameObjectWithTag("Player");
        WUI = GameObject.Find("WeaponUI").GetComponent<WeaponUI>();
        BCS = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().GetComponent<BunnyCamScript>();
        IS = GameObject.Find("InventorySystem");
    }

    // Update is called once per frame
    void Update()
    {
        // Can tell if slot is full or not
        if (transform.childCount <= 0) {
            inventory.isFull[i] = false;
            WeaponNumber = -1;              // No weapon
        }
        else if (transform.childCount > 0) {
            inventory.isFull[i] = true;
        }

        if (Input.GetKeyDown(KeyCode.G) && selected && inventory.isFull[i]) {
            GameObject go = IS.GetComponent<WeaponSystem>().WeaponsHeld[i];

            IDS.DropItem(WeaponNumber, go);
            go.transform.parent = null;
            IS.GetComponent<WeaponSystem>().WeaponsHeld[i] = null;
            
            // Soft destroy
            go.transform.position = new Vector2(100, 100);

            if (go.GetComponent<WeaponInfo>()) {
                go.GetComponent<WeaponInfo>().dropped = true;
                go.GetComponent<WeaponInfo>().selected = false;
            }
            
            Destroy(transform.GetChild(0).gameObject);
        }

        // Change UI with item if there is an item, shake screen if item
        if (selected && inventory.isFull[i]) {
            GameObject go = IS.GetComponent<WeaponSystem>().WeaponsHeld[i];
            if (go)
            {
                if (go.GetComponent<WeaponInfo>()) {
                    go.GetComponent<WeaponInfo>().selected = true;
                    WUI.ChangeUI(i);
                }
            }
        }
        // Empty UI with empty slot, dont shake if no item
        else if (selected && !inventory.isFull[i]) {
            WUI.EmptyUI();
            BCS.CanShake = false;
        }

        if (!selected && inventory.isFull[i]) {
            // GameObject go = player.transform.GetChild(i).gameObject;
            GameObject go = IS.GetComponent<WeaponSystem>().WeaponsHeld[i];
            if (go.GetComponent<WeaponInfo>()) {
                go.GetComponent<WeaponInfo>().selected = false;
            }
        }
    }  
}
