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
    public partial class Administradores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["cargo"].ToString() != "1")
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

            String linhasql = "SELECT * from Administradores;";
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
            txt_pass.Value = "";
            txt_email.Value = "";

        }

        protected void Atualizar()
        {

            string linhadesql = "select * from administradores where id_adm = " + labelCod.Text + ";";
            var sqlConn = new SqlConnection(ObjSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                txt_nome.Value = r["nome_adm"].ToString();
                txt_email.Value = r["email_adm"].ToString();
                hiddenPassword.Value = Encoding.UTF8.GetString(Convert.FromBase64String(r["pass_adm"].ToString()));

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

            operacao.Text = "1";
            reset();
            exampleModalFormTitle.InnerText = "Criar Administrador";
            btn_enviar.Text = "Criar Administrador";
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
                exampleModalFormTitle.InnerText = "Editar Administrador";
                btn_enviar.Text = "Editar Administrador";
                Alert.Visible = false;
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

            if (labelCod.Text != "0")
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


            if (operacao.Text == "1")
            {

                if (!erro)
                {

                    String linhasql = "insert into Administradores (nome_adm, email_adm, pass_adm, id_cargo) values('" + txt_nome.Value + "', '" + txt_email.Value + "', '" + Convert.ToBase64String(Encoding.ASCII.GetBytes(txt_pass.Value)) + "', 1 );";

                    Database.NonQuerySqlSrv(linhasql);
                    reset();
                    refresh();
                    exampleModalForm.Visible = false;
                    exampleModal.Visible = false;
                }


            }

            if (operacao.Text == "2")
            {

                if (!erro)
                {

                    String linhasql = "update Administradores set nome_adm = '" + txt_nome.Value + "', email_adm = '" + txt_email.Value + "', pass_adm = '" + Convert.ToBase64String(Encoding.ASCII.GetBytes(txt_pass.Value)) + "' where id_adm = " + labelCod.Text + ";";

                    Database.NonQuerySqlSrv(linhasql);
                    reset();
                    refresh();

                    exampleModalForm.Visible = false;
                    exampleModal.Visible = false;
                }


            }

            if (operacao.Text == "3")
            {

                String linhasql = "delete from Administradores where id_adm = " + labelCod.Text + ";";

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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            refresh();
        }
    }
}