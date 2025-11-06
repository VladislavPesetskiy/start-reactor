using Game.Core;
using Game.Environment.Core;
using UnityEngine;

namespace Game.Environment.Fields
{
    public class FieldFactory
    {
        private readonly GameEnvironmentView m_environmentView;
        private readonly GameVisualConfig m_gameVisualConfig;

        public FieldFactory(GameEnvironmentView environmentView, GameVisualConfig gameVisualConfig)
        {
            m_environmentView = environmentView;
            m_gameVisualConfig = gameVisualConfig;
        }
        
        public ReferenceFieldView CreateReferenceField()
        {
            Transform spawnPoint = m_environmentView.ReferenceFieldPoint;
            ReferenceFieldView prefab = m_gameVisualConfig.ReferenceFieldViewPrefab;
            ReferenceFieldView instance = Object.Instantiate(prefab, spawnPoint);
            return instance;
        }

        public FieldView CreateInputField()
        {
            Transform spawnPoint = m_environmentView.InputFieldPoint;
            FieldView prefab = m_gameVisualConfig.InputFieldViewPrefab;
            FieldView instance = Object.Instantiate(prefab, spawnPoint);
            return instance;
        }
    }
}