using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectToFollow;
    public float sensitivity = 20;
    public float speedMovement = 20;
    public float rotateHorizontal;
    private bool playerIsRotating = false;
    public Vector3 offsetWithTargetObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateHorizontal = Input.GetAxis("Mouse X");
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Mouse Button is down");
            playerIsRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            playerIsRotating = false;
        }

        if (playerIsRotating)
        {
            offsetWithTargetObject = Quaternion.AngleAxis(rotateHorizontal * sensitivity, -Vector3.up) * offsetWithTargetObject;
            transform.position = objectToFollow.transform.position + offsetWithTargetObject;
            transform.LookAt(objectToFollow.transform.position);
        } else
        {
            transform.position = objectToFollow.transform.position + offsetWithTargetObject;
            transform.LookAt(objectToFollow.transform.position);
        }
        
    }
}
