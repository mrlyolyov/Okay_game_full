
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Transform parantToReturn = null;
    public Transform discardParent = null;
    public Transform placeholderParent = null;

    public enum Slot { Allowed, notAllowed }
    public Slot type = Slot.notAllowed;
    Quaternion q;
    GameObject placeHolder = null;
    public int previousPlayerIndx;
    Vector3 deckPos;
    Vector3 discardPos;
    public static bool isDragOnDiscard;
    public static bool isDragOnDeck;
    public static Drag drawnTile;
    public static GameObject T;
    public static bool allowDrag;
    string discardName = "Discard";
    string deckName = "DeckOn";


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameObject.transform.parent.name == discardName)
        {
            discardParent = gameObject.transform.parent.transform;
            disableDragOnDeck();
            discardPos = this.transform.parent.position;
            isDragOnDiscard = true;
            drawnTile = this.transform.GetComponent<Drag>();
        }
        else if(gameObject.transform.parent.name == deckName)
        {
            discardParent = gameObject.transform.parent.transform;
            deckPos = this.transform.parent.position;
            disableDragOnDiscard();
            drawnTile = this.transform.GetComponent<Drag>();
            allowDrag = true;
            isDragOnDeck = true;
        }
        else
        {
            discardParent = this.transform.parent;
        }

        Clone();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (allowDrag)
        {
            this.transform.position = eventData.position;
        }
        
        if (eventData.pointerDrag.transform.parent.name == "P1")
            {
            this.transform.position = eventData.position;
            clonePosition("P1");
            }
            else if (eventData.pointerDrag.transform.parent.name == "P3")
            {
            this.transform.position = eventData.position;
            clonePosition("P3");
            }
            else if (eventData.pointerDrag.transform.parent.name == "P2")
            {
            this.transform.position = eventData.position;
            clonePosition("P2");
            }
            else if (eventData.pointerDrag.transform.parent.name == "P4")
            {
            this.transform.position = eventData.position;
            clonePosition("P4");
            }
    }

    /* 
     * 
     * Triggered when Tile is released. 
     */
    public void OnEndDrag(PointerEventData eventData)
    {
        if (discardParent != null)
        {
            if (this.transform.parent.name == parantToReturn.parent.name && parantToReturn.parent.name != "Image" && discardParent.name != discardName && parantToReturn.childCount<=8)
            {
                this.transform.SetParent(this.parantToReturn);

                if (parantToReturn.name == discardName)
                {
                    this.transform.SetAsLastSibling();
                }
                else
                    this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
            }
            else if (discardParent.name == discardName && "P" + Player.currentID.ToString() == parantToReturn.parent.name)
            {
                this.transform.SetParent(this.parantToReturn);
                rotationSet();
                validateOkay();
                isDragOnDiscard = false;
                gameObject.transform.rotation = q;
                this.transform.GetComponent<Image>().sprite = findImage(this.transform.name);
            }
            else if (discardParent.name == deckName && "P" + Player.currentID.ToString() == parantToReturn.parent.name)
            {
                drawFromDeck();
                isDragOnDeck = false;
            }
            else if (discardParent.name == deckName && "P" + Player.currentID.ToString() != parantToReturn.parent.name)
            {
                returnToDeck();
                isDragOnDeck = false;
            }
            else if (discardParent.name == discardName && "P" + Player.currentID.ToString() != parantToReturn.parent.name)
            {
                returnToDiscard();
                DiscardZone.EnableDeckDraw();
                isDragOnDiscard = false;
            }
            else
            {
                
                this.transform.SetParent(discardParent);
                this.transform.position = discardPos;
            }
            discardParent = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            Destroy(placeHolder);
        }
    }

    // Used when correct player is drawing a tile from Deck to reparent it.
    // Triggers a hand check for Okay combination. 
    public void drawFromDeck()
    {
        this.transform.GetComponent<Drag>().type = Slot.Allowed;
        this.transform.SetParent(this.parantToReturn);
        this.transform.GetComponent<Image>().sprite = findImage(this.transform.name);
        validateOkay();
    }

    // Return the dragged from Deck tile.
    // Used when Tile fails to reach valid drop spot && when Time for players turn is up.
    public void returnToDeck()
    {
        this.transform.position = deckPos;
        this.transform.SetParent(discardParent);
     // drawnTile = null;
        isDragOnDeck = false;
    }

    // Return the dragged from discard tile.
    // Used when Tile fails to reach valid drop spot and when Time for players turn is up. 
    public void returnToDiscard()
    {
        this.transform.position = discardPos;
        this.transform.SetParent(discardParent);
        this.transform.SetAsLastSibling();
        drawnTile = null;
        isDragOnDiscard = false;
    }


    
    public Sprite findImage(string imageN)
    {
        GameObject d = GameObject.Find("Deck").gameObject;

        if (parantToReturn.GetComponent<HandZone>().isShow) { 
        for(int i=0; i < d.GetComponent<Deck>().Tiles.Length; i++) { 
           if( d.GetComponent<Deck>().Tiles[i].name == imageN)
            {
                return d.GetComponent<Deck>().Tiles[i];
            }
        }
        }
        return d.GetComponent<Deck>().Tiles[106];
    }

    public void childSetIndex(string name)
    {
        int index=0;
        for(int i = 0; i < parantToReturn.childCount; i++)
        {
            if (parantToReturn.GetChild(i).name == name)
            {
                index = i;
            }
        }
        this.parantToReturn.GetChild(index).SetSiblingIndex(10000);
    }


    /*
     * Rotates the tile based on the current player.
     * Invoked when 
     */
    public void rotationSet()
    {
     match();
     }

    //Assign rotation on tile depending on @param Player.currentP.
    //Used when successfully dropped the tile taken from Discard spot.
    public void match()
    {
        switch (Player.currentID)
        {
            case 1:
                q = Quaternion.Euler(0, 0, 0);
                break;
            case 2:
                q = Quaternion.Euler(0, 0, 90);
                break;
            case 3:
                q = Quaternion.Euler(0, 0, 180);
                break;
            case 4:
                q = Quaternion.Euler(0, 0, -90);
                break;
        }
    }

    /*
     * Invoked on dragging creates a copy of dragged tile.
     */
    public void Clone()
    {
        placeHolder = new GameObject("Clone");
        if (this.transform.parent.name != deckName) { 
            placeHolder.transform.SetParent(this.transform.parent);
        }
        LayoutElement le = placeHolder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        if (this.transform.parent != null) 
        { 
            parantToReturn = this.transform.parent;
      
        placeholderParent = parantToReturn;

        if (this.transform.parent.parent.name == parantToReturn.parent.name)
        {
            this.transform.SetParent(this.transform.parent.parent);
        }
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    /*
     * 
     * Is invoked whenever a player is dragging any of the tiles in his hand.
     * Depending on the player it creates a 'Clone tile' to act as a replacement.
     * @param s is used to determains the dragging tile's owner and 'Clone tile' possition. 
     */
    public void clonePosition(string s)
    {
        switch (s)
        {
            case "P1":
                botClone();
                break;
            case "P2":
              rightClone();
                break;
            case "P3":
                topClone();
                break;
            case "P4":
                leftClone();
                break;
        }
    }

    /*
     * 
     * Is invoked to create a 'Clone tile' for bottom player and rearranging tiles in hand.
     */
    public void botClone()
    {
        if (placeHolder.transform.parent != placeholderParent && discardParent.name != discardName) { 
            placeHolder.transform.SetParent(placeholderParent);
        }
        int newIndex = placeholderParent.childCount;
        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                newIndex = i;

                if (placeHolder.transform.GetSiblingIndex() < newIndex)
                    newIndex--;
                break;
            }
        }
        placeHolder.transform.SetSiblingIndex(newIndex);
    }

    /*
     * 
     * Is invoked to create a 'Clone tile' for right player.
     */
    public void rightClone()
    {
        if (placeHolder.transform.parent != placeholderParent && discardParent.name != discardName)
        {
            placeHolder.transform.SetParent(placeholderParent);
        }
        int newIndex = placeholderParent.childCount;
        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (this.transform.position.y < placeholderParent.GetChild(i).position.y)
            {
                newIndex = i;

                if (placeHolder.transform.GetSiblingIndex() < newIndex)
                    newIndex--;
                break;
            }
        }
        placeHolder.transform.SetSiblingIndex(newIndex);
    }

    //Is invoked to create a 'Clone tile' for top player.
    public void topClone()
    {
        if (placeHolder.transform.parent != placeholderParent && discardParent.name != discardName)
        {
            placeHolder.transform.SetParent(placeholderParent);
        }
        int newIndex = placeholderParent.childCount;
        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (this.transform.position.x > placeholderParent.GetChild(i).position.x)
            {
                newIndex = i;

                if (placeHolder.transform.GetSiblingIndex() < newIndex)
                    newIndex--;
                break;
            }
        }
        placeHolder.transform.SetSiblingIndex(newIndex);
    }

    /*
     * 
     * Is invoked to create a 'Clone tile' for left player.
     */
    public void leftClone()
    {
        if (placeHolder.transform.parent != placeholderParent && discardParent.name != discardName)
        {
            placeHolder.transform.SetParent(placeholderParent);
        }
        int newIndex = placeholderParent.childCount;
        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (this.transform.position.y > placeholderParent.GetChild(i).position.y)
            {
                newIndex = i;

                if (placeHolder.transform.GetSiblingIndex() < newIndex)
                    newIndex--;
                break;
            }
        }
        placeHolder.transform.SetSiblingIndex(newIndex);
    }

    /*
     * 
     * Invokes this whenever a player has drawn a Tile and has successfully dropped it in his hand.
     * Takes the current players name of tiles and converts them to list of strings that are formatted and validated for Okay winning match.  
    */
    public void validateOkay()
    {
        List<string> list = new List<string>();
        GameObject g = GameObject.Find("P" + Player.currentID);

        for (int i = 0; i < g.transform.GetChild(0).childCount; i++)
        {   
            list.Add(g.transform.GetChild(0).GetChild(i).name);
        }
        for (int i = 0; i < g.transform.GetChild(1).childCount; i++)
        {
            list.Add(g.transform.GetChild(1).GetChild(i).name);
        }

        Okay okay = new Okay();
        okay.setNames(list);
    }

    /*
     * 
     * Disables dragging on deck to prevent double draw.
     * Activated when draw from Discard spot was initiated.
     */
    public static void disableDragOnDeck()
    {
        GameObject deck = GameObject.Find("DeckOn");
        for (int i = 0; i < deck.transform.childCount; i++)
        {
            if (deck.transform.GetChild(i).GetComponent<Drag>() != null)
            {
                Destroy(deck.transform.GetChild(i).GetComponent<Drag>());
            }
        }
    }

    /* 
     * 
     * Disables drag on discard spots to prevent 
     * double draw activated when draw was initiated from Deck.
     */
    public void disableDragOnDiscard()
    {
        for (int i = 0; i < OkayGame.inGame.Length; i++)
        {
            if (OkayGame.inGame[i])
            {
                int t = i + 1;
                GameObject g = GameObject.Find("P" + t);
          
                for(int j = 0; j < g.transform.childCount; j++)
                {
                    if (g.transform.GetChild(j).name == discardName)
                    {
                        removeDrag(g.transform.GetChild(j).transform);
                    }
                }
            }
        }
    }

    /* 
     * 
     * Removing draw script component from the transform of the object.
     * @param tr uses it to check all of the children of discard with Drag scrips and Destroys them.
    */
    public void removeDrag(Transform tr)
    {
        if (tr.childCount != 0)
        {
            for(int i = 0;i < tr.childCount; i++)
            {
                if (tr.GetChild(i).GetComponent<Drag>() != null)
                {
                    Destroy(tr.GetChild(i).GetComponent<Drag>());
                }
            }
        }
    }
}

