using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using System.Net;

namespace SendMulti.Services
{
    class PerceptionReport
    {

        public byte[] CreatePdfReport(ParameterValue[] Parameters)
        {
            string deviceInfo = null;
            string extension = String.Empty;
            string mimeType = String.Empty;
            string encoding = String.Empty;
            Warning[] warnings = null;
            string historyId = null;
            string[] streamIDs = null;
            Byte[] results = null;
            try
            {

                ReportExecutionService rs = new ReportExecutionService();
            rs.Credentials = new NetworkCredential("tduser1", "Aiza2004", "tdwin08sql08sp3");
            rs.Url = "http://tdwin08sql08sp3/reportserver/ReportExecution2005.asmx";
            rs.LoadReport("/Reports/PerceptionReport", historyId);

            rs.SetExecutionParameters(Parameters, null);
                        

            
                results = rs.Render("PDF", deviceInfo,
                out extension, out encoding,
                out mimeType, out warnings, out streamIDs);

            
            }
            catch (Exception e)
            {
                Console.WriteLine("Catch clause caught : {0} \n", e.Message);
                return null;
            }
            return results;
        }

    }
}
