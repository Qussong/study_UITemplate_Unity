using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GH
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AdminManager<T> : SingletonTemplate<T>, IAdminManager where T : MonoBehaviour
    {
        public event EventHandler<AdminStateChangedEventArgs> AdminStateChangeHandler;

        public string CurAdminState { get; set; }

        protected override void Awake()
        {
            base.Awake();
            AdminStateChangeHandler += OnStateMachine;
        }

        public virtual void StateMachine(AdminStateChangedEventArgs e)
        {
            //Debug.Log($"Call {this.GetType().Name}.StateMachine()");
            if (null == AdminStateChangeHandler) return;
            AdminStateChangeHandler?.Invoke(this, e);
        }

        public virtual void OnStateMachine(object sender, AdminStateChangedEventArgs e)
        {
            //Debug.Log($"Call {this.GetType().Name}.OnStateMachine()");
            string prevState = e.PrevState;
            string newState = e.NewState;

            CloseState(prevState);
            OpenState(newState);

            CurAdminState = newState;
        }

        public abstract void InitProgram();
        public abstract void CompletedProgram();
        protected abstract void CloseState(string state);
        protected abstract void OpenState(string state);
    }

}
