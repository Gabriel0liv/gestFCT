using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestaoFCT
{
    public partial class GestTutor : System.Web.UI.Page
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

            if (Session["cargo"].ToString() != "1")
                NavAdm.Visible = false;

            if (!Convert.ToBoolean(Session["direcao"]) && Session["cargo"].ToString() != "1")
                NavObj.Visible = false; NavProf.Visible = false;

        }

        protected void refresh()
        {
            String linhasql = "select * from tutores;";
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
            string workConn = ConfigurationManager.ConnectionStrings["FCTConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(workConn))
            {
                SqlCommand cmd = new SqlCommand("select id_entidade, nome_entidade from Entidades;", con);
                con.Open();
                ddl_entidade.DataTextField = "nome_entidade";
                ddl_entidade.DataValueField = "id_entidade";
                ddl_entidade.DataSource = cmd.ExecuteReader();
                ddl_entidade.DataBind();
                con.Close();
            }

            txt_nome.Value = "";
            txt_nif.Value = "";
            txt_email.Value = "";
            txt_tlf.Value = "";
            txt_morada.Value = "";
            txt_local.Value = "";
            txt_CodPost.Value = "";
            txt_tlm.Value = "";
            txt_pass.Value = "";


        }

        protected void Atualizar()
        {

            string linhadesql = "select * from tutores where id_tutor = " + labelCod.Text + ";";
            var sqlConn = new SqlConnection(TutSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                txt_nome.Value = r["nome_tutor"].ToString();
                txt_nif.Value = r["nif_tutor"].ToString();
                txt_email.Value = r["email_tutor"].ToString();
                txt_tlf.Value = r["telefone_tutor"].ToString();
                txt_morada.Value = r["morada_tutor"].ToString();
                txt_local.Value = r["loc_tutor"].ToString();
                txt_CodPost.Value = r["cpostal_tutor"].ToString();
                txt_tlm.Value = r["telemovel_tutor"].ToString();
                ddl_entidade.SelectedValue = r["id_entidade"].ToString();
                txt_pass.Value = Encoding.UTF8.GetString(Convert.FromBase64String(r["pass_tutor"].ToString()));

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
            exampleModalFormTitle.InnerText = "Criar Tutor";
            btn_enviar.Text = "Criar Tutor";
            exampleModalForm.Visible = true;


        }

        protected void Editar(object sender, EventArgs e)
        {
            operacao.Text = "2";
            labelCod.Text = HiddenField1.Value;

            if (labelCod.Text != "0")
            {
                string workConn = ConfigurationManager.ConnectionStrings["FCTConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(workConn))
                {
                    SqlCommand cmd = new SqlCommand("select id_entidade, nome_entidade from Entidades;", con);
                    con.Open();
                    ddl_entidade.DataTextField = "nome_entidade";
                    ddl_entidade.DataValueField = "id_entidade";
                    ddl_entidade.DataSource = cmd.ExecuteReader();
                    ddl_entidade.DataBind();
                    con.Close();
                }

                Atualizar();
                exampleModalFormTitle.InnerText = "Editar Tutor";
                btn_enviar.Text = "Editar Tutor";
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
                string linhadesql = "select nome_tutor from tutores where id_tutor = " + labelCod.Text + ";";
                var sqlConn = new SqlConnection(TutSQLData.ConnectionString);
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
                else if (txt_cargo.Value.Replace(" ", "") == "")
                {
                    erro = true;
                    alerMessage.InnerText = "O cargo não pode conter caracteres vazios!";
                    Alert.Visible = true;
                }
                else if (GlobalFunctions.HasSqlInjection(txt_cargo.Value))
                {
                    erro = true;
                    if (GlobalFunctions.SqlInjectionChecker(txt_cargo.Value))
                    {
                        alerMessage.InnerHtml = "Cargo inserido inválido. <br/> (Palavra reservada SQL encontrada).";
                        Alert.Visible = true;
                    }
                    else
                    {
                        alerMessage.InnerHtml = "Caracteres inválidos no cargo. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
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
                
                String linhasql = "insert into tutores (nome_tutor, nif_tutor, morada_tutor, loc_tutor, email_tutor, cpostal_tutor, telefone_tutor, telemovel_tutor, id_entidade, pass_tutor, id_cargo) values('" + txt_nome.Value + "', '" + txt_nif.Value + "','" + txt_morada.Value + "', '" + txt_local.Value + "', '" + txt_email.Value + "' ,'" + txt_CodPost.Value + "', '" + txt_tlf.Value + "', '" + txt_tlm.Value + "', " + ddl_entidade.SelectedValue + ", '" + Convert.ToBase64String(Encoding.ASCII.GetBytes(txt_pass.Value)) + "', 3);";

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

                String linhasql = "update tutores set nome_tutor = '" + txt_nome.Value + "', nif_tutor = '" + txt_nif.Value + "', email_tutor = '" + txt_email.Value + "', loc_tutor = '" + txt_local.Value + "', morada_tutor = '" + txt_morada.Value + "', telefone_tutor = '" + txt_tlf.Value + "', cpostal_tutor = '" + txt_CodPost.Value + "', telemovel_tutor = '" + txt_tlm.Value + "', id_entidade = '" + ddl_entidade.SelectedValue + "', pass_tutor = '" + Convert.ToBase64String(Encoding.ASCII.GetBytes(txt_pass.Value)) + "' where id_tutor = " + labelCod.Text + ";";

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
                //Response.Write("<script>alert('33333')</script>");

                String linhasql = "delete from Tutores where id_tutor = " + labelCod.Text + ";";

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