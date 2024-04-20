using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IdoFolk_Essentials.Systems.UI
{
    public abstract class UIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        //inherit from this class if it is a ui element
        public event Action<UIElement> OnMouseEnter;
        public event Action<UIElement> OnMouseExit;
        public RectTransform RectTransform => rectTransform;
        public bool isMouseOver { get; private set; }

        [SerializeField, HideInInspector] protected RectTransform rectTransform;

        [BoxGroup("UI Element")] [SerializeField]
        private bool showOnAwake = false;

        [BoxGroup("UI Element")] [SerializeField]
        private bool hideOnAwake = false;

        [BoxGroup("UI Element")] [SerializeField]
        private bool assignUIGroup = false;

        [BoxGroup("UI Element")] [SerializeField, ShowIf(nameof(assignUIGroup))]
        protected UIGroup uiGroup;

        [BoxGroup("UI Element")] [SerializeField]
        private bool showInfoWindow = false;

        [BoxGroup("UI Element")] [SerializeField, ShowIf(nameof(showInfoWindow))]
        protected WindowInfo _windowInfo;




        protected virtual void Awake()
        {
            if (assignUIGroup) UIManager.Instance.AddUIElement(this, uiGroup);
            if (showOnAwake)
                Show();
            if (hideOnAwake)
                Hide();
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void UpdateVisual()
        {

        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnValidate()
        {
            rectTransform ??= GetComponent<RectTransform>();
        }

        protected virtual void OnDestroy()
        {
            OnMouseExit?.Invoke(this);
            if (assignUIGroup)
            {
                if (UIManager.Instance is not null)
                    UIManager.Instance.RemoveUIElement(this, uiGroup);
            }
        }

        protected virtual void Update()
        {

        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            isMouseOver = true;
            OnMouseEnter?.Invoke(this);
            if (showInfoWindow) InformationWindow.Instance.RequestShow(this, _windowInfo);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            isMouseOver = false;
            OnMouseExit?.Invoke(this);
            if (showInfoWindow)
            {
                if (InformationWindow.Instance.isActive) InformationWindow.Instance.Hide();
            }
        }

        protected virtual void OnDisable()
        {
            isMouseOver = false;
            OnMouseExit?.Invoke(this);
        }
    }
}