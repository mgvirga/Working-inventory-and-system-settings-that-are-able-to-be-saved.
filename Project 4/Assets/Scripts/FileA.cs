using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class FileA : MonoBehaviour
{
    public static FileA control;
    //public VideoSettings videoSettings;// = new VideoSettings();
    public AudioSource backGround;
    public AudioSource soundEffect;
    public Rect optionsRect = new Rect(0, 0, 500, 300);
    public int full_para = 1;
    public bool full_bool = true;
    public bool GUIopen = false;
    public float fov = 90.0f;
    public int VQ; // VQ is resolution
    public string vidq; //video quality
    public float Shadowt = 1f;
    public float backvol = .5f;
    public float effectvol = .5f;
    public bool background_bool = true;
    public bool effect_bool = true;

    void Awake()
    {
        backGround.Play();
        backGround.loop = true;
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
        
    }
    public void SetDefault()
    {
        SetSettings("High");
        ToggleShadows(1f);
        SetFOV(90.0f);
        //SetAA(2);
        //SetVsync(1);
        SetResolution(0, 1);
    }

    //adjusts the game video qaulity settings
    public void SetSettings(string name)
    {
        switch (name)
        {
            case "Low":
                QualitySettings.SetQualityLevel(0);
                break;
            case "Medium":
                QualitySettings.SetQualityLevel(1);
                break;
            case "High":
                QualitySettings.SetQualityLevel(2);
                break;
        }
    }

    //togles none, hard, and soft shadow settings
    public void ToggleShadows(float newToggle)
    {
        Light[] lights = GameObject.FindObjectsOfType<Light>();

        foreach (Light l in lights)
        {
            if (newToggle == 0)
            {
                l.shadows = LightShadows.None;
            }
            else if (newToggle == 1)
            {
                l.shadows = LightShadows.Hard;
            }
            else if (newToggle == 2)
            {
                l.shadows = LightShadows.Soft;
            }
        }
    }

    //adjusts the main camera's field of view
    public void SetFOV(float newFOV)
    {
        Camera.main.fieldOfView = newFOV;
    }

    //adjusts the antiAliasing settings giving smoother edges to objects
    public void SetAA(int samples)
    {
        if (samples == 0 || samples == 2 || samples == 4 || samples == 8)
        {
            QualitySettings.antiAliasing = samples;
        }

    }

    //adjusts the frame display rate
    public void SetVsync(int sync)
    {
        switch (sync)
        {
            //outputs frames as fast as possible
            case 0:
                QualitySettings.vSyncCount = 0;
                break;

            //outputs 1 frame update for every 1 screen update
            case 1:
                QualitySettings.vSyncCount = 1;
                break;

            //outputs 1 frame updates for every 2 screen updates
            case 2:
                QualitySettings.vSyncCount = 2;
                break;
        }
    }

    //adjusts the screen resolution size
    public void SetResolution(int res, int fullScreen)
    {
        bool full = ConvertToBool(fullScreen);
        switch (res)
        {
            case 0:
                Screen.SetResolution(1920, 1080, full);
                break;
            case 1:
                Screen.SetResolution(1600, 900, full);
                break;
            case 2:
                Screen.SetResolution(1280, 1024, full);
                break;
            case 3:
                Screen.SetResolution(1280, 800, full);
                break;
            case 4:
                Screen.SetResolution(640, 1080, full);
                break;

        }

    }

    public void SetFullScreen(bool full)
    {
        Screen.fullScreen = full;
    }

    //Converts int to bool
    private bool ConvertToBool(int toConvert)
    {
        if (toConvert == 0)
            return false;
        else
            return true;
    }
    void Update()
    {
        //opens and closes the options menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GUIopen = !GUIopen;
            if (effect_bool)
            {
                soundEffect.Play();
            }
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
        if (GUIopen)
        {
            Time.timeScale = 0;
            optionsRect = GUI.Window(0, optionsRect, OptionsGUI, "Options");
        }

    }
    public void OptionsGUI(int gui)
    {
        GUILayout.BeginArea(new Rect(0, 50, 800, 300));

        //Quting the application
        if (GUI.Button(new Rect(180, 200, 100, 20), "Quit Game"))
        {
            Application.Quit();
        }


        //Screen Resolution Adjustments
        GUI.Label(new Rect(25, 20, 100, 30), "Resolution:");

        if (GUI.Button(new Rect(100, 20, 75, 20), " Very High"))
        {
            VQ = 0;
            SetResolution(VQ, full_para);
        }
        if (GUI.Button(new Rect(175, 20, 75, 20), "High"))
        {
            VQ = 1;
            SetResolution(VQ, full_para);
        }
        if (GUI.Button(new Rect(250, 20, 75, 20), "Medium"))
        {
            VQ = 2;
            SetResolution(VQ, full_para);
        }
        if (GUI.Button(new Rect(325, 20, 75, 20), "Low"))
        {
            VQ = 3;
            SetResolution(VQ, full_para);
        }
        if (GUI.Button(new Rect(400, 20, 75, 20), "Very Low"))
        {
            VQ = 4;
            SetResolution(4, full_para);
        }

        //Field of View slide bar
        GUI.Label(new Rect(25, 40, 100, 30), "field of View:");
        fov = GUI.HorizontalSlider(new Rect(115, 45, 100, 30), fov, 60f, 120f);
        //Debug.Log("line 170 fov is: " + fov + "video settings is: " + videoSettings);
        SetFOV(fov);



        //full Screen Toggle
        GUI.Label(new Rect(25, 60, 100, 30), "FullScreen:");
        full_bool = GUI.Toggle(new Rect(95, 60, 100, 15), full_bool, "ON/OFF");
        if (full_bool)
        {
            full_para = 1;
        }
        else
            full_para = 0;
        SetFullScreen(full_bool);



        //Shdaow Toggling options
        GUI.Label(new Rect(25, 85, 100, 30), "Shadows:");
        if (GUI.Button(new Rect(100, 85, 75, 20), "None"))
        {
            Shadowt = 0;
            ToggleShadows(Shadowt);
        }
        if (GUI.Button(new Rect(175, 85, 75, 20), "Hard"))
        {
            Shadowt = 1;
            ToggleShadows(Shadowt);
        }
        if (GUI.Button(new Rect(250, 85, 75, 20), "Soft"))
        {
            Shadowt = 2;
            ToggleShadows(Shadowt);
        }


        //Video Quality Settings
        GUI.Label(new Rect(25, 110, 100, 30), "Video Quality:");
        if (GUI.Button(new Rect(125, 110, 75, 20), "Low"))
        {
            vidq = "Low";
            SetSettings(vidq);
        }
        if (GUI.Button(new Rect(200, 110, 75, 20), "Medium"))
        {
            vidq = "Medium";
            SetSettings(vidq);
        }
        if (GUI.Button(new Rect(275, 110, 75, 20), "High"))
        {
            vidq = "High";
            SetSettings(vidq);
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


        if (GUI.Button(new Rect(325, 200, 100, 15), "Save"))
        {
            Save();
        }
        if (GUI.Button(new Rect(325, 225, 100, 15), "Load"))
        {
            Load();
            ToggleShadows(Shadowt);
        }
        GUILayout.EndArea();
    }
    [System.Serializable]
    public class PlayerData
    {
        //public VideoSettings videoSettings;
        //public AudioSource backGround;
        //public AudioSource soundEffect;
        //public Rect optionsRect;
        public int full_para;
        public bool full_bool;
        public int VQ;
        public string vidq;
        public bool GUIopen;
        public float Shadowt;
        public float fov;
        public float backvol;
        public float effectvol;
        public bool background_bool;
        public bool effect_bool;
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.full_para = full_para;
        data.full_bool = full_bool;
        data.VQ = VQ;
        data.vidq = vidq;
        data.GUIopen = GUIopen;
        data.fov = fov;
        data.backvol = backvol;
        data.effectvol = effectvol;
        data.background_bool = background_bool;
        data.effect_bool = effect_bool;
        data.Shadowt = Shadowt;
        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            //videoSettings = data.videoSettings;
            //backGround = data.backGround;
            //soundEffect = data.soundEffect;
            //optionsRect = data.optionsRect;
            full_para = data.full_para;
            full_bool = data.full_bool;
            VQ = data.VQ;
            vidq = data.vidq;
            GUIopen = data.GUIopen;
            fov = data.fov;
            Shadowt = data.Shadowt;
            backvol = data.backvol;
            effectvol = data.effectvol;
            background_bool = data.background_bool;
            effect_bool = data.effect_bool;
        }
    }
}
