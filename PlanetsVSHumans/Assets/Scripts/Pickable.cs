using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public GameObject go;

    Selector select;

    private void Start()
    {
        select = GetComponent<Selector>();
    }

    void Update()
    {
        if (select.hit.collider != null)
        {
            if (select.hit.transform.gameObject.tag == "Pickable")
            {
                go = select.hit.transform.gameObject;
                go.GetComponent<Outline>().enabled = true;
            }
        }
        if (go != null && select.hit.collider == null)
        {
            go.GetComponent<Outline>().enabled = false;
            go = null;
        }
    }
}
