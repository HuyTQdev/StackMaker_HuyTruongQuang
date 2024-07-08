﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PanelManager : UnityEngine.MonoBehaviour
    {
        public static PanelManager Instance;
        [SerializeField]
        private List<Panel> panels = new List<Panel>();
        private Stack<Panel> stack = new Stack<Panel>();

        // Start is called before the first frame update
        void Start()
        {
            Init();
        }
        private void OnDestroy()
        {
            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].Clear();
                panels[i].OnDestroy();
                panels[i] = null;
            }

        }
        public void Register(Panel panel)
        {
            if (panels.Contains(panel)) return;
            panels.Add(panel);
        }
        public void DeRegister(Panel panel)
        {
            if (!panels.Contains(panel)) return;
            panels.Remove(panel);
        }
        public void Init()
        {
            Instance = this;
            Transform holder = transform;
            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].PostInit();
            }
            for (int i = 0; i < holder.childCount; i++)
            {
                Panel panel = holder.GetChild(i).GetComponent<Panel>();
                if (panel == null) continue;
                try
                {
                    panel.PostInit();
                    panels.Add(panel);
                }
                catch (System.Exception e) { Debug.LogError(panel.gameObject.name + " \n" + e); }
            }
        }


        public void OnPanelShown(UI.Panel panel)
        {
            if (!stack.Contains(panel))
            {
                //Debug.Log("Show :" + panel.gameObject.name);
                stack.Push(panel);
            }
        }
        public void OnPanelHidden(UI.Panel panel)
        {
            if (stack.Count > 0 && stack.Peek() == panel)
            {
                Debug.Log("HIDE :" + panel.gameObject.name);
                stack.Pop();
            }
        }
        public Panel GetPanel(int index)
        {
            return panels[index];
        }
        private void OnEnable()
        {
            EventManager.Instance.StartListening("Bonus", OpenBonus);
            EventManager.Instance.StartListening("GetGem", GetGem);
            EventManager.Instance.StartListening("AddBlock", AddBlock);
        }


        private void OnDisable()
        {
            if (!EventManager.CheckNull())
            {
                EventManager.Instance.StopListening("Bonus", OpenBonus);
                EventManager.Instance.StopListening("GetGem", GetGem);
                EventManager.Instance.StopListening("AddBlock", AddBlock);
            }
        }

        private void OpenBonus(object[] parameters)
        {
            BonusPanel bonusPanel = (BonusPanel)GetPanel(1);
            bonusPanel.gameObject.SetActive(true);
            bonusPanel.OpenBonus((int)parameters[0]);
        }

        private void GetGem(object[] parameters)
        {
            PointPanel pointPanel = (PointPanel)GetPanel(2);
            ((GamePlayPanel)GetPanel(0)).AddGem((int)parameters[0]);
            pointPanel.gameObject.SetActive(true);
            pointPanel.GetGem((int)parameters[0]);
        }

        private void AddBlock(object[] parameters)
        {
            ((GamePlayPanel)GetPanel(0)).AddBlock();
        }
    }
}