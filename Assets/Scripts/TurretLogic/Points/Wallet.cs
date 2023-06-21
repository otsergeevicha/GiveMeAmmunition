using System;
using Services.ServiceLocator;

namespace TurretLogic.Points
{
    public class Wallet : IWallet
    {
        public bool Check(int pricePurchase)
        {
            throw new NotImplementedException();
        }
    }
    
    public interface IWallet : IService
    {
        bool Check(int pricePurchase);
    }
}