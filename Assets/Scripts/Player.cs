using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject playerPrefabProperty;
    public GameObject Hand;
    public GameObject discardF;
    public GameObject show;
    public GameObject timer;
    public Deck deckTiles;
    private GameObject pState;
    public bool findWinner;
    GameObject player= null;
    public static int currentID;
    public bool draw;
    public bool discard;
    public Dice dice;
    public Sprite IconFace;
    public Sprite IconBack;
    public static bool thrown;
    public static bool changeTurn=true;
    public static bool onStart;
    GameObject p;


    /*
     * Checks for the active players and invokes their object spawn.
     */
    public void spawn()
    {
        for (int i = 0; i < OkayGame.inGame.Length; i++)
        {
            if (OkayGame.inGame[i])
            {
                setUp(i);
            }
        }
    }


    /*
     * 
     * Sets up the player prefabs required for game start.
     */
    public void setUp(int index)
    {
        switch (index)
            {
            case 0:
                bottomHand();
                createP(1, player);
                bottomDiscard();
                generateBotButtons(); 
                dice.botDice();
                break;
            case 1:
                rightHand();
                createP(2, player);
                rightDiscard();
                generateRightButtons();
                dice.rightDice();
                break;
            case 2:
                topHand();
                createP(3, player);
                topDiscard();
                generateTopButtons();
                dice.topDice();
                break;
            case 3:
                leftHand();
                createP(4, player);
                leftDiscard();
                generateLeftButtons();
                dice.leftDice();
                break;
        }
    }

    /*
     * 
     * Instantiates the timebar prefab attached to @param 'timer'.
     * @param indx is used to reference the required player possition 'timebar'.
     * It's used when ever a player's turn starts.
     */
    public void setTimer(int indx)
    {
        GameObject gameObject = GameObject.Find("P" + indx);
        switch(indx){
 
            case 1:
                player = Instantiate(timer, new Vector3(950, 232, 0), transform.rotation);
                player.name = "TimeBar"; 
                player.transform.SetParent(gameObject.transform);
                break;
            case 2:
                player = Instantiate(timer, new Vector3(1687.9f, 546, 90), Quaternion.Euler(new Vector3(0, 0, 90)));
                player.name = "TimeBar";
                player.transform.SetParent(gameObject.transform);
                break;
            case 3:
                player = Instantiate(timer, new Vector3(953, 849, 0), Quaternion.Euler(new Vector3(0, 0, 180)));
                player.name = "TimeBar";
                player.transform.SetParent(gameObject.transform);
                break;
            case 4:
                player = Instantiate(timer, new Vector3(241.1f, 547.9f, 0), Quaternion.Euler(new Vector3(0, 0, -90)));
                player.name = "TimeBar";
                player.transform.SetParent(gameObject.transform);
                break;
        }
    }

    /*
     * 
     * Instantiates 'Show/Hide', 'Win' and 'Exit' for the player on 
     * 'Bot' possition button with fixed object possitions on scene.
     * Used when players is validated for game start.
     */
    public void generateBotButtons()
    {
        player = Instantiate(show, new Vector3(1350, 45, 0), transform.rotation);
        player.name = "Show/Hide";
        player.transform.SetParent(p.transform);
        player.GetComponent<Button>().onClick.AddListener(deckTiles.showHideBot);

        player = Instantiate(show, new Vector3(550, 45, 0), transform.rotation);
        player.name = "Win";
        player.transform.SetParent(p.transform);
        player.transform.GetChild(0).GetComponent<Text>().text = "Win";

        player = Instantiate(show, new Vector3(950, 278, 0), transform.rotation);
        player.name = "Exit";
        player.transform.SetParent(p.transform);
        player.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(253, 85f);
        Destroy(player.transform.GetChild(0).GetComponent<Text>().gameObject);
        player.transform.GetComponent<Image>().color = new Color(0, 0, 0,0);
     //   player.GetComponent<Button>().onClick.AddListener(exitTableBot);
    }

    /*
     * 
     * Instantiates 'Show/Hide', 'Win' and 'Exit' for the player on 
     * 'Right' possition button with fixed object possitions on scene.
     * Used when players is validated for game start.
     */
    public void generateRightButtons()
    {
        player = Instantiate(show, new Vector3(1870, 951, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
        player.name = "Show/Hide";
        player.transform.SetParent(p.transform);
        player.GetComponent<Button>().onClick.AddListener(deckTiles.showHideRight);

        player = Instantiate(show, new Vector3(1870, 150, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
        player.name = "Win";
        player.transform.SetParent(p.transform);
        player.transform.GetChild(0).GetComponent<Text>().text = "Win";

        player = Instantiate(show, new Vector3(1640, 600, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
        player.name = "Exit";
        player.transform.SetParent(p.transform);
        player.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(253, 85f);
        Destroy(player.transform.GetChild(0).GetComponent<Text>().gameObject);
        player.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
       // player.GetComponent<Button>().onClick.AddListener(exitTableRight);

    }

    /*
     * 
     * Instantiates 'Show/Hide', 'Win' and 'Exit' for the player on 
     * 'Top' possition button with fixed object possitions on scene.
     * Used when players is validated for game start.
     */
    public void generateTopButtons()
    {
        player = Instantiate(show, new Vector3(548, 1041, 0), Quaternion.Euler(new Vector3(0, 0, 180)));
        player.name = "Show/Hide";
        player.transform.SetParent(p.transform);
        player.GetComponent<Button>().onClick.AddListener(deckTiles.showHideTop);

        player = Instantiate(show, new Vector3(1350, 1041, 0), Quaternion.Euler(new Vector3(0, 0, 180)));
        player.name = "Win";
        player.transform.SetParent(p.transform);
        player.transform.GetChild(0).GetComponent<Text>().text = "Win";

        player = Instantiate(show, new Vector3(950, 800, 0), Quaternion.Euler(new Vector3(0, 0, 180)));
        player.name = "Exit";
        player.transform.SetParent(p.transform);
        player.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(253, 85f);
        Destroy(player.transform.GetChild(0).GetComponent<Text>().gameObject);
        player.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
       // player.GetComponent<Button>().onClick.AddListener(exitTableTop);
    }

    /*
     * 
     * Instantiates 'Show/Hide', 'Win' and 'Exit' for the player on 
     * 'Left' possition button with fixed object possitions on scene.
     * Used when players is validated for game start.
     */
    public void generateLeftButtons()
    {
        player = Instantiate(show, new Vector3(43.5f, 147.5f, 0), Quaternion.Euler(new Vector3(0, 0, -90)));
        player.name = "Show/Hide";
        player.transform.SetParent(p.transform);
        player.GetComponent<Button>().onClick.AddListener(deckTiles.showHideLeft);

        player = Instantiate(show, new Vector3(43.5f, 950, 0), Quaternion.Euler(new Vector3(0, 0, -90)));
        player.name = "Win";
        player.transform.SetParent(p.transform);
        player.transform.GetChild(0).GetComponent<Text>().text = "Win";

        player = Instantiate(show, new Vector3(290, 600, 0), Quaternion.Euler(new Vector3(0, 0, -90)));
        player.name = "Exit";
        player.transform.SetParent(p.transform);
        player.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(253, 85f);
        Destroy(player.transform.GetChild(0).GetComponent<Text>().gameObject);
        player.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
     //   player.GetComponent<Button>().onClick.AddListener(exitTableLeft);
    }

    /*
     * 
     * Instantiate 'Bottom' discard prefab.
     * Invoked on start game generate it's 'Discard' spot.
     */
    public void bottomDiscard()
    {
        player = Instantiate(discardF, new Vector3(1230, 325, 0), transform.rotation);
        player.name = "Discard";
        player.transform.SetParent(p.transform);
    }

    /*
     * 
     * Instantiate 'Right' discard prefab.
     * Invoked on start game generate it's 'Discard' spot.
     */
    public void rightDiscard()
    {
        player = Instantiate(discardF, new Vector3(1560, 820, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
        player.name = "Discard";
        player.transform.SetParent(p.transform);
    }

    /*
     * 
     * Instantiate 'Top' discard prefab.
     * Invoked on start game generate it's 'Discard' spot.
     */
    public void topDiscard()
    {
        player = Instantiate(discardF, new Vector3(680, 755, 0), Quaternion.Euler(new Vector3(0, 0, 180)));
        player.name = "Discard";
        player.transform.SetParent(p.transform);
    }

    /*
     * 
     * Instantiate 'Left' discard prefab.
     * Invoked on start game generate it's 'Discard' spot.
     */
    public void leftDiscard()
    {
        player = Instantiate(discardF, new Vector3(325, 277, 0), Quaternion.Euler(new Vector3(0, 0, -90)));
        player.name = "Discard";
        player.transform.SetParent(p.transform);
    }


    /*
     * 
     * Generates 'Bottom' hand boards 'Top' and 'Bot prefab'
     * Invoked on game start.
     */
    public void bottomHand()
    {
        p = new GameObject("P1");
        p.transform.SetParent(GameObject.Find("GameBoard").transform);
        player = Instantiate(Hand, new Vector3(950, 172, 0), transform.rotation);
        player.name = "Top";
        player.transform.SetParent(p.transform);

        p.transform.SetParent(GameObject.Find("GameBoard").transform);
        player = Instantiate(Hand, new Vector3(950, 69, 0), transform.rotation);
        player.name = "Bot";
        player.transform.SetParent(p.transform);
    }

    /*
     * 
     * Generates 'Right' hand boards 'Top' and 'Bot prefab'
     * Invoked on game start.
     */
    public void rightHand()
    {
        p = new GameObject("P2");
        p.transform.SetParent(GameObject.Find("GameBoard").transform);
        player = Instantiate(Hand, new Vector3(1850, 550, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
        player.name = "Top";
        player.transform.SetParent(p.transform);

        p.transform.SetParent(GameObject.Find("GameBoard").transform);
        player = Instantiate(Hand, new Vector3(1747, 550, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
        player.name = "Bot";
        player.transform.SetParent(p.transform);
    }

    /*
     * 
     * Generates 'Top' hand boards 'Top' and 'Bot prefab'
     * Invoked on game start.
     */
    public void topHand()
    {
        p = new GameObject("P3");
        p.transform.SetParent(GameObject.Find("GameBoard").transform);
        player = Instantiate(Hand, new Vector3(950, 910, 0), Quaternion.Euler(new Vector3(0, 0, 180)));
        player.name = "Top";
        player.transform.SetParent(p.transform);

        p.transform.SetParent(GameObject.Find("GameBoard").transform);
        player = Instantiate(Hand, new Vector3(950, 1013, 0), Quaternion.Euler(new Vector3(0, 0, 180)));
        player.name = "Bot";
        player.transform.SetParent(p.transform);
    }


    /*
     * Generates 'Left' hand boards 'Top' and 'Bot prefab'
     * Invoked on game start.
     */
    public void leftHand()
    {
        p = new GameObject("P4");
        p.transform.SetParent(GameObject.Find("GameBoard").transform);
        player = Instantiate(Hand, new Vector3(75, 550, 0), Quaternion.Euler(new Vector3(0, 0, -90)));
        player.name = "Top";
        player.transform.SetParent(p.transform);

        p.transform.SetParent(GameObject.Find("GameBoard").transform);
        player = Instantiate(Hand, new Vector3(180, 550, 0), Quaternion.Euler(new Vector3(0, 0, -90)));
        player.name = "Bot";
        player.transform.SetParent(p.transform);
    }
    /* Update is called once per frame
     * 
     */
    void Update()
    {
        if (!findWinner && Dice.isDealerFound)
        {
            if (!onStart)
            {
                playerSetUp();
            }
        }
    }


    /*
     * 
     * Assigns the first player.
     * Invoked when dice roll is over.
     */
    public void playerSetUp()
    {
        onStart = true;
        int c=0;
        for (int i = 0; i < 4; i++)
        {  
            if (i > Dice.playerIndex && OkayGame.inGame[i])
            {
                c++;
            }
        }
        if (c == 0)
        {
            for(int i = 0; i < OkayGame.inGame.Length; i++)
            {
                if( i != Dice.playerIndex && OkayGame.inGame[i])
                {
                    currentID = i + 1;
                    break;
                }
            }
        }else if (c != 0)
        {
            for(int i = Dice.playerIndex; i < OkayGame.inGame.Length; i++)
            {
                if(i!=Dice.playerIndex && OkayGame.inGame[i])
                {
                    currentID = i + 1;
                    break;
                }
            }
        }
    }

    /*
     * 
     * Removes 'Bottom' player from game.
     * Attched to 'Exit' button.
     */
    public void exitBottom()
    {
        int k = 0;
        if (OkayGame.hasMinimumPlayers()) { 
        if (k == currentID - 1)
        {
            DiscardZone d = GameObject.Find("P1").transform.GetChild(discardIndex()).GetComponent<DiscardZone>();
            DiscardZone.EnableDeckDraw();
            d.nextP();
            d.deckRotate();
            Destroy(GameObject.Find("P1"));
            OkayGame.inGame[0] = false;
            GameObject pl = GameObject.Find("Player");
            pl.GetComponent<Player>().setTimer(currentID);
            OkayGame.hasMinimumPlayers();
            }
        else if (k != currentID - 1)
        {
            Destroy(GameObject.Find("P1"));
            OkayGame.inGame[0] = false;
            OkayGame.hasMinimumPlayers();
        }
        }
    }


    /*
     * 
     * Finds the index for discard hierarchy.
     * Invoked when exit button is clicked.
     */
    public int discardIndex()
    {
        GameObject g = GameObject.Find("P" + currentID);
        int a = 0;
        for (int i = 0; i < g.transform.childCount;i++)
        {
            if (g.transform.GetChild(i).name == "Discard")
            {
                a = i;
            }
        }
        return a;
    }

    /*
     * 
     * Removes 'Right' player from the game.
     * Attched to 'Exit' button.
     */
    public void exitRight()
    {
        int k = 1;
        if (OkayGame.hasMinimumPlayers())
        {
            if (k == currentID - 1)
            {
                DiscardZone d = GameObject.Find("P2").transform.GetChild(discardIndex()).GetComponent<DiscardZone>();
                DiscardZone.EnableDeckDraw();
                d.nextP();
                d.deckRotate();
                Destroy(GameObject.Find("P2"));
                OkayGame.inGame[1] = false;
                GameObject pl = GameObject.Find("Player");
                pl.GetComponent<Player>().setTimer(currentID);
                OkayGame.hasMinimumPlayers();
            }
            else if (k != currentID - 1)
            {
                Destroy(GameObject.Find("P2"));
                OkayGame.inGame[1] = false;
                OkayGame.hasMinimumPlayers();
            }
        }
    }

    /*
     * 
     * Removes 'Top' player from  the game.
     * Attched to 'Exit' button.
     */
    public void exitTop()
    {
        int k = 2;
        if (OkayGame.hasMinimumPlayers())
        {
            if (k == currentID - 1)
            {
                DiscardZone d = GameObject.Find("P3").transform.GetChild(discardIndex()).GetComponent<DiscardZone>();
                DiscardZone.EnableDeckDraw();
                d.nextP();
                d.deckRotate();
                Destroy(GameObject.Find("P3"));
                OkayGame.inGame[2] = false;
                GameObject pl = GameObject.Find("Player");
                pl.GetComponent<Player>().setTimer(currentID);
                OkayGame.hasMinimumPlayers();
            }
            else if (k != currentID - 1)
            {
                Destroy(GameObject.Find("P3"));
                OkayGame.inGame[2] = false;
                OkayGame.hasMinimumPlayers();
            }
        }
    }

    /*
     * 
     * Removes 'Left' player from the game.
     * Attched to 'Exit' button.
     */
    public void exitLeft()
    {
        int k = 3;
        if (OkayGame.hasMinimumPlayers())
        {
            if (k == currentID - 1)
            {
                DiscardZone d = GameObject.Find("P4").transform.GetChild(discardIndex()).GetComponent<DiscardZone>();
                DiscardZone.EnableDeckDraw();
                d.nextP();
                d.deckRotate();
                Destroy(GameObject.Find("P4"));
                OkayGame.inGame[3] = false;
                GameObject pl = GameObject.Find("Player");
                pl.GetComponent<Player>().setTimer(currentID);
                OkayGame.hasMinimumPlayers();
            }
            else if (k != currentID - 1)
            {
                Destroy(GameObject.Find("P4"));
                OkayGame.inGame[3] = false;
                OkayGame.hasMinimumPlayers();
            }
        }
    }

    /*
     * 
     * Instantiate player prefab referencing the @param num.
     */
    public void createP(int num,GameObject player)
    {
        pState = GameObject.Find("P"+num);
        if (num == 1)
        {
            player = Instantiate(playerPrefabProperty, new Vector3(950, 278, 0), transform.rotation);
        }
        else if (num == 2)
        {
            player = Instantiate(playerPrefabProperty, new Vector3(1640, 600, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
        }
        else if (num == 3)
        {
            player = Instantiate(playerPrefabProperty, new Vector3(950, 800, 0), Quaternion.Euler(new Vector3(0, 0, 180)));
        }
        else if (num == 4)
        {
            player = Instantiate(playerPrefabProperty, new Vector3(290, 600, 0), Quaternion.Euler(new Vector3(0, 0, -90)));
        }

        player.GetComponentInChildren<Text>().text = "P" + num;
        player.GetComponent<Image>().sprite = IconFace;
        player.GetComponentInChildren<Image>().sprite = IconBack;
        player.transform.SetParent(pState.transform);
        player.name = "Status";
    }
}
