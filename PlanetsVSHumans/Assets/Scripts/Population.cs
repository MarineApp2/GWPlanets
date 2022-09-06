using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utils;

enum GrowthFunction
{
    SquareTime,
    CubicTime,
    ExponentialTime
};

public class Population : MonoBehaviour
{
    // planet's population number
    public long population = 1000000000;
    private long startPopulation = 0;

    // planet's can only stand 10 billion individuals (as in 10 billion consumers)
    // planet's max population (in billion)
    [SerializeField] private int maxPopulation = 10;
    private long max = 0;

    // tick for population growth (growth per second)
    [SerializeField] private float growthRate = 1.0f;
    [SerializeField] private GrowthFunction growthFct = GrowthFunction.SquareTime;

    private Timer timer = new Timer();

    // Start is called before the first frame update
    void Start()
    {
        startPopulation = population;
        max = maxPopulation * Numerics.billion;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.Bip(growthRate))
            population = startPopulation + GrowthAmount();

        if (population >= max)
            BlackHole();

        timer.UpdateTimeStamp();
    }

    private long GrowthAmount()
    {
        long p = 0;
        switch (growthFct)
        {
            case GrowthFunction.SquareTime:
                p = (long)timer.SqrTime();
                break;
            case GrowthFunction.CubicTime:
                p = (long)timer.CubTime();
                break;
            case GrowthFunction.ExponentialTime:
                p = (long)timer.ExpTime();
                break;
            default:
                break;
        }

        if (p < 0)
            p = long.MaxValue;

        return p;
    }

    private void BlackHole()
    {
        Debug.Log("Black hole");
        //instantiate prefab blackhole
        //destroy this
    }
}