<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sumarios.aspx.cs" EnableViewState="true" Inherits="GestaoFCT.Sumarios" %>


<!DOCTYPE html>

<!--
 // WEBSITE: https://themefisher.com
 // TWITTER: https://twitter.com/themefisher
 // FACEBOOK: https://www.facebook.com/themefisher
 // GITHUB: https://github.com/themefisher/
-->

<html lang="pt-pt" dir="ltr">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <title>GestFCT - Sumários</title>

    <!-- GOOGLE FONTS -->
    <link href="https://fonts.googleapis.com/css?family=Karla:400,700|Roboto" rel="stylesheet">
    <link href="plugins/material/css/materialdesignicons.min.css" rel="stylesheet" />
    <link href="plugins/simplebar/simplebar.css" rel="stylesheet" />
    <script src="https://kit.fontawesome.com/89865b6117.js" crossorigin="anonymous"></script>

    <!-- PLUGINS CSS STYLE -->
    <link href="plugins/nprogress/nprogress.css" rel="stylesheet" />

    <!-- MONO CSS -->
    <link id="main-css-href" rel="stylesheet" href="css/style.css" />

    <!-- TABULATOR -->
    <link href="tabulator-master/dist/css/tabulator.css" rel="stylesheet">
    <script type="text/javascript" src="tabulator-master/dist/js/tabulator.js"></script>

    <!-- FAVICON -->
    <link href="images/logo GestFCT.png" rel="shortcut icon" />

    <!-- jQuery -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.3/jquery.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

    <!--
    HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries
  -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    <script src="plugins/nprogress/nprogress.js"></script>
</head>


