using SendMulti.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;

namespace SendMulti.Services
{
    class Parameters
    {
        ReportServerRepository _repo = new ReportServerRepository();

        public ParameterValue[] GetArrayParameters(string parameters)
        {
            XmlDocument xDoc = new XmlDocument();
            List<ParameterValue> _params = new List<ParameterValue>();
            

            
            xDoc.LoadXml(parameters);

            XmlNodeList xnList = xDoc.SelectNodes("/ParameterValues/ParameterValue");
            foreach (XmlNode xn in xnList)
            {
                ParameterValue aParameter = new ParameterValue();
                string Name = xn["Name"].InnerText;
                if(Name != "CustomerRowID")
                {
                    aParameter.Name = Name;
                    string Value = xn["Value"].InnerText;
                    aParameter.Value = Value;
                    _params.Add(aParameter);
                }
                
            }

            int index = 0;
            ParameterValue[] parameterValues = new ParameterValue[_params.Count];
            foreach (ParameterValue parameterValue in _params)
            {
                parameterValues[index] = parameterValue;
                index++;
            }
            
            return parameterValues;

        }
    }
}
