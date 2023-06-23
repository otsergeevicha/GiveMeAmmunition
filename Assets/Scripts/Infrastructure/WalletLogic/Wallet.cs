﻿using System;
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
            _settlementAccount = _saveLoadService.Progress.DataWallet.Read;
        }

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

        private void Save()
        {
            _saveLoadService.Progress.DataWallet.Record(_settlementAccount);
            _saveLoadService.Save();
        }

        private void Notify() => 
            Changed?.Invoke(_settlementAccount);
    }
}