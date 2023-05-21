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

            if (rptItems.Items.Count == 0)
            {
                refresh();
            }



        }

        protected void refresh()
        {
            String linhasql = "select * from Alunos_info;";
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
            txt_anoFCT.Value = "";
            txt_dataFim.Text = "";
            txt_dataInicio.Text = "";
            txt_numMaxHoras.Text = "";
            txt_numHora.Value = "";

            using (SqlConnection sqlConn = new SqlConnection(AlnSQLData.ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("select id_curso, nome_curso, ano_curso, turma_curso, nome_curso + ' ' + ano_curso + turma_curso as curso_info from cursos;", sqlConn);
                sqlConn.Open();
                ddl_curso.DataTextField = "curso_info";
                ddl_curso.DataValueField = "id_curso";
                ddl_curso.DataSource = cmd.ExecuteReader();
                ddl_curso.DataBind();
                sqlConn.Close();

                SqlCommand cmd2 = new SqlCommand("select id_ee, nome_ee from EncarregadosEducacao;", sqlConn);
                sqlConn.Open();
                ddl_Encarregado.DataTextField = "nome_ee";
                ddl_Encarregado.DataValueField = "id_ee";
                ddl_Encarregado.DataSource = cmd2.ExecuteReader();
                ddl_Encarregado.DataBind();
                sqlConn.Close();

            }

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
                ddl_Encarregado.SelectedValue = r["id_ee"].ToString(); //ERRO AQUI
                ddl_curso.SelectedValue = r["id_curso"].ToString();
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
            formAluno.Visible = true;
            formFCT.Visible = false;
            exampleModalForm.Visible = true;

        }

        protected void Editar(object sender, EventArgs e)
        {
            operacao.Text = "2";
            labelCod.Text = HiddenField1.Value;
            reset();
            if(labelCod.Text != "0")
            {

                Atualizar();
                exampleModalFormTitle.InnerText = "Editar Aluno";
                btn_enviar.Text = "Editar Aluno";
                formAluno.Visible = true;
                formFCT.Visible = false;
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
                btnDeletar.Visible = false;
            }


            exampleModal.Visible = true;


        }

        protected void FCT(object sender, EventArgs e)
        {
            operacao.Text = "4";
            labelCod.Text = HiddenField1.Value;
            int mes;

            if (labelCod.Text != "0")
            {
                exampleModalFormTitle.InnerText = "Gerar registo da FCT";
                btn_enviar.Text = "Gerar FCT";

                using (SqlConnection sqlConn = new SqlConnection(AlnSQLData.ConnectionString))
                {
                    string query = "SELECT nome_aluno FROM Alunos WHERE id_aluno = " + labelCod.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        txt_aluno.Text = dt.Rows[0]["nome_aluno"].ToString();
                    }

                    SqlCommand cmd = new SqlCommand("select id_curso, nome_curso from cursos;", sqlConn);
                    sqlConn.Open();
                    ddl_curso.DataTextField = "nome_curso";
                    ddl_curso.DataValueField = "id_curso";
                    ddl_curso.DataSource = cmd.ExecuteReader();
                    ddl_curso.DataBind();
                    sqlConn.Close();

                    SqlCommand cmd2 = new SqlCommand("select id_prof, nome_prof from professores;", sqlConn);
                    sqlConn.Open();
                    ddl_professor.DataTextField = "nome_prof";
                    ddl_professor.DataValueField = "id_prof";
                    ddl_professor.DataSource = cmd2.ExecuteReader();
                    ddl_professor.DataBind();
                    sqlConn.Close();

                    SqlCommand cmd3 = new SqlCommand("select id_entidade, nome_entidade from Entidades;", sqlConn);
                    sqlConn.Open();
                    ddl_entidade.DataTextField = "nome_entidade";
                    ddl_entidade.DataValueField = "id_entidade";
                    ddl_entidade.DataSource = cmd3.ExecuteReader();
                    ddl_entidade.DataBind();
                    sqlConn.Close();

                    SqlCommand cmd4 = new SqlCommand("select id_tutor, nome_tutor from tutores;", sqlConn);
                    sqlConn.Open();
                    ddl_tutor.DataTextField = "nome_tutor";
                    ddl_tutor.DataValueField = "id_tutor";
                    ddl_tutor.DataSource = cmd4.ExecuteReader();
                    ddl_tutor.DataBind();
                    sqlConn.Close();

                }

                // ano da FCT
                mes = DateTime.Now.Month;
                if (mes >= 9) { txt_anoFCT.Value = (DateTime.Now.Year).ToString() + "/" + (DateTime.Now.Year + 1).ToString(); }
                else if (mes < 9) { txt_anoFCT.Value = (DateTime.Now.Year -1).ToString() + "/" + DateTime.Now.Year.ToString(); }

                formAluno.Visible = false;
                formFCT.Visible = true;
                exampleModalForm.Visible = true;
            }
            else
            {
                textoCancelar.InnerText = "Nenhum registo foi selecionado!";
                btnDeletar.Visible = false;
                exampleModal.Visible = true;
            }

        }
        
        protected void Comandos(object sender, EventArgs e)
        {


            if (operacao.Text == "1")
            {
                //Response.Write("<script>alert('11111')</script>");

                String linhasql = "insert into alunos (nome_aluno, nif_aluno, morada_aluno, loc_aluno, email_aluno, cpostal_aluno, telefone_aluno, bi_aluno, valBi_aluno, pass_aluno, id_ee, id_curso, id_cargo) values('" + txt_nome.Value + "', '" + txt_nif.Value + "','" + txt_morada.Value + "', '" + txt_local.Value + "', '" + txt_email.Value + "' ,'" + txt_CodPost.Value + "', '" + txt_telefone.Value +  "', '" + txt_bi.Value + "', '" + txt_val.Value + "', '" + txt_pass.Value + "', '" + ddl_Encarregado.SelectedValue + "', '" + ddl_curso.SelectedValue + "', 4);";

                Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
                exampleModalForm.Visible = false;
            }

            if (operacao.Text == "2")
            {
                //Response.Write("<script>alert('22222')</script>");

                String linhasql = "update alunos set nome_aluno = '" + txt_nome.Value + "', nif_aluno = '" + txt_nif.Value + "', email_aluno = '" + txt_email.Value + "', loc_aluno = '" + txt_local.Value + "', morada_aluno = '" + txt_morada.Value +  "', telefone_aluno = '" + txt_telefone.Value +  "', cpostal_aluno = '" + txt_CodPost.Value + "', bi_aluno = '" + txt_bi.Value + "', valBi_aluno = '" + txt_val.Value + "', pass_aluno = '" + txt_pass.Value + "', id_ee = " + ddl_Encarregado.SelectedValue + ", id_curso = " + ddl_curso.SelectedValue + "where id_aluno = " + labelCod.Text + ";";

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

            if(operacao.Text == "4")
            {
                String linhasql = "insert into FichasFCT (id_aluno, id_entidade, id_tutor, id_professor, num_horas, ano_fct, HorasDiarias, inicio_fct, fim_fct) values('" + labelCod.Text + "', '" + ddl_entidade.SelectedValue + "', '" + ddl_tutor.SelectedValue + "', '" + ddl_professor.SelectedValue + "' ,'" + txt_numHora.Value + "', '" + txt_anoFCT.Value + "', '" + txt_numMaxHoras.Text + "', '" + txt_dataInicio.Text + "', '" + txt_dataFim.Text + "');";

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
                exampleModalForm.Visible = false;

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

            using (SqlConnection sqlConn = new SqlConnection(AlnSQLData.ConnectionString))
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
