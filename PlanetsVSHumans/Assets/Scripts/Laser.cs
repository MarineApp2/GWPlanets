using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Laser : MonoBehaviour
{
    public GameObject vfx = null;
    private GameObject target = null;

    private WeaponManager wm;

    Timer timerDelay = new Timer();
    Timer timer = new Timer();

    public float percentDamage = 0.6f;
    public float delay = 3f;

    private bool isShooting = false;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        wm = FindObjectOfType<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canShoot)
            canShoot = timerDelay.Bip(delay);

        if (Input.GetMouseButtonDown(0) && wm.weapon == Weapon.LASER && !isShooting && canShoot)
        {
            RaycastHit hit = Raycaster.Pick();
            if (hit.collider && hit.collider.gameObject.tag == "Planet")
            {
                Transform child = hit.transform.GetChild(1);
                target = hit.transform.gameObject;
                child.gameObject.SetActive(true);

                isShooting = true;
                canShoot = false;

                return;
            }
        }

        if (isShooting)
        {
            bool time = timer.Bip(2f);
            if (time)
            {
                Population pop = target.GetComponent<Population>();
                pop.populationRate *= 1 - percentDamage;
                Transform child = target.transform.GetChild(1);
                child.gameObject.SetActive(false);


                target = null;
                isShooting = false;
            }
        }
    }
}
