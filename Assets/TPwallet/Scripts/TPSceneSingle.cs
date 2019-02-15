/**

* 全局单例，转换场景不销毁

* @author ChenXingHua

* @Time 2018-10-17

*/

using UnityEngine;
using System.Collections;
namespace TpMSpcace
{
    public class TPSceneSingle<T> : MonoBehaviour where T : Component
    {
        //泛指单例
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    //寻找对象确保一个场景只有一个
                    _instance = FindObjectOfType(typeof(T)) as T;
                    if (_instance == null)
                    {
                        GameObject tempObj = new GameObject(typeof(T).Name);
                        //tempObj.hideFlags = HideFlags.HideAndDontSave; //隐藏标志（隐藏保存）
                        _instance = (T)tempObj.AddComponent(typeof(T));
                    }
                }
                return _instance;
            }
        }

        public virtual void Awake()
        {
            //转换场景不销毁
            DontDestroyOnLoad(this);
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
