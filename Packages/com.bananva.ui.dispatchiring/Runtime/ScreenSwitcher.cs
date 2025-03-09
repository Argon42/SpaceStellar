using System;
using Bananva.UI.Dispatchiring.Api;
using Bananva.UI.Dispatchiring.Api.Presenters;
using Cysharp.Threading.Tasks;

namespace Bananva.UI.Dispatchiring
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