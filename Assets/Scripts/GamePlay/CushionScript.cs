using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CushionScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    public PlayerDirection REDIRECT_FROM_LEFT_TO;
    public PlayerDirection REDIRECT_FROM_RIGHT_TO;
    public PlayerDirection REDIRECT_FROM_FORWARD_TO;
    public PlayerDirection REDIRECT_FROM_BACK_TO;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BOUNCE");
        if (other.gameObject.CompareTag("Player"))
        {
            EventManager.Instance.TriggerEvent("Bounce", this);
            if (animator.GetInteger("zhuanjiaoSet") != 1) animator.SetInteger("zhuanjiaoSet", 1);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if(animator.GetInteger("zhuanjiaoSet")!= 0) animator.SetInteger("zhuanjiaoSet", 0);
    }
}
