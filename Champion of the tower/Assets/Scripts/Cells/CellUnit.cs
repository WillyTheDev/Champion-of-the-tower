using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellUnit : MonoBehaviour
{
    // Start is called before the first frame update
    private Material cellMaterial;
    private bool canBeTargeted = true;
    void Start()
    {
        cellMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canBeTargeted)
        {
            cellMaterial.color = Color.red;
        }
    }

    private void OnMouseEnter()
    {
        if (canBeTargeted)
        {
            cellMaterial.color = Color.green;
            PlayerController.targetCell = gameObject;
        }
    }

    private void OnMouseExit()
    {
        if (canBeTargeted)
        {
            cellMaterial.color = new Color(1, 0.4622955f, 0.25f, 0.6f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log(gameObject.name + "Can't be targeted because there is an Obstcale");
            canBeTargeted = false;
        }
    }


}
