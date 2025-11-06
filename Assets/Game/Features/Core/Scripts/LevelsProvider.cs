using System;
using Game.Saves.Scripts;
using Random = UnityEngine.Random;

namespace Game.Core
{
    public class LevelsProvider
    {
        private readonly GameStorageDataModel m_storageDataModel;
        private readonly LevelsConfig m_levelsConfig;

        public LevelsProvider(GameStorageDataModel storageDataModel, LevelsConfig levelsConfig)
        {
            m_storageDataModel = storageDataModel;
            m_levelsConfig = levelsConfig;
        }

        public LevelVariantData GetCurrentLevelVariant()
        {
            LevelVariantData levelVariant;
            
            LevelsRepeatType repeatType = m_levelsConfig.RepeatType;
            switch (repeatType)
            {
                case LevelsRepeatType.RestartSequence:
                    levelVariant = GetSequentialLevelVariant(m_levelsConfig.LevelVariants);
                    break;
                case LevelsRepeatType.StartRandom:
                    levelVariant = GetRandomLevelVariant(m_levelsConfig.LevelVariants);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            return levelVariant;
        }

        private LevelVariantData GetSequentialLevelVariant(LevelVariantData[] variants)
        {
            int currentLevelIndex = m_storageDataModel.CurrentLevelIndex % variants.Length;
            return variants[currentLevelIndex];
        }

        private LevelVariantData GetRandomLevelVariant(LevelVariantData[] variants)
        {
            int randomLevelIndex = Random.Range(0,  variants.Length - 1);
            return variants[randomLevelIndex];
        }
    }
}