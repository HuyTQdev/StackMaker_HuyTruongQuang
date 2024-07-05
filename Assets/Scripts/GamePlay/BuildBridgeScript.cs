using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridgeScript : MonoBehaviour
{
    [SerializeField] MeshRenderer mesh;
    [SerializeField] BoxCollider collider;
    static Vector3 savePos;
    public void Build()
    {
        collider.enabled = false;
        mesh.enabled = true;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (AddBlockScript.Instance.CurNumBlock > 0)
            {
                if (AddBlockScript.Instance.CurNumBlock == 1) savePos = transform.position;
                AddBlockScript.Instance.Minus();
                Build();
            } 
            else
            {
                EventManager.Instance.TriggerEvent("StopMoving", savePos);
            }
        }
    }
}
