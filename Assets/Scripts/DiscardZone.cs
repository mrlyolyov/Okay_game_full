using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiscardZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    string nameOfDroppedTile;
    public Drag.Slot type = Drag.Slot.notAllowed;
    public static int counter;
    public static string lastTile;
    public static bool isValid;
    public void OnPointerEnter(PointerEventData eventdata)
    {   
        if (eventdata.pointerDrag == null)
            return;

        Drag d = eventdata.pointerDrag.GetComponent<Drag>();
        if (d != null)
        {
            d.placeholderParent = this.transform;
        }
    }


    public void OnPointerExit(PointerEventData eventdata)
    {
        if (eventdata.pointerDrag == null)
            return;

        Drag d = eventdata.pointerDrag.GetComponent<Drag>();
        if (d != null && d.placeholderParent == this.transform)
        {
            d.placeholderParent = d.parantToReturn;
        }
    }

    /*
     * Validates drop on discard possition and changes turns.
     */
    public void OnDrop(PointerEventData eventdata)
    {
        if (isValid) 
        {
            Okay okay = new Okay();
            string s = okay.format(eventdata.pointerDrag.name);
            Enable(s);
        }

        Drag d = eventdata.pointerDrag.GetComponent<Drag>();
        nameOfDroppedTile = eventdata.pointerDrag.name;
        if (d != null)
        {
            int a;
            a = eventdata.pointerDrag.transform.parent.GetChild(0).childCount + eventdata.pointerDrag.transform.parent.GetChild(1).childCount;
             if (type != d.type && eventdata.pointerDrag.transform.parent.name == "P" + Player.currentID && gameObject.transform.parent.name =="P"+Player.currentID&& a==14&& gameObject.transform.parent.name!="Discard")
             {
                type = Drag.Slot.Allowed;
                d.parantToReturn = this.transform;
                eventdata.pointerDrag.transform.position = matchDrop();
                eventdata.pointerDrag.GetComponent<Image>().sprite = eventdata.pointerDrag.GetComponent<SpriteRenderer>().sprite;
                this.transform.SetAsLastSibling();
                type = Drag.Slot.notAllowed;
                nextP();
                EnableDeckDraw();
                deckRotate();

                GameObject pl = GameObject.Find("Player");
                pl.GetComponent<Player>().setTimer(Player.currentID);
            }
        }
    }

    /*
     * 
     * Rotates the deck based on @param currentID.
     */
    public void deckRotate()
    {
        GameObject mid = GameObject.Find("Image");
        switch (Player.currentID)
        {
            case 1:
                mid.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 2:
                mid.transform.rotation= Quaternion.Euler(0, 0, 90);
                break;
            case 3:
                mid.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case 4:
                mid.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
        }
    }


    /*
     * 
     * Adds a drag component on the last tile on Deck.
     */
    public static void EnableDeckDraw()
    {
        if (GameObject.Find("DeckOn").transform.childCount >= 1) { 
        int c = GameObject.Find("DeckOn").transform.childCount - 1;
        GameObject enableDraw = GameObject.Find("DeckOn").transform.GetChild(c).gameObject;
        if (enableDraw.GetComponent<Drag>() == null) { 
            enableDraw.AddComponent<Drag>();
        }
        }
    }

    /*
     * 
     * Fixxed possition on the dropped tile in the discard spot based on current player.
     */
     public Vector3 matchDrop()
    {
        Vector3 vector3 = new Vector3();
        switch (Player.currentID)
        {
            case 1:
                vector3 = new Vector3(1230, 325, 0);
                return vector3;
            case 2:
                vector3 = new Vector3(1560, 820, 90);
                return vector3;
            case 3:
                vector3 = new Vector3(680, 755, 180);
                return vector3;
            case 4:
                vector3 = new Vector3(325, 277, -90);
                return vector3;
        }
        return vector3;
    }


    /*
     * 
     * Changes the value of currentID to the next active player.
     */
    public void nextP()
    {
        Destroy(GameObject.Find("TimeBar"));
        counter = Player.currentID;
        int c = 0;
        Player.currentID--;
        for (int i = 0; i < 4; i++)
        {
            if (i > Player.currentID && OkayGame.inGame[i])
            {
                c++;
            }
        }
        if (c == 0)
        {
            for (int i = 0; i < OkayGame.inGame.Length; i++)
            {
                if (i != Player.currentID && OkayGame.inGame[i])
                {
                    Player.currentID = i+1;
                    break;
                }
            }
        }
        else if (c != 0)
        {
            for (int i = Player.currentID; i < OkayGame.inGame.Length; i++)
            {
                if (i != Player.currentID && OkayGame.inGame[i])
                {
                    Player.currentID = i+1;
                    break;
                }
            }
        }
    }


    public void getTurn()
    {
        switch (Player.currentID)
        {
            case 0:
                Player.currentID++;
                break;
            case 1:
                Player.currentID++;
                break;
            case 2:
                Player.currentID++;
                break;
            case 3:
                Player.currentID++;
                break;
        }
    }


    /*
     * 
     * Attaches the end game method on the 'Win' button. 
     */
    public void Enable(string name)
    {
        Okay okay = new Okay();
        string s = lastTile;
        
        int offset = okay.spaceSearch(name);
        int n1 = int.Parse(name.Substring(0, offset));
        offset = okay.spaceSearch(s);
        int n2 = int.Parse(s.Substring(0, offset));

        if (n1 == n2)
        {
            offset = okay.spaceSearch(name);
            string c1 = name.Substring(offset,name.Length-offset).Trim();
            offset = okay.spaceSearch(s);
            string c2 = s.Substring(offset, s.Length - offset).Trim();
            if (c1.Equals(c2))
            {
                GameObject winner = GameObject.Find("P" + Okay.winner);
                winner.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(endGame);
            }
        }
    }


    /*
     * 
     * Invoked when winning is allowed.
     * End game method.
     */
    public void endGame()
    {
        RecetGame.setValues();
        SceneManager.LoadScene("Start");
    }
}
