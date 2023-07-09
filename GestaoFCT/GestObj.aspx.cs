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
    public partial class GestObj : System.Web.UI.Page
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
                if (Session["cargo"].ToString() == "1")
                    divCurso.Visible = true;

                //Redirect to home page
                NomeUser.InnerText = Session["Utilizador"].ToString();
            }

            if (rptItems.Items.Count == 0)
            {
                refresh ();
            }

        }

        protected void refresh()
        {
            if (Session["cargo"].ToString() == "1" )
            {
                String linhasql = "SELECT * from Objetivos_table;";
                DataTable dt = Database.GetFromDBSqlSrv(linhasql);

                rptItems.DataSource = dt;
                rptItems.DataBind();
            }
            else
            {
                String linhasql = "SELECT * from Objetivos_table where id_curso = " + Session["curso"].ToString() + ";";
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


        protected void reset()
        {
            txt_nome.Value = "";

            using (SqlConnection sqlConn = new SqlConnection(ObjSQLData.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select id_curso, nome_curso from Cursos;", sqlConn);
                sqlConn.Open();
                slc_curso.DataTextField = "nome_curso";
                slc_curso.DataValueField = "id_curso";
                slc_curso.DataSource = cmd.ExecuteReader();
                slc_curso.DataBind();
                sqlConn.Close();
            }

        }

        protected void Atualizar()
        {

            string linhadesql = "select * from Objetivos where id_objetivo = " + labelCod.Text + ";";
            var sqlConn = new SqlConnection(ObjSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                txt_nome.Value = r["nome_curso"].ToString();

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
            exampleModalFormTitle.InnerText = "Criar Objetivo do curso";
            btn_enviar.Text = "Criar Objetivo";
            exampleModalForm.Visible = true;

        }

        protected void Editar(object sender, EventArgs e)
        {
            operacao.Text = "2";
            labelCod.Text = HiddenField1.Value;

            if(labelCod.Text != "0")
            {

                Atualizar();
                exampleModalFormTitle.InnerText = "Editar Objetivo";
                btn_enviar.Text = "Editar Objetivo";
                exampleModalForm.Visible = true;
            }
            else
            {
                exampleModalFormTitle.InnerText = "Nenhum Registo ";
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

                textoCancelar.InnerText = "Deseja eliminar o registo ?";
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
                String linhasql = "";
                Boolean erro = false;

                linhasql = "select * from Objetivos where id_curso = " + Session["curso"].ToString() + ";";
                DataTable dt = Database.GetFromDBSqlSrv(linhasql);

                if (dt.Rows.Count < 14)
                    erro = false;
                else
                {
                    erro = true;
                    alerMessage.InnerText = "Não pode haver mais do que 14 objetivos por curso!";
                }

                if (!erro)
                {
                    if (Session["cargo"].ToString() == "1")
                        linhasql = "insert into Objetivos (descricao_objetivo, id_curso) values('" + txt_nome.Value + "', " + slc_curso.SelectedValue + ");";
                    else
                        linhasql = "insert into Objetivos (descricao_objetivo, id_curso) values('" + txt_nome.Value + "', " + Session["curso"].ToString() + ");";

                    Database.NonQuerySqlSrv(linhasql);
                    reset();
                    refresh();
                    exampleModalForm.Visible = false;
                    exampleModal.Visible = false;
                }
                else
                {
                    Alert.Visible = true;

                }

            }

            if (operacao.Text == "2")
            {
                //Response.Write("<script>alert('22222')</script>");
                String linhasql = "";
                if(Session["cargo"].ToString() == "1")
                    linhasql = "update cursos set descricao_objetivo = '" + txt_nome.Value + "', turma_curso = '" + slc_curso.SelectedValue + "' where id_objetivo = " + labelCod.Text + ";";
                else
                    linhasql = "update cursos set descricao_objetivo = '" + txt_nome.Value + "', turma_curso = '" + Session["curso"].ToString() + "' where id_objetivo = " + labelCod.Text + ";";

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();

                exampleModalForm.Visible = false;
                exampleModal.Visible = false;

            }

            if (operacao.Text == "3")
            {

                String linhasql = "delete from Objetivos where id_objetivo = " + labelCod.Text + ";";

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
                exampleModalForm.Visible = false;
                exampleModal.Visible = false;
            }


        }
        
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            exampleModal.Visible = false;
        }
    }
}