using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecetGame : MonoBehaviour
{
    /*
     * Resets all values that are necessary for valid game flow to start. 
     */
    public static void setValues()
    {
        Dice.reset();
        DiscardZone.counter = 0;
        DiscardZone.lastTile = null;
        DiscardZone.isValid = false;
        Okay.winner = 0;
        Timer.sec = 0;
        Drag.isDragOnDiscard = false;
        Drag.isDragOnDeck = false;
        Drag.drawnTile = null;
        Player.currentID = 0;
        Player.thrown = false;
        Player.changeTurn = true;
        Player.onStart = false;
        OkayGame.inGame[0] = true;
        OkayGame.inGame[1] = true;
        OkayGame.inGame[2] = true;
        OkayGame.inGame[3] = true;
        PlayersCount.p[0] = false;
        PlayersCount.p[1] = false;
        PlayersCount.p[2] = false;
        PlayersCount.p[3] = false;
    }
}
