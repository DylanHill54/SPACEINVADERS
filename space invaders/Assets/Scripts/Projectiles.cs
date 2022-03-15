using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public Vector3 direction;

    public float speed;
    public Action destroyed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        destroyed?.Invoke();

        Destroy(gameObject);
    }
}
