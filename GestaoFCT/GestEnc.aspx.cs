using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestaoFCT
{
    public partial class GestEnc : System.Web.UI.Page
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
            String linhasql = "select * from EncarregadosEducacao;";
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
            txt_bi.Value = "";
            txt_val.Value = "";
            txt_morada.Value = "";
            txt_local.Value = "";
            txt_CodPost.Value = "";



        }

        protected void Atualizar()
        {

            string linhadesql = "select * from EncarregadosEducacao where id_ee = " + labelCod.Text + ";";
            var sqlConn = new SqlConnection(EncSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                txt_nome.Value = r["nome_ee"].ToString();
                txt_nif.Value = r["nif_ee"].ToString();
                txt_email.Value = r["email_ee"].ToString();
                txt_telefone.Value = r["telefone_ee"].ToString();
                txt_telemovel.Value = r["telemovel_ee"].ToString();
                txt_bi.Value = r["bi_ee"].ToString();
                txt_val.Value = r["valBi_ee"].ToString();
                txt_morada.Value = r["morada_ee"].ToString();
                txt_local.Value = r["loc_ee"].ToString();
                txt_CodPost.Value = r["cpostal_ee"].ToString();


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
            exampleModalFormTitle.InnerText = "Criar Encarregado";
            btn_enviar.Text = "Criar Encarregado";
            exampleModalForm.Visible = true;

        }

        protected void Editar(object sender, EventArgs e)
        {
            operacao.Text = "2";
            labelCod.Text = HiddenField1.Value;

            if(labelCod.Text != "0")
            {

                Atualizar();
                exampleModalFormTitle.InnerText = "Editar Encarregado";
                btn_enviar.Text = "Editar Encarregado";
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
                string linhadesql = "select nome_ee from EncarregadosEducacao where id_ee = " + labelCod.Text + ";";
                var sqlConn = new SqlConnection(EncSQLData.ConnectionString);
                var com = new SqlCommand(linhadesql, sqlConn);
                sqlConn.Open();
                SqlDataReader r = com.ExecuteReader();
                r.Read();
                textoCancelar.InnerText = "Deseja eliminar o registo \"" + r["nome_ee"] + "\"?";
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

            if(operacao.Text != "3")
            {
                if (txt_nome.Value.Replace(" ", "") == "")
                {
                    erro = true;
                    alerMessage.InnerText = "O nome não pode conter caracteres vazios!";
                }
                else if (GlobalFunctions.HasSqlInjection(txt_nome.Value))
                {
                    erro = true;
                    if (GlobalFunctions.SqlInjectionChecker(txt_nome.Value))
                    {
                        alerMessage.InnerHtml = "Nome inserido inválido. <br/> (Palavra reservada SQL encontrada).";
                        Alert.Visible = true;
                    }
                    else //se não foi uma palavra reservada, então foi por algum caractere especial
                    {
                        alerMessage.InnerHtml = "Caracteres inválidos no nome. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
                        Alert.Visible = true;
                    }
                }
                else if (txt_nif.Value.Length < 9 || txt_nif.Value.Length > 9)
                {
                    erro = true;
                    if (txt_nif.Value.Length < 9)
                    {
                        alerMessage.InnerText = "O nif não pode conter menos de 9 algoritmos!";
                        Alert.Visible = true;
                    }
                    else
                    {
                        alerMessage.InnerText = "O nif não pode conter mais de 9 algoritmos!";
                        Alert.Visible = true;
                    }

                }
                else if (GlobalFunctions.HasSqlInjection(txt_nif.Value))
                {
                    erro = true;
                    if (GlobalFunctions.SqlInjectionChecker(txt_nif.Value))
                    {
                        alerMessage.InnerHtml = "NIF inserido inválido. <br/> (Palavra reservada SQL encontrada).";
                        Alert.Visible = true;
                    }
                    else
                    {
                        alerMessage.InnerHtml = "Caracteres inválidos no NIF. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
                        Alert.Visible = true;
                    }
                }
                else if (txt_bi.Value.Replace(" ", "") == "")
                {
                    erro = true;
                    alerMessage.InnerText = "O B.I. não pode conter caracteres vazios!";
                    Alert.Visible = true;
                }
                else if (txt_bi.Value.Length < 8 || txt_bi.Value.Length > 9)
                {
                    erro = true;
                    if (txt_bi.Value.Length < 8)
                    {
                        alerMessage.InnerText = "O B.I. não pode conter menos de 8 caracteres!";
                        Alert.Visible = true;
                    }
                    else
                    {
                        alerMessage.InnerText = "O B.I. não pode conter mais de 9 caracteres!";
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
                else if (txt_telemovel.Value.Replace(" ", "") == "")
                {
                    erro = true;
                    alerMessage.InnerText = "O telemóvel não pode conter caracteres vazios!";
                    Alert.Visible = true;
                }
                else if (txt_telemovel.Value.Length < 9 || txt_telemovel.Value.Length > 9)
                {
                    erro = true;
                    if (txt_telemovel.Value.Length < 9)
                    {
                        alerMessage.InnerText = "O telemóvel não pode conter menos de 9 algoritmos!";
                        Alert.Visible = true;
                    }
                    else
                    {
                        alerMessage.InnerText = "O telemóvel não pode conter mais de 9 algoritmos!";
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

            }



            if (operacao.Text == "1")
            {

                String linhasql = "insert into EncarregadosEducacao (nome_ee, nif_ee, morada_ee, loc_ee, email_ee, cpostal_ee, telefone_ee, telemovel_ee, bi_ee, valBi_ee) values('" + txt_nome.Value + "', '" + txt_nif.Value + "','" + txt_morada.Value + "', '" + txt_local.Value + "', '" + txt_email.Value + "' ,'" + txt_CodPost.Value + "', '" + txt_telefone.Value +  "', '" + txt_telemovel.Value + "', '" + txt_bi.Value + "', '" + txt_val.Value + "');";

                if (!erro)
                {
                    Database.NonQuerySqlSrv(linhasql);
                    reset();
                    refresh();
                    exampleModalForm.Visible = false;
                }
                else { exampleModalForm.Visible = true; }
            }

            if (operacao.Text == "2")
            {

                String linhasql = "update EncarregadosEducacao set nome_ee = '" + txt_nome.Value + "', nif_ee = '" + txt_nif.Value + "', email_ee = '" + txt_email.Value + "', loc_ee = '" + txt_local.Value + "', morada_ee = '" + txt_morada.Value +  "', telefone_ee = '" + txt_telefone.Value + "', telemovel_ee = '" + txt_telemovel.Value + "', cpostal_ee = '" + txt_CodPost.Value + "', bi_ee = '" + txt_bi.Value + "', valBi_ee = '" + txt_val.Value + "' where id_ee = " + labelCod.Text + ";";

                if (!erro)
                {
                    Database.NonQuerySqlSrv(linhasql);
                    reset();
                    refresh();
                    exampleModalForm.Visible = false;

                }
                //else { exampleModalForm.Visible = true; }

            }

            if (operacao.Text == "3")
            {
                //Response.Write("<script>alert('33333')</script>");

                String linhasql = "delete from EncarregadosEducacao where id_ee = " + labelCod.Text + ";";
                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                if (!erro)
                {
                    Database.NonQuerySqlSrv(linhasql);
                    reset();
                    refresh();
                    exampleModal.Visible = false;
                }
                //else { exampleModalForm.Visible = true; }
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