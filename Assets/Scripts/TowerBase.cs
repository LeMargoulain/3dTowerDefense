using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public LayerMask groundLayer;
    public bool isColliding;
    private Renderer towerbaserenderer;
    private Color originalColor;
    void Start()
    {
        towerbaserenderer = GetComponent<Renderer>();
        originalColor = towerbaserenderer.material.color;
        if (GameManager.getMoney() < Player.towerCost)
        {
            towerbaserenderer.sharedMaterial.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.getMoney() >= Player.towerCost && !isColliding)
        {
            towerbaserenderer.sharedMaterial.color = originalColor;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != groundLayer)
        {
            isColliding = true;
            towerbaserenderer.sharedMaterial.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != groundLayer)
        {
            isColliding = false;
            if (GameManager.getMoney() > Player.towerCost)
            {
                towerbaserenderer.sharedMaterial.color = originalColor;
            }
        }
    }
}
