using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
        {
            EventManager.Instance.TriggerEvent("AddGem", 150);
            collision.gameObject.SetActive(false);
        } else if (collision.gameObject.CompareTag("WinPos"))
        {
            EventManager.Instance.TriggerEvent("WinGame");
        }else if (collision.gameObject.CompareTag("Chest"))
        {
            rb.velocity = Vector3.zero;
        }
    }
}
