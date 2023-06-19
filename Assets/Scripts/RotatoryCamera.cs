using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(AudioListener))]
public class RotatoryCamera : MonoBehaviour
{
    [SerializeField] private Transform joint;
    [SerializeField] private Camera original;
    [SerializeField] private float minRotationRadian;
    [SerializeField] private float maxRotationRadian;
    public float anglesPerFixedFrame;
    [SerializeField] private float minZoom = 62;
    [SerializeField] private float maxZoom = 25;
    public float zoomPerFixedFrame;
    [SerializeField] private Material endMaterial; 

    private Camera cam;
    private AudioListener audioListener;

    public Camera Cam => cam;

    public Material EndMaterial => endMaterial; 

    private void Awake()
    {
        cam = GetComponent<Camera>();
        audioListener = GetComponent<AudioListener>();
    }

    public void Move(float direction)
    {
        direction = Mathf.Clamp(direction, - 1, 1);
        //print(joint.rotation.y);
        if (direction > 0 && joint.rotation.y < maxRotationRadian)
        {
            joint.Rotate(new Vector3(0, Time.fixedDeltaTime * anglesPerFixedFrame));
            return;
        }
        if (direction < 0 && joint.rotation.y > minRotationRadian)
        {
            joint.Rotate(new Vector3(0, -Time.fixedDeltaTime * anglesPerFixedFrame));
        }
    }

    public void Zoom(float direction)
    {
        direction = Mathf.Clamp(direction, -1, 1);
        print(direction);
        if (direction < 0 && original.fieldOfView < minZoom)
        {
            original.fieldOfView += zoomPerFixedFrame;
            return;
        }
        if (direction > 0 && original.fieldOfView > maxZoom)
        {
            original.fieldOfView -= zoomPerFixedFrame;
        }
    }

    public void SetAudio(bool enable)
    {
        audioListener.enabled = enable;
    }
}
