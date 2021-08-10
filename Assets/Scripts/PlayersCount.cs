using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayersCount : MonoBehaviour
{
    public static bool A;
    public static bool B;
    public static bool C;
    public static bool D;

    public static bool isInstructionsOn;

    public static bool[] p= {false,false,false,false};
    public Toggle[] toggle;
    // Start is called before the first frame update
    public void startGame()
    {
        
        int active = 0;
        for (int i = 0; i < toggle.Length; i++)
        {
            if (toggle[i].isOn)
            {
                active++;
                toggle[i].isOn = p[i];
            }
        }

        if (active >= 2)
        {
            for (int i = 0; i < toggle.Length; i++)
            {
                if (!toggle[i].isOn)
                {
                    switch (i)
                    {
                        case 0:
                            Dice.O = true;
                            break;
                        case 1:
                            Dice.K = true;
                            break;
                        case 2:
                            Dice.A = true;
                            break;
                        case 3:
                            Dice.Y = true;
                            break;
                    }
                }
                SceneManager.LoadScene("MainGameScene");
            }
        }
    }

  public void bot()
    {
        p[0] = toggle[0].isOn;
    }
    public void left()
    {
        p[3] = toggle[3].isOn;
    }
    public void top()
    {
        p[2] = toggle[2].isOn;
    }
    public void right()
    {
        p[1] = toggle[1].isOn;
    }

    public void Instructions()
    {
        if (!isInstructionsOn)
        {
            GameObject.Find("Instructions").transform.SetParent(GameObject.Find("Background").transform);
            isInstructionsOn = true;
            return;
        }
         if(isInstructionsOn)
        {
            GameObject.Find("Instructions").transform.SetParent(GameObject.Find("Main Camera").transform);
            isInstructionsOn = false;
        }
    }
}
