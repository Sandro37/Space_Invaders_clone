using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
public class SaveLoad
{
    private static string fileName = "SaveData.txt";
    private static string directoryName = "SaveData";
    public static void SaveState(SaveObject obj)
    {
        if (!DirectoryExists())
            Directory.CreateDirectory(Application.persistentDataPath + "/" + directoryName);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetSavePath());
        bf.Serialize(file, obj);
        file.Close();
            
        
    }

    public static SaveObject LoadState()
    {
        SaveObject obj = new();
        if (SaveExists())
        {
            try
            {
                BinaryFormatter bf = new();
                FileStream fileStream = File.Open(GetSavePath(), FileMode.Open);
                obj = (SaveObject) bf.Deserialize(fileStream);
                fileStream.Close();
            }catch(SerializationException)
            {
                Debug.LogWarning("Falha ao carregar save");
                obj = GetDefaultSave();
            }
        }
        else
        {
            obj = GetDefaultSave();
            SaveState(obj);
        }

        return obj;
    }

    public static bool SaveExists()
    {
        return File.Exists(GetSavePath());
    }

    public static bool DirectoryExists()
    {
        return Directory.Exists(Application.persistentDataPath + "/" + directoryName);
    }

    private static string GetSavePath()
    {
        return Application.persistentDataPath + "/" + directoryName + "/" + fileName;
    }

    private static SaveObject GetDefaultSave()
    {
        SaveObject saveObject = new(0, 0, new ShipStats(3, 3, 3, 0.5f));
        return saveObject;
    }
}
 