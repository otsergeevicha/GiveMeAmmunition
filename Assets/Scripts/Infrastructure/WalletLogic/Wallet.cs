using System;
using Services.SaveLoadLogic;
using Services.ServiceLocator;
using Services.Wallet;

namespace Infrastructure.WalletLogic
{
    public class Wallet : IWallet
    {
        private readonly ISave _save;
        private int _settlementAccount = 50;

        public Wallet() => 
            _save = ServiceLocator.Container.Single<ISave>();

        public event Action<int> Changed;

        public bool Check(int pricePurchase) => 
            _settlementAccount - pricePurchase >= 0;

        public void Spend(int price)
        {
            _settlementAccount -= Math.Clamp(price, 0, int.MaxValue);
            Notify();
            Save();
        }

        public void Apply(int amountReplenishment)
        {
            _settlementAccount += amountReplenishment;
            Notify();
            Save();
        }

        private void Notify() => 
            Changed?.Invoke(_settlementAccount);

        private void Save() => 
            _save.Add(this);
    }
}