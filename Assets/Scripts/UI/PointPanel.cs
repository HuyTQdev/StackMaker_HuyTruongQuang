using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace UI
{
    public class PointPanel : Panel
    {
        [SerializeField] TextMeshProUGUI text, plusText;
        [SerializeField] float timer;
        [SerializeField] RectTransform startPos, endPos;
        [SerializeField] List<GameObject> gems; // Ensure this is populated via the Inspector
        int targetVal;

        public override void PostInit()
        {
            targetVal = PlayerPrefs.HasKey("CurGem") ? PlayerPrefs.GetInt("CurGem") : 0;
            text.text = targetVal.ToString();
        }

        private void Update()
        {
            timer -= Time.unscaledDeltaTime;
            if (timer < 0f)
            {
                gameObject.SetActive(false);
            }
            else if (timer < .5f)
            {
                text.text = targetVal.ToString();
                plusText.gameObject.SetActive(false);
            }
        }

        public void GetGem(int gemPlus)
        {
            timer = 1f;
            plusText.gameObject.SetActive(true);
            plusText.text = "+ " + gemPlus;
            targetVal += gemPlus;
            PlayerPrefs.SetInt("CurGem", targetVal);

            for (int i = 0; i < gems.Count; i++)
            {
                GameObject gem = gems[i];
                if (gem != null)
                {
                    gem.SetActive(true);
                    gem.transform.position = startPos.position;
                    gem.transform.DOKill();

                    Vector3 targetValue = Random.Range(-200f, 200f) * Vector3.right + Random.Range(-200f, 200f) * Vector3.up + startPos.position;

                    gem.transform.DOMove(targetValue, .5f)
                        .SetUpdate(true)
                        .OnComplete(() =>
                        {
                            gem.transform.DOMove(endPos.position, .5f)
                                .SetUpdate(true)
                                .OnComplete(() =>
                                {
                                    gem.SetActive(false);
                                });
                        });
                }
            }
        }
    }
}
