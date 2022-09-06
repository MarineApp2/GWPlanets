using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy : MonoBehaviour
{
    private GameObject[] planets;
    private GameObject[] stars;
    private GameObject[] blackholes;

    // Start is called before the first frame update
    void Start()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        stars = GameObject.FindGameObjectsWithTag("Star");
        blackholes = GameObject.FindGameObjectsWithTag("BlackHole");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}