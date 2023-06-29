using Plugins.MonoCache;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class EnemyPortals : MonoCache
    {
        [SerializeField] private Portal[] _portals;

        public void FirstSet()
        {
            for (int i = 1; i < _portals.Length; i++) 
                _portals[i].gameObject.SetActive(false);
        }

        public Transform[] GetPortals()
        {
            Transform[] tempArray = new Transform[_portals.Length];
            
            for (int i = 0; i < _portals.Length; i++) 
                tempArray[i] = _portals[i].transform;

            return tempArray;
        }
    }
}