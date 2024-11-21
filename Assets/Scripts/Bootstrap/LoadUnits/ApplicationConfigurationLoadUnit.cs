using Cysharp.Threading.Tasks;
using SpaceStellar.Utility;
using UnityEngine;

namespace SpaceStellar.Bootstrap
{
    public class ApplicationConfigurationLoadUnit : ILoadUnit
    {
        public UniTask Load()
        {
            Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
            return UniTask.CompletedTask;
        }
    }
}