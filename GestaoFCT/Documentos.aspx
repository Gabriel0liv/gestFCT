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
    <link href="images/logo GestFCT.png" rel="shortcut icon" />

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
            <asp:HiddenField ID="HiddenField2" runat="server" />
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
                            <img src="images/logo GestFCT.png" style="max-width: 50px" alt="Mono" />
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
                            <ul >
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
                        <button id="sidebar-toggler" class="sidebar-toggle">
                            <span class="sr-only">Toggle navigation</span>
                        </button>

                        <span class="page-title">Documentos</span>

                        <div class="navbar-right ">



                            <ul class="nav navbar-nav">
                                <!-- User Account -->
                                <li class="dropdown user-menu">
                                    <button class="dropdown-toggle nav-link" data-toggle="dropdown">
                                        <img src="images/user/icon-user 40x40.png" class="user-image rounded-circle" alt="User Image" />
                                        <span id="NomeUser" class="d-none d-lg-inline-block" runat="server"></span>
                                    </button>
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
                                                    <asp:LinkButton ID="GeraCaderneta" class="mb-1 btn btn-outline-primary" runat="server" OnClick="GeraCaderneta_Click">
                                                        <i class=" mdi mdi-star-outline mr-1"></i>
                                                        Gerar Caderneta
                                                    </asp:LinkButton>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-xl-4">
                                        <div class="card card-default p-4">
                                            <div class="media text-secondary">
                                                <img src="images/ContratoFoto.png" class="mr-3 img-fluid rounded" alt="Avatar Image" style="max-width: 100px; max-height: 100px;" />

                                                <div class="media-body">
                                                    <h5 class="mt-0 mb-2 text-dark">Contrato de Formação</h5>
                                                    <asp:LinkButton ID="GeraContrato" CssClass="mb-1 btn btn-outline-primary" runat="server" OnClick="GeraContrato_Click">
                                                        <i class=" mdi mdi-star-outline mr-1"></i>
                                                        Gerar Contrato
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="DivProtocolo" class="col-lg-6 col-xl-4" runat="server">
                                        <div class="card card-default p-4">
                                            <div class="media text-secondary">
                                                <img src="images/ProtocoloFoto.png" class="mr-3 img-fluid rounded" alt="Avatar Image" style="max-width: 100px; max-height: 100px;" />

                                                <div class="media-body">
                                                    <h5 class="mt-0 mb-2 text-dark">Protocolo</h5>
                                                    <asp:LinkButton ID="GeraProtocolo" class="mb-1 btn btn-outline-primary" runat="server" OnClick="GeraProtocolo_Click">
                                                        <i class=" mdi mdi-star-outline mr-1"></i>
                                                        Gerar Protocolo
                                                    </asp:LinkButton>

                                                </div>
                                            </div>

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
                                            <h5 class="modal-title" id="exampleModalLabel" runat="server">Nenhum aluno selecionado</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <span id="textoCancelar" runat="server">Selecione um aluno antes de Gerar um documento.</span>
                                        </div>
                                        <div class="modal-footer">
                                            <asp:Button ID="btnCancelar" class="btn btn-danger btn-pill" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-header align-items-center px-3 px-md-5" style="padding-top: 0px">
                                <div class="btnAtualizar">
                                    <asp:LinkButton ID="LinkButton2" class="btn btn-outline" runat="server" OnClick="LinkButton1_Click">Atualizar</asp:LinkButton>
                                </div>

                                <div style="display:flex">
                                    <img src="images/word.png" style="max-height: 30px; max-width: 30px" />
                                    <label class="switch switch-primary switch-pill form-control-label ml-2 mr-2" >
                                        <input id="checkFileFormat" type="checkbox" class="switch-input form-check-input" runat="server" value="on" checked="checked" />
                                        <%--<asp:CheckBox ID="CheckBox1" class="switch-input form-check-input" runat="server" />--%>
                                        <span class="switch-label"></span>
                                        <span class="switch-handle"></span>
                                    </label>
                                    <img src="images/pdf.png" style="max-height: 30px; max-width: 30px" />
                                </div>
                                <button type="button" id="toggleButton" onclick="toggleDivs()" class="btn btn-primary">
                                    Mostrar Entidades
                                </button>
                            </div>

                            <div class="boxTableDocs">
                                <div id="tableDocs" runat="server"></div>
                                <div id="tableProt" style="display: none" runat="server"></div>

                            </div>

                        </div>
                    </div>
                </div>
                <!-- footer -->
            </div>
        </div>


        <script>


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

            var table2 = new Tabulator("#tableProt", {
                selectable: 1,
                placeholder: "Sem dados",
                height: "400px",
                layout: "fitDataStretch",
                columns: [
                    { title: "idC", field: "id_curso", width: 50, resizable: false, visible: false },
                    { title: "Entidade", field: "nome_entidade", width: 300, resizable: false, headerFilter: "input" },
                    { title: "Localidade", field: "loc_entidade", width: 230, resizable: false, headerFilter: "input" },
                    { title: "Responsável", field: "resp_entidade", width: 150, resizable: false, headerFilter: "input" },
                    { title: "Cargo", field: "cargo_resp", width: 100, resizable: false, headerFilter: "input" },
                    { title: "Professor", field: "nome_professor", width: 200, resizable: false, headerFilter: "input" },
                    { title: "idP", field: "codigo", width: 50, resizable: false, visible: false },
                ],

            });



            table1.on("rowSelectionChanged", function (data, rows) {
                //rows - array of row components for the selected rows in order of selection
                //data - array of data objects for the selected rows in order of selection


                var controle = document.getElementById("HiddenField1");
                var ctrl = document.getElementById("HiddenField2");

                if (data.length == 1) {
                    //obtem os dados do primeiro e único elemento (linha) selecionado
                    var ab = rows[0].getData().codigo;
                    controle.value = ab;
                    ctrl.value = data.length + "A";
                    //alert(ctrl.value);

                }
                if (data.length != 1) {
                    controle.value = 0;
                    ctrl.value = 0;
                }
            });

            table2.on("rowSelectionChanged", function (data, rows) {
                //rows - array of row components for the selected rows in order of selection
                //data - array of data objects for the selected rows in order of selection


                var controle = document.getElementById("HiddenField1");
                var ctrl = document.getElementById("HiddenField2");


                if (data.length == 1) {
                    //obtem os dados do primeiro e único elemento (linha) selecionado
                    var ab = rows[0].getData().codigo;
                    controle.value = ab;
                    ctrl.value = data.length + "E";
                    //pra conferir os dados
                    //alert("LINHAS: 1, AB =" + ab + "!");
                }
                if (data.length != 1) {
                    controle.value = 0;
                    ctrl.value = 0;
                }
            });


            var cat_tabledata1 = [
                <asp:Repeater ID="rptItems" runat="server">
                    <ItemTemplate>
                        {
                            id_curso: '<%# DataBinder.Eval(Container.DataItem, "id_curso") %>',
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

            var cat_tabledata2 = [
                <asp:Repeater ID="rptItems2" runat="server">
                    <ItemTemplate>
                        {
                            codigo: '<%# DataBinder.Eval(Container.DataItem, "id_professor") %>',
			                nome_entidade: '<%# DataBinder.Eval(Container.DataItem, "nome_entidade") %>',
			                resp_entidade: '<%# DataBinder.Eval(Container.DataItem, "resp_entidade") %>',
			                cargo_resp: '<%# DataBinder.Eval(Container.DataItem, "cargo_resp") %>',
                            nome_professor: '<%# DataBinder.Eval(Container.DataItem, "nome_prof") %>',
                            loc_entidade: '<%# DataBinder.Eval(Container.DataItem, "loc_entidade") %>',
                        },
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
                $('#ddl_TarEntidade').select2();
                $('#ddl_TarTutor').select2();
            });

            function toggleDivs() {
                var btn = document.getElementById("toggleButton");
                var div1 = document.getElementById("tableDocs");
                var div2 = document.getElementById("tableProt");

                // Verifica qual div está visível no momento
                if (div1.style.display === "block") {
                    // Faz a div1 desaparecer com fade-out
                    div1.style.opacity = "0";
                    setTimeout(function () {
                        div1.style.display = "none";
                    }, 300); // Tempo da transição (500ms)

                    // Faz a div2 aparecer com fade-in
                    div2.style.display = "block";
                    setTimeout(function () {
                        div2.style.opacity = "1";
                    }, 50); // Pequeno intervalo para dar tempo de mudar a propriedade display
                    btn.innerText = "Mostrar Aunos";

                } else {
                    // Faz a div2 desaparecer com fade-out
                    div2.style.opacity = "0";
                    setTimeout(function () {
                        div2.style.display = "none";
                    }, 300); // Tempo da transição (500ms)

                    // Faz a div1 aparecer com fade-in
                    div1.style.display = "block";
                    setTimeout(function () {
                        div1.style.opacity = "1";
                    }, 50); // Pequeno intervalo para dar tempo de mudar a propriedade display
                    btn.innerText = "Mostrar Entidades";
                }
            }

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

            .btnAtualizar {
                color: #31343d;
                border: 1px solid #e5e9f2;
                margin-right: 0.5rem;
                font-weight: 700;
                text-transform: capitalize;
            }
        </style>

        <!--  -->

    </form>
</body>
</html>
