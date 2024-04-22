using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBehaviour : MonoBehaviour
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

            Debug.Log("Ammo!");
            gameManager.Ammo += 5;
        }
    }
}