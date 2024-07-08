using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UI
{
    public class PointPanel : Panel
    {
        [SerializeField] TextMeshProUGUI text, plusText;
        [SerializeField]float timer;
        int targetVal;
        public override void PostInit()
        {
            targetVal = PlayerPrefs.HasKey("CurGem")? PlayerPrefs.GetInt("CurGem"):0;
            text.text = targetVal.ToString();
        }
        private void OnEnable()
        {
            timer = 1f;
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
            plusText.gameObject.SetActive(true);
            plusText.text = "+ " + gemPlus;
            targetVal += gemPlus;
            PlayerPrefs.SetInt("CurGem", targetVal);
        }
    }
}