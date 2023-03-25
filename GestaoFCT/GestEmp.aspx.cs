using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestaoFCT
{
    public partial class GestEmp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (Session["Utilizador"] == null)
            //{
            //    //Redirect to login page.
            //    Response.Redirect("~/Login.aspx");
            //}
            //else
            //{
            //    //Redirect to home page
            //    NomeUser.InnerText = Session["Utilizador"].ToString();
            //}

            if (rptItems.Items.Count == 0)
            {
                refresh();
            }

        }

        protected void refresh()
        {
            String linhasql = "select * from Empresas;";
            DataTable dt = Database.GetFromDBSqlSrv(linhasql);

            rptItems.DataSource = dt;
            rptItems.DataBind();
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx"); // redireciona para a página de login
        }


        protected void reset()
        {
            txt_atvPrinc.Value = "";
            txt_CodPost.Value = "";
            txt_email.Value = "";
            txt_local.Value = "";
            txt_NatJuri.Value = "";
            txt_nif.Value = "";
            txt_resp.Value = "";

        }

        protected void btn_enviar_Click(object sender, EventArgs e)
        {

            String linhasql = "insert into Empresas (nome_empresa, nif_empresa, ende_empresa, loc_empresa, cpostal_empresa, natJuridica, responsavel, ativ_principal) values('" + txt_nome.Value + "', '" + txt_nif.Value + "','" + txt_email.Value + "', '" + txt_local.Value + "' ,'" + txt_CodPost.Value + "', '" + txt_NatJuri.Value + "', '" + txt_resp.Value + "', '" + txt_atvPrinc.Value + "');";

            //Response.Write("<script>alert('" + linhasql + "')</script>");
            //Response.Write("<script>alert('aaaaa')</script>");

            //Database.NonQuerySqlSrv(linhasql);
            //reset();
            //refresh();


        }

        protected void Criar()
        {
            Response.Write("<script>alert('aaaaa')</script>");

        }

        protected void Editar(object sender, EventArgs e)
        {
            txt_nome.Value = "ESTOU EDITANDO";
        }
    }
}