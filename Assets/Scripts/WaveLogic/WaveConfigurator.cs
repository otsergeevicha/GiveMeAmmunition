﻿using System;
using Infrastructure;

namespace WaveLogic
{
    public class WaveConfigurator
    {
        private readonly DataWave _dataWaves = new ();
        
        public WaveConfigurator(int currentLevel)
        {
            switch (currentLevel)
            {
                case (int)IndexLevel.One:
                    LevelOne((int)IndexLevel.One);
                    break;
                
                case (int)IndexLevel.Two:
                    LevelTwo((int)IndexLevel.Two);
                    break;
                
                case (int)IndexLevel.Three:
                    LevelThree((int)IndexLevel.Three);
                    break;
                default:
                    LevelOne((int)IndexLevel.One);
                    break;
            }
        }

        public DataWave Get() =>
            _dataWaves;

        private void LevelOne(int currentLevel) =>
            _dataWaves.InjectDependency(currentLevel, Constants.TimeLevelOne,
                Constants.TurtlePath, Constants.SlimePath, Constants.SpiderPath);

        private void LevelTwo(int currentLevel) =>
            _dataWaves.InjectDependency(currentLevel, Constants.TimeLevelTwo,
                Constants.BatPath, Constants.EvilMagePath, Constants.DragonPath);

        private void LevelThree(int currentLevel) =>
            _dataWaves.InjectDependency(currentLevel, Constants.TimeLevelThree,
                Constants.GolemPath, Constants.MonsterPlantPath, Constants.OrcPath);
    }
}