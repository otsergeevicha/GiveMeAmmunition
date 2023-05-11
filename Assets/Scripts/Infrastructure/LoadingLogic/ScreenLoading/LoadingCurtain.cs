using System.Collections;
using Plugins.MonoCache;
using UnityEngine;

namespace Infrastructure.LoadingLogic.ScreenLoading
{
    public class LoadingCurtain : MonoCache
    {
        [SerializeField] private CanvasGroup _curtain;

        private readonly WaitForSeconds _waitSeconds = new(Constants.RateAlfaCurtain);
        private Coroutine _coroutine;

        private void Awake() =>
            DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1;
        }

        public void Hide() => 
            StartCoroutine(DoFadeIn());

        private IEnumerator DoFadeIn()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= Constants.RateAlfaCurtain;
                yield return _waitSeconds;
            }

            gameObject.SetActive(false);
        }
    }
}