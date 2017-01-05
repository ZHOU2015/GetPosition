using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

public partial class showArea : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            //获取本机外网IP地址
            //HttpRequest request = HttpContext.Current.Request;
            //string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //if (string.IsNullOrEmpty(result))
            //{
            //    result = request.ServerVariables["REMOTE_ADDR"];
            //}
            //if (string.IsNullOrEmpty(result))
            //{
            //    result = request.UserHostAddress;
            //}
            //if (string.IsNullOrEmpty(result))
            //{
            //    result = "0.0.0.0";
            //}
            //string ip = result;
            //TextBox1.Text = ip;
            ////http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=58.100.2.164

            try
            {
                string[] temp = GetAddressByIp();
                TextBox2.Text = temp[0].ToString().Trim();//省
                TextBox3.Text = temp[1].ToString().Trim();//市
            }
            catch (Exception ex) { 
                TextBox2.Text="未知";
                TextBox3.Text = "未知";
            }
        
        
    }

	

    ///
    /// 根据IP获取省市
    ///
    public string[] GetAddressByIp()
    {
        //string ip = TextBox1.Text.ToString().Trim(); //"115.193.217.249";
        //string PostUrl = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=" + ip;
        string PostUrl = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=js";
        string res = GetDataByPost(PostUrl);//该条请求返回的数据为：res=1\t115.193.210.0\t115.194.201.255\t中国\t浙江\t杭州\t电信
        string[] arr = getAreaInfoList(res);
        //return arr[1].ToString().Trim();
        return arr;
    }
    ///
    /// Post请求数据
    ///
    ///
    ///
    public string GetDataByPost(string url)
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        string s = "anything";
        byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(s);
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        req.ContentLength = requestBytes.Length;
        Stream requestStream = req.GetRequestStream();
        requestStream.Write(requestBytes, 0, requestBytes.Length);
        requestStream.Close();
        HttpWebResponse res = (HttpWebResponse)req.GetResponse();
        StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
        string backstr = sr.ReadToEnd();
        sr.Close();
        res.Close();
        return backstr;
    }
    ///
    /// 处理所要的数据
    ///
    ///
    ///
    public static string[] getAreaInfoList(string ipData)
    {
        //1\t115.193.210.0\t115.194.201.255\t中国\t浙江\t杭州\t电信
        string[] areaArr = new string[10];
        string[] newAreaArr = new string[2];
        try
        {
            var match = Regex.Match(ipData, @"\{(.*)\}", RegexOptions.Singleline);
            Console.WriteLine(match.Groups[1].Value);
            //取所要的数据，这里只取省市
            areaArr = match.ToString().Split(',');
            string provice = areaArr[4];
            string city = areaArr[5];
            //截取标准unicode码
            provice = provice.Substring(12, 12);
            city = city.Substring(8, 12);
            newAreaArr[0] = Unicode2String(provice);//可以给后台用的省份
            newAreaArr[1] = Unicode2String(city);//可以给后台用的城市
                //provice;//省
            //newAreaArr[1] = city;//市
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
        return newAreaArr;
    }

    /// <summary>
    /// Unicode转字符串
    /// </summary>
    /// <param name="source">经过Unicode编码的字符串</param>
    /// <returns>正常字符串</returns>
    public static string Unicode2String(string source)
    {
        return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
                     source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
    }

    #region 获取浏览器版本号

    /// <summary>  
    /// 获取浏览器版本号  
    /// </summary>  
    /// <returns></returns>  
    public static string GetBrowser()
    {
        HttpBrowserCapabilities bc = HttpContext.Current.Request.Browser;
        return bc.Browser + bc.Version;
    }

    #endregion

    #region 获取操作系统版本号

    /// <summary>  
    /// 获取操作系统版本号  
    /// </summary>  
    /// <returns></returns>  
    public static string GetOSVersion()
    {
        //UserAgent   
        var userAgent = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];

        var osVersion = "未知";

        if (userAgent.Contains("NT 6.1"))
        {
            osVersion = "Windows 7";
        }
        else if (userAgent.Contains("NT 6.0"))
        {
            osVersion = "Windows Vista/Server 2008";
        }
        else if (userAgent.Contains("NT 5.2"))
        {
            osVersion = "Windows Server 2003";
        }
        else if (userAgent.Contains("NT 5.1"))
        {
            osVersion = "Windows XP";
        }
        else if (userAgent.Contains("NT 5"))
        {
            osVersion = "Windows 2000";
        }
        else if (userAgent.Contains("NT 4"))
        {
            osVersion = "Windows NT4";
        }
        else if (userAgent.Contains("Me"))
        {
            osVersion = "Windows Me";
        }
        else if (userAgent.Contains("98"))
        {
            osVersion = "Windows 98";
        }
        else if (userAgent.Contains("95"))
        {
            osVersion = "Windows 95";
        }
        else if (userAgent.Contains("Mac"))
        {
            osVersion = "Mac";
        }
        else if (userAgent.Contains("Unix"))
        {
            osVersion = "UNIX";
        }
        else if (userAgent.Contains("Linux"))
        {
            osVersion = "Linux";
        }
        else if (userAgent.Contains("SunOS"))
        {
            osVersion = "SunOS";
        }
        return osVersion;
    }
    #endregion

    #region 获取客户端IP地址

    /// <summary>  
    /// 获取客户端IP地址  
    /// </summary>  
    /// <returns></returns>  
    public static string GetIP()
    {
        string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(result))
        {
            result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        if (string.IsNullOrEmpty(result))
        {
            result = HttpContext.Current.Request.UserHostAddress;
        }
        if (string.IsNullOrEmpty(result))
        {
            return "0.0.0.0";
        }
        return result;
    }

    #endregion

    #region 取客户端真实IP

    ///  <summary>    
    ///  取得客户端真实IP。如果有代理则取第一个非内网地址    
    ///  </summary>    
    public static string GetIPAddress
    {
        get
        {
            var result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(result))
            {
                //可能有代理    
                if (result.IndexOf(".") == -1)        //没有“.”肯定是非IPv4格式    
                    result = null;
                else
                {
                    if (result.IndexOf(",") != -1)
                    {
                        //有“,”，估计多个代理。取第一个不是内网的IP。    
                        result = result.Replace("  ", "").Replace("'", "");
                        string[] temparyip = result.Split(",;".ToCharArray());
                        for (int i = 0; i < temparyip.Length; i++)
                        {
                            if (IsIPAddress(temparyip[i])
                                    && temparyip[i].Substring(0, 3) != "10."
                                    && temparyip[i].Substring(0, 7) != "192.168"
                                    && temparyip[i].Substring(0, 7) != "172.16.")
                            {
                                return temparyip[i];        //找到不是内网的地址    
                            }
                        }
                    }
                    else if (IsIPAddress(result))  //代理即是IP格式    
                        return result;
                    else
                        result = null;        //代理中的内容  非IP，取IP    
                }

            }

            string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];

            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];

            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;

            return result;
        }
    }

    #endregion

    #region  判断是否是IP格式

    ///  <summary>  
    ///  判断是否是IP地址格式  0.0.0.0  
    ///  </summary>  
    ///  <param  name="str1">待判断的IP地址</param>  
    ///  <returns>true  or  false</returns>  
    public static bool IsIPAddress(string str1)
    {
        if (string.IsNullOrEmpty(str1) || str1.Length < 7 || str1.Length > 15) return false;

        const string regFormat = @"^d{1,3}[.]d{1,3}[.]d{1,3}[.]d{1,3}$";
        
        var regex = new Regex(regFormat, RegexOptions.IgnoreCase);
        return regex.IsMatch(str1);
    }

    #endregion

    #region 获取公网IP
    /// <summary>  
    /// 获取公网IP  
    /// </summary>  
    /// <returns></returns>  
    public static string GetNetIP()
    {
        string tempIP = "";
        try
        {
            System.Net.WebRequest wr = System.Net.WebRequest.Create("http://city.ip138.com/ip2city.asp");
            System.IO.Stream s = wr.GetResponse().GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(s, System.Text.Encoding.GetEncoding("gb2312"));
            string all = sr.ReadToEnd(); //读取网站的数据  

            int start = all.IndexOf("[") + 1;
            int end = all.IndexOf("]", start);
            tempIP = all.Substring(start, end - start);
            sr.Close();
            s.Close();
        }
        catch
        {
            if (System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.Length > 1)
                tempIP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[1].ToString();
            if (string.IsNullOrEmpty(tempIP))
                return GetIP();
        }
        return tempIP;
    }
    #endregion  
}
