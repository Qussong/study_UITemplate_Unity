using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


namespace GH
{
    /// <summary>
    /// 
    /// </summary>
    public interface IContentManager
    {
        // 이벤트 핸들러 정의
        event EventHandler<ContentStateChangedEventArgs> ContentStateChangeHandler;

        // 콘텐츠 관리 메서드
        void StateMachine(ContentStateChangedEventArgs e);

        // 상태 머신 핸들러 메서드
        void OnStateMachine(object sender, ContentStateChangedEventArgs e);
    }

    public class ContentStateChangedEventArgs : EventArgs
    {
        public ContentStateChangedEventArgs(string prevState, string newState) 
        {
            PrevState = prevState;
            NewState = newState;
        }
        public string PrevState { get; set; }
        public string NewState { get; set; }
    }

}
