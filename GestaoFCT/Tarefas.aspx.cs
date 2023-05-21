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
    public partial class Tarefas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Utilizador"] == null || Session["cargo"] == null)
            {
                //Redirect to login page.
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                //Redirect to home page
                NomeUser.InnerText = Session["Utilizador"].ToString();
            }

            if (Session["cargo"].ToString() == "4")
            {

                NavAln.Visible = false;
                NavCurso.Visible = false;
                NavEE.Visible = false;
                NavEnt.Visible = false;
                NavFCT.Visible = false;
                NavProf.Visible = false;
                SecGest.Visible = false;
                NavTar.Visible = false;
                NavTut.Visible = false;
            }


            if (Session["cargo"].ToString() == "1" || Session["cargo"].ToString() == "2")
                ddl_entidade.Visible = true; 
            else
                ddl_entidade.Visible = false;




            if (!IsPostBack)
            {
                if (rptItems.Items.Count == 0)
                {
                    refresh();
                }


                using (SqlConnection sqlConn = new SqlConnection(TarSQLData.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("select id_entidade, nome_entidade from entidades;", sqlConn);
                    sqlConn.Open();
                    ddl_entidade.DataTextField = "nome_entidade";
                    ddl_entidade.DataValueField = "id_entidade";
                    ddl_entidade.DataSource = cmd.ExecuteReader();
                    ddl_entidade.DataBind();
                    sqlConn.Close();
                }
            }


        }

        protected void refresh()
        {
            String linhasql = "select * from Tarefas_table;";
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

            txt_tarefa.Value = "";

            if (Session["cargo"].ToString() == "1" || Session["cargo"].ToString() == "2")
            {

                using (SqlConnection sqlConn = new SqlConnection(TarSQLData.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand("select id_entidade, nome_entidade from Entidades;", sqlConn);
                    sqlConn.Open();
                    ddl_TarEntidade.DataTextField = "nome_entidade";
                    ddl_TarEntidade.DataValueField = "id_entidade";
                    ddl_TarEntidade.DataSource = cmd.ExecuteReader();
                    ddl_TarEntidade.DataBind();
                    sqlConn.Close();

                    SqlCommand cmd2 = new SqlCommand("select id_tutor, nome_tutor from tutores;", sqlConn);
                    sqlConn.Open();
                    ddl_TarTutor.DataTextField = "nome_tutor";
                    ddl_TarTutor.DataValueField = "id_tutor";
                    ddl_TarTutor.DataSource = cmd2.ExecuteReader();
                    ddl_TarTutor.DataBind();
                    sqlConn.Close();
                }
            }

        }

        protected void Atualizar()
        {

            string linhadesql = "select * from Tarefas where id_tarefa = " + labelCod.Text + ";";
            var sqlConn = new SqlConnection(TarSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                txt_tarefa.Value = r["descricao_tarefa"].ToString();
                ddl_TarEntidade.SelectedValue = r["id_entidade"].ToString();
                ddl_TarTutor.SelectedValue = r["id_tutor"].ToString();
            }
            r.Close();
            sqlConn.Close();
        }

        protected void spanFechar_Click(object sender, EventArgs e)
        {
            formTar.Visible = false;
        }

        protected void Fechar(object sender, EventArgs e)
        {
            formTar.Visible = false;
            reset();
        }

        protected void Criar(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('aaaaa')</script>");
            operacao.Text = "1";
            reset();
            TarTitle.InnerText = "Criar Tarefa";

            if (Session["cargo"].ToString() == "1" || Session["cargo"].ToString() == "2")
            {
                ChooseDiv.Visible = true;

                using (SqlConnection sqlConn = new SqlConnection(TarSQLData.ConnectionString))
                {

                    SqlCommand cmd = new SqlCommand("select id_entidade, nome_entidade from Entidades;", sqlConn);
                    sqlConn.Open();
                    ddl_TarEntidade.DataTextField = "nome_entidade";
                    ddl_TarEntidade.DataValueField = "id_entidade";
                    ddl_TarEntidade.DataSource = cmd.ExecuteReader();
                    ddl_TarEntidade.DataBind();
                    sqlConn.Close();

                    SqlCommand cmd2 = new SqlCommand("select id_tutor, nome_tutor from tutores;", sqlConn);
                    sqlConn.Open();
                    ddl_TarTutor.DataTextField = "nome_tutor";
                    ddl_TarTutor.DataValueField = "id_tutor";
                    ddl_TarTutor.DataSource = cmd2.ExecuteReader();
                    ddl_TarTutor.DataBind();
                    sqlConn.Close();

                }

            }
            else
                ChooseDiv.Visible = false;


            formTar.Visible = true;

        }


        protected void Editar(object sender, EventArgs e)
        {
            operacao.Text = "2";
            labelCod.Text = HiddenField1.Value;

            if (labelCod.Text != "0")
            {

                if (Session["cargo"].ToString() == "1" || Session["cargo"].ToString() == "2")
                {
                    ChooseDiv.Visible = true;

                    using (SqlConnection sqlConn = new SqlConnection(TarSQLData.ConnectionString))
                    {

                        SqlCommand cmd = new SqlCommand("select id_entidade, nome_entidade from Entidades;", sqlConn);
                        sqlConn.Open();
                        ddl_TarEntidade.DataTextField = "nome_entidade";
                        ddl_TarEntidade.DataValueField = "id_entidade";
                        ddl_TarEntidade.DataSource = cmd.ExecuteReader();
                        ddl_TarEntidade.DataBind();
                        sqlConn.Close();

                        SqlCommand cmd2 = new SqlCommand("select id_tutor, nome_tutor from tutores;", sqlConn);
                        sqlConn.Open();
                        ddl_TarTutor.DataTextField = "nome_tutor";
                        ddl_TarTutor.DataValueField = "id_tutor";
                        ddl_TarTutor.DataSource = cmd2.ExecuteReader();
                        ddl_TarTutor.DataBind();
                        sqlConn.Close();

                    }

                }
                else
                    ChooseDiv.Visible = false;

                Atualizar();
                TarTitle.InnerText = "Editar Tarefa";
                formTar.Visible = true;

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
                textoCancelar.InnerText = "Tem certeza que deseja eliminar a Tarefa?";
            }
            else
            {
                textoCancelar.InnerText = "Nenhum registo foi selecionado!";
                btnDeletar.Visible = false;
            }


            exampleModal.Visible = true;


        }


        protected void Comandos(object sender, EventArgs e)
        {


            if (operacao.Text == "1")
            {
                //Response.Write("<script>alert('11111')</script>");
                string linhasql = "";

                if (Session["cargo"].ToString() == "1" || Session["cargo"].ToString() == "2")
                {
                    linhasql = "insert into Tarefas (descricao_tarefa, id_entidade, id_tutor) values('" + txt_tarefa.Value + "', " + ddl_TarEntidade.SelectedValue + ", " + ddl_TarTutor.SelectedValue + ");";
                
                }
                else if (Session["cargo"].ToString() == "3")
                {
                    linhasql = "insert into Tarefas (descricao_tarefa, id_entidade, id_tutor) values('" + txt_tarefa.Value + "', " + Session["Entidade"].ToString() + ", " + Session["codigo"].ToString() + ");";
                }

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();

                formTar.Visible = false;
            }

            if (operacao.Text == "2")
            {
                //Response.Write("<script>alert('22222')</script>");

                String linhasql = "";

                if (Session["cargo"].ToString() == "1" || Session["cargo"].ToString() == "2")
                {
                    linhasql = "UPDATE Tarefas set descricao_tarefa ='" + txt_tarefa.Value + "', id_entidade = " + ddl_TarEntidade.SelectedValue + ", id_tutor = " + ddl_TarTutor.SelectedValue + " where id_tarefa = " + labelCod.Text + ";";

                }
                else if (Session["cargo"].ToString() == "3")
                {
                    linhasql = "UPDATE Tarefas set descricao_tarefa = '" + txt_tarefa.Value + "', id_entidade = " + Session["Entidade"].ToString() + ", id_tutor = " + Session["codigo"].ToString() + " where id_tarefa = " + labelCod.Text + ";";
                }

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");
                //Response.Write("<script>alert('aaaaa')</script>");

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();

                formTar.Visible = false;
            }

            if (operacao.Text == "3")
            {
                //Response.Write("<script>alert('33333')</script>");

                String linhasql = "delete from tarefas where id_tarefa = " + labelCod.Text + ";";
                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
            }

            exampleModal.Visible = false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            exampleModal.Visible = false;
        }

        protected void ddl_TarEntidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            using (SqlConnection sqlConn = new SqlConnection(TarSQLData.ConnectionString))
            {

                SqlCommand cmd2 = new SqlCommand("select id_tutor, nome_tutor from tutores where id_entidade =" + ddl_TarEntidade.SelectedValue + ";", sqlConn);
                sqlConn.Open();
                ddl_TarTutor.DataTextField = "nome_tutor";
                ddl_TarTutor.DataValueField = "id_tutor";
                ddl_TarTutor.DataSource = cmd2.ExecuteReader();
                ddl_TarTutor.DataBind();
                sqlConn.Close();

            }

        }

        protected void ddl_entidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            String linhasql = "select * from Tarefas_table where id_entidade =" + ddl_entidade.SelectedValue + ";";
            DataTable dt = Database.GetFromDBSqlSrv(linhasql);

            rptItems.DataSource = dt;
            rptItems.DataBind();

        }
    }
}