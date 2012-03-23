using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Collections;

namespace APICallerTemplate
{
    public class API
    {
        public static Dictionary<string, object> CallApi(string sMethod, Dictionary<string, string> sPostData)
        {
            try
            {
                string sDataToSend = dictToJson(sPostData);
                Uri uriApiMethod = new Uri("https://login.netromedia.com/rest.svc/" + sMethod);

                // Build request
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uriApiMethod);
                request.Method = WebRequestMethods.Http.Post;
                request.ContentLength = sDataToSend.Length;
                request.ContentType = "application/json; charset=utf-8";

                // Send request
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(sDataToSend);
                writer.Close();

                // Receive response
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string tmp = reader.ReadToEnd();
                response.Close();

                // Convert from JSON to Dictionary<string, object>
                return jsonToDict(tmp);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        static string dictToJson(Dictionary<string, string> dctDictionary)
        {
            string jsonReturn = "{";
            foreach (KeyValuePair<string, string> dictPair in dctDictionary)
            {
                jsonReturn += "\"" + dictPair.Key + "\":\"" + dictPair.Value + "\",";
            }
            jsonReturn.TrimEnd(',');
            jsonReturn += "}";

            return jsonReturn;
        }

        static Dictionary<string, object> jsonToDict(string sJsonString)
        {
            Dictionary<string, object> dctReturnDict = new Dictionary<string, object>();

            while (!string.IsNullOrEmpty(sJsonString))
            {
                // Remove outermost curly braces (if both exist), and leading comma if it exists.
                if (sJsonString.StartsWith("{"))
                    sJsonString = sJsonString.Substring(1,sJsonString.Length - 2);
                sJsonString = sJsonString.TrimStart(',');

                if (Regex.IsMatch(sJsonString, @"^""[^\""]+"":""[^\""]*""")) // Match first "Key":"Value"
                {
                    string key = Regex.Match(sJsonString, @"^""[^\""]+"":").Value.TrimEnd(':').Trim('"');
                    sJsonString = Regex.Replace(sJsonString, @"^""[^\""]+"":", "");

                    string value = Regex.Match(sJsonString, @"^""[^\""]*""").Value.Trim('"');
                    sJsonString = Regex.Replace(sJsonString, @"^""[^\""]*""", "");

                    dctReturnDict.Add(key, value);
                    continue;
                }

                if (Regex.IsMatch(sJsonString, @"^""[^\""]+"":\{.*\}")) // Match first "Key":{"InnerKey":"InnerValue", ... }
                {
                    string key = Regex.Match(sJsonString, @"^""[^\""]+"":").Value.TrimEnd(':').Trim('"');
                    sJsonString = Regex.Replace(sJsonString, @"^""[^\""]+"":", "");

                    string stringValue = Regex.Match(sJsonString, @"^\{.*\}").Value;
                    sJsonString = Regex.Replace(sJsonString, @"^\{.*\}", "");

                    Dictionary<string, object> value = jsonToDict(stringValue); // Recurse
                    dctReturnDict.Add(key, value);
                    continue;
                }

                if (Regex.IsMatch(sJsonString, @"^""[^\""]+"":\[.*\]")) // Match first "Key":[{"InnerKey":"InnerValue", ... }, {...}, {...}]
                {
                    string key = Regex.Match(sJsonString, @"^""[^\""]+"":").Value.TrimEnd(':').Trim('"');
                    sJsonString = Regex.Replace(sJsonString, @"^""[^\""]+"":", "");

                    string stringValue = Regex.Match(sJsonString, @"\[.*\]").Value;
                    sJsonString = Regex.Replace(sJsonString, @"\[.*\]", "");

                    ArrayList value = new ArrayList();
                    MatchCollection groups = Regex.Matches(stringValue.Substring(1, stringValue.Length - 2), @"\{[^\}]+\}");
                    foreach (Match group in groups)
                        value.Add(jsonToDict(group.Value));
                    
                    dctReturnDict.Add(key, value);

                    continue;
                }

                // At this point, nothing should be left in the JSON string unless it's badly formed.  
                // So just empty it to make sure the loop finishes.
                sJsonString = "";
            }

            return dctReturnDict;
        }
    }
}