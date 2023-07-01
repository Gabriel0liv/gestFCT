using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Globalization;

namespace GestaoFCT
{
    public partial class GestFCT : System.Web.UI.Page
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
            String linhasql = "select * from tabelas_FCT;";
            DataTable dt = Database.GetFromDBSqlSrv(linhasql);

            rptItems.DataSource = dt;
            rptItems.DataBind();
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx"); // redireciona para a página de login
        }



        protected void Atualizar()
        {

            string linhadesql = "select * from tabelas_fct where id_fct = " + labelCod.Text + ";";
            var sqlConn = new SqlConnection(FCTSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                txt_aluno.Text = r["nome_aluno"].ToString();
                ddl_professor.SelectedValue = r["id_professor"].ToString();
                ddl_entidade.SelectedValue = r["id_entidade"].ToString();
                ddl_entidade.SelectedValue = r["id_entidade"].ToString();
                ddl_tutor.SelectedValue = r["id_tutor"].ToString();
                txt_anoFCT.Value = r["ano_fct"].ToString();
                txt_numHora.Value = r["num_horas"].ToString();
                txt_numMaxHoras.Text = r["horasDiarias"].ToString();
                // Converter o Texto da data do sumário 
                DateTime data;
                if (DateTime.TryParseExact(r["inicio_fct"].ToString(), "dd/MM/yyyy" , CultureInfo.InvariantCulture, DateTimeStyles.None, out data) && DateTime.TryParseExact(r["fim_fct"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data))
                {
                    txt_dataInicio.Text = data.ToString("yyyy-MM-dd");
                    txt_dataFim.Text = data.ToString("yyyy-MM-dd");
                }
                else
                {
                    // A string fornecida não está no formato esperado
                    // Faça o tratamento adequado, como mostrar uma mensagem de erro
                }

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

        }



        protected void Editar(object sender, EventArgs e)
        {
            operacao.Text = "2";
            labelCod.Text = HiddenField1.Value;

            if(labelCod.Text != "0")
            {
                string workConn = ConfigurationManager.ConnectionStrings["FCTConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(workConn))
                {
                    string query = "SELECT nome_aluno FROM tabelas_FCT WHERE id_fct = " + labelCod.Text + ";";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        txt_aluno.Text = dt.Rows[0]["nome_aluno"].ToString();
                    }

                    SqlCommand cmd2 = new SqlCommand("select id_prof, nome_prof from professores;", con);
                    con.Open();
                    ddl_professor.DataTextField = "nome_prof";
                    ddl_professor.DataValueField = "id_prof";
                    ddl_professor.DataSource = cmd2.ExecuteReader();
                    ddl_professor.DataBind();
                    con.Close();

                    SqlCommand cmd3 = new SqlCommand("select id_entidade, nome_entidade from Entidades;", con);
                    con.Open();
                    ddl_entidade.DataTextField = "nome_entidade";
                    ddl_entidade.DataValueField = "id_entidade";
                    ddl_entidade.DataSource = cmd3.ExecuteReader();
                    ddl_entidade.DataBind();
                    con.Close();

                    SqlCommand cmd4 = new SqlCommand("select id_tutor, nome_tutor from tutores;", con);
                    con.Open();
                    ddl_tutor.DataTextField = "nome_tutor";
                    ddl_tutor.DataValueField = "id_tutor";
                    ddl_tutor.DataSource = cmd4.ExecuteReader();
                    ddl_tutor.DataBind();
                    con.Close();

                }

                Atualizar();
                exampleModalFormTitle.InnerText = "Editar FCT";
                btn_enviar.Text = "Editar FCT";
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
                string linhadesql = "select nome_tutor from tutores where id_tutor = " + labelCod.Text + ";";
                var sqlConn = new SqlConnection(FCTSQLData.ConnectionString);
                var com = new SqlCommand(linhadesql, sqlConn);
                sqlConn.Open();
                SqlDataReader r = com.ExecuteReader();
                r.Read();
                textoCancelar.InnerText = "Deseja eliminar o registo \"" + r["nome_tutor"] + "\"?";
                r.Close();
                sqlConn.Close();
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



            if (operacao.Text == "2")
            {
                //Response.Write("<script>alert('22222')</script>");


                DateTime data;
                if (DateTime.TryParseExact(txt_dataInicio.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out data) && DateTime.TryParseExact(txt_dataFim.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out data))
                {
                    txt_dataInicio.Text = data.ToString("dd/MM/yyyy");
                    txt_dataFim.Text = data.ToString("dd/MM/yyyy");
                }
                else
                {
                    // A string fornecida não está no formato esperado
                    // Faça o tratamento adequado, como mostrar uma mensagem de erro
                }


                String linhasql = "update tabelas_FCT set id_tutor = '" + ddl_tutor.SelectedValue + "', id_professor = '" + ddl_professor.SelectedValue + "', id_entidade = '" + ddl_entidade.SelectedValue + "', ano_fct = '" + txt_anoFCT.Value + "', num_horas = '" + txt_numHora.Value + "', fim_fct = '" + txt_dataFim.Text + "', horasDiarias = '" + txt_numMaxHoras.Text + "', inicio_fct = '" + txt_dataInicio.Text + "' where id_fct = " + labelCod.Text + ";";

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");
                //Response.Write("<script>alert('aaaaa')</script>");

                Database.NonQuerySqlSrv(linhasql);
                refresh();
                
            }

            if (operacao.Text == "3")
            {
                //Response.Write("<script>alert('33333')</script>");
                String linhasql = "delete from fichasFCT where id_fct = " + labelCod.Text + ";";
                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                Database.NonQuerySqlSrv(linhasql);
                refresh();
            }

            exampleModalForm.Visible = false;
            exampleModal.Visible = false;
        }
        
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            exampleModal.Visible = false;
        }

        protected void ddl_entidade_SelectedIndexChanged(object sender, EventArgs e)
        {

            using (SqlConnection sqlConn = new SqlConnection(FCTSQLData.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("select id_tutor, nome_tutor from tutores where id_entidade = " + ddl_entidade.SelectedValue + ";", sqlConn);
                sqlConn.Open();
                ddl_tutor.DataTextField = "nome_tutor";
                ddl_tutor.DataValueField = "id_tutor";
                ddl_tutor.DataSource = cmd.ExecuteReader();
                ddl_tutor.DataBind();
                sqlConn.Close();
            }

        }

        protected void txt_dataInicio_TextChanged(object sender, EventArgs e)
        {

            DateTime dataInicio = DateTime.Parse(txt_dataInicio.Text);
            int horasFormacao = int.Parse(txt_numHora.Value);
            int maxHorasDia = int.Parse(txt_numMaxHoras.Text);


            DateTime dataTermino = CalcularDataTermino(dataInicio, horasFormacao, maxHorasDia);

            // Calcular a diferença em dias entre a data de término e a data atual
            //int diasRestantes = (dataTermino - DateTime.Today).Days;
            int diasRestantes = CalcularDiasUteis(DateTime.Today, dataTermino);

            txt_dataFim.Text = dataTermino.ToShortDateString();
            // Exibir os dias restantes na TextBox
            //txtDiasRestantes.Text = diasRestantes.ToString();

        }

        protected int CalcularDiasUteis(DateTime dataInicio, DateTime dataFim)
        {
            int diasUteis = 0;

            DateTime dataAtual = dataInicio;

            while (dataAtual <= dataFim)
            {
                if (dataAtual.DayOfWeek != DayOfWeek.Saturday && dataAtual.DayOfWeek != DayOfWeek.Sunday && !EhFeriadoNacional(dataAtual))
                {
                    diasUteis++;
                }

                dataAtual = dataAtual.AddDays(1);
            }

            return diasUteis;
        }

        protected DateTime CalcularDataTermino(DateTime dataInicio, int horasFormacao, int horasPorDia)
        {
            DateTime dataAtual = dataInicio;
            int horasRestantes = horasFormacao;

            while (horasRestantes > 0)
            {
                // Verificar se o dia atual é útil (não é sábado, domingo ou feriado)
                if (dataAtual.DayOfWeek != DayOfWeek.Saturday && dataAtual.DayOfWeek != DayOfWeek.Sunday && !EhFeriadoNacional(dataAtual))
                {
                    int horasDia = Math.Min(horasPorDia, horasRestantes);

                    // Adicionar as horas do dia atual
                    dataAtual = dataAtual.AddHours(horasDia);
                    horasRestantes -= horasDia;


                }

                if (horasRestantes != 0)
                    dataAtual = dataAtual.AddDays(1); // Passar para o próximo dia
            }

            return dataAtual;
        }

        bool EhFeriadoNacional(DateTime data)
        {
            int dia = data.Day;
            int mes = data.Month;
            int ano = data.Year;

            // Feriados fixos
            if (dia == 1 && mes == 1) // Ano Novo
                return true;
            if (dia == 25 && mes == 4) // Dia da Liberdade
                return true;
            if (dia == 1 && mes == 5) // Dia do Trabalhador
                return true;
            if (dia == 10 && mes == 6) // Dia de Portugal
                return true;
            if (dia == 5 && mes == 10) // Implantação da República
                return true;
            if (dia == 1 && mes == 11) // Dia de Todos os Santos
                return true;
            if (dia == 1 && mes == 12) // Dia da Restauração da Independência
                return true;
            if (dia == 8 && mes == 12) // Dia da Imaculada Conceição
                return true;
            if (dia == 25 && mes == 12) // Natal
                return true;

            // Feriados móveis
            DateTime pascoa = CalcularDataPascoa(ano);
            DateTime sextaFeiraSanta = pascoa.AddDays(-2);
            DateTime segundaFeiraPascoa = pascoa.AddDays(1);
            DateTime corpoDeus = pascoa.AddDays(60);

            if (data.Date == sextaFeiraSanta.Date)
                return true;
            if (data.Date == segundaFeiraPascoa.Date)
                return true;
            if (data.Date == corpoDeus.Date)
                return true;

            return false;
        }

        DateTime CalcularDataPascoa(int ano)
        {
            int a = ano % 19;
            int b = ano / 100;
            int c = ano % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int mes = (h + l - 7 * m + 114) / 31;
            int dia = ((h + l - 7 * m + 114) % 31) + 1;

            return new DateTime(ano, mes, dia);
        }


    }
}