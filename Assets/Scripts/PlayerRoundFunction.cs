using UnityEngine;
using System.Collections;

public class PlayerRoundFunction : MonoBehaviour {


    public GameObject playerround1;
    public GameObject playerround2;
    public bool isPlayOneTurn = true;

    void CallPlayerOne()
    {
        playerround1.SetActive(true);
        isPlayOneTurn = false;
    }
    void CallPlayerTwo()
    {
        playerround2.SetActive(true);
        isPlayOneTurn = true;
    }

    public void WhoseRound()
    {
        if (isPlayOneTurn)
        {
            CallPlayerOne();
        } else
        {
            CallPlayerTwo();
        }

    }


}
