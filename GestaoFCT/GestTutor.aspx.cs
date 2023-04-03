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
    public partial class GestTutor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (Session["Utilizador"] == null)
            //{
            //    //Redirect to login page.
            //    Response.Redirect("~/Login.aspx");
            //}
            //else
            //{
            //    //Redirect to home page
            //    NomeUser.InnerText = Session["Utilizador"].ToString();
            //}

            if (rptItems.Items.Count == 0)
            {
                refresh();
            }

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
            txt_nome.Value = "";
            txt_nif.Value = "";
            txt_email.Value = "";
            txt_telefone.Value = "";
            txt_morada.Value = "";
            txt_local.Value = "";
            txt_CodPost.Value = "";
            txt_dataNasc.Value = "";
            //txt_resp.Value = "";
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
                txt_nome.Value = "" + r["nome_tutor"];
                txt_nif.Value = "" + r["nif_tutor"];
                txt_email.Value = "" + r["email_tutor"];
                txt_telefone.Value = "" + r["telefone_tutor"];
                txt_morada.Value = "" + r["morada_tutor"];
                txt_local.Value = "" + r["loc_tutor"];
                txt_CodPost.Value = "" + r["cpostal_tutor"];
                txt_dataNasc.Value = "" + r["dataNasc_tutor"];
                //txt_resp.Value = "" + r["resp_tutor"];
                txt_pass.Value = "" + r["tlmResp_tutor"];

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

            exampleModalFormTitle.InnerText = "Criar Tutor";
            btn_enviar.Text = "Criar Tutor";
            exampleModalForm.Visible = true;


        }

        protected void Editar(object sender, EventArgs e)
        {
            operacao.Text = "2";
            labelCod.Text = HiddenField1.Value;
            Atualizar();
            exampleModalFormTitle.InnerText = "Editar Tutor";
            btn_enviar.Text = "Editar Tutor";
            exampleModalForm.Visible = true;

        }

        protected void Eliminar(object sender, EventArgs e)
        {

            operacao.Text = "3";
            labelCod.Text = HiddenField1.Value;

            if(labelCod.Text != "0")
            {
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

        /*
        protected void Comandos(object sender, EventArgs e)
        {


            if (operacao.Text == "1")
            {
                //Response.Write("<script>alert('11111')</script>");

                String linhasql = "insert into tutores (nome_tutor, nif_tutor, morada_tutor, loc_tutor, email_tutor, cpostal_tutor, telefone_tutor, natJuridica, resp_tutor, tlmResp_tutor, cargo_resp, atv_principal) values('" + txt_nome.Value + "', '" + txt_nif.Value + "','" + txt_morada.Value + "', '" + txt_local.Value + "', '" + txt_email.Value + "' ,'" + txt_CodPost.Value + "', '" + txt_telefone.Value +  "', '" + txt_NatJuri.Value + "', '" + txt_resp.Value + "', '" + txt_tlmResp.Value + "', '" + txt_cargo.Value + "', '" + txt_atvPrinc.Value + "');";

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                //Database.NonQuerySqlSrv(linhasql);
                //reset();
                //refresh();
                //exampleModalForm.Visible=false;
            }

            if (operacao.Text == "2")
            {
                //Response.Write("<script>alert('22222')</script>");

                String linhasql = "update tutores  set nome_tutor = '" + txt_nome.Value + "', nif_tutor = '" + txt_nif.Value + "', email_tutor = '" + txt_email.Value + "', loc_tutor = '" + txt_local.Value + "', morada_tutor = '" + txt_morada.Value +  "', telefone_tutor = '" + txt_telefone.Value +  "', cpostal_tutor = '" + txt_CodPost.Value + "', natjuridica = '" + txt_NatJuri.Value + "', resp_tutor = '" + txt_resp.Value + "', tlmResp_tutor = '" + txt_tlmResp.Value + "', cargo_resp = '" + txt_cargo.Value + "', atv_principal = '" + txt_atvPrinc.Value + "' where id_tutor = " + labelCod.Text + ";";

                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");
                //Response.Write("<script>alert('aaaaa')</script>");

                //Database.NonQuerySqlSrv(linhasql);
                //reset();
                //refresh();

            }

            if (operacao.Text == "3")
            {
                //Response.Write("<script>alert('33333')</script>");

                String linhasql = "delete from Tutores where id_tutor = " + labelCod.Text + ";";
                //Response.Write("<script>alert('" + HttpUtility.JavaScriptStringEncode(linhasql) + "')</script>");

                //Database.NonQuerySqlSrv(linhasql);
                //reset();
                //refresh();
            }
            
            exampleModalForm.Visible = false;
            exampleModal.Visible = false;
        }
        */
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            exampleModal.Visible = false;
        }
    }
}