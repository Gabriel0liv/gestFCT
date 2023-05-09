<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestCursos.aspx.cs" Inherits="GestaoFCT.GestCursos" %>


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

    <title>GestFCT</title>

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
            <asp:SqlDataSource ID="CursoSQLData" runat="server" ConnectionString="<%$ ConnectionStrings:FCTConnectionString %>"></asp:SqlDataSource>



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

                            <li>
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

                            <li class="active">
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

                        <span class="page-title">Gestão de Cursos</span>

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
                                    <div class="email-left-column email-options p-4 ">
                                        <p class="text-dark font-weight-medium">Opções</p>
                                        <ul>
                                            <li class="mt-4">

                                                <button type="button" id="btnCriar" onserverclick="Criar" runat="server">
                                                    <i class="mdi mdi-checkbox-blank-circle-outline text-success mr-3"></i>
                                                    Adicionar
                                                </button>
                                            </li>
                                            <li class="mt-4">
                                                <button type="button" id="btnEditar" onserverclick="Editar" runat="server">
                                                    <i class="mdi mdi-checkbox-blank-circle-outline text-warning mr-3"></i>
                                                    Editar
                                                </button>
                                            </li>
                                            <li class="mt-4">
                                                <button type="button" id="btnEliminar" onserverclick="Eliminar" runat="server">
                                                    <i class="mdi mdi-checkbox-blank-circle-outline text-danger mr-3"></i>
                                                    Eliminar
                                                </button>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-lg-8 col-xl-9 col-xxl-10">
                                    <div class="email-right-column p-4 p-xl-5">
                                        <!-- Email Right Header -->
                                        <div class="email-right-header mb-5">


                                            <!-- FORM MODAL -->

                                            <div class="modal" id="exampleModalForm" tabindex="-1" role="dialog" aria-labelledby="exampleModalFormTitle"
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
                                                                <label for="txt_nome">Nome do curso</label>
                                                                <input type="text" class="form-control" id="txt_nome" placeholder="Insira o nome do curso" enableviewstate="true" runat="server" />
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_CodPost">Ano </label>
                                                                <input type="text" class="form-control" id="txt_ano" placeholder="Insira o ano da turma. Ex: 11º" runat="server" />
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_CodPost">Turma</label>
                                                                <input type="text" class="form-control" id="txt_turma" placeholder="Insira a letra da turma. Ex: S" runat="server" />
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_CodPost">Data de Início</label>
                                                                <input type="text" class="form-control" id="txt_dataIni" placeholder="Insira o ano de inicio do curso. Ex: 2022" runat="server" />
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_CodPost">Data de Finalização</label>
                                                                <input type="text" class="form-control" id="txt_dataFim" placeholder="Insira o ano de finalização do curso. Ex: 2023" runat="server" />
                                                            </div>

                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" id="btn_fechar" class="btn btn-danger btn-pill" runat="server" onserverclick="Fechar">Cancelar</button>

                                                            <%--<button type="button" class="btn btn-primary btn-pill" onclick="<% Enviar(1); %>">Criar entidade</button>--%>
                                                            <asp:Button ID="btn_enviar" class="btn btn-primary btn-pill" runat="server" Text="Criar professor" OnClick="Comandos" />
                                                        </div>
                                                </div>
                                            </div>

                                            <!-- DELETE MODAL -->
                                            <div class="modal" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
                                                aria-hidden="true" runat="server" visible="false">
                                                <div class="modal-dialog" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="exampleModalLabel">Eliminar registo da professor</h5>
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


                                        </div>
                                        <%--TABULATOR--%>
                                        <div class="border border-top-0 rounded table-responsive email-list" style="height: 400px">
                                            <!-- <table class="table mb-0 table-email"> </table> -->
                                            <div id="example-table"></div>
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

            var table = new Tabulator("#example-table", {
                selectable: 1,
                placeholder: "Sem dados",
                height: "100%",
                layout: "fitDataStretch",
                columns: [
                    { title: "Cód.", field: "codigo", width: 100, resizable: false, headerFilter: "number" },
                    { title: "Nome", field: "nome", width: 300, resizable: false, headerFilter: "input" },
                    { title: "Ano", field: "ano", width: 100, resizable: false, headerFilter: "input" },
                    { title: "Turma", field: "turma", width: 100, resizable: false, headerFilter: "input" },
                    { title: "Data de início", field: "dataini", width: 150, resizable: false, headerFilter: "input" },
                    { title: "Data de finalização", field: "datafim", width: 150, resizable: false, headerFilter: "input" },
                ],

            });

            table.on("rowSelectionChanged", function (data, rows) {
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


            var cat_tabledata = [
                <asp:Repeater ID="rptItems" runat="server">
                    <ItemTemplate>
                        {codigo: '<%#DataBinder.Eval(Container.DataItem, "id_curso") %>',
                    nome: '<%#DataBinder.Eval(Container.DataItem, "nome_curso") %>',
                    ano: '<%#DataBinder.Eval(Container.DataItem, "ano_curso") %>',
                    turma: '<%#DataBinder.Eval(Container.DataItem, "turma_curso") %>',
                    dataini: '<%#DataBinder.Eval(Container.DataItem, "dataI_curso") %>', 
                    datafim: '<%#DataBinder.Eval(Container.DataItem, "dataF_curso") %>'},
                    </ItemTemplate>
                </asp:Repeater >];

            table.on("tableBuilt", function () {
                table.setData(cat_tabledata);
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
            .tabulator-header-filter input {
                height: 20px
            }
        </style>

    </form>
</body>
</html>
