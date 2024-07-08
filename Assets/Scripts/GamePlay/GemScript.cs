using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    int value;
    bool isBonus;

    public void Init(int value, bool isBonus)
    {
        this.value = value;
        this.isBonus = isBonus;
    }

    public void GetGem()
    {
        if (isBonus)
        {
            EventManager.Instance.TriggerEvent("Bonus", value);
            Time.timeScale = 0;
        }
        else EventManager.Instance.TriggerEvent("GetGem", value);
        gameObject.SetActive(false);
    }
}
