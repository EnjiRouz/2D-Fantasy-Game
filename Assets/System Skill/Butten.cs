using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butten : MonoBehaviour
{
    int ButtenNumber;
    public void Health()
    {
        ButtenNumber = 1;
        FindObjectOfType<SystemPumping>().PumpingSkills(ButtenNumber); 
    }

    public void Strength()
    {
        ButtenNumber = 2;
        FindObjectOfType<SystemPumping>().PumpingSkills(ButtenNumber); 
    }

    public void Magic()
    {
        ButtenNumber = 3;
        FindObjectOfType<SystemPumping>().PumpingSkills(ButtenNumber); 
    }

    public void Intellegnce()
    {
        ButtenNumber = 4;
        FindObjectOfType<SystemPumping>().PumpingSkills(ButtenNumber);
    }

    public void Exit()
    {
        FindObjectOfType<WindowPumping>().ConcealWindow();
    }
}