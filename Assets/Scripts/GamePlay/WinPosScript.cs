using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPosScript : MonoBehaviour
{
    [SerializeField] ParticleSystem p1, p2;
    [SerializeField] GameObject openedChest, closedChest;
    float timer;
    bool isWin;
    private void Start()
    {
        isWin = false;
    }
    private void Update()
    {
        if(isWin) timer += Time.deltaTime;
        if (timer >= 1f)
        {
            EventManager.Instance.TriggerEvent("WinGame");
        }
        else if (timer >= .2f)
        {
            closedChest.SetActive(false);
            openedChest.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            timer = 0;
            isWin = true;
            EventManager.Instance.TriggerEvent("ResetBlock");
            p1.Play();
            p2.Play();
        }
    }
}
