using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestaoFCT
{
    public partial class GestAluno : System.Web.UI.Page
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
            String linhasql = "select * from Alunos;";
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
            txt_bi.Value = "";
            txt_val.Value = "";
            txt_morada.Value = "";
            txt_local.Value = "";
            txt_CodPost.Value = "";
            txt_pass.Value = "";


        }

        protected void Atualizar()
        {

            string linhadesql = "select * from Alunos where id_aluno = " + labelCod.Text + ";";
            var sqlConn = new SqlConnection(AlnSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                txt_nome.Value = r["nome_aluno"].ToString();
                txt_nif.Value = r["nif_aluno"].ToString();
                txt_email.Value = r["email_aluno"].ToString();
                txt_telefone.Value = r["telefone_aluno"].ToString();
                txt_bi.Value = r["bi_aluno"].ToString();
                txt_val.Value = r["valBi_aluno"].ToString();
                txt_morada.Value = r["morada_aluno"].ToString();
                txt_local.Value = r["loc_aluno"].ToString();
                txt_CodPost.Value = r["cpostal_aluno"].ToString();
                txt_pass.Value = r["pass_aluno"].ToString();

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
            exampleModalFormTitle.InnerText = "Criar Aluno";
            btn_enviar.Text = "Criar Aluno";
            exampleModalForm.Visible = true;

        }

        protected void Editar(object sender, EventArgs e)
        {
            operacao.Text = "2";
            labelCod.Text = HiddenField1.Value;

            if(labelCod.Text != "0")
            {

                Atualizar();
                exampleModalFormTitle.InnerText = "Editar Aluno";
                btn_enviar.Text = "Editar Aluno";
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
                string linhadesql = "select nome_aluno from alunos where id_aluno = " + labelCod.Text + ";";
                var sqlConn = new SqlConnection(AlnSQLData.ConnectionString);
                var com = new SqlCommand(linhadesql, sqlConn);
                sqlConn.Open();
                SqlDataReader r = com.ExecuteReader();
                r.Read();
                textoCancelar.InnerText = "Deseja eliminar o registo \"" + r["nome_aluno"] + "\"?";
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


            if (operacao.Text == "1")
            {
                //Response.Write("<script>alert('11111')</script>");

                String linhasql = "insert into alunos (nome_aluno, nif_aluno, morada_aluno, loc_aluno, email_aluno, cpostal_aluno, telefone_aluno, bi_aluno, valBi_aluno, pass_aluno) values('" + txt_nome.Value + "', '" + txt_nif.Value + "','" + txt_morada.Value + "', '" + txt_local.Value + "', '" + txt_email.Value + "' ,'" + txt_CodPost.Value + "', '" + txt_telefone.Value +  "', '" + txt_bi.Value + "', '" + txt_val.Value + "', '" + txt_pass.Value + "');";

                Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
                exampleModalForm.Visible = false;
            }

            if (operacao.Text == "2")
            {
                //Response.Write("<script>alert('22222')</script>");

                String linhasql = "update alunos set nome_aluno = '" + txt_nome.Value + "', nif_aluno = '" + txt_nif.Value + "', email_aluno = '" + txt_email.Value + "', loc_aluno = '" + txt_local.Value + "', morada_aluno = '" + txt_morada.Value +  "', telefone_aluno = '" + txt_telefone.Value +  "', cpostal_aluno = '" + txt_CodPost.Value + "', bi_aluno = '" + txt_bi.Value + "', valBi_aluno = '" + txt_val.Value + "', pass_aluno = '" + txt_pass.Value + "' where id_aluno = " + labelCod.Text + ";";

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");
                //Response.Write("<script>alert('aaaaa')</script>");

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
                
            }

            if (operacao.Text == "3")
            {
                //Response.Write("<script>alert('33333')</script>");

                String linhasql = "delete from alunos where id_aluno = " + labelCod.Text + ";";
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