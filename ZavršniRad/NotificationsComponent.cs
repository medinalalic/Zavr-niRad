using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using ZavršniRad.Models;

namespace ZavršniRad
{
    public class NotificationsComponent
    {
        public void RegisterNotification(DateTime vrijeme)
        {
            string conn = ConfigurationManager.ConnectionStrings["sqlConn"].ConnectionString;
            string sqlCommand = @"SELECT [Id] from [dbo].[Pacijents] where [AddedOn] > @AddedOn";
            using (SqlConnection cn=new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand,cn);
                cmd.Parameters.AddWithValue("@AddedOn", vrijeme);
                if(cn.State != System.Data.ConnectionState.Open)
                {
                    cn.Open();
                }
                cmd.Notification = null;
                SqlDependency d = new SqlDependency(cmd);
                d.OnChange += D_OnChange;
                using(SqlDataReader reader=cmd.ExecuteReader())
                {

                }

            }

        }

        private void D_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if(e.Type == SqlNotificationType.Change)
            {
                SqlDependency sqldep = sender as SqlDependency;
                sqldep.OnChange -= D_OnChange;
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<notification>();
                notificationHub.Clients.All.notify("addded");
                RegisterNotification(DateTime.Now);

            }
        }

        public List<Pacijent> GetPacijents(DateTime afterDate)
        {

            using (Stomatoloska_MLEntities1 dc = new Stomatoloska_MLEntities1())
            {
                return dc.Pacijents.Where(a => a.AddedOn > afterDate).OrderByDescending(a => a.AddedOn).ToList();
            }
        }
    }
}