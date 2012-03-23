using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;

namespace APICallerTemplate
{
    public partial class ViewChannelInfo : System.Web.UI.Page
    {
        string sToken = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            sToken = (Session["Token"] == null ? "" : Session["Token"].ToString());

            if (string.IsNullOrEmpty(sToken))
            {
                pnlNotLoggedIn.Visible = true;
                pnlDefault.Visible = false;
                return;
            }

            pnlNotLoggedIn.Visible = false;
            pnlDefault.Visible = true;

            // Check that Channel Id is not empty, and is in a valid GUID format.
            string sChannelId = Request.QueryString.Get("ChannelId");
            if (string.IsNullOrEmpty(sChannelId))
            {
                dlMyChannelInfo.Visible = false;
                pError.InnerText = "ChannelId required.";
                return;
            }
            try
            {
                Guid gChannelId = XmlConvert.ToGuid(sChannelId);
            }
            catch (Exception ex)
            {
                dlMyChannelInfo.Visible = false;
                pError.InnerText = ex.Message;
                return;
            }

            // Send request for channel information
            Dictionary<string, string> sPostData = new Dictionary<string, string>();
            sPostData.Add("Token", sToken);
            sPostData.Add("ChannelId", sChannelId);

            Dictionary<string, object> dctCallResults = API.CallApi("GetPublishInfo", sPostData);

            if (!dctCallResults["Status"].Equals("Success"))
            {
                dlMyChannelInfo.Visible = false;
                pError.InnerText = dctCallResults["Error"].ToString();
                return;
            }

            // Display channel information in datalist
            Dictionary<string, object> successData = (Dictionary<string, object>)dctCallResults["Data"];

            DataTable allChannelInfo = new DataTable();
            allChannelInfo.Columns.Add("ChannelPropertyName", typeof(System.String));
            allChannelInfo.Columns.Add("ChannelPropertyValue", typeof(System.String));

            foreach (KeyValuePair<string, object> kvp in successData)
            {
                DataRow currentRow = allChannelInfo.NewRow();
                currentRow["ChannelPropertyName"] = kvp.Key;
                currentRow["ChannelPropertyValue"] = kvp.Value;
                allChannelInfo.Rows.Add(currentRow);
            }

            dlMyChannelInfo.DataSource = allChannelInfo;
            dlMyChannelInfo.DataBind();
            dlMyChannelInfo.Visible = true;

            // Send request for channel stats information
            sPostData = new Dictionary<string, string>();
            sPostData.Add("Token", sToken);
            sPostData.Add("ChannelId", sChannelId);

            dctCallResults = API.CallApi("GetChannelStats", sPostData);

            if (!dctCallResults["Status"].Equals("Success"))
            {
                dlMyChannelStats.Visible = false;
                pError.InnerText = dctCallResults["Error"].ToString();
                return;
            }

            // Display channel stats in datalist
            successData = (Dictionary<string, object>)dctCallResults["Data"];

            DataTable allChannelStats = new DataTable();
            allChannelStats.Columns.Add("ChannelStatPropertyName", typeof(System.String));
            allChannelStats.Columns.Add("ChannelStatPropertyValue", typeof(System.String));

            foreach (KeyValuePair<string, object> kvp in successData)
            {
                DataRow currentRow = allChannelStats.NewRow();
                currentRow["ChannelStatPropertyName"] = kvp.Key;
                currentRow["ChannelStatPropertyValue"] = kvp.Value;
                allChannelStats.Rows.Add(currentRow);
            }

            dlMyChannelStats.DataSource = allChannelStats;
            dlMyChannelStats.DataBind();
            dlMyChannelStats.Visible = true;
        }
    }
}