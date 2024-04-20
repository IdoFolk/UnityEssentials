using TMPro;
using UnityEngine;

namespace IdoFolk_Essentials.Systems.UI
{
    public abstract class CounterUIElement : UIElement
    {
        [SerializeField] private TextMeshProUGUI currentValueText;
        [SerializeField] private TextMeshProUGUI maxValueText;

        public virtual void ElementInit(int maxValue, int currentValue = -1)
        {
            maxValueText.text = $"/{maxValue.ToString()}";
            currentValueText.text = currentValue == -1 ? maxValue.ToString() : currentValue.ToString();
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
            currentValueText.text = value.ToString();
        }

        public sealed override void UpdateVisual()
        {
            base.UpdateVisual();
        }
    }
}