using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            String linhasql = "select * from entidades;";
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
            txt_nome.Value = "";

        }

        protected void btn_enviar_Click(object sender, EventArgs e)
        {

            String linhasql = "insert into Entidades (nome_entidade, nif_entidade, ende_entidade, loc_entidade, cpostal_entidade, natJuridica, resp_entidade, ativ_principal) values('" + txt_nome.Value + "', '" + txt_nif.Value + "','" + txt_email.Value + "', '" + txt_local.Value + "' ,'" + txt_CodPost.Value + "', '" + txt_NatJuri.Value + "', '" + txt_resp.Value + "', '" + txt_atvPrinc.Value + "');";

            //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");
            //Response.Write("<script>alert('aaaaa')</script>");

            //Database.NonQuerySqlSrv(linhasql);
            //reset();
            //refresh();


        }

        protected void Criar(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('aaaaa')</script>");
            
            exampleModalForm.Visible = true;


        }

        protected void Atualizar()
        {

            string linhadesql = "select * from Entidades where id_entidade = " + LabelCod.Text + ";";
            var sqlConn = new SqlConnection(EntSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                txt_nome.Value = "" + r["nome_entidade"];
                txt_nif.Value = "" + r["nif_entidade"];
                txt_email.Value = "" + r["email_entidade"];

            }
            r.Close();
            sqlConn.Close();
        }

            protected void spanFechar_Click(object sender, EventArgs e)
        {
            exampleModalForm.Visible = false;
        }

        protected void Fechar(object sender, EventArgs e)
        {
            exampleModalForm.Visible = false;
            reset();
        }

        protected void Editar(object sender, EventArgs e)
        {
            LabelCod.Text = HiddenField1.Value;
            Atualizar();
            exampleModalForm.Visible = true;
            //txt_nome.Value = "ESTOU EDITANDO";
        }
    }
}