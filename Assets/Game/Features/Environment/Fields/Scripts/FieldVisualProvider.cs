using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core;
using Game.Environment.Indicators;
using Game.Environment.Slots.Scripts;

namespace Game.Environment.Fields
{
    public class FieldVisualProvider
    {
        private readonly ResourcesProvider m_resourcesProvider;

        public FieldVisualProvider(ResourcesProvider resourcesProvider)
        {
            m_resourcesProvider = resourcesProvider;
        }
        
        public UniTask<ReferenceFieldView> GetReferenceFieldPrefabAsync(CancellationToken cancellationToken)
        {
            return m_resourcesProvider.LoadAsync<ReferenceFieldView>(GameConstants.PrefabsNames.ReferenceFieldView, cancellationToken);
        }
        
        public UniTask<FieldSlotView> GetReferenceFieldSlotPrefabAsync(CancellationToken cancellationToken)
        {
            return m_resourcesProvider.LoadAsync<FieldSlotView>(GameConstants.PrefabsNames.ReferenceFieldSlotView, cancellationToken);
        }
        
        public UniTask<FieldView> GetInputFieldPrefabAsync(CancellationToken cancellationToken)
        {
            return m_resourcesProvider.LoadAsync<FieldView>(GameConstants.PrefabsNames.InputFieldView, cancellationToken);
        }
        
        public UniTask<FieldSlotView> GetInputFieldSlotPrefabAsync(CancellationToken cancellationToken)
        {
            return m_resourcesProvider.LoadAsync<FieldSlotView>(GameConstants.PrefabsNames.InputFieldSlotView, cancellationToken);
        }

        public UniTask<FieldIndicatorView> GetFieldIndicatorPrefabAsync(CancellationToken cancellationToken)
        {
            return m_resourcesProvider.LoadAsync<FieldIndicatorView>(GameConstants.PrefabsNames.FieldIndicatorView, cancellationToken);
        }
    }
}