using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastChecker : MonoBehaviour
{
    RaycastHit hit;
    Vector3 baseRay;
    [SerializeField] float distance;
    [SerializeField] Transform startPoint;
    [SerializeField] LayerMask blockLayer;
    [SerializeField] AddBlockScript addBlockScript;

    public void ChangeRaycast(PlayerDirection direction)
    {
        switch (direction)
        {
            case PlayerDirection.FORWARD:
                baseRay = Vector3.forward;
                break;
            case PlayerDirection.BACK:
                baseRay = Vector3.back;
                break;
            case PlayerDirection.LEFT:
                baseRay = Vector3.left;
                break;
            case PlayerDirection.RIGHT:
                baseRay = Vector3.right;
                break;
        }
    }
    private void FixedUpdate()
    {
        CheckRaycast();
    }

    public bool CheckRaycast()
    {
        Debug.DrawLine(startPoint.position, startPoint.position + baseRay * distance);
        bool wasHit = Physics.Raycast(startPoint.position, baseRay, out hit, distance, blockLayer);
        if (wasHit)
        {
            if(hit.collider.CompareTag("EatableBrick"))
            {
                hit.collider.gameObject.SetActive(false);
                EventManager.Instance.TriggerEvent("AddBlock");
                return true;
            }
            else if (hit.collider.CompareTag("Wall"))
            {
                EventManager.Instance.TriggerEvent("StopMoving");
                return false;
            }
        }
        return true;
    }
}


