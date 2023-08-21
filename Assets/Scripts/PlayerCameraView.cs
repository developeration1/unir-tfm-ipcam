using Doozy.Runtime.UIManager.Components;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using doozyManager = Doozy.Runtime.UIManager;

[RequireComponent(typeof(Image))]
public class PlayerCameraView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Material cameraRenderTextureMaterial;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask interactibleLayer;
    [SerializeField] private UIButton rotationRightButton;
    [SerializeField] private UIButton rotationLeftButton;
    [SerializeField] private UIButton zoomInButton;
    [SerializeField] private UIButton zoomOutButton;
    private CameraManager cameraManager;
    private Image image;
    private float actualMovingDirection;
    private float actualZoomDirection;

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
        if(!PlayerManager.Instance.InCintematic && RectTransformUtility.ScreenPointToLocalPointInRectangle(image.rectTransform, eventData.position, null, out Vector2 result))
        {
            result += image.rectTransform.sizeDelta / 2;
            result = new Vector2(result.x * cameraManager.SelectedCamera.Cam.pixelWidth, result.y * cameraManager.SelectedCamera.Cam.pixelHeight) / image.rectTransform.sizeDelta;
            Ray ray = cameraManager.SelectedCamera.Cam.ScreenPointToRay(result);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                PlayerManager.Instance.MoveToPosition(hit.point);
                return;
            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactibleLayer))
            {
                if(hit.transform.TryGetComponent(out Interactible interactible))
                {
                    PlayerManager.Instance.Inspect(interactible);
                }
                //PlayerManager.Instance.MoveToPosition(hit.transform.GetChild(0).position);
                //PlayerManager.Instance.Inspect(hit.transform.GetChild(0).position);
                return;
            }
        }
    }

    private void UpdateSelectedCamera ()
    {
        actualMovingDirection = 0;
        image.material = cameraManager.SelectedCamera.EndMaterial;
        rotationLeftButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerDown).Event.RemoveAllListeners();
        rotationRightButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerDown).Event.RemoveAllListeners();
        rotationLeftButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerUp).Event.RemoveAllListeners();
        rotationRightButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerUp).Event.RemoveAllListeners();
        rotationLeftButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerDown).Event.AddListener(() => actualMovingDirection = -1);
        rotationRightButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerDown).Event.AddListener(() => actualMovingDirection = 1);
        rotationLeftButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerUp).Event.AddListener(() => actualMovingDirection = 0);
        rotationRightButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerUp).Event.AddListener(() => actualMovingDirection = 0);
        zoomInButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerDown).Event.RemoveAllListeners();
        zoomOutButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerDown).Event.RemoveAllListeners();
        zoomInButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerUp).Event.RemoveAllListeners();
        zoomOutButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerUp).Event.RemoveAllListeners();
        zoomInButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerDown).Event.AddListener(() => actualZoomDirection = 1);
        zoomOutButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerDown).Event.AddListener(() => actualZoomDirection = -1);
        zoomInButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerUp).Event.AddListener(() => actualZoomDirection = 0);
        zoomOutButton.AddBehaviour(doozyManager.UIBehaviour.Name.PointerUp).Event.AddListener(() => actualZoomDirection = 0);
    }

    private void FixedUpdate()
    {
        cameraManager.SelectedCamera.Move(actualMovingDirection);
        cameraManager.SelectedCamera.Zoom(actualZoomDirection);
    }
}
