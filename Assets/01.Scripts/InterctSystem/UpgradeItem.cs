using UnityEngine;
namespace InteractSystem
{
    public class UpgradeItem : InteractObject
    {
        [SerializeField] private PowerUpSO _powerUpSO;
        [SerializeField] private PlayerSkill _skillEnumType;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
            OnInteractEvent.AddListener(HandleInteract);
        }

        public void SetUpgradeInfo(PowerUpSO powerUp)
        {
            _powerUpSO = powerUp;
            _spriteRenderer.sprite = _powerUpSO.icon;
        }

        private void HandleInteract()
        {
            _powerUpSO.effectList.ForEach(effect => effect.UseEffect());
            
            Destroy(gameObject);
        }


    }
}