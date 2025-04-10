using UnityEngine;
namespace ObjectManage
{


    public class MovePointRenderer : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private Sprite _disabledSprite;
        [SerializeField] private ParticleSystem _destroyVFX;
        [SerializeField] private ParticleSystem _reviveVFX;


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetActive(bool value)
        {
            _spriteRenderer.sprite = value ? _enabledSprite : _disabledSprite;
            if (value)
                _reviveVFX.Play();
            else
                _destroyVFX.Play();
        }

    }

}