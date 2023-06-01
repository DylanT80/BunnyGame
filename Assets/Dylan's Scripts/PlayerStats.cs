using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    public Button OpenButton;
    public Button CloseButton;
    private PlayerInfo PI;
    private void Start() {
        OpenButton.onClick.AddListener(Open);
        CloseButton.onClick.AddListener(Close);

        PI = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
    }

    private void Open() {
        transform.GetChild(1).gameObject.SetActive(true);
    }
    private void Close() {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    private void Update() {
        if (transform.GetChild(1).gameObject.activeSelf) {
            GameObject container = transform.GetChild(1).gameObject;

            GameObject health = container.transform.GetChild(1).gameObject;
            GameObject strength = container.transform.GetChild(2).gameObject;
            GameObject speed = container.transform.GetChild(3).gameObject;

            GameObject healthNo = health.transform.GetChild(1).gameObject;
            GameObject strengthNo = strength.transform.GetChild(1).gameObject;
            GameObject speedNo = speed.transform.GetChild(1).gameObject;

            healthNo.GetComponent<TextMeshProUGUI>().text = PI.maxHealth.ToString();
            strengthNo.GetComponent<TextMeshProUGUI>().text = PI.strength.ToString();
            speedNo.GetComponent<TextMeshProUGUI>().text = PI.speed.ToString();
        }
    }
}
