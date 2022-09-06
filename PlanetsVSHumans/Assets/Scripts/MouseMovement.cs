using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    Vector2 position = new Vector2();
    public float sensivity = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsMouseOffTheScreen() && Input.GetMouseButton(0))
        {
            position.x -= Input.GetAxis("Mouse X") * sensivity;
            position.y -= Input.GetAxis("Mouse Y") * sensivity;

            transform.position = new Vector3(position.x, 53.5f, position.y);
        }
    }

    private bool IsMouseOffTheScreen()
    {
        if (Input.mousePosition.x <= 2f || Input.mousePosition.y <= 2f || Input.mousePosition.x >= Screen.width - 2f || Input.mousePosition.y >= Screen.height - 2f)
            return true;

        return false;
    }
}
