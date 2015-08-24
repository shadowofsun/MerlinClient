using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;



namespace MerlinClient.Model
{
    public class Router
    {
        public string IP;
        public string MAC;
        public string password;
        public int port;



        private RouterHttp routerHttp = new RouterHttp();

        public Router(string IP = "", string MAC = "", int port = 0, string password = "")
        {
            this.IP = IP;
            this.MAC = MAC;
            this.port = port;
            this.password = password;
        }

        public bool Login()
        {
            string Url = String.Format("http://{0}:{1}/login.cgi", IP, port);
            string AuthorizationString = String.Format("{0}:{1}", "admin", password);
            byte[] AuthorizationByte = Encoding.UTF8.GetBytes(AuthorizationString);
            string Authorization = Convert.ToBase64String(AuthorizationByte);
            string PostData = String.Format("group_id=&action_mode=&action_script=&action_wait=5&current_page=Main_Login.asp&next_page=Main_Login.asp&flag=&login_authorization={0}", Authorization);

            routerHttp.Post(Url, PostData);


            if (routerHttp.CookiesString.IndexOf("asus_token") < 0 || String.IsNullOrEmpty(routerHttp.CookiesString))
            {
                return false;
            }

            return true;
        }

        public int GetMode()
        {

            string ss_conf_url = String.Format("http://{0}:{1}/ss_conf", IP, port);
            string ssconfDoc = routerHttp.Get(ss_conf_url);

            if (ssconfDoc.IndexOf("error") > -1)
            {
                return -1;
            }

            ssconfDoc = ssconfDoc.Replace("\"", "");
            ssconfDoc = ssconfDoc.Replace(",", "");
            Regex status = new Regex(@"(?is)var ss={]*.*?}");
            MatchCollection mc_status = status.Matches(ssconfDoc);
            ssconfDoc = mc_status[0].ToString();

            string[] ss = ssconfDoc.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] sss;
            foreach (string s in ss)
            {
                sss = s.Split(':');
                if (sss.Length > 1)
                {
                    if (sss[0].ToString() == "mode")
                    {
                        return Convert.ToInt32(sss[1]);
                    }
                }
            }
            return -1;
        }

        public bool ChangeMode(int mode)
        {
            Hashtable PostDataTable = new Hashtable();
            string ss_conf_url = String.Format("http://{0}:{1}/ss_conf", IP, port);
            string state_url = String.Format("http://{0}:{1}/state.js", IP, port);
            string ss_setting_url = String.Format("http://{0}:{1}/applyss.cgi", IP, port);

            string ssconfDoc = routerHttp.Get(ss_conf_url);

            ssconfDoc = ssconfDoc.Replace("\"", "");
            ssconfDoc = ssconfDoc.Replace(",", "");
            Regex status = new Regex(@"(?is)var ss={]*.*?}");
            MatchCollection mc_status = status.Matches(ssconfDoc);
            ssconfDoc = mc_status[0].ToString();

            string[] ss = ssconfDoc.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] sss;
            foreach (string s in ss)
            {
                sss = s.Split(':');
                if (sss.Length > 1)
                {
                    PostDataTable.Add(sss[0], sss[1]);
                }
            }

            if (mode == Convert.ToInt32(PostDataTable["mode"]))
            {
                return true;
            }

            //请求系统配置状态信息
            ssconfDoc = routerHttp.Get(state_url);
            status = new Regex(@"(?is)var firmver = ']*.*?'");
            mc_status = status.Matches(ssconfDoc);
            ssconfDoc = mc_status[0].ToString();
            ss = ssconfDoc.Split('\'');
            PostDataTable.Add("firmver", ss[1]);

            ssconfDoc = routerHttp.LastResultHtml;
            status = new Regex(@"(?is)name=""preferred_lang\"" value=]*.*?>");
            mc_status = status.Matches(ssconfDoc);
            ssconfDoc = mc_status[0].ToString();
            ssconfDoc = ssconfDoc.Replace("\"", "");
            ss = ssconfDoc.Split(' ');
            int startindex = ss[1].IndexOf("=") + 1;
            int lenth = ss[1].IndexOf(">") - startindex;
            PostDataTable.Add("preferred_lang", ss[1].Substring(startindex, lenth));
            
            //设置ss
            string[] DataArray = new string[] {
                PostDataTable["preferred_lang"].ToString(),
                PostDataTable["firmver"].ToString(),
                mode.ToString(),
                PostDataTable["server"].ToString(),
                PostDataTable["port"].ToString(),
                PostDataTable["password"].ToString(),
                PostDataTable["method"].ToString(),
                PostDataTable["chromecast"].ToString()
            };
            string postdata = String.Format("current_page=Main_Ss_Content.asp&next_page=Main_Ss_Content.asp&group_id=&modified=0&action_mode=+Refresh+&action_script=&action_wait=&first_time=&preferred_lang={0}&SystemCmd=ssconfig+basic&firmver={1}&ss_mode={2}&ss_server={3}&ss_port={4}&ss_password={5}&ss_method={6}&ss_chromecast={7}", DataArray);

            if (routerHttp.Post(ss_setting_url, postdata).IndexOf("您的网络登录名和密码仍然是默认设置") > -1)
            {
                return true;
            }
            return false;
        }

        public bool IsLogin()
        {
            if (routerHttp.CookiesString.IndexOf("asus_token") < 0 || String.IsNullOrEmpty(routerHttp.CookiesString))
            {
                return false;
            }
            return true;
        }

    }

}