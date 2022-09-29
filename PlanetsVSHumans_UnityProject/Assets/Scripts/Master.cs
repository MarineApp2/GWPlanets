using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    public List<GameObject> planets = new List<GameObject>();
    public int planetCount = 0;
    public long totalPopulation = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        totalPopulation = 0;

        foreach (GameObject planet in planets)
        {
            totalPopulation += planet.GetComponent<Population>().population;
        }

        planetCount = planets.Count;
    }
}