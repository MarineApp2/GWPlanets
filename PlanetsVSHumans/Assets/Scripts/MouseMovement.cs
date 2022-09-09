using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    Vector3 position = new Vector3();

    public float sensivityMove = 1f;
    public float maxX = 75f;
    public float minX = -75f;
    public float maxY = 75f;
    public float minY = -75f;
    public float sensivityScroll = 50f;
    public float minZoom = 0f;
    public float maxZoom = 60f;

    private Focus focus;

    // Start is called before the first frame update
    void Start()
    {
        focus = GetComponent<Focus>();
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsMouseOffTheScreen() && Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetAxis("Mouse ScrollWheel") > 0f && transform.position.y != minZoom)
        {
            position = transform.position;

            focus.Reset();

            position += Input.GetAxis("Mouse ScrollWheel") * transform.forward * sensivityScroll;
            position.y = Mathf.Clamp(position.y, minZoom, maxZoom);

            if (position.y != maxZoom)
                transform.position = position;
        }


        if (!focus.focused && !IsMouseOffTheScreen() && Input.GetMouseButton(0))
        {
            position = transform.position;

            position.x -= Input.GetAxis("Mouse X") * sensivityMove;
            position.z -= Input.GetAxis("Mouse Y") * sensivityMove;

            position.x = Mathf.Clamp(position.x, minX, maxX);
            position.z = Mathf.Clamp(position.z, minY, maxY);

            transform.position = position;
        }
    }

    private bool IsMouseOffTheScreen()
    {
        if (Input.mousePosition.x <= 2f || Input.mousePosition.y <= 2f || Input.mousePosition.x >= Screen.width - 2f || Input.mousePosition.y >= Screen.height - 2f)
            return true;

        return false;
    }
}
