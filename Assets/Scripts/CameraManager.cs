using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] RotatoryCamera _selectedCamera;

    public delegate void OnSelectCameraEvent();
    public OnSelectCameraEvent onSelectCamera;

    public RotatoryCamera SelectedCamera
    {
        get => _selectedCamera;
        set
        {
            _selectedCamera = value; 
            onSelectCamera.Invoke();
        } 
    }
}
