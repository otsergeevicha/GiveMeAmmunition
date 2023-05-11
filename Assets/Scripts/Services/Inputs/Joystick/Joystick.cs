using Plugins.MonoCache;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

namespace Services.Inputs.Joystick
{
    public enum VirtualJoystickType
    {
        Fixed,
        Floating
    }

    public class Joystick : MonoCache, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private RectTransform _centerArea;
        [SerializeField] private RectTransform _handle;

        [SerializeField] private float _movementRange = 100f;

        protected VirtualJoystickType JoystickType = VirtualJoystickType.Fixed;
        protected bool HideOnPointerUp = false;
        protected bool CentralizeOnPointerUp = true;
        private Canvas _canvas;
        private RectTransform _baseRect;
        private OnScreenStick _handleStickController;
        private CanvasGroup _bgCanvasGroup;
        private Vector2 _initialPosition = Vector2.zero;

        protected virtual void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _baseRect = GetComponent<RectTransform>();
            _bgCanvasGroup = _centerArea.GetComponent<CanvasGroup>();
            _handleStickController = _handle.gameObject.AddComponent<OnScreenStick>();
            _handleStickController.movementRange = _movementRange;

            Vector2 center = new Vector2(0.5f, 0.5f);
            
            _centerArea.pivot = center;
            _handle.anchorMin = center;
            _handle.anchorMax = center;
            _handle.pivot = center;
            _handle.anchoredPosition = Vector2.zero;

            _initialPosition = _centerArea.anchoredPosition;

            if (JoystickType == VirtualJoystickType.Fixed)
            {
                _centerArea.anchoredPosition = _initialPosition;
                _bgCanvasGroup.alpha = 1;
            }
            else if (JoystickType == VirtualJoystickType.Floating)
                _bgCanvasGroup.alpha = HideOnPointerUp ? 0 : 1;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PointerEventData constructedEventData = new PointerEventData(EventSystem.current);
            constructedEventData.position = _handle.position;
            _handleStickController.OnPointerDown(constructedEventData);

            if (JoystickType == VirtualJoystickType.Floating)
            {
                _centerArea.anchoredPosition = GetAnchoredPosition(eventData.position);

                if (HideOnPointerUp)
                    _bgCanvasGroup.alpha = 1;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (JoystickType == VirtualJoystickType.Floating)
                _handleStickController.OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (JoystickType == VirtualJoystickType.Floating)
            {
                if (CentralizeOnPointerUp)
                    _centerArea.anchoredPosition = _initialPosition;

                _bgCanvasGroup.alpha = HideOnPointerUp ? 0 : 1;
            }

            PointerEventData constructedEventData = new PointerEventData(EventSystem.current);
            constructedEventData.position = Vector2.zero;

            _handleStickController.OnPointerUp(constructedEventData);
        }

        private Vector2 GetAnchoredPosition(Vector2 screenPosition)
        {
            Camera cam = (_canvas.renderMode == RenderMode.ScreenSpaceCamera) ? _canvas.worldCamera : null;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, screenPosition, cam, out Vector2 localPoint))
            {
                Vector2 pivotOffset = _baseRect.pivot * _baseRect.sizeDelta;
                return localPoint - (_centerArea.anchorMax * _baseRect.sizeDelta) + pivotOffset;
            }

            return Vector2.zero;
        }
    }
}