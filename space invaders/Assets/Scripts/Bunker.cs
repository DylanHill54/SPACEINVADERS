using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("invader") ||
            col.gameObject.layer == LayerMask.NameToLayer("laser") ||
            col.gameObject.layer == LayerMask.NameToLayer("missile"))
        {
            if (health > 0)
            {
                health--;
                if (health <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
            
        }
    }

    public void bunkerRest()
    {
        health = 10;
        gameObject.SetActive(true);
    }
}
