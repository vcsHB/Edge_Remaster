using System;
using System.Collections;
using UnityEngine;
namespace Agents.Enemies
{

    public class EnemyRenderer : MonoBehaviour, IAgentComponent
    {
        protected Animator _animator;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] protected float _dissolveDuration = 2f;
        private readonly int _dissolveHash = Shader.PropertyToID("_DissolveLevel");
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(Agent agent)
        {

        }
        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        public void SetDissolveLevel(float dissolveLevel)
        {
            _spriteRenderer.material.SetFloat(_dissolveHash, dissolveLevel);
        }
        public void StartDissolve(Action OnComplete)
        {
            StartCoroutine(DissolveOutCoroutine(OnComplete));
        }

        private IEnumerator DissolveOutCoroutine(Action OnComplete)
        {
            float currentTime = 0f;
            while (currentTime < _dissolveDuration)
            {
                currentTime += Time.deltaTime;
                SetDissolveLevel(1f- currentTime / _dissolveDuration);
                yield return null;
            }
            SetDissolveLevel(0f);
            OnComplete?.Invoke();
        }

        public void SetParam(int hash, bool value)
        {
            _animator.SetBool(hash, value);
        }

    }
}