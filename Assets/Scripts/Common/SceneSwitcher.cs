using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceStellar.Common
{
    public class SceneSwitcher
    {
        public AsyncOperation SwitchTo(GameScenes scene)
        {
            var name = scene.ToString();
            return SwitchTo(name);
        }

        private AsyncOperation SwitchTo(string name)
        {
            return SceneManager.LoadSceneAsync(name, LoadSceneMode.Single)
                   ?? throw new InvalidOperationException($"Failed to switch to scene '{name}'");
        }
    }
}