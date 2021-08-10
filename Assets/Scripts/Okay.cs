using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Okay  
{
    List<string> handCheck = new List<string>();
    Dictionary<string, int> handDictionary = new Dictionary<string, int>(); 
    public List<string> matchColor=new List<string>();
    public List<string> matchNum= new List<string>();
    public bool[] okayGame;
    public int counSet;
    public int counRun;
    public int okNumber = 14;
    public static int winner;
    public string extraTile;
    int numTile;
    string colorTile;

    /*
     * 
     *Taking the list with Tile Names and formatting them to validate Okay.
     *@param list taking the names of tiles.
     *Invoked when 15th tile has successfully been dropped in hand.
    */
    public void setNames(List<string> list)
    {
        for (int j = 0; j < list.Capacity-1; j++)
        {
            string name = format(list[j]);
            int offset = spaceSearch(name);

            if (!(name.Substring(offset, name.Length - offset)).Equals(" Clone") && !(name.Substring(offset, name.Length - offset)).Equals(" Joker"))
            {
                handCheck.Add(name);
            }
          else if((name.Substring(offset, name.Length - offset)).Equals(" Joker"))
            {
                handCheck.Add(format(Deck.jokerSearch));
            }
        }

        insertToDictionary();
        colourSet();
    }

    /*
     *Formats the name of the sprite.
     *@param name is the name of the sprite.
     */
    public string format(string name)
    {
        int spaceIndx = 0;
        string con = "";
        char[] space = name.ToCharArray();
        for (int k = 0; k < space.Length; k++)
        {
            if (space[k] == ' ')
            {
                spaceIndx = k;
            }
        }
        switch (spaceIndx)
        {
            case 1:
                con = space[spaceIndx - 1].ToString();
                numTile = int.Parse(con);
                break;
            case 2:
                if (space[spaceIndx - 2].ToString() != "0")
                {
                    con = space[spaceIndx - 2].ToString() + space[spaceIndx - 1].ToString();
                }
                else if (space[spaceIndx - 2].ToString() == "0")
                {
                    con = space[spaceIndx - 1].ToString();
                }
                numTile = int.Parse(con);
                break;
            case 3:
                con = space[spaceIndx - 2].ToString() + space[spaceIndx - 1].ToString();
                numTile = int.Parse(con);
                break;
        }
        colorTile = name.Substring(spaceIndx, name.Length - spaceIndx);
        return numTile + " " + colorTile;
    }

    /*
     * 
     * @param handCheck Inserting of strings in  
     * @param Hand(dictionary).
    */
    public void insertToDictionary()
    {
       // test();

        foreach (string tile in handCheck)
        {
            if (!handDictionary.ContainsKey(tile))
            {
                handDictionary.Add(tile, 1);
            }
            else
            {
                int count;
                handDictionary.TryGetValue(tile, out count);
                handDictionary.Remove(tile);
                handDictionary.Add(tile, count + 1);
            }
        }
    }

    /*
     * 
     * Divide String of Names in hand into two lists of numbers and colors.
     */
    public void colourSet()
    {
        string color;
        string num;
        
        foreach (KeyValuePair<string, int> tile in handDictionary)
        {
            int index = spaceSearch(tile.Key);
            color = tile.Key.Substring(index, tile.Key.Length - index);
            //      matchColor.Add(color);
            if (tile.Key.Substring(0, index).StartsWith("0"))
            {
                num = tile.Key.Substring(1, index);
            }
            else
                num = tile.Key.Substring(0, index);

            matchNum.Add(num);                   // Fill in matchNum. 
        }
        checkNum();
    }

    /*
     * Inserting all numbers into dictionary 
     */
    public void checkNum()
    {
        
        handDictionary.Clear();
        foreach (string tile in matchNum)
        {
            if (!handDictionary.ContainsKey(tile))
            {
                handDictionary.Add(tile, 1);
            }
            else
            {
                int count;
                handDictionary.TryGetValue(tile, out count);
                handDictionary.Remove(tile);
                handDictionary.Add(tile, count + 1);
            }
        }
        counSet = 0;
        foreach (KeyValuePair<string, int> tile in handDictionary)
        {
            if (tile.Value == 3)
            {
                validateSet(tile.Key.ToString());
            }
            else if (tile.Value == 4)
            {
                validateSet(tile.Key.ToString());
            }
        }

        colorFinder();
    }


    /*
     * 
     * Validates an okay set.
     * when consists of three or four tiles of the same number and different colours. 
     */
    public void validateSet(string num)
    {
        List<string> setCheck = new List<string>();

        foreach (string name in handCheck)
        {
            int space = spaceSearch(name);
            if (name.StartsWith(num) && !setCheck.Contains(name) && name.Substring(space, name.Length - space) != " Joker")
            {
                setCheck.Add(name);
                counRun++;
            }
        }

        //var duplicateExists = setCheck.GroupBy(n => n).Any(g => g.Count() > 1);
        foreach (string s in setCheck)
        {
            handCheck.Remove(s);
            okNumber--;
            int t = spaceSearch(s);
        }
        counSet++;
    }

    /*
     * 
     * Searches for identical colors in hand with @param handDictionary and validates a run.
     */
    public void colorFinder()
    {
        handDictionary.Clear();

        foreach (string tile in handCheck)
        {
          //  int sIndx = spaceSearch(tile);
            //string color = tile.Substring(sIndx, tile.Length - sIndx);
            if (!handDictionary.ContainsKey(tile))
            {
                handDictionary.Add(tile, 1);
            }
            else
            {
                int count;
                handDictionary.TryGetValue(tile, out count);
                handDictionary.Remove(tile);
                handDictionary.Add(tile, count + 1);
            }
        }

        foreach (KeyValuePair<string, int> tile in handDictionary)
        {
            string color;
            int index = spaceSearch(tile.Key);
            color = tile.Key.Substring(index, tile.Key.Length - index);
            matchColor.Add(color);
        }
        handDictionary.Clear();

        foreach (string tile in matchColor)
        {
            if (!handDictionary.ContainsKey(tile))
            {
                handDictionary.Add(tile, 1);
            }
            else
            {
                int count = 0;
                handDictionary.TryGetValue(tile, out count);
                handDictionary.Remove(tile);
                handDictionary.Add(tile, count + 1);
            }
        }
        foreach (KeyValuePair<string, int> tile in handDictionary)
        {
            if (tile.Value == 3)
            {
                validateRun(tile.Key.Trim().ToString());
            }
            else if (tile.Value == 4)
            {
                validateRun(tile.Key.Trim().ToString());
            }
            else if (tile.Value == 5 || tile.Value > 5)
            {
                validateRun(tile.Key.Trim().ToString());
            }
            
        }
    }

    /*
     * 
     * Validates a Okay Run pair.
     * @param color finds the tiles in hand with that color and inserts their numbers to a list wich is later user to check if they are consecutive.
     */
    public void validateRun(string color)
    {
        List<int> runCheck = new List<int>();
        int counter = 0;
        foreach (string tile in handCheck)
        {
            int offSet = spaceSearch(tile);
            string tileColor = tile.Substring(offSet, tile.Length - offSet);
            
            if (tileColor.Trim().Equals(color.Trim()))
            {
                int tileNum = int.Parse(tile.Substring(0, offSet).Trim());
                runCheck.Add(tileNum);
                counter++;
            }
        }

        foreach( int num in runCheck)
        {
             handCheck.Remove(num+" "+color);
        }

        // var duplicateExists = runCheck.GroupBy(n => n).Any(g => g.Count() > 1);

        runCheck.Sort();
        if(isConsecutive(runCheck) == 1)
        {
            okNumber = okNumber - counter;
        }
        if (okNumber == 0)
        {
            activateWin();
        }
    }

    /*
     * Used to check if numbers are consecutive.
     * @param numbers needs a list of sorted numbers to check and validate if are consecutive.
     * @return integer value later used to validate Okay Runs. 
     */
    public int isConsecutive(List<int> numbers)
    {

        for (int i = 0; i < numbers.Count-1; i++)
        {
            if (numbers[i] + 1 != numbers[i + 1] )
                return 0; //which means false and it's not a consecutive list.
        }
       return 1;
      }

    /*Searches the space in a string and returns the index of it.
     *@param s takes a string with space and finds the char index.
     *@return valid empty space index used for further string validation.
    */
    public int spaceSearch(string s)
    {
        char[] space = s.ToArray();
        int spaceIndx = 0;
        for (int k = 0; k < space.Length; k++)
        {
            if (space[k] == ' ')
            {
                spaceIndx = k;
            }
        }
        return spaceIndx;
    }

    /*
     * When all other tiles in hand are either set's or run's gives the player chance to win only if he discards the one tile that is not a valid combination.
     */
    public void activateWin()
    {
        winner = Player.currentID;
        DiscardZone.lastTile = handCheck[0];
        DiscardZone.isValid = true;
    }

    /*   public void test()
       {
           tileNames[0] = "7 Blue";
           tileNames[1] = "7 Red";
           tileNames[2] = "7 Yellow";
           tileNames[3] = "12 Green";
           tileNames[4] = "12 Blue";
           tileNames[5] = "12 Black";
           tileNames[6] = "4 Red";

           tileNames[7] = "7 Blue";
           tileNames[8] = "9 Blue";
           tileNames[9] = "5 Yellow";
           tileNames[10] = "8 Blue";
           tileNames[11] = "12 Yellow";
           tileNames[12] = "1 Red";
           tileNames[13] = "3 Red";
           tileNames[14] = "2 Red";
       }

       public void test()
       {
           tileNames[0] = "6 Black";
           tileNames[1] = "6 Red";
           tileNames[2] = "6 Yellow";
           tileNames[3] = "7 Blue";
           tileNames[4] = "8 Blue";
           tileNames[5] = "9 Blue";
           tileNames[6] = "10 Blue";

           tileNames[7] = "3 Red";
           tileNames[8] = "3 Yellow";
           tileNames[9] = "3 Black";
           tileNames[10] = "3 Blue";
           tileNames[11] = "2 Yellow";
           tileNames[12] = "3 Yellow";
           tileNames[13] = "4 Yellow";
           tileNames[14] = "2 Red";
       }
       */
    public void test()
    {
        handCheck[0] = "1 Black";
        handCheck[1] = "1 Red";
        handCheck[2] = "1 Yellow";
        handCheck[3] = "11 Blue";
        handCheck[4] = "8 Blue";
        handCheck[5] = "9 Blue";
        handCheck[6] = "10 Blue";

        handCheck[7] = "7 Blue";
        handCheck[8] = "12 Blue";
        handCheck[9] = "1 Blue";
        handCheck[10] = "7 Yellow";
        handCheck[11] = "7 Red";
        handCheck[12] = "7 Blue";
        handCheck[13] = "7 Black";
        handCheck[14] = "2 Red";
    }
}
