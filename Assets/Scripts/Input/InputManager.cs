using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    private static Dictionary<string, List<KeyCode>> bindings;

    public static void Init()
    {
        if(bindings != null || bindings.Count != 0)
        {
            return;
        }

        // Load default and current key bindings and merge...
        LoadAndMerge();
    }

    private static void LoadAndMerge()
    {
        // Load default keys from file...
        var defaultKeys = GameIO.FileToObject<Dictionary<string, List<KeyCode>>>(GameIO.DefaultInputPath);
        int defNum = defaultKeys.Count;

        // Load current keys from file...
        var currentKeys = GameIO.FileToObject<Dictionary<string, List<KeyCode>>>(GameIO.DefaultInputPath);

        // Merge current keys into default keys, but current keys have priority.
        foreach (var pair in currentKeys)
        {
            if (defaultKeys.ContainsKey(pair.Key))
            {
                defaultKeys[pair.Key] = pair.Value;
            }
            else
            {
                defaultKeys.Add(pair.Key, pair.Value);
            }
        }

        bindings = defaultKeys;
        Debug.Log(string.Format("Loaded {0} default key bindings, {1} current key bindings, and merged to create {2} bindings.", defNum, currentKeys.Count, defaultKeys.Count));
    }
}