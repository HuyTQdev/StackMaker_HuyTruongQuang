using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody rb;
    [SerializeField] RaycastChecker rc;
    public Vector3 startPos => MapGenerator.Instance.playerPos;
    [SerializeField] Vector3 blockPos;
    [SerializeField] Animator animator;
    PlayerDirection saveMoving, direction;
    private void Start()
    {
        saveMoving = PlayerDirection.NONE;
        StopMoving();
    }
    private void OnEnable()
    {
        EventManager.Instance.StartListening("Move", Move);
        EventManager.Instance.StartListening("StopMoving", StopMoving);
        EventManager.Instance.StartListening("Bounce", Bounce);
    }
    private void OnDisable()
    {
        EventManager.Instance.StopListening("StopMoving", StopMoving);
        EventManager.Instance.StopListening("Move", Move);
        EventManager.Instance.StopListening("Bounce", Bounce);
    }

    private void Bounce(object[] parameters)
    {
        if (parameters.Length >0 && parameters[0] is CushionScript) {
            CushionScript cus = (CushionScript)parameters[0];
            switch(direction)
            {
                case PlayerDirection.NONE: saveMoving = PlayerDirection.NONE;
                    break;
                case PlayerDirection.LEFT:
                    saveMoving = cus.REDIRECT_FROM_LEFT_TO;
                    break;
                case PlayerDirection.RIGHT:
                    saveMoving = cus.REDIRECT_FROM_RIGHT_TO;
                    break;
                case PlayerDirection.FORWARD:
                    saveMoving = cus.REDIRECT_FROM_FORWARD_TO;
                    break;
                case PlayerDirection.BACK:
                    saveMoving = cus.REDIRECT_FROM_BACK_TO;
                    break;
            }
        }

    }

    void Move(params object[] parameters)
    {
        if (parameters.Length > 0 && parameters[0] is PlayerDirection)
        {
            direction = (PlayerDirection)parameters[0];
        }
        rc.enabled = true;
        rc.ChangeRaycast(direction);
        if (rc.CheckRaycast())
        {
            switch (direction)
            {
                case PlayerDirection.FORWARD:
                    rb.velocity = Vector3.forward * speed;
                    break;
                case PlayerDirection.BACK:
                    rb.velocity = Vector3.back * speed;
                    break;
                case PlayerDirection.LEFT:
                    rb.velocity = Vector3.left * speed;
                    break;
                case PlayerDirection.RIGHT:
                    rb.velocity = Vector3.right * speed;
                    break;
                case PlayerDirection.NONE:
                    StopMoving();
                    break;
            }
        }
    }
    public void StopMoving(params object[] parameters)
    {
        if (animator.GetInteger("renwu") != 0)
        {
            animator.SetInteger("renwu", 0);
            Debug.Log("Idle");
        }
        rb.velocity = Vector3.zero;
        //rc.enabled = false;
        if (parameters.Length > 0 && parameters[0] is Vector3 newPosition)
        {
            transform.position = newPosition + Vector3.up * (transform.position.y - newPosition.y);
        }
        else
        {
            transform.position = Mathf.Round((transform.position.x - startPos.x) / blockPos.x) * Vector3.right * blockPos.x +
                Mathf.Round((transform.position.z - startPos.z) / blockPos.z) * Vector3.forward * blockPos.z
                + startPos;
        }

        if (saveMoving != PlayerDirection.NONE)
        {
            EventManager.Instance.TriggerEvent("Move", saveMoving);
            saveMoving = PlayerDirection.NONE;
        }
    }
}
public enum PlayerDirection { NONE = 0, LEFT, RIGHT, FORWARD, BACK }
