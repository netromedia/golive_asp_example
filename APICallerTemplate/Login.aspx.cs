using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace APICallerTemplate
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack){
                Session["Token"] = "";
                Session["UserName"] = "";
            }
        }

        protected void Page_Command(Object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Login")
            {
                string sUserName = txtUserName.Text;
                
                Dictionary<string, string> sPostData = new Dictionary<string, string>();
                sPostData.Add("Email", sUserName);
                sPostData.Add("Password", txtPassword.Text);

                Dictionary<string, object> dctLoginResults = API.CallApi("Login", sPostData);

                if (dctLoginResults["Status"].Equals("Success"))
                {
                    // Successfully logged in!  Store token.
                    Dictionary<string, object> successData = (Dictionary<string, object>)dctLoginResults["Data"];
                    Session["Token"] = successData["Token"];
                    Session["UserName"] = sUserName;
                    Response.Redirect("~/MyChannels.aspx");
                    return;
                }

                loginfeedback.InnerText = dctLoginResults["Error"].ToString();
                // Failed to log in.
            }
        }
    }
}
