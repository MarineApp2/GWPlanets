using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using Utils;

public class OrbitalGenerator : MonoBehaviour
{
    public GameObject prefab;

    public float generationRadius = 999.9f;
    public int entities = 10;
    public float orbitalRotationSpeed = 9999.9f;

    public float minimumSize = 1.0f;
    public float maximumSize = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        float[] steps = new float[entities];
        for (int i = 0; i < entities; ++i)
        {
            steps[i] = generationRadius / entities * (i + 1);
        }

        for (int i = 0; i < entities; ++i)
        {
            GameObject go = Instantiate(prefab);
            go.transform.parent = transform;
            go.transform.position = transform.position;
            go.transform.position += new Vector3(Mathf.Cos(Random.Range(0.0f, Numerics.TAU)),
                0.0f,
                Mathf.Sin(Random.Range(0.0f, Numerics.TAU))).normalized * steps[i];
            float ls = go.transform.parent.transform.lossyScale.x;
            float s = Random.Range(minimumSize, maximumSize) / ls;
            go.transform.localScale = new Vector3(s, s, s);
            float dist = (transform.position - go.transform.position).magnitude;

            Orbital o = go.GetComponent<Orbital>();
            o.pivotObject = gameObject;
            o.rotationSpeed = orbitalRotationSpeed / dist;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}