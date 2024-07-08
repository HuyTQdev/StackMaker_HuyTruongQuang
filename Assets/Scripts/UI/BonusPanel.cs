using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class BonusPanel:Panel
    {
        [SerializeField] RectTransform rollBar;
        [SerializeField] Image clickImage;
        [SerializeField] Sprite graySprite, greenSprite;
        [SerializeField] float speed;
        [SerializeField] float focusX;
        [SerializeField] float blockLength;
        [SerializeField] TextMeshProUGUI multipleText;
        [SerializeField] TextMeshProUGUI valueText;
        [SerializeField] int value;
        [SerializeField] Animator animator;
        int targetValue;
        bool isRolling;
        public void OpenBonus(int gemVal)
        {
            value = gemVal;
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            animator.SetTrigger("open");
            isRolling = false;
            targetValue = value;
            valueText.text = "+" + value;
            clickImage.sprite = greenSprite;
            rollBar.anchoredPosition += Vector2.left * (rollBar.anchoredPosition.x - 3867);
        }
        private void Update()
        {
            if (isRolling)
            {
                if (rollBar.anchoredPosition.x > focusX)
                {
                    rollBar.anchoredPosition += Time.unscaledDeltaTime * speed * Vector2.left;
                }
                else
                {
                    multipleText.rectTransform.DOScale(1.1f, .05f).SetUpdate(true).OnComplete(() =>
                    {
                        multipleText.rectTransform.DOScale(1f, .05f);
                    });
                    rollBar.anchoredPosition += Vector2.left * (rollBar.anchoredPosition.x - focusX);
                    isRolling = false;
                }
            }
            else if(targetValue != value)
            {
                value += (int)(speed * Time.unscaledDeltaTime);
                if (value >= targetValue)
                {
                    value = targetValue;
                    StartCoroutine(WaitAddGem());
                }
                valueText.text = "+" + value;
            }
        }
        IEnumerator WaitAddGem()
        {
            EventManager.Instance.TriggerEvent("GetGem", value);
            yield return new WaitForSecondsRealtime(1f);
            multipleText.DOKill();
            animator.SetTrigger("close");
            multipleText.rectTransform.DOScale(0, .5f).SetUpdate(true).OnComplete(() =>
            {
                Time.timeScale = 1;
                gameObject.SetActive(false);
                });
        }
        public void OnRoll()
        {
            if (isRolling) return;
            clickImage.sprite = graySprite;
            int p = Random.Range(10, 30);
            multipleText.text = "x " + Calculate(p);
            targetValue = (int)(value * Calculate(p));
            focusX = rollBar.anchoredPosition.x - p * blockLength;
            isRolling = true;
        }
        float Calculate(int p)
        {
            float ans = 0;
            switch (p % 9)
            {
                case 0:
                    ans = 2;
                    break;
                case 1:
                    ans = 2.5f;
                    break;
                case 2:
                    ans = 3;
                    break;
                case 3:
                    ans = 4;
                    break;
                case 4:
                    ans = 5;
                    break;
                case 5:
                    ans = 7;
                    break;
                case 6:
                    ans = 10;
                    break;
                case 7:
                    ans = Random.RandomRange(1, 20) * .5f;
                    break;
                case 8:
                    ans = 1.5f;
                    break;
            }
            return ans;
        }

        public override void PostInit()
        {
        }
    }
    
}
