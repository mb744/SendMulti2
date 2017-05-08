using SendMulti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMulti.Repository
{
    class ReportServerRepository
    {

        public List<GetSubscriptionInfo_Result> GetAllReports()
        {
            List<GetSubscriptionInfo_Result> subInfo = new List<GetSubscriptionInfo_Result>();
            var db = new ReportServerEntities();

            subInfo = db.GetSubscriptionInfo().ToList();

            if (subInfo != null)
            {
                return subInfo;
            };
            return null;
        }

        public string GetParameters(Guid? subId )
        {
            GetSubscription_Result subInfo = new GetSubscription_Result();
            

            subInfo = GetSubscriptionFromId(subId);

            if (subInfo != null)
            {
                var param = subInfo.Parameters;
                return param;
            };

            return null;
        }

        public string GetExtensionSettings(Guid subId)
        {
            GetSubscriptionInfo_Result subInfo = new GetSubscriptionInfo_Result();
            var db = new ReportServerEntities();

            var extensionSettings = db.GetSubscriptionInfo().Where(s => s.subscriptionID == subId).FirstOrDefault().ToString();

            if (subInfo != null)
            {
                return extensionSettings;
            };

            return null;
        }

        public string GetPath(Guid subId)
        {
            GetSubscriptionInfo_Result subInfo = new GetSubscriptionInfo_Result();
            var db = new ReportServerEntities();

            var path = db.GetSubscriptionInfo().Where(s => s.subscriptionID == subId).FirstOrDefault().ToString();

            if (subInfo != null)
            {
                return path;
            };

            return null;
        }

        public GetSubscriptionInfoFromStoreNameandSite_Result GetSubscription(string StoreName, int Site)
        {
            GetSubscriptionInfoFromStoreNameandSite_Result subInfo = new GetSubscriptionInfoFromStoreNameandSite_Result();
            var db = new ReportServerEntities();

            subInfo = db.GetSubscriptionInfoFromStoreNameandSite(StoreName, Site).FirstOrDefault();

            

            if (subInfo != null)
            {
                return subInfo;
            };

            return null;
        }

        public GetSubscription_Result GetSubscriptionFromId(Guid? Id)
        {
            GetSubscription_Result subInfo = new GetSubscription_Result();
            var db = new ReportServerEntities();

            subInfo = db.GetSubscription(Id).FirstOrDefault();



            if (subInfo != null)
            {
                return subInfo;
            };

            return null;
        }

    }
}
