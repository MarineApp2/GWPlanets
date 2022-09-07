using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    Selector select = null;

    private GameObject toFollow = null;
    private GameObject instMeteor = null;
    public GameObject meteor = null;

    bool isShoot = false;

    private float elapsedTime = 0f;
    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        select = GetComponent<Selector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (select.hit.collider != null && Input.GetMouseButtonDown(1))
        {
            toFollow = select.hit.transform.gameObject;
            instMeteor = Instantiate(meteor, Camera.main.transform.position, Quaternion.identity);
            isShoot = true;
        }
        
        if (instMeteor != null && isShoot)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / speed;

            instMeteor.transform.position = Vector3.Lerp(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z), toFollow.transform.position, percentageComplete);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Destroy");
        Destroy(this.gameObject);
    }
}
