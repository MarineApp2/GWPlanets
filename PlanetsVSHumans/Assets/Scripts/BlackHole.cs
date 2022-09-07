using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float attractionForce = 1.0f;
    public float rotationSpeed = 100.0f;

    // attracted planets, stars
    private List<GameObject> bodies = new List<GameObject>();

    private float hole;

    // Start is called before the first frame update
    void Start()
    {
        hole = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> destroyed = new List<GameObject>();

        foreach (GameObject body in bodies)
        {
            if (body == null)
            {
                destroyed.Add(body);
                continue;
            }

            Vector3 dir = transform.position - body.transform.position;
            body.transform.position += dir.normalized * (attractionForce * Time.deltaTime);

            body.GetComponent<Orbital>().rotationSpeed = rotationSpeed / dir.magnitude;

            if ((transform.position - body.transform.position).magnitude <= hole)
            {
                Destroy(body);
            }
        }

        foreach (GameObject go in destroyed)
        {
            bodies.Remove(go);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().detectCollisions = false;

        bodies.Add(other.gameObject);

        GameObject galaxyBlackHoleInstance = GameObject.Find("GalaxyBlackHole");
        
        for (int i = 0; i < other.transform.childCount; ++i)
        {
            Transform child = other.transform.GetChild(i);
            Orbital ot = child.GetComponent<Orbital>();
            ot.pivotObject = galaxyBlackHoleInstance;
            child.transform.parent = other.transform.parent;
        }
        other.transform.parent = null;

        Orbital o = other.GetComponent<Orbital>();
        o.pivotObject = gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        bodies.Remove(other.gameObject);
        other.GetComponent<Rigidbody>().detectCollisions = true;
    }
}