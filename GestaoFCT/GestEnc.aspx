<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestEnc.aspx.cs" Inherits="GestaoFCT.GestEnc" %>


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

    <title>GestFCT - Encarregados</title>

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
            <asp:SqlDataSource ID="EncSQLData" runat="server" ConnectionString="<%$ ConnectionStrings:FCTConnectionString %>"></asp:SqlDataSource>



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

                            <li id="NavEE" runat="server" class="active">
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
                        <button id="sidebar-toggler" class="sidebar-toggle">
                            <span class="sr-only">Toggle navigation</span>
                        </button>

                        <span class="page-title">Gestão de Encarregados de Educação</span>

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
                        <!-- ====================================
  ——— EMAIL WRAPPER
	===================================== -->
                        <div class="email-wrapper rounded border bg-white">
                            <div class="row no-gutters justify-content-center">
                                <div class="col-lg-4 col-xl-3 col-xxl-2">
                                    <div class="email-left-column email-options p-4 p-xl-5">
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
                                        <div class="email-right-header">



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
                                                            <div id="Alert" class="alert alert-secondary alert-icon" role="alert" visible="false" runat="server">
                                                                <i class="mdi mdi-alert"></i><span id="alerMessage" runat="server"></span>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_nome">Nome</label>
                                                                <input type="text" class="form-control" id="txt_nome" placeholder="Insira o nome do Encarregado" enableviewstate="true" runat="server" required="required" />
                                                            </div>

                                                            <div class="form-group">
                                                                <label for="txt_nif">NIF</label>
                                                                <input type="number" class="form-control" id="txt_nif" placeholder="Insira o NIF do Encarregado" runat="server" required="required" />
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_bi">Bilhete de Identidade/Cartão Único</label>
                                                                <input type="text" class="form-control" id="txt_bi" placeholder="Insira o número de identificação do Encarregado" runat="server" required="required" />
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_val">Validade</label>
                                                                <input type="text" class="form-control" id="txt_val" placeholder="Insira o mês/ano de validade do nº de identificação" runat="server" />
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_email">Email address</label>
                                                                <input type="email" class="form-control" id="txt_email" aria-describedby="emailHelp" placeholder="Insira o email do Encarregado" runat="server" required="required" />
                                                                <!-- <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small> -->
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_telefone">Telefone</label>
                                                                <input type="tel" class="form-control" id="txt_telefone" placeholder="Insira o telefone fixo, se houver." pattern="[2-8][0-9]{8}" title="Insira um número de telefone de 9 digitos" runat="server" />
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_telemovel">Telemovel</label>
                                                                <input type="tel" class="form-control" id="txt_telemovel" placeholder="Insira o telemóvel do Encarregado" pattern="[9][1-9][0-9]{7}" title="insira um número de telemóvel de 9 digitos" runat="server" required="required" />
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_morada">Morada</label>
                                                                <input type="text" class="form-control" id="txt_morada" placeholder="Insira a morada do encarregado" runat="server" required="required" />
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_local">Localidade</label>
                                                                <input type="text" class="form-control" id="txt_local" placeholder="Insira a localidade do encarregado" runat="server" required="required" />
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="txt_CodPost">Código Postal</label>
                                                                <input type="text" class="form-control" id="txt_CodPost" placeholder="Insira o código postal. ex: 3750-326" runat="server" pattern="[0-9]{4}-[0-9]{3}" title="Exemplo do formato: 0000-000" required="required" />
                                                            </div>

                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" id="btn_fechar" class="btn btn-danger btn-pill" runat="server" onserverclick="Fechar">Cancelar</button>
                                                            <asp:Button ID="btn_enviar" class="btn btn-primary btn-pill" runat="server" Text="Criar encarregado" OnClick="Comandos" />
                                                        </div>
                                                </div>
                                            </div>

                                            <!-- DELETE MODAL -->
                                            <div class="modal" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
                                                aria-hidden="true" runat="server" visible="false">
                                                <div class="modal-dialog" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="exampleModalLabel">Eliminar registo do encarregado</h5>
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
                    { title: "Cód.", field: "codigo", width: 50, resizable: false, headerFilter: "number" },
                    { title: "Nome", field: "nome", width: 230, resizable: false, headerFilter: "input" },
                    { title: "NIF", field: "nif", width: 80, resizable: false, headerFilter: "number" },
                    { title: "E-mail", field: "email", width: 230, resizable: false, headerFilter: "input" },
                    { title: "Morada", field: "morada", width: 230, resizable: false, headerFilter: "input" },
                    { title: "localidade", field: "localidade", width: 156, resizable: false, headerFilter: "input" },
                    { title: "C.Postal", field: "codPost", width: 80, resizable: false, headerFilter: "input" },
                    { title: "Telefone", field: "telefone", width: 156, resizable: false, headerFilter: "number" },
                    { title: "Telemovel", field: "telemovel", width: 156, resizable: false, headerFilter: "input" },
                    { title: "Bi", field: "bi", width: 156, resizable: false, headerFilter: "input" },
                    { title: "Validade", field: "valbi", width: 100, resizable: false, headerFilter: "input" },

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
                        {codigo: '<%#DataBinder.Eval(Container.DataItem, "id_ee") %>',
                    nome: '<%#DataBinder.Eval(Container.DataItem, "nome_ee") %>',
                    nif: '<%#DataBinder.Eval(Container.DataItem, "nif_ee") %>', 
                    email: '<%#DataBinder.Eval(Container.DataItem, "email_ee") %>', 
                    morada: '<%#DataBinder.Eval(Container.DataItem, "morada_ee") %>',
                    localidade: '<%#DataBinder.Eval(Container.DataItem, "loc_ee") %>', 
                    codPost: '<%#DataBinder.Eval(Container.DataItem, "cpostal_ee") %>',
                    telefone: '<%#DataBinder.Eval(Container.DataItem, "telefone_ee") %>',
                    telemovel: '<%#DataBinder.Eval(Container.DataItem, "telemovel_ee") %>',
                    bi: '<%#DataBinder.Eval(Container.DataItem, "bi_ee") %>',
                    valbi: '<%#DataBinder.Eval(Container.DataItem, "valBi_ee") %>'},

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

            .email-right-column .email-right-header {
                margin-bottom: auto !important
            }
        </style>

    </form>
</body>
</html>
