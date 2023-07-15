using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestaoFCT
{
    public partial class GestProf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["cargo"].ToString() != "1" && Session["cargo"].ToString() != "2")
            {
                //Redirect to login page.
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                //Redirect to home page
                NomeUser.InnerText = Session["Utilizador"].ToString();
            }

            if (rptItems.Items.Count == 0)
            {
                refresh();
            }

        }

        protected void refresh()
        {
            String linhasql = "select * from professores;";
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

            txt_nome.Value = "";
            txt_nif.Value = "";
            txt_email.Value = "";
            txt_telefone.Value = "";
            txt_telemovel.Value = "";
            txt_morada.Value = "";
            txt_local.Value = "";
            txt_CodPost.Value = "";
            txt_pass.Value = "";


        }

        protected void Atualizar()
        {

            string linhadesql = "select * from professores where id_prof = " + labelCod.Text + ";";
            var sqlConn = new SqlConnection(ProfSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                txt_nome.Value = r["nome_prof"].ToString();
                txt_nif.Value = r["nif_prof"].ToString();
                txt_email.Value = r["email_prof"].ToString();
                txt_telefone.Value = r["telefone_prof"].ToString();
                txt_telemovel.Value = r["telemovel_prof"].ToString();
                txt_morada.Value = r["morada_prof"].ToString();
                txt_local.Value = r["loc_prof"].ToString();
                txt_CodPost.Value = r["cpostal_prof"].ToString();
                txt_pass.Value = Encoding.UTF8.GetString(Convert.FromBase64String(r["pass_prof"].ToString()));

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

        protected void Criar(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('aaaaa')</script>");
            operacao.Text = "1";
            reset();
            exampleModalFormTitle.InnerText = "Criar Professor";
            btn_enviar.Text = "Criar Professor";
            exampleModalForm.Visible = true;

        }

        protected void Editar(object sender, EventArgs e)
        {
            operacao.Text = "2";
            labelCod.Text = HiddenField1.Value;

            if(labelCod.Text != "0")
            {

                Atualizar();
                exampleModalFormTitle.InnerText = "Editar Professor";
                btn_enviar.Text = "Editar Professor";
                exampleModalForm.Visible = true;
            }
            else
            {
                textoCancelar.InnerText = "Nenhum registo foi selecionado!";
                btnDeletar.Visible = false;
                exampleModal.Visible = true;

            }


        }

        protected void Eliminar(object sender, EventArgs e)
        {

            operacao.Text = "3";
            labelCod.Text = HiddenField1.Value;

            if(labelCod.Text != "0")
            {
                btnDeletar.Visible = true;
                string linhadesql = "select nome_prof from professores where id_prof = " + labelCod.Text + ";";
                var sqlConn = new SqlConnection(ProfSQLData.ConnectionString);
                var com = new SqlCommand(linhadesql, sqlConn);
                sqlConn.Open();
                SqlDataReader r = com.ExecuteReader();
                r.Read();
                textoCancelar.InnerText = "Deseja eliminar o registo \"" + r["nome_prof"] + "\"?";
                r.Close();
                sqlConn.Close();
            }
            else
            {
                textoCancelar.InnerText = "Nenhum registo foi selecionado!";
                //textoCancelar.Style[""]
                btnDeletar.Visible = false;
            }


            exampleModal.Visible = true;


        }

        
        protected void Comandos(object sender, EventArgs e)
        {

            Boolean erro = false;

            if (txt_nome.Value.Replace(" ", "") == "")
            {
                erro = true;
                
            }
                
            if (operacao.Text == "1")
            {
                //Response.Write("<script>alert('11111')</script>");

                String linhasql = "insert into professores (nome_prof, nif_prof, morada_prof, loc_prof, email_prof, cpostal_prof, telefone_prof, telemovel_prof, pass_prof, id_cargo) values('" + txt_nome.Value + "', '" + txt_nif.Value + "','" + txt_morada.Value + "', '" + txt_local.Value + "', '" + txt_email.Value + "' ,'" + txt_CodPost.Value + "', '" + txt_telefone.Value +  "', '" + txt_telemovel.Value + "', '" + Convert.ToBase64String(Encoding.ASCII.GetBytes(txt_pass.Value)) + "', 2);";

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
                exampleModalForm.Visible = false;
            }

            if (operacao.Text == "2")
            {
                //Response.Write("<script>alert('22222')</script>");

                String linhasql = "update professores set nome_prof = '" + txt_nome.Value + "', nif_prof = '" + txt_nif.Value + "', email_prof = '" + txt_email.Value + "', loc_prof = '" + txt_local.Value + "', morada_prof = '" + txt_morada.Value +  "', telefone_prof = '" + txt_telefone.Value +  "', cpostal_prof = '" + txt_CodPost.Value + "', telemovel_prof = '" + txt_telemovel.Value + "', pass_prof = '" + Convert.ToBase64String(Encoding.ASCII.GetBytes(txt_pass.Value)) + "' where id_prof = " + labelCod.Text + ";";

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");
                //Response.Write("<script>alert('aaaaa')</script>");

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
                
            }

            if (operacao.Text == "3")
            {
                //Response.Write("<script>alert('33333')</script>");

                String linhasql = "delete from professores where id_prof = " + labelCod.Text + ";";
                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
            }

            exampleModalForm.Visible = false;
            exampleModal.Visible = false;
        }
        
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            exampleModal.Visible = false;
        }
    }
}