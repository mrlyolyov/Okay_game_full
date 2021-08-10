using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public GameObject tilePrefab;
    public Sprite[] Tiles;
    public List<Sprite> spTiles;
    GameObject pHand;
    public int[] tileIndex;
    public int start;
    public int end;
    GameObject pTile;
    GameObject spawnSpot;
    bool isDeal;
    GameObject remT;
    GameObject joker;
    public GameObject deck;
    public GameObject remTiles;
    public GameObject remaning;
    public int indexJoker;
    public static string jokerSearch;
    // Start is called before the first frame update
    void Start()
    {
    reshuffleTiles();
    }

    /*
     * 
     * Attached to button 'Show/Hide'.
     * Invoked to change Tiles face-up/face-down in Hand.
     */
    public void showHideBot()
    {
        pHand = GameObject.Find("P1");
        Transform g =pHand.transform.GetChild(0);

        for (int i = 0; i < g.childCount; i++)
        {
            if (g.transform.GetChild(i).GetComponent<Image>().sprite != g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite)
            {
                g.transform.GetChild(i).GetComponent<Image>().sprite = g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
                pHand.transform.GetChild(0).GetComponent<HandZone>().isShow = true;
            }
            else 
            { 
                g.transform.GetChild(i).GetComponent<Image>().sprite = Tiles[106];
                pHand.transform.GetChild(0).GetComponent<HandZone>().isShow = false;
            }
        }
        g = pHand.transform.GetChild(1);
        for (int i = 0; i < g.childCount; i++)
        {
            if (g.transform.GetChild(i).GetComponent<Image>().sprite != g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite)
            {
                g.transform.GetChild(i).GetComponent<Image>().sprite = g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
                pHand.transform.GetChild(1).GetComponent<HandZone>().isShow = true;
            }
            else
            {
                g.transform.GetChild(i).GetComponent<Image>().sprite = Tiles[106];
                pHand.transform.GetChild(1).GetComponent<HandZone>().isShow = false;
            }
            }
    }

    /*
     * 
     * Attached to button 'Show/Hide'.
     * Invoked to change Tiles face-up/face-down in Hand.
     */
    public void showHideRight()
    {
        pHand = GameObject.Find("P2");
        Transform g = pHand.transform.GetChild(0);

        for (int i = 0; i < g.childCount; i++)
        {
            if (g.transform.GetChild(i).GetComponent<Image>().sprite != g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite)
            {
                g.transform.GetChild(i).GetComponent<Image>().sprite = g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
                pHand.transform.GetChild(0).GetComponent<HandZone>().isShow = true;
            }
            else { 
                g.transform.GetChild(i).GetComponent<Image>().sprite = Tiles[106];
                pHand.transform.GetChild(0).GetComponent<HandZone>().isShow = false;
            }
        }
        g = pHand.transform.GetChild(1);
        for (int i = 0; i < g.childCount; i++)
        {
            if (g.transform.GetChild(i).GetComponent<Image>().sprite != g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite)
            {
                g.transform.GetChild(i).GetComponent<Image>().sprite = g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
                pHand.transform.GetChild(1).GetComponent<HandZone>().isShow = true;
            }
            else
            {
                g.transform.GetChild(i).GetComponent<Image>().sprite = Tiles[106];
                pHand.transform.GetChild(1).GetComponent<HandZone>().isShow = false;
            }
            }
    }

    /*
     * 
     * Attached to button 'Show/Hide'.
     * Invoked to change Tiles face-up/face-down in Hand.
     */
    public void showHideTop()
    {
        pHand = GameObject.Find("P3");
        Transform g = pHand.transform.GetChild(0);

        for (int i = 0; i < g.childCount; i++)
        {
            if (g.transform.GetChild(i).GetComponent<Image>().sprite != g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite)
            {
                g.transform.GetChild(i).GetComponent<Image>().sprite = g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
                pHand.transform.GetChild(0).GetComponent<HandZone>().isShow = true;
            }
            else
            {
                g.transform.GetChild(i).GetComponent<Image>().sprite = Tiles[106];
                pHand.transform.GetChild(0).GetComponent<HandZone>().isShow = false;
            }
            }
        g = pHand.transform.GetChild(1);
        for (int i = 0; i < g.childCount; i++)
        {
            if (g.transform.GetChild(i).GetComponent<Image>().sprite != g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite)
            {
                g.transform.GetChild(i).GetComponent<Image>().sprite = g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
                pHand.transform.GetChild(1).GetComponent<HandZone>().isShow = true;
            }
            else
            {
                g.transform.GetChild(i).GetComponent<Image>().sprite = Tiles[106];
                pHand.transform.GetChild(1).GetComponent<HandZone>().isShow = false;
            }
        }
    }

    /*
     * 
     * Attached to button 'Show/Hide'.
     * Invoked to change Tiles face-up/face-down in Hand.
     */
    public void showHideLeft()
    {
        pHand = GameObject.Find("P4");
        Transform g = pHand.transform.GetChild(0);

        for (int i = 0; i < g.childCount; i++)
        {
            if (g.transform.GetChild(i).GetComponent<Image>().sprite != g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite)
            {
                g.transform.GetChild(i).GetComponent<Image>().sprite = g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
                pHand.transform.GetChild(0).GetComponent<HandZone>().isShow = true;
            }
            else
            {
                g.transform.GetChild(i).GetComponent<Image>().sprite = Tiles[106];
                pHand.transform.GetChild(0).GetComponent<HandZone>().isShow = false;
            }
        }
        g = pHand.transform.GetChild(1);
        for (int i = 0; i < g.childCount; i++)
        {
            if (g.transform.GetChild(i).GetComponent<Image>().sprite != g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite)
            {
                g.transform.GetChild(i).GetComponent<Image>().sprite = g.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
                pHand.transform.GetChild(1).GetComponent<HandZone>().isShow = true;
            }
            else
            {
                g.transform.GetChild(i).GetComponent<Image>().sprite = Tiles[106];
                pHand.transform.GetChild(1).GetComponent<HandZone>().isShow = false;
            }
        }
    }
    
    /*
     * 
     * Fills a 'List<Sprites>' from the Sprites[] that are shuffled.
     */
    public void fill()
    {
        for(int i = 0; i < Tiles.Length; i++)
        {
            spTiles.Add(Tiles[i]);
        }
    }

    /*
     * 
     * Creates the deck.
     * Instantiate tilePrefab with fixed possition in centre of game scene.
     */
    public void deckCreate()
    {
        fill();
        spawnTiles();

        Sprite [] rem = spTiles.ToArray();
        deck = GameObject.Find("DeckOn");
        for (int i = 0; i < rem.Length - 1; i++)
        {
            Sprite tileSprite = rem[i];
            string tileName = tileSprite.name;
            remTiles = Instantiate(tilePrefab, transform.position, transform.rotation);
            remTiles.transform.SetParent(deck.transform);
            remTiles.name = tileName;
            remTiles.GetComponent<SpriteRenderer>().sprite = tileSprite;
            remTiles.GetComponent<Image>().sprite = Tiles[106];
            remTiles.GetComponent<Tile>().name = tileName;
            remTiles.transform.position = new Vector3(890, 540, 0);


            if (remTiles.GetComponent<Drag>() != null)
            {
                Destroy(remTiles.GetComponent<Drag>());
            }
        }

        isDeal = true;
        getJoker();
        jokerFind();
        joker = GameObject.Find("JokerShow");    
        joker.GetComponent<Image>().sprite = Tiles[indexJoker];
    }

    /*
     * 
     * All active players get 14 tiles and the 1st player gets an extra Tile.
     */
    public void spawnTiles()
    {
        for (int i = 0; i < OkayGame.inGame.Length; i++)
        {
            if (OkayGame.inGame[i])
            {
                setUpTiles(i);
            }
            if (i == Dice.playerIndex)
            {
                dealerTile(Player.currentID);
            }
        }
        GameObject g = GameObject.Find("Player");
        g.GetComponent<Player>().setTimer(Player.currentID);
        
    }

    /*
     * 
     * Invoked when dealer is found.
     * @param 'index' is referencing to the players that are in game and deals Tiles to their hand.  
     */
    public void setUpTiles(int index)
    {

        switch (index)
        {
            case 3:
                leftTiles();
                break;
            case 2:
                topTiles();
                break;
            case 1:
                rightTiles();
                break;
            case 0:
                bottomTiles();
                break;
        }
    }

    /*
     * 
     * Identify 1st player turn based on thrown dice.
     * Gives this player the extra 15th Tile in hand.
     */
    public void dealerTile(int first)
    {
        pHand = GameObject.Find("P"+ first);
        spawnSpot = pHand.transform.GetChild(0).gameObject;
        Sprite tileSprite = Tiles[14];              
        string tileName = tileSprite.name;
        pTile = Instantiate(tilePrefab, transform.position, transform.rotation);
        pTile.name = tileName;
        pTile.transform.SetParent(spawnSpot.transform);
        pTile.GetComponent<Tile>().name = tileName;                            //PlayerOneTop
        pTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
      //  pTile.GetComponent<Image>().sprite = tileSprite;
        switch (first-1)
        {
            case 1:
                pTile.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 90));
                break;
            case 2:
                pTile.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 180));
                break;
            case 3:
                pTile.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, -90));
                break;
            default:
                pTile.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 0));
                break;
        }
        pTile.transform.position = new Vector3(0, 0, 0);
       // sp.RemoveAt(14);
    }

    /*
     * 
     * Instantiates Tile prefab.
     * Gives tiles to 'Bottom' possition player with fixed rotation and parant set.
     */
    public void bottomTiles()
    {
        pHand = GameObject.Find("P1");
        spawnSpot = pHand.transform.GetChild(0).gameObject;

        for (int i = 0; i < 7; i++)
        {
            Sprite tileSprite = spTiles[i];
            string tileName = tileSprite.name;
            pTile = Instantiate(tilePrefab, transform.position, transform.rotation);
            pTile.name = tileName;
            pTile.transform.SetParent(spawnSpot.transform);
            pTile.GetComponent<Tile>().name = tileName;                             //PlayerOneBot
            pTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
            pTile.transform.position = new Vector3(0, 0, 0);
            spTiles.RemoveAt(i);
        }
            spawnSpot = pHand.transform.GetChild(1).gameObject;
        for (int i = 0; i < 7; i++)
        {
            Sprite tileSprite = spTiles[i];
            string tileName = tileSprite.name;
            pTile = Instantiate(tilePrefab, transform.position, transform.rotation);
            pTile.name = tileName;
            pTile.transform.SetParent(spawnSpot.transform);
            pTile.GetComponent<Tile>().name = tileName;                            //PlayerOneTop
            pTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
            pTile.transform.position = new Vector3(0, 0, 0);
            spTiles.RemoveAt(i);
        }
    }

    /*
     * 
     * Instantiates Tile prefab.
     * Gives tiles to 'Right' possition player with fixed rotation and parant set.
     */
    public void rightTiles()
    {
        pHand = GameObject.Find("P2");
        spawnSpot = pHand.transform.GetChild(0).gameObject;
        for (int i = 0; i < 7; i++)
        {
            Sprite tileSprite = spTiles[i];
            string tileName = tileSprite.name;
            pTile = Instantiate(tilePrefab, transform.position, transform.rotation);
            pTile.name = tileName;
            pTile.transform.SetParent(spawnSpot.transform);
            pTile.GetComponent<Tile>().name = tileName;                         //PlayerTwoBot
            pTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
            pTile.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 90));
            pTile.transform.position = new Vector3(0, 0, 0);
            spTiles.RemoveAt(i);
        }

        spawnSpot = pHand.transform.GetChild(1).gameObject;
        for (int i = 0; i < 7; i++)
        {
            Sprite tileSprite = spTiles[i];
            string tileName = tileSprite.name;
            pTile = Instantiate(tilePrefab, transform.position, transform.rotation);
            pTile.name = tileName;
            pTile.transform.SetParent(spawnSpot.transform);
            pTile.GetComponent<Tile>().name = tileName;                         //PlayerTwoTop
            pTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
            pTile.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 90));
            pTile.transform.position = new Vector3(0, 0, 0);
            spTiles.RemoveAt(i);
        }
    }

    /*
     * 
     * Instantiates Tile prefab.
     * Gives tiles to 'Top' possition player with fixed rotation and parant set.
     */
    public void topTiles()
    {
        
        pHand = GameObject.Find("P3");
        spawnSpot = pHand.transform.GetChild(0).gameObject;
        for (int i = 0; i < 7; i++)
        {
            Sprite tileSprite = spTiles[i];
            string tileName = tileSprite.name;
            pTile = Instantiate(tilePrefab, transform.position, transform.rotation);
            pTile.name = tileName;
            pTile.transform.SetParent(spawnSpot.transform);
            pTile.GetComponent<Tile>().name = tileName;                         //PlayerThreeBot
            pTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
            pTile.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 180));
            pTile.transform.position = new Vector3(0, 0, 0);
            spTiles.RemoveAt(i);
        }

        spawnSpot = pHand.transform.GetChild(1).gameObject;
        for (int i = 0; i < 7; i++)
        {
            Sprite tileSprite = spTiles[i];
            string tileName = tileSprite.name;
            pTile = Instantiate(tilePrefab, transform.position, transform.rotation);
            pTile.name = tileName;
            pTile.transform.SetParent(spawnSpot.transform);
            pTile.GetComponent<Tile>().name = tileName;                         //PlayerThreeTop
            pTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
            pTile.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 180));
            pTile.transform.position = new Vector3(0, 0, 0);
            spTiles.RemoveAt(i);
        }
    }

    /*
     * 
     * Instantiates Tile prefab.
     * Gives tiles to 'Left' possition player with fixed rotation and parant set.
     */
    public void leftTiles()
    {
        pHand = GameObject.Find("P4");
        spawnSpot = pHand.transform.GetChild(0).gameObject;
        for (int i = 0; i < 7; i++)
        {
            Sprite tileSprite = spTiles[i];
            string tileName = tileSprite.name;
            pTile = Instantiate(tilePrefab, transform.position, transform.rotation);
            pTile.name = tileName;
            pTile.transform.SetParent(spawnSpot.transform);
            pTile.GetComponent<Tile>().name = tileName;                         //PlayerFourTop
            pTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
            pTile.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, -90));
            pTile.transform.position = new Vector3(0, 0, 0);
            spTiles.RemoveAt(i);
        }

        spawnSpot = pHand.transform.GetChild(1).gameObject;
        for (int i = 0; i < 7; i++)
        {
            Sprite tileSprite = spTiles[i];
            string tileName = tileSprite.name;
            pTile = Instantiate(tilePrefab, transform.position, transform.rotation);
            pTile.name = tileName;
            pTile.transform.SetParent(spawnSpot.transform);
            pTile.GetComponent<Tile>().name = tileName;                         //PlayerFourBot
            pTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
            pTile.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, -90));
            pTile.transform.position = new Vector3(0, 0, 0);
            spTiles.RemoveAt(i);
        }
    }

    /*
     * 
     * Finds the name of Joker.
     */
    public void jokerFind()
    {
        jokerSearch = Tiles[indexJoker].name;
        Okay okay = new Okay();
       int offset = okay.spaceSearch(jokerSearch);
        if (jokerSearch.Substring(offset, jokerSearch.Length - offset).Equals(" Joker"))
        {
            indexJoker++;
        }
    }

    /*
     * 
     * Calculates Joker Index.
     */
    public void getJoker()
    {
        if (Dice.playerIndex == 0)
        {
            indexJoker = Dice.p1Num * 5;
        }
       else if (Dice.playerIndex == 1)
        {
            indexJoker = Dice.p2Num * 5;
        }
        else if (Dice.playerIndex == 2)
        {
            indexJoker = Dice.p3Num * 5;
        }
        else if (Dice.playerIndex == 3)
        {
            indexJoker = Dice.p4Num * 5;
        }
    }

    /*
     * 
     * Shuffle Tiles.
     */
    public void reshuffleTiles()
    {
        // Shuffle algorithm ::  
        for (int t = 0; t < Tiles.Length-1; t++)
        {
            Sprite tmp = Tiles[t];
            int r = Random.Range(t, Tiles.Length-1);
            Tiles[t] = Tiles[r];
            Tiles[r] = tmp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeal) 
        { 
        displayRemaining();
        }
    }
    /*
     * 
     * Displays the number of remaining tiles in deck.
     */
    public void displayRemaining()
    {
        remT = GameObject.Find("RemT");
        remT.GetComponent<Text>().text = deck.transform.childCount.ToString();
        remT.transform.SetParent(deck.transform.parent);
        remT.transform.SetAsLastSibling();
        if (deck.transform.childCount == 0)
        {
            RecetGame.setValues();
            SceneManager.LoadScene("Start");
        }
    }
}
