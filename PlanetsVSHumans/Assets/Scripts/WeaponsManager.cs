using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    Selector select = null;


    // Start is called before the first frame update
    void Start()
    {
        select = GetComponent<Selector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (select.hit.transform.gameObject != null && Input.GetMouseButtonDown(1))
        {

        }
    }
}
