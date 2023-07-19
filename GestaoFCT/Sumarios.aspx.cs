using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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

            if (Session["codigo"] == null) // Verifica se a sessão expirou
                Response.Redirect("~/Login.aspx"); // Redireciona para a página de login

            int[] DadosAtual = new int[3];

            if (Session["cargo"].ToString() != "1" && Session["cargo"].ToString() != "2" && Session["cargo"].ToString() != "3" && Session["cargo"].ToString() != "4")
            {
                //Redirect to login page.
                Response.Redirect("~/Login.aspx");
            }
            else
            {

                if (rptItems2.Items.Count == 0)
                {
                    refresh();
                }

                NomeUser.InnerText = Session["Utilizador"].ToString();

                if (Session["cargo"].ToString() != "4")
                    slc_aluno.Visible = true;



                //se for aluno
                if (Session["cargo"].ToString() == "4")
                {

                    ddl_Status.Enabled = false;
                    ddl_entidade.Visible = false;

                    //oculta opções de navegação
                    NavDoc.Visible = false;
                    NavAln.Visible = false;
                    NavCurso.Visible = false;
                    NavEE.Visible = false;
                    NavEnt.Visible = false;
                    NavFCT.Visible = false;
                    NavProf.Visible = false;
                    SecGest.Visible = false;
                    NavTar.Visible = false;
                    NavTut.Visible = false;
                    NavObj.Visible = false;
                    NavAdm.Visible = false;

                    slc_aluno.Visible = false;
                    ddl_entidade.Visible = false;
                    ddl_aluno.Visible = false;

                }
                // se for tutor ou professor
                if (Session["cargo"].ToString() == "3" || Session["cargo"].ToString() == "2" || Session["cargo"].ToString() == "1")
                {
                    ddl_Status.Enabled = true;

                    if (Session["cargo"].ToString() == "3") //tutor
                    {
                        //oculta filtro
                        ddl_entidade.Visible = false;
                        //oculta opções de navegação
                        NavDoc.Visible = false;
                        NavAln.Visible = false;
                        NavCurso.Visible = false;
                        NavEE.Visible = false;
                        NavEnt.Visible = false;
                        NavFCT.Visible = false;
                        NavProf.Visible = false;
                        SecGest.Visible = false;
                        NavTut.Visible = false;
                        NavObj.Visible = false;


                    }

                    if (Session["cargo"].ToString() != "1")
                        NavAdm.Visible = false;

                    if (!Convert.ToBoolean(Session["direcao"]) && Session["cargo"].ToString() != "1")
                    {
                        NavObj.Visible = false;
                        NavProf.Visible = false;
                    }


                }

                if (Session["cargo"].ToString() == "4")
                {
                    String linhasql = "select SUM(CAST(horas_sumario as INT)) as horasFeitas, (select num_horas from FichasFCT where id_aluno = " + Session["codigo"].ToString() + ") as horasTotais from Sumarios_table where id_aluno = " + Session["codigo"].ToString() + ";";
                    var sqlConn = new SqlConnection(SumSQLData.ConnectionString);
                    var com = new SqlCommand(linhasql, sqlConn);
                    sqlConn.Open();
                    SqlDataReader r = com.ExecuteReader();
                    while (r.Read())
                    {
                        Label1.Text = "Horas feitas: " + r["horasFeitas"].ToString() + "/" + r["horasTotais"];
                    }
                    r.Close();
                    sqlConn.Close();
                    Label1.Visible = true;

                    DivAluno.Visible = false;
                    slc_aluno.Visible = false;
                    ddl_entidade.Visible = false;
                    ddl_aluno.Visible = false;
                }

                if (Session["cargo"].ToString() == "4")
                {
                    inf_cargo.InnerText = "Cargo: " + Session["nome_cargo"].ToString();
                    inf_curso.InnerText = "Curso: " + Session["nome_curso"].ToString();
                    inf_turma.InnerText = "Turma: " + Session["turma"].ToString();
                    Div_infDirecao.Visible = false;
                    Div_infEnt.Visible = false;
                    Div_infCT.Visible = false;
                    slc_aluno.Visible = false;
                    ddl_entidade.Visible = false;
                    ddl_aluno.Visible = false;
                }
                else if (Session["cargo"].ToString() == "3")
                {
                    inf_cargo.InnerText = "Cargo: " + Session["nome_cargo"].ToString();
                    Div_infEnt.InnerText = Session["nome_entidade"].ToString();
                    Div_infCT.InnerText = Session["cargo_tutor"].ToString();
                    Div_infCurso.Visible = false;
                    Div_infTurma.Visible = false;
                    inf_turma.Visible = false;
                    Div_infDirecao.Visible = false;



                }
                else if (Session["cargo"].ToString() == "2")
                {
                    inf_cargo.InnerText = "Cargo: " + Session["nome_cargo"].ToString();
                    inf_curso.InnerText = "Curso: " + Session["nome_curso"].ToString();

                    if (Convert.ToBoolean(Session["direcao"]))
                        Div_infDirecao.Visible = true;
                    else
                        Div_infDirecao.Visible = false;

                    Div_infTurma.Visible = false;
                    Div_infEnt.Visible = false;
                    Div_infCT.Visible = false;
                }
                else
                {
                    inf_cargo.InnerText = "Cargo: " + Session["nome_cargo"].ToString();
                    Div_infCT.Visible = false;
                    Div_infCurso.Visible = false;
                    Div_infDirecao.Visible = false;
                    Div_infEnt.Visible = false;
                    Div_infTurma.Visible = false;
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

                if (Session["cargo"].ToString() == "2" || Session["cargo"].ToString() == "1")
                {
                    slc_aluno.Visible = true;
                    using (SqlConnection sqlConn = new SqlConnection(SumSQLData.ConnectionString))
                    {
                        SqlCommand cmd = null;
                        if (Session["cargo"].ToString() == "2")
                            cmd = new SqlCommand("select distinct id_aluno, nome_aluno from sumarios_table where id_professor = " + Session["codigo"] + ";", sqlConn);
                        else
                            cmd = new SqlCommand("select distinct id_aluno, nome_aluno from sumarios_table;", sqlConn);

                        sqlConn.Open();
                        slc_aluno.DataTextField = "nome_aluno";
                        slc_aluno.DataValueField = "id_aluno";
                        slc_aluno.DataSource = cmd.ExecuteReader();
                        slc_aluno.DataBind();
                        sqlConn.Close();
                    }

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


                    using (SqlConnection sqlConn = new SqlConnection(SumSQLData.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("select distinct id_aluno, nome_aluno from Sumarios_table where id_entidade = " + ddl_entidade.Items[0].Value + ";", sqlConn);
                        sqlConn.Open();
                        ddl_aluno.DataTextField = "nome_aluno";
                        ddl_aluno.DataValueField = "id_aluno";
                        ddl_aluno.DataSource = cmd.ExecuteReader();
                        ddl_aluno.DataBind();
                        sqlConn.Close();
                    }
                    ddl_aluno.Visible = true;

                    using (SqlConnection sqlConn = new SqlConnection(SumSQLData.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("select distinct id_aluno, nome_aluno from sumarios_table where id_professor = " + Session["codigo"] + ";", sqlConn);
                        sqlConn.Open();
                        slc_aluno.DataTextField = "nome_aluno";
                        slc_aluno.DataValueField = "id_aluno";
                        slc_aluno.DataSource = cmd.ExecuteReader();
                        slc_aluno.DataBind();
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

                    using (SqlConnection sqlConn = new SqlConnection(SumSQLData.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("select distinct id_aluno, nome_aluno from sumarios_table;", sqlConn);

                        sqlConn.Open();
                        slc_aluno.DataTextField = "nome_aluno";
                        slc_aluno.DataValueField = "id_aluno";
                        slc_aluno.DataSource = cmd.ExecuteReader();
                        slc_aluno.DataBind();
                        sqlConn.Close();
                    }
                }

                if (Session["cargo"].ToString() == "3")
                {
                    using (SqlConnection sqlConn = new SqlConnection(SumSQLData.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("select distinct id_aluno, nome_aluno from Sumarios_table where id_entidade = " + Session["entidade"].ToString() + "and id_tutor = " + Session["codigo"].ToString() + ";", sqlConn);
                        sqlConn.Open();
                        ddl_aluno.DataTextField = "nome_aluno";
                        ddl_aluno.DataValueField = "id_aluno";
                        ddl_aluno.DataSource = cmd.ExecuteReader();
                        ddl_aluno.DataBind();
                        sqlConn.Close();
                    }
                    ddl_aluno.Visible = true;


                    using (SqlConnection sqlConn = new SqlConnection(SumSQLData.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("select distinct id_aluno, nome_aluno from sumarios_table where id_tutor = " + Session["codigo"] + ";", sqlConn);

                        sqlConn.Open();
                        slc_aluno.DataTextField = "nome_aluno";
                        slc_aluno.DataValueField = "id_aluno";
                        slc_aluno.DataSource = cmd.ExecuteReader();
                        slc_aluno.DataBind();
                        sqlConn.Close();
                    }
                }


            }


        }

        protected void refresh()
        {
            if (Session["cargo"].ToString() == "1")
            {
                String linhasql2 = "select * from Sumarios_table ORDER BY CONVERT(date, data_sumario, 103) desc;";
                DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);

                rptItems2.DataSource = dt2;
                rptItems2.DataBind();
            }
            if (Session["cargo"].ToString() == "2")
            {
                String linhasql2 = "select * from Sumarios_table where id_professor = " + Session["codigo"].ToString() + " ORDER BY CONVERT(date, data_sumario, 103) desc;";
                DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);

                rptItems2.DataSource = dt2;
                rptItems2.DataBind();
            }
            if (Session["cargo"].ToString() == "3") //se for tutor
            {
                String linhasql2 = "select * from Sumarios_table where id_tutor =" + Session["codigo"].ToString() + " ORDER BY CONVERT(date, data_sumario, 103) desc;";
                DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);

                rptItems2.DataSource = dt2;
                rptItems2.DataBind();
            }
            if (Session["cargo"].ToString() == "4")
            {
                String linhasql2 = "select * from Sumarios_table where id_aluno = " + Session["codigo"].ToString() + " ORDER BY CONVERT(date, data_sumario, 103) desc;";
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
            txt_dataSum.Text = "";
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
                    SqlCommand cmd = null;

                    if (Session["cargo"].ToString() == "2" || Session["cargo"].ToString() == "1")
                        cmd = new SqlCommand("select id_tarefa, descricao_tarefa from Tarefas where id_entidade = (select id_entidade from FichasFCT where id_aluno = " + slc_aluno.SelectedValue + ");", sqlConn);
                    else if (Session["cargo"].ToString() == "3")
                        cmd = new SqlCommand("select id_tarefa, descricao_tarefa from Tarefas where id_entidade =" + Session["entidade"].ToString() + ";", sqlConn);

                    sqlConn.Open();
                    ddl_Tarefas.DataTextField = "descricao_tarefa";
                    ddl_Tarefas.DataValueField = "id_tarefa";
                    ddl_Tarefas.DataSource = cmd.ExecuteReader();
                    ddl_Tarefas.DataBind();
                    sqlConn.Close();
                }

                using (SqlConnection sqlConn = new SqlConnection(SumSQLData.ConnectionString))
                {
                    SqlCommand cmd = null;
                    if (Session["cargo"].ToString() == "2")
                        cmd = new SqlCommand("select distinct id_aluno, nome_aluno from sumarios_table where id_professor = " + Session["codigo"] + ";", sqlConn);
                    if (Session["cargo"].ToString() == "3")
                        cmd = new SqlCommand("select distinct id_aluno, nome_aluno from sumarios_table where id_tutor = " + Session["codigo"] + ";", sqlConn);
                    else
                        cmd = new SqlCommand("select distinct id_aluno, nome_aluno from sumarios_table;", sqlConn);

                    sqlConn.Open();
                    slc_aluno.DataTextField = "nome_aluno";
                    slc_aluno.DataValueField = "id_aluno";
                    slc_aluno.DataSource = cmd.ExecuteReader();
                    slc_aluno.DataBind();
                    sqlConn.Close();
                }

            }


        }

        protected void Atualizar()
        {

            string linhadesql = "select * from Sumarios_table where id_sumario = " + labelCod.Text + ";";
            var sqlConn = new SqlConnection(SumSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                txt_sumario.Text = r["descricao_sumario"].ToString().Replace(" \\n ", "\r\n");
                txt_numHora.Value = r["horas_sumario"].ToString();
                ddl_Status.SelectedValue = r["status_sumario"].ToString();
                slc_aluno.SelectedValue = r["id_aluno"].ToString();
                // Converter o Texto da data do sumário 
                DateTime data;
                if (DateTime.TryParseExact(r["data_sumario"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data))
                {
                    txt_dataSum.Text = data.ToString("yyyy-MM-dd");
                }


            }
            r.Close();
            sqlConn.Close();

            ObterTarefas();


        }

        protected void ObterTarefas()
        {
            string tarefas = "";
            String linhadesql = "";

            if (operacao.Text == "3" || operacao.Text == "2")
                linhadesql = "select id_tarefa from Tarefas_Sumarios where id_sumario =" + labelCod.Text + ";";
            else
                linhadesql = "select id_tarefa from Tarefas_Sumarios where id_sumario = (select id_sumario from sumarios where descricao_sumario = '" + txt_sumario.Text + "' and data_sumario = '" + txt_dataSum.Text + "');";


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

            if (Session["cargo"].ToString() != "4")
            {
                ddl_aluno.Visible = true;
                ddl_entidade.Visible = true;
            }
        }

        protected void Fechar(object sender, EventArgs e)
        {
            formSum.Visible = false;
            if (Session["cargo"].ToString() != "4")
            {
                ddl_aluno.Visible = true;
                ddl_entidade.Visible = true;
            }

            reset();
        }

        protected void Criar(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('aaaaa')</script>");
            operacao.Text = "1";
            TextBox2.Text = "";
            reset();
            exampleModalFormTitle.InnerText = "Criar Sumário";
            btn_enviar.Text = "Criar Sumário";
            btn_enviar.Enabled = false;
            formSum.Visible = true;
            ddl_entidade.Visible = false;
            ddl_aluno.Visible = false;
            Alert.Visible = false;
        }


        protected void Editar(object sender, EventArgs e)
        {
            operacao.Text = "2";
            TextBox2.Text = "";
            labelCod.Text = HiddenField1.Value;

            if (labelCod.Text != "0")
            {
                reset();
                Atualizar();
                exampleModalFormTitle.InnerText = "Editar Sumário";
                btn_enviar.Text = "Editar Sumário";
                btn_enviar.Enabled = false;
                formSum.Visible = true;
                btn_enviar.Enabled = true;
                ddl_entidade.Visible = false;
                ddl_aluno.Visible = false;
            }
            else
            {
                textoCancelar.InnerText = "Nenhum registo foi selecionado!";
                btnDeletar.Visible = false;
                ddl_entidade.Visible = false;
                ddl_aluno.Visible = false;
                exampleModal.Visible = true;

            }

            Alert.Visible = false;
        }


        protected void Eliminar(object sender, EventArgs e)
        {

            operacao.Text = "3";
            labelCod.Text = HiddenField1.Value;

            if (labelCod.Text != "0")
            {
                reset();
                ddl_entidade.Visible = false;
                ddl_aluno.Visible = false;
                btnDeletar.Visible = true;
                textoCancelar.InnerText = "Tem certeza que deseja eliminar o Sumário?";
            }
            else
            {
                textoCancelar.InnerText = "Nenhum registo foi selecionado!";
                ddl_entidade.Visible = false;
                ddl_aluno.Visible = false;
                btnDeletar.Visible = false;
            }


            exampleModal.Visible = true;


        }

        protected void Comandos(object sender, EventArgs e)
        {

            Boolean erro = false;

            if (txt_dataSum.Text == "")
            {
                erro = true;
                alerMessage.InnerText = "Selecione a data do sumário";
                Alert.Visible = true;
            }


            if (operacao.Text == "1")
            {


                // Converter o Texto da data do sumário 
                DateTime data;
                if (DateTime.TryParseExact(txt_dataSum.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out data))
                {
                    txt_dataSum.Text = data.ToString("dd/MM/yyyy");
                }



                string[] array = TextBox1.Text.Split(',');
                int[] result = null;

                if (array[0] != "")
                {
                    result = new int[array.Length];
                    for (int i = 0; i < array.Length; i++)
                    {
                        result[i] = int.Parse(array[i]);
                    }
                }

                if (array[0] == "")
                {
                    Response.Write("<script>alert('Não há Tarefas guardadas')</script>");

                }
                else
                {
                    if (!erro)
                    {

                        String linhasql = "";
                        if (Session["cargo"].ToString() == "4")
                            linhasql = "insert into Sumarios (descricao_sumario, horas_sumario, status_sumario, data_sumario, id_fct) values('" + txt_sumario.Text.Replace("\r\n", " \\n ") + "', '" + txt_numHora.Value + "','" + ddl_Status.SelectedValue + "', '" + txt_dataSum.Text + "', (select id_fct from FichasFCT where id_aluno = " + Session["codigo"].ToString() + ") );";
                        else
                            linhasql = "insert into Sumarios (descricao_sumario, horas_sumario, status_sumario, data_sumario, id_fct) values('" + txt_sumario.Text.Replace("\r\n", " \\n ") + "', '" + txt_numHora.Value + "','" + ddl_Status.SelectedValue + "', '" + txt_dataSum.Text + "', (select id_fct from FichasFCT where id_aluno = " + slc_aluno.SelectedValue + ") );";


                        Database.NonQuerySqlSrv(linhasql);

                        for (int i = 0; i < array.Length; i++)
                        {
                            result[i] = int.Parse(array[i]);
                            linhasql = "insert into Tarefas_Sumarios (id_tarefa, id_sumario) values(" + result[i] + ", ( select id_sumario from sumarios where descricao_sumario = '" + txt_sumario.Text.Replace("\r\n", " \\n ") + "' and data_sumario = '" + txt_dataSum.Text + "'));";
                            Database.NonQuerySqlSrv(linhasql);

                        }

                        reset();
                        refresh();

                        formSum.Visible = false;
                        exampleModal.Visible = false;
                        if (Session["cargo"].ToString() != "4")
                        {
                            ddl_aluno.Visible = true;
                            ddl_entidade.Visible = true;
                        }

                    }


                    //formSum.Visible = false;
                }
            }

            if (operacao.Text == "2")
            {
                if (!erro)
                {
                    // Converter o Texto da data do sumário 
                    DateTime data;
                    if (DateTime.TryParseExact(txt_dataSum.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out data))
                    {
                        txt_dataSum.Text = data.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        // A string fornecida não está no formato esperado
                        // Faça o tratamento adequado, como mostrar uma mensagem de erro
                    }

                    string[] newTar = TextBox1.Text.Split(',');
                    string[] idTar = oldTar.Text.Split(',');


                    if (newTar[0] == "")
                    {
                        Response.Write("<script>alert('Não há Tarefas guardadas')</script>");

                    }
                    else
                    {
                        String linhasql = "";
                        if (Session["cargo"].ToString() == "4")
                            linhasql = "update Sumarios set descricao_sumario = '" + txt_sumario.Text.Replace("\r\n", " \\n ") + "', horas_sumario = '" + txt_numHora.Value + "', status_sumario = '" + ddl_Status.SelectedValue + "', data_sumario = '" + txt_dataSum.Text + "' where id_sumario = " + labelCod.Text + ";";
                        else
                            linhasql = "update Sumarios set descricao_sumario = '" + txt_sumario.Text.Replace("\r\n", " \\n ") + "', horas_sumario = '" + txt_numHora.Value + "', status_sumario = '" + ddl_Status.SelectedValue + "', data_sumario = '" + txt_dataSum.Text + "', id_fct = (select id_fct from FichasFCT where id_aluno = " + slc_aluno.SelectedValue + ")  where id_sumario = " + labelCod.Text + ";";


                        //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");
                        Database.NonQuerySqlSrv(linhasql);

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


                    }

                    reset();
                    refresh();

                    formSum.Visible = false;
                    exampleModal.Visible = false;
                    if (Session["cargo"].ToString() != "4")
                    {
                        ddl_aluno.Visible = true;
                        ddl_entidade.Visible = true;
                    }
                }

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

                reset();
                refresh();

                formSum.Visible = false;
                exampleModal.Visible = false;
                if (Session["cargo"].ToString() != "4")
                {
                    ddl_aluno.Visible = true;
                    ddl_entidade.Visible = true;
                }

            }





        }



        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Session["cargo"].ToString() != "4")
            {
                ddl_aluno.Visible = true;
                ddl_entidade.Visible = true;
            }
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
                SqlCommand cmd = new SqlCommand("select distinct id_aluno, nome_aluno from Sumarios_table where id_entidade = " + ddl_entidade.SelectedValue + ";", sqlConn);
                sqlConn.Open();
                ddl_aluno.DataTextField = "nome_aluno";
                ddl_aluno.DataValueField = "id_aluno";
                ddl_aluno.DataSource = cmd.ExecuteReader();
                ddl_aluno.DataBind();                sqlConn.Close();
            }
            ddl_aluno.Visible = true;

            String linhasql = "select SUM(CAST(horas_sumario as INT)) as horasFeitas, (select num_horas from FichasFCT where id_aluno = " + ddl_aluno.SelectedValue + ") as horasTotais from Sumarios_table where id_aluno = " + ddl_aluno.SelectedValue + ";";
            var sqlConn2 = new SqlConnection(SumSQLData.ConnectionString);
            var com = new SqlCommand(linhasql, sqlConn2);
            sqlConn2.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                Label1.Text = "Horas feitas: " + r["horasFeitas"].ToString() + "/" + r["horasTotais"];
            }
            r.Close();
            sqlConn2.Close();

        }

        protected void ddl_aluno_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (Session["cargo"].ToString() == "2" || Session["cargo"].ToString() == "1")
            {
                String linhasql2 = "select * from sumarios_table where id_entidade =" + ddl_entidade.SelectedValue + " and id_aluno = " + ddl_aluno.SelectedValue + ";";
                DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);

                rptItems2.DataSource = dt2;
                rptItems2.DataBind();

                String linhasql = "select SUM(CAST(horas_sumario as INT)) as horasFeitas, (select num_horas from FichasFCT where id_aluno = " + ddl_aluno.SelectedValue + ") as horasTotais from Sumarios_table where id_aluno = " + ddl_aluno.SelectedValue + ";";
                var sqlConn = new SqlConnection(SumSQLData.ConnectionString);
                var com = new SqlCommand(linhasql, sqlConn);
                sqlConn.Open();
                SqlDataReader r = com.ExecuteReader();
                while (r.Read())
                {
                    Label1.Text = "Horas feitas: " + r["horasFeitas"].ToString() + "/" + r["horasTotais"];
                }
                r.Close();
                sqlConn.Close();
                Label1.Visible = true;
            }

            if (Session["cargo"].ToString() == "3")
            {
                String linhasql2 = "select * from sumarios_table where id_entidade =" + Session["entidade"].ToString() + " and id_aluno = " + ddl_aluno.SelectedValue + ";";
                DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);

                rptItems2.DataSource = dt2;
                rptItems2.DataBind();
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            refresh();
            if (Session["cargo"].ToString() != "4")
                Label1.Visible = false;
        }



        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (TextBox2.Text != "")
                btn_enviar.Enabled = true;
        }
    }
}