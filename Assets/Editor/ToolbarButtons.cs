using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

namespace SpaceStellar.Editor
{
    [InitializeOnLoad]
    public class ToolbarButtons
    {
        static ToolbarButtons()
        {
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUILeft);
        }

        private static void OnToolbarGUILeft()
        {
            GUILayout.FlexibleSpace();

            if(GUILayout.Button(new GUIContent("Boot", "Open Scene")))
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Bootstrap.unity");
                AssetDatabase.Refresh();
            }
            
            if(GUILayout.Button(new GUIContent("Game", "Open Scene")))
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");
                AssetDatabase.Refresh();
            }
        }

        static void OnToolbarGUI()
        {
            if(GUILayout.Button(new GUIContent("Play from boot", "Start Scene from boot scene")))
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Bootstrap.unity");
                EditorApplication.isPlaying = true;
                AssetDatabase.Refresh();
            }

            GUILayout.FlexibleSpace();
        }
    }
}