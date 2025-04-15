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
        Content1,
        None
    }

    /// <summary>
    /// 
    /// </summary>
    public class Child_ContentManager : ContentManager<Child_ContentManager>
    {
        protected override void CloseState(string state)
        {
            EContentState eState = (EContentState)Enum.Parse(typeof(EContentState), state);
            switch (eState)
            {
                case EContentState.Content1:
                    break;
                default:
                    break;
            }
        }

        protected override void OpenState(string state)
        {
            EContentState eState = (EContentState)Enum.Parse(typeof(EContentState), state);
            switch (eState)
            {
                case EContentState.Content1:
                    break;
                default:
                    break;
            }
        }

    }
}
