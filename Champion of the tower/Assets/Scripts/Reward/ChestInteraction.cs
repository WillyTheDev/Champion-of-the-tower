using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject rewardCanvas;
    void Start()
    {
        rewardCanvas = GameObject.FindGameObjectWithTag("Reward Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rewardCanvas.GetComponent<Canvas>().enabled = true;
        }
    }
}
