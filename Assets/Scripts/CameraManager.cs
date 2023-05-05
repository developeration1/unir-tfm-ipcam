using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] Camera _selectedCamera;

    public delegate void OnSelectCameraEvent();
    public OnSelectCameraEvent onSelectCamera;

    public Camera SelectedCamera
    {
        get => _selectedCamera;
        set
        {
            _selectedCamera = value;
            onSelectCamera.Invoke();
        } 
    }
}
