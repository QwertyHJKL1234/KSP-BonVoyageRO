﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace BonVoyage
{
    /// <summary>
    /// Common settings for all windows
    /// </summary>
    static class CommonWindowProperties
    {
        public static UISkinDef UnitySkin { get; set; }
        public static UISkinDef ActiveSkin { get; set; } // Actual skin used

        public static RectOffset mainElementPadding = new RectOffset(5, 5, 10, 10);
        public static RectOffset mainListPadding = new RectOffset(4, 4, 3, 3);

        public static readonly Vector2 mainWindowAnchorMin = new Vector2(0.5f, 0.5f);
        public static readonly Vector2 mainWindowAnchorMax = new Vector2(0.5f, 0.5f);
        public static Vector2 MainWindowPosition = new Vector2(0.5f, 0.5f);
        public const float mainWindowWidth = mainListMinWidth + 20;
        public const float mainWindowHeight = mainListMinHeight + 330f;

        public const float mainWindowSpacing = 3f;
        public const float mainListMinWidth = 570f;
        public const float mainListMinHeight = 100f;

        public const int buttonIconWidth = 20;


        #region Styles

        public static readonly UIStyleState StyleState_White = new UIStyleState() { textColor = Color.white };
        public static readonly UIStyleState StyleState_Green = new UIStyleState() { textColor = Color.green };
        public static readonly UIStyleState StyleState_Yellow = new UIStyleState() { textColor = Color.yellow };
        public static readonly UIStyleState StyleState_Red = new UIStyleState() { textColor = Color.red };
        public static readonly UIStyleState StyleState_Grey = new UIStyleState() { textColor = Color.grey };

        public static UIStyle Style_Label_Bold_Left;
        public static UIStyle Style_Label_Bold_Center;
        public static UIStyle Style_Label_Normal_Center;
        public static UIStyle Style_Label_Normal_Center_White;
        public static UIStyle Style_Label_Normal_Center_Green;
        public static UIStyle Style_Label_Normal_Center_Yellow;
        public static UIStyle Style_Label_Normal_Center_Red;
        public static UIStyle Style_Label_Normal_Center_Grey;


        /// <summary>
        /// Get sprite form texture
        /// </summary>
        /// <param name="tex"></param>
        /// <returns></returns>
        private static Sprite SpriteFromTexture(Texture2D tex)
        {
            if (tex != null)
            {
                return Sprite.Create(
                    tex,
                    new Rect(0, 0, tex.width, tex.height),
                    new Vector2(0.5f, 0.5f),
                    tex.width
                );
            }
            else
            {
                return null;
            }
        }


        /// <returns>
		/// A texture object for the image file at the given path.
		/// </returns>
		/// <param name="filepath">Path to image file to load</param>
		private static Texture2D GetImage(string filepath)
        {
            return GameDatabase.Instance.GetTexture(filepath, false);
        }


        /// <returns>
		/// A sprite object for the image at the given path.
		/// </returns>
        private static Sprite GetSprite(string filepath)
        {
            return SpriteFromTexture(GetImage(filepath));
        }


        /// <summary>
        /// Refresh styles after skin change
        /// </summary>
        public static void RefreshStyles()
        {
            // Style_Label_Bold_Left
            Style_Label_Bold_Left = new UIStyle(ActiveSkin.label);
            Style_Label_Bold_Left.fontStyle = FontStyle.Bold;

            // Style_Label_Bold_Center
            Style_Label_Bold_Center = new UIStyle(ActiveSkin.label);
            Style_Label_Bold_Center.fontStyle = FontStyle.Bold;
            Style_Label_Bold_Center.alignment = TextAnchor.UpperCenter;

            // Style_Label_Normal_Center
            Style_Label_Normal_Center = new UIStyle(ActiveSkin.label);
            Style_Label_Normal_Center.alignment = TextAnchor.UpperCenter;

            // Style_Label_Normal_Center_White
            Style_Label_Normal_Center_White = new UIStyle(ActiveSkin.label);
            Style_Label_Normal_Center_White.alignment = TextAnchor.UpperCenter;
            Style_Label_Normal_Center_White.active = StyleState_White;
            Style_Label_Normal_Center_White.normal = StyleState_White;
            Style_Label_Normal_Center_White.disabled = StyleState_White;
            Style_Label_Normal_Center_White.highlight = StyleState_White;

            // Style_Label_Normal_Center_Green
            Style_Label_Normal_Center_Green = new UIStyle(ActiveSkin.label);
            Style_Label_Normal_Center_Green.alignment = TextAnchor.UpperCenter;
            Style_Label_Normal_Center_Green.active = StyleState_Green;
            Style_Label_Normal_Center_Green.normal = StyleState_Green;
            Style_Label_Normal_Center_Green.disabled = StyleState_Green;
            Style_Label_Normal_Center_Green.highlight = StyleState_Green;

            // Style_Label_Normal_Center_Yellow
            Style_Label_Normal_Center_Yellow = new UIStyle(ActiveSkin.label);
            Style_Label_Normal_Center_Yellow.alignment = TextAnchor.UpperCenter;
            Style_Label_Normal_Center_Yellow.active = StyleState_Yellow;
            Style_Label_Normal_Center_Yellow.normal = StyleState_Yellow;
            Style_Label_Normal_Center_Yellow.disabled = StyleState_Yellow;
            Style_Label_Normal_Center_Yellow.highlight = StyleState_Yellow;

            // Style_Label_Normal_Center_Red
            Style_Label_Normal_Center_Red = new UIStyle(ActiveSkin.label);
            Style_Label_Normal_Center_Red.alignment = TextAnchor.UpperCenter;
            Style_Label_Normal_Center_Red.active = StyleState_Red;
            Style_Label_Normal_Center_Red.normal = StyleState_Red;
            Style_Label_Normal_Center_Red.disabled = StyleState_Red;
            Style_Label_Normal_Center_Red.highlight = StyleState_Red;

            // Style_Label_Normal_Center_Grey
            Style_Label_Normal_Center_Grey = new UIStyle(ActiveSkin.label);
            Style_Label_Normal_Center_Grey.alignment = TextAnchor.UpperCenter;
            Style_Label_Normal_Center_Grey.active = StyleState_Grey;
            Style_Label_Normal_Center_Grey.normal = StyleState_Grey;
            Style_Label_Normal_Center_Grey.disabled = StyleState_Grey;
            Style_Label_Normal_Center_Grey.highlight = StyleState_Grey;
        }

        #endregion


        /// <summary>
		/// Add a button outside of the normal DialogGUI* flow layout,
		/// with positioning relative to edges of a parent element.
		/// By DMagic, with modifications by HebaruSan.
		/// </summary>
		/// <param name="parentTransform">Transform of UI object within which to place this button</param>
		/// <param name="innerHorizOffset">Horizontal position; if positive, number of pixels between left edge of window and left edge of button, if negative, then vice versa on right side</param>
		/// <param name="innerVertOffset">Vertical position; if positive, number of pixels between bottom edge of window and bottom edge of button, if negative, then vice versa on top side</param>
		/// <param name="style">Style object containing the sprites for the button</param>
		/// <param name="tooltip">String to show when user hovers on button</param>
		/// <param name="onClick">Function to call when the user clicks the button</param>
		public static void AddFloatingButton(Transform parentTransform, float innerHorizOffset, float innerVertOffset, UIStyle style, string text, string tooltip, UnityAction onClick)
        {
            // This creates a new button object using the prefab from KSP's UISkinManager.
            // The same prefab is used for the PopupDialog system buttons.
            // Anything we set on this object will be reflected in the button we create.
            GameObject btnGameObj = GameObject.Instantiate<GameObject>(UISkinManager.GetPrefab("UIButtonPrefab"));

            // Set the button's parent transform.
            btnGameObj.transform.SetParent(parentTransform, false);

            // Add a layout element and set it to be ignored.
            // Otherwise the button will end up on the bottom of the window.
            btnGameObj.AddComponent<LayoutElement>().ignoreLayout = true;

            // This is how we position the button.
            // The anchors and pivot make the button positioned relative to the top-right corner.
            // The anchored position sets the position with values in pixels.
            RectTransform rect = btnGameObj.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(innerHorizOffset, innerVertOffset);
            rect.sizeDelta = new Vector2(buttonIconWidth, buttonIconWidth);
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(
                rect.anchoredPosition.x < 0 ? 1 : 0,
                rect.anchoredPosition.y < 0 ? 1 : 0
            );

            // Set the button's image component to the normal sprite.
            // Since this object comes from the button's GameObject,
            // changing it affects the button directly!
            Image btnImg = btnGameObj.GetComponent<Image>();
            btnImg.sprite = style.normal.background;

            // Now set the different states to their respective sprites.
            Button button = btnGameObj.GetComponent<Button>();
            button.transition = Selectable.Transition.SpriteSwap;
            button.spriteState = new SpriteState()
            {
                highlightedSprite = style.highlight.background,
                pressedSprite = style.active.background,
                disabledSprite = style.disabled.background
            };

            // The text will be "Button" if we don't clear it.
            btnGameObj.GetChild("Text").GetComponent<TextMeshProUGUI>().text = text;

            // Set the tooltip
            btnGameObj.SetTooltip(tooltip);

            // Set the code to call when clicked.
            button.onClick.AddListener(onClick);

            // Activate the button object, making it visible.
            btnGameObj.SetActive(true);
        }

    }

}
