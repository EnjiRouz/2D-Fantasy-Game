using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class СonclusionParameters : MonoBehaviour {

    //вывод на экран виджета характеристики героя
    public void DataOutput(int health, int strength, int mana, int intelligence, int point)
    {
        GameObject.Find("Health").GetComponent<Text>().text = health.ToString();
        GameObject.Find("Strength").GetComponent<Text>().text = strength.ToString();
        GameObject.Find("Magic").GetComponent<Text>().text = mana.ToString();
        GameObject.Find("Intellegence").GetComponent<Text>().text = intelligence.ToString();
        GameObject.Find("Points_quantity").GetComponent<Text>().text = point.ToString();
    }
    public void DataOutputExp(int endexp)
    {
        GameObject.Find("Exp").GetComponent<Text>().text = endexp.ToString();
    }
}
