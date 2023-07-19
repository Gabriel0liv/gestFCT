using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Office.Interop.Word;


namespace GestaoFCT
{
    public partial class Documentos : System.Web.UI.Page
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

            NomeUser.InnerText = Session["Utilizador"].ToString();

            if (rptItems.Items.Count == 0)
            {
                refresh();
            }

            if (Session["cargo"].ToString() != "1")
                NavAdm.Visible = false;

            if (!Convert.ToBoolean(Session["direcao"]) && Session["cargo"].ToString() != "1")
                NavObj.Visible = false;

            if (!Convert.ToBoolean(Session["direcao"]) && Session["cargo"].ToString() != "1")
            {
                NavObj.Visible = false;
                NavProf.Visible = false;
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

        }

        protected void refresh()
        {

            if (Session["cargo"].ToString() == "2")
            {
                String linhasql = "select distinct * from tabelas_FCT where id_professor =" + Session["codigo"].ToString() + ";";
                System.Data.DataTable dt = Database.GetFromDBSqlSrv(linhasql);

                rptItems.DataSource = dt;
                rptItems.DataBind();

                String linhasql2 = "";
                if (Session["cargo"].ToString() == "2")
                {
                    if (Convert.ToBoolean(Session["direcao"]))
                        linhasql2 = "select distinct * from protocolo where id_curso = " + Session["curso"].ToString() + ";";
                    else
                        linhasql2 = "select distinct * from Protocolo where id_professor =" + Session["codigo"].ToString() + ";";
                }

                if (Session["cargo"].ToString() == "1")
                {
                    linhasql2 = "select distinct * from protocolo";
                }

                    System.Data.DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);

                rptItems2.DataSource = dt2;
                rptItems2.DataBind();

            }
            else
            {
                String linhasql = "select distinct * from tabelas_FCT;";
                System.Data.DataTable dt = Database.GetFromDBSqlSrv(linhasql);
                rptItems.DataSource = dt;
                rptItems.DataBind();

                String linhasql2 = "select distinct * from protocolo;";
                System.Data.DataTable dt2 = Database.GetFromDBSqlSrv(linhasql2);
                rptItems2.DataSource = dt2;
                rptItems2.DataBind();
            }

        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx"); // redireciona para a página de login
        }

        protected void GeraContrato_Click(object sender, EventArgs e)
        {
            labelCod.Text = HiddenField1.Value;
            labelStats.Text = HiddenField2.Value;

            if (labelCod.Text != "0")
            {
                if (labelStats.Text == "1A")
                {
                    Application wordApp = null;
                    Document doc = null;
                    try
                    {
                        wordApp = new Application();
                        doc = new Document();
                        doc = wordApp.Documents.Open(Server.MapPath("~/teste/Contrato.doc"));
                    }
                    catch (Exception ex)
                    {
                        exampleModalLabel.InnerText = "Documento possívelmente aberto!";
                        textoCancelar.InnerText = "Feche o documento modelo ou termine as tarefas do word.";
                        exampleModal.Visible = true;
                    }

                    string linhadesql = "";



                    if (doc != null)
                    {

                        linhadesql = "select * from Contratos where id_fct = " + labelCod.Text + ";";

                        var sqlConn = new SqlConnection(DocSQLData.ConnectionString);
                        var com = new SqlCommand(linhadesql, sqlConn);
                        sqlConn.Open();
                        SqlDataReader r = com.ExecuteReader();
                        while (r.Read())
                        {

                            string[] dataInicio = r["inicio_fct"].ToString().Split('/');
                            // Obtém o dia, mês e ano
                            int dia = int.Parse(dataInicio[0]);
                            int mes = int.Parse(dataInicio[1]);
                            int ano = int.Parse(dataInicio[2]);
                            // Converte o mês para formato por extenso
                            string mesPorExtenso = new DateTime(ano, mes, dia).ToString("MMMM");
                            string data1 = dia.ToString() + "/" + mesPorExtenso + "/" + ano.ToString();

                            try { doc.Variables["dataInicio"].Value = data1; } catch (Exception ex) { doc.Variables["dataInicio"].Value = " "; }

                            dataInicio = r["fim_fct"].ToString().Split('/');
                            dia = int.Parse(dataInicio[0]);
                            mes = int.Parse(dataInicio[1]);
                            ano = int.Parse(dataInicio[2]);
                            mesPorExtenso = new DateTime(ano, mes, dia).ToString("MMMM");
                            data1 = dia.ToString() + "/" + mesPorExtenso + "/" + ano.ToString();
                            
                            try { doc.Variables["dataFim"].Value = data1; } catch(Exception ex) { doc.Variables["dataFim"].Value = " "; }

                            try { doc.Variables["ano_fct"].Value = r["ano_fct"].ToString(); } catch (Exception ex) { doc.Variables["ano_fct"].Value = " "; }
                            try { doc.Variables["nome_curso"].Value = r["nome_curso"].ToString(); } catch (Exception ex) { doc.Variables["nome_curso"].Value = " "; }
                            try { doc.Variables["ano_fct"].Value = r["ano_fct"].ToString(); } catch (Exception ex) { doc.Variables["ano_fct"].Value = " "; }
                            try { doc.Variables["nome_aluno"].Value = r["nome_aluno"].ToString(); } catch (Exception ex) { doc.Variables["nome_aluno"].Value = " "; }
                            try { doc.Variables["bi_aluno"].Value = r["bi_aluno"].ToString(); } catch (Exception ex) { doc.Variables["bi_aluno"].Value = " "; }
                            try { doc.Variables["turma"].Value = r["turma"].ToString(); } catch (Exception ex) { doc.Variables["turma"].Value = " "; }
                            try { doc.Variables["nome_professor"].Value = r["nome_prof"].ToString(); } catch (Exception ex) { doc.Variables["nome_professor"].Value = " "; }
                            try { doc.Variables["nome_tutor"].Value = r["nome_tutor"].ToString(); } catch (Exception ex) { doc.Variables["nome_tutor"].Value = " "; }
                            try { doc.Variables["nome_entidade"].Value = r["nome_entidade"].ToString(); } catch (Exception ex) { doc.Variables["nome_entidade"].Value = " "; }
                            try { doc.Variables["loc_entidade"].Value = r["loc_entidade"].ToString(); } catch (Exception ex) { doc.Variables["loc_entidade"].Value = " "; }
                            try { doc.Variables["nome_resp"].Value = r["resp_entidade"].ToString(); } catch (Exception ex) { doc.Variables["nome_resp"].Value = " "; }
                            try { doc.Variables["num_horas"].Value = r["num_horas"].ToString(); } catch (Exception ex) { doc.Variables["num_horas"].Value = " "; }
                            try { doc.Variables["nome_ee"].Value = r["nome_ee"].ToString(); } catch (Exception ex) { doc.Variables["nome_ee"].Value = " "; }
                            try { doc.Variables["dataExFim"].Value = dia.ToString() + " de " + mesPorExtenso + " " + ano.ToString(); } catch (Exception ex) { doc.Variables["dataExFim"].Value = " "; }


                            dataInicio = r["inicio_fct"].ToString().Split('/');
                            dia = int.Parse(dataInicio[0]);
                            mes = int.Parse(dataInicio[1]);
                            ano = int.Parse(dataInicio[2]);
                            mesPorExtenso = new DateTime(ano, mes, dia).ToString("MMMM");
                            data1 = dia.ToString() + " de " + mesPorExtenso + " " + ano.ToString();

                            try { doc.Variables["dataExInicio"].Value = data1; } catch (Exception ex) { doc.Variables["dataExInicio"].Value = " "; }

                        }
                        r.Close();
                        sqlConn.Close();

                        doc.Fields.Update();
                        doc.Save();


                        if (checkFileFormat.Checked)
                        {
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
                            response.AppendHeader("Content-Disposition", "attachment; filename=ContratoFormação.pdf");
                            response.TransmitFile(pdfPath);
                            response.End();
                        }
                        else
                        {
                            string directoryPath = Server.MapPath("~/temp/");
                            string docPath = Path.Combine(directoryPath, "Contrato.doc");

                            // Verificar se o diretório existe e criar se necessário
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }

                            doc.SaveAs2(docPath, WdSaveFormat.wdFormatDocument);

                            doc.Close();
                            wordApp.Quit();

                            // Transferir o arquivo para o usuário
                            HttpResponse response = HttpContext.Current.Response;
                            response.Clear();
                            response.ContentType = "application/msword";
                            response.AppendHeader("Content-Disposition", "attachment; filename=ContratoFormação.doc");
                            response.TransmitFile(docPath);
                            response.End();
                        }



                    }

                }
                else
                {
                    exampleModalLabel.InnerText = "Nenhum Aluno selecionado!";
                    textoCancelar.InnerText = "Uma entidade foi selecionado, alterne para a tabela de Alunos.";
                    exampleModal.Visible = true;
                }



            }
            else
            {
                textoCancelar.InnerText = "Nenhum registo foi selecionado!";
                exampleModal.Visible = true;
            }

        }



        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            exampleModal.Visible = false;
        }

        protected void GeraProtocolo_Click(object sender, EventArgs e)
        {
            labelCod.Text = HiddenField1.Value;
            labelStats.Text = HiddenField2.Value;

            if (labelCod.Text != "0")
            {
                if (labelStats.Text == "1E")
                {
                    Application wordApp = null;
                    Document doc = null;
                    try
                    {
                        wordApp = new Application();
                        doc = new Document();
                        doc = wordApp.Documents.Open(Server.MapPath("~/teste/Protocolo.doc"));
                    }
                    catch (Exception ex)
                    {
                        exampleModalLabel.InnerText = "Documento possívelmente aberto!";
                        textoCancelar.InnerText = "Feche o documento modelo ou termine as tarefas do word.";
                        exampleModal.Visible = true;
                    }

                    string linhadesql = "";

                    if (doc != null)
                    {
                        linhadesql = "select distinct * from Protocolo where id_entidade = " + labelCod.Text + ";";

                        var sqlConn = new SqlConnection(DocSQLData.ConnectionString);
                        var com = new SqlCommand(linhadesql, sqlConn);
                        sqlConn.Open();
                        SqlDataReader r = com.ExecuteReader();
                        while (r.Read())
                        {
                            try { doc.Variables["nome_entidade"].Value = r["nome_entidade"].ToString(); } catch(Exception ex) { doc.Variables["nome_entidade"].Value = " "; }
                            try { doc.Variables["loc_entidade"].Value = r["loc_entidade"].ToString(); } catch (Exception ex) { doc.Variables["loc_entidade"].Value = " "; }
                            try { doc.Variables["nome_resp"].Value = r["resp_entidade"].ToString(); } catch (Exception ex) { doc.Variables["nome_resp"].Value = " "; }
                            try { doc.Variables["cargo_resp"].Value = r["cargo_resp"].ToString(); } catch (Exception ex) { doc.Variables["cargo_resp"].Value = " "; }
                            try { doc.Variables["nome_curso"].Value = r["nome_curso"].ToString(); } catch (Exception ex) { doc.Variables["nome_curso"].Value = " "; }
                            try { doc.Variables["dia"].Value = DateTime.Now.Day.ToString(); } catch (Exception ex) { doc.Variables["dia"].Value = " "; }
                            try { doc.Variables["mes"].Value = DateTime.Now.ToString("MMMM", new CultureInfo("pt-BR")); } catch (Exception ex) { doc.Variables["mes"].Value = " "; }
                            try { doc.Variables["ano"].Value = DateTime.Now.Year.ToString(); } catch (Exception ex) { doc.Variables["ano"].Value = " "; }
                                                        

                        }
                        r.Close();
                        sqlConn.Close();

                        doc.Fields.Update();
                        doc.Save();

                        if (checkFileFormat.Checked)
                        {
                            string directoryPath = Server.MapPath("~/temp/");
                            string pdfPath = Path.Combine(directoryPath, "Protocolo.pdf");

                            // Verificar se o diretório existe e criar se necessário
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }

                            doc.SaveAs2(pdfPath, WdSaveFormat.wdFormatPDF);
                            doc.Close();
                            wordApp.Quit();

                            //Transferir o arquivo para o usuário
                            HttpResponse response = HttpContext.Current.Response;
                            response.Clear();
                            response.ContentType = "application/pdf";
                            response.AppendHeader("Content-Disposition", "attachment; filename=ProtocoloCooperação.pdf");
                            response.TransmitFile(pdfPath);
                            response.End();
                        }
                        else
                        {
                            string directoryPath = Server.MapPath("~/temp/");
                            string docPath = Path.Combine(directoryPath, "Protocolo.doc");

                            // Verificar se o diretório existe e criar se necessário
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }
                            doc.SaveAs2(docPath, WdSaveFormat.wdFormatDocument);
                            doc.Close();
                            wordApp.Quit();
                            // Transferir o arquivo para o usuário
                            HttpResponse response = HttpContext.Current.Response;
                            response.Clear();
                            response.ContentType = "application/msword";
                            response.AppendHeader("Content-Disposition", "attachment; filename=ProtocoloCooperação.doc");
                            response.TransmitFile(docPath);
                            response.End();
                        }
                    }
                }
                else
                {
                    exampleModalLabel.InnerText = "Nenhuma entidade selecionada!";
                    textoCancelar.InnerText = "Um aluno foi selecionado, alterne para a tabela de entidades.";
                    exampleModal.Visible = true;
                }



            }
            else
            {
                exampleModalLabel.InnerText = "Nenhuma entidade selecionada!";
                textoCancelar.InnerText = "Nenhum registo foi selecionado!";
                exampleModal.Visible = true;
            }
        }

        protected void GeraCaderneta_Click(object sender, EventArgs e)
        {
            labelCod.Text = HiddenField1.Value;
            labelStats.Text = HiddenField2.Value;

            if (labelCod.Text != "0")
            {
                if (labelStats.Text == "1A")
                {
                    Application wordApp = null;
                    Document doc = null;
                    try
                    {
                        wordApp = new Application();
                        doc = new Document();
                        doc = wordApp.Documents.Open(Server.MapPath("~/teste/CadernetaAluno.doc"));
                    }
                    catch (Exception ex)
                    {
                        exampleModalLabel.InnerText = "Documento possívelmente aberto!";
                        textoCancelar.InnerText = "Feche o documento modelo ou termine as tarefas do word.";
                        exampleModal.Visible = true;
                    }




                    string linhadesql = "";
                    if (doc != null)
                    {

                        // Dados gerais ***************************************************************************
                        linhadesql = "select * from Cadernetas where id_fct = " + labelCod.Text + ";";
                        string curso = "";

                        var sqlConn = new SqlConnection(DocSQLData.ConnectionString);
                        var com = new SqlCommand(linhadesql, sqlConn);
                        sqlConn.Open();
                        SqlDataReader r = com.ExecuteReader();
                        while (r.Read())
                        {

                            //variaveis ALUNO
                            try { doc.Variables["nome_aluno"].Value = r["nome_aluno"].ToString(); } catch (Exception ex) { doc.Variables["nome_aluno"].Value = " "; }
                            try { doc.Variables["morada_aluno"].Value = r["morada_aluno"].ToString(); } catch (Exception ex) { doc.Variables["morada_aluno"].Value = " "; }
                            try { doc.Variables["cpostal_aluno"].Value = r["cpostal_aluno"].ToString(); } catch (Exception ex) { doc.Variables["cpostal_aluno"].Value = " "; }
                            try { doc.Variables["loc_aluno"].Value = r["loc_aluno"].ToString(); } catch (Exception ex) { doc.Variables["loc_aluno"].Value = " "; }
                            try { doc.Variables["bi_aluno"].Value = r["bi_aluno"].ToString(); } catch (Exception ex) { doc.Variables["bi_aluno"].Value = " "; }
                            try { doc.Variables["tlm_aluno"].Value = r["tlf_aluno"].ToString(); } catch (Exception ex) { doc.Variables["tlf_aluno"].Value =" "; }
                            try { doc.Variables["email_aluno"].Value = r["email_aluno"].ToString(); } catch (Exception ex) { doc.Variables["email_aluno"].Value = " "; }

                            //Variáveis ENCARREGADO
                            try { doc.Variables["nome_ee"].Value = r["nome_ee"].ToString(); } catch (Exception ex) { doc.Variables["nome_ee"].Value = " "; }
                            try { doc.Variables["nome_ee"].Value = r["nome_ee"].ToString(); } catch (Exception ex) { doc.Variables["nome_ee"].Value = " "; }
                            try { doc.Variables["tlm_ee"].Value = r["tlm_ee"].ToString(); } catch (Exception ex) { doc.Variables["tlm_ee"].Value = " "; }
                            try { doc.Variables["email_ee"].Value = r["email_ee"].ToString(); } catch (Exception ex) { doc.Variables["email_ee"].Value = " "; }

                            //Variáveis ENTIDADE
                            try { doc.Variables["nome_entidade"].Value = r["nome_entidade"].ToString(); } catch (Exception ex) { doc.Variables["nome_entidade"].Value = " "; }
                            try { doc.Variables["nif_entidade"].Value = r["nif_entidade"].ToString(); } catch (Exception ex) { doc.Variables["nif_entidade"].Value = " "; }
                            try { doc.Variables["morada_entidade"].Value = r["morada_entidade"].ToString(); } catch (Exception ex) { doc.Variables["morada_entidade"].Value = " "; }
                            try { doc.Variables["cpostal_entidade"].Value = r["cpostal_entidade"].ToString(); } catch (Exception ex) { doc.Variables["cpostal_entidade"].Value = " "; }
                            try { doc.Variables["loc_entidade"].Value = r["loc_entidade"].ToString(); } catch (Exception ex) { doc.Variables["loc_entidade"].Value = " "; }
                            try { doc.Variables["tlf_entidade"].Value = r["tlf_entidade"].ToString(); } catch (Exception ex) { doc.Variables["tlf_entidade"].Value = " "; }
                            try { doc.Variables["tlm_entidade"].Value = r["tlm_entidade"].ToString(); } catch (Exception ex) { doc.Variables["tlm_entidade"].Value = " "; }
                            try { doc.Variables["email_entidade"].Value = r["email_entidade"].ToString(); } catch (Exception ex) { doc.Variables["email_entidade"].Value = " "; }
                            try { doc.Variables["natjuridica"].Value = r["natjuridica"].ToString(); } catch (Exception ex) { doc.Variables["natjuridica"].Value = " "; }
                            try { doc.Variables["resp_entidade"].Value = r["resp_entidade"].ToString(); } catch (Exception ex) { doc.Variables["resp_entidade"].Value = " ";  }
                            try { doc.Variables["cargo_resp"].Value = r["cargo_resp"].ToString(); } catch (Exception ex) { doc.Variables["cargo_resp"].Value = " "; }
                            try { doc.Variables["atv_principal"].Value = r["atv_principal"].ToString(); } catch (Exception ex) { doc.Variables["atv_principal"].Value = " "; }

                            //Variáveis  TUTOR
                            try { doc.Variables["nome_tutor"].Value = r["nome_tutor"].ToString(); } catch (Exception ex) { doc.Variables["nome_tutor"].Value = " "; }
                            try { doc.Variables["tlf_tutor"].Value = r["tlf_tutor"].ToString(); } catch (Exception ex) { doc.Variables["tlf_tutor"].Value = " "; }
                            try { doc.Variables["tlm_tutor"].Value = r["tlm_tutor"].ToString(); } catch (Exception ex) { doc.Variables["tlm_tutor"].Value = " "; }
                            try { doc.Variables["email_tutor"].Value = r["email_tutor"].ToString(); } catch (Exception ex) { doc.Variables["email_tutor"].Value = " "; }
                            try { doc.Variables["cargo_tutor"].Value = r["cargo_tutor"].ToString(); } catch (Exception ex) { doc.Variables["cargo_tutor"].Value = " "; }

                            //Variáveis PROFESSOR
                            try { doc.Variables["nome_prof"].Value = r["nome_prof"].ToString(); } catch (Exception ex) { doc.Variables["nome_prof"].Value = " "; }
                            try { doc.Variables["tlm_prof"].Value = r["tlm_prof"].ToString(); } catch (Exception ex) { doc.Variables["tlm_prof"].Value = " "; }
                            try { doc.Variables["email_prof"].Value = r["email_prof"].ToString(); } catch (Exception ex) { doc.Variables["email_prof"].Value = " "; }



                            //Calendário da FCT
                            string[] data1 = r["inicio_fct"].ToString().Split('/');
                            int dia = int.Parse(data1[0]);
                            int mes = int.Parse(data1[1]);
                            int ano = int.Parse(data1[2]);
                            string mesPorExtenso = new DateTime(ano, mes, dia).ToString("MMMM");
                            
                            try { doc.Variables["inicio_fct"].Value = dia.ToString() + " de " + mesPorExtenso + " " + ano.ToString(); } catch (Exception ex) { doc.Variables["inicio_fct"].Value = " "; }

                            data1 = r["fim_fct"].ToString().Split('/');
                            dia = int.Parse(data1[0]);
                            mes = int.Parse(data1[1]);
                            ano = int.Parse(data1[2]);
                            mesPorExtenso = new DateTime(ano, mes, dia).ToString("MMMM");
                            

                            try { doc.Variables["fim_fct"].Value = dia.ToString() + " de " + mesPorExtenso + " " + ano.ToString(); } catch (Exception ex) { doc.Variables["fim_fct"].Value = " "; }
                            try { doc.Variables["num_horas"].Value = r["num_horas"].ToString(); } catch (Exception ex) { doc.Variables["num_horas"].Value = " "; }
                            

                            curso = r["nome_curso"].ToString();
                        }
                        r.Close();
                        sqlConn.Close();


                        // Objetivos ***************************************************************************
                        linhadesql = "select * from Objetivos_table where nome_curso = '" + curso + "';";

                        System.Data.DataTable dt = Database.GetFromDBSqlSrv(linhadesql);

                        for (int i = 0; i < 14; i++)
                        {
                            if (i < dt.Rows.Count) //se tiver registos os introduz nas variáveis
                                doc.Variables["objetivo" + (i + 1).ToString()].Value = dt.Rows[i]["descricao_objetivo"].ToString(); 
                            else
                                doc.Variables["objetivo" + (i + 1).ToString()].Value = " ";
                        }


                        // Sumários ***************************************************************************
                        linhadesql = "select * from Sumarios_table where id_fct = " + labelCod.Text + " and status_sumario = 'Validado' ORDER BY CONVERT(date, data_sumario, 103) asc;";

                        dt = Database.GetFromDBSqlSrv(linhadesql);

                        int totalHoras = 0; // Variável para armazenar o total de horas
                        int j = 1;
                        for (int i = 0; i < 75; i++)
                        {
                            if (i < dt.Rows.Count) //se tiver registos os introduz nas variáveis
                            {
                                string[] data1 = dt.Rows[i]["data_sumario"].ToString().Split('/');
                                int dia = int.Parse(data1[0]);
                                int mes = int.Parse(data1[1]);
                                int ano = int.Parse(data1[2]);
                                string mesPorExtenso = new DateTime(ano, mes, dia).ToString("MMMM");
                                doc.Variables["datasumario" + (i + 1).ToString()].Value = dia.ToString() + " de " + mesPorExtenso + " " + ano.ToString();

                                doc.Variables["descSumario" + (i + 1).ToString()].Value = dt.Rows[i]["descricao_sumario"].ToString().Replace("\\n", Environment.NewLine);
                                doc.Variables["horasSumario" + (i + 1).ToString()].Value = dt.Rows[i]["horas_sumario"].ToString();

                                doc.Variables["txtData" + (i + 1).ToString()].Value = "Data do sumário: ";
                                // Somar o número de horas aos 5 registros anteriores
                                totalHoras += Convert.ToInt32(dt.Rows[i]["horas_sumario"]);
                                // Atribuir o total de horas a uma variável a cada 5 registros
                                if ((i + 1) % 5 == 0)
                                {

                                    doc.Variables["HorasSemanais" + j.ToString()].Value = totalHoras.ToString();
                                    j++;
                                    // Zerar o total de horas
                                    totalHoras = 0;
                                }
                            }
                            else //se não tiver mais registos, não exibir mensagem de erro nas seguintes variaveis
                            {
                                try
                                {
                                    doc.Variables["datasumario" + (i + 1).ToString()].Value = " ";
                                    doc.Variables["descSumario" + (i + 1).ToString()].Value = " ";
                                    doc.Variables["horasSumario" + (i + 1).ToString()].Value = " ";
                                }
                                catch (Exception ex) { }

                                if (totalHoras != 0)
                                {
                                    totalHoras += 0;
                                    if ((i + 1) % 5 == 0)
                                    {
                                        doc.Variables["HorasSemanais" + j.ToString()].Value = totalHoras.ToString();
                                        j++;
                                        // Zerar o total de horas
                                        totalHoras = 0;
                                    }
                                }
                                else { doc.Variables["HorasSemanais" + j.ToString()].Value = " "; j++; }
                            }


                        }

                        doc.Fields.Update();
                        doc.Save();



                        if (checkFileFormat.Checked)
                        {

                            string directoryPath = Server.MapPath("~/temp/");
                            string pdfPath = Path.Combine(directoryPath, "CadernetaAluno.pdf");

                            // Verificar se o diretório existe e criar se necessário
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }

                            doc.SaveAs2(pdfPath, WdSaveFormat.wdFormatPDF);

                            doc.Close();
                            wordApp.Quit();

                            //Transferir o arquivo para o usuário
                            HttpResponse response = HttpContext.Current.Response;
                            response.Clear();
                            response.ContentType = "application/pdf";
                            response.AppendHeader("Content-Disposition", "attachment; filename=CadernetaAluno.pdf");
                            response.TransmitFile(pdfPath);
                            response.End();
                        }
                        else
                        {
                            string directoryPath = Server.MapPath("~/temp/");
                            string docPath = Path.Combine(directoryPath, "CadernetaAluno.doc");

                            // Verificar se o diretório existe e criar se necessário
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }

                            doc.SaveAs2(docPath, WdSaveFormat.wdFormatDocument);

                            doc.Close();
                            wordApp.Quit();

                            // Transferir o arquivo para o usuário
                            HttpResponse response = HttpContext.Current.Response;
                            response.Clear();
                            response.ContentType = "application/msword";
                            response.AppendHeader("Content-Disposition", "attachment; filename=CadernetaAluno.doc");
                            response.TransmitFile(docPath);
                            response.End();
                        }


                    }
                }
                else
                {
                    exampleModalLabel.InnerText = "Nenhum Aluno selecionado!";
                    textoCancelar.InnerText = "Uma entidade foi selecionado, alterne para a tabela de Alunos.";
                    exampleModal.Visible = true;
                }



            }
            else
            {
                exampleModalLabel.InnerText = "Nenhum registo selecionado!";
                textoCancelar.InnerText = "Selecione um registo na tabela abaixo.";
                exampleModal.Visible = true;
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            refresh();
        }
    }
}