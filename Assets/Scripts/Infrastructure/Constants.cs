using UnityEngine;

namespace Infrastructure
{
    public static class Constants
    {
        //hero
        public const int HeroSpeed = 4;
        public const int SizeHeroBasket = 10;
        public const int AmmunitionDeliveryRate = 500;

        //enemy
        public const int EnemyDamage = 10;
        
        //turret
        public const int PricePurchaseTurret = 1;
    
        //regeneration
        public const float DelayRegeneration = .5f;
        public const int DelayRegenerationMagazine = 5000;
        
        //Firearms
        public const int DelayShots = 100;
        public const int FirearmsMagazineSize = 20;

        //turret
        public const int TurretMagazineSize = 10;
        public const int DelayShotsTurret = 1500;

        //grenade
        public const float RadiusExplosion = 10f;
        public const float ForceExplosion = 500f;
        public const float AngleInDegrees = 45f;
        public const int GrenadeDamage = 50;
        public const int GrenadeMagazineSize = 1;
        
        //AmmoDepot
        public const int AmmoDepotSize = 20;
        public const int TimeSpawnAirDrop = 10;
        public const int AmountAmmo = 1;
        
        //shield
        public const int TimeLifeShield = 5000;
    
        //bullet
        public const int BulletDamage = 50;
        public const float BulletSpeed = 20f;

        //pool
        public const int CountSpawnGrenade = 20;
        public const int CountSpawnBullets = 150;
    
        //curtain
        public const int RateCurtain = 3;
        public const float RateAlfaCurtain = .03f;

        //path loaded
        public const string InitialScene = "Initial";
        public const string MainScene = "Main";
        public const string PlayerPath = "Player/Hero";
        public const string HudPath = "Canvases/Hud";
        public const string CameraPath = "Camera/MainCamera";
        public const string LightPath = "View/Light";
        public const string PoolPath = "Ammo/Pool";
        public const string TurretPath = "Turrets/Turret";
        public const string TurretPointsPath = "Turrets/TurretPoints";
        public const string GrenadePath = "Ammo/Grenade";
        public const string BulletPath = "Ammo/Bullet";
    
        //check device
        public const string KeyboardMouse = "KeyboardMouse";

        //animator
        public static readonly int HeroWalkHash = Animator.StringToHash("Walk");
        public static readonly int HeroRollHash = Animator.StringToHash("Roll");
    }
}