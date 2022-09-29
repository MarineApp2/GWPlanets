using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbital : MonoBehaviour
{
    public float rotationSpeed;
    public bool clockwise = true;
    public GameObject pivotObject;

    void Update()
    {
        float clockwisef = -1.0f;
        if (clockwise)
            clockwisef = 1.0f;
        transform.RotateAround(pivotObject.transform.position, new Vector3(0, 1, 0), rotationSpeed * clockwisef * Time.deltaTime);
    }
}