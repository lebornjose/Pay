﻿using System;
using System.Collections.Generic;
using System.Text;
using Jabinfo;

namespace Bill99
{
    public class Bill99Send
    {
        public string Page_Load(JabinfoKeyValue requireData)
        {

            //人民币网关账户号
            ///请登录快钱系统获取用户编号，用户编号后加01即为人民币网关账户号。
            String merchantAcctId = requireData["merchantAcctId"];

            //人民币网关密钥
            ///区分大小写.请与快钱联系索取
            String key = requireData["key"];

            //字符集.固定选择值。可为空。
            ///只能选择1、2、3.
            ///1代表UTF-8; 2代表GBK; 3代表gb2312
            ///默认值为1
            String inputCharset = "1";

            //接受支付结果的页面地址.与[bgUrl]不能同时为空。必须是绝对地址。
            ///如果[bgUrl]为空，快钱将支付结果Post到[pageUrl]对应的地址。
            ///如果[bgUrl]不为空，并且[bgUrl]页面指定的<redirecturl>地址不为空，则转向到<redirecturl>对应的地址
            String pageUrl = requireData["pageUrl"];

            //服务器接受支付结果的后台地址.与[pageUrl]不能同时为空。必须是绝对地址。
            ///快钱通过服务器连接的方式将交易结果发送到[bgUrl]对应的页面地址，在商户处理完成后输出的<result>如果为1，页面会转向到<redirecturl>对应的地址。
            ///如果快钱未接收到<redirecturl>对应的地址，快钱将把支付结果post到[pageUrl]对应的页面。
            String bgUrl = requireData["bgUrl"];

            //网关版本.固定值
            ///快钱会根据版本号来调用对应的接口处理程序。
            ///本代码版本号固定为v2.0
            String version = "v2.0";

            //语言种类.固定选择值。
            ///只能选择1、2、3
            ///1代表中文；2代表英文
            ///默认值为1
            String language = "1";

            //签名类型.固定值
            ///1代表MD5签名
            ///当前版本固定为1
            String signType = "1";

            //支付人姓名
            ///可为中文或英文字符
            String payerName = requireData["payerName"];

            //支付人联系方式类型.固定选择值
            ///只能选择1
            ///1代表Email
            String payerContactType = "1";

            //支付人联系方式
            ///只能选择Email或手机号
            String payerContact = "";

            //商户订单号
            ///由字母、数字、或[-][_]组成
            //String orderId = DateTime.Now.ToString("yyyyMMddHHmmss");
            String orderId = requireData["ordersId"];

            //订单金额
            ///以分为单位，必须是整型数字
            ///比方2，代表0.02元
            String orderAmount = requireData["orderAmount"];

            //订单提交时间
            ///14位数字。年[4位]月[2位]日[2位]时[2位]分[2位]秒[2位]
            ///如；20080101010101
            String orderTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            //商品名称
            ///可为中文或英文字符
            String productName = requireData["productName"];

            //商品数量
            ///可为空，非空时必须为数字
            String productNum = requireData["productNum"];

            //商品代码
            ///可为字符或者数字
            String productId = "";

            //商品描述
            String productDesc = "";

            //扩展字段1
            ///在支付结束后原样返回给商户
            String ext1 = "";

            //扩展字段2
            ///在支付结束后原样返回给商户
            String ext2 = "";

            //支付方式.固定选择值
            ///只能选择00、10、11、12、13、14
            ///00：组合支付（网关支付页面显示快钱支持的各种支付方式，推荐使用）10：银行卡支付（网关支付页面只显示银行卡支付）.11：电话银行支付（网关支付页面只显示电话支付）.12：快钱账户支付（网关支付页面只显示快钱账户支付）.13：线下支付（网关支付页面只显示线下支付方式）.14：B2B支付（网关支付页面只显示B2B支付，但需要向快钱申请开通才能使用）
            String payType = requireData["payType"];

            //银行代码
            ///实现直接跳转到银行页面去支付,只在payType=10时才需设置参数
            ///具体代码参见 接口文档银行代码列表
            String bankId = requireData["bankId"];

            //同一订单禁止重复提交标志
            ///固定选择值： 1、0
            ///1代表同一订单号只允许提交1次；0表示同一订单号在没有支付成功的前提下可重复提交多次。默认为0建议实物购物车结算类商户采用0；虚拟产品类商户采用1
            String redoFlag = "0";

            //快钱的合作伙伴的账户号
            ///如未和快钱签订代理合作协议，不需要填写本参数
            String pid = "";


            //生成加密签名串
            ///请务必按照如下顺序和规则组成加密串！
            String signMsgVal = "";
            signMsgVal = appendParam(signMsgVal, "inputCharset", inputCharset);
            signMsgVal = appendParam(signMsgVal, "pageUrl", pageUrl);
            signMsgVal = appendParam(signMsgVal, "bgUrl", bgUrl);
            signMsgVal = appendParam(signMsgVal, "version", version);
            signMsgVal = appendParam(signMsgVal, "language", language);
            signMsgVal = appendParam(signMsgVal, "signType", signType);
            signMsgVal = appendParam(signMsgVal, "merchantAcctId", merchantAcctId);
            signMsgVal = appendParam(signMsgVal, "payerName", payerName);
            signMsgVal = appendParam(signMsgVal, "payerContactType", payerContactType);
            signMsgVal = appendParam(signMsgVal, "payerContact", payerContact);
            signMsgVal = appendParam(signMsgVal, "orderId", orderId);
            signMsgVal = appendParam(signMsgVal, "orderAmount", orderAmount);
            signMsgVal = appendParam(signMsgVal, "orderTime", orderTime);
            signMsgVal = appendParam(signMsgVal, "productName", productName);
            signMsgVal = appendParam(signMsgVal, "productNum", productNum);
            signMsgVal = appendParam(signMsgVal, "productId", productId);
            signMsgVal = appendParam(signMsgVal, "productDesc", productDesc);
            signMsgVal = appendParam(signMsgVal, "ext1", ext1);
            signMsgVal = appendParam(signMsgVal, "ext2", ext2);
            signMsgVal = appendParam(signMsgVal, "payType", payType);
            signMsgVal = appendParam(signMsgVal, "bankId", bankId);
            signMsgVal = appendParam(signMsgVal, "redoFlag", redoFlag);
            signMsgVal = appendParam(signMsgVal, "pid", pid);
            signMsgVal = appendParam(signMsgVal, "key", key);

            //如果在web.config文件中设置了编码方式，例如<globalization requestEncoding="utf-8" responseEncoding="utf-8"/>（如未设则默认为utf-8），
            //那么，inputCharset的取值应与已设置的编码方式相一致；
            //同时，GetMD5()方法中所传递的编码方式也必须与此保持一致。
            String signMsg = GetMD5(signMsgVal, "utf-8").ToUpper();


            //?
            //Lab_orderId.Text = orderId;
            //Lab_orderAmount.Text = orderAmount;
            //Lab_payerName.Text = payerName;
            //Lab_productName.Text = productName;
            String result = "<!doctype html public \"-//w3c//dtd html 4.0 transitional//en\" >" +
            "<html><head><title>使用快钱支付</title><meta http-equiv=\"content-type\" content=\"text/html; charset=gb2312\" ></head><BODY>";

            result += "<form name=\"kqPay\" method=\"post\" action=\"" + "https://www.99bill.com/gateway/recvMerchantInfoAction.htm" + "\"/>" +
            "<input type=\"hidden\" name=\"inputCharset\"  value=\"" + inputCharset + "\"/>" +
            "<input type=\"hidden\" name=\"bgUrl\"  value=\"" + bgUrl + "\"/>" +
            "<input type=\"hidden\" name=\"pageUrl\"  value=\"" + pageUrl + "\"/>" +
            "<input type=\"hidden\" name=\"version\"  value=\"" + version + "\"/>" +
            "<input type=\"hidden\" name=\"language\"  value=\"" + language + "\"/>" +
            "<input type=\"hidden\" name=\"signType\"  value=\"" + signType + "\"/>" +
            "<input type=\"hidden\" name=\"signMsg\"  value=\"" + signMsg + "\"/>" +
            "<input type=\"hidden\" name=\"merchantAcctId\"  value=\"" + merchantAcctId + "\"/>" +
            "<input type=\"hidden\" name=\"payerName\"  value=\"" + payerName + "\"/>" +
            "<input type=\"hidden\" name=\"payerContactType\"  value=\"" + payerContactType + "\"/>" +
            "<input type=\"hidden\" name=\"payerContact\"  value=\"" + payerContact + "\"/>" +
            "<input type=\"hidden\" name=\"orderId\"  value=\"" + orderId + "\"/>" +
            "<input type=\"hidden\" name=\"orderAmount\"  value=\"" + orderAmount + "\"/>" +
            "<input type=\"hidden\" name=\"orderTime\"  value=\"" + orderTime + "\"/>" +
            "<input type=\"hidden\" name=\"productName\"  value=\"" + productName + "\"/>" +
            "<input type=\"hidden\" name=\"productNum\"  value=\"" + productNum + "\"/>" +
            "<input type=\"hidden\" name=\"productId\"  value=\"" + productId + "\"/>" +
            "<input type=\"hidden\" name=\"productDesc\"  value=\"" + productDesc + "\"/>" +
            "<input type=\"hidden\" name=\"ext1\"  value=\"" + ext1 + "\"/>" +
            "<input type=\"hidden\" name=\"ext2\"  value=\"" + ext2 + "\"/>" +
            "<input type=\"hidden\" name=\"payType\"  value=\"" + payType + "\"/>" +
            "<input type=\"hidden\" name=\"bankId\"  value=\"" + bankId + "\"/>" +
            "<input type=\"hidden\" name=\"redoFlag\"  value=\"" + redoFlag + "\"/>" +
            "<input type=\"hidden\" name=\"pid\"  value=\"" + pid + "\"/>" +
            "</form>";
            result += "<script language=\"javascript\">document.forms[\"kqPay\"].submit();</script>";
            result += "</BODY></HTML>";
            return result;

        }
        //功能函数。将变量值不为空的参数组成字符串
        public String appendParam(String returnStr, String paramId, String paramValue)
        {

            if (returnStr != "")
            {

                if (paramValue != "")
                {

                    returnStr += "&" + paramId + "=" + paramValue;
                }

            }
            else
            {

                if (paramValue != "")
                {
                    returnStr = paramId + "=" + paramValue;
                }
            }

            return returnStr;
        }
        //功能函数。将变量值不为空的参数组成字符串。结束
        //功能函数。将字符串进行编码格式转换，并进行MD5加密，然后返回。开始
        private static string GetMD5(string dataStr, string codeType)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(System.Text.Encoding.GetEncoding(codeType).GetBytes(dataStr));
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
        //功能函数。将字符串进行编码格式转换，并进行MD5加密，然后返回。结束
    }
}
