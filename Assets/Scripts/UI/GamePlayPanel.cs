using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UI
{
    public class GamePlayPanel : Panel
    {
        [SerializeField] TextMeshProUGUI textBlock, textGem, levelText;

        int curBlock, curGem;

        public override void PostInit()
        {
            curBlock = 0;
            curGem = PlayerPrefs.GetInt("CurGem");
            textBlock.text = curBlock.ToString();
            textGem.text = curGem.ToString();
            levelText.text = "Level " + (PlayerPrefs.GetInt("CurLevel") + 1);
        }
        public void AddBlock()
        {
            curBlock += 1;
            textBlock.text = curBlock.ToString();
        }
        public void AddGem(int gem)
        {
            curGem += gem;
            textGem.text = curGem.ToString();
        }
    }
}
