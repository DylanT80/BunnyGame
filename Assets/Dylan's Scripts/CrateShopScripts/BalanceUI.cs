using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalanceUI : MonoBehaviour
{
    public int balance = 0;
    private TextMeshProUGUI text;
    private ShopSystem ShopSystem;
    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        ShopSystem = GameObject.Find("ShopSystem").GetComponent<ShopSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        balance = ShopSystem.balance;
        text.text = balance.ToString();
    }
}
