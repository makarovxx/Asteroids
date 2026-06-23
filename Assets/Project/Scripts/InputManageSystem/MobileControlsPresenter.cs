using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace Project.Scripts.InputManageSystem
{
    public sealed class MobileControlsPresenter : IInitializable, IDisposable
    {
        private readonly MobileInputState _inputState;
        private GameObject _root;
        private bool _visible;

        public MobileControlsPresenter(MobileInputState inputState)
        {
            _inputState = inputState;
        }

        public void Initialize()
        {
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
                throw new InvalidOperationException("Mobile controls require a Canvas in the scene.");

            _root = CreateRoot(canvas.transform);
            CreateJoystick(_root.transform);
            CreateHoldButton(_root.transform, "THRUST", new Vector2(-170f, 155f));
            CreateActionButton(_root.transform, "FIRE", new Vector2(-330f, 110f), _inputState.PressBullet);
            CreateActionButton(_root.transform, "LASER", new Vector2(-155f, 330f), _inputState.PressLaser);
            _root.SetActive(_visible);
        }

        public void Dispose()
        {
            if (_root != null)
                Object.Destroy(_root);
        }

        public void SetVisible(bool visible)
        {
            _visible = visible;
            if (_root != null)
                _root.SetActive(visible);
        }

        private GameObject CreateRoot(Transform parent)
        {
            GameObject root = new GameObject("MobileControls", typeof(RectTransform));
            RectTransform rect = root.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            root.AddComponent<SafeAreaPanel>();
            return root;
        }

        private void CreateJoystick(Transform parent)
        {
            GameObject background = CreateImage("Joystick", parent, new Color(0.08f, 0.12f, 0.18f, 0.55f));
            RectTransform backgroundRect = background.GetComponent<RectTransform>();
            SetBottomLeft(backgroundRect, new Vector2(170f, 170f), new Vector2(240f, 240f));

            GameObject handle = CreateImage("Handle", background.transform, new Color(0.35f, 0.8f, 1f, 0.85f));
            RectTransform handleRect = handle.GetComponent<RectTransform>();
            handleRect.anchorMin = new Vector2(0.5f, 0.5f);
            handleRect.anchorMax = new Vector2(0.5f, 0.5f);
            handleRect.pivot = new Vector2(0.5f, 0.5f);
            handleRect.sizeDelta = new Vector2(90f, 90f);
            handleRect.anchoredPosition = Vector2.zero;
            handle.GetComponent<Image>().raycastTarget = false;

            VirtualJoystick joystick = background.AddComponent<VirtualJoystick>();
            joystick.Initialize(backgroundRect, handleRect, _inputState);
        }

        private void CreateHoldButton(Transform parent, string label, Vector2 position)
        {
            GameObject button = CreateButtonVisual(label, parent, position, new Vector2(145f, 145f));
            button.AddComponent<MobileHoldButton>().Initialize(_inputState);
        }

        private void CreateActionButton(Transform parent, string label, Vector2 position, Action action)
        {
            GameObject button = CreateButtonVisual(label, parent, position, new Vector2(130f, 130f));
            button.AddComponent<MobileActionButton>().Initialize(action);
        }

        private GameObject CreateButtonVisual(string label, Transform parent, Vector2 position, Vector2 size)
        {
            GameObject button = CreateImage(label, parent, new Color(0.08f, 0.12f, 0.18f, 0.72f));
            SetBottomRight(button.GetComponent<RectTransform>(), position, size);

            GameObject textObject = new GameObject("Label", typeof(RectTransform), typeof(Text));
            RectTransform textRect = textObject.GetComponent<RectTransform>();
            textRect.SetParent(button.transform, false);
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            Text text = textObject.GetComponent<Text>();
            text.text = label;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = 24;
            text.fontStyle = FontStyle.Bold;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.white;
            text.raycastTarget = false;
            return button;
        }

        private GameObject CreateImage(string name, Transform parent, Color color)
        {
            GameObject imageObject = new GameObject(name, typeof(RectTransform), typeof(Image));
            imageObject.transform.SetParent(parent, false);
            Image image = imageObject.GetComponent<Image>();
            image.color = color;

            Sprite sprite = Resources.GetBuiltinResource<Sprite>("UI/Skin/UISprite.psd");
            if (sprite != null)
            {
                image.sprite = sprite;
                image.type = Image.Type.Sliced;
            }

            return imageObject;
        }

        private static void SetBottomLeft(RectTransform rect, Vector2 position, Vector2 size)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.zero;
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = size;
        }

        private static void SetBottomRight(RectTransform rect, Vector2 position, Vector2 size)
        {
            rect.anchorMin = new Vector2(1f, 0f);
            rect.anchorMax = new Vector2(1f, 0f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = size;
        }
    }

    public sealed class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private const float DeadZone = 0.12f;
        private RectTransform _background;
        private RectTransform _handle;
        private MobileInputState _inputState;

        public void Initialize(RectTransform background, RectTransform handle, MobileInputState inputState)
        {
            _background = background;
            _handle = handle;
            _inputState = inputState;
        }

        public void OnPointerDown(PointerEventData eventData) => UpdateDirection(eventData);

        public void OnDrag(PointerEventData eventData) => UpdateDirection(eventData);

        public void OnPointerUp(PointerEventData eventData)
        {
            _handle.anchoredPosition = Vector2.zero;
            _inputState.SetDirection(Vector2.zero);
        }

        private void UpdateDirection(PointerEventData eventData)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _background,
                    eventData.position,
                    eventData.pressEventCamera,
                    out Vector2 localPoint))
                return;

            float radius = _background.rect.width * 0.5f;
            Vector2 direction = Vector2.ClampMagnitude(localPoint / radius, 1f);
            if (direction.magnitude < DeadZone)
                direction = Vector2.zero;

            _handle.anchoredPosition = direction * radius * 0.55f;
            _inputState.SetDirection(direction);
        }
    }

    public sealed class MobileHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private MobileInputState _inputState;

        public void Initialize(MobileInputState inputState) => _inputState = inputState;

        public void OnPointerDown(PointerEventData eventData) => _inputState.SetThrust(true);

        public void OnPointerUp(PointerEventData eventData) => _inputState.SetThrust(false);

        private void OnDisable() => _inputState?.SetThrust(false);
    }

    public sealed class MobileActionButton : MonoBehaviour, IPointerDownHandler
    {
        private Action _action;

        public void Initialize(Action action) => _action = action;

        public void OnPointerDown(PointerEventData eventData) => _action.Invoke();
    }

    public sealed class SafeAreaPanel : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Rect _lastSafeArea;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            ApplySafeArea();
        }

        private void Update()
        {
            if (_lastSafeArea != Screen.safeArea)
                ApplySafeArea();
        }

        private void ApplySafeArea()
        {
            Rect safeArea = Screen.safeArea;
            _lastSafeArea = safeArea;
            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
            _rectTransform.offsetMin = Vector2.zero;
            _rectTransform.offsetMax = Vector2.zero;
        }
    }
}
