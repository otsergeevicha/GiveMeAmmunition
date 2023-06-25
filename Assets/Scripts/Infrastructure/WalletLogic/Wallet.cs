using System;
using Services.SaveLoadLogic;
using Services.Wallet;

namespace Infrastructure.WalletLogic
{
    public class Wallet : IWallet
    {
        private readonly ISave _saveLoadService;
        private int _settlementAccount;

        public Wallet(ISave saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _settlementAccount = _saveLoadService.AccessProgress().DataWallet.Read();
        }

        public event Action<int> Changed;

        public bool Check(int pricePurchase) => 
            _settlementAccount - pricePurchase >= 0;

        public void Spend(int price)
        {
            _settlementAccount -= Math.Clamp(price, 0, int.MaxValue);
            Notify();
            WritingSave();
        }

        public void Apply(int amountReplenishment)
        {
            _settlementAccount += amountReplenishment;
            Notify();
            WritingSave();
        }

        private void WritingSave()
        {
            _saveLoadService.AccessProgress().DataWallet.Record(_settlementAccount);
            _saveLoadService.Save();
        }

        private void Notify() => 
            Changed?.Invoke(_settlementAccount);
    }
}