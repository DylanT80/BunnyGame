using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    public bool Open = false;
    public Button CloseButton;
    // Start is called before the first frame update
    void Start()
    {
        CloseButton.onClick.AddListener(Close);
    }

    // Update is called once per frame
    void Update()
    {
        Open = GetComponent<Interactable>().finished;

        if (Open) {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        if (!Open || !GetComponent<Interactable>().PlayerInRange) {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void Close() {
        GetComponent<Interactable>().counter.GetComponent<Image>().fillAmount = 1;
    }
}
