using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestaoFCT
{
    public partial class GestEmp : System.Web.UI.Page
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
            String linhasql = "select * from entidades;";
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
            txt_morada.Value = "";
            txt_local.Value = "";
            txt_CodPost.Value = "";
            txt_NatJuri.Value = "";
            txt_resp.Value = "";
            txt_tlmResp.Value = "";
            txt_cargo.Value = "";
            txt_atvPrinc.Value = "";

        }

        protected void Atualizar()
        {

            string linhadesql = "select * from Entidades where id_entidade = " + labelCod.Text + ";";
            var sqlConn = new SqlConnection(EntSQLData.ConnectionString);
            var com = new SqlCommand(linhadesql, sqlConn);
            sqlConn.Open();
            SqlDataReader r = com.ExecuteReader();
            while (r.Read())
            {
                txt_nome.Value = "" + r["nome_entidade"];
                txt_nif.Value = "" + r["nif_entidade"];
                txt_email.Value = "" + r["email_entidade"];
                txt_telefone.Value = "" + r["telefone_entidade"];
                txt_morada.Value = "" + r["morada_entidade"];
                txt_local.Value = "" + r["loc_entidade"];
                txt_CodPost.Value = "" + r["cpostal_entidade"];
                txt_NatJuri.Value = "" + r["natjuridica"];
                txt_resp.Value = "" + r["resp_entidade"];
                txt_tlmResp.Value = "" + r["tlmResp_entidade"];
                txt_cargo.Value = "" + r["cargo_resp"];
                txt_atvPrinc.Value = "" + r["atv_principal"];

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

            exampleModalFormTitle.InnerText = "Criar Entidade";
            btn_enviar.Text = "Criar Entidade";
            exampleModalForm.Visible = true;


        }

        protected void Editar(object sender, EventArgs e)
        {
            operacao.Text = "2";
            labelCod.Text = HiddenField1.Value;
            Atualizar();
            exampleModalFormTitle.InnerText = "Editar Entidade";
            btn_enviar.Text = "Editar Entidade";
            exampleModalForm.Visible = true;

        }

        protected void Eliminar(object sender, EventArgs e)
        {

            operacao.Text = "3";
            labelCod.Text = HiddenField1.Value;

            if(labelCod.Text != "0")
            {
                string linhadesql = "select nome_entidade from Entidades where id_entidade = " + labelCod.Text + ";";
                var sqlConn = new SqlConnection(EntSQLData.ConnectionString);
                var com = new SqlCommand(linhadesql, sqlConn);
                sqlConn.Open();
                SqlDataReader r = com.ExecuteReader();
                r.Read();
                textoCancelar.InnerText = "Deseja eliminar o registo \"" + r["nome_entidade"] + "\"?";
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
                else if (txt_telefone.Value.Replace(" ", "") == "")
                {
                    erro = true;
                    alerMessage.InnerText = "O telefone não pode conter caracteres vazios!";
                    Alert.Visible = true;
                }
                else if (txt_NatJuri.Value.Replace(" ", "") == "")
                {
                    erro = true;
                    alerMessage.InnerText = "A Natureza juridica não pode conter caracteres vazios!";
                    Alert.Visible = true;
                }
                else if (GlobalFunctions.HasSqlInjection(txt_NatJuri.Value))
                {
                    erro = true;
                    if (GlobalFunctions.SqlInjectionChecker(txt_NatJuri.Value))
                    {
                        alerMessage.InnerHtml = "Natureza juridica inserida inválida. <br/> (Palavra reservada SQL encontrada).";
                        Alert.Visible = true;
                    }
                    else
                    {
                        alerMessage.InnerHtml = "Caracteres inválidos na natureza juridica. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
                        Alert.Visible = true;
                    }
                }
                else if (txt_resp.Value.Replace(" ", "") == "")
                {
                    erro = true;
                    alerMessage.InnerText = "Responsável não pode conter caracteres vazios!";
                    Alert.Visible = true;
                }
                else if (GlobalFunctions.HasSqlInjection(txt_resp.Value))
                {
                    erro = true;
                    if (GlobalFunctions.SqlInjectionChecker(txt_resp.Value))
                    {
                        alerMessage.InnerHtml = "Responsável inserido inválido. <br/> (Palavra reservada SQL encontrada).";
                        Alert.Visible = true;
                    }
                    else
                    {
                        alerMessage.InnerHtml = "Caracteres inválidos no responsavel. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
                        Alert.Visible = true;
                    }
                }
                else if (txt_tlmResp.Value.Replace(" ", "") == "")
                {
                    erro = true;
                    alerMessage.InnerText = "O telefone do responsável não pode conter caracteres vazios!";
                    Alert.Visible = true;
                }
                else if (txt_cargo.Value.Replace(" ", "") == "")
                {
                    erro = true;
                    alerMessage.InnerText = "Cargo do responsável não pode conter caracteres vazios!";
                    Alert.Visible = true;
                }
                else if (GlobalFunctions.HasSqlInjection(txt_cargo.Value))
                {
                    erro = true;
                    if (GlobalFunctions.SqlInjectionChecker(txt_cargo.Value))
                    {
                        alerMessage.InnerHtml = "Cargo do responsável inserido inválido. <br/> (Palavra reservada SQL encontrada).";
                        Alert.Visible = true;
                    }
                    else
                    {
                        alerMessage.InnerHtml = "Caracteres inválidos no cargo do responsavel. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
                        Alert.Visible = true;
                    }
                }
                else if (txt_atvPrinc.Value.Replace(" ", "") == "")
                {
                    erro = true;
                    alerMessage.InnerText = "Atividade principal não pode conter caracteres vazios!";
                    Alert.Visible = true;
                }
                else if (GlobalFunctions.HasSqlInjection(txt_atvPrinc.Value))
                {
                    erro = true;
                    if (GlobalFunctions.SqlInjectionChecker(txt_atvPrinc.Value))
                    {
                        alerMessage.InnerHtml = "Atividade principal inserida inválido. <br/> (Palavra reservada SQL encontrada).";
                        Alert.Visible = true;
                    }
                    else
                    {
                        alerMessage.InnerHtml = "Caracteres inválidos no atividade principal. <br/> (Caracteres proibidos: ;'()[]{}<>%)";
                        Alert.Visible = true;
                    }
                }







            }


            if (operacao.Text == "1")
            {
                //Response.Write("<script>alert('11111')</script>");

                String linhasql = "insert into Entidades (nome_entidade, nif_entidade, morada_entidade, loc_entidade, email_entidade, cpostal_entidade, telefone_entidade, natJuridica, resp_entidade, tlmResp_entidade, cargo_resp, atv_principal) values('" + txt_nome.Value + "', '" + txt_nif.Value + "','" + txt_morada.Value + "', '" + txt_local.Value + "', '" + txt_email.Value + "' ,'" + txt_CodPost.Value + "', '" + txt_telefone.Value +  "', '" + txt_NatJuri.Value + "', '" + txt_resp.Value + "', '" + txt_tlmResp.Value + "', '" + txt_cargo.Value + "', '" + txt_atvPrinc.Value + "');";

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");
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
                //Response.Write("<script>alert('22222')</script>");

                String linhasql = "update Entidades  set nome_entidade = '" + txt_nome.Value + "', nif_entidade = '" + txt_nif.Value + "', email_entidade = '" + txt_email.Value + "', loc_entidade = '" + txt_local.Value + "', morada_entidade = '" + txt_morada.Value +  "', telefone_entidade = '" + txt_telefone.Value +  "', cpostal_entidade = '" + txt_CodPost.Value + "', natjuridica = '" + txt_NatJuri.Value + "', resp_entidade = '" + txt_resp.Value + "', tlmResp_entidade = '" + txt_tlmResp.Value + "', cargo_resp = '" + txt_cargo.Value + "', atv_principal = '" + txt_atvPrinc.Value + "' where id_entidade = " + labelCod.Text + ";";

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");
                //Response.Write("<script>alert('aaaaa')</script>");

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

                String linhasql = "delete from Entidades where id_entidade = " + labelCod.Text + ";";
                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

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
    }
}