using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestaoFCT
{
    public partial class teste1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ShowModal_Click(object sender, EventArgs e)
        {
            ModalPanel.Visible = true;

            TextBox1.Text = "aloo";
        }
    }
}