using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBlockScript : Singleton<AddBlockScript>
{
    int curNumBlock;
    public int CurNumBlock => curNumBlock;
    Stack<GameObject> blocks;
    [SerializeField] Transform firstBlock;
    [SerializeField] Transform playerRenderer;
    [SerializeField] Vector3 blockDistance;
    [SerializeField] Animator animator;
    GameObject go;
    private void OnEnable()
    {
        EventManager.Instance.StartListening("ResetBlock", WinGame);
        EventManager.Instance.StartListening("AddBlock", Add);
    }


    private void OnDisable()
    {
        if (!EventManager.CheckNull())
        {
            EventManager.Instance.StopListening("ResetBlock", WinGame);
            EventManager.Instance.StopListening("AddBlock", Add);
        }
    }

    private void WinGame(object[] parameters)
    {
        ResetBlocks();
        if (animator.GetInteger("renwu") != 2) animator.SetInteger("renwu", 2);
    }

    private void Start()
    {
        curNumBlock = 1;
        blocks = new Stack<GameObject>();
        blocks.Push(firstBlock.gameObject);
    }

    public void Add(object[] parameters)
    {
        if(animator.GetInteger("renwu") != 1)animator.SetInteger("renwu", 1);
        blocks.Push(ObjectPool.instance.GetObject("StackableBrick", firstBlock.transform.position + blockDistance * curNumBlock));
        playerRenderer.position += blockDistance;
        curNumBlock += 1;
    }

    public void Minus()
    {
        go = blocks.Peek();
        blocks.Pop();
        go.SetActive(false);
        playerRenderer.position -= blockDistance;
        curNumBlock -= 1;
    }
    public void ResetBlocks()
    {
        while (blocks.Count > 0)
        {
            blocks.Peek().SetActive(false);
            blocks.Pop();
        }
        playerRenderer.position = firstBlock.transform.position;
    }
}
