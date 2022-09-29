using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using Utils;

public class Population : MonoBehaviour
{
    public GameObject blackHolePrefab;

    public float populationRate = 0.0f;
    public long population = 0L;

    // planet's can only stand 10 billion individuals (as in 10 billion consumers)
    // planet's max population (in billion)
    [SerializeField] private int maxPopulation = 10;

    // tick for population growth (growth per second)
    [SerializeField] private float growthTick = 1.0f;
    [SerializeField] private float growthSpeedMin = 1.0f;
    [SerializeField] private float growthSpeedMax = 5.0f;
    private float growthRate = 0.01f;

    private Utils.Timer timer = new Utils.Timer();

    // only planets have the Population script
    Master master;

    // Start is called before the first frame update
    void Start()
    {
        growthRate *= Random.Range(growthSpeedMin, growthSpeedMax);

        master = FindObjectOfType<Master>();
        master.planets.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.Bip(growthTick))
        {
            populationRate += growthRate;

            population = (long)(populationRate * (double)(maxPopulation * Numerics.billion));

            Transform child0 = transform.GetChild(2).GetChild(0);
            for (int i = 0; i < child0.childCount; ++i)
                child0.GetChild(i).GetComponent<MeshRenderer>().materials[0].SetFloat("_Alpha", 1.0f - populationRate);
            Transform child1 = transform.GetChild(2).GetChild(1);
            for (int i = 0; i < child1.childCount; ++i)
                child1.GetChild(i).GetComponent<MeshRenderer>().materials[0].SetFloat("_Alpha", 1.0f - populationRate);
        }

        if (populationRate >= 1.0f)
            BlackHole();
    }

    private void BlackHole()
    {
        GameObject bh = Instantiate<GameObject>(blackHolePrefab);
        bh.transform.position = transform.position;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        master.planets.Remove(gameObject);
    }
}