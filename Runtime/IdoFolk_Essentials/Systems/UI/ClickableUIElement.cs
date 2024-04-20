using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IdoFolk_Essentials.Systems.UI
{
    public abstract class ClickableUIElement : UIElement, IPointerClickHandler
    {
        //inherit from this class for clickable ui element
        public event Action<PointerEventData> OnClickEvent;
        public event Action OnDoubleClickEvent;

        [BoxGroup("UI Element")] [SerializeField]
        private bool enableDoubleClick;

        [BoxGroup("UI Element")] [SerializeField, ShowIf(nameof(enableDoubleClick))]
        private float doubleClickSpeed = 0.5f;

        private int _clickNum;

        private float _doubleClickTimer;


        public void OnPointerClick(PointerEventData eventData)
        {
            switch (_clickNum)
            {
                case 0:
                    OnClick(eventData);
                    return;
                case 1:
                    OnDoubleClick(eventData);
                    return;
            }
        }

        protected override void Update()
        {
            base.Update();
            if (_clickNum == 0 || !enableDoubleClick)
                return;

            _doubleClickTimer -= Time.deltaTime;

            if (_doubleClickTimer <= 0)
            {
                _clickNum = 0;
                _doubleClickTimer = 0;
            }
        }

        protected virtual void OnClick(PointerEventData eventData)
        {
            OnClickEvent?.Invoke(eventData);

            if (!enableDoubleClick) return;

            _clickNum++;
            _doubleClickTimer = doubleClickSpeed;
        }

        protected virtual void OnDoubleClick(PointerEventData eventData)
        {
            OnDoubleClickEvent?.Invoke();
            _doubleClickTimer = 0;
            _clickNum = 0;
        }

        protected virtual void OnDisable()
        {
            _clickNum = 0;
            _doubleClickTimer = 0;
        }
    }
}