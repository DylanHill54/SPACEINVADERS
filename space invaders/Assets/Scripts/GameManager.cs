using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI currentScore;

    public TextMeshProUGUI hiScore;
    

    private int currentscore=0;

    private int hiscore=0;

    private Player player;

    private Invaders invaders;

    public Bunker bunker1;
    public Bunker bunker2;
    public Bunker bunker3;
    public Bunker bunker4;
    // Start is called before the first frame update
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        invaders = FindObjectOfType<Invaders>();
    }

    void Start()
    {
        invaders.add10 += add10;
        invaders.add20 += add20;
        invaders.add30 += add30;
        player.killed += gamereset;
    }

    private void gamereset()
    {
        if (currentscore > hiscore)
        {
            hiscore = currentscore;
        }
        hiScore.SetText(hiscore.ToString().PadLeft(4,'0'));
        currentscore = 0;
        currentScore.SetText(currentscore.ToString().PadLeft(4,'0'));
        bunker1.bunkerRest();
        bunker2.bunkerRest();
        bunker3.bunkerRest();
        bunker4.bunkerRest();
        invaders.InvaderReset();
        Respawn();
        
    }

    private void add30()
    {
        currentscore += 30;
        currentScore.SetText(currentscore.ToString().PadLeft(4,'0'));
    }

    private void add20()
    {
        currentscore += 20;
        currentScore.SetText(currentscore.ToString().PadLeft(4,'0'));
    }

    private void add10()
    {
        currentscore += 10;
        currentScore.SetText(currentscore.ToString().PadLeft(4,'0'));
    }
    private void Respawn()
    {
        Vector3 position = player.transform.position;
        position.x = 0f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
