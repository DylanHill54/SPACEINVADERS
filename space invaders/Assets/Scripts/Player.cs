using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Projectiles laser;
    public float speed = 5.0f;
    public Action killed;
    private bool laserACtive;
    private Animator playerAnimator;

    private static readonly int Dead = Animator.StringToHash("dead");

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!laserACtive)
        {
            Projectiles projectile = Instantiate(laser, transform.position, quaternion.identity);
            projectile.destroyed += laserDestroyed;
            laserACtive = true;
        }
       
    }

    private void laserDestroyed()
    {
        laserACtive = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("missile") ||
            col.gameObject.layer == LayerMask.NameToLayer("invader"))
        {
            if (killed != null)
            {
                playerAnimator.SetTrigger(Dead);
                //killed.Invoke();
                Invoke("killPlayer",2.0f);
            }
        }
    }

    void killPlayer()
    {
        killed.Invoke();
    }
}
