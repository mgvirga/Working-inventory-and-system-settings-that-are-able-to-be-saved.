using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text : MonoBehaviour
{
    public Button done;
    public InputField ini;
    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (ini.text != null)

            PlayerPrefs.SetString("name", ini.text);
        // we use player prefs to save the players initials

    }
}
