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
    private void Start()
    {
        curNumBlock = 1;
        blocks = new Stack<GameObject>();
        blocks.Push(firstBlock.gameObject);
    }

    public void Add()
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
}
