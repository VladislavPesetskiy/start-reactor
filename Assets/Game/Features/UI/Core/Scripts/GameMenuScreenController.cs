using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Saves.Scripts;
using Playtika.Controllers;
using UnityEngine;

namespace Game.UI.Core.Scripts
{
    public class GameMenuScreenController : ControllerWithResultBase
    {
        private readonly ScreensFactory m_screensFactory;
        private readonly GameStorageDataModel m_storageDataModel;
        private GameMenuScreenView m_view;
        
        private UniTaskCompletionSource m_completionSource = new();
        
        public GameMenuScreenController(IControllerFactory controllerFactory, ScreensFactory screensFactory, GameStorageDataModel storageDataModel) : base(controllerFactory)
        {
            m_screensFactory = screensFactory;
            m_storageDataModel = storageDataModel;
        }

        protected override async UniTask OnFlowAsync(CancellationToken cancellationToken)
        {
            m_view = m_screensFactory.CreateScreen<GameMenuScreenView>();
            m_view.SetLevelNumber(m_storageDataModel.CurrentLevelIndex + 1);
            m_view.EventStartButtonClick += OnStartButtonClick;

            await m_completionSource.Task;
            cancellationToken.ThrowIfCancellationRequested();
            
            Complete();
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            m_view.EventStartButtonClick -= OnStartButtonClick;
            Object.Destroy(m_view.gameObject);
        }

        private void OnStartButtonClick()
        {
            m_completionSource.TrySetResult();   
        }
    }
}