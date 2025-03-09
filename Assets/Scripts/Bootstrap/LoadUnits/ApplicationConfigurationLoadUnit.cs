using Bananva.LoadingService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SpaceStellar.Bootstrap.LoadUnits
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