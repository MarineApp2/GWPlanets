using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    Selector select = null;

    private GameObject toFollow = null;
    private GameObject instMeteor = null;

    public GameObject meteor = null;

    Population population = null;

    private float elapsedTime = 0f;
    public float speed = 2f;
    public float damage = 0.5f;

    private bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        select = GetComponent<Selector>();
        population = GetComponent<Population>();
    }

    // Update is called once per frame
    void Update()
    {
        if (instMeteor == null && isShooting)
        {
            Debug.Log(toFollow.GetComponent<Population>().populationRate);
            toFollow.GetComponent<Population>().populationRate *= 1 - damage;
            Debug.Log(toFollow.GetComponent<Population>().populationRate);

            isShooting = false;
            elapsedTime = 0;
        }

        if (select.hit.collider != null && Input.GetKeyDown(KeyCode.Alpha1) && !isShooting)
        {
            toFollow = select.hit.transform.gameObject;
            instMeteor = Instantiate(meteor, Camera.main.transform.position, Quaternion.identity);
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAH");

            isShooting = true;
        }
        
        if (instMeteor != null && isShooting)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / speed;

            instMeteor.transform.position = Vector3.Lerp(instMeteor.transform.position, toFollow.transform.position, percentageComplete);
        }
    }
}
