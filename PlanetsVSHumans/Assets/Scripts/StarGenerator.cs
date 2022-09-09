using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class StarGenerator : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();

    public float generationRadius = 999.9f;
    public int maxEntities = 10;
    private int entityCount = 0;

    public float orbitalRotationSpeed = 9999.9f;

    public float generationTime = 1.0f;
    private Timer generationTimer = new Timer();

    public float minimumSize = 1.0f;
    public float maximumSize = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        float[] steps = new float[maxEntities];
        for (int i = 0; i < maxEntities; ++i)
        {
            steps[i] = generationRadius / maxEntities * (i + 1);
        }

        GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Count)];
        GameObject go = Instantiate(randomPrefab);
        go.transform.parent = transform;
        go.transform.position = transform.position;
        go.transform.position += new Vector3(Mathf.Cos(Random.Range(0.0f, Numerics.TAU)),
            0.0f,
            Mathf.Sin(Random.Range(0.0f, Numerics.TAU))).normalized * steps[entityCount++];
        float ls = go.transform.parent.transform.lossyScale.x;
        float s = Random.Range(minimumSize, maximumSize) / ls;
        go.transform.localScale = new Vector3(s, s, s);
        float dist = (transform.position - go.transform.position).magnitude;

        Orbital o = go.GetComponent<Orbital>();
        o.pivotObject = gameObject;
        o.rotationSpeed = orbitalRotationSpeed / dist;
    }

    // Update is called once per frame
    void Update()
    {
        if (entityCount < maxEntities && generationTimer.Bip(generationTime))
        {
            float[] steps = new float[maxEntities];
            for (int i = 0; i < maxEntities; ++i)
            {
                steps[i] = generationRadius / maxEntities * (i + 1);
            }

            GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Count)];
            GameObject go = Instantiate(randomPrefab);
            go.transform.parent = transform;
            go.transform.position = transform.position;
            go.transform.position += new Vector3(Mathf.Cos(Random.Range(0.0f, Numerics.TAU)),
                0.0f,
                Mathf.Sin(Random.Range(0.0f, Numerics.TAU))).normalized * steps[entityCount++];
            float ls = go.transform.parent.transform.lossyScale.x;
            float s = Random.Range(minimumSize, maximumSize) / ls;
            go.transform.localScale = new Vector3(s, s, s);
            float dist = (transform.position - go.transform.position).magnitude;

            Orbital o = go.GetComponent<Orbital>();
            o.pivotObject = gameObject;
            o.rotationSpeed = orbitalRotationSpeed / dist;
        }
    }
}
