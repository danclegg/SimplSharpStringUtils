using System;
using System.Text;
using System.Net;
using Crestron.SimplSharp;                          				// For Basic SIMPL# Classes
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


namespace SimplSharpStringUtils
{
    public class StringUtils
    {
        public Crestron.SimplSharp.IPAddress ipaddress { get; set; }

        public StringUtils()
        {
#if DEBUG
            CrestronConsole.PrintLine("New stringUtil instantiated.");
#endif
        }

        public string ip(string iptmp)
        {
            try
            {
                //string ipTmp = ipaddress.ToString();
                string[] ipArr = iptmp.Split('.');
                string first = Int32.Parse(ipArr[0]).ToString("D3");
                string second = Int32.Parse(ipArr[1]).ToString("D3");
                string third = Int32.Parse(ipArr[2]).ToString("D3");
                string fourth = Int32.Parse(ipArr[3]).ToString("D3");

                return String.Format("{}.{}.{}.{}", first, second, third, fourth);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public string HexToAscii(String hexString)
        {
            try
            {
                string ascii = string.Empty;

                for (int i = 0; i < hexString.Length; i += 2)
                {
                    String hs = string.Empty;

                    hs = hexString.Substring(i, 2);
                    uint decval = System.Convert.ToUInt32(hs, 16);
                    char character = System.Convert.ToChar(decval);
                    ascii += character;

                }

                return ascii;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return string.Empty;
        }

        public string JSONAttributes(String body, String desiredAttribute)
        {
            try
            {
                JObject jsonObj = JsonConvert.DeserializeObject(body) as JObject;
                JEnumerable<JToken> children = jsonObj.Children();
                List<string> strList = new List<string>();

                foreach (JObject content in children)
                {
                    foreach (JProperty prop in content.Properties())
                    {
                        if (prop.Name.ToLower().Equals(desiredAttribute.ToLower()))
                        {
                            strList.Add(prop.Value.ToString());
                        }
                    }
                }
#if DEBUG
                CrestronConsole.PrintLine("Returning a list of " + strList.Count + " items:"); 
#endif
                return string.Join(";", strList.ToArray()); ;
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine(ex.Message);
            }
            return string.Empty;
        }
        public string GetMemberOfStringArray(String arrayToSearch, String delimiter, int member)
        {
            string[] strArr = arrayToSearch.Split(delimiter[0]);
            try
            {
                return strArr[member];
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine(ex.Message);
                return string.Empty;
            }
        }

    }
}