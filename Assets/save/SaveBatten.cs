using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBatten : MonoBehaviour {

    public void Save()
    {
        FindObjectOfType<SystemPumping>().Save();
       // SaveEXML<Inventory_Data>.save(SaveInventory.i._data, Application.persistentDataPath + "/data.xml");
       FindObjectOfType<Inventory>().Save();
    }

    public void Load()
    {
        FindObjectOfType<SystemPumping>().Load();
       // SaveInventory.i._data = SaveEXML<Inventory_Data>.load(new Inventory_Data(), Application.persistentDataPath + "/data.xml");
       FindObjectOfType<Inventory>().Load();
    }
}
