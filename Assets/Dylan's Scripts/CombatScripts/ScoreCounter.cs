using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreCounter : MonoBehaviour
{
    private TextMeshProUGUI scoreCounterText;
    public int scoreValue;
    // Start is called before the first frame update
    void Start()
    {
        scoreCounterText = GetComponent<TextMeshProUGUI>();
        scoreValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreCounterText.text = scoreValue.ToString();
        PlayerInfo.recordedScore = scoreValue;
    }

    private IEnumerator Pulse(int score) {
        for (float i = 1f; i <= 1.2f; i += 0.05f) {
            scoreCounterText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        scoreCounterText.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        scoreValue += score;

        for (float i = 1.2f; i >= 1f; i -= 0.05f) {
            scoreCounterText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        scoreCounterText.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void RunCo(int score) {
        StartCoroutine(Pulse(score));
    }
}
