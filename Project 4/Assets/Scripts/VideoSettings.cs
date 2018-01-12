using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[System.Serializable]
public class VideoSettings : MonoBehaviour
{
    //deafult settings for game
    public void SetDefault()
    {
        SetSettings("High");
        ToggleShadows(1);
        SetFOV(90.0f);
        SetAA(2);
        SetVsync(1);
        SetResolution(0,1);
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
    public void ToggleShadows(int newToggle)
    {
        Light[] lights = GameObject.FindObjectsOfType<Light>();

        foreach(Light l in lights)
        {
           if(newToggle == 0)
           {
                l.shadows = LightShadows.None;
           }
           else if(newToggle == 1)
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
        if(samples == 0 || samples == 2 || samples == 4 || samples == 8)
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
}
