using System.Collections.Generic;
using UnityEngine;

public class InputManagerGameObject : MonoBehaviour
{
    [SerializeField]
    private List<NameKeyBinding> Bindings;

    public void SaveToFile()
    {

    }
}

[System.Serializable]
internal class NameKeyBinding
{
    public string Name;
    public List<KeyCode> Keys;
}