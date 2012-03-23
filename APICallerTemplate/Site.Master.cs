using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace APICallerTemplate
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        string sToken = "";
        string sUserName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            sToken = (Session["Token"] == null ? "" : Session["Token"].ToString());
            sUserName = (Session["UserName"] == null ? "" : Session["UserName"].ToString());

            if (string.IsNullOrEmpty(sToken))
            {
                pnlNavNotLoggedIn.Visible = true;
                pnlNavDefault.Visible = false;
                return;
            }

            pnlNavNotLoggedIn.Visible = false;
            pnlNavDefault.Visible = true;
            spnUserName.InnerText = sUserName;

        }
    }
}
