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
    public partial class Sumarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Utilizador"] == null)
            {
                //Redirect to login page.
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                //Redirect to home page
                NomeUser.InnerText = Session["Utilizador"].ToString();
            }



           if (!IsPostBack)
            {
                if (rptItems.Items.Count == 0)
                {
                    refresh();
                }

                using (SqlConnection sqlConn = new SqlConnection(SumSQLData.ConnectionString))
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

            String linhasql2 = "select * from Sumarios;";
            DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);

            rptItems2.DataSource = dt2;
            rptItems2.DataBind();
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx"); // redireciona para a página de login
        }


        protected void reset()
        {

            txt_sumario.Text = "";



        }

        protected void Atualizar()
        {

            string linhadesql = "select * from Sumarios where id_sumario = " + labelCod.Text + ";";
            var sqlConn = new SqlConnection(SumSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                ddl_Tarefas.SelectedValue = r["tarefas_sumario"].ToString();
                txt_sumario.Text = r["descricao_sumario"].ToString();
                txt_numHora.Value = r["horas_sumario"].ToString();
                txt_status.Value = r["status_sumario"].ToString();

            }
            r.Close();
            sqlConn.Close();
        }

        protected void spanFechar_Click(object sender, EventArgs e)
        {
            formSum.Visible = false;
            formTar.Visible = false;
        }

        protected void Fechar(object sender, EventArgs e)
        {
            formSum.Visible = false;
            formTar.Visible = false;
            reset();
        }

        protected void CriarSum(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('aaaaa')</script>");
            operacao.Text = "1";
            reset();
            exampleModalFormTitle.InnerText = "Criar Sumário";
            btn_enviar.Text = "Criar Sumário";
            formSum.Visible = true;
            formTar.Visible = false;

        }

        protected void CriarTar(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('aaaaa')</script>");
            operacao.Text = "4";
            reset();
            exampleModalFormTitle.InnerText = "Criar Tarefa";
            btn_enviar.Text = "Criar Tarefa";
            formSum.Visible = false;
            formTar.Visible = true;
            

        }

        protected void EditarSum(object sender, EventArgs e)
        {
            operacao.Text = "2";
            labelCod.Text = HiddenField1.Value;

            if(labelCod.Text != "0")
            {
                Atualizar();
                exampleModalFormTitle.InnerText = "Editar Sumário";
                btn_enviar.Text = "Editar Sumário";
                formSum.Visible = true;
                formTar.Visible = false;

            }
            else
            {
                textoCancelar.InnerText = "Nenhum registo foi selecionado!";
                btnDeletar.Visible = false;
                exampleModal.Visible = true;

            }


        }

        protected void EditarTar(object sender, EventArgs e)
        {
            operacao.Text = "5";
            labelCod.Text = HiddenField1.Value;

            if (labelCod.Text != "0")
            {

                Atualizar();
                exampleModalFormTitle.InnerText = "Editar Tarefa";
                btn_enviar.Text = "Editar Tarefa";
                formSum.Visible = false;
                formTar.Visible = true;
            }
            else
            {
                textoCancelar.InnerText = "Nenhum registo foi selecionado!";
                btnDeletar.Visible = false;
                exampleModal.Visible = true;

            }


        }

        protected void EliminarSum(object sender, EventArgs e)
        {

            operacao.Text = "3";
            labelCod.Text = HiddenField1.Value;

            if(labelCod.Text != "0")
            {
                btnDeletar.Visible = true;
                textoCancelar.InnerText = "Tem certeza que deseja eliminar o Sumário?";
            }
            else
            {
                textoCancelar.InnerText = "Nenhum registo foi selecionado!";
                btnDeletar.Visible = false;
            }


            exampleModal.Visible = true;


        }

        protected void EliminarTar(object sender, EventArgs e)
        {

            operacao.Text = "6";
            labelCod.Text = HiddenField1.Value;

            if (labelCod.Text != "0")
            {
                btnDeletar.Visible = true;
                textoCancelar.InnerText = "Tem certeza que deseja eliminar a tarefa?";

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

                //String linhasql = "insert into alunos (nome_aluno, nif_aluno, morada_aluno, loc_aluno, email_aluno, cpostal_aluno, telefone_aluno, bi_aluno, valBi_aluno, pass_aluno, id_cargo) values('" + txt_nome.Value + "', '" + txt_nif.Value + "','" + txt_morada.Value + "', '" + txt_local.Value + "', '" + txt_email.Value + "' ,'" + txt_CodPost.Value + "', '" + txt_telefone.Value +  "', '" + txt_bi.Value + "', '" + txt_val.Value + "', '" + txt_pass.Value + "', 4);";

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                //Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
                formSum.Visible = true;
            }

            if (operacao.Text == "2")
            {
                //Response.Write("<script>alert('22222')</script>");

                //String linhasql = "update alunos set nome_aluno = '" + txt_nome.Value + "', nif_aluno = '" + txt_nif.Value + "', email_aluno = '" + txt_email.Value + "', loc_aluno = '" + txt_local.Value + "', morada_aluno = '" + txt_morada.Value +  "', telefone_aluno = '" + txt_telefone.Value +  "', cpostal_aluno = '" + txt_CodPost.Value + "', bi_aluno = '" + txt_bi.Value + "', valBi_aluno = '" + txt_val.Value + "', pass_aluno = '" + txt_pass.Value + "' where id_aluno = " + labelCod.Text + ";";

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");
                //Response.Write("<script>alert('aaaaa')</script>");

                //Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
                
            }

            if (operacao.Text == "3")
            {
                //Response.Write("<script>alert('33333')</script>");

                //String linhasql = "delete from alunos where id_aluno = " + labelCod.Text + ";";
                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                //Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
            }


            formSum.Visible = false;
            exampleModal.Visible = false;
        }
        
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            exampleModal.Visible = false;
        }



        protected void ddl_entidade_SelectedIndexChanged1(object sender, EventArgs e)
        {
            String linhasql = "select * from Tarefas_table where id_entidade =" + ddl_entidade.SelectedValue + ";";
            DataTable dt = Database.GetFromDBSqlSrv(linhasql);

            rptItems.DataSource = dt;
            rptItems.DataBind();

            String linhasql2 = "select * from sumarios where id_entidade =" + ddl_entidade.SelectedValue + ";";
            DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);

            rptItems2.DataSource = dt2;
            rptItems2.DataBind();
        }
    }
}