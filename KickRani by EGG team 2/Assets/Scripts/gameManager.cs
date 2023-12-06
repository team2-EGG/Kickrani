using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public bool wearingHelmet = false;
    public int stamina = 100;


    public void gameOver()
    {
        
    }

    public void getItemHelmet()
    {
        wearingHelmet = true;
    }

    public void getItemBattery()
    {
        
    }

    public void getItemStar()
    {

    }

    public void collideToSmallObstacle()
    {
        if (wearingHelmet)
        {

        }
        else
        {
            gameOver();
        }
    }

    public void collideToBigObstacle()
    {
        gameOver();
    }




}
