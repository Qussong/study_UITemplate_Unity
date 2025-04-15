using System;
using System.Collections;
using System.Collections.Generic;
using GH;
using UnityEngine;

namespace GH
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ContentManager<T> : SingletonTemplate<T>, IContentManager where T : MonoBehaviour
    {
        public string CurContentState { get; set; }

        public event EventHandler<ContentStateChangedEventArgs> ContentStateChangeHandler;

        protected override void Awake()
        {
            base.Awake();
            ContentStateChangeHandler += OnStateMachine;
        }

        public virtual void StateMachine(ContentStateChangedEventArgs e)
        {
            if (null == ContentStateChangeHandler) return;
            ContentStateChangeHandler?.Invoke(this, e);
        }

        public virtual void OnStateMachine(object sender, ContentStateChangedEventArgs e)
        {
            string prevState = e.PrevState;
            string newState = e.NewState;

            CloseState(prevState);
            OpenState(newState);

            CurContentState = newState;
        }

        protected abstract void CloseState(string state);
        protected abstract void OpenState(string state);

    }
}

