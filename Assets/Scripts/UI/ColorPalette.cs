using UnityEngine;

public class ColorPalette {
    public enum ColorEnum {
        red, green, gray, yellow
    }

    public Color GetColor(ColorEnum color) {
        return color switch {
            ColorEnum.red => Color.HSVToRGB(0 / 360f, 1f, 1f),
            ColorEnum.green => Color.HSVToRGB(120 / 360f, 1f, 1f),
            ColorEnum.gray => Color.HSVToRGB(0 / 360f, 0f, 0.5f),
            ColorEnum.yellow => Color.HSVToRGB(60 / 360f, 1f, 1f),
            _ => Color.white,
        };
    }

    public Color AddTransparency(Color imgColor, int percentageVisible) {
        imgColor.a = percentageVisible / 100f;
        return imgColor;
    }
}