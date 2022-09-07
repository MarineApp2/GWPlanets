using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    GameObject selectedObject = null;
    GameObject interactableObject = null;

    [HideInInspector] public RaycastHit hit = new RaycastHit();

    private void Update()
    {
        hit = CastRay();

        if (hit.collider != null)
        {
            if (!hit.collider.CompareTag("Pickable"))
            {
                interactableObject = null;
                return;
            }

            interactableObject = hit.collider.gameObject;
            selectedObject = interactableObject;
        }

        if (hit.collider == null)
            interactableObject = null;
    }
                      
    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        Vector3 worldMousePosFar  = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
}
