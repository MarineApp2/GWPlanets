using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utils;

public class Galaxy : MonoBehaviour
{
    public GameObject starPrefab;
    private List<GameObject> stars = new List<GameObject>();

    public float generationRangeMin = -1000;
    public float generationRangeMax = 1000;

    public int num = 10;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 range = new Vector2(generationRangeMin, generationRangeMax);

        for (int i = 0; i < num; ++i)
        {
            GameObject star = Instantiate(starPrefab);
            star.transform.parent = transform;
            star.transform.position = transform.position + Numerics.RNGPosition(range, Vector2.zero, range); ;

            float dist = (transform.position - star.transform.position).magnitude;

            Orbital o = star.GetComponent<Orbital>();
            o.pivotObject = gameObject;
            o.rotationSpeed = 10000.0f / dist;

            stars.Add(star);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}