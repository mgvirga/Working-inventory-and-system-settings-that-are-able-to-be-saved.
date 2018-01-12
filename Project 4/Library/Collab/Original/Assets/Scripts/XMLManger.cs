using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.UI;

public class XMLManger : MonoBehaviour {

    //public Inventory inventory;
    public static XMLManger ins;

    private void Awake()
    {
        ins = this;

    }

    public ItemDatabase itemDB;
    public void SaveItems()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "item_data.xml", FileMode.Create);
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
        //public Inventory inventory;
        
        public List<Itementry> storage = new List<Itementry>();
        
    }
    
