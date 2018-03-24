using System.Collections.Generic;
using UnityEngine;

public class InputManagerGameObject : MonoBehaviour
{
    [SerializeField]
    private List<NameKeyBinding> Bindings;

    public void SaveToFile()
    {
        Dictionary<string, List<KeyCode>> dic = new Dictionary<string, List<KeyCode>>();
        foreach (var item in Bindings)
        {
            dic.Add(item.Name, item.Keys);
        }
        GameIO.ObjectToFile(dic, GameIO.DefaultInputPath);
        Debug.Log(string.Format("Saved {0} inputs to '{1}'", Bindings.Count, GameIO.DefaultInputPath));
    }
}

[System.Serializable]
public class NameKeyBinding
{
    public string Name;
    public List<KeyCode> Keys;
}