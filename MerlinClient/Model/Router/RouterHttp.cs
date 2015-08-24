using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace MerlinClient.Model
{
    class RouterHttp
    {

        public string LastResultHtml;
        public CookieContainer CookieContainer;
        public string CookiesString;
        public string NextRequestUrl;

        public string Post(string Url, string postdata = "", string referer = "")
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(Url);
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en-US;q=0.5,en;q=0.3");
                request.UserAgent = "asusrouter";
                request.Method = "POST";
                if (String.IsNullOrEmpty(referer))
                {
                    request.Referer = Url;
                }
                else
                {
                    request.Referer = referer;
                }
                request.KeepAlive = true;

                request.Headers.Add("Cookie", CookiesString);

                //if (CookieContainer == null)
                {
                    CookieContainer = new CookieContainer();
                }
                request.CookieContainer = CookieContainer;
                

                byte[] postdataBytes = Encoding.UTF8.GetBytes(postdata);

                request.ContentLength = postdataBytes.Length;

                Stream postStream = request.GetRequestStream();
                postStream.Write(postdataBytes, 0, postdataBytes.Length);
                postStream.Close();


                response = (HttpWebResponse)request.GetResponse();

                CookieContainer = request.CookieContainer;
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                CookiesString = request.CookieContainer.GetCookieHeader(request.RequestUri);

                StreamReader dataStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                LastResultHtml = dataStream.ReadToEnd();
                dataStream.Close();

                request.Abort();
                response.Close();

                return LastResultHtml;

            }
            catch (WebException ex)
            {
                request = null;
                response = null;
                GC.Collect();
                return "";
            }

        }

        public string Get(string Url, string referer = "")
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(Url);
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en-US;q=0.5,en;q=0.3");
                request.UserAgent = "asusrouter";
                request.Method = "GET";
                if (String.IsNullOrEmpty(referer))
                {
                    request.Referer = Url;
                }
                else 
                { 
                    request.Referer = referer;
                }
                request.KeepAlive = true;

                request.Headers.Add("Cookie", CookiesString);

                /*if (CookieContainer == null)
                {
                    CookieContainer = new CookieContainer();
                }
                request.CookieContainer = CookieContainer;
                */
                response = (HttpWebResponse)request.GetResponse();

                /*CookieContainer = request.CookieContainer;
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                CookiesString = request.CookieContainer.GetCookieHeader(request.RequestUri);
                */
                StreamReader dataStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                LastResultHtml = dataStream.ReadToEnd();
                dataStream.Close();

                request.Abort();
                response.Close();

                return LastResultHtml;

            }
            catch (WebException ex)
            {
                request = null;
                response = null;
                GC.Collect();
                return "";
            }

        }
    }
}
