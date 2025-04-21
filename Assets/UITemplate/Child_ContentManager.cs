using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GH
{
    public enum EContentState
    {
        Content_Content1,
        None
    }

    /// <summary>
    /// 
    /// </summary>
    public class Child_ContentManager : ContentManager<Child_ContentManager>
    {
        [Header("Content : Content 1")]
        [Tooltip("Content1 에 필요한 변수 1")]
        [SerializeField] int a;
        [Tooltip("Content1 에 필요한 변수 2")]
        [SerializeField] bool b;

        private void Init_Content1()
        {
            a = 0;
            b = false;
        }

        public void Start()
        {
            Init_Content1();
        }


        protected override void OpenState(string state)
        {
            EContentState eState = (EContentState)Enum.Parse(typeof(EContentState), state);
            switch (eState)
            {
                case EContentState.Content_Content1:
                    Start_Content1();
                    break;
                default:
                    break;
            }
        }

        private void Start_Content1()
        {
            //
        }

        protected override void CloseState(string state)
        {
            EContentState eState = (EContentState)Enum.Parse(typeof(EContentState), state);
            switch (eState)
            {
                case EContentState.Content_Content1:
                    End_Content1();
                    break;
                default:
                    break;
            }
        }

        private void End_Content1()
        {
            Init_Content1();
        }
    }
}
