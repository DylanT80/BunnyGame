using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The UI for ammo system
public class WeaponUI : MonoBehaviour
{
    public Sprite DefaultIcon;
    [System.NonSerialized] public Sprite icon;
    private WeaponSystem weaponSystem;
    private GameObject player;
    private Inventory inventory;
    private GameObject SelectedWeapon;
    private WeaponInfo SelectedWeaponInfo;
    private GameObject IS;

    // Return true if there is ammo to shoot, false if there is no ammo

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        weaponSystem = GameObject.Find("InventorySystem").GetComponent<WeaponSystem>();
        inventory = player.GetComponent<Inventory>();
        IS = GameObject.Find("InventorySystem");
    
    }

    // Updates the UI
    public void ChangeUI(int slot) {
        SelectedWeapon = IS.GetComponent<WeaponSystem>().WeaponsHeld[slot];     // Active weapon
        SelectedWeaponInfo = SelectedWeapon.GetComponent<WeaponInfo>();
        icon = SelectedWeaponInfo.WeaponImage;
        UpdateUI();
    }

    // Children of WeaponUI Canvas
    // 0 - ReserveCounter
    // 1 - AmmoCounter
    // 2 - Icon
    public void UpdateUI() {
        BunnyMovement.holdWeapon = true;
        transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = SelectedWeaponInfo.InReserve / SelectedWeaponInfo.InReserveSize;
        transform.GetChild(1).gameObject.GetComponent<Image>().fillAmount = SelectedWeaponInfo.AmmoCount / SelectedWeaponInfo.MagazineSize;
        transform.GetChild(2).gameObject.GetComponent<Image>().sprite = icon;
    }

    public void EmptyUI() {
        
        transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = 100;
        transform.GetChild(1).gameObject.GetComponent<Image>().fillAmount = 100;
        transform.GetChild(2).gameObject.GetComponent<Image>().sprite = DefaultIcon;
    }
}
