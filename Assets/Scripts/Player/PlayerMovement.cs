using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    // A client-sided camera control script.
    // WASD or arrow keys for movement.
    // Mouse wheel for zoom.    

    [Header("References")]
    public Player Player;

    [Header("Controls")]
    public Vector2 TargetPos;
    public float LerpSpeed = 2f;
    public float BaseSpeed = 5f;

    public void Start()
	{
        TargetPos = transform.position;
	}
	
	public void Update()
	{
        // Update the target position...
        UpdateTargetPos();

        // Move towards the target position!
        LerpToTarget();
	}

    public void UpdateTargetPos()
    {
        float dt = Time.unscaledDeltaTime;

        if(InputManager.IsPressed("Move Right"))
        {
            TargetPos.x += BaseSpeed * dt;
        }
        if (InputManager.IsPressed("Move Left"))
        {
            TargetPos.x -= BaseSpeed * dt;
        }
        if (InputManager.IsPressed("Move Up"))
        {
            TargetPos.y += BaseSpeed * dt;
        }
        if (InputManager.IsPressed("Move Down"))
        {
            TargetPos.y -= BaseSpeed * dt;

        }
    }

    public void LerpToTarget()
    {
        transform.position = Vector2.Lerp(transform.position, TargetPos, Time.unscaledDeltaTime * LerpSpeed);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        float mag = 0.2f;
        Vector2 off = new Vector2(-mag, mag);
        Vector2 off2 = new Vector2(mag, -mag);
        Vector2 off3 = new Vector2(-mag, -mag);
        Vector2 off4 = new Vector2(mag, mag);
        Gizmos.DrawLine(TargetPos + off, TargetPos + off2);
        Gizmos.DrawLine(TargetPos + off3, TargetPos + off4);
    }
}