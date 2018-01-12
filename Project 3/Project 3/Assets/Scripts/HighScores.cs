using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class HighScores : MonoBehaviour
{

    StreamWriter writer;
    StreamReader reader;
    StreamReader tempRead;
    public List<Score> temp = new List<Score>();

    void Start()
    {

        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
                fileChecker("HighScores.txt");                  // This is my file checker. We checked it. It works. It calls the script 3 times with each different file name
            if (i == 1)
                fileChecker("PlayerSpawn.txt");
            if (i == 2)
                fileChecker("WeaponsSpawn.txt");



            if (i == 3)
            {


                lastcheck("HighScores.txt");
                PlayerPrefs.SetInt("last", temp[4].score);

            }
        }
    }

    public void fileChecker(string name)
    {

        try
        {

            reader = new StreamReader(Application.dataPath + "/" + name);

        }
        catch (FileNotFoundException)
        {


            if (name == "HighScores.txt")
            {
                FileInfo file = new FileInfo(Application.dataPath + "/" + name);
                writer = file.CreateText();
                writer.WriteLine("JLD,38");
                writer.WriteLine("MGV, 37");
                writer.WriteLine("DJT,10");
                writer.WriteLine("BET,9");
                writer.WriteLine("AJD,5");

                writer.Close();

            }
            if (name == "PlayerSpawn.txt")
            {
                FileInfo file = new FileInfo(Application.dataPath + "/" + name);
                writer = file.CreateText();
                writer.Write("Player,20,.5,0");
                writer.Close();
            }
            if (name == "WeaponsSpawn.txt")
            {
                FileInfo file = new FileInfo(Application.dataPath + "/" + name);
                writer = file.CreateText();
                writer.WriteLine("25,.5,-2");
                writer.WriteLine("-31,.5,32");
                writer.WriteLine("3.5,.5,7");
                writer.Close();
            }
        }
        finally
        {
            if (reader != null)
                reader.Close();
            if (writer != null)
                writer.Close();
            PlayerPrefs.DeleteKey("name");
            PlayerPrefs.DeleteKey("YourScore");

        }


    }
    void lastcheck(string name)
    {
        try
        {
            tempRead = new StreamReader(Application.dataPath + "/" + name);
            string line;
            while ((line = tempRead.ReadLine()) != null)
            {
                string[] details = line.Split(',');
                Score d = new Score(details[0], int.Parse(details[1]));
                temp.Add(d);
            }
            reader.Close();
            temp.Sort();
            temp.Reverse();
        }
        catch (IOException e)
        {
            Debug.Log(e.Message);
        }
    }

}
