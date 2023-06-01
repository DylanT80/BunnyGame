using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls weapon stats and ammo system
public class WeaponInfo : MonoBehaviour
{
    [System.NonSerialized] public bool dropped = false;
    [System.NonSerialized] public bool selected = false;
    public float damage;
    public float MagazineSize;
    public float InReserve;
    [System.NonSerialized] public float InReserveSize;  // ammo restore & ammo UI
    public float AmmoCount;
    [System.NonSerialized] public float AmmoCountSize;  // Keep track for ammo restore
    public Sprite WeaponImage;
    [System.NonSerialized] public AudioSource audio;
    public AudioClip[] audios;
    private PlayerInfo playerInfo;
    public Vector3 Offset;
    public bool WaitForAudio;
    public float ReloadTime;
    public float ReloadSpeedFactor; // Big for large magazine sizes, small for small magazine sizes
    private WeaponUI WUI;
    public bool reloading = false;
    private GameObject Stats;
    private void Start() {
        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        WUI = GameObject.Find("WeaponUI").GetComponent<WeaponUI>();
        audio = GetComponent<AudioSource>();
        InReserveSize = InReserve;
        AmmoCountSize = AmmoCount;
        Stats = GameObject.Find("PlayerStats");
    }

    public void SetDamage(float adjustment) {
        damage += adjustment;
    }

    public void SetMagazineSize(float adjustment) {
        MagazineSize += adjustment;
    }

    public void SetReserveSize(float adjustment) {
        InReserve += adjustment;
    }

    // Ammo system

    public bool Shoot() {
        // No shooting during shopping or looking at player stats
        if ((GameObject.FindGameObjectWithTag("Shop") && GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>().Open) || Stats.transform.GetChild(1).gameObject.activeSelf) {
            return false;
        } 
        // If empty magazine
        if (AmmoCount <= 0) {   
            // Play empty audio clip
            audio.clip = audios[1];
            if (!audio.isPlaying) {
                audio.Play();
            }
            return false;
        }
        // Reduce ammo and update UI if able to shoot
        audio.clip = audios[0];
        if (WaitForAudio) {
            if (!audio.isPlaying) {
                audio.Play();
            }
        }
        else {
            audio.Play(); 
        }
        AmmoCount--;
        UpdateUI();

        return true;
    }

    public void Reload() {
        StartCoroutine(ReloadSequence());
    }
    // Return true if there is ammo to reload, false if there is no ammo
    private IEnumerator ReloadSequence() {
        if (reloading)
            yield break;
        reloading = true;
        // AmmoCount is already full
        if (AmmoCount == MagazineSize) {

        }
        // Ammo in reserve is empty
        else if (InReserve <= 0) {
            // Play empty audio clip
            audio.clip = audios[1];
            if (!audio.isPlaying) {
                audio.Play();
            }
        }
        // Else, take normal magainze amount that fills the clip
        else {
            float amtToAdd = MagazineSize - AmmoCount;
            // Not enough reserve
            if (amtToAdd >= InReserve) {
                amtToAdd = InReserve;
            }
            int finalAmmo = (int) (AmmoCount + amtToAdd);
            int finalReserve = (int) (InReserve - amtToAdd);
            // Enough reserve
            float start = 0;
            while (start < amtToAdd && AmmoCount < MagazineSize && InReserve > 0 && (!Input.GetKey(KeyCode.Mouse0) && selected)) {  // Break from reloading if shoot or not selected
                yield return null;
                float adjustment = (1 / ReloadTime) * ReloadSpeedFactor;
                AmmoCount += adjustment;
                InReserve -= adjustment;
                start += adjustment;
                    
                // Visuals only like the integers
                if (start % 1 == 0) {
                    UpdateUI();
                }
            }
            AmmoCount = Mathf.Round(AmmoCount);
            InReserve = Mathf.Round(InReserve);
            
            UpdateUI(); 
        }
        reloading = false;
    }

    // Destroys the actual weapon when not picked up after some time
    private void FixedUpdate() {
        if (dropped) {
            Invoke("DestroyMe", 20);
        }
        else if (!dropped) {
            CancelInvoke("DestroyMe");
        }

        if (AmmoCount > AmmoCountSize) {
            AmmoCount = AmmoCountSize;
        }
        if (InReserve > InReserveSize) {
            InReserve = InReserveSize;
        }
    }

    private void DestroyMe() {
        Destroy(gameObject);
    }

    public void UpdateUI() {
        if (selected)
            WUI.UpdateUI();
    }
}
