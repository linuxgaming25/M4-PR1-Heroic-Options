using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public GameBehavior gameManager;

    void Start()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Destroy(this.transform.parent.gameObject);

            Debug.Log("Item collected!");
        }
        gameManager.Items += 1;
        gameManager.PrintLootReport();

    }
}
