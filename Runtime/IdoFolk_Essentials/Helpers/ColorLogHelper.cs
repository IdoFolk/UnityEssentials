using UnityEngine;

namespace IdoFolk_Essentials.Helpers
{
    /// <summary>
    /// A class that have reference to colors that represents a system or object log message color
    /// </summary>
    public static class ColorLogHelper
    {
        //add defined color values for your project 
        public const string RED = "#ff0000";
        public const string GREEN = "#00ff00";

        public static string SetColorToString(string message, Color color)
            => $"<color={ToRGBHex(color)}>{message}</color>";

        public static string SetColorToString(string message, string hexCode)
            => $"<color={hexCode}>{message}/<color>";

        private static string ToRGBHex(Color c)
            => $"#{ToByte(c.r):X2}{ToByte(c.g):X2}{ToByte(c.b):X2}";

        private static byte ToByte(float f)
        {
            f = Mathf.Clamp01(f);
            return (byte)(f * 255);
        }
    }
}