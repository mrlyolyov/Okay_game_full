using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OkayGame : MonoBehaviour
{
    public Deck deck;
    public int runOnce;
    public Player p;
    public static bool[] inGame= {true,true,true,true};
    public bool winner;
    // Start is called before the first frame update

    public void Start()
    {
        gameSetUp();
        p.spawn();
    }

    // Update is called once per frame
    void Update()
    {
         if (runOnce==0 && Dice.isDealerFound)
        { 
            deck.deckCreate();
            addExitButton();
            runOnce++;  
        }

    }

    /*
     * 
     * Attaches on exit buttons methods from 'Player' class.
     */
    public void addExitButton()
    {
        GameObject empty;
        for ( int i = 0; i < 5; i++)
        {
            if (GameObject.Find("P" + i) != null) { 
                empty = GameObject.Find("P" + i);
                for(int j = 0; j< empty.transform.childCount; j++)
                {
                    if (empty.transform.GetChild(j).name =="Exit" && i == 1)
                    {
                        empty.transform.GetChild(j).GetComponent<Button>().onClick.AddListener(p.exitBottom);
                    }
                    else if (empty.transform.GetChild(j).name == "Exit" && i == 2)
                    {
                        empty.transform.GetChild(j).GetComponent<Button>().onClick.AddListener(p.exitRight);
                    }
                    else if (empty.transform.GetChild(j).name == "Exit" && i == 3)
                    {
                        empty.transform.GetChild(j).GetComponent<Button>().onClick.AddListener(p.exitTop);
                    }
                    else if (empty.transform.GetChild(j).name == "Exit" && i == 4)
                    {
                        empty.transform.GetChild(j).GetComponent<Button>().onClick.AddListener(p.exitLeft);
                    }
                }
            }
        }
    }


    public void gameSetUp()
    {
        for(int i = 0; i< inGame.Length; i++)
        {
            inGame[i] = PlayersCount.p[i];
        }
    }

    /*
     * 
     * Does a check on game's current state by checking the number of active players.
     * If remaining players < 2 Loads 'start' scene.
     */
    public static bool hasMinimumPlayers()
    {
        int c = 0;
        for(int i = 0; i < inGame.Length; i++)
        {
            if (inGame[i])
            {
                c++;
            }
        }
        if (c > 1)
        {
            return true;
        }
        else
        {
            RecetGame.setValues();
            SceneManager.LoadScene("Start");
            return false;
        }
    }
}
