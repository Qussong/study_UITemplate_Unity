using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

namespace GH
{
    public enum EUIPage
    {
        Page_Home,
        Page_Content,
        Page_End,
        None,
    }

    /// <summary>
    /// 
    /// </summary>
    public class Child_UIManager : UIManager<Child_UIManager>
    {
        protected override void Start()
        {
            base.Start();
        }

        public override void OnPageChanged(object sender, PageChangedEventArgs e)
        {
            base.OnPageChanged(sender, e);

            // Admin
            string adminPrevState = GetAdminState((EUIPage)e.PrevPageIdx);
            string adminNewState = GetAdminState((EUIPage)e.NewPageIdx);
            Child_AdminManager.Instance.StateMachine(new AdminStateChangedEventArgs(adminPrevState, adminNewState));

            // Content
            string contentPrevState = GetContentState((EUIPage)e.PrevPageIdx);
            string contentNewState = GetContentState((EUIPage)e.NewPageIdx);
            Child_ContentManager.Instance.StateMachine(new ContentStateChangedEventArgs(contentPrevState, contentNewState));
        }

        // UI 페이지에 대응하는 AdminState 반환
        private string GetAdminState(EUIPage page)
        {
            return page switch
            {
                EUIPage.Page_Home => EAdminState.Init.ToString(),
                EUIPage.Page_End => EAdminState.Completed.ToString(),
                _ => EAdminState.None.ToString(),
            };
        }

        private string GetContentState(EUIPage page)
        {
            return page switch
            {
                EUIPage.Page_Content => EContentState.Content_Content1.ToString(),
                _ => EContentState.None.ToString()
            };
        }

    }
}
