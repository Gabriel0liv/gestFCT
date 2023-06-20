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
        string Id_TarSum;
        protected void Page_Load(object sender, EventArgs e)
        {
            int[] DadosAtual = new int[3];

            if (Session["cargo"].ToString() != "1" && Session["cargo"].ToString() != "2" && Session["cargo"].ToString() != "3" && Session["cargo"].ToString() != "4")
            {
                //Redirect to login page.
                Response.Redirect("~/Login.aspx");
            }
            else
            {
               
                NomeUser.InnerText = Session["Utilizador"].ToString();


                //se for aluno
                if (Session["cargo"].ToString() == "4")
                { 

                    ddl_Status.Enabled = false;
                    ddl_entidade.Visible = false;

                    //oculta opções de navegação
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
                // se for tutor ou professor
                if (Session["cargo"].ToString() == "3" || Session["cargo"].ToString() == "2" || Session["cargo"].ToString() == "1")
                {
                    ddl_Status.Enabled = true;

                    if(Session["cargo"].ToString() == "3")
                    {
                        //oculta filtro
                        ddl_entidade.Visible = false;
                        //oculta opções de navegação
                        NavAln.Visible = false;
                        NavCurso.Visible = false;
                        NavEE.Visible = false;
                        NavEnt.Visible = false;
                        NavFCT.Visible = false;
                        NavProf.Visible = false;
                        SecGest.Visible = false;
                        NavTut.Visible = false;
                    }

                }



            }

            if (IsPostBack)
            {

                if (TextBox1.Text != "")
                {
                    //vai manter a multi-select com os itens selecionados ao recarregar a página
                    TarefasSelecionadas();
                    
                }

            }

           if (!IsPostBack)
            {
                if (rptItems2.Items.Count == 0)
                {
                    refresh();
                }

                if (Session["cargo"].ToString() == "2")//se for professor
                {
                    using (SqlConnection sqlConn = new SqlConnection(SumSQLData.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("select distinct id_entidade, nome_entidade from sumarios_table where id_professor = " + Session["codigo"] + ";", sqlConn);
                        sqlConn.Open();
                        ddl_entidade.DataTextField = "nome_entidade";
                        ddl_entidade.DataValueField = "id_entidade";
                        ddl_entidade.DataSource = cmd.ExecuteReader();
                        ddl_entidade.DataBind();
                        sqlConn.Close();
                    }
                }
                if (Session["cargo"].ToString() == "1")//se for administrador
                {
                    using (SqlConnection sqlConn = new SqlConnection(SumSQLData.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("select distinct id_entidade, nome_entidade from sumarios_table ;", sqlConn);
                        sqlConn.Open();
                        ddl_entidade.DataTextField = "nome_entidade";
                        ddl_entidade.DataValueField = "id_entidade";
                        ddl_entidade.DataSource = cmd.ExecuteReader();
                        ddl_entidade.DataBind();
                        sqlConn.Close();
                    }
                }




            }


        }

        protected void refresh()
        {
            if (Session["cargo"].ToString() == "1")
            {
                String linhasql2 = "select * from Sumarios_table;";
                DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);

                rptItems2.DataSource = dt2;
                rptItems2.DataBind();
            }
            if (Session["cargo"].ToString() == "2")
            {
                String linhasql2 = "select * from Sumarios_table where id_professor = " + Session["codigo"].ToString() + ";";
                DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);

                rptItems2.DataSource = dt2;
                rptItems2.DataBind();
            }
            if (Session["cargo"].ToString() == "3") //se for tutor
            {
                String linhasql2 = "select * from Sumarios_table where id_tutor =" + Session["codigo"].ToString() + ";";
                DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);

                rptItems2.DataSource = dt2;
                rptItems2.DataBind();
            }
            if (Session["cargo"].ToString() == "4")
            {
                String linhasql2 = "select * from Sumarios_table where id_aluno = " + Session["codigo"].ToString() + "order by id_sumario asc, data_sumario asc, horas_sumario asc, nome_aluno asc, status_sumario asc, descricao_sumario asc, id_fct asc;";
                DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);

                rptItems2.DataSource = dt2;
                rptItems2.DataBind();
            }
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
            txt_numHora.Value = "";
            TextBox1.Text = "";


            if (Session["cargo"].ToString() == "4")
            {
                using (SqlConnection sqlConn = new SqlConnection(SumSQLData.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("select id_tarefa, descricao_tarefa from Tarefas where id_entidade = (select id_entidade from FichasFCT where id_aluno = " + Session["codigo"].ToString() + ");", sqlConn);
                    sqlConn.Open();
                    ddl_Tarefas.DataTextField = "descricao_tarefa";
                    ddl_Tarefas.DataValueField = "id_tarefa";
                    ddl_Tarefas.DataSource = cmd.ExecuteReader();
                    ddl_Tarefas.DataBind();
                    sqlConn.Close();
                }
            }
            else
            {
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
                txt_sumario.Text = r["descricao_sumario"].ToString();
                txt_numHora.Value = r["horas_sumario"].ToString();
                ddl_Status.SelectedValue = r["status_sumario"].ToString();
                txt_dataSum.Value = r["data_sumario"].ToString();
            }
            r.Close();
            sqlConn.Close();

            ObterTarefas();
          

        }

        protected void ObterTarefas()
        {
            string tarefas = "";
            String linhadesql = "";

            if(operacao.Text == "3" || operacao.Text == "2")
                linhadesql = "select id_tarefa from Tarefas_Sumarios where id_sumario =" + labelCod.Text + ";";
            else
                linhadesql = "select id_tarefa from Tarefas_Sumarios where id_sumario = (select id_sumario from sumarios where descricao_sumario = '" + txt_sumario.Text + "' and data_sumario = '" + txt_dataSum.Value + "');";


            var sqlConn = new SqlConnection(SumSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();

                // Obter as tarefas
   
                while (r.Read())
                {
                    string tar = r["id_tarefa"].ToString();
                    tarefas += tar + ',';
                }


                // Remover a última vírgula, se houver
                if (!string.IsNullOrEmpty(tarefas))
                {
                    tarefas = tarefas.TrimEnd(',');
                }

                // Atribuir os valores à propriedade Text da TextBox
                TextBox1.Text = tarefas;


                // selecionar na dropdown as tarefas
                TarefasSelecionadas();

            
            r.Close();
            sqlConn.Close();

            linhadesql = "select id_TS from Tarefas_Sumarios where id_sumario = " + labelCod.Text + ";";
            com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            r = com.ExecuteReader();

            while (r.Read())
            {
                string dados = r["id_TS"].ToString();
                Id_TarSum += dados + ',';
            }
            r.Close();
            sqlConn.Close();

            if (!string.IsNullOrEmpty(Id_TarSum))
            {
                Id_TarSum = Id_TarSum.TrimEnd(',');
            }

            oldTar.Text = Id_TarSum;
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
                reset();
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
                reset();
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

        protected void Comandos(object sender, EventArgs e)
        {


            if (operacao.Text == "1")
            {

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                if (Session["cargo"].ToString() == "4")
                {



                    string[] array = TextBox1.Text.Split(',');
                    int[] result = new int[array.Length];

                    for (int i = 0; i < array.Length; i++)
                    {
                        result[i] = int.Parse(array[i]);
                    }

                    if (result[0] == 0)
                    {
                        Response.Write("<script>alert('Não há Tarefas guardadas')</script>");

                    }
                    else
                    {
                        String linhasql = "insert into Sumarios (descricao_sumario, horas_sumario, status_sumario, data_sumario, id_fct) values('" + txt_sumario.Text + "', '" + txt_numHora.Value + "','" + ddl_Status.SelectedValue + "', '" + txt_dataSum.Value + "', (select id_fct from FichasFCT where id_aluno = " + Session["codigo"].ToString() + ") );";

                        Database.NonQuerySqlSrv(linhasql);

                        for (int i = 0; i < array.Length; i++)
                        {
                            result[i] = int.Parse(array[i]);
                            linhasql = "insert into Tarefas_Sumarios (id_tarefa, id_sumario) values(" + result[i] + ", ( select id_sumario from sumarios where descricao_sumario = '" + txt_sumario.Text + "' and data_sumario = '" + txt_dataSum.Value + "'));";
                            Database.NonQuerySqlSrv(linhasql);

                        }
                    }


                }

                formSum.Visible = true;
                //formSum.Visible = false;
            }

            if (operacao.Text == "2")
            {
                String linhasql = "update Sumarios set descricao_sumario = '" + txt_sumario.Text + "', horas_sumario = '" + txt_numHora.Value + "', status_sumario = '" + ddl_Status.SelectedValue + "', data_sumario = '" + txt_dataSum.Value + "' where id_sumario = " + labelCod.Text + ";";

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");
                Database.NonQuerySqlSrv(linhasql);

                string[] newTar = TextBox1.Text.Split(',');
                string[] idTar = oldTar.Text.Split(',');

                    for (int i = 0; i < idTar.Length; i++)
                    {
                        linhasql = "delete from Tarefas_Sumarios where id_TS = " + idTar[i] + ";";
                        Database.NonQuerySqlSrv(linhasql);

                    }
                    for (int j = 0; j < newTar.Length; j++)
                    {
                        linhasql = "insert into Tarefas_Sumarios (id_tarefa, id_sumario) values(" + newTar[j] + "," + labelCod.Text + ");";
                        Database.NonQuerySqlSrv(linhasql);
                    }

                //formSum.Visible = true;
                formSum.Visible = false;
            }

            if (operacao.Text == "3")
            {
                //Response.Write("<script>alert('33333')</script>");

                String linhasql = "";


                ObterTarefas();
                string[] array = oldTar.Text.Split(',');
                int[] result = new int[array.Length];

                for (int i = 0; i < array.Length; i++)
                {
                    result[i] = int.Parse(array[i]);
                    linhasql = "delete from Tarefas_Sumarios where id_TS = " + result[i] + ";";
                    Database.NonQuerySqlSrv(linhasql);

                }

                linhasql = "delete from Sumarios where id_sumario = " + labelCod.Text + ";";
                Database.NonQuerySqlSrv(linhasql);
            }


            reset();
            refresh();

            formSum.Visible = false;
            exampleModal.Visible = false;

        }


        
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            exampleModal.Visible = false;
        }

        protected void TarefasSelecionadas()
        {
            string[] array = TextBox1.Text.Split(',');
            int[] result = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = int.Parse(array[i]);
            }

            // SELECIONAR ITENS DO DROPDOWN
            if (result.Length == 1)
            {
                ClientScript.RegisterStartupScript(
                      this.GetType(),
                      "SlcTar1",
                      "SlcTar1(" + result[0] + ")",
                      true);
            }

            if (result.Length == 2)
            {
                ClientScript.RegisterStartupScript(
                      this.GetType(),
                      "SlcTar2",
                      "SlcTar2(" + result[0] + "," + result[1] + ")",
                      true);
            }

            if (result.Length == 3)
            {
                ClientScript.RegisterStartupScript(
                      this.GetType(),
                      "SlcTar3",
                      "SlcTar3(" + result[0] + "," + result[1] + "," + result[2] + ")",
                      true);
            }
        }

        protected void ddl_entidade_SelectedIndexChanged1(object sender, EventArgs e)
        {

            String linhasql2 = "select * from sumarios_table where id_entidade =" + ddl_entidade.SelectedValue + ";";
            DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);

            rptItems2.DataSource = dt2;
            rptItems2.DataBind();

            using (SqlConnection sqlConn = new SqlConnection(SumSQLData.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select id_aluno, nome_aluno from Sumarios_table where id_entidade = " + ddl_entidade.SelectedValue + ";", sqlConn);
                sqlConn.Open();
                ddl_aluno.DataTextField = "nome_aluno";
                ddl_aluno.DataValueField = "id_aluno";
                ddl_aluno.DataSource = cmd.ExecuteReader();
                ddl_aluno.DataBind();
                sqlConn.Close();
            }
            ddl_aluno.Visible = true;
        }

        protected void ddl_aluno_SelectedIndexChanged1(object sender, EventArgs e)
        {
            String linhasql2 = "select * from sumarios where id_entidade =" + ddl_entidade.SelectedValue + " and id_aluno = " + ddl_aluno.SelectedValue + ";";
            DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);

            rptItems2.DataSource = dt2;
            rptItems2.DataBind();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            refresh();
        }


    }
}