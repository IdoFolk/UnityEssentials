using System;
using System.Collections.Generic;
using IdoFolk_Essentials.Helpers;
using UnityEngine;

namespace IdoFolk_Essentials.Systems.UI
{
    [DefaultExecutionOrder(-1)]
    public class UIManager : MonoSingleton<UIManager>
    {
        public bool MouseOverUI { get; private set; }

        private Dictionary<UIGroup, List<UIElement>> _uiGroups;
        private List<UIElement> _mouseOnUIElements = new List<UIElement>();

        protected override void Awake()
        {
            base.Awake();

            _uiGroups = new Dictionary<UIGroup, List<UIElement>>();

            var uiGroupTypes = Enum.GetValues(typeof(UIGroup));
            for (int i = 0; i < uiGroupTypes.Length; i++)
            {
                _uiGroups.Add((UIGroup)i, new List<UIElement>());
            }
        }

        public void AddUIElement(UIElement element, UIGroup group)
        {
            if (_uiGroups.TryGetValue(group, out var uiElements))
            {
                uiElements.Add(element);
                element.OnMouseEnter += MouseOnUIEnter;
                element.OnMouseExit += MouseOnUIExit;
            }
            else
            {
                Debug.LogError("could not find UIGroup");
            }
        }

        public void RemoveUIElement(UIElement element, UIGroup group)
        {
            if (_uiGroups.TryGetValue(group, out var uiElements))
            {
                uiElements.Remove(element);
            }
            else
            {
                Debug.LogError("could not find UIGroup");
            }
        }

        public void ShowUIGroup(UIGroup uiGroup)
        {
            if (_uiGroups.TryGetValue(uiGroup, out var uiElements))
            {
                foreach (var element in uiElements)
                {
                    element.Show();
                }
            }
        }

        public void HideUIGroup(UIGroup uiGroup)
        {
            if (_uiGroups.TryGetValue(uiGroup, out var uiElements))
            {
                foreach (var element in uiElements)
                {
                    element.Hide();
                }
            }
        }

        public void UpdateUIGroup(UIGroup uiGroup)
        {
            if (_uiGroups.TryGetValue(uiGroup, out var uiElements))
            {
                foreach (var element in uiElements)
                {
                    element.UpdateVisual();
                }
            }
        }

        private void MouseOnUIEnter(UIElement element)
        {
            MouseOverUI = true;
            _mouseOnUIElements.Add(element);
        }

        private void MouseOnUIExit(UIElement element)
        {
            if (_mouseOnUIElements.Contains(element)) _mouseOnUIElements.Remove(element);
            if (_mouseOnUIElements.Count == 0)
                MouseOverUI = false;
        }
    }

    public enum UIGroup
    {
        GameUI,
        MapUI,
        MenuUI,
        EndGameUI,
        SelectionUI,
        TopCounterUI,
        PartyUI,
        Indicators,
        InfoWindow,
        DevTools,
    }
}