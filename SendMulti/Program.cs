using SendMulti.Repository;
using SendMulti.Data;
using System.Collections.Generic;
using System.Net.Mail;
using System.IO;
using System;
using System.Web.Services.Protocols;
using System.Net;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using SendMulti.Services;
using SendMulti.Model;
using System.Linq;

namespace SendMulti
{
    class Program
    {
        

        static void Main(string[] args)
        {


            ReportServerRepository repo = new ReportServerRepository();
            ExtensionSettings _extSettings = new ExtensionSettings();
            Parameters _parameters = new Parameters();
            PerceptionReport _perceptionReport = new PerceptionReport();
            List<Reports> results = new List<Reports>();
            List<GetSubscriptionInfo_Result> subInfo = new List<GetSubscriptionInfo_Result>();
            string Subject = "";
            

            subInfo = repo.GetAllReports();

            foreach (var sub in subInfo)
            {
                EmailSettings _emailSettings = new EmailSettings();

                _emailSettings = _extSettings.EmailSettings(sub.extensionSettings);

                var Stores = _extSettings.Stores(sub.extensionSettings);

                Subject = Stores[0] + " Stores ";
                for (int i = 1; i < Stores.Count; i++)
                {
                    var Sub = repo.GetSubscription(Stores[0], Int32.Parse(Stores[i]));
                    if (Sub != null)
                    {
                        var Parameters = _parameters.GetArrayParameters(Sub.Parameters);
                        var result = _perceptionReport.CreatePdfReport(Parameters);
                        
                        if (result != null)
                        {
                            Reports report = new Reports();
                            report.Name = "Perception_Report_" + Stores[0] + "_" + Stores[i] + "_Date_" + DateTime.Now.ToString("MM-dd-yyyy");
                            Subject = Subject  + Stores[i] + ", ";
                            report.Result = result;
                            results.Add(report);
                        }
                    }
                    else
                    {
                        
                        //MailMessage Ermail = new MailMessage("perception_reports@telecomdesigns.com", "perceptiontroublereport@telecomdesigns.com");
                        //SmtpClient Erclient = new SmtpClient();

                        //Erclient.Port = 25;
                        //Erclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        //Erclient.UseDefaultCredentials = false;
                        //Erclient.Host = "10.119.60.8";
                        //Ermail.Subject = "Perception Report for " + Stores[0] + " " + Stores[i] + " does not exist";
                        //Ermail.Body = "There was an error in the Multi report subscription for " + _emailSettings.EmailSubject;
                        //Erclient.Send(Ermail);
                    }

                    
                }

                Subject = Subject + " Perception Usage Reports for Last Week";

                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient();

                if (_emailSettings.EmailTo != null)
                {
                   var TOEmails = _emailSettings.EmailTo.Split(';').Select(sValue => sValue.Trim()).ToArray();
                   foreach (var em in TOEmails)
                   {
                       mail.To.Add(em);
                   }
                }
                if(_emailSettings.EmailCC != null)
                {
                   var CCEmails = _emailSettings.EmailCC.Split(';').Select(sValue => sValue.Trim()).ToArray();
                   foreach (var em in CCEmails)
                   {
                       mail.CC.Add(em);
                   }
                }

                if (_emailSettings.EmailBCC != null)
                {
                    var BCCEmails = _emailSettings.EmailBCC.Split(';').Select(sValue => sValue.Trim()).ToArray();
                    foreach (var em in BCCEmails)
                    {
                        mail.Bcc.Add(em);
                    }
                }
                              

                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "10.119.60.8";
                mail.From = new MailAddress("perception_reports@telecomdesigns.com");
                
                mail.Subject = Subject;
                foreach (var res in results)
                {
                    mail.Attachments.Add(new Attachment(new MemoryStream(res.Result), String.Format("{0}", res.Name + ".pdf")));
                }
                mail.Body = _emailSettings.EmailBody;
                //client.Send(mail);

                try
                {
                   // client.Send(mail);
                }
                catch (SmtpFailedRecipientsException ex)
                {
                    for (int i = 0; i < ex.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                        if (status == SmtpStatusCode.MailboxBusy ||
                            status == SmtpStatusCode.MailboxUnavailable)
                        {
                            Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                            System.Threading.Thread.Sleep(5000);
                            client.Send(mail);
                        }
                        else
                        {
                            Console.WriteLine("Failed to deliver message to {0}",
                                ex.InnerExceptions[i].FailedRecipient);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in RetryIfBusy(): {0}",
                            ex.ToString());
                }

            }

            

            

        }
    }
}
