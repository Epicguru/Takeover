using UnityEngine;
using UnityEngine.Networking;

public class NetPosSync : NetworkBehaviour
{
    // Synchronises the position of an object across all clients and also on the server.
    // The server always has the authorative position:
    // TODO implement snapping back to last known pos.

    [Header("Global")]
    [Range(0f, 60f)]
    public float UpdatesPerSecond = 20f;
    public QosType Channel = QosType.Unreliable;

    [System.Serializable]
    private class Pos
    {
        [Range(0.1f, 100f)]
        public float LerpSpeed = 25f;
        public bool Lerp = true;
    }
    [Header("Controls")]
    [SerializeField]
    private Pos _Position;

    [System.Serializable]
    private class Rot
    {
        public bool Sync = false;
        public bool Lerp = true;
        [Range(0.1f, 100f)]
        public float LerpSpeed = 25f;
    }
    [SerializeField]
    private Rot _Rotation;

    [Header("Debug")]
    [SyncVar]
    [ReadOnly]
    public Vector2 Position;

    [SyncVar]
    [ReadOnly]
    public float Angle;

    private bool dirty;

    public void Update()
    {
        if (isServer)
        {
            transform.Rotate(0f, 0f, 360f * Time.deltaTime);
        }

        if(isClient && !isServer)
        {
            LerpToPosition();
            LerpToAngle();
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
            dirty = true;
        }
        if (!dirty)
        {
            if(transform.localEulerAngles.z != Angle)
            {
                dirty = true;
            }
        }
    }

    [Server]
    public void UpdateSending()
    {
        if (dirty)
        {
            Position = transform.position;
            if (_Rotation.Sync)
            {
                Angle = transform.localEulerAngles.z;
            }
            dirty = false;
        }
    }

    [Client]
    public void LerpToPosition()
    {
        if (_Position.Lerp)
        {
            transform.position = Vector2.Lerp(transform.position, Position, Time.deltaTime * _Position.LerpSpeed);
        }
        else
        {
            transform.position = Position;
        }
    }

    [Client]
    public void LerpToAngle()
    {
        if (!_Rotation.Sync)
        {
            return;
        }

        if (_Rotation.Lerp)
        {
            Vector3 euler = transform.localEulerAngles;
            euler.z = Mathf.LerpAngle(euler.z, Angle, Time.deltaTime * _Rotation.LerpSpeed);
            transform.localEulerAngles = euler;
        }
        else
        {
            Vector3 euler = transform.localEulerAngles;
            euler.z = Angle;
            transform.localEulerAngles = euler;
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

    public override int GetNetworkChannel()
    {
        return (int)Channel;
    }
}