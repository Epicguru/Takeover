using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public static Player Local;
    public static bool LocalPlayerActive
    {
        get
        {
            return Local != null;
        }
    }

    public PlayerMovement Movement;
    public PlayerCamera Camera;

    public void Awake()
    {
        if (isLocalPlayer)
        {
            Local = this;
        }
    }

    public void OnDestroy()
    {
        if (isLocalPlayer)
        {
            Local = null;
        }
    }
}