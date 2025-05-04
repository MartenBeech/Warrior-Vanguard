using UnityEngine;

public class ColorPalette {
    public enum ColorEnum {
        red, green, gray, yellow, black, white, teal, tealMedium, tealWeak, purple
    }

    public static Color GetColor(ColorEnum color) {
        return color switch {
            ColorEnum.red => Color.HSVToRGB(0 / 360f, 1f, 1f),
            ColorEnum.green => Color.HSVToRGB(120 / 360f, 1f, 1f),
            ColorEnum.gray => Color.HSVToRGB(0 / 360f, 0f, 0.5f),
            ColorEnum.yellow => Color.HSVToRGB(60 / 360f, 1f, 1f),
            ColorEnum.black => Color.HSVToRGB(0 / 360f, 0f, 0f),
            ColorEnum.white => Color.HSVToRGB(0 / 360f, 0f, 1f),
            ColorEnum.teal => Color.HSVToRGB(180 / 360f, 1f, 1f),
            ColorEnum.tealMedium => Color.HSVToRGB(180 / 360f, 0.5f, 1f),
            ColorEnum.tealWeak => Color.HSVToRGB(180 / 360f, 0.1f, 1f),
            ColorEnum.purple => Color.HSVToRGB(300 / 360f, 1f, 1f),
            _ => Color.white,
        };
    }

    public static Color AddTransparency(Color imgColor, int percentageVisible) {
        imgColor.a = percentageVisible / 100f;
        return imgColor;
    }
}