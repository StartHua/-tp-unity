//
//  TPWallet.m
//  SDKDemo
//
//  Created by xinghua on 2018/10/16.
//  Copyright © 2018年 TokenPocket. All rights reserved.
//  com.tokenpocket.ios

/*
 1.在Project - Build settings 搜索Other Linker Flags, 加一项 -ObjC
 2.在Xcode设置URL scheme 设置跳转appid(自己起) 这个很重要  [TPApi registerAppID:@"appid起好的id"];
 3.拷贝添加回调到unityappController.mm openURL函数下面大概215行
 4.在info.plist中LSApplicationQueriesSchemes下添加一项，值为tpoutside
 */

#import <Foundation/Foundation.h>
#import <TPSDK/TPSDK.h>

extern "C"
{
    //添加回调
    /*
    //区分是在com.tokenpocket.ios是返回
    if ([sourceApplication isEqualToString:@"com.tokenpocket.ios"]) {
        NSDictionary<UIApplicationOpenURLOptionsKey,id>* options =notifData;
        [TPApi handleURL:url options:options result:^(TPRespObj *respObj) {
            
            
            NSString *title = @"Success";
            if (respObj.result == TPRespResultFailure) {
                title = @"Failure";
            } else if (respObj.result == TPRespResultCanceled) {
                title = @"Cancel";
            }
            
            //发送unity 事件回调
            //第一个参数：调用的Unity函数绑定的模型
            //第二个参数：调用的Unity函数名称
            //第三个参数：调用的Unity函数参数
            UnitySendMessage("TokenPocketManager","TpCallBakc",[title UTF8String]);
            
            //弹框
            //            UIAlertController *alert = [UIAlertController alertControllerWithTitle:title message:respObj.message preferredStyle:UIAlertControllerStyleAlert];
            //            UIAlertAction *action = [UIAlertAction actionWithTitle:@"OK" style:UIAlertActionStyleDefault handler:nil];
            //            [alert addAction:action];
            //            [self.window.rootViewController presentViewController:alert animated:YES completion:nil];
            
            
        }];
    }
    */
    
    
    /*
     appid: 注册appid（要和URL scheme 一致）
     */
    void _onRegisterAppID(const char *appid)
    {
        [TPApi registerAppID: [NSString stringWithUTF8String:appid]];
    }
    
    
    /*
     fun:登陆
     dappname  :app名字
     blockchain:区块链 @"eos"
     dappIcon  :app的iconurl
     expired   :时间戳 15359897700
     */
    void _onLoginApp(const char *json){
        
        NSString* sj =[NSString stringWithUTF8String:json];
        NSData *data= [sj dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary* dic = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableLeaves error:nil];
        
        TPLoginObj *login = [TPLoginObj new];
        login.dappName =[dic objectForKey:@"dappName"];
        login.blockchain = [dic objectForKey:@"blockchain"];
        login.dappIcon = [dic objectForKey:@"dappIcon"];
        login.expired =[dic objectForKey:@"expired"];
        [TPApi sendObj:login];
        
        sj= nullptr;
        data = nullptr;
        dic = nullptr;
        
    }
    
    
    /*
     fun:转账
     dappName   :app名字
     dappIcon   :app的iconurl
     symbol     :符号 @"EOS" ==> "1.000 EOS"
     contract   :合约名字
     to         :转到用户
     memo       :提示
     precision  :小数点后几位 4
     amount     :数量
     expired    :时间戳
     */
    void _onTransfer(const char *json){
        
        NSString* sj =[NSString stringWithUTF8String:json];
        NSData *data= [sj dataUsingEncoding:NSUTF8StringEncoding];
        NSDictionary* dic = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableLeaves error:nil];
        
        TPTransferObj *transfer = [TPTransferObj new];
        transfer.dappName =[dic objectForKey:@"dappName"];
        transfer.dappIcon = [dic objectForKey:@"dappIcon"];
        transfer.symbol = [dic objectForKey:@"symbol"];
        transfer.contract = [dic objectForKey:@"contract"];
        transfer.to =[dic objectForKey:@"to"];
        transfer.memo = [dic objectForKey:@"memo"];
        transfer.precision = [dic objectForKey:@"precision"];
        
//        NSString* str =  [dic objectForKey:@"amount"];
//        float s = [str floatValue];
        
//        NSNumber* tempnumber = [NSNumber numberWithDouble:[[NSString stringWithFormat:@"%.4f",(float)s] doubleValue]];
        transfer.amount =[dic objectForKey:@"amount"];;
        transfer.expired = [dic objectForKey:@"expired"];
        
        [TPApi sendObj:transfer];
        
        sj= nullptr;
        data = nullptr;
        dic = nullptr;
    }
}
