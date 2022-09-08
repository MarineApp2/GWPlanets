using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utils;

public class Meteor : MonoBehaviour
{
    private GameObject toFollow = null;
    private GameObject instMeteor = null;

    private WeaponManager wm;

    public GameObject meteor = null;

    Population population = null;
    Timer timer = new Timer();

    private float elapsedTime = 0f;
    public float speed = 2f;
    public float percentDamage = 0.5f;
    public float delay = 3f;

    private bool isShooting = false;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        wm = FindObjectOfType<WeaponManager>();
        population = GetComponent<Population>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canShoot)
            canShoot = timer.Bip(delay);

        if (instMeteor == null && isShooting)
        {
            toFollow.GetComponent<Population>().populationRate *= 1 - percentDamage;

            isShooting = false;
            elapsedTime = 0;
        }

        if (Input.GetMouseButtonDown(0) && wm.w == Weapon.METEOR && !isShooting && canShoot)
        {
            RaycastHit hit = Raycaster.Pick();
            if (hit.collider && hit.collider.gameObject.tag == "Planet")
            {
                toFollow = hit.transform.gameObject;
                instMeteor = Instantiate(meteor, Camera.main.transform.position, Quaternion.identity);

                isShooting = true;
                canShoot = false;
            }
        }
        
        if (instMeteor != null && isShooting)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / speed;

            instMeteor.transform.position = Vector3.Lerp(instMeteor.transform.position, toFollow.transform.position, percentageComplete);
        }
    }
}
