using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

namespace APICallerTemplate
{
    public partial class About : System.Web.UI.Page
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

            refreshChannelsList();
        }

        protected void Page_Command(Object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                Dictionary<string, string> sPostData = new Dictionary<string, string>();
                sPostData.Add("Token", sToken);
                sPostData.Add("ChannelId", e.CommandArgument.ToString());

                Dictionary<string, object> dctCallResults = API.CallApi("DeleteChannel", sPostData);

                if (!dctCallResults["Status"].Equals("Success"))
                {
                    pError.InnerText = dctCallResults["Error"].ToString();
                    return;
                }

                refreshChannelsList();
            }

            if (e.CommandName == "View" || e.CommandName == "Edit")
            {
                Response.Redirect(e.CommandArgument.ToString());
            }
        }

        private void refreshChannelsList()
        {
            Dictionary<string, string> sPostData = new Dictionary<string, string>();
            sPostData.Add("Token", sToken);

            Dictionary<string, object> dctCallResults = API.CallApi("GetChannels", sPostData);

            if (!dctCallResults["Status"].Equals("Success"))
            {
                dgMyChannels.Visible = false;
                pError.InnerText = dctCallResults["Error"].ToString();
                return;
            }

            ArrayList successData = (ArrayList)dctCallResults["Data"];

            DataTable allChannels = new DataTable();
            allChannels.Columns.Add("ChannelId", typeof(System.String));
            allChannels.Columns.Add("ChannelName", typeof(System.String));
            allChannels.Columns.Add("ChannelType", typeof(System.String));

            foreach (object item in successData)
            {
                Dictionary<string, object> currentItem = (Dictionary<string, object>)item;
                DataRow currentRow = allChannels.NewRow();
                currentRow["ChannelId"] = currentItem["Channel Id"];
                currentRow["ChannelName"] = currentItem["Channel Name"];
                currentRow["ChannelType"] = currentItem["Channel Type"];
                allChannels.Rows.Add(currentRow);
            }

            dgMyChannels.DataSource = allChannels;
            dgMyChannels.DataBind();
            dgMyChannels.Visible = true;
        }
    }
}
