using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 1f;
    public Vector3 Offset = new Vector3(0, 2f ,0);
    public Vector3 RandomizeIntensity = new Vector3(2.5f, 2.5f, 0);
    // Start is called before the first frame update
    void Start()
    {
        transform.position += Offset;
        transform.position += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
        Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
        Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));
        Destroy(gameObject, DestroyTime);
    }
}
