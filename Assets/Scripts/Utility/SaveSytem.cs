
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

public static class SaveSytem
{

    //saving PlayerData
    public static void SavePlayerData(ScoreManager scoreManager, string sName)
    {

        //create a path
        string sPath = Application.persistentDataPath + "/HighScore.Dat";

        //binary formatter used to serialize data
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(sPath) && LoadPlayerData().iHighScore > scoreManager.GetScore())
        {
            Debug.Log("Current score is not greater than the highscore");
            return;
        }


        //create a file stream
        FileStream stream = new FileStream(sPath, FileMode.Create);



        //create a new player data 
        PlayerData data = new PlayerData(scoreManager, sName);
        //serialize data to binary
        formatter.Serialize(stream, data);

        //close stream
        stream.Close();
    }

    //loading PlayerData
    public static PlayerData LoadPlayerData()
    {
        string sPath = Application.persistentDataPath + "/HighScore.Dat";

        if (File.Exists(sPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(sPath, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Highscore.Dat not found in: " + sPath);
            return null;

        }


    }
};
