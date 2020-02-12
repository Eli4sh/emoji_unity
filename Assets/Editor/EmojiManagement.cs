using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class EmojiManagement : IPreprocessBuildWithReport
{
    private const string _emojiSpriteAssetPath = "Assets/SpriteAssets/emoji_mobile_32.asset";
    private const string _googleEmojiSpriteSheetPath = "Assets/EmojiSpriteSheets/sheet_google_32.png";
    private const string _appleEmojiSpriteSheetPath = "Assets/EmojiSpriteSheets/sheet_apple_32.png";

    #region IPreprocessBuildWithReport Implementation
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(BuildReport report)
    {
        TMP_SpriteAsset spriteAsset = GetSpriteAsset();
        if (spriteAsset != null)
        {
            Texture spriteSheet = GetSpriteSheet(target: report.summary.platform);

            if (spriteSheet != null)
            {
                spriteAsset.spriteSheet = spriteSheet;
            }
            else
            {
                Debug.LogErrorFormat("SpriteSheet texture couldn't be found for {0}", report.summary.platform);
            }
        }
        else
        {
            Debug.LogErrorFormat("SpriteAsset couldn't be loaded at path: {0}", _emojiSpriteAssetPath);
        }
    }
    #endregion

    private TMP_SpriteAsset GetSpriteAsset()
    {
        TMP_SpriteAsset asset = AssetDatabase.LoadAssetAtPath(_emojiSpriteAssetPath, (typeof(TMP_SpriteAsset))) as TMP_SpriteAsset;

        return asset;
    }

    private Texture GetSpriteSheet(BuildTarget target)
    {
        Texture EmojiSpriteSheet = null;
        if (target == BuildTarget.iOS)
        {
            EmojiSpriteSheet = AssetDatabase.LoadAssetAtPath<Texture>(_appleEmojiSpriteSheetPath);
        }
        else if (target == BuildTarget.Android)
        {
            EmojiSpriteSheet = AssetDatabase.LoadAssetAtPath<Texture>(_googleEmojiSpriteSheetPath);
        }

        return EmojiSpriteSheet;
    }
}
#endif