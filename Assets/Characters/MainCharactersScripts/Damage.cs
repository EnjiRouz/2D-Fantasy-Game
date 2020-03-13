using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{

    public float physicaldamage;   // физический урон игрока
    public float magicdamage;      // магический урон игрока
    public float damageonPlayer;   // урон по игроку
    public float damageonPlayer2;  // урон по игроку
    public int protection;         // защита

    // Update is called once per frame
    void Update()
    {
        //ищем игрока
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //получаем его инвентарь
        Inventory inventoryPlayer = player.GetComponent<Inventory>();
        //Если на нас вообще что-то надето
        if (inventoryPlayer.currentWeapon != null)  //TODO currentArmor
        {
            if (inventoryPlayer.currentWeapon.attack != 0)
            {
                Debug.Log("Получен бонус +" + inventoryPlayer.currentWeapon.attack + " к атаке");
            }
        }
        if (inventoryPlayer.currentBack != null)
        {
            if (inventoryPlayer.currentBack.protection != 0)
            {
                Debug.Log("Получен бонус +" + inventoryPlayer.currentBack.protection + " к защите");
            }
        }

    }

    // тот урон, который наносит персонаж
    public float DamagePhysical()
    {
        //ищем игрока
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //получаем его инвентарь
        Inventory inventoryPlayer = player.GetComponent<Inventory>();
        int attackBonus = 0;

        //Если на нас вообще что-то надето
        if (inventoryPlayer.currentWeapon != null)
        {
            if (inventoryPlayer.currentWeapon.attack != 0)
            {
                //получаем силу атаки текущего оружия
                attackBonus = inventoryPlayer.currentWeapon.attack;
                physicaldamage = Int32.Parse(GameObject.Find("Strength").GetComponent<Text>().text) * 10 + attackBonus;
                if (inventoryPlayer.currentWeapon.name == "Wand")
                {
                    physicaldamage += DamegeMagical();
                }
                Debug.Log("Действует бонус +" + inventoryPlayer.currentWeapon.attack + " к атаке. Общий урон: " + physicaldamage);
            }
            else
            {
                physicaldamage = Int32.Parse(GameObject.Find("Strength").GetComponent<Text>().text) * 10;
                if (inventoryPlayer.currentWeapon.name == "Wand")
                {
                    physicaldamage += DamegeMagical();
                }
                Debug.Log("Атака без бонуса. Общий урон:    " + physicaldamage);
            }

        }
        else
        {
            physicaldamage = Int32.Parse(GameObject.Find("Strength").GetComponent<Text>().text) * 10;
            Debug.Log("Атака без бонуса. Общий урон:    " + physicaldamage);
        }
        return physicaldamage;
    }

    // урон, который получает персонаж
    public float Damege()
    {
        damageonPlayer = damageonPlayer2 - Protection();
        return damageonPlayer;
    }

    // тот магический урон, который наносит персонаж
    public float DamegeMagical()
    {
        magicdamage = Int32.Parse(GameObject.Find("Intellegence").GetComponent<Text>().text) * 10;  // +(посох) + (заклинание)  
        return magicdamage;
    }

    // защита игрока 
    public float Protection()
    {
        //ищем игрока
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //получаем его инвентарь
        Inventory inventoryPlayer = player.GetComponent<Inventory>();
        int protectionBonus = 0;

        //Если на нас вообще что-то надето
        if (inventoryPlayer.currentBack != null)
        {
            if (inventoryPlayer.currentBack.protection != 0)
            {
                //получаем защиту текущего плаща если она не равна 0
                protectionBonus = inventoryPlayer.currentBack.protection;
                Debug.Log("Действует бонус +" + inventoryPlayer.currentWeapon.protection + " к защите. Общая защита: " + + protectionBonus);
            }
            else
            {
                protection = 0;   // + ("sword")
                Debug.Log("Защиты нет:    " + protectionBonus);
            }
        }
        else
        {
            protection = 0;   // + ("sword")
            Debug.Log("Защиты нет:    " + protectionBonus);
        }
        //protection = шлем+нагрудник+штаны+боты+щит
        return protection;
    }
}