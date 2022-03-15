using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public bool is10points;
    public Action is10Points;
    public bool is20points;
    public Action is20Points;
    public bool is30points;
    public Action is30Points;
    private Animator invaderAnimator;
    private static readonly int Explode = Animator.StringToHash("explode");

    private void Start()
    {
        invaderAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("laser"))
        {
            invaderAnimator.SetTrigger(Explode);
            //Thread.Sleep(5000);
            //Task.Delay(5000);
            Invoke("setFalse",1.0f);
            //gameObject.SetActive(false);

            if (is10points)
            {
                is10Points.Invoke();
            }

            if (is20points)
            {
                is20Points.Invoke();
            }

            if (is30points)
            {
                is30Points.Invoke();
            }

            
        }
    }

    void setFalse()
    {
        gameObject.SetActive(false);
        
    }
}
