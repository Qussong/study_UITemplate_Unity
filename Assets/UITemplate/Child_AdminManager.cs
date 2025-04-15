using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GH
{
    public enum EAdminState
    {
        Init,       // 초기화 중 (=Initializing)
        Running,    // 실행 중
        Completed,  // 완료 됨
        Idle,       // 대기 상태
        Error,      // 오류 발생
        Paused,     // 일시 정지
        Restarting, // 재시작
        None,
    }

    /// <summary>
    /// 
    /// </summary>
    public class Child_AdminManager : AdminManager<Child_AdminManager>
    {
        public void Start()
        {
            // 초기값 설정 (prev : none , new : init)
            string prevState = EAdminState.None.ToString();
            string newState = EAdminState.Init.ToString();
            StateMachine(new AdminStateChangedEventArgs(prevState, newState));
        }

        protected override void CloseState(string state)
        {
            EAdminState eState = (EAdminState)Enum.Parse(typeof(EAdminState), state);
            switch (eState)
            {
                case EAdminState.Init:
                    break;
                case EAdminState.Running:
                    break;
                case EAdminState.Completed:
                    break;
                default:
                    break;
            }
        }

        protected override void OpenState(string state)
        {
            EAdminState eState = (EAdminState)Enum.Parse(typeof(EAdminState), state);
            switch (eState)
            {
                case EAdminState.Init:
                    InitProgram();
                    break;
                case EAdminState.Running:
                    break;
                case EAdminState.Completed:
                    CompletedProgram();
                    break;
                default:
                    break;
            }
        }

        public override void InitProgram()
        {
            // 프로그램 초기화 로직
        }

        public override void CompletedProgram()
        {
            // 프로그램 종료 로직
        }

    }
}
