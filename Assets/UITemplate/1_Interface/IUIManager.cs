using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;

namespace GH
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUIManager
    {
        // 이벤트 핸들러 정의
        event EventHandler MoveHomeHandler;
        event EventHandler MovePrevPageHandler;
        event EventHandler MoveNextPageHandler;
        event EventHandler<PageChangedEventArgs> PageChangedHandler;

        // 페이지 이동 메서드
        void MoveHome();
        void MovePrevPage();
        void MoveNextPage();
        void PageChanged(PageChangedEventArgs e);

        // 페이지 이동 핸들러 메서드
        void OnMoveHome(object sender, EventArgs e);
        void OnMovePrevPage(object sender, EventArgs e);
        void OnMoveNextPage(object sender, EventArgs e);
        void OnPageChanged(object sender, PageChangedEventArgs e);

        // UI 상태 정보
        bool HasUIElement<T>(T enumId) where T : Enum;

        //  UI 활성화/비활성화
        void ActivateUI<T>(T enumId) where T : Enum;
        void DeactivateUI<T>(T enumId) where T : Enum;
    }

    /// <summary>
    /// 페이지 변경 이벤트 데이터
    /// </summary>
    public class PageChangedEventArgs : EventArgs
    {
        public PageChangedEventArgs(int prevPageIdx, int newPageIdx) 
        {
            PrevPageIdx = prevPageIdx;
            NewPageIdx = newPageIdx;
        }
        public int PrevPageIdx { get; set; }
        public int NewPageIdx { get; set; }
    }

}
