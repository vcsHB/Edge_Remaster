using System.Collections;
using UnityEngine;
namespace Combat.CombatObjects
{

    public class BarrierVisual : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private readonly int _blinkLevelHash = Shader.PropertyToID("_BlinkLevel");
        private readonly int _hitPositionHash = Shader.PropertyToID("_HitPosition");
        private readonly int _barrierDissolveLevel = Shader.PropertyToID("_BarrierDissolveLevel");

        [SerializeField] private float _blinkEffectDuration = 0.2f;
        [SerializeField] private float _barrierMinDurabilityLevel = 0.4f;
        private Coroutine _currentEffectCoroutine;
        private Material _mateiral;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _mateiral = _spriteRenderer.material;
        }

        public void StartHitEffect(Vector2 hitPosition)
        {
            SetHitPosition(hitPosition);
            if (_currentEffectCoroutine != null)
                StopCoroutine(_currentEffectCoroutine);

            _currentEffectCoroutine = StartCoroutine(HitEffectRoutine());
        }

        private IEnumerator HitEffectRoutine()
        {
            float currentTime = 0f;
            while (currentTime < _blinkEffectDuration)
            {
                currentTime += Time.deltaTime;
                float ratio = currentTime / _blinkEffectDuration;
                float value = Mathf.Sin(ratio * Mathf.PI);
                SetBlinkLevel(value);
                yield return null;
            }
            SetBlinkLevel(0f);

        }

        private void SetBlinkLevel(float blinkLevel)
        {
            _mateiral.SetFloat(_blinkLevelHash, blinkLevel);
        }

        private void SetHitPosition(Vector2 hitPosition)
        {
            _mateiral.SetVector(_hitPositionHash, hitPosition);

        }
        public void HandleBarrierDurabilityChange(float current, float max)
        {
            float ratio = current / max;
            SetDissolveLevel(Mathf.Lerp(_barrierMinDurabilityLevel, 1f, ratio));
        }

        public void SetDissolveLevel(float value)
        {
            _mateiral.SetFloat(_barrierDissolveLevel, value);
        }

        public void SetVisualEnable(bool value)
        {
            _spriteRenderer.enabled = value;
        }

    }
}