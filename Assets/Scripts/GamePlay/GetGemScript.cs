using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGemScript : MonoBehaviour
{
    [SerializeField] int point;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.Instance.TriggerEvent("AddGem", point);
            gameObject.SetActive(false);
        }
    }
}
