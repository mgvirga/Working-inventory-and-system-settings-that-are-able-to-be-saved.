using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings_GUI : MonoBehaviour {


    public VideoSettings videoSettings;
    public Rect optionsRect = new Rect(0,0,500,500);
    public int full_para = 1;
    bool full_bool = true;
    public bool GUIopen = false;
    public float fov = 90.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.O))
        {
            GUIopen = !GUIopen;
        }
	}

    public void OnGUI()
    {
        if(GUIopen)
        optionsRect = GUI.Window(0,optionsRect, OptionsGUI, "Options");
    }

    public void OptionsGUI(int gui)
    {
        GUILayout.BeginArea(new Rect(0,50,800,800));

        //resolution adjustments depending on what button is pushed
        GUI.Label(new Rect(25,0,100,30), "Resolution");

        if (GUI.Button(new Rect(25, 20, 75, 20), " Very High"))
        {
            videoSettings.SetResolution(0, full_para);
        }
        if (GUI.Button(new Rect(100, 20, 75, 20), "High"))
        {
            videoSettings.SetResolution(1, full_para);
        }
        if (GUI.Button(new Rect(175, 20, 75, 20), "Medium"))
        {
            videoSettings.SetResolution(2, full_para);
        }
        if (GUI.Button(new Rect(250, 20, 75, 20), "Low"))
        {
            videoSettings.SetResolution(3, full_para);
        }
        if (GUI.Button(new Rect(325, 20, 75, 20), "Very Low"))
        {
            videoSettings.SetResolution(4, full_para);
        }

        //Field of View slide bar
        GUI.Label(new Rect(25, 40, 100, 30), "field of View");
        fov = GUI.HorizontalSlider(new Rect(115,45,100,30),  fov, 60f, 120f);
        videoSettings.SetFOV(fov);


        //full screen
        GUI.Label(new Rect(25, 60, 100, 30), "FullScreen");
        full_bool = GUI.Toggle(new Rect(95, 60, 100, 15), full_bool, "ON/OFF");
        if (full_bool)
        {
            full_para = 1;
        }
        else
            full_para = 0;
        videoSettings.SetFullScreen(full_bool);

        GUILayout.EndArea();
    }
}
