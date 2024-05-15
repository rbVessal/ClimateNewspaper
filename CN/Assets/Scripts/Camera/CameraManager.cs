using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CurrentCamera
{
    Office,
    Computer,
    Bulletin, 
    Town
}
public class CameraManager : MonoBehaviour
{
    public CurrentCamera camera;
    public List<CinemachineVirtualCamera> cameras;

    public CinemachineVirtualCamera currentCamera;

    // Start is called before the first frame update
    void Start()
    {
        currentCamera = cameras[0];
        SetCamPriorities();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug Only
        switch (camera)
        {
            case CurrentCamera.Office:
                SetCamera(cameras[0]);
                break;

            case CurrentCamera.Computer:
                SetCamera(cameras[1]);
                break;
            case CurrentCamera.Bulletin:
                SetCamera(cameras[2]);
                break;
            case CurrentCamera.Town:
                SetCamera(cameras[3]);
                break;
            default: Debug.Log("No camera selected"); break;
        }
    }

    void GetAllCameras()
    {
    }

    void SetCamera(CinemachineVirtualCamera cam)
    {
        if (cam != currentCamera)
        {
            currentCamera = cam;
            SetCamPriorities();
        }

        
    }

    void SetCamPriorities()
    {
        foreach (CinemachineVirtualCamera cam in cameras)
        {
            if (cam == currentCamera)
            {
                cam.Priority = 10;
            }
            else
            {
                cam.Priority = 5;
            }
        }
    }
}
