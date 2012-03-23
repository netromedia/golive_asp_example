using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace APICallerTemplate
{
    public partial class EditChannel : System.Web.UI.Page
    {
        string sToken = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

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
                pnlMyChannelInfo.Visible = false;
                pError.InnerText = "ChannelId required.";
                return;
            }
            try
            {
                Guid gChannelId = XmlConvert.ToGuid(sChannelId);
            }
            catch (Exception ex)
            {
                pnlMyChannelInfo.Visible = false;
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
                pnlMyChannelInfo.Visible = false;
                pError.InnerText = dctCallResults["Error"].ToString();
                return;
            }

            // Display channel information in datalist
            Dictionary<string, object> successData = (Dictionary<string, object>)dctCallResults["Data"];

            lblChannelName.Text = successData["Channel Name"].ToString();
            ddlChannelFormat.SelectedValue = successData["Channel Type"].ToString();
            if (ddlChannelFormat.SelectedValue.Equals("WMS_LIVE_PULL"))
                txtChannelSource.Text = successData["Channel Source"].ToString();
            txtMaxConnections.Text = successData["Max Connections"].ToString();
            txtMaxBitrate.Text = successData["Max Bitrate"].ToString();
        }

        protected void Page_Command(Object sender, CommandEventArgs e)
        {
            if (e.CommandName == "EditChannel"){
                Dictionary<string, string> sPostData = new Dictionary<string, string>();
                sPostData.Add("Token", Session["Token"].ToString());
                sPostData.Add("ChannelName", lblChannelName.Text);
                sPostData.Add("ChannelFormat", ddlChannelFormat.SelectedValue);
                if (!string.IsNullOrEmpty(txtChannelSource.Text))
                    sPostData.Add("ChannelSource", txtChannelSource.Text);
                sPostData.Add("MaxConnections", txtMaxConnections.Text);
                sPostData.Add("MaxBitRate", txtMaxBitrate.Text);

                Dictionary<string, object> dctCallResults = API.CallApi("CreateChannel", sPostData);

                if (dctCallResults["Status"].Equals("Success"))
                {
                    // Successfully created the channel!  Go back to MyChannels to see the new details.
                    Response.Redirect("~/MyChannels.aspx");
                    return;
                }

                pError.InnerText = dctCallResults["Error"].ToString();
            }
        }
    }
}