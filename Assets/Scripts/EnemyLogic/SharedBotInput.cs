﻿using System;
using BehaviorDesigner.Runtime;

namespace EnemyLogic
{
    [Serializable]
    public class SharedBotInput : SharedVariable<BotInput>
    {
        public static implicit operator SharedBotInput(BotInput value) => 
            new SharedBotInput { Value = value };
    }
}