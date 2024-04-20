using UnityEngine;
using UnityEngine.UI;

namespace IdoFolk_Essentials.Systems.UI
{
    public abstract class StatBarUIElement : UIElement
    {
        [SerializeField] private Image _statBar;
        private int _maxValue;

        public virtual void ElementInit(int maxValue, int currentValue)
        {
            _maxValue = maxValue;
            UpdateUIData(currentValue);
            gameObject.SetActive(true);
        }

        public virtual void ElementInit(int maxValue)
        {
            _maxValue = maxValue;
            UpdateUIData(maxValue);
            gameObject.SetActive(true);
        }

        public sealed override void Show()
        {
            Init();
        }

        /// <summary>
        /// instantiate ElementInit
        /// </summary>
        public abstract void Init();

        protected void UpdateUIData(int value)
        {
            _statBar.fillAmount = (float)value / _maxValue;
        }

        public sealed override void UpdateVisual()
        {
            base.UpdateVisual();
        }
    }
}