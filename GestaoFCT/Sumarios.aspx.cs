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
        static string Xis = "";
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

            if (IsPostBack)
            {


                if (TextBox1.Text != "")
                {
                    //TextBox1.Text = TextBox1.Text.Replace(',', ';');
                    ClientScript.RegisterStartupScript(
                          this.GetType(),
                          "showAlert",
                          "showAlert(\"" + TextBox1.Text + "\")",
                          true);

                    //string[] array = TextBox1.Text.Split(',');
                    //int[] result = new int[array.Length];

                    //for (int i = 0; i < array.Length; i++)
                    //{
                    //    result[i] = int.Parse(array[i]);
                    //}

                    //foreach (int i in result)
                    //{
                    //    ddl_Tarefas.Items.FindByValue(i.ToString()).Selected = true;
                    //}
                }

            }

           if (!IsPostBack)
            {
                if (rptItems2.Items.Count == 0)
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
            txt_dataSum.Value = "";


            using (SqlConnection sqlConn = new SqlConnection(SumSQLData.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select id_tarefa, descricao_tarefa from Tarefas;", sqlConn);
                sqlConn.Open();
                ddl_Tarefas.DataTextField = "descricao_tarefa";
                ddl_Tarefas.DataValueField = "id_tarefa";
                ddl_Tarefas.DataSource = cmd.ExecuteReader();
                ddl_Tarefas.DataBind();
                sqlConn.Close();
            }

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

        }

        protected void Fechar(object sender, EventArgs e)
        {
            formSum.Visible = false;
            reset();
        }

        protected void Criar(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('aaaaa')</script>");
            operacao.Text = "1";
            reset();
            exampleModalFormTitle.InnerText = "Criar Sumário";
            btn_enviar.Text = "Criar Sumário";
            formSum.Visible = true;

        }


        protected void Editar(object sender, EventArgs e)
        {
            operacao.Text = "2";
            labelCod.Text = HiddenField1.Value;

            if(labelCod.Text != "0")
            {
                Atualizar();
                exampleModalFormTitle.InnerText = "Editar Sumário";
                btn_enviar.Text = "Editar Sumário";
                formSum.Visible = true;

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
                textoCancelar.InnerText = "Tem certeza que deseja eliminar o Sumário?";
            }
            else
            {
                textoCancelar.InnerText = "Nenhum registo foi selecionado!";
                btnDeletar.Visible = false;
            }


            exampleModal.Visible = true;


        }


        public static string MinhaFuncaoCSharp(string variaveis)
        {
            // Acessar as variáveis individuais
            
            string parametro1 = variaveis.ToString();
            string parametro2 = variaveis.ToString();


            // Código da sua função C#
            Xis = variaveis.ToString();
            // Retornar um resultado
            return "";
        }

        protected void Comandos(object sender, EventArgs e)
        {


            if (operacao.Text == "1")
            {

                //string parametro1 = variaveis.ToString();

                //Response.Write("<script>alert('11111')</script>");

                if (Session["cargo"].ToString() == "4")
                {
                    String linhasql = "insert into Sumarios (descricao_sumario, horas_sumario, status_sumario, data_sumario, id_fct) values('" + txt_sumario.Text + "', '" + txt_numHora.Value + "','" + txt_status.Value + "', '" + txt_dataSum.Value + "', (select id_fct from FichasFCT where id_aluno = " + Session["codigo"].ToString() + ") );";

                    //Database.NonQuerySqlSrv(linhasql)

                    string[] cortado = TextBox1.Text.Split(',');
                    int[] result = new int[cortado.Length];
                    for (int i = 0; i < cortado.Length; i++)
                    {
                        result[i] = int.Parse(cortado[i]);
                    }

                    for (int i = 0; 1 < cortado.Length; i++)
                    {
                        linhasql = "insert into Tarefas_Sumarios (id_tarefa, id_sumario) values ( " + cortado[i] + ", (select id_sumario from Sumarios where ));";

                    }

                }


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

            String linhasql2 = "select * from sumarios where id_entidade =" + ddl_entidade.SelectedValue + ";";
            DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);

            rptItems2.DataSource = dt2;
            rptItems2.DataBind();
        }

    }
}