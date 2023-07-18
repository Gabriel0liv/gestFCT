using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestaoFCT
{
    public partial class GestProf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["codigo"] == null) // Verifica se a sessão expirou
                Response.Redirect("~/Login.aspx"); // Redireciona para a página de login

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

            if (!Convert.ToBoolean(Session["direcao"]) && Session["cargo"].ToString() != "1")
            {
                NavObj.Visible = false;
                NavProf.Visible = false;
            }

            if ((Session["cargo"].ToString() == "1"))
            {
                divCurso.Visible = true;
                divDirecao.Visible = true;

                string workConn = ConfigurationManager.ConnectionStrings["FCTConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(workConn))
                {
                    SqlCommand cmd = new SqlCommand("select id_curso, nome_curso from cursos where ano_curso = 12;", con);
                    con.Open();
                    ddl_curso.DataTextField = "nome_curso";
                    ddl_curso.DataValueField = "id_curso";
                    ddl_curso.DataSource = cmd.ExecuteReader();
                    ddl_curso.DataBind();
                    con.Close();
                }
            }

            if (Session["cargo"].ToString() == "2")
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

            if ((Session["cargo"].ToString() != "1"))
                NavAdm.Visible = false;
            else
                NavAdm.Visible = true;


            }

        protected void refresh()
        {
            String linhasql = "select * from professores;";
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
            txt_telemovel.Value = "";
            txt_morada.Value = "";
            txt_local.Value = "";
            txt_CodPost.Value = "";
            txt_pass.Value = "";


        }

        protected void Atualizar()
        {

            string linhadesql = "select * from professores where id_prof = " + labelCod.Text + ";";
            var sqlConn = new SqlConnection(ProfSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                txt_nome.Value = r["nome_prof"].ToString();
                txt_nif.Value = r["nif_prof"].ToString();
                txt_email.Value = r["email_prof"].ToString();
                txt_telefone.Value = r["telefone_prof"].ToString();
                txt_telemovel.Value = r["telemovel_prof"].ToString();
                txt_morada.Value = r["morada_prof"].ToString();
                txt_local.Value = r["loc_prof"].ToString();
                txt_CodPost.Value = r["cpostal_prof"].ToString();
                hiddenPassword.Value = Encoding.UTF8.GetString(Convert.FromBase64String(r["pass_prof"].ToString()));

                //txt_pass.Value = Encoding.UTF8.GetString(Convert.FromBase64String(r["pass_prof"].ToString()));

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
            exampleModalFormTitle.InnerText = "Criar Professor";
            btn_enviar.Text = "Criar Professor";
            Alert.Visible = false;
            exampleModalForm.Visible = true;

        }

        protected void Editar(object sender, EventArgs e)
        {
            operacao.Text = "2";
            labelCod.Text = HiddenField1.Value;

            if (labelCod.Text != "0")
            {

                Atualizar();
                exampleModalFormTitle.InnerText = "Editar Professor";
                btn_enviar.Text = "Editar Professor";
                Alert.Visible = false;
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

            if (labelCod.Text != "0")
            {
                btnDeletar.Visible = true;
                string linhadesql = "select nome_prof from professores where id_prof = " + labelCod.Text + ";";
                var sqlConn = new SqlConnection(ProfSQLData.ConnectionString);
                var com = new SqlCommand(linhadesql, sqlConn);
                sqlConn.Open();
                SqlDataReader r = com.ExecuteReader();
                r.Read();
                textoCancelar.InnerText = "Deseja eliminar o registo \"" + r["nome_prof"] + "\"?";
                r.Close();
                sqlConn.Close();
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

            Boolean erro = false;

            if (operacao.Text != "3")
            {

                if (txt_nome.Value.Replace(" ", "") == "")
                {
                    erro = true;
                    alerMessage.InnerText = "O nome não pode conter caracteres vazios!";
                    Alert.Visible = true;
                }
                else if (GlobalFunctions.HasSqlInjection(txt_nome.Value))
                {
                    erro = true;
                    if (GlobalFunctions.SqlInjectionChecker(txt_nome.Value))
                    {
                        alerMessage.InnerHtml = "Nome inserido inválido. <br/> (Palavra reservada SQL encontrada).";
                        Alert.Visible = true;
                    }
                    else
                    {
                        alerMessage.InnerHtml = "Caracteres inválidos no nome. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
                        Alert.Visible = true;
                    }

                }
                else if (txt_nif.Value.Replace(" ", "") == "")
                {
                    erro = true;
                    alerMessage.InnerText = "O nif não pode conter caracteres vazios!";
                    Alert.Visible = true;
                }
                else if (GlobalFunctions.HasSqlInjection(txt_email.Value))
                {
                    erro = true;
                    if (GlobalFunctions.SqlInjectionChecker(txt_email.Value))
                    {
                        alerMessage.InnerHtml = "Email inserido inválido. <br/> (Palavra reservada SQL encontrada).";
                        Alert.Visible = true;
                    }
                    else //se não foi uma palavra reservada, então foi por algum caractere especial
                    {
                        alerMessage.InnerHtml = "Caracteres inválidos no email. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
                        Alert.Visible = true;
                    }
                }
                else if (txt_morada.Value.Replace(" ", "") == "")
                {
                    erro = true;
                    alerMessage.InnerText = "A morada não pode conter caracteres vazios!";
                    Alert.Visible = true;
                }
                else if (GlobalFunctions.HasSqlInjection(txt_morada.Value))
                {
                    erro = true;
                    if (GlobalFunctions.SqlInjectionChecker(txt_morada.Value))
                    {
                        alerMessage.InnerHtml = "Morada inserida inválido. <br/> (Palavra reservada SQL encontrada).";
                        Alert.Visible = true;
                    }
                    else
                    {
                        alerMessage.InnerHtml = "Caracteres inválidos na morada. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
                        Alert.Visible = true;
                    }
                }
                else if (txt_local.Value.Replace(" ", "") == "")
                {
                    erro = true;
                    alerMessage.InnerText = "A localidade não pode conter caracteres vazios!";
                    Alert.Visible = true;
                }
                else if (GlobalFunctions.HasSqlInjection(txt_local.Value))
                {
                    erro = true;
                    if (GlobalFunctions.SqlInjectionChecker(txt_local.Value))
                    {
                        alerMessage.InnerHtml = "Localidade inserida inválido. <br/> (Palavra reservada SQL encontrada).";
                        Alert.Visible = true;
                    }
                    else
                    {
                        alerMessage.InnerHtml = "Caracteres inválidos na localidade. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
                        Alert.Visible = true;
                    }
                }
                else if (GlobalFunctions.HasSqlInjection(txt_CodPost.Value))
                {
                    erro = true;
                    if (GlobalFunctions.SqlInjectionChecker(txt_CodPost.Value))
                    {
                        alerMessage.InnerHtml = "Código postal inserido inválido. <br/> (Palavra reservada SQL encontrada).";
                        Alert.Visible = true;
                    }
                    else
                    {
                        alerMessage.InnerHtml = "Caracteres inválidos no código postal. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
                        Alert.Visible = true;
                    }
                }
                else if (GlobalFunctions.HasSqlInjection(txt_pass.Value))
                {
                    erro = true;
                    if (GlobalFunctions.SqlInjectionChecker(txt_pass.Value))
                    {
                        alerMessage.InnerHtml = "Password inserida inválido. <br/> (Palavra reservada SQL encontrada).";
                        Alert.Visible = true;
                    }
                    else
                    {
                        alerMessage.InnerHtml = "Caracteres inválidos na password. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
                        Alert.Visible = true;
                    }
                }




            }

            if (txt_nome.Value.Replace(" ", "") == "")
            {
                erro = true;

            }

            if (operacao.Text == "1")
            {
                String linhasql = "";
                if (Session["cargo"].ToString() == "2")
                    linhasql = "insert into professores (nome_prof, nif_prof, morada_prof, loc_prof, email_prof, cpostal_prof, telefone_prof, telemovel_prof, pass_prof, id_cargo, id_curso, direcao) values('" + txt_nome.Value + "', '" + txt_nif.Value + "','" + txt_morada.Value + "', '" + txt_local.Value + "', '" + txt_email.Value + "' ,'" + txt_CodPost.Value + "', '" + txt_telefone.Value + "', '" + txt_telemovel.Value + "', '" + Convert.ToBase64String(Encoding.ASCII.GetBytes(txt_pass.Value)) + "', 2, " + Session["curso"].ToString() + ", 0);";
                else
                {
                    int d;
                    if (DC.Checked)
                        d = 1;
                    else
                        d = 0;

                    linhasql = "insert into professores (nome_prof, nif_prof, morada_prof, loc_prof, email_prof, cpostal_prof, telefone_prof, telemovel_prof, pass_prof, id_cargo, id_curso, direcao) values('" + txt_nome.Value + "', '" + txt_nif.Value + "','" + txt_morada.Value + "', '" + txt_local.Value + "', '" + txt_email.Value + "' ,'" + txt_CodPost.Value + "', '" + txt_telefone.Value + "', '" + txt_telemovel.Value + "', '" + Convert.ToBase64String(Encoding.ASCII.GetBytes(txt_pass.Value)) + "', 2, " + ddl_curso.SelectedValue + ", " + d + ");";
                }

                if (!erro)
                {
                    Database.NonQuerySqlSrv(linhasql);
                    reset();
                    refresh();
                    exampleModalForm.Visible = false;
                }
            }

            if (operacao.Text == "2")
            {
                String linhasql = "";
                if (Session["cargo"].ToString() == "2")
                    linhasql = "update professores set nome_prof = '" + txt_nome.Value + "', nif_prof = '" + txt_nif.Value + "', email_prof = '" + txt_email.Value + "', loc_prof = '" + txt_local.Value + "', morada_prof = '" + txt_morada.Value + "', telefone_prof = '" + txt_telefone.Value + "', cpostal_prof = '" + txt_CodPost.Value + "', telemovel_prof = '" + txt_telemovel.Value + "', pass_prof = '" + Convert.ToBase64String(Encoding.ASCII.GetBytes(txt_pass.Value)) + "' where id_prof = " + labelCod.Text + ";";
                else
                {
                    int d;
                    if (DC.Checked)
                        d = 1;
                    else
                        d = 0;

                    linhasql = "update professores set nome_prof = '" + txt_nome.Value + "', nif_prof = '" + txt_nif.Value + "', email_prof = '" + txt_email.Value + "', loc_prof = '" + txt_local.Value + "', morada_prof = '" + txt_morada.Value + "', telefone_prof = '" + txt_telefone.Value + "', cpostal_prof = '" + txt_CodPost.Value + "', telemovel_prof = '" + txt_telemovel.Value + "', pass_prof = '" + Convert.ToBase64String(Encoding.ASCII.GetBytes(txt_pass.Value)) + "', direcao = " + d + ", id_curso = " + ddl_curso.SelectedValue + " where id_prof = " + labelCod.Text + ";";
                }

                if (!erro)
                {
                    Database.NonQuerySqlSrv(linhasql);
                    reset();
                    refresh();
                    exampleModalForm.Visible = false;
                }

            }

            if (operacao.Text == "3")
            {

                String linhasql = "delete from professores where id_prof = " + labelCod.Text + ";";

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
                exampleModal.Visible = false;
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            exampleModal.Visible = false;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            refresh();
        }
    }
}