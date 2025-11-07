using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Core
{
    public class GameModel
    {
        private readonly LevelsProvider m_levelsProvider;

        public event Action<int> EventIterationPointerChanged;
        public event Action<int> EventIterationChanged;
        public LevelVariantData CurrentLevelVariant { get; private set; }
        public Vector2[] ReferenceSequence { get; private set; }
        public int CurrentIterationPointerIndex { get; private set; } = 0;
        public int CurrentLevelIterationIndex { get; private set; } = 0;
        public bool IsFieldInteractableEnabled { get; private set; } = false;

        public GameModel(LevelsProvider levelsProvider)
        {
            m_levelsProvider = levelsProvider;
        }
        
        public void Initialize()
        {
            CurrentLevelVariant = m_levelsProvider.GetCurrentLevelVariant();
            CurrentLevelIterationIndex = 0;
            CurrentIterationPointerIndex = 0;
            IsFieldInteractableEnabled = false;
            
            CreateTargetSequence();
        }

        public void IncreaseIterationPointer()
        {
            CurrentIterationPointerIndex++;
            EventIterationPointerChanged?.Invoke(CurrentIterationPointerIndex);
        }

        public void IncreaseIterationIndex()
        {
            CurrentLevelIterationIndex++;
            EventIterationChanged?.Invoke(CurrentLevelIterationIndex);
            
            if (CurrentLevelIterationIndex >= CurrentLevelVariant.Iterations.Length)
            {
                return;
            }
            
            CreateTargetSequence();
        }

        public void ResetIterationPointer()
        {
            CurrentIterationPointerIndex = 0;
            EventIterationPointerChanged?.Invoke(CurrentIterationPointerIndex);
        }

        public void SetFieldInputEnabled(bool enabled)
        {
            IsFieldInteractableEnabled = enabled;
        }

        public bool IsCorrectSlot(Vector2 slotCoord)
        {
            Vector2 currentCoord = ReferenceSequence[CurrentIterationPointerIndex];
            return slotCoord == currentCoord;
        }

        public bool IsInputSequenceFilled()
        {
            return CurrentIterationPointerIndex >= ReferenceSequence.Length;
        }

        public bool IsIterationsCompleted()
        {
            return CurrentLevelIterationIndex >= CurrentLevelVariant.Iterations.Length;
        }
        
        private void CreateTargetSequence()
        {
            int sequenceLength = CurrentLevelVariant.Iterations[CurrentLevelIterationIndex];
            ReferenceSequence = new Vector2[sequenceLength];
            for (int i = 0; i < sequenceLength; i++)
            {
                int randomX = Random.Range(0, CurrentLevelVariant.FieldSizeX);
                int randomY = Random.Range(0, CurrentLevelVariant.FieldSizeY);
                
                ReferenceSequence[i] = new Vector2(randomX, randomY);
            }
        }
    }
}