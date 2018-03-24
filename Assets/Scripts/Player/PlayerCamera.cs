using UnityEngine;
using UnityEngine.Networking;

public class PlayerCamera : NetworkBehaviour
{
    public Camera Camera;
    public bool CameraFollows = true;

    public void Start()
    {
        if (!isLocalPlayer)
            return;

        FindCamera();
    }

    public void FindCamera()
    {
        // Find the main camera.
        MainCam cam = GameObject.FindObjectOfType<MainCam>();
        if (cam != null)
        {
            Camera = cam.Camera;
        }
    }

    public void Update()
    {
        if (CameraFollows)
        {
            if(Camera != null)
            {
                Camera.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.transform.position.z);
            }
        }
    }
}