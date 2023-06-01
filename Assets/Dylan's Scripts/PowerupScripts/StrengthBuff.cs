using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthBuff : MonoBehaviour
{
    [SerializeField] private float strengthIncreaseAmt = 1;
    [SerializeField] private float heartsIncreaseAmt = 1;
    public float duration = 8;

    private void OnTriggerEnter2D(Collider2D other) {
        // Only when player hits
        if (other.CompareTag("Player")) {
            PlayerInfo pi = other.gameObject.GetComponent<PlayerInfo>();
            // Can't go over 6 hearts
            if (pi.gameObject.GetComponent<PlayerInfo>().maxHealth < 6)
                StartCoroutine(PowerupSequence(pi));
        }
    }

    private IEnumerator PowerupSequence(PlayerInfo pi) {
        // Soft destroy
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        // Activate
        pi.SetStrength(strengthIncreaseAmt);                                                       // Strength                                                       
        Vector3 iScale = pi.gameObject.transform.localScale;                                       // Size
        Vector3 newScale1 = new Vector3(iScale.x*1.1f, iScale.y*1.1f, iScale.z*1.1f);     
        Vector3 newScale2 = new Vector3(iScale.x*1.3f, iScale.y*1.3f, iScale.z*1.3f);     
        Vector3 newScale3 = new Vector3(iScale.x*1.5f, iScale.y*1.5f, iScale.z*1.5f);      
        Vector4 iColor = pi.gameObject.GetComponent<SpriteRenderer>().color;                       // Color
        // Hearts
        pi.gameObject.GetComponent<PlayerInfo>().SetMaxHearts(heartsIncreaseAmt);

            yield return new WaitForSeconds(0.08f);
            pi.gameObject.transform.localScale = newScale1;
            pi.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;

            yield return new WaitForSeconds(0.08f);
            pi.gameObject.transform.localScale = newScale2;
            pi.gameObject.GetComponent<SpriteRenderer>().color = iColor;

            yield return new WaitForSeconds(0.08f);
            pi.gameObject.transform.localScale = newScale3;
            pi.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;


            yield return new WaitForSeconds(0.08f);
            pi.gameObject.transform.localScale = newScale2;
            pi.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;

            yield return new WaitForSeconds(0.08f);
            pi.gameObject.transform.localScale = newScale1;
            pi.gameObject.GetComponent<SpriteRenderer>().color = iColor;

            yield return new WaitForSeconds(0.08f);
            pi.gameObject.transform.localScale = iScale;


        // Deactivate (not really lol)

        Destroy(gameObject);
    }
}
