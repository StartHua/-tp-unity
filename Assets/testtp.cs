using System.Collections;
using System.Collections.Generic;
using TpMSpcace;
using UnityEngine;
using System;
using UnityEngine.UI;

public class testtp : MonoBehaviour {


    public Text _tex;
    void Awake()
	{
        TokenPocketManager.Instance.callback = Call;

        //注册
        TokenPocketManager.Instance.Init();
        TokenPocketManager.Instance.onRegisterAppID(TokenPocketManager.Appid);
        //检查是否已经安装tp钱包
	    if (TokenPocketManager.Instance.IsInstallTpApp() == false)
	    {
	        //弹框提示
	    }

	}

    public void Call(TpState state)
    {
        Debug.Log("call unity :" + state);
        _tex.text ="call back : " +  state.ToString();
    }



	public void login()
    {
     
        TokenPocketManager.Instance.onLoginApp();

    }

    public void Transfer()
    {

        TokenPocketManager.Instance.onTransfer("eosio.token","ahuaeostrade", "adminxhuaeos", 0.0001f, "test");
    }
}
