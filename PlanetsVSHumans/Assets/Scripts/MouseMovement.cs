using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    Vector3 position = new Vector3();

    public float sensivityMove = 0.3f;
    public float sensivityScroll = 10f;
    public float min = 53.5f;
    public float max = 100f;

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
        if (!IsMouseOffTheScreen() && Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetAxis("Mouse ScrollWheel") > 0f && transform.position.y != min)
        {
            focus.Reset();

            position = transform.position;
            
            position += Input.GetAxis("Mouse ScrollWheel") * transform.forward * sensivityScroll;
            position.y = Mathf.Clamp(position.y, min, max);
            
            if (position.y != max)
                transform.position = new Vector3(position.x, position.y, position.z);
        }


        if (!IsMouseOffTheScreen() && Input.GetMouseButton(0))
        {
            focus.Reset();

            position.x -= Input.GetAxis("Mouse X") * sensivityMove;
            position.z -= Input.GetAxis("Mouse Y") * sensivityMove;

            transform.position = new Vector3(position.x, transform.position.y, position.z);
        }
    }

    private bool IsMouseOffTheScreen()
    {
        if (Input.mousePosition.x <= 2f || Input.mousePosition.y <= 2f || Input.mousePosition.x >= Screen.width - 2f || Input.mousePosition.y >= Screen.height - 2f)
            return true;

        return false;
    }
}
