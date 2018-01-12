using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.UI;

public class XMLManger : MonoBehaviour
{
    public Inventory inventory;
    public static XMLManger ins;

    void Awake()
    {
        ins = this;
    }

    public ItemDatabase itemDB;
    

    public void LoadItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/item_data.xml", FileMode.Open);
        itemDB = serializer.Deserialize(stream) as ItemDatabase;
        stream.Close();

        for (int i = 0; i < inventory.invItems.Count; i++)
        {
            inventory.invItems[i].name = "Empty";
            inventory.itemCount[i] = new KeyValuePair<int, int>(i, 0);
        }
        foreach (Itementry item in itemDB.storage)
        {
            inventory.AddFromFile(item.itemName, item.quantity);
        }
    }

    public void SaveItems()
    {
        itemDB.storage.Clear();
        inventory.SaveToFile();
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/item_data.xml", FileMode.Create);
        serializer.Serialize(stream, itemDB);
        stream.Close();
    }
}

[System.Serializable]
public class Itementry
{
    public string itemName;
    public int quantity;
}

[System.Serializable]
public class ItemDatabase
{     
    public List<Itementry> storage = new List<Itementry>();        
}

