using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Updates UI canvas counter
public class EnemyCounter : MonoBehaviour
{
    [System.NonSerialized] public int counter;

    private void Update() {
        GetComponent<TextMeshProUGUI>().text = "Enemies: " + counter;
    }
}
