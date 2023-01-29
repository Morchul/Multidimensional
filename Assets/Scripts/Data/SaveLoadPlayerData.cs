using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadPlayerData
{
    private static readonly string path = Application.persistentDataPath + "/PlayerData.txt";

    public static void Save(PlayerData playerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = new FileStream(path, FileMode.Create);

        formatter.Serialize(file, playerData);
        file.Close();
    }

    public static PlayerData Load()
    {
        Debug.Log("Path: " + path); 

        if (!File.Exists(path)) return null;

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = new FileStream(path, FileMode.Open);

        PlayerData loadedData = (PlayerData)formatter.Deserialize(file);
        file.Close();

        return loadedData;
    }
}
