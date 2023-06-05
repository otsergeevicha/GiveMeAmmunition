using Cysharp.Threading.Tasks;
using Infrastructure;
using UnityEngine;

namespace AbilityLogic
{
    public class ShieldAbility : Ability
    {
        [SerializeField] private ParticleSystem _vfxShield;
        [SerializeField] private Transform _spawnPoint;
        
        private ParticleSystem _vfx;

        private void Awake()
        {
            _vfx = Instantiate(_vfxShield, _spawnPoint.position, Quaternion.identity,_spawnPoint);
            _vfx.transform.localScale = new Vector3(.03f, .03f, .03f);
        }

        public override int GetIndexAbility() =>
            (int)IndexAbility.Shield;

        public override void Cast() => 
            OffShield();
 
        private async void OffShield()
        {
            _vfx.Play();
            await UniTask.Delay(Constants.TimeLifeShield);
            _vfx.Stop();
        }
    }
}