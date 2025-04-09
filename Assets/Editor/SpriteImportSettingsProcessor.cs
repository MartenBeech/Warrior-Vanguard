using UnityEditor;
using UnityEngine;

public class SpriteImportSettingsProcessor : AssetPostprocessor {
    void OnPreprocessTexture() {
        TextureImporter importer = (TextureImporter)assetImporter;

        // Only apply to images in the correct folder (optional)
        if (!assetPath.ToLower().EndsWith(".png") && !assetPath.ToLower().EndsWith(".jpg"))
            return;

        importer.spriteImportMode = SpriteImportMode.Single;
    }
}