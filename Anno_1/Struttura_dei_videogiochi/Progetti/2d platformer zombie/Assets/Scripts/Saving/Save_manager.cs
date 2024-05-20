using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Handles the save management, accepting any type of class
// Forcing only class accepted with the where condition
public static class Save_manager<T> where T : class
{
    // path is equal to the default Unity save path + our filename
    private static string GetSavePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    public static void Save_Data(T data, string fileName)
    {
        Debug.Log(fileName);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetSavePath(fileName), json);
    }

    public static T Load_Data(string fileName)
    {
        string path = GetSavePath(fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }
        return null; // or create new data
    }

}
