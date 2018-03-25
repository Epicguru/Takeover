using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(NetPosSync))]
public class Pawn : NetworkBehaviour
{
    // A pawn is an intelligent creature that can navigate and interact with the world.
    // Has support for health, pathfinding, that kind of stuff.

    public Health Health
    {
        get
        {
            if(_Health == null)
            {
                _Health = GetComponent<Health>();
            }
            return _Health;
        }
    }
    private Health _Health;

    public NetPosSync NetPosSync
    {
        get
        {
            if (_NetPosSync == null)
            {
                _NetPosSync = GetComponent<NetPosSync>();
            }
            return _NetPosSync;
        }
    }
    private NetPosSync _NetPosSync;
}