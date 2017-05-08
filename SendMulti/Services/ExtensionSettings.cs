using SendMulti.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SendMulti.Services
{
    class ExtensionSettings
    {
        public List<string> Stores(string strXml)
        {
            XmlDocument xDoc = new XmlDocument();
            List<string> items = new List<string>();
                        
            xDoc.LoadXml(strXml);

            XmlNodeList xnList = xDoc.SelectNodes("/ParameterValues/ParameterValue");
            foreach (XmlNode xn in xnList)
            {
                string Name = xn["Name"].InnerText;
                if (Name.Equals("Subject"))
                {
                    string Value = xn["Value"].InnerText;
                    string[] Values = Value.Split(' ').Select(sValue => sValue.Trim()).ToArray();
                    for (int i = 1; i < Values.Count(); i ++ )
                    {
                        int number;
                        bool result = Int32.TryParse(Values[i], out number);
                        if (i == 1) result = true;
                        if (result)
                        {
                            items.Add(Values[i]);
                            
                        }
                        
                    }
                }
                
            }

            if (items != null)
            {
                return items;
            }

            return null;
        }

        public EmailSettings EmailSettings(string strXml)
        {
            XmlDocument xDoc = new XmlDocument();
            EmailSettings _emailSettings = new EmailSettings();

            xDoc.LoadXml(strXml);

            XmlNodeList xnList = xDoc.SelectNodes("/ParameterValues/ParameterValue");
            foreach (XmlNode xn in xnList)
            {
                string Name = xn["Name"].InnerText;
                if (Name.Equals("TO"))
                {
                    string Value = xn["Value"].InnerText;
                    _emailSettings.EmailTo = Value;
                }
                if (Name.Equals("CC"))
                {
                    string Value = xn["Value"].InnerText;
                    _emailSettings.EmailCC = Value;
                }
                if (Name.Equals("BCC"))
                {
                    string Value = xn["Value"].InnerText;
                    _emailSettings.EmailBCC = Value;
                }
                if (Name.Equals("Subject"))
                {
                    string Value = xn["Value"].InnerText;
                    _emailSettings.EmailSubject = Value;
                }
                if (Name.Equals("Comment"))
                {
                    string Value = xn["Value"].InnerText;
                    _emailSettings.EmailBody = Value;
                }

            }

            if (_emailSettings != null)
            {
                return _emailSettings;
            }

            return null;
        }
    }
}
