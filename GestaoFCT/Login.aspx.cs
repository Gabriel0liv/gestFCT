using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace GestaoFCT
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_login_Click(object sender, EventArgs e)
        {

            if (GlobalFunctions.HasSqlInjection(txt_email.Value) || GlobalFunctions.HasSqlInjection(txt_pass.Value))
            {
                //verifica se houve tentativa de SQL Injection no email
                if (GlobalFunctions.HasSqlInjection(txt_email.Value))
                {   // verifica se foi detectada uma palavra reservada do SQL
                    if (GlobalFunctions.SqlInjectionChecker(txt_email.Value))
                    {
                        AlertaTexto.InnerHtml = "Email inserido inválido. <br/> (Palavra reservada SQL encontrada).";
                        AlertaErro.Visible = true;
                    }
                    else //se não foi uma palavra reservada, então foi por algum caractere especial
                    {
                        AlertaTexto.InnerHtml = "Caracteres inválidos no email. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
                        AlertaErro.Visible = true;
                    }

                }
                else // Verifica que tentava de SQL Injection foi feita na senha
                {
                    if (GlobalFunctions.SqlInjectionChecker(txt_pass.Value))
                    { // verifica se foi detectada uma palavra reservada do SQL
                        AlertaTexto.InnerHtml = "Senha inserida inválida. <br/> (Palavra reservada SQL encontrada).";
                        AlertaErro.Visible = true;
                    }
                    else //se não foi uma palavra reservada, então foi por algum caractere especial
                    {
                        AlertaTexto.InnerHtml = "Caracteres inválidos na senha. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
                        AlertaErro.Visible = true;
                    }
                }

            }
            else
            {

                string Sql_login = "select * from tbl_login where email='" + txt_email.Value +
            "' and pass ='" + Convert.ToBase64String(Encoding.ASCII.GetBytes(txt_pass.Value)) + "';";

                DataTable dt = Database.GetFromDBSqlSrv(Sql_login);

                if (dt.Rows.Count == 1)
                {
                    //obtem o cargo e ID do utilizador
                    Session["cargo"] = dt.Rows[0]["cargo"];
                    Session["codigo"] = dt.Rows[0]["id"];

                    //Obter o primeiro e ultimo nome
                    string nomeCompleto = dt.Rows[0]["Nome"].ToString();
                    string[] abreviado = nomeCompleto.Split(' '); // divide a string em palavras utilizando o espaço como separador
                    string nome = abreviado[0]; // obtem a primeira palavra do array
                    string apelido = abreviado[abreviado.Length - 1]; // obtem a última palavra do array

                    string linhasql = "";

                    if (nome != apelido)
                        Session["Utilizador"] = nome + " " + apelido;
                    else
                        Session["Utilizador"] = nome;

                    // Armazena dados importantês de cada utilizador
                    if (Session["cargo"].ToString() == "4") // se for um aluno
                    {
                        Session["nome"] = dt.Rows[0]["nome"];
                        Session["email"] = dt.Rows[0]["email"];
                        Session["curso"] = dt.Rows[0]["curso"];

                        string linhadesql = "select Ca.descricao_cargo, C.nome_curso, C.ano_curso + C.turma_curso as turma from Alunos A inner join Cargos Ca ON Ca.id_cargo = A.id_cargo inner join Cursos C ON C.id_curso = A.id_curso where A.id_aluno = " + Session["codigo"].ToString() + ";";
                        var sqlConn = new SqlConnection(LogSQLData.ConnectionString);
                        var com = new SqlCommand(linhadesql, sqlConn);
                        sqlConn.Open();
                        SqlDataReader r = com.ExecuteReader();
                        while (r.Read())
                        {
                            Session["turma"] = r["turma"].ToString();
                            Session["nome_curso"] = r["nome_curso"].ToString();
                            Session["nome_cargo"] = r["descricao_cargo"].ToString();
                        }
                        r.Close();
                        sqlConn.Close();

                        Response.Redirect("~/Sumarios.aspx"); //redireciona para pagina de sumários
                    }
                    if (Session["cargo"].ToString() == "3") // se for um tutor
                    {
                        Session["nome"] = dt.Rows[0]["nome"];
                        Session["email"] = dt.Rows[0]["email"];
                        Session["entidade"] = dt.Rows[0]["entidade"];

                        string linhadesql = "select Ca.descricao_cargo, E.nome_entidade, T.cargo_tutor from Tutores T inner join Cargos Ca ON Ca.id_cargo = T.id_cargo inner join Entidades E ON E.id_entidade = T.id_entidade where T.id_tutor = " + Session["codigo"].ToString() + ";";  

                        var sqlConn = new SqlConnection(LogSQLData.ConnectionString);
                        var com = new SqlCommand(linhadesql, sqlConn);
                        sqlConn.Open();
                        SqlDataReader r = com.ExecuteReader();
                        while (r.Read())
                        {
                            Session["nome_cargo"] = r["descricao_cargo"].ToString();
                            Session["nome_entidade"] = r["nome_entidade"].ToString();
                            Session["cargo_tutor"] = r["cargo_tutor"].ToString();
                        }
                        r.Close();
                        sqlConn.Close();

                        Response.Redirect("~/Tarefas.aspx"); // redireciona para pagina de tarefas
                    }
                    if (Session["cargo"].ToString() == "2") //Se for um professor
                    {

                        Session["nome"] = dt.Rows[0]["nome"];
                        Session["email"] = dt.Rows[0]["email"];
                        Session["curso"] = dt.Rows[0]["curso"];
                        Session["direcao"] = dt.Rows[0]["direcao"];

                        string linhadesql = "select Ca.descricao_cargo, C.nome_curso from Professores P inner join Cargos Ca ON Ca.id_cargo = P.id_cargo inner join Cursos C ON C.id_curso = P.id_curso where P.id_prof = " + Session["codigo"].ToString() + ";";
                        var sqlConn = new SqlConnection(LogSQLData.ConnectionString);
                        var com = new SqlCommand(linhadesql, sqlConn);
                        sqlConn.Open();
                        SqlDataReader r = com.ExecuteReader();
                        while (r.Read())
                        {
                            Session["nome_curso"] = r["nome_curso"].ToString();
                            Session["nome_cargo"] = r["descricao_cargo"].ToString();
                        }
                        r.Close();
                        sqlConn.Close();

                        Response.Redirect("~/GestAluno.aspx"); // redireciona para pagina de alunos
                    }
                    if (Session["cargo"].ToString() == "1") // se for um administrador
                    {
                        Session["nome"] = dt.Rows[0]["nome"];
                        Session["email"] = dt.Rows[0]["email"];

                        string linhadesql = "select Ca.descricao_cargo from Administradores A inner join Cargos Ca ON Ca.id_cargo = A.id_cargo where A.id_adm = " + Session["codigo"].ToString() + ";";
                        var sqlConn = new SqlConnection(LogSQLData.ConnectionString);
                        var com = new SqlCommand(linhadesql, sqlConn);
                        sqlConn.Open();
                        SqlDataReader r = com.ExecuteReader();
                        while (r.Read())
                        {
                            Session["nome_cargo"] = r["descricao_cargo"].ToString();
                        }
                        r.Close();
                        sqlConn.Close();

                        Response.Redirect("~/GestFCT.aspx"); //redireciona para a pagina de administradores
                    }
                }
                else
                {
                    AlertaTexto.InnerHtml = "Utilizador não encontrado! <br/> Email ou senha estão errados. ";
                    AlertaErro.Visible = true;
                }

            }




        }
    }

}