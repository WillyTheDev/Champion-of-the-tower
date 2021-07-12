using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform cam;
    // Update is called once per frame

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
