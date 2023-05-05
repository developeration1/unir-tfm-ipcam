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
    [SerializeField] private Camera _camera;
    private Image image;
    private Button button;

    void Start()
    {
        image = GetComponent<Image>();
        image.material = new Material(cameraRenderTextureMaterial);
        image.material.SetTexture(mainTextureProperty, _camera.targetTexture);
        button = GetComponent<Button>();
        button.onClick.AddListener(SelectCamera);
    }

    private void SelectCamera()
    {
        CameraManager.Instance.SelectedCamera = _camera;
        print("executed");
    }
}
