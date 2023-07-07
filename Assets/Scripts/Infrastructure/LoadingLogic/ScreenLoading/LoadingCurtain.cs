using Cysharp.Threading.Tasks;
using Plugins.MonoCache;
using UnityEngine;

namespace Infrastructure.LoadingLogic.ScreenLoading
{
    public class LoadingCurtain : MonoCache
    {
        [SerializeField] private CanvasGroup _curtain;

        private Coroutine _coroutine;

        private void Awake() =>
            DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1;
        }

        public void Hide() => 
           _ = FadeIn();

        private async UniTaskVoid FadeIn()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= Constants.RateAlfaCurtain;
                await UniTask.Delay(Constants.RateCurtain);
            }

            gameObject.SetActive(false);
        }
    }
}