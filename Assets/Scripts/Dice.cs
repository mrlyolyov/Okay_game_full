using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public Sprite[] diceSidess;
    public GameObject dicePre;
    private GameObject P1Dice;
    private GameObject P2Dice;
    private GameObject P3Dice;
    private GameObject P4Dice;
    public static int playerIndex=-1;
    public bool runOnce = true;
    public static int p1Num;
    public static int p2Num;
    public static int p3Num;
    public static int p4Num;
    public static bool O;
    public static bool K;
    public static bool A;
    public static bool Y;
    public int maxDice;
    public static bool haveDuplicates;
    public static bool isDealerFound;
    public static bool foundJoker;
    public static int indexJoker;
    GameObject pDiceState;
    GameObject stopDiceButton;

    /*
     * Resets the dice static field and dice objects prepares for New game.   
     */
    public static void reset()
    {
        p1Num = 0;
        p2Num = 0;
        p3Num = 0;
        p4Num = 0;
        O = false;
        K = false;
        A = false;
        Y = false;
        haveDuplicates = false;
        isDealerFound = false;
        foundJoker = false;
        indexJoker = 0;
        playerIndex = -1;
    }

    /*
     * Runs multiple checks on the active thrown dices.Until dealer of tiles is found. 
     */
    public void Update()
    {
        if (O && K && A && Y)
        {
            if (!isDealerFound)
            {
                findMax();
                checkDuplicates();
                if (haveDuplicates)
                {
                    updateDice();
                }
                if (!haveDuplicates)
                {
                    findMax();
                    restartRoll();
                    isDealerFound = true;
                }
            }
        }
    }
    /*
     * 
     * Creates a Dice prefab for Bottom player. Sets it's possion, values, image and activates it's button with a roller method. 
     */
    public void botDice()
    {
        pDiceState = GameObject.Find("P1");
        P1Dice = Instantiate(dicePre, new Vector3(1350, 100, 0), transform.rotation);
        P1Dice.GetComponent<Image>().sprite = diceSidess[2];
        P1Dice.transform.SetParent(pDiceState.transform);
        P1Dice.name = name + 1;                                                          //Dice for player One                                                                            // P1Dice.transform.position = new Vector3(1350, 100, 0);
        P1Dice.GetComponent<Button>().onClick.AddListener(firstRoll);
    }

    /*
     * 
     * Creates a Dice prefab for Right player. Sets it's possion, values, image and activates it's button with a roller method. 
     */
    public void rightDice()
    {
        pDiceState = GameObject.Find("P2");
        P2Dice = Instantiate(dicePre, transform.position, transform.rotation);
        P2Dice.GetComponent<Image>().sprite = diceSidess[2];
        P2Dice.transform.SetParent(pDiceState.transform);
        P2Dice.name = name + 2;                                                          //Dice for player Two
        P2Dice.transform.position = new Vector3(1800, 950, 0);
        P2Dice.GetComponent<Button>().onClick.AddListener(secondRoll);
    }

    /*
     * Creates a Dice prefab for Top player. Sets it's possion, values, image and activates it's button with a roller method. 
     */
    public void topDice()
    {
        pDiceState = GameObject.Find("P3");
        P3Dice = Instantiate(dicePre, transform.position, transform.rotation);
        P3Dice.GetComponent<Image>().sprite = diceSidess[2];
        P3Dice.transform.SetParent(pDiceState.transform);
        P3Dice.name = name + 3;                                                      //Dice for player Three
        P3Dice.transform.position = new Vector3(550, 980, 0);
        P3Dice.GetComponent<Button>().onClick.AddListener(thirdRoll);
    }

    /*
     * Creates a Dice prefab for Left player. Sets it's possion, values, image and activates it's button with a roller method. 
     */
    public void leftDice()
    {
        pDiceState = GameObject.Find("P4");
        P4Dice = Instantiate(dicePre, transform.position, transform.rotation);
        P4Dice.GetComponent<Image>().sprite = diceSidess[2];
        P4Dice.transform.SetParent(pDiceState.transform);
        P4Dice.name = name + 4;                                                        //Dice for player Four
        P4Dice.transform.position = new Vector3(110, 150, 0);
        P4Dice.GetComponent<Button>().onClick.AddListener(fourthRoll);
    }

    /*
     * 
     * Performs a dice roll on bottom player.
     */
    public void firstRoll()
    {
        rollOne(1);
    }

    /*
     * 
     * Performs a dice roll on right player.
     */
    public void secondRoll()
    {
        rollTwo(2);
    }
    /*
     * 
     * Performs a dice roll on top player.
     */
    public void thirdRoll()
    {
        rollThree(3);
    }
    /*
     * 
     * Performs a dice roll on left player.
     */
    public void fourthRoll()
    {
        rollFour(4);
    }

    /*
     * 
     * Simulates the rolling of dice of Bot possition player.
     * @param p used to assign name of rolling dice later needed to for search.
     */
    public void rollOne(int p)
    {
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            P1Dice.GetComponent<Image>().sprite = diceSidess[randomDiceSide];
        }
        p1Num = randomDiceSide + 1;
        stopDiceButton = GameObject.Find("Dice"+p);
        stopDiceButton.GetComponent<Button>().interactable = false;
        O = true;
    }

    /*
     * 
     * Simulates the rolling of dice of Right possition player.
     * @param p used to assign name of rolling dice later needed to for search.
     */
    public void rollTwo(int p)
    {
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            P2Dice.GetComponent<Image>().sprite = diceSidess[randomDiceSide];
        }
        p2Num = randomDiceSide + 1;
        stopDiceButton = GameObject.Find("Dice"+p);
        stopDiceButton.GetComponent<Button>().interactable = false;
        K = true;
    }

    /*
     * 
     * Simulates the rolling of dice of Top possition player.
     * @param p used to assign name of rolling dice later needed to for search.
     */
    public void rollThree(int p)
    {
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            P3Dice.GetComponent<Image>().sprite = diceSidess[randomDiceSide];
        }
        p3Num = randomDiceSide + 1;

        stopDiceButton = GameObject.Find("Dice"+p);
        stopDiceButton.GetComponent<Button>().interactable = false;
        A = true;
    }

    /*
     * 
     * Simulates the rolling of dice of Left possition player.
     * @param p used to assign name of rolling dice later needed to for search.
     */
    public void rollFour(int p)
    {
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            P4Dice.GetComponent<Image>().sprite = diceSidess[randomDiceSide];
        }

        p4Num = randomDiceSide + 1;
        stopDiceButton = GameObject.Find("Dice"+p);
        stopDiceButton.GetComponent<Button>().interactable = false;
        Y = true;
    }
  

    /*
     * 
     * Reactivates the dice button for the players with duplicate maximum values from last thrown.
     * It's used when duplicates are found.
     */
    public void restartRoll()
    {
        if (p1Num == maxDice)
        {
            P1Dice.GetComponent<Button>().onClick.AddListener(firstRoll);
            O = false;
        }

        else if (p2Num == maxDice)
        {
            P2Dice.GetComponent<Button>().onClick.AddListener(secondRoll);
            K = false;
        }

        else if (p3Num == maxDice)
        {
            P3Dice.GetComponent<Button>().onClick.AddListener(thirdRoll);
            A = false;
        }

        else if (p4Num == maxDice)
        {
            P4Dice.GetComponent<Button>().onClick.AddListener(fourthRoll);
            Y = false;
        }
    }

    /*
     * 
     * Performs a check if duplicate dice's values have been thrown.
     * Used to determine if Dice needs to be thrown again. 
     */
    public void checkDuplicates()
    {
        int[] b = { p1Num, p2Num, p3Num, p4Num };
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            if (b[i] == maxDice)
            {
                count = count + 1;   
            }
            else if (b[i] != maxDice)
            {
                b[i] = 0;
            }
        }
        if (count > 1)
        {
            haveDuplicates = true;
        }
        else
        {
            haveDuplicates = false;
        }
    }

    /*
     * 
     * Updates the dice's for the players that have thrown equal values.
     */
    public void updateDice()
    {
       
        int[] c = { p1Num, p2Num, p3Num, p4Num };
        
        for (int i = 0; i < 4; i++)
        {
            if (c[i]==maxDice)
            {
                resetButton(i);
                c[i] = 0;
            }

            else if (c[i] != maxDice)
            {
                c[i] = 0;
            }
            p1Num = 0;
            p2Num = 0;
            p3Num = 0;
            p4Num = 0;
        }
    }

    /*
     * 
     * Reactivates the button of the players that are have thrown duplicate dice values.
     * @param p corresponding player who should have his button reset and perform another throw.
     */
    public void resetButton(int p)
    {
        if (p == 0)
        {
            P1Dice.GetComponent<Button>().interactable = true;
            P1Dice.GetComponent<Button>().onClick.AddListener(firstRoll);
            O = false;
        }else if (p == 1)
        {
            P2Dice.GetComponent<Button>().interactable = true;
            P2Dice.GetComponent<Button>().onClick.AddListener(secondRoll);
            p2Num = 0;
            K = false;
        }else if (p == 2)
        {
            P3Dice.GetComponent<Button>().interactable = true;
            P3Dice.GetComponent<Button>().onClick.AddListener(thirdRoll);
            p3Num = 0;
            A = false;
        }else if (p == 3)
        {
            P4Dice.GetComponent<Button>().interactable = true;
            P4Dice.GetComponent<Button>().onClick.AddListener(fourthRoll);
            Y = false;
            p4Num = 0;
        }
    }

    /*
     * 
     * Finds the maximum value that have been thrown of the dice.
     * @param playerIndex is beeing assing.
     */
    public void findMax()
    {
        int[] a = { p1Num, p2Num, p3Num, p4Num };
        int max = 0;
        maxDice = 0;
        for (int i = 0; i < 4; i++)
        {
            if (a[i] > maxDice)
            {
                maxDice = a[i];
                max = i;
            }
        }
        playerIndex = max;
    }
}
