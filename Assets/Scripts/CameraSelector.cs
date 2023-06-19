using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class CameraSelector : MonoBehaviour
{
    [SerializeField] private Material cameraRenderTextureMaterial;
    [SerializeField] private string mainTextureProperty;
    [SerializeField] private RotatoryCamera rotatoryCamera;
    private Image image;
    private Button button;

    void Start()
    {
        image = GetComponent<Image>();
        image.material = rotatoryCamera.EndMaterial;
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectCamera);
    }

    private void SelectCamera()
    {
        CameraManager.Instance.SelectedCamera.SetAudio(false);
        rotatoryCamera.SetAudio(true);
        CameraManager.Instance.SelectedCamera = rotatoryCamera;
    }
}
