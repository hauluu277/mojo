using mojoPortal.Service.CommonBusiness;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.Secure
{
    public partial class HandleSignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var pr_token = Request.Params.Get("token");
                if (!string.IsNullOrEmpty(pr_token))
                {
                    if (!string.IsNullOrEmpty(pr_token))
                    {
                        var getToken = ReadToken(pr_token);
                        if (getToken != null)
                        {
                            FormsAuthentication.SetAuthCookie(getToken.username, true);
                            SiteUtils.RedirectToDefault();
                            //var redirectAffterAuthenticate = WebConfigSettings.ApiSSO_SignInSuccess;
                            //HttpContext.Current.Response.Redirect(redirectAffterAuthenticate);

                            return;
                        }
                    }
                }
            }
        }

        private TokenDto ReadToken(string token)
        {
            using (var client = new HttpClient())
            {
                //sử dụng HttpPost
                var apiGetUserInfo = string.Format("{0}?token={1}", WebConfigSettings.ApiSSO_DefineToken, token);
                //Passing service base url
                client.BaseAddress = new Uri(apiGetUserInfo);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent("", Encoding.UTF8, "application/json");
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                var response = client.PostAsync(apiGetUserInfo, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var readResponse = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<TokenDto>(readResponse);
                    return result;
                }
                //returning the employee list to view
            }
            return null;
        }

    }
}