using Game.Environment.Core;
using Game.Environment.Fields;
using Game.Environment.Slots.Scripts;
using Game.GameLogic.Scripts;
using Game.Saves.Scripts;
using Game.UI.Core.Scripts;
using Playtika.Controllers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Core
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private GameView m_gameView;
        
        [SerializeField]
        private GameVisualConfig m_gameVisualConfig;
        
        [SerializeField]
        private LevelsConfig m_levelsConfig;
        
        [SerializeField]
        private GameScreensConfig m_screensConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Bootstrap>();
            builder.Register<IControllerFactory, ControllerFactory>(Lifetime.Scoped);
            builder.Register<BootstrapController>(Lifetime.Transient);
            builder.Register<GameLoopController>(Lifetime.Transient);

            builder.RegisterInstance(m_gameView.GameEnvironmentView);
            builder.RegisterInstance(m_gameView.GameUIView);
            builder.RegisterInstance(m_gameVisualConfig);
            builder.RegisterInstance(m_levelsConfig);
            builder.RegisterInstance(m_screensConfig);
            
            builder.Register<GameModel>(Lifetime.Singleton);
            builder.Register<FieldEventsModel>(Lifetime.Singleton);
            builder.Register<IGameEventsModel, IGameEventsRequestsModel, GameEventsModel>(Lifetime.Singleton);
            builder.Register<IFieldEventsModel, IFieldEventsRequestsModel, FieldEventsModel>(Lifetime.Singleton);
            
            builder.Register<GameEnvironmentController>(Lifetime.Transient);
            builder.Register<GameLogicController>(Lifetime.Transient);
            builder.Register<GameUIController>(Lifetime.Transient);
            builder.Register<GameStorageController>(Lifetime.Transient);
            builder.Register<InputFieldController>(Lifetime.Transient);
            builder.Register<ReferenceFieldController>(Lifetime.Transient);
            builder.Register<InputFieldSlotController>(Lifetime.Transient);
            builder.Register<ReferenceFieldSlotController>(Lifetime.Transient);
            builder.Register<ReferenceFieldSequenceController>(Lifetime.Transient);
            builder.Register<FieldSlotInteractableController>(Lifetime.Transient);
            builder.Register<GameWinScreenController>(Lifetime.Transient);
            
            builder.Register<LevelsProvider>(Lifetime.Singleton);
            builder.Register<FieldFactory>(Lifetime.Singleton);
            builder.Register<FieldSlotFactory>(Lifetime.Singleton);
            builder.Register<ResourcesProvider>(Lifetime.Singleton);
            builder.Register<GameStorageDataModel>(Lifetime.Singleton);
            builder.Register<ScreensFactory>(Lifetime.Singleton);
        }
    }
}