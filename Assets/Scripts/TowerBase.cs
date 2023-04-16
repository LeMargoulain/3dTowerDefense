using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public LayerMask groundLayer;
    public bool isColliding = false;
    private Renderer towerbaserenderer;
    private Color originalColor;
    void Start()
    {
        towerbaserenderer = GetComponent<Renderer>();
        originalColor = towerbaserenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != groundLayer)
        {
            Debug.Log("trigger");
            isColliding = true;
            towerbaserenderer.sharedMaterial.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != groundLayer)
        {
            isColliding = false;
            towerbaserenderer.sharedMaterial.color = originalColor;
        }
    }
}
