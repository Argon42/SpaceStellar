using SpaceStellar.Common;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

namespace SpaceStellar.Editor
{
    [InitializeOnLoad]
    public class ToolbarButtons
    {
        private static readonly GameScenes[] SceneNamesInToolbar =
        {
            GameScenes.Bootstrap,
            GameScenes.Meta,
        };

        static ToolbarButtons()
        {
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUILeft);
        }

        private static void OnToolbarGUILeft()
        {
            GUILayout.FlexibleSpace();


            foreach (var sceneName in SceneNamesInToolbar)
            {
                if (!GUILayout.Button(new GUIContent(sceneName.ToString(), "Open Scene")))
                {
                    continue;
                }

                EditorSceneManager.OpenScene($"Assets/Scenes/{sceneName}.unity");
                AssetDatabase.Refresh();
            }
        }

        static void OnToolbarGUI()
        {
            if (GUILayout.Button(new GUIContent("Play from boot", "Start Scene from boot scene")))
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Bootstrap.unity");
                EditorApplication.isPlaying = true;
                AssetDatabase.Refresh();
            }

            GUILayout.FlexibleSpace();
        }
    }
}