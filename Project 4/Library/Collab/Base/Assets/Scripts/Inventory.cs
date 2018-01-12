using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Inventory : MonoBehaviour
{
    public static Inventory controlI;
    bool message = false;
    public float start_health;
    public float current_health;
    public Text Health;
    private float speed = 30;
    public GameObject BulletSpawn;
    public bool showInventory = false;
    public Rect inventoryRect = new Rect(Screen.width / 2, Screen.height / 2, 400, 400);
    public GameObject EmptyObject;
    public int InventorySize;
    public List<GameObject> invItems;
    public List<GameObject> QuickItems;
    int item_number;
    public GameObject RedBullet;
    public GameObject BlueBullet;
    public GameObject GreenBullet;
    private float mes = 0.5f;
    private float nextmes = 6f;
    public List<KeyValuePair<int, GameObject>> items = new List<KeyValuePair<int, GameObject>>();
    public List<KeyValuePair<int, int>> itemCount = new List<KeyValuePair<int, int>>();
    public XMLManger File;
    public void Start()
    {
        start_health = 100;
        current_health = start_health;
        message = false;
        
    }
    public void delay()
    {
        nextmes = Time.time + mes;
    }
    public void InitializeInventory()
    {
        invItems = new List<GameObject>(InventorySize);
        for (int i = 0; i < InventorySize; i++)
        {

            invItems.Add(EmptyObject);
            items.Add(new KeyValuePair<int, GameObject>(i, invItems[i]));
            itemCount.Add(new KeyValuePair<int, int>(i, 0));

            if (i < QuickItems.Count)
                QuickItems[i] = invItems[i];

        }

    }

    /*
    IEnumerator Waitset(float w)
    {
        yield return new WaitForSeconds(w);
        message = false;
    }
    */
    public void AddFromFile(string itemname, int count)
    {
        GameObject temp = new GameObject();
        temp.name = itemname;
        AddToInventory(count, temp);
    }

    public void SaveToFile()
    {
        for(int i = 0; i < invItems.Count; i++)
        {
            if(invItems[i].name != "Empty")
            {
                Itementry temp = new Itementry();
                temp.itemName = invItems[i].name;
                temp.quantity = itemCount[i].Value;
                File.itemDB.storage.Add(temp);
            }
        }
    }

    void AddToInventory(int HowMany, GameObject NewItem)
    {
        for (int i = 0; i < invItems.Count; i++)
        {
            if (invItems[i].name != "Empty")
            {
                if (invItems[i].name == NewItem.name)
                {
                    int val = itemCount[i].Value + HowMany;
                    itemCount[i] = new KeyValuePair<int, int>(itemCount[i].Key, val);
                    break;
                }
            }

            else
            {
                int val = itemCount[i].Value + HowMany;
                invItems[i] = NewItem;
                items[i] = new KeyValuePair<int, GameObject>(i, NewItem);
                itemCount[i] = new KeyValuePair<int, int>(i, val);
                item_number++;
                break;
            }

            if (i < QuickItems.Count)
                QuickItems[i] = invItems[i];
        }
    }

    void RemoveFromInventory(int HowMany, GameObject Item)
    {
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (invItems[i].name == Item.name)
                {
                    int val = itemCount[i].Value - HowMany;
                    itemCount[i] = new KeyValuePair<int, int>(itemCount[i].Key, val);
                }

                if (itemCount[i].Value <= 0)
                {

                    invItems.RemoveAt(i);
                    itemCount.RemoveAt(i);
                    invItems.Add(EmptyObject);
                    itemCount.Add(new KeyValuePair<int, int>(itemCount.Count, 0));
                   
                }

            }
        }
    }

    public void takeDamage(float damage)
    {
        current_health -= damage;
    }

    public virtual void Use()
    {


    }

    public virtual void use_item(int i)
    {

        if (invItems[i].name.Equals("Red_Bullet"))
        {
            GameObject instProjectile = Instantiate(RedBullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation) as GameObject;
            instProjectile.GetComponent<Rigidbody>().velocity = BulletSpawn.transform.TransformDirection(new Vector3(-speed, 0, 0));
            RemoveFromInventory(1, invItems[i]);
        }
        else if (invItems[i].name.Equals("Green_Bullet"))
        {
            GameObject instProjectile = Instantiate(GreenBullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation) as GameObject;
            instProjectile.GetComponent<Rigidbody>().velocity = BulletSpawn.transform.TransformDirection(new Vector3(-speed, 0, 0));
            RemoveFromInventory(1, invItems[i]);
        }
        else if (invItems[i].name.Equals("Blue_Bullet"))
        {
            GameObject instProjectile = Instantiate(BlueBullet, BulletSpawn.transform.position, BulletSpawn.transform.rotation) as GameObject;
            instProjectile.GetComponent<Rigidbody>().velocity = BulletSpawn.transform.TransformDirection(new Vector3(-speed, 0, 0));
            RemoveFromInventory(1, invItems[i]);
        }
        else if (invItems[i].name.Equals("Health 10"))
        {
            if (current_health < 100)
            {
                current_health += 10;
                //Health.text = current_health.ToString();
                RemoveFromInventory(1, invItems[i]);
            }

        }
        else if (invItems[i].name.Equals("Health 20"))
        {
            if (current_health <= 80)
            {
                current_health += 20;
                //Health.text = current_health.ToString();
                RemoveFromInventory(1, invItems[i]);
            }
        }
        else if (invItems[i].name.Equals("Health 30"))
        {
            if (current_health <= 70)
            {
                current_health += 30;
                //Health.text = current_health.ToString();
                RemoveFromInventory(1, invItems[i]);
            }

        }
        else
        {
            RemoveFromInventory(1, invItems[i]);
        }
    }


    void SetQuickItem(GameObject NewItem, int QuickInput)
    {
        if (QuickItems[QuickInput].name != NewItem.name)
            if (QuickInput < QuickItems.Count)
                QuickItems[QuickInput] = NewItem;
    }

    void Awake()
    {
        InitializeInventory();

        if (controlI == null)
        {
            DontDestroyOnLoad(gameObject);
            controlI = this;
        }
        else if (controlI != this)
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        Health.text = current_health.ToString();

        if (Input.GetKeyDown(KeyCode.I))
        {
            showInventory = (showInventory) ? false : true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            use_item(0);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            use_item(1);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            use_item(2);


        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            use_item(3);
        }

    }

    void OnTriggerEnter(Collider col)
    {
        int invAmount = 1;
        if ((col.gameObject.tag.Equals("InventoryItem") && (invItems[11].name == "Empty")))
        {
            if (col.gameObject.name.Equals("Red_Bullet"))
            {
                invAmount = col.gameObject.GetComponent<GroupInventoryAmount>().amount;
            }
            if (col.gameObject.name.Equals("Green_Bullet"))
            {
                invAmount = col.gameObject.GetComponent<GroupInventoryAmount>().amount;
            }
            if (col.gameObject.name.Equals("Blue_Bullet"))
            {
                invAmount = col.gameObject.GetComponent<GroupInventoryAmount>().amount;
            }
            AddToInventory(invAmount, col.gameObject);
            col.gameObject.SetActive(false);

        }
        if (col.tag == "Enemy")
        {
            current_health -= 10;
        }
        if (col.tag == "Enemy_Bullet")
        {
            current_health -= 10;
            col.gameObject.SetActive(false);
        }
        if (col.tag == "Player")
        {
            current_health -= 10;
        }
        if ((invItems[11].name != "Empty"))
        {

            message = true;

        }
        else
        {
            /*
            IEnumerator courutine;
            courutine = Waitset(20);
            StartCoroutine(courutine);
            */

        }
    }

    void OnGUI()
    {
        if (showInventory)
        {
            inventoryRect = GUI.Window(0, inventoryRect, InventoryGUI, "Inventory");
        }

        for (int i = 0; i < 4; i++)
        {
            if (GUILayout.Button(itemCount[i].Value.ToString() + " " + invItems[i].name))
            {
                use_item(i);
            }

        }
        if (message)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 300, 200), " Cant add more than 12 items");

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

        }
       
    }

    void InventoryGUI(int ID)
    {
        GUI.DragWindow(new Rect(0, 0, 1000, 20)); //makes dragable window
        GUILayout.BeginArea(new Rect(0, 50, 400, 400));

        
        GUILayout.BeginHorizontal();
        for (int i = 0; i < 6; i++)
        {
            GUILayout.Button(itemCount[i].Value.ToString() + " " + invItems[i].name, GUILayout.Height(25));
            
            if ((i + 1) % 3 == 0)
            {

                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
            }
        }
        GUILayout.EndHorizontal();
        if (itemCount[6].Value != 0)
        {
            GUILayout.BeginHorizontal();
            for (int i = 6; i < item_number; i++)
            {
                GUILayout.Button(itemCount[i].Value.ToString() + " " + invItems[i].name, GUILayout.Height(25));

                if ((i + 1) % 3 == 0)
                {
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();

                }
            }
            GUILayout.EndHorizontal();

        }
       
        if (GUI.Button(new Rect(50, 175, 100, 15), "Save"))
        {
            Save();
        }
        if (GUI.Button(new Rect(250, 175, 100, 15), "Load"))
        {
            Load();
        }
     
        GUILayout.EndArea();
    }
    public void Save()
    {
        File.SaveItems();
    }
    public void Load()
    {
        File.LoadItems();
    }
    /*
    [System.Serializable]
    public class InventoryItemsIO
    {
       
        public List<KeyValuePair<int, GameObject>> items = new List<KeyValuePair<int, GameObject>>();
        public List<KeyValuePair<int, int>> itemCount = new List<KeyValuePair<int, int>>();
        public bool showInventory;
        
        public List<GameObject> invItems;
        //public List<GameObject> QuickItems;
        
    }
    public void Save()
    {
        //StreamWriter
        //file.Close();
        StreamWriter SW = new StreamWriter(Application.persistentDataPath + "/playerItem.dat");
        SW.Close();
    }
    public void Load()
    {
        
        if (File.Exists(Application.persistentDataPath + "/playerItem.dat"))
        {
            StreamReader SR = new StreamReader(Application.persistentDataPath + "/playerItem.dat");
            char delims = ',';
            string nextline;
            List<float> quant = new List<float>();
            nextline = SR.ReadLine();
            string[] storage;
            while(nextline != null)
            {
                storage = nextline.Split(delims);
                quant.Add(float.Parse(storage[1]));
               
            }
            SR.Close();
        }
        */
}
    
//}