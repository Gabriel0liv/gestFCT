using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace GestaoFCT
{
    public partial class Documentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (Session["cargo"].ToString() != "1" && Session["cargo"].ToString() != "2" && Session["cargo"].ToString() != "3")
            //{
            //    //Redirect to login page.
            //    Response.Redirect("~/Login.aspx");
            //}

            //NomeUser.InnerText = Session["Utilizador"].ToString();

            if (rptItems.Items.Count == 0)
            {
                refresh();
            }


        }

        protected void refresh()
        {
            if (Session["cargo"].ToString() == "1")
            {
                String linhasql = "select * from tabelas_FCT;";
                DataTable dt = Database.GetFromDBSqlSrv(linhasql);

                rptItems.DataSource = dt;
                rptItems.DataBind();
            }

            if(Session["cargo"].ToString() == "2")
            {
                String linhasql = "select * from tabelas_FCT where id_professor =" + Session["codigo"].ToString() + ";";
                DataTable dt = Database.GetFromDBSqlSrv(linhasql);

                rptItems.DataSource = dt;
                rptItems.DataBind();
            }

            if (Session["cargo"].ToString() == "3")
            {
                String linhasql = "select * from tabelas_FCT where id_tutor =" + Session["codigo"].ToString() + ";";
                DataTable dt = Database.GetFromDBSqlSrv(linhasql);

                rptItems.DataSource = dt;
                rptItems.DataBind();
            }



        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx"); // redireciona para a página de login
        }
    }
}