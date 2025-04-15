using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GH
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAdminManager
    {
        // 이벤트 핸들러 정의
        event EventHandler<AdminStateChangedEventArgs> AdminStateChangeHandler;
        // 프로그램 플로우 관리 메서드
        void StateMachine(AdminStateChangedEventArgs e);
        // 이벤트 핸들러 메서드
        void OnStateMachine(object sender, AdminStateChangedEventArgs e);
        // 프로그램 초기화/시작
        void InitProgram();
        // 프로그램 완료/종료
        void CompletedProgram();
    }

    public class AdminStateChangedEventArgs : EventArgs
    {
        public AdminStateChangedEventArgs(string prevState, string newState)
        {
            PrevState = prevState;
            NewState = newState;
        }
        public string PrevState { get; set; }
        public string NewState { get; set; }
    }

}
