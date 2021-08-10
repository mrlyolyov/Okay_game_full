using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    Image timebar;
    public float maxTime = 5f;
    float timeLeft;
    public static int sec;
    public GameObject timeIsUp;
    // Start is called before the first frame update
    void Start()
    {
        timebar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {

            timeLeft -= Time.deltaTime;
            
            sec = (int)timeLeft;
            this.GetComponentInChildren<Text>().text = sec.ToString();
            timebar.fillAmount = timeLeft / maxTime;
        }
        if (sec == 0)
        {
            Destroy(GameObject.Find("TimeBar"));
            auto();
        }
    }

    /*
     * 
     * Automated turn end.
     * Invoked when time has run out.
     */
    public void auto()
    {
        GameObject gObj = GameObject.Find("P" + Player.currentID);
        int c = 0;

        for (int i = 0; i < gObj.transform.GetChild(0).childCount; i++)
        {
            {
                c++;
            }
        }
        for (int i = 0; i < gObj.transform.GetChild(1).childCount; i++)
        {
            {
                c++;
            }
        }

        switch (c)
        {
            case 14:
                if (Drag.isDragOnDiscard)
                {
                    Drag.drawnTile.returnToDiscard();
                }
                else if (Drag.isDragOnDeck)
                {
                    GameObject g = Drag.drawnTile.transform.gameObject;
                    Drag.drawnTile.returnToDeck();
                    Drag.disableDragOnDeck();
                    Destroy(Drag.drawnTile.transform.GetComponent<Drag>());
                    Drag.allowDrag = false;
                    Drag.drawnTile.transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    Drag.drawnTile = g.AddComponent<Drag>();
                    Drag.drawnTile.type = Drag.Slot.Allowed;
                    Drag.drawnTile.transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    //Drag.allowDrag = true;
                }
                draw();
                discard();
                break;
            case 15:
                discard();
                break;
        }
        if (c < 14)
        {
            draw();
            discard();
        }
    }

    /*
     * 
     * Automated draw of a tile from Deck.
     * Invoked when timer runs out and no tile was drawn neither from deck or discard possition.
     */
    public void draw()
    {
        GameObject deck = GameObject.Find("DeckOn");
        GameObject par = GameObject.Find("P" + Player.currentID);
        int indxH = 0;
        for (int i = 0; i < par.transform.childCount; i++)
        {
            if (par.transform.GetChild(i).name == "Top" && par.transform.GetChild(i).childCount < 8)
            {
                indxH = i;
            }
            else if (par.transform.GetChild(i).name == "Bot" && par.transform.GetChild(i).childCount < 8)
            {
                indxH = i;
            }
        }
        if (par.transform.GetChild(indxH).GetComponent<HandZone>().isShow)
        {
            deck.transform.GetChild(deck.transform.childCount - 1).transform.GetComponent<Image>().sprite = deck.transform.GetChild(deck.transform.childCount - 1).transform.GetComponent<SpriteRenderer>().sprite;
        }
        DiscardZone.EnableDeckDraw();
        deck.transform.GetChild(deck.transform.childCount - 1).transform.GetComponent<Drag>().type = Drag.Slot.Allowed;
        deck.transform.GetChild(deck.transform.childCount - 1).transform.SetParent(par.transform.GetChild(indxH).transform);
    }

    /*
     * 
     * Automated draw of a tile from Deck with tiles.
     * Invoked when timer runs out to random pick from hand a tile of current player and Discards it.
     */
    public void discard()
    {
        GameObject par = GameObject.Find("P" + Player.currentID);
        int r = Random.Range(0, 1);
        int r2 = Random.Range(0, par.transform.GetChild(r).transform.GetChild(r).childCount - 1);
        if (par.transform.GetChild(1).transform.GetChild(r2).name == "Clone")
        {
            r2++;
        }

        for (int j = 0; j < par.transform.childCount; j++)
        {
            if (par.transform.GetChild(j).name == "Discard")
            {
                par.transform.GetChild(r).transform.GetChild(r2).transform.GetComponent<Drag>().disableDragOnDiscard();
                par.transform.GetChild(r).transform.GetChild(r2).transform.position = par.transform.GetChild(j).GetComponent<DiscardZone>().matchDrop();
                par.transform.GetChild(r).transform.GetChild(r2).transform.GetComponent<Image>().sprite = par.transform.GetChild(r).transform.GetChild(r2).transform.GetComponent<SpriteRenderer>().sprite;
                par.transform.GetChild(r).transform.GetChild(r2).transform.SetParent(par.transform.GetChild(j).transform);
                par.transform.GetChild(r).transform.GetChild(r2).transform.SetAsLastSibling();
                DiscardZone.EnableDeckDraw();
                par.transform.GetChild(j).GetComponent<DiscardZone>().nextP();
                par.transform.GetChild(j).GetComponent<DiscardZone>().deckRotate();
            }
        }
        par = GameObject.Find("Player");
        par.GetComponent<Player>().setTimer(Player.currentID);
    }
}

