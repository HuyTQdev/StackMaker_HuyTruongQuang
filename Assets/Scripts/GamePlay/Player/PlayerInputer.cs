using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputer : MonoBehaviour
{
    bool isMoving;
    private Vector2 startPos;

    private void Start()
    {
        isMoving = false;
    }
    public void GetStartPosition()
    {
        startPos = Input.mousePosition;
    }

    private void OnEnable()
    {
        EventManager.Instance.StartListening("StopMoving", ResetListener);
        EventManager.Instance.StartListening("Move", StopListener);
    }
    private void OnDisable()
    {
        if (!EventManager.CheckNull())
        {
            EventManager.Instance.StopListening("StopMoving", ResetListener);
            EventManager.Instance.StopListening("Move", StopListener);
        }
        }

    private void ResetListener(object[] parameters)
    {
        isMoving = false;
    }
    private void StopListener(object[] parameters)
    {
        isMoving = true;
    }
    public void GetEndPosition()
    {
        if (isMoving) return;
        Vector2 direction = (Vector2)Input.mousePosition - startPos;
        if (Vector2.SqrMagnitude(direction) < 1f) return; 
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                EventManager.Instance.TriggerEvent("Move", PlayerDirection.RIGHT); 
            }
            else
            {
                EventManager.Instance.TriggerEvent("Move", PlayerDirection.LEFT);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                isMoving = true;
                EventManager.Instance.TriggerEvent("Move", PlayerDirection.FORWARD);
            }
            else
            {
                isMoving = true;
                EventManager.Instance.TriggerEvent("Move", PlayerDirection.BACK);
            }
        }
    }
}
