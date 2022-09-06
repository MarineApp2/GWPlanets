using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utils;

public class OrbitalGenerator : MonoBehaviour
{
    public GameObject prefab;

    public float generationRadius = 999.9f;

    public int entities = 10;

    public float orbitalRotationSpeed = 9999.9f;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 range = new Vector2(-generationRadius, generationRadius);
        float[] steps = new float[entities];
        for (int i = 0; i < entities; ++i)
        {
            steps[i] = generationRadius / entities * (i + 1);
        }

        for (int i = 0; i < entities; ++i)
        {
            GameObject star = Instantiate(prefab);
            star.transform.parent = transform;
            star.transform.position = transform.position;
            star.transform.position += new Vector3(Mathf.Cos(Random.Range(0.0f, Numerics.TAU)),
                0.0f,
                Mathf.Sin(Random.Range(0.0f, Numerics.TAU))).normalized * steps[i];

            float dist = (transform.position - star.transform.position).magnitude;

            Orbital o = star.GetComponent<Orbital>();
            o.pivotObject = gameObject;
            o.rotationSpeed = orbitalRotationSpeed / dist;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}