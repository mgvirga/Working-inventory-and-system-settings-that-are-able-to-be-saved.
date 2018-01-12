using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //public Text error;

    int pressed;
    bool message = false;
    public float start_health;
    public float current_health;
    public Image Health;
    //private float lastTime;
    private float speed = 50;
    public GameObject BulletSpawn;
    //public GameObject Projectile;
    bool showInventory = false;
    public Rect inventoryRect = new Rect(Screen.width / 2, Screen.height / 2, 400, 400);
    public GameObject EmptyObject;
    public int InventorySize;
    public List<GameObject> invItems;
    public List<GameObject> QuickItems;
    int item_number;
    //GameObject Red_Bullet;
    public GameObject RedBullet;
    public GameObject BlueBullet;
    public GameObject GreenBullet;
    int temp;
    string tem2;
    private float mes = 0.5f;
    private float nextmes = 6f;
    List<KeyValuePair<int, GameObject>> items = new List<KeyValuePair<int, GameObject>>();

    List<KeyValuePair<int, int>> itemCount = new List<KeyValuePair<int, int>>();
    bool rightMouse = false;
    bool leftMouse = false;

    private void Start()
    {
        start_health = 100;
        current_health = start_health;
        message = false;
        
    }
    public void delay()
    {
        nextmes = Time.time + mes;
    }
    void InitializeInventory()
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
    IEnumerator Waitset(float w)
    {
        yield return new WaitForSeconds(w);
        message = false;
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

    public virtual void Use()
    {


    }

    public virtual void use_item(int i)
    {

        if (invItems[i].name.Equals("Red_Bullet"))
        {
            //QuickItems[i].gameObject.GetComponent<Shootr>().Shootn();
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
                Health.fillAmount = current_health / start_health;
                RemoveFromInventory(1, invItems[i]);
            }

        }
        else if (invItems[i].name.Equals("Health 20"))
        {
            if (current_health <= 80)
            {
                current_health += 20;
                Health.fillAmount = current_health / start_health;
                RemoveFromInventory(1, invItems[i]);
            }
        }
        else if (invItems[i].name.Equals("Health 30"))
        {
            if (current_health <= 70)
            {
                current_health += 30;
                Health.fillAmount = current_health / start_health;
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
    }

    void Update()
    {
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
            //Debug.Log("Collect one " + col.gameObject.name);
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
            //lastTime = Time.time;
            current_health -= 10;
            Health.fillAmount = current_health / start_health;
        }
        if (col.tag == "Enemy_Bullet")
        {
            current_health -= 10;
            Health.fillAmount = current_health / start_health;
        }
        if (col.tag == "Player")
        {
            current_health -= 10;
            Health.fillAmount = current_health / start_health;
        }
        if ((invItems[11].name != "Empty"))
        {

            message = true;

        }
        else
        {
            IEnumerator courutine;
            courutine = Waitset(20);
            StartCoroutine(courutine);

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
        int x = 0;
        int y = 0;
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
        if (itemCount[6].Value != 0) //checks if there is a value in the list
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
        GUILayout.EndArea();
    }
}