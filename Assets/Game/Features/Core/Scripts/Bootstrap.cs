using System;
using System.Threading;
using VContainer.Unity;

namespace Game.Core
{
    public class Bootstrap : IStartable, IDisposable
    {
        private readonly BootstrapController m_bootstrapController;

        private CancellationTokenSource m_cancellationTokenSource = new();

        public Bootstrap(BootstrapController bootstrapController)
        {
            m_bootstrapController = bootstrapController;
        }

        public void Start()
        {
            try
            {
                m_bootstrapController.LaunchTree(m_cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogError($"Can't start bootstrap controller {exception}");
            }
        }

        public void Dispose()
        {
            m_cancellationTokenSource?.Cancel();
            m_cancellationTokenSource?.Dispose();

            m_cancellationTokenSource = null;
        }
    }
}