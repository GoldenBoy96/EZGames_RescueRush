using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonHelper<T>
{

    public static void SaveFile(T data, string fileName)
    {
        string saverData = JsonUtility.ToJson(data);
        string saverPath = GetAbsoluteFilePath(fileName);
        string directoryPath = Path.GetDirectoryName(saverPath);
        if (!Directory.Exists(directoryPath))
        {
              Directory.CreateDirectory(directoryPath);
            Debug.Log(directoryPath);
        }
        File.WriteAllText(saverPath, saverData);

        Debug.Log("Save file created at: " + saverPath);
    }

    public static T LoadFile(string fileName)
    {
        T data = default;
        string saverPath = GetAbsoluteFilePath(fileName);
        if (File.Exists(saverPath))
        {
            string loadData = File.ReadAllText(saverPath);
            data = JsonUtility.FromJson<T>(loadData);
            Debug.Log("Load data complete!");
        }
        else
        {
            Debug.Log("There is no save files to load!");
        }

        return data;

    }

    private static string GetAbsoluteFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName + ".txt";
    }
}
