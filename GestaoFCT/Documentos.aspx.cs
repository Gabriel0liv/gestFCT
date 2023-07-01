using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Word;


namespace GestaoFCT
{
    public partial class Documentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (Session["cargo"].ToString() != "1" && Session["cargo"].ToString() != "2" && Session["cargo"].ToString() != "3")
            //{
            //    //Redirect to login page.
            //    Response.Redirect("~/Login.aspx");
            //}

            NomeUser.InnerText = Session["Utilizador"].ToString();

            if (rptItems.Items.Count == 0)
            {
                refresh();
            }

            if (Session["cargo"].ToString() == "4")
            {
                tableDocs.Visible = false;
                DivProtocolo.Visible = false;

                //oculta opções de navegação
                NavAln.Visible = false;
                NavCurso.Visible = false;
                NavEE.Visible = false;
                NavEnt.Visible = false;
                NavFCT.Visible = false;
                NavProf.Visible = false;
                SecGest.Visible = false;
                NavTar.Visible = false;
                NavTut.Visible = false;
            }



        }

        protected void refresh()
        {
            if (Session["cargo"].ToString() == "1")
            {
                String linhasql = "select * from tabelas_FCT;";
                System.Data.DataTable dt = Database.GetFromDBSqlSrv(linhasql);

                rptItems.DataSource = dt;
                rptItems.DataBind();
            }

            if(Session["cargo"].ToString() == "2")
            {
                String linhasql = "select * from tabelas_FCT where id_professor =" + Session["codigo"].ToString() + ";";
                System.Data.DataTable dt = Database.GetFromDBSqlSrv(linhasql);

                rptItems.DataSource = dt;
                rptItems.DataBind();
            }

            if (Session["cargo"].ToString() == "3")
            {
                String linhasql = "select * from tabelas_FCT where id_tutor =" + Session["codigo"].ToString() + ";";
                System.Data.DataTable dt = Database.GetFromDBSqlSrv(linhasql);

                rptItems.DataSource = dt;
                rptItems.DataBind();
            }



        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx"); // redireciona para a página de login
        }

        protected void GeraContrato_Click(object sender, EventArgs e)
        {
            Application wordApp = new Application();
            Document doc = new Document();


            doc = wordApp.Documents.Open(Server.MapPath("~/teste/Contrato.doc"));

            string linhadesql = "";
            if (doc != null)
            {

                if (Session["cargo"].ToString() == "4")
                   linhadesql = "select * from Contratos where id_aluno = " + Session["codigo"].ToString() + ";";
                else
                    linhadesql = "select * from Contratos where id_fct = " + labelCod.Text + ";";


                var sqlConn = new SqlConnection(DocSQLData.ConnectionString);
                var com = new SqlCommand(linhadesql, sqlConn);
                sqlConn.Open();
                SqlDataReader r = com.ExecuteReader();
                while (r.Read())
                {
                    //txt_sumario.Text = r["descricao_sumario"].ToString();
                    //txt_numHora.Value = r["horas_sumario"].ToString();
                    //ddl_Status.SelectedValue = r["status_sumario"].ToString();
                    //txt_dataSum.Value = r["data_sumario"].ToString();

                    doc.Variables["nome_curso"].Value = r["nome_curso"].ToString();
                    doc.Variables["nome_aluno"].Value = r["nome_aluno"].ToString();
                    doc.Variables["bi_aluno"].Value = r["bi_aluno"].ToString();
                    doc.Variables["turma"].Value = r["turma"].ToString();
                    doc.Variables["nome_professor"].Value = r["nome_prof"].ToString();
                    doc.Variables["nome_tutor"].Value = r["nome_tutor"].ToString();
                    doc.Variables["nome_entidade"].Value = r["nome_entidade"].ToString();
                    doc.Variables["localidade_entidade"].Value = r["loc_entidade"].ToString();
                    doc.Variables["nome_resp"].Value = r["resp_entidade"].ToString();
                    doc.Variables["num_horas"].Value = r["num_horas"].ToString();


   

                }
                r.Close();
                sqlConn.Close();

                doc.Fields.Update();
                doc.Save();




                string directoryPath = Server.MapPath("~/temp/");
                string pdfPath = Path.Combine(directoryPath, "Contrato.pdf");

                // Verificar se o diretório existe e criar se necessário
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                doc.SaveAs2(pdfPath, WdSaveFormat.wdFormatPDF);

                doc.Close();
                wordApp.Quit();

                //string pdfPath = Server.MapPath("~/temp/documento.pdf");





                //Transferir o arquivo para o usuário
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ContentType = "application/pdf";
                response.AppendHeader("Content-Disposition", "attachment; filename=documento.pdf");
                response.TransmitFile(pdfPath);
                response.End();

            }

        }
    }
}