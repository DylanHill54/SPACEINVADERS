using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;


public class Invaders : MonoBehaviour
{
    // Start is called before the first frame update
    public Invader[] prefabs;
    public int rows = 5;
    public int columns = 11;
    public AnimationCurve speed;
    private Vector3 direction = Vector2.right;
    public Action add10;
    public Action add20;
    public Action add30;
    public float attackRate=1.0f;
    public Projectiles missile;
    public Vector3 initalposition { get; private set; }
    public int amountKilled { get; private set; }
    public int amountAlive => total - amountKilled;
    public int total => rows * columns;
    public float percentKilled => (float) amountKilled /  total;
    private void Awake()
    {
        initalposition = transform.position;
        for (int row = 0; row < rows; row++)
        {
            float width = 2.0f * (columns - 1);
            float height= 2.0f * (rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition=new Vector3(centering.x,centering.y+(row*2.0f),0.0f);
            for (int col = 0; col < columns; col++)
            {
                Invader invader = Instantiate(prefabs[row], transform);
                invader.is10Points += points10;
                invader.is20Points += points20;
                invader.is30Points += points30;
                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                invader.transform.localPosition = position;
            }
        }
    }
    

    public void points30()
    {
        amountKilled++;
        add30.Invoke();
        if (amountKilled >= total)
        {
            InvaderReset();
        }
    }

    public void InvaderReset()
    {
        amountKilled = 0;
        direction = Vector3.right;
        transform.position = initalposition;
        foreach (Transform invader in transform)
        {
            invader.gameObject.SetActive(true);
        }
        
    }

    public void points20()
    {
        amountKilled++;
        add20.Invoke();
        if (amountKilled >= total)
        {
            InvaderReset();
        }
    }

    public void points10()
    {
        amountKilled++;
        add10.Invoke();
        if (amountKilled >= total)
        {
            InvaderReset();
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(missileAttack),attackRate,attackRate);
    }

    private void missileAttack()
    {
        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (Random.value < (1.0f / (float) amountAlive))
            {
                Instantiate(missile, invader.position, quaternion.identity);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += direction * speed.Evaluate(percentKilled) * Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f))
            {
                AdvanceRow();
            } 
            else if (direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }

        }

    }

    private void AdvanceRow()
    {
        direction = new Vector3(-direction.x, 0, 0);
        Vector3 position = transform.position;
        position.y -= 1.0f;
        transform.position = position;
    }
}
