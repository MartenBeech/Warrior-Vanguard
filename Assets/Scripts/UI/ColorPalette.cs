using Unity.VisualScripting;
using UnityEngine;

public class ColorPalette {
    public enum ColorEnum {
        Red, Green, GreenDark, Gray, Yellow, Black, White, Teal, TealMedium, TealWeak, Purple, Orange, Blue
    }

    public static Color GetColor(ColorEnum color) {
        return color switch {
            ColorEnum.Red => Color.HSVToRGB(0 / 360f, 1f, 1f),
            ColorEnum.Green => Color.HSVToRGB(120 / 360f, 1f, 1f),
            ColorEnum.GreenDark => Color.HSVToRGB(120 / 360f, 1f, 0.7f),
            ColorEnum.Gray => Color.HSVToRGB(0 / 360f, 0f, 0.5f),
            ColorEnum.Yellow => Color.HSVToRGB(60 / 360f, 1f, 1f),
            ColorEnum.Black => Color.HSVToRGB(0 / 360f, 0f, 0f),
            ColorEnum.White => Color.HSVToRGB(0 / 360f, 0f, 1f),
            ColorEnum.Teal => Color.HSVToRGB(180 / 360f, 1f, 1f),
            ColorEnum.TealMedium => Color.HSVToRGB(180 / 360f, 0.5f, 1f),
            ColorEnum.TealWeak => Color.HSVToRGB(180 / 360f, 0.1f, 1f),
            ColorEnum.Purple => Color.HSVToRGB(300 / 360f, 1f, 1f),
            ColorEnum.Orange => Color.HSVToRGB(30 / 360f, 1f, 1f),
            ColorEnum.Blue => Color.HSVToRGB(240 / 360f, 1f, 1f),
            _ => Color.white,
        };
    }

    public static Color AddTransparency(Color imgColor, int percentageVisible) {
        imgColor.a = percentageVisible / 100f;
        return imgColor;
    }

    public static string AddColorToText(string text, Color color) {
        return $"<color=#{color.ToHexString()}>{text}</color>";
    }
}