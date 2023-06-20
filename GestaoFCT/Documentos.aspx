<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Documentos.aspx.cs" Inherits="GestaoFCT.Documentos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Documentos</title>

    <!-- GOOGLE FONTS -->
    <link href="https://fonts.googleapis.com/css?family=Karla:400,700|Roboto" rel="stylesheet" />
    <link href="plugins/material/css/materialdesignicons.min.css" rel="stylesheet" />
    <link href="plugins/simplebar/simplebar.css" rel="stylesheet" />
    <script src="https://kit.fontawesome.com/89865b6117.js" crossorigin="anonymous"></script>

    <!-- PLUGINS CSS STYLE -->
    <link href="plugins/nprogress/nprogress.css" rel="stylesheet" />

    <!-- MONO CSS -->
    <link rel="stylesheet" href="css/style.css" />

    <!-- TABULATOR -->
    <link href="tabulator-master/dist/css/tabulator.css" rel="stylesheet" />
    <script type="text/javascript" src="tabulator-master/dist/js/tabulator.js"></script>

    <!-- FAVICON -->
    <link href="images/favicon.png" rel="shortcut icon" />

    <!-- jQuery -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>


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
            <asp:SqlDataSource ID="DocSQLData" runat="server" ConnectionString="<%$ ConnectionStrings:FCTConnectionString %>"></asp:SqlDataSource>


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

                            <li id="NavSum" runat="server">
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

                            <li id="NavDoc" class="active" runat="server">
                                <a class="sidenav-item-link" href="Documentos.aspx">
                                    <i class="mdi mdi-file-multiple"></i>
                                    <span class="nav-text">Documentos</span>
                                </a>
                            </li>


                            <li id="SecGest" class="section-title" runat="server">Gestão</li>

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

                            <li id="Li1" class="section-title" runat="server">Conta</li>

                            <li id="Li2" runat="server">
                                <asp:LinkButton ID="LinkButton1" class="dropdown-link-item" runat="server" OnClick="btn_logout_Click">
                                    <i class="mdi mdi-logout"></i> 
                                    Log Out 
                                </asp:LinkButton>
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

                        <span class="page-title">Documentos</span>

                        <div class="navbar-right ">



                            <ul class="nav navbar-nav">
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
                        <div class="card card-default">

                            <div class="card-body px-3 px-md-5" style="padding-bottom: 5px; padding-top: 30px">
                                <div class="row">


                                    <div class="col-lg-6 col-xl-4">
                                        <div class="card card-default p-4">
                                            <div class="media text-secondary">
                                                <img src="images/CadernetaFoto.png" class="mr-3 img-fluid rounded" alt="Avatar Image" style="max-width: 100px; max-height: 100px;" />

                                                <div class="media-body">
                                                    <h5 class="mt-0 mb-2 text-dark">Caderneta de Estágio</h5>
                                                    <button type="button" class="mb-1 btn btn-outline-primary">
                                                        <i class=" mdi mdi-star-outline mr-1"></i>
                                                        Gerar Caderneta
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-xl-4">
                                        <div class="card card-default p-4">
                                            <div class="media text-secondary">
                                                <img src="images/ContratoFoto.png" class="mr-3 img-fluid rounded" alt="Avatar Image" style="max-width: 100px; max-height: 100px;">

                                                <div class="media-body">
                                                    <h5 class="mt-0 mb-2 text-dark">Contrato de Formação</h5>
                                                    <button type="button" class="mb-1 btn btn-outline-primary">
                                                        <i class=" mdi mdi-star-outline mr-1"></i>
                                                        Gerar Contrato
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-xl-4">
                                        <div class="card card-default p-4">
                                            <div class="media text-secondary">
                                                <img src="images/ProtocoloFoto.png" class="mr-3 img-fluid rounded" alt="Avatar Image" style="max-width: 100px; max-height: 100px;" />

                                                <div class="media-body">
                                                    <h5 class="mt-0 mb-2 text-dark">Protocolo</h5>
                                                    <button type="button" class="mb-1 btn btn-outline-primary">
                                                        <i class=" mdi mdi-star-outline mr-1"></i>
                                                        Gerar Protocolo
                                                    </button>

                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="card-header align-items-center px-3 px-md-5" style="padding-top: 0px">
                                <h2>Documentos</h2>

                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modal-add-contact">
                                    Add Contact
                                </button>
                            </div>

                            <div class="boxTableDocs">
                                <div id="tableDocs"></div>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- footer -->
            </div>
        </div>


        <script>

            var stat = document.getElementById('<%= labelStats.ClientID %>');
            var cod = document.getElementById('<%= labelCod.ClientID %>');

            var table1 = new Tabulator("#tableDocs", {
                selectable: 1,
                placeholder: "Sem dados",
                height: "400px",
                layout: "fitDataStretch",
                columns: [
                    { title: "FCT", field: "codigo", width: 50, resizable: false, headerFilter: "number" },
                    { title: "idA", field: "id_aluno", width: 50, resizable: false, visible: false },
                    { title: "Aluno", field: "nome_aluno", width: 230, resizable: false, headerFilter: "input" },
                    { title: "idC", field: "id_curso", width: 50, resizable: false, visible: false },
                    { title: "idP", field: "id_professor", width: 50, resizable: false, visible: false },
                    { title: "Professor", field: "nome_prof", width: 230, resizable: false, headerFilter: "input" },
                    { title: "idE", field: "id_entidade", width: 50, resizable: false, visible: false },
                    { title: "Entidade", field: "nome_entidade", width: 200, resizable: false, headerFilter: "input" },
                    { title: "idT", field: "id_tutor", width: 50, resizable: false, visible: false },
                    { title: "Tutor", field: "nome_tutor", width: 200, resizable: false, headerFilter: "input" },
                    { title: "Ano", field: "ano_fct", width: 150, resizable: false, headerFilter: "input" },
                    { title: "Horas", field: "num_horas", width: 50, resizable: false, headerFilter: "input" },
                    { title: "Hr Diárias", field: "hrdiaria", width: 50, resizable: false, headerFilter: "input" },
                    { title: "Inicio", field: "inicio_fct", width: 100, resizable: false, headerFilter: "input" },
                    { title: "Fim", field: "fim_fct", width: 100, resizable: false, headerFilter: "input" },
                ],

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
                        {
                            codigo: '<%# DataBinder.Eval(Container.DataItem, "id_fct") %>',
			                id_aluno: '<%# DataBinder.Eval(Container.DataItem, "id_aluno") %>',
			                nome_aluno: '<%# DataBinder.Eval(Container.DataItem, "nome_aluno") %>',
			                id_professor: '<%# DataBinder.Eval(Container.DataItem, "id_professor") %>',
			                nome_prof: '<%# DataBinder.Eval(Container.DataItem, "nome_prof") %>',
			                id_entidade: '<%# DataBinder.Eval(Container.DataItem, "id_entidade") %>',
			                nome_entidade: '<%# DataBinder.Eval(Container.DataItem, "nome_entidade") %>',
			                id_tutor: '<%# DataBinder.Eval(Container.DataItem, "id_tutor") %>',
			                nome_tutor: '<%# DataBinder.Eval(Container.DataItem, "nome_tutor") %>',
			                num_horas: '<%# DataBinder.Eval(Container.DataItem, "num_horas") %>',
                            ano_fct: '<%# DataBinder.Eval(Container.DataItem, "ano_fct") %>',
                            hrdiaria: '<%# DataBinder.Eval(Container.DataItem, "horasDiarias") %>',
                            inicio_fct: '<%# DataBinder.Eval(Container.DataItem, "inicio_fct") %>',
                        fim_fct: '<%# DataBinder.Eval(Container.DataItem, "fim_fct") %>'
                        },
                    </ItemTemplate>
                </asp:Repeater >];



            table1.on("tableBuilt", function () {
                table1.setData(cat_tabledata1);
            });



            $(document).ready(function () {
                $('#ddl_entidade').select2();
                $('#ddl_TarEntidade').select2();
                $('#ddl_TarTutor').select2();
            });

        </script>



        <script src="plugins/jquery/jquery.min.js"></script>
        <script src="plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
        <script src="plugins/simplebar/simplebar.min.js"></script>
        <script src="https://unpkg.com/hotkeys-js/dist/hotkeys.min.js"></script>


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

        <!--  -->

    </form>
</body>
</html>
