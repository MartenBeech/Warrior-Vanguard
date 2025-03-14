using UnityEngine;

public class ColorPalette {
    public enum ColorEnum {
        red, green,
    }

    public Color GetColor(ColorEnum color) {
        return color switch {
            ColorEnum.red => Color.HSVToRGB(0 / 360f, 1f, 1f),
            ColorEnum.green => Color.HSVToRGB(120 / 360f, 1f, 1f),
            _ => Color.white,
        };
    }

    public Color AddTransparency(Color imgColor, int percentageVisible) {
        imgColor.a = percentageVisible / 100f;
        return imgColor;
    }
}