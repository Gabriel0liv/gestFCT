using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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


            if (operacao.Text == "1")
            {
                //Response.Write("<script>alert('11111')</script>");

                String linhasql = "insert into Entidades (nome_entidade, nif_entidade, morada_entidade, loc_entidade, email_entidade, cpostal_entidade, telefone_entidade, natJuridica, resp_entidade, tlmResp_entidade, cargo_resp, atv_principal) values('" + txt_nome.Value + "', '" + txt_nif.Value + "','" + txt_morada.Value + "', '" + txt_local.Value + "', '" + txt_email.Value + "' ,'" + txt_CodPost.Value + "', '" + txt_telefone.Value +  "', '" + txt_NatJuri.Value + "', '" + txt_resp.Value + "', '" + txt_tlmResp.Value + "', '" + txt_cargo.Value + "', '" + txt_atvPrinc.Value + "');";

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
                exampleModalForm.Visible=false;
            }

            if (operacao.Text == "2")
            {
                //Response.Write("<script>alert('22222')</script>");

                String linhasql = "update Entidades  set nome_entidade = '" + txt_nome.Value + "', nif_entidade = '" + txt_nif.Value + "', email_entidade = '" + txt_email.Value + "', loc_entidade = '" + txt_local.Value + "', morada_entidade = '" + txt_morada.Value +  "', telefone_entidade = '" + txt_telefone.Value +  "', cpostal_entidade = '" + txt_CodPost.Value + "', natjuridica = '" + txt_NatJuri.Value + "', resp_entidade = '" + txt_resp.Value + "', tlmResp_entidade = '" + txt_tlmResp.Value + "', cargo_resp = '" + txt_cargo.Value + "', atv_principal = '" + txt_atvPrinc.Value + "' where id_entidade = " + labelCod.Text + ";";

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");
                //Response.Write("<script>alert('aaaaa')</script>");

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();

            }

            if (operacao.Text == "3")
            {
                //Response.Write("<script>alert('33333')</script>");

                String linhasql = "delete from Entidades where id_entidade = " + labelCod.Text + ";";
                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                Database.NonQuerySqlSrv(linhasql);
                reset();
                refresh();
            }
            
            exampleModalForm.Visible = false;
            exampleModal.Visible = false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            exampleModal.Visible = false;
        }
    }
}