using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestaoFCT
{
    public partial class GestCursos : System.Web.UI.Page
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
            String linhasql = "select * from Cursos;";
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
            txt_turma.Value = "";

        }

        protected void Atualizar()
        {

            string linhadesql = "select * from cursos where id_curso = " + labelCod.Text + ";";
            var sqlConn = new SqlConnection(CursoSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                txt_nome.Value = r["nome_curso"].ToString();
                slc_ano.SelectedValue = r["ano_curso"].ToString();
                txt_turma.Value = r["turma_curso"].ToString();
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
            exampleModalFormTitle.InnerText = "Criar Curso";
            btn_enviar.Text = "Criar Curso";
            exampleModalForm.Visible = true;

        }

        protected void Editar(object sender, EventArgs e)
        {
            operacao.Text = "2";
            labelCod.Text = HiddenField1.Value;

            if (labelCod.Text != "0")
            {

                Atualizar();
                exampleModalFormTitle.InnerText = "Editar cursos";
                btn_enviar.Text = "Editar cursos";
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

            if (labelCod.Text != "0")
            {
                btnDeletar.Visible = true;
                string linhadesql = "select nome_curso from cursos where id_curso = " + labelCod.Text + ";";
                var sqlConn = new SqlConnection(CursoSQLData.ConnectionString);
                var com = new SqlCommand(linhadesql, sqlConn);
                sqlConn.Open();
                SqlDataReader r = com.ExecuteReader();
                r.Read();
                textoCancelar.InnerText = "Deseja eliminar o registo \"" + r["nome_curso"] + "\"?";
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

            if (operacao.Text != "3")
            {
                if (txt_nome.Value.Replace(" ", "") == "")
                {
                    erro = true;
                    alerMessage.InnerText = "O nome do curso não pode conter caracteres vazios!";
                    Alert.Visible = true;
                }
                else if (GlobalFunctions.HasSqlInjection(txt_nome.Value))
                {
                    erro = true;
                    if (GlobalFunctions.SqlInjectionChecker(txt_nome.Value))
                    {
                        alerMessage.InnerHtml = "Nome do curso inserido inválido. <br/> (Palavra reservada SQL encontrada).";
                        Alert.Visible = true;
                    }
                    else //se não foi uma palavra reservada, então foi por algum caractere especial
                    {
                        alerMessage.InnerHtml = "Caracteres inválidos no nome do curso. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
                        Alert.Visible = true;
                    }
                }
            }

            if (operacao.Text == "1")
            {
                String linhasql = "insert into cursos (nome_curso, ano_curso, turma_curso) values('" + txt_nome.Value + "', '" + slc_ano.SelectedValue + "', '" + txt_turma.Value + "');";

                if (!erro)
                {
                    Database.NonQuerySqlSrv(linhasql);
                    reset();
                    refresh();
                    exampleModalForm.Visible = false;
                }

            }

            if (operacao.Text == "2")
            {

                String linhasql = "update cursos set nome_curso = '" + txt_nome.Value + "', ano_curso = '" + slc_ano.SelectedValue + "', turma_curso = '" + txt_turma.Value + "' where id_curso = " + labelCod.Text + ";";

                if (!erro)
                {
                    Database.NonQuerySqlSrv(linhasql);
                    reset();
                    refresh();
                    exampleModalForm.Visible = false;
                }
            }

            if (operacao.Text == "3")
            {
                String linhasql = "delete from cursos where id_curso = " + labelCod.Text + ";";

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
                exampleModal.Visible = false;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            exampleModal.Visible = false;
        }
    }
}