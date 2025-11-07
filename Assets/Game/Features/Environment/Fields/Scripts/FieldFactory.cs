using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Environment.Core;
using Game.Environment.Indicators;
using Game.Environment.Slots.Scripts;
using UnityEngine;

namespace Game.Environment.Fields
{
    public class FieldFactory
    {
        private readonly GameEnvironmentView m_environmentView;
        private readonly FieldVisualProvider m_visualProvider;

        public FieldFactory(GameEnvironmentView environmentView, FieldVisualProvider visualProvider)
        {
            m_environmentView = environmentView;
            m_visualProvider = visualProvider;
        }

        public async UniTask<ReferenceFieldView> CreateReferenceFieldView(CancellationToken cancellationToken)
        {
            ReferenceFieldView prefab = await m_visualProvider.GetReferenceFieldPrefabAsync(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            Transform spawnPoint = m_environmentView.ReferenceFieldPoint;
            ReferenceFieldView instance = Object.Instantiate(prefab, spawnPoint);
            
            return instance;
        }

        public async UniTask<FieldView> CreateInputFieldView(CancellationToken cancellationToken)
        {
            FieldView prefab = await m_visualProvider.GetInputFieldPrefabAsync(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            Transform spawnPoint = m_environmentView.InputFieldPoint;
            FieldView instance = Object.Instantiate(prefab, spawnPoint);
            
            return instance;
        }

        public FieldSlotView CreateFieldSlotView(FieldSlotView prefab, Transform parent)
        {
            FieldSlotView instance = Object.Instantiate(prefab, parent);
            
            return instance;
        }

        public FieldIndicatorView CreateFieldIndicatorView(FieldIndicatorView prefab, Transform parent)
        {
            FieldIndicatorView instance = Object.Instantiate(prefab, parent);
            
            return instance;
        }
    }
}