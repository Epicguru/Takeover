using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public static class GameIO
{
    private static string PersistentDataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

    public static string DataDirectory
    {
        get
        {
            return Path.Combine(PersistentDataPath, "My Games", "Takeover");
        }
    }
    public static string DefaultInputPath
    {
        get
        {
            return Path.Combine(DataDirectory, "Input", "Default.keys");
        }
    }
    public static string CurrentInputPath
    {
        get
        {
            return Path.Combine(DataDirectory, "Input", "Current.keys");
        }
    }

    public static void ObjectToFile(object o, string path)
    {
        // Make the containing directory.
        EnsureDirectory(DirectoryFromFile(path));

        // Make json from the object.
        string json = JsonConvert.SerializeObject(o, Formatting.Indented);

        // Write the json to file.
        File.WriteAllText(path, json);
    }

    public static T FileToObject<T>(string path)
    {
        // Check if it exists.
        if (!File.Exists(path))
        {
            Debug.LogError(string.Format("File not found: '{0}', cannot deserialize!", path));
            return default(T);
        }
        else
        {
            // Try to load from file and deserialize.
            try
            {
                string json = File.ReadAllText(path);
                T obj = JsonConvert.DeserializeObject<T>(json);
                return obj;
            }
            catch
            {
                Debug.LogError(string.Format("Exception when loading or deserialzing json from file '{0}'!", path));
                return default(T);
            }
        }
    }

    public static string DirectoryFromFile(string filePath)
    {
        string dir = Path.GetDirectoryName(filePath);
        return dir;
    }

    public static bool EnsureDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            return false;
        }
        else
        {
            Directory.CreateDirectory(path);
            return true;
        }
    }
}