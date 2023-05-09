<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sumarios.aspx.cs" Inherits="GestaoFCT.Sumarios" %>


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

    <title>Mono - Responsive Admin & Dashboard Template</title>

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
    <link href="images/favicon.png" rel="shortcut icon" />

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


<body class="navbar-fixed sidebar-fixed " id="body">
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
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:SqlDataSource ID="SumSQLData" runat="server" ConnectionString="<%$ ConnectionStrings:FCTConnectionString %>"></asp:SqlDataSource>



            <!-- ====================================
          ——— LEFT SIDEBAR WITH OUT FOOTER
        ===================================== -->

            <aside class="left-sidebar sidebar-dark" id="left-sidebar">
                <div id="sidebar" class="sidebar sidebar-with-footer">
                    <!-- Aplication Brand -->
                    <div class="app-brand">
                        <a href="/index.html">
                            <img src="images/logo.png" alt="Mono">
                            <span class="brand-name">GestFCT</span>
                        </a>
                    </div>
                    <!-- begin sidebar scrollbar -->
                    <div class="sidebar-left" data-simplebar="" style="height: 100%;">
                        <!-- sidebar menu -->
                        <ul class="nav sidebar-inner" id="sidebar-menu">

                            <li class="active">
                                <a class="sidenav-item-link" href="Tarefas.aspx">
                                    <i class="mdi mdi-calendar-check"></i>
                                    <span class="nav-text">Tarefas</span>
                                </a>
                            </li>

                            <li>
                                <a class="sidenav-item-link" href="chat.html">
                                    <i class="mdi mdi-file-multiple"></i>
                                    <span class="nav-text">Documentos</span>
                                </a>
                            </li>


                            <li class="section-title">Gestão
                            </li>

                            <li>
                                <a class="sidenav-item-link" href="GestFCT.aspx">
                                    <i class="fa-solid fa-address-card" style="font-size: 18px"></i>
                                    <span class="nav-text">Gestão da FCT</span>
                                </a>
                            </li>

                            <li>
                                <a class="sidenav-item-link" href="GestAluno.aspx">
                                    <i class="fa-solid fa-users" style="font-size: 18px"></i>
                                    <span class="nav-text">Alunos</span>
                                </a>
                            </li>

                            <li>
                                <a class="sidenav-item-link" href="GestEnc.aspx">
                                    <i class="mdi mdi-account-group"></i>
                                    <span class="nav-text">Enc. Educação</span>
                                </a>
                            </li>

                            <li>
                                <a class="sidenav-item-link" href="GestCursos.aspx">
                                    <i class="fa-solid fa-graduation-cap" style="font-size: 18px"></i>
                                    <span class="nav-text">Cursos</span>
                                </a>
                            </li>

                            <li>
                                <a class="sidenav-item-link" href="GestEmp.aspx">
                                    <i class="fa-solid fa-building" style="font-size: 18px"></i>
                                    <span class="nav-text">Entidades</span>
                                </a>
                            </li>

                            <li>
                                <a class="sidenav-item-link" href="GestProf.aspx">
                                    <i class="fa-solid fa-people-group" style="font-size: 18px"></i>
                                    <span class="nav-text">Professores</span>
                                </a>
                            </li>

                            <li>
                                <a class="sidenav-item-link" href="GestTutor.aspx">
                                    <i class="fa-solid fa-people-group" style="font-size: 18px"></i>
                                    <span class="nav-text">Tutores</span>
                                </a>
                            </li>

                        </ul>
                    </div>

                    <div class="sidebar-footer">
                        <div class="sidebar-footer-content">
                            <ul class="d-flex">
                                <li>
                                    <a href="user-account-settings.html" data-toggle="tooltip" title="Profile settings"><i class="mdi mdi-settings"></i></a></li>
                                <li>
                                    <a href="#" data-toggle="tooltip" title="No chat messages"><i class="mdi mdi-chat-processing"></i></a>
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
                        <button id="sidebar-toggler" class="sidebar-toggle">
                            <span class="sr-only">Toggle navigation</span>
                        </button>

                        <span class="page-title">Tarefas</span>

                        <div class="navbar-right ">

                            <!-- search form -->
                            <div class="search-form">
                                <form action="index.html" method="get">
                                    <div class="input-group input-group-sm" id="input-group-search">
                                        <input type="text" autocomplete="off" name="query" id="search-input" class="form-control" placeholder="Search..." />
                                        <div class="input-group-append">
                                            <button class="btn" type="button">/</button>
                                        </div>
                                    </div>
                                </form>
                                <ul class="dropdown-menu dropdown-menu-search">

                                    <li class="nav-item">
                                        <a class="nav-link" href="index.html">Morbi leo risus</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" href="index.html">Dapibus ac facilisis in</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" href="index.html">Porta ac consectetur ac</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" href="index.html">Vestibulum at eros</a>
                                    </li>

                                </ul>

                            </div>

                            <ul class="nav navbar-nav">
                                <!-- Offcanvas -->

                                <!-- User Account -->
                                <li class="dropdown user-menu">
                                    <button class="dropdown-toggle nav-link" data-toggle="dropdown">
                                        <img src="images/user/user-xs-01.jpg" class="user-image rounded-circle" alt="User Image" />
                                        <span id="NomeUser" class="d-none d-lg-inline-block" runat="server"></span>
                                    </button>
                                    <ul class="dropdown-menu dropdown-menu-right">
                                        <li>
                                            <a class="dropdown-link-item" href="user-profile.html">
                                                <i class="mdi mdi-account-outline"></i>
                                                <span class="nav-text">My Profile</span>
                                            </a>
                                        </li>
                                        <li>
                                            <a class="dropdown-link-item" href="email-inbox.html">
                                                <i class="mdi mdi-email-outline"></i>
                                                <span class="nav-text">Message</span>
                                                <span class="badge badge-pill badge-primary">24</span>
                                            </a>
                                        </li>
                                        <li>
                                            <a class="dropdown-link-item" href="user-activities.html">
                                                <i class="mdi mdi-diamond-stone"></i>
                                                <span class="nav-text">Activitise</span></a>
                                        </li>
                                        <li>
                                            <a class="dropdown-link-item" href="user-account-settings.html">
                                                <i class="mdi mdi-settings"></i>
                                                <span class="nav-text">Account Setting</span>
                                            </a>
                                        </li>

                                        <li class="dropdown-footer">
                                            <%--<a class="dropdown-link-item" href="sign-in.html"> <i class="mdi mdi-logout"></i> Log Out </a>--%>
                                            <asp:LinkButton ID="btn_logout" class="dropdown-link-item" runat="server" OnClick="btn_logout_Click">
                                              <i class="mdi mdi-logout"></i> 
                                              Log Out 
                                            </asp:LinkButton>
                                        </li>
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
                                                    <button type="button" id="btnCriarSum" onserverclick="CriarSum" runat="server">
                                                        <i class="mdi mdi-checkbox-blank-circle-outline text-success mr-3"></i>
                                                        Adicionar
                                                    </button>
                                                </li>
                                                <li class="mt-4">
                                                    <button type="button" id="btnEditarSum" onserverclick="EditarSum" runat="server">
                                                        <i class="mdi mdi-checkbox-blank-circle-outline text-warning mr-3"></i>
                                                        Editar
                                                    </button>
                                                </li>
                                                <li class="mt-4">
                                                    <button type="button" id="btnEliminarSum" onserverclick="EliminarSum" runat="server">
                                                        <i class="mdi mdi-checkbox-blank-circle-outline text-danger mr-3"></i>
                                                        Eliminar
                                                    </button>
                                                </li>

                                            </ul>
                                        </div>

                                        <div id="optTar" style="display: none">
                                            <ul>
                                                <li class="mt-4">
                                                    <button type="button" id="btnCriarTar" onserverclick="CriarTar" runat="server">
                                                        <i class="mdi mdi-checkbox-blank-circle-outline text-success mr-3"></i>
                                                        Adicionar
                                                    </button>
                                                </li>
                                                <li class="mt-4">
                                                    <button type="button" id="btnEditarTar" onserverclick="EditarTar" runat="server">
                                                        <i class="mdi mdi-checkbox-blank-circle-outline text-warning mr-3"></i>
                                                        Editar
                                                    </button>
                                                </li>
                                                <li class="mt-4">
                                                    <button type="button" id="btnEliminarTar" onserverclick="EliminarTar" runat="server">
                                                        <i class="mdi mdi-checkbox-blank-circle-outline text-danger mr-3"></i>
                                                        Eliminar
                                                    </button>
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

                                            <div class="modal" id="formSum" tabindex="-1" role="dialog" aria-labelledby="exampleModalFormTitle"
                                                aria-hidden="true" runat="server" visible="false">
                                                <div class="modal-dialog" role="document">
                                                    <iv class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="exampleModalFormTitle" runat="server" style="font-weight: bold;"></h5>
                                                            <button type="button" id="spanFechar" class="close" runat="server" onserverclick="spanFechar_Click">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body" style="height: 400px; overflow-y: auto;">
                                                            <div class="form-group">
                                                                <label for="ddl_Tarefas">Tarefas Associadas</label>
                                                                <asp:DropDownList ID="ddl_Tarefas" CssClass="form-control" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_sumario">Descrição do sumário</label>
                                                                <asp:TextBox ID="txt_sumario" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <div style="display: inline-block">
                                                                    <label for="txt_numhora">Horas feitas</label>
                                                                    <input type="text" class="form-control" id="txt_numHora" placeholder="Número de horas feitas" enableviewstate="true" runat="server" />
                                                                </div>
                                                                <div style="display: inline-block">
                                                                    <label for="txt_status">Estado do sumário</label>
                                                                    <input type="text" class="form-control" id="txt_status" enableviewstate="true" value="Pendente" readonly="readonly" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" id="btn_fechar" class="btn btn-danger btn-pill" runat="server" onserverclick="Fechar">Cancelar</button>
                                                            <asp:Button ID="btn_enviar" class="btn btn-primary btn-pill" runat="server" Text="Criar Aluno" OnClick="Comandos" />
                                                        </div>
                                                </div>
                                            </div>

                                            <div class="modal" id="formTar" role="dialog" aria-labelledby="exampleModalLabel"
                                                aria-hidden="true" runat="server" visible="false">
                                                <div class="modal-dialog" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="TarTitle">Criar Tarefa</h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <span id="Span1" runat="server"></span>
                                                            <div class="form-group">
                                                                <label for="txt_tarefa">Descrição da tarefa</label>
                                                                <input type="text" class="form-control" id="txt_tarefa" enableviewstate="true" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button ID="Button1" class="btn btn-danger btn-pill" runat="server" Text="Cancelar" OnClick="Fechar" />
                                                            <asp:Button ID="Button2" class="btn btn-primary btn-pill" runat="server" Text="Eliminar" OnClick="Comandos" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <!-- DELETE MODAL -->
                                            <div class="modal" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
                                                aria-hidden="true" runat="server" visible="false">
                                                <div class="modal-dialog" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="exampleModalLabel">Eliminar registo do aluno</h5>
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
                                                <button type="button" class="btn btn-outline" onclick="tblSumarios()">
                                                    Sumários
                                                </button>
                                                <button type="button" class="btn btn-outline" onclick="tblTarefas()">
                                                    Tarefas
                                                </button>
                                            </div>

                                            <div class="head-right-options">
                                                <div class="btn-group" role="group" aria-label="Basic example">
                                                    <asp:DropDownList ID="ddl_entidade" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_entidade_SelectedIndexChanged1"></asp:DropDownList>
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

            var table1 = new Tabulator("#tableTarefas", {
                selectable: 1,
                placeholder: "Sem dados",
                height: "100%",
                layout: "fitDataStretch",
                columns: [
                    { title: "Cód.", field: "codigo", width: 50, resizable: false, headerFilter: "number" },
                    { title: "Descrição", field: "descricao_tarefa", width: 230, resizable: false, headerFilter: "input" },
                    { title: "idE", field: "id_entidade", width: 50, resizable: false, headerFilter: "number" },
                    { title: "Entidade", field: "nome_entidade", width: 230, resizable: false, headerFilter: "input" },
                    { title: "idT", field: "id_tutor", width: 50, resizable: false, headerFilter: "number" },
                    { title: "Tutor", field: "nome_tutor", width: 230, resizable: false, headerFilter: "input" },

                ],

            });

            var table2 = new Tabulator("#tableSumarios", {
                selectable: 1,
                placeholder: "Sem dados",
                height: "100%",
                layout: "fitDataStretch",
                columns: [
                    { title: "Cód.", field: "codigo", width: 50, resizable: false, headerFilter: "number" },
                    { title: "Tarefas", field: "tarefas_sumario", width: 325, resizable: false, headerFilter: "input" },
                    { title: "Descrição", field: "descricao_sumario", width: 325, resizable: false, headerFilter: "input" },
                    { title: "Horas", field: "horas_sumario", width: 50, resizable: false, headerFilter: "number" },
                    { title: "Status", field: "status_sumario", width: 150, resizable: false, headerFilter: "input" },
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

            table1.on("rowSelectionChanged", function (data, rows) {
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


            var cat_tabledata1 = [
                <asp:Repeater ID="rptItems" runat="server">
                    <ItemTemplate>
                        {codigo: '<%#DataBinder.Eval(Container.DataItem, "id_tarefa") %>',
                    descricao_tarefa: '<%#DataBinder.Eval(Container.DataItem, "descricao_tarefa") %>',
                    id_entidade: '<%#DataBinder.Eval(Container.DataItem, "id_entidade") %>', 
                    nome_entidade: '<%#DataBinder.Eval(Container.DataItem, "nome_entidade") %>', 
                    id_tutor: '<%#DataBinder.Eval(Container.DataItem, "id_tutor") %>',
                    nome_tutor: '<%#DataBinder.Eval(Container.DataItem, "nome_tutor") %>'},

                    </ItemTemplate>
                </asp:Repeater >];

            var cat_tabledata2 = [
                <asp:Repeater ID="rptItems2" runat="server">
                    <ItemTemplate>
                        {codigo: '<%#DataBinder.Eval(Container.DataItem, "id_sumario") %>',
                    descricao_sumario: '<%#DataBinder.Eval(Container.DataItem, "tarefas_sumario") %>',
                    descricao_sumario: '<%#DataBinder.Eval(Container.DataItem, "descricao_sumario") %>', 
                    horas_sumario: '<%#DataBinder.Eval(Container.DataItem, "horas_sumario") %>', 
                    status_sumario: '<%#DataBinder.Eval(Container.DataItem, "status_sumario") %>',
                    id_fct: '<%#DataBinder.Eval(Container.DataItem, "id_fct") %>'},
                    </ItemTemplate>
                </asp:Repeater >];

            table1.on("tableBuilt", function () {
                table1.setData(cat_tabledata1);
            });

            table2.on("tableBuilt", function () {
                table2.setData(cat_tabledata2);
            });


            $(document).ready(function () {
                $('#ddl_entidade').select2();
                $('#ddl_curso').select2();
                $('#ddl_professor').select2();
                $('#ddl_tutor').select2();
            });

            function tblSumarios() {
                document.querySelector('#tableTarefas').style.display = 'none';
                document.querySelector('#optTar').style.display = 'none';
                document.querySelector('#tableSumarios').style.display = 'block';
                document.querySelector('#optSum').style.display = 'block';
            }
            function tblTarefas() {
                document.querySelector('#tableSumarios').style.display = 'none';
                document.querySelector('#optSum').style.display = 'none';
                document.querySelector('#tableTarefas').style.display = 'block';
                document.querySelector('#optTar').style.display = 'block';
            }

        </script>


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
