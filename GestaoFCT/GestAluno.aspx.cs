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
            String linhasql = "select * from Alunos;";
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

                String linhasql = "insert into alunos (nome_aluno, nif_aluno, morada_aluno, loc_aluno, email_aluno, cpostal_aluno, telefone_aluno, bi_aluno, valBi_aluno, pass_aluno, id_cargo) values('" + txt_nome.Value + "', '" + txt_nif.Value + "','" + txt_morada.Value + "', '" + txt_local.Value + "', '" + txt_email.Value + "' ,'" + txt_CodPost.Value + "', '" + txt_telefone.Value +  "', '" + txt_bi.Value + "', '" + txt_val.Value + "', '" + txt_pass.Value + "', 4);";

                Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
                exampleModalForm.Visible = false;
            }

            if (operacao.Text == "2")
            {
                //Response.Write("<script>alert('22222')</script>");

                String linhasql = "update alunos set nome_aluno = '" + txt_nome.Value + "', nif_aluno = '" + txt_nif.Value + "', email_aluno = '" + txt_email.Value + "', loc_aluno = '" + txt_local.Value + "', morada_aluno = '" + txt_morada.Value +  "', telefone_aluno = '" + txt_telefone.Value +  "', cpostal_aluno = '" + txt_CodPost.Value + "', bi_aluno = '" + txt_bi.Value + "', valBi_aluno = '" + txt_val.Value + "', pass_aluno = '" + txt_pass.Value + "' where id_aluno = " + labelCod.Text + ";";

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
                String linhasql = "insert into FichasFCT (id_aluno, id_curso, id_entidade, id_tutor, id_professor, num_horas, ano_fct) values('" + labelCod.Text + "', '" + ddl_curso.SelectedValue + "','" + ddl_entidade.SelectedValue + "', '" + ddl_tutor.SelectedValue + "', '" + ddl_professor.SelectedValue + "' ,'" + txt_numHora.Value + "', '" + txt_anoFCT.Value + "');";

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
    }
}