<body class="navbar-fixed sidebar-fixed " onunload="onPageUnload()" id="body">
    <form id="form1" runat="server">
        <script>
            NProgress.configure({ showSpinner: false });
            NProgress.start();
        </script>



        <!-- ====================================
    ——— WRAPPER
    ===================================== -->
        <div class="wrapper">


            <!-- LABELS DE CONTROLE -->
            <asp:Label ID="labelStats" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="labelCod" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="operacao" runat="server" Text="" Visible="false"></asp:Label>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
            <asp:SqlDataSource ID="SumSQLData" runat="server" ConnectionString="<%$ ConnectionStrings:FCTConnectionString %>"></asp:SqlDataSource>



            <!-- ====================================
          ——— LEFT SIDEBAR WITH OUT FOOTER
        ===================================== -->

            <aside class="left-sidebar sidebar-dark" id="left-sidebar">
                <div id="sidebar" class="sidebar sidebar-with-footer">
                    <!-- Aplication Brand -->
                    <div class="app-brand">
                        <a href="/index.html">
                            <img src="images/logo GestFCT.png" style="max-width: 50px" alt="Mono">
                            <span class="brand-name">GestFCT</span>
                        </a>
                    </div>
                    <!-- begin sidebar scrollbar -->
                    <div class="sidebar-left" data-simplebar="" style="height: 100%;">
                        <!-- sidebar menu -->
                        <ul class="nav sidebar-inner" id="sidebar-menu">

                            <li id="NavSum" runat="server" class="active">
                                <a class="sidenav-item-link" href="Sumarios.aspx">
                                    <i class="mdi mdi-calendar-check"></i>
                                    <span class="nav-text">Sumarios</span>
                                </a>
                            </li>

                            <li id="NavTar" runat="server">
                                <a class="sidenav-item-link" href="Tarefas.aspx">
                                    <i class="mdi mdi-calendar-check"></i>
                                    <span class="nav-text">Tarefas</span>
                                </a>
                            </li>

                            <li id="NavDoc" runat="server">
                                <a class="sidenav-item-link" href="Documentos.aspx">
                                    <i class="mdi mdi-file-multiple"></i>
                                    <span class="nav-text">Documentos</span>
                                </a>
                            </li>


                            <li id="SecGest" class="section-title" runat="server">Gestão
                            </li>

                            <li id="NavFCT" runat="server">
                                <a class="sidenav-item-link" href="GestFCT.aspx">
                                    <i class="fa-solid fa-address-card" style="font-size: 18px"></i>
                                    <span class="nav-text">Gestão da FCT</span>
                                </a>
                            </li>

                            <li id="NavAln" runat="server">
                                <a class="sidenav-item-link" href="GestAluno.aspx">
                                    <i class="fa-solid fa-users" style="font-size: 18px"></i>
                                    <span class="nav-text">Alunos</span>
                                </a>
                            </li>

                            <li id="NavEE" runat="server">
                                <a class="sidenav-item-link" href="GestEnc.aspx">
                                    <i class="mdi mdi-account-group"></i>
                                    <span class="nav-text">Enc. Educação</span>
                                </a>
                            </li>

                            <li id="NavCurso" runat="server">
                                <a class="sidenav-item-link" href="GestCursos.aspx">
                                    <i class="fa-solid fa-graduation-cap" style="font-size: 18px"></i>
                                    <span class="nav-text">Cursos</span>
                                </a>
                            </li>
                            <li id="NavObj" runat="server">
                                <a class="sidenav-item-link" href="GestObj.aspx">
                                    <i class="fa-solid fa-graduation-cap" style="font-size: 18px"></i>
                                    <span class="nav-text">Objetivos</span>
                                </a>
                            </li>
                            <li id="NavEnt" runat="server">
                                <a class="sidenav-item-link" href="GestEmp.aspx">
                                    <i class="fa-solid fa-building" style="font-size: 18px"></i>
                                    <span class="nav-text">Entidades</span>
                                </a>
                            </li>

                            <li id="NavProf" runat="server">
                                <a class="sidenav-item-link" href="GestProf.aspx">
                                    <i class="fa-solid fa-people-group" style="font-size: 18px"></i>
                                    <span class="nav-text">Professores</span>
                                </a>
                            </li>

                            <li id="NavTut" runat="server">
                                <a class="sidenav-item-link" href="GestTutor.aspx">
                                    <i class="fa-solid fa-people-group" style="font-size: 18px"></i>
                                    <span class="nav-text">Tutores</span>
                                </a>
                            </li>
                            <li id="NavAdm" runat="server">
                                <a class="sidenav-item-link" href="Administradores.aspx">
                                    <i class="fa-solid fa-people-group" style="font-size: 18px"></i>
                                    <span class="nav-text">Administradores</span>
                                </a>
                            </li>
                        </ul>
                    </div>

                    <div class="sidebar-footer">
                        <div class="sidebar-footer-content">
                            <ul>
                                <li style="width: 100% !important">
                                    <asp:LinkButton ID="LinkButton3" class="dropdown-link-item" runat="server" OnClick="btn_logout_Click">
                                    <i class="mdi mdi-logout"></i> 
                                    Log Out 
                                    </asp:LinkButton>
                                </li>

                            </ul>
                        </div>
                    </div>
                </div>
            </aside>



            <!-- ====================================
      ——— PAGE WRAPPER
      ===================================== -->
            <div class="page-wrapper">

                <!-- Header -->
                <header class="main-header" id="header">
                    <nav class="navbar navbar-expand-lg navbar-light" id="navbar">
                        <!-- Sidebar toggle button -->
                        <div id="sidebar-toggler" style="display: flex; justify-content: center; align-items: center" class="sidebar-toggle">
                            <span class="sr-only">Toggle navigation</span>
                        </div>

                        <span class="page-title">Sumários</span>

                        <div class="navbar-right ">

                            <ul class="nav navbar-nav">
                                <!-- User Account -->
                                <li class="dropdown user-menu">
                                    <button class="dropdown-toggle nav-link" data-toggle="dropdown">
                                        <img src="images/user/icon-user 40x40.png" class="user-image rounded-circle" alt="User Image" />
                                        <span id="NomeUser" class="d-none d-lg-inline-block" runat="server"></span>
                                    </button>
                                    <ul class="dropdown-menu dropdown-menu-right">
                                        <li id="Div_infCargo" runat="server">
                                            <a class="dropdown-link-item">
                                                <span id="inf_cargo" runat="server" class="nav-text"></span>
                                            </a>
                                        </li>
                                        <li id="Div_infDirecao" runat="server">
                                            <a class="dropdown-link-item">
                                                <span class="nav-text">Diretor de Curso</span>
                                            </a>
                                        </li>
                                        <li id="Div_infTurma" runat="server">
                                            <a class="dropdown-link-item">
                                                <span id="inf_turma" runat="server" class="nav-text"></span>
                                            </a>
                                        </li>
                                        <li id="Div_infCurso" runat="server">
                                            <a class="dropdown-link-item">
                                                <span id="inf_curso" runat="server" class="nav-text"></span>
                                            </a>
                                        </li>
                                        <li id="Div_infEnt" runat="server">
                                            <a class="dropdown-link-item">
                                                <span id="inf_entidade" runat="server" class="nav-text"></span>
                                            </a>
                                        </li>
                                        <li id="Div_infCT" runat="server">
                                            <a class="dropdown-link-item">
                                                <span id="inf_cargoT" runat="server" class="nav-text"></span>
                                            </a>
                                        </li>
                                        <br />
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </nav>


                </header>

                <!-- ====================================
        ——— CONTENT WRAPPER
        ===================================== -->
                <div class="content-wrapper">
                    <div class="content">
                        <!-- ====================================
  ——— EMAIL WRAPPER
	===================================== -->
                        <div class="email-wrapper rounded border bg-white">
                            <div class="row no-gutters justify-content-center">
                                <div class="col-lg-4 col-xl-3 col-xxl-2">
                                    <div class="email-left-column email-options p-4 p-xl-5">
                                        <p class="text-dark font-weight-medium">Opções</p>
                                        <div id="optSum">
                                            <ul>
                                                <li class="mt-4">
                                                    <button type="button" id="btnCriarSum" onserverclick="Criar" runat="server">
                                                        <i class="mdi mdi-checkbox-blank-circle-outline text-success mr-3"></i>
                                                        Adicionar
                                                    </button>
                                                </li>
                                                <li class="mt-4">
                                                    <button type="button" id="btnEditarSum" onserverclick="Editar" runat="server">
                                                        <i class="mdi mdi-checkbox-blank-circle-outline text-warning mr-3"></i>
                                                        Editar
                                                    </button>
                                                </li>
                                                <li class="mt-4">
                                                    <button type="button" id="btnEliminarSum" onserverclick="Eliminar" runat="server">
                                                        <i class="mdi mdi-checkbox-blank-circle-outline text-danger mr-3"></i>
                                                        Eliminar
                                                    </button>
                                                </li>
                                                <li class="mt-4" >
                                                    <asp:Label ID="Label1" class="btn btn-outline" style="cursor: default !important" runat="server" Text="" Visible="false"></asp:Label>

                                                </li>
                                            </ul>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-lg-8 col-xl-9 col-xxl-10">
                                    <div class="email-right-column p-4 p-xl-5">
                                        <!-- Email Right Header -->
                                        <div class="email-right-header">

                                            <!-- FORM MODAL -->
                                            <div class="modal" id="formSum" runat="server" visible="false">
                                                <div class="modal-dialog" role="document">
                                                    <iv class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="exampleModalFormTitle" runat="server" style="font-weight: bold;"></h5>
                                                            <button type="button" id="spanFechar" class="close" runat="server" onserverclick="spanFechar_Click">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body" style="height: 400px; overflow-y: auto;">
                                                            <div id="Alert" class="alert alert-secondary alert-icon" role="alert" visible="false" runat="server">
                                                                <i class="mdi mdi-alert"></i><span id="alerMessage" runat="server"></span>
                                                            </div>
                                                            <div class="form-group" id="DivAluno" runat="server">
                                                                <label for="slc_aluno">Aluno</label>
                                                                <asp:DropDownList ID="slc_aluno" CssClass="form-control" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_numhora">Data do sumário</label>
                                                                <%--<input type="text" class="form-control" id="" placeholder="Data de sumario" enableviewstate="true" runat="server" />--%>
                                                                <asp:TextBox ID="txt_dataSum" class="form-control" runat="server" TextMode="Date"> </asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_sumario">Descrição do sumário</label>
                                                                <asp:TextBox ID="txt_sumario" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="ddl_Tarefas">Tarefas Associadas</label>
                                                                <asp:TextBox ID="TextBox1" runat="server" Style="display: none"></asp:TextBox>
                                                                <asp:TextBox ID="TextBox2" runat="server" Style="display: none" AutoPostBack="true" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
                                                                <asp:DropDownList ID="ddl_Tarefas" CssClass="form-control" runat="server" Style="height: 60px !important" ClientIDMode="Static" Multiple="True"></asp:DropDownList>
                                                                <div style="display: flex; justify-content: space-between; margin-top: 5px">
                                                                    <label for="ddl_Tarefas" style="visibility: hidden">Tarefas Associadas</label>
                                                                    <asp:Button ID="Button1" runat="server" class="btn btn-secondary" Style="height: 20px; font-size: 12px; padding: 4px; line-height: 5px;" OnClientClick="ShowValue()" Text="Guardar Tarefas" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div style="display: inline-block">
                                                                    <label for="txt_numhora">Horas feitas</label>
                                                                    <input type="text" class="form-control" id="txt_numHora" placeholder="Número de horas feitas" pattern="^[1-8]$" title="insira de 1 a 8 horas." enableviewstate="true" runat="server" />
                                                                </div>
                                                                <div style="display: inline-block">
                                                                    <label for="txt_status">Estado do sumário</label>
                                                                    <%--<input type="text" class="form-control" id="txt_status" enableviewstate="true" value="Pendente" readonly="readonly" runat="server" />--%>
                                                                    <asp:DropDownList ID="ddl_Status" CssClass="form-control" runat="server" Enabled="false">
                                                                        <asp:ListItem Text="Pendente" Value="Pendente"></asp:ListItem>
                                                                        <asp:ListItem Text="Validado" Value="Validado"></asp:ListItem>
                                                                        <asp:ListItem Text="Rejeitado" Value="Rejeitado"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" id="btn_fechar" class="btn btn-danger btn-pill" runat="server" onserverclick="Fechar">Cancelar</button>
                                                            <asp:Button ID="btn_enviar" class="btn btn-primary btn-pill" runat="server" OnClick="Comandos" Text="Criar Sumário" ToolTip="Guarde as tarefas antes de submeter" />
                                                        </div>
                                                </div>
                                            </div>

                                            <!-- DELETE MODAL -->
                                            <div class="modal" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
                                                aria-hidden="true" runat="server" visible="false">
                                                <div class="modal-dialog" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="exampleModalLabel" runat="server">Nenhum sumário selecionado</h5>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">
                                                            <span id="textoCancelar" runat="server"></span>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button ID="btnCancelar" class="btn btn-danger btn-pill" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                                                            <asp:Button ID="btnDeletar" class="btn btn-primary btn-pill" runat="server" Text="Eliminar" OnClick="Comandos" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="head-left-options">
                                                <asp:LinkButton ID="LinkButton1" class="btn btn-outline-primary" runat="server" OnClick="LinkButton1_Click">
                                                    <i class="mdi mdi-refresh"></i>
                                                    Atualizar
                                                </asp:LinkButton>
                                            </div>

                                            <div class="head-right-options">

                                                <div class="btn-group" role="group" aria-label="Basic example">
                                                    <asp:DropDownList ID="ddl_entidade" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_entidade_SelectedIndexChanged1"></asp:DropDownList>
                                                </div>
                                                <div class="btn-group" role="group" aria-label="Basic example" style="margin-right: 200px">
                                                    <asp:DropDownList ID="ddl_aluno" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_aluno_SelectedIndexChanged1" Visible="false"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <%--TABULATOR--%>
                                        <div class="border border-top-0 rounded table-responsive email-list" style="height: 400px">
                                            <!-- <table class="table mb-0 table-email"> </table> -->
                                            <div id="tableTarefas" style="display: none"></div>
                                            <div id="tableSumarios"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <!-- Footer -->


        </div>



        <script>

            var stat = document.getElementById('<%= labelStats.ClientID %>');
            var cod = document.getElementById('<%= labelCod.ClientID %>');
            var bt = document.getElementById('<%= TextBox1.ClientID %>');
            var enviar = document.getElementById('<%= TextBox2.ClientID %>');

            var selectedTasks = "";

            var table2 = new Tabulator("#tableSumarios", {
                selectable: 1,
                placeholder: "Sem dados",
                height: "100%",
                layout: "fitDataStretch",
                columns: [
                    { title: "Cód.", field: "codigo", width: 50, resizable: false, headerFilter: "number" },
                    { title: "Descrição", field: "descricao_sumario", width: 325, resizable: false, headerFilter: "input" },
                    { title: "Horas", field: "horas_sumario", width: 50, resizable: false, headerFilter: "number" },
                    { title: "Data", field: "data_sumario", width: 100, resizable: false, headerFilter: "input" },
                    { title: "Status", field: "status_sumario", width: 150, resizable: false, headerFilter: "input" },
                    { title: "Aluno", field: "aluno", width: 300, resizable: false, headerFilter: "input" },
                    { title: "FCT", field: "id_fct", width: 50, resizable: false, headerFilter: "number" },

                ],

            });

            table2.on("rowSelectionChanged", function (data, rows) {
                //rows - array of row components for the selected rows in order of selection
                //data - array of data objects for the selected rows in order of selection


                var controle = document.getElementById("HiddenField1");


                if (data.length == 1) {
                    //obtem os dados do primeiro e único elemento (linha) selecionado
                    var ab = rows[0].getData().codigo;
                    controle.value = ab;

                    //pra conferir os dados
                    //alert("LINHAS: 1, AB =" + ab + "!");
                }
                if (data.length != 1) {
                    controle.value = 0;
                }
            });

            var cat_tabledata2 = [
                <asp:Repeater ID="rptItems2" runat="server">
                    <ItemTemplate>
                        {codigo: '<%#DataBinder.Eval(Container.DataItem, "id_sumario") %>',
                    descricao_sumario: '<%#DataBinder.Eval(Container.DataItem, "descricao_sumario") %>', 
                    horas_sumario: '<%#DataBinder.Eval(Container.DataItem, "horas_sumario") %>', 
                    status_sumario: '<%#DataBinder.Eval(Container.DataItem, "status_sumario") %>',
                    data_sumario: '<%#DataBinder.Eval(Container.DataItem, "data_sumario") %>',
                    aluno: '<%#DataBinder.Eval(Container.DataItem, "nome_aluno") %>',
                    id_fct: '<%#DataBinder.Eval(Container.DataItem, "id_fct") %>'},
                    </ItemTemplate>
                </asp:Repeater >];


            table2.on("tableBuilt", function () {
                table2.setData(cat_tabledata2);
            });


            $(document).ready(function () {
                $('#ddl_Tarefas').select2({ maximumSelectionLength: 3 });
                $('#ddl_entidade').select2();
                $('#ddl_aluno').select2();
                $('#slc_aluno').select2();
            });

            function ShowValue() {

                bt.value = $('#ddl_Tarefas').select2("val");
                /*alert(bt.value);*/
                bt.innerText = $('#ddl_Tarefas').select2("val");
                enviar.value = "enviado";
                enviar.innerText = "enviado";
                //alert("o valor2 é:" + $('#ddl_Tarefas').select2("val") + "!");

            }


            function SlcTar1(a) {
                //alert("Valores: " + a);
                $('#ddl_Tarefas').val([a]);
            }

            function SlcTar2(a, b) {
                //alert("Valores: " + b + "," + a );
                $('#ddl_Tarefas').val([a, b]);
            }
            function SlcTar3(a, b, c) {
                //alert("Valores: " + a + "," + b + "," + c);
                $('#ddl_Tarefas').val([a, b, c]);
            }

        </script>

        <asp:TextBox ID="oldTar" runat="server" Visible="false"></asp:TextBox>

        <script src="https://unpkg.com/hotkeys-js/dist/hotkeys.min.js"></script>
        <script src="plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
        <script src="plugins/simplebar/simplebar.min.js"></script>


        <script src="js/mono.js"></script>
        <script src="js/chart.js"></script>
        <script src="js/map.js"></script>
        <script src="js/custom.js"></script>

        <style>
            .tabulator {
                background-color: white;
            }

                .tabulator .tabulator-header {
                    width: auto;
                }
            /* .tabulator .tabulator-tableholder{overflow-x: hidden;} */
            /* .tabulator .tabulator-row-even {} */
            .select2-container {
                z-index: 99999;
            }


                .select2-container .select2-selection--multiple {
                    min-height: 60px !important;
                }

                .select2-container .select2-selection--single .select2-selection__rendered {
                    display: block;
                    width: 100%;
                    height: calc(1.5em + 1.12rem + 2px);
                    padding: 0.56rem 1rem;
                    font-size: 0.9375rem;
                    font-weight: 400;
                    line-height: 1.5;
                    color: #495057;
                    background-color: #ffffff;
                    background-clip: padding-box;
                    border: 1px solid #e5e9f2;
                    border-radius: 0.25rem;
                }

            .select2-container--default .select2-selection--single {
                border: none
            }

            .select2-dropdown {
                border: 1px solid #e5e9f2
            }

            .select2-search--dropdown {
                padding: 8px 4px 4px 4px;
            }

            .select2-container--default .select2-search--dropdown .select2-search__field {
                border: 1px solid #e5e9f2;
            }

            .tabulator-header-filter input {
                height: 20px
            }

            .email-right-column .email-right-header {
                margin-bottom: auto !important
            }
        </style>

    </form>
</body>
</html>
