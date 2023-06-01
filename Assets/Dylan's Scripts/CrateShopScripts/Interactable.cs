using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public float target;
    public float second;
    public bool PlayerInRange;
    public float TimeToOpen;
    private GameObject PopUp;
    private Color iColorC;
    private Color iColorF;
    private Color iColorT;
    public GameObject counter;
    private GameObject fill;
    private GameObject text;
    private bool holding;
    public bool finished;   // If holding E is done
    // Start is called before the first frame update
    void Start()
    {
        PopUp = transform.GetChild(0).gameObject;

        counter = PopUp.transform.GetChild(0).gameObject;
        fill = PopUp.transform.GetChild(1).gameObject;
        text = PopUp.transform.GetChild(2).gameObject;

        iColorC = counter.GetComponent<Image>().color;
        iColorF = fill.GetComponent<Image>().color;
        iColorT = text.GetComponent<TextMeshProUGUI>().color;
        
        PlayerInRange = false;
        finished = false;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerInRange) {
            if (finished) {}
            else if (Input.GetKeyDown(KeyCode.E)) {
                holding = true;
                Open();
            }
            else if (Input.GetKeyUp(KeyCode.E)) {
                holding = false;
                Close();
            }
        }
        else {
            finished = false;
            holding = false;
            Close();
        }

        if (counter.GetComponent<Image>().fillAmount <= 0) {
            finished = true;
        }
        else {
            finished = false;
        }
    }

    // For popup holding counter
    public void Open() {
        StartCoroutine(OpeningSequence());
    }

    public void Close() {
        StartCoroutine(RefillSequence());
    }
    private IEnumerator OpeningSequence() {
        while (counter.GetComponent<Image>().fillAmount > 0 && holding && PlayerInRange) {
            counter.GetComponent<Image>().fillAmount -= 1 / TimeToOpen;
            yield return null;
        }
    }

    private IEnumerator RefillSequence() {
        while (counter.GetComponent<Image>().fillAmount < 1 && !holding) {
            counter.GetComponent<Image>().fillAmount += 1 / TimeToOpen;
            yield return null;
        }
    }







    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<PolygonCollider2D>().isTrigger = true;
            PlayerInRange = true;
            StartCoroutine(FadeInSequence());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<PolygonCollider2D>().isTrigger = false;
            PlayerInRange = false;
            StartCoroutine(FadeOutSequence());
        }
    }

    private IEnumerator FadeInSequence() {
        var alpha = counter.GetComponent<Image>().color.a;
        for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / second)
        {
           float a = Mathf.Lerp(alpha, target / 255 , t);
            //change color as you want
            counter.GetComponent<Image>().color = new Color(iColorC.r, iColorC.g, iColorC.b, a);
            fill.GetComponent<Image>().color = new Color(iColorF.r, iColorF.g, iColorF.b, a);
            text.GetComponent<TextMeshProUGUI>().color = new Color(iColorT.r, iColorT.g, iColorT.b, a);
            yield return null;
        }
    }
    
    private IEnumerator FadeOutSequence() {
        for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / second)
        {
            //change color as you want
            counter.GetComponent<Image>().color = new Color(iColorC.r, iColorC.g, iColorC.b, Mathf.Lerp(target / 255, 0 , t));
            fill.GetComponent<Image>().color = new Color(iColorF.r, iColorF.g, iColorF.b, Mathf.Lerp(target / 255, 0 , t));
            text.GetComponent<TextMeshProUGUI>().color = new Color(iColorT.r, iColorT.g, iColorT.b, Mathf.Lerp(target / 255, 0 , t));
            yield return null;
        }
    }
}
