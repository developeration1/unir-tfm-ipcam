using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerCameraView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Material cameraRenderTextureMaterial;
    [SerializeField] private string mainTextureProperty;
    [SerializeField] private LayerMask groundLayer;
    private CameraManager cameraManager;
    private Image image;

    private void OnEnable()
    {
        CameraManager.Instance.onSelectCamera += UpdateSelectedCamera;
    }

    private void OnDisable()
    {
        CameraManager.Instance.onSelectCamera -= UpdateSelectedCamera;
    }

    private void Start()
    {
        cameraManager = CameraManager.Instance;
        image = GetComponent<Image>();
        UpdateSelectedCamera();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(image.rectTransform, eventData.position, null, out Vector2 result))
        {
            result += image.rectTransform.sizeDelta / 2;
            result = new Vector2(result.x * cameraManager.SelectedCamera.pixelWidth, result.y * cameraManager.SelectedCamera.pixelHeight) / image.rectTransform.sizeDelta;
            Ray ray = cameraManager.SelectedCamera.ScreenPointToRay(result);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                PlayerManager.Instance.MoveToPosition(hit.point);
            }
        }
    }

    private void UpdateSelectedCamera ()
    {
        image.material = new Material(cameraRenderTextureMaterial);
        image.material.SetTexture(mainTextureProperty, cameraManager.SelectedCamera.targetTexture);
        print("update");
    }
}
