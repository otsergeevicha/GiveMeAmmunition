using System;

namespace AbilityLogic.Cartridges
{
    public interface IMagazine
    {
        void Spend();
        bool Check();

        void Replenishment(Action fulled);

        void Shortage();
    }
}