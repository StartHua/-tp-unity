using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TpMSpcace{
    public class TpAndroid : TpApiInterface
    {
        //上下文
        private AndroidJavaObject _context;
        public  AndroidJavaObject Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"); //获得Context
                }
                return _context;
            }
        }


        //android调用
        private AndroidJavaClass _jc;
        public AndroidJavaClass jc
        {
            get
            {
                if (_jc == null)
                {
                    _jc = new AndroidJavaClass("com.tokenpocket.opensdk.ForUnityTpAip");
                }
                return _jc;
            }
        }

        public bool IsInstallTpApp()
        {
            return jc.CallStatic<bool>("isTPInstall", Context);
        }

        public void onRegisterAppID(string appid)
        {
            return;
        }

        public void onLoginApp(string json)
        {
            jc.CallStatic("_onLoginApp", Context, json);
        }

        public void onTransfer(string json)
        {
            jc.CallStatic("_onTransfer", Context, json);
        }
    }
}


