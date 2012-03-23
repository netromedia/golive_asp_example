using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace APICallerTemplate
{
    public partial class CreateChannel : System.Web.UI.Page
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
        }

        protected void Page_Command(Object sender, CommandEventArgs e)
        {
            if (e.CommandName == "CreateChannel"){
                Dictionary<string, string> sPostData = new Dictionary<string, string>();
                sPostData.Add("Token", Session["Token"].ToString());
                sPostData.Add("ChannelName", txtChannelName.Text);
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