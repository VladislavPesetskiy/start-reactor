using Playtika.Controllers;
using VContainer;

namespace Game.Core
{
    public class ControllerFactory : IControllerFactory
    {
        private readonly IObjectResolver m_container;

        public ControllerFactory(IObjectResolver container)
        {
            m_container = container;
        }

        public IController Create<T>() where T : class, IController
        {
            var controller = m_container.Resolve<T>();
            return controller;
        }
    }
}