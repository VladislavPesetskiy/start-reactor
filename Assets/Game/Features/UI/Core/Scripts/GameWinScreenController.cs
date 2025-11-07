using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core;
using Playtika.Controllers;
using UnityEngine;

namespace Game.UI.Core.Scripts
{
    public class GameWinScreenController : ControllerWithResultBase
    {
        private readonly ScreensFactory m_screensFactory;
        private readonly IGameEventsRequestsModel m_gameEventsRequestsModel;
        private GameWinScreenView m_view;

        private UniTaskCompletionSource m_completionSource = new();

        public GameWinScreenController
        (
            IControllerFactory controllerFactory,
            ScreensFactory screensFactory,
            IGameEventsRequestsModel gameEventsRequestsModel
        ) : base(controllerFactory)
        {
            m_screensFactory = screensFactory;
            m_gameEventsRequestsModel = gameEventsRequestsModel;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            m_view = m_screensFactory.CreateScreen<GameWinScreenView>();
            m_view.EventNextButtonClick += OnNextButtonClick;
            
            await m_view.PlayShowAnimation(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            await m_completionSource.Task;
            cancellationToken.ThrowIfCancellationRequested();
            
            await m_view.PlayHideAnimation(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            
            m_gameEventsRequestsModel.RequestRestart();
            Complete();
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            m_view.EventNextButtonClick -= OnNextButtonClick;
            Object.Destroy(m_view.gameObject);
        }

        private void OnNextButtonClick()
        {
            m_completionSource.TrySetResult();
        }
    }
}