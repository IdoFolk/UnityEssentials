using System;
using TMPro;
using UnityEngine;

namespace IdoFolk_Essentials.Systems.UI
{
    public class InformationWindow : UIElement
    {
        public static InformationWindow Instance { get; private set; }
        public bool isActive { get; private set; }
        [SerializeField] private Transform _holder;
        [SerializeField] private TextMeshProUGUI _titleTMP;
        [SerializeField] private TextMeshProUGUI _discriptionTMP;
        [SerializeField] private RectTransform _rectTransform;
        [Space] [SerializeField] private Vector2 _windowSize;
        [SerializeField] private float _delayTime;

        private float infoWindowDelayTimer;
        private bool _activeShowRequest;
        private UIElement _currentUIElement;
        private WindowInfo _currentWindowInfo;

        protected override void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            else Instance = this;

            base.Awake();
        }

        private void Start()
        {
            _holder.gameObject.SetActive(false);
        }

        public void RequestShow(UIElement uiElement, WindowInfo windowInfo)
        {
            _currentUIElement = uiElement;
            _currentWindowInfo = windowInfo;
            _activeShowRequest = true;
        }

        private void Show(UIElement uiElement, WindowInfo windowInfo)
        {
            _rectTransform.rect.Set(0, 0, _windowSize.x, _windowSize.y);

            _titleTMP.text = windowInfo.Name;
            _discriptionTMP.text = windowInfo.Discription;

            var uiElementRect = uiElement.RectTransform.rect;
            var windowPos = uiElement.transform.position;
            windowPos.x += uiElementRect.width / 2;
            windowPos.y += uiElementRect.height / 2;
            transform.position = windowPos;
            _holder.gameObject.SetActive(true);
            isActive = true;
        }

        public void Hide()
        {
            _activeShowRequest = false;
            isActive = false;
            _holder.gameObject.SetActive(false);
        }

        private void OnValidate()
        {
            _rectTransform ??= GetComponent<RectTransform>();
        }

        protected override void Update()
        {
            if (_activeShowRequest)
            {
                infoWindowDelayTimer += Time.deltaTime;
                if (infoWindowDelayTimer > _delayTime)
                {
                    infoWindowDelayTimer = 0;
                    if (_currentUIElement.isMouseOver) Show(_currentUIElement, _currentWindowInfo);
                    _activeShowRequest = false;
                }
            }
        }
    }

    [Serializable]
    public struct WindowInfo
    {
        public string Name;
        public string Discription;
    }
}