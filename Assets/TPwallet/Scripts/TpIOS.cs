using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;// 加入运行时动态加载服务

namespace TpMSpcace
{
    public class TpIOS : TpApiInterface
    {
        [DllImport("__Internal")]
        public static extern void _onRegisterAppID(string appid);

        [DllImport("__Internal")]
        public static extern void _onLoginApp(string json);


        [DllImport("__Internal")]
        public static extern void _onTransfer(string json);

        public bool IsInstallTpApp()
        {
            return true;
        }

        public void onRegisterAppID(string appid)
        {
            _onRegisterAppID(appid);
        }

        public void onLoginApp(string json)
        {
            _onLoginApp(json);
        }

        public void onTransfer(string json)
        {
            _onTransfer(json);
        }
    }
}

