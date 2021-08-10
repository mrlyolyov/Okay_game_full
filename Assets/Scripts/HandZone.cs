using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Drag.Slot type = Drag.Slot.Allowed;
    int p;
    public bool isShow;
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
    }

    /*
     * Validates drop in hand zone.
     */
    public void OnDrop(PointerEventData eventdata)
    {
        Drag d = eventdata.pointerDrag.GetComponent<Drag>();
        if (Player.onStart && eventdata.pointerDrag.transform.parent.name=="P"+Player.currentID)
        {
        }
        previousP();
        if (d != null && d.parantToReturn != null)
        { 
            if (type == d.type && d.parantToReturn.parent.name == gameObject.transform.parent.name && gameObject.transform.childCount <= 8)                            // Ako ne e v drop zone allowed varni se 
            {
               d.parantToReturn = this.transform;
            }
            else if (d.parantToReturn.parent.name == "P" +p)
            {
                d.parantToReturn = this.transform;
            }
            else if(d.parantToReturn.name=="DeckOn")
            { 
                d.parantToReturn = this.transform;
            }
        }
    }

    /*
     * 
     * Find the previous player.
     * Invoked to check if the tile from discard comes from previous player.
     */
    public void previousP()
    {
        int c = 0;
        p=Player.currentID-1;
        
        for (int i = 0; i < 4; i++)
        {
            if (i < p && OkayGame.inGame[i])
            {   
                c++;
            }
        }
        if (c == 0)
        {
            for (int i = 0; i < OkayGame.inGame.Length; i++)
            {
                if ( OkayGame.inGame[i])
                {
                    p = i+1;
                }
            }
        }
        else if (c != 0)
        {
            for (int i = p; i >= 0; i--)
            {
                if (i != p && OkayGame.inGame[i])
                {
                    p = i+1;
                    break;
                }
            }
        }
    }
}
