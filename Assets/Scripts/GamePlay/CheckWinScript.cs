using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CheckWinScript: MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        EventManager.Instance.TriggerEvent("StopMoving");
        EventManager.Instance.TriggerEvent("WinGame");
    }
}
