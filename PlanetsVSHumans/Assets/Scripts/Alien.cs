using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Alien : MonoBehaviour
{
    private GameObject toFollow = null;
    private GameObject instAlien = null;
    public GameObject alien = null;
    private WeaponManager wm;

    Population population = null;
    Timer timer = new Timer();

    private float elapsedTime = 0f;
    public float speed = 20f;
    public float percentDamage = 0.55f;
    public float delay = 5f;
    public float fixTime = 7f;

    public bool canActivate = true;
    private bool isActivate = false;
    private bool canGoBack = false;
    private bool canDestroy = false;

    private Vector3 destPos = new Vector3();
    private Vector3 initPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        population = GetComponent<Population>();
        wm = FindObjectOfType<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canActivate && !isActivate)
            canActivate = timer.Bip(delay);

        if (instAlien == null && isActivate && toFollow != null)
        {
            Debug.Log(toFollow.GetComponent<Population>().populationRate);
            toFollow.GetComponent<Population>().populationRate *= 1 - percentDamage;
            Debug.Log(toFollow.GetComponent<Population>().populationRate);
            isActivate = false;
            canGoBack = false;
        }

        if (Input.GetMouseButtonDown(0) && wm.weapon == Weapon.ALIEN && !isActivate && canActivate)
        {
            RaycastHit hit = Raycaster.Pick();
            if (hit.collider && hit.collider.gameObject.tag == "Planet")
            {
                toFollow = hit.transform.gameObject;
                Vector3 pos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 15f, Camera.main.transform.position.z);
                instAlien = Instantiate(alien, pos, Quaternion.identity);

                isActivate = true;
                canActivate = false;
            }
        }

        if (instAlien != null && isActivate)
        {
            if (toFollow == null)
            {
                canGoBack = true;
                elapsedTime = 0f;
            }

            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / speed;

            if (!canGoBack)
            {
                if (toFollow == null)
                    canDestroy = true;

                destPos = new Vector3(toFollow.transform.position.x, toFollow.transform.position.y + toFollow.transform.localScale.y + 7, toFollow.transform.position.z);
                instAlien.transform.position = Vector3.Lerp(instAlien.transform.position, destPos, percentageComplete);

                canGoBack = timer.Bip(fixTime);

                if (canGoBack)
                    elapsedTime = 0;
            }
            else
            {
                if (toFollow == null)
                    canDestroy = true;

                initPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 15f, Camera.main.transform.position.z);
                instAlien.transform.position = Vector3.Lerp(instAlien.transform.position, initPos, percentageComplete);

                canDestroy = timer.Bip(1);

                if (canDestroy)
                {
                    Destroy(instAlien);
                    elapsedTime = 0;
                }
            }


        }
    }
}
