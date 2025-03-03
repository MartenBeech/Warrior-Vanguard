using UnityEngine;

public class ColorPalette {
    public enum ColorEnum {
        red, green,
    }

    public Color GetColor(ColorEnum color) {
        switch (color) {
            case ColorEnum.red:
                return Color.HSVToRGB(0 / 360, 1f, 1f);
            case ColorEnum.green:
                return Color.HSVToRGB(120 / 360, 1f, 1f);
            default:
                return Color.white;
        }
    }
}