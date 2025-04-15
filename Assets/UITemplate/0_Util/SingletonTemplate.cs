using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GH
{
    // Marker Interface
    public interface IScenePersistent { }

    // Singleton Template
    public class SingletonTemplate<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;
        private static readonly object locker = new object();

        public static T Instance
        {
            get
            {
                if (null == instance)
                {
                    lock (locker)
                    {
                        instance = FindByType();
                        if (null == instance)
                        {
                            GameObject obj = new GameObject(typeof(T).Name);
                            instance = obj.AddComponent<T>();

                            if (instance is IScenePersistent)
                            {
                                DontDestroyOnLoad(obj);
                            }
                        }
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            // 중복된 객체 생성 방지
            if (instance != null && instance != this)
            {
                Debug.LogWarning("Duplicate " + typeof(T).Name + " instance found, destroying this one.");
                Destroy(gameObject);
                return;
            }

            instance = this as T;   // this가 T로 형변환이 불가능하면 null 반환
            if (instance is IScenePersistent)
            {
                DontDestroyOnLoad(gameObject);
            }

            Debug.Log("Create \"" + typeof(T).Name + "\" Singleton Class");
        }

        // 싱글톤 비활성화
        public static void DisableSingleton()
        {
            Debug.Log("Disable " + typeof(T).Name);
            if (null != instance)
            {
                instance.gameObject.SetActive(false);
            }
        }

        // 싱글톤 활성화
        public static void EnableSingleton()
        {
            Debug.Log("Enable " + typeof(T).Name);
            if (null != instance)
            {
                instance.gameObject.SetActive(true);
            }
        }

        // 싱글톤 삭제
        public static void DestroySingleton()
        {
            if (instance != null)
            {
                Debug.Log("Destroy " + typeof(T).Name);
                Destroy(instance.gameObject);
                instance = null;
            }
        }

        // T 타입 컴포넌트 탐색
        private static T FindByType()
        {
            List<T> allObjects = new List<T>();
            Scene curScene = SceneManager.GetActiveScene();
            GameObject[] rootObjects = curScene.GetRootGameObjects();

            foreach (var rootObject in rootObjects)
            {
                allObjects.AddRange(rootObject.GetComponentsInChildren<T>(true));
            }

            if (1 == allObjects.Count)
            {
                return allObjects[0];
            }
            else if (1 < allObjects.Count)
            {
                Debug.LogError("Multiple instance of " + typeof(T).Name + " exist.");
            }

            return null;
        }

    }
}
