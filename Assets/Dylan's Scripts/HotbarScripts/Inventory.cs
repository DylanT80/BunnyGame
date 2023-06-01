using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The inventory system that shows what in hotbar is selected and adds items to it
public class Inventory : MonoBehaviour
{
    //animator
 
    //Inventory
    public bool[] isFull;       // If slot in hotbar is taken up
    public GameObject[] slots;  // The hotbar array

    public GameObject selectedSlot;
    private Inventory inventory;
    public int slot = 0;
    private Color iColor;
    private GameObject player;
    private GameObject IS;
    
    // Start is called before the first frame update
    void Start()
    {       
        // Set slot number to the slot info
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<SlotInfo>().i = i;
        }
        
        player = GameObject.FindGameObjectWithTag("Player");
        IS = GameObject.Find("InventorySystem");
    }

    private void Update() {
        // Scroll wheel and left/right arrow hotbar selection
        if (Input.mouseScrollDelta.y > 0 || Input.GetKeyDown(KeyCode.RightArrow)) {
            // Loop to beginning
            if (slot >= slots.Length - 1) {
                slot = 0;
            } else {
                slot++;
            }
        } 
        else if (Input.mouseScrollDelta.y < 0 || Input.GetKeyDown(KeyCode.LeftArrow)) {
            // Loop to end
            if (slot <= 0) {
                slot = slots.Length - 1;
            } else {
                slot--;
            }
        }
        selectedSlot = slots[slot];

        // Make normal configuration on all slots except selected
        foreach (GameObject s in slots) {
            // Selected configuration
            if (s == selectedSlot) {
                // Change UI slot Color
                Color32 color32 = new Color32(179, 168, 167, 255);
                Color color = color32;
                selectedSlot.GetComponent<Image>().color = color; 

                s.GetComponent<SlotInfo>().selected = true;
            }
            // Unselected slots
            else {
                s.GetComponent<Image>().color = Color.white;  // Color for default slot image
                s.GetComponent<SlotInfo>().selected = false;
            }
        }
    }
    public bool AddItem(GameObject Icon, GameObject Weapon, int weaponNumber, bool exists) {
        for (int i = 0; i < slots.Length; i++)
        {
            // If slot is empty, add item
            if (!isFull[i]) {
                Instantiate(Icon, slots[i].transform, false);                   // Hotbar UI image
                slots[i].GetComponent<SlotInfo>().WeaponNumber = weaponNumber;  // Update slot info

                if (exists) {
                    Debug.Log("added existing item");
                    if (Weapon.GetComponent<WeaponInfo>())
                        Weapon.transform.position = player.transform.position + Weapon.GetComponent<WeaponInfo>().Offset;
                    else
                        Weapon.transform.position = player.transform.position;
                    Weapon.transform.parent = player.transform;
                    Weapon.transform.SetSiblingIndex(i);
                    IS.GetComponent<WeaponSystem>().WeaponsHeld[i] = Weapon;

                    if (Weapon.GetComponent<WeaponInfo>()) {
                        Weapon.GetComponent<WeaponInfo>().dropped = false;
                        Weapon.GetComponent<WeaponInfo>().selected = false;
                    }
                }
                else {
                    Debug.Log("new item added");
                    GameObject go = Instantiate(Weapon, player.transform, false);                   // Put weapon as player's child
                    go.transform.SetSiblingIndex(i);
                    IS.GetComponent<WeaponSystem>().WeaponsHeld[i] = go;
                    
                }
                return true;
            }
        }
        return false;
    }
}
