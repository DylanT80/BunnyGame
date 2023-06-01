using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCrate : MonoBehaviour
{
    public GameObject[] drops;
    public bool Open = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Open = GetComponent<Interactable>().finished;

        if (Open) {
            // Drop a random item from the list
            int iRandom = Random.Range(0, drops.Length);
            Vector2 pos = transform.position;
            Vector2 newPos = new Vector2(pos.x, pos.y + 2.5f);
            Instantiate(drops[iRandom], newPos, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
