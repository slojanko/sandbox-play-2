using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Sandbox { 
    [InitializeOnLoad]
    public class EditorUtils : MonoBehaviour
    {
        static EditorUtils()
        {
            EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/InitScene.unity");
        }
    }
}