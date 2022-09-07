using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float attractionForce = 1.0f;

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
        int destroyedCount = 0;
        List<int> destroyed = new List<int>();
        int id = 0;

        foreach (GameObject body in bodies)
        {
            if (body == null)
            {
                ++destroyedCount;
                destroyed.Add(id);
                ++id;

                continue;
            }

            Vector3 dir = (transform.position - body.transform.position).normalized;
            body.transform.position += dir * (attractionForce * Time.deltaTime);

            if ((transform.position - body.transform.position).magnitude <= hole)
            {
                ++destroyedCount;
                destroyed.Add(id);
            }

            ++id;
        }

        for (int i = 0; i < destroyedCount; ++i)
        {
            GameObject body = bodies[i];
            bodies.Remove(body);
            Destroy(body);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bodies.Add(other.gameObject);
        other.GetComponent<Orbital>().pivotObject = gameObject;
        other.GetComponent<Rigidbody>().detectCollisions = false;
    }

    private void OnTriggerExit(Collider other)
    {
        bodies.Remove(other.gameObject);
        other.GetComponent<Rigidbody>().detectCollisions = true;
    }
}
