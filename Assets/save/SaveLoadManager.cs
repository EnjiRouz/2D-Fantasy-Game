using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager 
{

    public static void SavePlayer(SystemPumping player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream streams = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        PlayerData data = new PlayerData(player);

        bf.Serialize(streams, data);
        streams.Close();
    }

    public static int[] LoadPlayer()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream streams = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(streams) as PlayerData;

            streams.Close();
            return data.stats;
        } else
        {
            Debug.LogError("File does not exst.");
            return new int[5];
        }
    }

    public static void SaveInventory(Inventory inventory)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream streams = new FileStream(Application.persistentDataPath + "/inventory.sav", FileMode.Create);

        InventoryData data = new InventoryData(inventory);

        bf.Serialize(streams, data);
        streams.Close();
    }

    public static Dictionary<ItemType, Item> LoadInventory()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream streams = new FileStream(Application.persistentDataPath + "/inventory.sav", FileMode.Open);

            InventoryData data = bf.Deserialize(streams) as InventoryData;

            streams.Close();
            return data.stats;
        }
        else
        {
            Debug.LogError("File does not exst.");
            return new Dictionary<ItemType, Item>();
        }
    }
}

[Serializable]
public class PlayerData
{
    public int[] stats;

    public PlayerData(SystemPumping player)
    {
        stats = new int[5];
        stats[0] = player.health;
        stats[1] = player.strength;
        stats[2] = player.mana;
        stats[3] = player.intelligence;
        stats[4] = player.point;
    }
}

[Serializable]
public class InventoryData
{
    public Dictionary<ItemType, Item> stats;

    public InventoryData(Inventory inventory)
    {
        stats = new Dictionary<ItemType, Item>();
        stats = inventory.items;

    }
}