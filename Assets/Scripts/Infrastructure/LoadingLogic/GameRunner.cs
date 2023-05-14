using UnityEngine;

namespace Infrastructure.LoadingLogic
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _gameBootstrapper;

        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper != null)
                return;

            Instantiate(_gameBootstrapper);
        }
    }
}