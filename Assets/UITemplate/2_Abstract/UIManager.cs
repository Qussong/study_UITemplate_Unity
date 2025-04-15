using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GH
{
    /// <summary>
    ///     
    /// </summary>
    public abstract class UIManager<T> : SingletonTemplate<T>, IUIManager, IScreenInfo where T : MonoBehaviour
    {
        // UI Manager Interface
        #region IUIManager

        public event EventHandler MoveHomeHandler;
        public event EventHandler MovePrevPageHandler;
        public event EventHandler MoveNextPageHandler;
        public event EventHandler<PageChangedEventArgs> PageChangedHandler;

        [Tooltip("Page 역할을 담당하는 Panel 객체들을 관리하는 컨테이너")]
        [SerializeField] private List<GameObject> pageList = new List<GameObject>();
        public List<GameObject> PageList
        {
            get { return pageList; }
        }

        private static readonly object pageKeeper = new object();
        [Tooltip("현재 페이지 인덱스 값")]
        [ReadOnly] private int curPageIdx = 0;
        public int CurPageIdx
        {
            get
            {
                lock (pageKeeper) return curPageIdx;
            }
            set
            {
                lock (pageKeeper) curPageIdx = value;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            MoveHomeHandler += OnMoveHome;
            MovePrevPageHandler += OnMovePrevPage;
            MoveNextPageHandler += OnMoveNextPage;
            PageChangedHandler += OnPageChanged;
        }

        protected virtual void Start()
        {
            InitUI();
        }

        public virtual void InitUI()
        {
            for (int i = 0; i < pageList.Count; ++i)
            {
                DeactivateUI(i);
            }
            ActivateUI(0); // 첫번째 페이지 활성화
        }

        #region Page Change Method

        public virtual void MoveHome()
        {
            //Debug.Log($"Call {this.GetType().Name}.MoveHome()");
            if (null == MoveHomeHandler) return;
            MoveHomeHandler.Invoke(this, EventArgs.Empty);
        }

        public virtual void MovePrevPage()
        {
            //Debug.Log($"Call {this.GetType().Name}.MovePrePage()");
            if (null == MovePrevPageHandler) return;
            MovePrevPageHandler.Invoke(this, EventArgs.Empty);
        }

        public virtual void MoveNextPage()
        {
            //Debug.Log($"Call {this.GetType().Name}.MoveNextPage()");
            if (null == MoveNextPageHandler) return;
            MoveNextPageHandler.Invoke(this, EventArgs.Empty);
        }

        public void PageChanged(PageChangedEventArgs e)
        {
            //Debug.Log($"Call {this.GetType().Name}.PageChanged()");
            if (null == PageChangedHandler) return;
            PageChangedHandler?.Invoke(this, e);
        }
        #endregion

        #region Page Change Handler Method

        public virtual void OnMoveHome(object sender, EventArgs e)
        {
            PageChanged(new PageChangedEventArgs(CurPageIdx, 0));
        }

        public virtual void OnMovePrevPage(object sender, EventArgs e)
        {
            int maxPageCnt = PageList.Count;
            int newPageIdx = CurPageIdx - 1;
            // 첫번째 페이지인 경우
            if (newPageIdx < 0) return;
            // 페이지 이동
            PageChanged(new PageChangedEventArgs(CurPageIdx, newPageIdx));
        }

        public virtual void OnMoveNextPage(object sender, EventArgs e)
        {
            int maxPageCnt = PageList.Count;
            int newPageIdx = CurPageIdx + 1;
            // 마지막 페이지인 경우
            if (newPageIdx >= maxPageCnt) return;
            // 페이지 이동
            PageChanged(new PageChangedEventArgs(CurPageIdx, newPageIdx));
        }

        public virtual void OnPageChanged(object sender, PageChangedEventArgs e)
        {
            DeactivateUI(e.PrevPageIdx);
            ActivateUI(e.NewPageIdx);
            CurPageIdx = e.NewPageIdx;

            // 페이지 이동에 따른 추가 로직은 자식 클래스에서 구현

        }
        #endregion

        #region UI Status Method

        public virtual bool HasUIElement<E>(E enumId) where E : Enum
        {
            if (enumId is E page)
            {
                return HasUIElement(Convert.ToInt32(page));
            }
            Debug.LogError("An invalid type has been provided.");
            return false;
        }

        protected virtual bool HasUIElement(int enumId)
        {
            if (null != PageList[enumId]) return true;

            Debug.LogError("An invalid parameter value has been provided.");
            return false;
        }
        #endregion 

        #region Set UI Activity Method

        public virtual void ActivateUI<E>(E enumId) where E : Enum
        {
            if (enumId is E page)
            {
                ActivateUI(Convert.ToInt32(page));
            }
        }

        public virtual void ActivateUI(int enumId)
        {
            GameObject panel = PageList[enumId];
            if (null == panel) return;

            panel.SetActive(true);
        }

        public virtual void DeactivateUI<E>(E enumId) where E : Enum
        {
            if (enumId is E page)
            {
                DeactivateUI(Convert.ToInt32(page));
            }
        }

        public virtual void DeactivateUI(int enumId)
        {
            GameObject panel = PageList[enumId];
            if (null == panel) return;

            panel.SetActive(false);
        }
        #endregion

        #endregion

        // Screen Info Interface
        #region ScreenInfo

        public Vector2 GetScreenPixel()
        {
            return new Vector2(Screen.width, Screen.height);
        }

        public Vector2 GetScreenResolution()
        {
            return new Vector2(Display.main.systemWidth, Display.main.systemHeight);
        }

        public float GetScreenAspectRatio()
        {
            return (float)Screen.width / (float)Screen.height;
        }

        #endregion
    }
}

