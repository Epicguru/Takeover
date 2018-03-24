using UnityEngine;
using UnityEngine.Networking;

public class NetPosSync : NetworkBehaviour
{
    // Synchronises the position of an object across all clients and also on the server.
    // The server always has the authorative position:
    // TODO implement snapping back to last known pos.

    [Range(0f, 60f)]
    public float UpdatesPerSecond = 20f;
    [Range(0.1f, 100f)]
    public float LerpSpeed = 25f;
    public bool Lerp = true;

    [SyncVar]
    [ReadOnly]
    public Vector2 Position;

    [ReadOnly]
    public bool Dirty;

    public void Update()
    {
        if(isClient && !isServer)
        {
            LerpToPosition();
        }
        else
        {
            UpdateDirty();
            UpdateSending();
        }
    }

    [Server]
    public void UpdateDirty()
    {
        if((Vector2)transform.position != Position)
        {
            Dirty = true;
        }
    }

    [Server]
    public void UpdateSending()
    {
        if (Dirty)
        {
            Position = transform.position;
            Dirty = false;
        }
    }

    [Client]
    public void LerpToPosition()
    {
        if (Lerp)
        {
            transform.position = Vector2.Lerp(transform.position, Position, Time.deltaTime * LerpSpeed);
        }
        else
        {
            transform.position = Position;
        }
    }

    public override float GetNetworkSendInterval()
    {
        if(UpdatesPerSecond == 0f)
        {
            return 0f;
        }
        return 1f / UpdatesPerSecond;
    }
}