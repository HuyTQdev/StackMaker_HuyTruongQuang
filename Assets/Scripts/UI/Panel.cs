using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public abstract class Panel : MonoBehaviour
    {
        //public List<Transform> listScaleTrans;
        protected Animator ani;
        protected Transform _transform;
        public bool overrideBack = false;
        public virtual void Clear()
        {
            OnDestroy();
        }
        public virtual void OnDestroy()
        {

        }
        public virtual void Show()
        {
            Active();
        }
        public void ClearCloseTrigger()
        {
            ani.ResetTrigger("Close");
        }
        public virtual void Hide()
        {
            if (ani == null)
            {
                ani = GetComponent<Animator>();
            }
            if (ani != null)
            {
                ani.SetTrigger("Close");
            }
            else
            {
                Deactive();
            }
        }
        public virtual void Deactive()
        {
            gameObject.SetActive(false);
            UI.PanelManager.Instance.OnPanelHidden(this);
            PanelManager.Instance.DeRegister(this);

        }
        public virtual void Active()
        {

            gameObject.SetActive(true);
            UI.PanelManager.Instance.OnPanelShown(this);
            PanelManager.Instance.Register(this);
        }

        public virtual void ShowAfterAd() { }
        public virtual void Close()
        {
            Hide();
        }
        public abstract void PostInit();

        public virtual void OnBack()
        {
            if (overrideBack) return;
            Close();
        }

    }
}