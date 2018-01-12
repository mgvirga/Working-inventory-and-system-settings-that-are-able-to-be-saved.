using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings_GUI : MonoBehaviour {


    public VideoSettings videoSettings;
    public AudioSource backGround;
    public AudioSource soundEffect;
    public Rect optionsRect = new Rect(0,0,500,300);
    public int full_para = 1;
    bool full_bool = true;
    public bool GUIopen = false;
    public float fov = 90.0f;
    public float backvol = .5f;
    public float effectvol = .5f;
    private bool background_bool = true;
    private bool effect_bool = true;


	// Use this for initialization
	void Start ()
    {
        backGround.Play();
        backGround.loop = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //opens and closes the options menu
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            GUIopen = !GUIopen;
            if(effect_bool)
            {
                soundEffect.Play();
            }
            
        }

        //pauses game if settings menu open
        if(GUIopen)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        //plays background music if toggled on
        if (!background_bool)
        {
            backGround.Pause();
        }
        else
        {
            backGround.UnPause();
        }
    }

    public void OnGUI()
    {
        if(GUIopen)
        {
            optionsRect = GUI.Window(0, optionsRect, OptionsGUI, "Options");
        }

    }

    public void OptionsGUI(int gui)
    {
        GUILayout.BeginArea(new Rect(0,50,800,300));

        //Quting the application
        if(GUI.Button(new Rect(180,200,100,20), "Quit Game"))
        {
            Application.Quit();
        }


        //Screen Resolution Adjustments
        GUI.Label(new Rect(25,20,100,30), "Resolution:");

        if (GUI.Button(new Rect(100, 20, 75, 20), " Very High"))
        {
            videoSettings.SetResolution(0, full_para);
        }
        if (GUI.Button(new Rect(175, 20, 75, 20), "High"))
        {
            videoSettings.SetResolution(1, full_para);
        }
        if (GUI.Button(new Rect(250, 20, 75, 20), "Medium"))
        {
            videoSettings.SetResolution(2, full_para);
        }
        if (GUI.Button(new Rect(325, 20, 75, 20), "Low"))
        {
            videoSettings.SetResolution(3, full_para);
        }
        if (GUI.Button(new Rect(400, 20, 75, 20), "Very Low"))
        {
            videoSettings.SetResolution(4, full_para);
        }


        //Field of View slide bar
        GUI.Label(new Rect(25, 40, 100, 30), "field of View:");
        fov = GUI.HorizontalSlider(new Rect(115,45,100,30),  fov, 60f, 120f);
        videoSettings.SetFOV(fov);


        //full Screen Toggle
        GUI.Label(new Rect(25, 60, 100, 30), "FullScreen:");
        full_bool = GUI.Toggle(new Rect(95, 60, 100, 15), full_bool, "ON/OFF");
        if (full_bool)
        {
            full_para = 1;
        }
        else
            full_para = 0;
        videoSettings.SetFullScreen(full_bool);



        //Shdaow Toggling options
        GUI.Label(new Rect(25, 85, 100, 30), "Shadows:");
        if (GUI.Button(new Rect(100, 85, 75, 20), "None"))
        {
            videoSettings.ToggleShadows(0);
        }
        if (GUI.Button(new Rect(175, 85, 75, 20), "Hard"))
        {
            videoSettings.ToggleShadows(1);
        }
        if (GUI.Button(new Rect(250, 85, 75, 20), "Soft"))
        {
            videoSettings.ToggleShadows(2);
        }


        //Video Quality Settings
        GUI.Label(new Rect(25,110,100,30), "Video Quality:");
        if (GUI.Button(new Rect(125, 110, 75, 20), "Low"))
        {
            videoSettings.SetSettings("Low");
        }
        if (GUI.Button(new Rect(200, 110, 75, 20), "Medium"))
        {
            videoSettings.SetSettings("Medium");
        }
        if (GUI.Button(new Rect(275, 110, 75, 20), "High"))
        {
            videoSettings.SetSettings("High");
        }

        //background music volume slider
        GUI.Label(new Rect(25, 135, 120, 30), "Background music:");
        backvol = GUI.HorizontalSlider(new Rect(140, 141, 100, 30), backvol, 0f, 1.0f);
        backGround.volume = backvol;

        //toggle background music on and off
        background_bool = GUI.Toggle(new Rect(245, 135, 100, 15), background_bool, "ON/OFF");

        
        //sound effect volume slider
        GUI.Label(new Rect(25, 160, 120, 30), "Sound effects:");
        effectvol = GUI.HorizontalSlider(new Rect(140, 166, 100, 30), effectvol, 0f, 1.0f);
        soundEffect.volume = effectvol;

        //toggle sound effects on and off
        effect_bool = GUI.Toggle(new Rect(245, 160, 100, 15), effect_bool, "ON/OFF");
        GUILayout.EndArea();
        
    }
}
