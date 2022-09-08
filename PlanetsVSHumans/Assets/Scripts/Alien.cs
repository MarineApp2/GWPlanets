using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Alien : MonoBehaviour
{
    Selector select = null;

    private GameObject toFollow = null;
    private GameObject instAlien = null;
    public GameObject alien = null;

    Population population = null;
    Timer timer = new Timer();

    private float elapsedTime = 0f;
    public float speed = 20f;
    public float percentDamage = 0.55f;
    public float delay = 5f;
    public float fixTime = 7f;

    private bool canActivate = true;
    private bool isActivate = false;
    private bool canGoBack = false;
    private bool canDestroy = false;

    private Vector3 destPos = new Vector3();
    private Vector3 initPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        select = GetComponent<Selector>();
        population = GetComponent<Population>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canActivate && !isActivate)
            canActivate = timer.Bip(delay);

        if (instAlien == null && isActivate)
        {
            Debug.Log(toFollow.GetComponent<Population>().populationRate);
            toFollow.GetComponent<Population>().populationRate *= 1 - percentDamage;
            Debug.Log(toFollow.GetComponent<Population>().populationRate);
            isActivate = false;
            canGoBack = false;
        }

        if (select.hit.collider != null && Input.GetKeyDown(KeyCode.Alpha2) && !isActivate && canActivate)
        {
            toFollow = select.hit.transform.gameObject;
            instAlien = Instantiate(alien, Camera.main.transform.position, Quaternion.identity);

            isActivate = true;
            canActivate = false;
        }

        if (instAlien != null && isActivate)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / speed;

            if (!canGoBack)
            {
                destPos = new Vector3(toFollow.transform.position.x, toFollow.transform.position.y + toFollow.transform.localScale.y + 2, toFollow.transform.position.z);
                instAlien.transform.position = Vector3.Lerp(instAlien.transform.position, destPos, percentageComplete);

                canGoBack = timer.Bip(fixTime);

                if (canGoBack)
                    elapsedTime = 0;
            }
            else
            {
                initPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 5f, Camera.main.transform.position.z);
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