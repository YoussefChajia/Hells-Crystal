using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[RequireComponent(typeof(GameData))]

public static class SaveSystem
{
    public static void SavePlayer(Player player)
    {
        string path = Application.persistentDataPath + "/game.binar";
        GameData data = new GameData(player);
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = File.Create(path))
        {
            formatter.Serialize(stream, data);
        }
    }

    public static GameData LoadPlayer()
    {
        try
        {
            string path = Application.persistentDataPath + "/game.binar";
            if (File.Exists(path))
            {
                GameData data;
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = File.Open(path, FileMode.Open))
                {
                    data = formatter.Deserialize(stream) as GameData;
                }
                return data;
            }
            else
            {
                Debug.LogError("File missing in " + path);
                return null;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error Loading File " + e);
            return null;
        }
    }
}
