using UnityEngine;
using System;

namespace TpMSpcace
{

    public interface TpApiInterface{
        bool IsInstallTpApp();
        void onRegisterAppID(string appid);
        void onLoginApp(string json);
        void onTransfer(string json);
    }

    public enum TpState{
        Null =0 ,
        Success ,
        Cancle ,
        Failure
    }

    public class loginjson
    {
        //ios android
        public string dappName;
        public string dappIcon;
        public long expired;

        //ios
        public string blockchain;

        //android
        public string protocol;
        public string version;
        public string action;
        public string actionId;
        public string callbackUrl;
        public string memo;
        
        public loginjson()
        {
            dappName = TokenPocketManager.DappName;
            dappIcon = "https://gz.bcebos.com/v1/tokenpocket/temp/mobile_sdk_demo.pn";
            expired = TokenPocketManager.ConvertDateTimeToInt(DateTime.Now); 
            blockchain = "eos";
            protocol = "TPProtocol";
            version = "1.0.1";
            action = "login";
            actionId = "web-99784c28-70f0-49ff-3654-f27b137b3502";
            callbackUrl = "https://newdex.io/api/account/walletVerify";
            memo = "TokenPocket login EOS";
        }
    }

    public class transferjson
    {
        //android
        public string protocol;
        public string version;
        public string action;
        public string from;
        public string dappData; //memo
        public string callback;


        //ios android
        public string dappName;
        public string dappIcon;
        public string symbol;
        public string contract;
        public string to;
        public int precision;
        public string amount;
        public long expired;
        //ios
        public string memo;
       
        //public string actionId;
        //public string blockchain;
        public string callbackUrl;

        public transferjson()
         {
            dappName = TokenPocketManager.DappName;
            dappIcon = "https://gz.bcebos.com/v1/tokenpocket/temp/mobile_sdk_demo.pn";
            expired = TokenPocketManager.ConvertDateTimeToInt(DateTime.Now); 
            //blockchain = "eos";
            protocol = "TPProtocol";
            version = "1.0.1";
            action = "transfer";
            //actionId = "web-99784c28-70f0-49ff-3654-f27b137b3502";
            callback = "https://newdex.io/api/account/transferCallback?uuid=1-46e023fc-015b-4b76-3809-1cab3fd76d2c";
            callbackUrl = callback;
            symbol = "EOS";
            precision = 4;
         }
    }


    public class TokenPocketManager : TPSceneSingle<TokenPocketManager>
    {
        public static string Appid      = "tpTokenPocketForUnity";
        public static string DappName   = "testtp";
      
        private TpApiInterface tpApi;
        public Action<TpState> callback;

        public void Init()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            tpApi = new TpAndroid();
            #elif UNITY_IOS && !UNITY_EDITOR
            tpApi = new TpIOS();
            #else
            tpApi = new TpWin();
            #endif
        }

        public bool IsInstallTpApp()
        {
            if (tpApi == null) Init();
            return tpApi.IsInstallTpApp();
        }

        public void onRegisterAppID(string appid)
        {
            if (tpApi == null) Init();
            tpApi.onRegisterAppID(appid);
        }

        /// <summary>
        /// 登陆Tp
        /// </summary>
        public void onLoginApp()
        {
            loginjson t = new loginjson();
            onLoginApp(JsonUtility.ToJson(t));
           
        }

        public void onLoginApp(string json)
        {
            if (tpApi == null) Init();
            tpApi.onLoginApp(json);
        }

        public void onTransfer(string contract,string from,string to,float amount,string memo)
        {
            transferjson t = new transferjson();
            t.contract = contract;
            t.from = from;
            t.to = to;
            t.memo =memo;
            t.dappData = memo;
            t.amount = amount.ToString();
            onTransfer(JsonUtility.ToJson(t));
          
        }

        public void onTransfer(string json)
        {
            if (tpApi == null) Init();
            tpApi.onTransfer(json);
        }

        /// <summary>
        /// 回调
        /// </summary>
        /// <param name="state">State.</param>
        public void TpCallBakc(string state)
        {
            if (callback == null) return;
            if (state == "Success"){
                callback(TpState.Success);
            }else if (state == "Failure"){
                callback(TpState.Failure);
            }else if(state == "Cancel"){
                callback(TpState.Cancle);
            }else{
                callback(TpState.Null);
            }
        }

        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }
    }
}
