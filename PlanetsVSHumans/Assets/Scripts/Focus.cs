using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utils;

public class Focus : MonoBehaviour
{
    // can focus Pickable and Focusable game objects
    private GameObject target = null;
    private GameObject focusTarget = null;
    [HideInInspector] public bool focused = false;

    public float focusDistance = 10.0f;
    public float focusSpeed = 0.3f;
    public Vector3 offset = new Vector3(0, -3, -3);
    private Vector3 camDir = Vector3.zero;

    public float doubleClickTime = 0.3f;
    private bool timeout = true;
    private Timer timer = new Timer();

    // Start is called before the first frame update
    void Start()
    {
        camDir = transform.position.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (focusTarget)
            transform.position = Vector3.Lerp(transform.position, offset + focusTarget.transform.position +
                camDir * focusDistance, focusSpeed);

        if (timeout && Input.GetMouseButtonUp(0))
        {
            RaycastHit hit = Raycaster.Pick();
            if (hit.collider)
            {
                target = hit.collider.gameObject;
                timeout = false;
                return;
            }
        }

        if (!target)
            return;

        if (!timeout)
        {
            if (!target)
            {
                Reset();
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit = Raycaster.Pick();
                if (hit.collider && target == hit.collider.gameObject)
                {
                    focusTarget = target;
                    focused = true;
                }
            }

            timeout = timer.Bip(doubleClickTime);
        }
        else
        {
            target = null;
        }
    }

    public void Reset()
    {
        if (!focusTarget)
            return;

        target = null;
        focusTarget = null;
        focused = false;
    }
}