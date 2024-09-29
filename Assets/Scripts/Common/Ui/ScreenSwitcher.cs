using System;
using Cysharp.Threading.Tasks;
using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Abstraction.Presenters;

namespace SpaceStellar.Common.Ui
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

            IScreenPresenter? previousScreen = ActiveScreen;
            ActiveScreen = screenPresenter;
            OnSectionChangedEvent.Invoke(previousScreen, ActiveScreen);

            return UniTask.CompletedTask;
        }
    }
}
