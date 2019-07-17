using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Player.Stuart";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();
        formatter.Serialize(stream, data);
        stream.Close();
    }
	public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/Player.Stuart";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save Not Found" + path);
            return null;
        }
    }
}
