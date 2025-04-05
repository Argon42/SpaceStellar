using System;
using Bananva.UI.Dispatching.Api;
using Bananva.UI.Dispatching.Api.Presenters;
using Cysharp.Threading.Tasks;

namespace Bananva.UI.Dispatching
{
    public class ScreenSwitcher : IScreenSwitcher
    {
        public event Action<IScreenPresenter?, IScreenPresenter> OnSectionChangedEvent = delegate { };
        private IScreenPresenter? ActiveScreen { get; set; }

        public UniTask SwitchToScreen<TModel>(IScreenPresenter<TModel> screenPresenter, TModel model)
        {
            if (ActiveScreen is { IsOpened: true })
            {
                ActiveScreen.Close();
                ActiveScreen.ResetModel();
            }

            screenPresenter.SetModel(model);
            screenPresenter.Open();

            var previousScreen = ActiveScreen;
            ActiveScreen = screenPresenter;
            OnSectionChangedEvent.Invoke(previousScreen, ActiveScreen);

            return UniTask.CompletedTask;
        }
    }
}