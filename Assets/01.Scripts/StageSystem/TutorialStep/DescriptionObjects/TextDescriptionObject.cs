using System.Collections;
using TMPro;
using UnityEngine;
namespace StageSystem.TutorialManage
{

    public class TextDescriptionObject : DescriptionObject
    {
        [SerializeField] private TextMeshPro _textCompo;
        [SerializeField] private float _textEnableTerm = 0.06f;
        private WaitForSeconds _waitForSecond;
        private void Awake()
        {
            _waitForSecond = new WaitForSeconds(_textEnableTerm);
            _textCompo.maxVisibleCharacters = 0;
            gameObject.SetActive(false);
        }
        public override void Open()
        {
            _textCompo.maxVisibleCharacters = 0;
            gameObject.SetActive(true);
            StartCoroutine(PrintDescriptionCoroutine());

        }
        public override void Close()
        {
            StartCoroutine(DisableDescriptionCoroutine());

        }

        private IEnumerator PrintDescriptionCoroutine()
        {
            for (int i = 0; i < _textCompo.text.Length; i++)
            {
                _textCompo.maxVisibleCharacters++;
                yield return _waitForSecond;
            }

        }

        private IEnumerator DisableDescriptionCoroutine()
        {
            for (int i = 0; i < _textCompo.text.Length; i++)
            {
                _textCompo.maxVisibleCharacters--;
                yield return _waitForSecond;
            }
            gameObject.SetActive(false);
        }


    }
}