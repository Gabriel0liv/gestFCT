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
                    <a class="sidenav-item-link" href="index.html">
                      <i class="mdi mdi-briefcase-account-outline"></i>
                      <span class="nav-text">Business Dashboard</span>
                    </a>
                  </li>
                
                  <li>
                    <a class="sidenav-item-link" href="analytics.html">
                      <i class="mdi mdi-chart-line"></i>
                      <span class="nav-text">Analytics Dashboard</span>
                    </a>
                  </li>
                      
                  <li class="section-title">
                    Apps
                  </li>

                  <li>
                    <a class="sidenav-item-link" href="chat.html">
                      <i class="mdi mdi-wechat"></i>
                      <span class="nav-text">Chat</span>
                    </a>
                  </li>
                
                  <li>
                    <a class="sidenav-item-link" href="contacts.html">
                      <i class="mdi mdi-phone"></i>
                      <span class="nav-text">Contacts</span>
                    </a>
                  </li>
                
                  <li>
                    <a class="sidenav-item-link" href="team.html">
                      <i class="mdi mdi-account-group"></i>
                      <span class="nav-text">Team</span>
                    </a>
                  </li>

                  <li>
                    <a class="sidenav-item-link" href="calendar.html">
                      <i class="mdi mdi-calendar-check"></i>
                      <span class="nav-text">Calendar</span>
                    </a>
                  </li>

                  <li  class="has-sub active expand" >
                    <a class="sidenav-item-link" href="javascript:void(0)" data-toggle="collapse" data-target="#email"
                      aria-expanded="false" aria-controls="email">
                      <%--<i class="mdi mdi-email"></i>--%>
                      <i class="mdi mdi-account-group"></i>
                      <span class="nav-text">Administração</span> <b class="caret"></b>
                    </a>
                    <ul  class="collapse show"  id="email"
                      data-parent="#sidebar-menu">
                      <div class="sub-menu">
                        
                            <li>
                              <a class="sidenav-item-link" href="GestEmp.aspx">
                                <span class="nav-text">Gestão de Entidades</span>
                              </a>
                            </li>
                            <li>
                              <a class="sidenav-item-link" href="GestTutor.aspx">
                                <span class="nav-text">Gestão de tutores</span>
                              </a>
                            </li>
                            <li>
                              <a class="sidenav-item-link" href="GestProf.aspx">
                                <span class="nav-text">Gestão de professores</span>
                              </a>
                            </li>
                                 
                            <li >
                              <a class="sidenav-item-link" href="GestAluno.aspx">
                                <span class="nav-text">Gestão de alunos</span>
                              </a>
                            </li>
                            <li>
                              <a class="sidenav-item-link" href="GestEnc.aspx">
                                <span class="nav-text">Gestão de Encarregados</span>
                              </a>
                            </li>
                            <li class="active">
                              <a class="sidenav-item-link" href="GestCursos.aspx">
                                <span class="nav-text">Gestão de Cursos</span>
                              </a>
                            </li>
                      </div>
                    </ul>
                  </li>

                  <li class="section-title">
                    UI Elements
                  </li>

                  <li  class="has-sub" >
                    <a class="sidenav-item-link" href="javascript:void(0)" data-toggle="collapse" data-target="#ui-elements"
                      aria-expanded="false" aria-controls="ui-elements">
                      <i class="mdi mdi-folder-outline"></i>
                      <span class="nav-text">UI Components</span> <b class="caret"></b>
                    </a>
                    <ul  class="collapse"  id="ui-elements"
                      data-parent="#sidebar-menu">
                      <div class="sub-menu">

                            <li>
                              <a class="sidenav-item-link" href="alert.html">
                                <span class="nav-text">Alert</span>
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="badge.html">
                                <span class="nav-text">Badge</span>  
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="breadcrumb.html">
                                <span class="nav-text">Breadcrumb</span> 
                              </a>
                            </li>
                          
                        <li  class="has-sub" >
                          <a class="sidenav-item-link" href="javascript:void(0)" data-toggle="collapse" data-target="#buttons"
                            aria-expanded="false" aria-controls="buttons">
                            <span class="nav-text">Buttons</span> <b class="caret"></b>
                          </a>
                          <ul  class="collapse"  id="buttons">
                            <div class="sub-menu">
                              
                              <li >
                                <a href="button-default.html">Button Default</a>
                              </li>
                              
                              <li >
                                <a href="button-dropdown.html">Button Dropdown</a>
                              </li>
                              
                              <li >
                                <a href="button-group.html">Button Group</a>
                              </li>
                              
                              <li >
                                <a href="button-social.html">Button Social</a>
                              </li>
                              
                              <li >
                                <a href="button-loading.html">Button Loading</a>
                              </li>
                              
                            </div>
                          </ul>
                        </li>
                        
                            <li >
                              <a class="sidenav-item-link" href="card.html">
                                <span class="nav-text">Card</span>
                              </a>
                            </li>
  
                            <li >
                              <a class="sidenav-item-link" href="carousel.html">
                                <span class="nav-text">Carousel</span>
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="collapse.html">
                                <span class="nav-text">Collapse</span>
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="editor.html">
                                <span class="nav-text">Editor</span>
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="list-group.html">
                                <span class="nav-text">List Group</span>
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="modal.html">
                                <span class="nav-text">Modal</span>
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="pagination.html">
                                <span class="nav-text">Pagination</span>
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="popover-tooltip.html">
                                <span class="nav-text">Popover & Tooltip</span>
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="progress-bar.html">
                                <span class="nav-text">Progress Bar</span>
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="spinner.html">
                                <span class="nav-text">Spinner</span>
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="switches.html">
                                <span class="nav-text">Switches</span>
                              </a>
                            </li>

                        <li  class="has-sub" >
                          <a class="sidenav-item-link" href="javascript:void(0)" data-toggle="collapse" data-target="#tables"
                            aria-expanded="false" aria-controls="tables">
                            <span class="nav-text">Tables</span> <b class="caret"></b>
                          </a>
                          <ul  class="collapse"  id="tables">
                            <div class="sub-menu">
                              
                              <li >
                                <a href="bootstarp-tables.html">Bootstrap Tables</a>
                              </li>
                              
                              <li >
                                <a href="data-tables.html">Data Tables</a>
                              </li>
                              
                            </div>
                          </ul>
                        </li>

                            <li >
                              <a class="sidenav-item-link" href="tab.html">
                                <span class="nav-text">Tab</span>
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="toaster.html">
                                <span class="nav-text">Toaster</span>
                              </a>
                            </li>
                        
                        <li class="has-sub" >
                          <a class="sidenav-item-link" href="javascript:void(0)" data-toggle="collapse" data-target="#icons"
                            aria-expanded="false" aria-controls="icons">
                            <span class="nav-text">Icons</span> <b class="caret"></b>
                          </a>
                          <ul  class="collapse"  id="icons">
                            <div class="sub-menu">
                              
                              <li >
                                <a href="material-icons.html">Material Icon</a>
                              </li>
                              
                              <li >
                                <a href="flag-icons.html">Flag Icon</a>
                              </li>
                              
                            </div>
                          </ul>
                        </li>
                        
                        <li  class="has-sub" >
                          <a class="sidenav-item-link" href="javascript:void(0)" data-toggle="collapse" data-target="#forms"
                            aria-expanded="false" aria-controls="forms">
                            <span class="nav-text">Forms</span> <b class="caret"></b>
                          </a>
                          <ul  class="collapse"  id="forms">
                            <div class="sub-menu">
                              
                              <li >
                                <a href="basic-input.html">Basic Input</a>
                              </li>
                              
                              <li >
                                <a href="input-group.html">Input Group</a>
                              </li>
                              
                              <li >
                                <a href="checkbox-radio.html">Checkbox & Radio</a>
                              </li>
                              
                              <li >
                                <a href="form-validation.html">Form Validation</a>
                              </li>
                              
                              <li >
                                <a href="form-advance.html">Form Advance</a>
                              </li>
                              
                            </div>
                          </ul>
                        </li>
                        

                        
                        
                        <li  class="has-sub" >
                          <a class="sidenav-item-link" href="javascript:void(0)" data-toggle="collapse" data-target="#maps"
                            aria-expanded="false" aria-controls="maps">
                            <span class="nav-text">Maps</span> <b class="caret"></b>
                          </a>
                          <ul  class="collapse"  id="maps">
                            <div class="sub-menu">
                              
                              <li >
                                <a href="google-maps.html">Google Map</a>
                              </li>
                              
                              <li >
                                <a href="vector-maps.html">Vector Map</a>
                              </li>
                              
                            </div>
                          </ul>
                        </li>

                        <li  class="has-sub" >
                          <a class="sidenav-item-link" href="javascript:void(0)" data-toggle="collapse" data-target="#widgets"
                            aria-expanded="false" aria-controls="widgets">
                            <span class="nav-text">Widgets</span> <b class="caret"></b>
                          </a>
                          <ul  class="collapse"  id="widgets">
                            <div class="sub-menu">
                              
                              <li >
                                <a href="widgets-general.html">General Widget</a>
                              </li>
                              
                              <li >
                                <a href="widgets-chart.html">Chart Widget</a>
                              </li>
                              
                            </div>
                          </ul>
                        </li>

                      </div>
                    </ul>
                  </li>

                  <li  class="has-sub" >
                    <a class="sidenav-item-link" href="javascript:void(0)" data-toggle="collapse" data-target="#charts"
                      aria-expanded="false" aria-controls="charts">
                      <i class="mdi mdi-chart-pie"></i>
                      <span class="nav-text">Charts</span> <b class="caret"></b>
                    </a>
                    <ul  class="collapse"  id="charts"
                      data-parent="#sidebar-menu">
                      <div class="sub-menu">

                            <li >
                              <a class="sidenav-item-link" href="apex-charts.html">
                                <span class="nav-text">Apex Charts</span>
                                
                              </a>
                            </li>

                      </div>
                    </ul>
                  </li>
 
                  <li class="section-title">
                    Pages
                  </li>

                  <li  class="has-sub" >
                    <a class="sidenav-item-link" href="javascript:void(0)" data-toggle="collapse" data-target="#users"
                      aria-expanded="false" aria-controls="users">
                      <i class="mdi mdi-image-filter-none"></i>
                      <span class="nav-text">User</span> <b class="caret"></b>
                    </a>
                    <ul  class="collapse"  id="users"
                      data-parent="#sidebar-menu">
                      <div class="sub-menu">

                            <li >
                              <a class="sidenav-item-link" href="user-profile.html">
                                <span class="nav-text">User Profile</span>
                                
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="user-activities.html">
                                <span class="nav-text">User Activities</span>                    
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="user-profile-settings.html">
                                <span class="nav-text">User Profile Settings</span>
                                
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="user-account-settings.html">
                                <span class="nav-text">User Account Settings</span> 
                              </a>
                            </li>
                          
                            <li >
                              <a class="sidenav-item-link" href="user-planing-settings.html">
                                <span class="nav-text">User Planing Settings</span>
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="user-billing.html">
                                <span class="nav-text">User billing</span>
                                
                              </a>
                            </li>
                          
                            <li >
                              <a class="sidenav-item-link" href="user-notify-settings.html">
                                <span class="nav-text">User Notify Settings</span>             
                              </a>
                            </li>

                      </div>
                    </ul>
                  </li>

                  <li  class="has-sub" >
                    <a class="sidenav-item-link" href="javascript:void(0)" data-toggle="collapse" data-target="#authentication"
                      aria-expanded="false" aria-controls="authentication">
                      <i class="mdi mdi-account"></i>
                      <span class="nav-text">Authentication</span> <b class="caret"></b>
                    </a>
                    <ul  class="collapse"  id="authentication"
                      data-parent="#sidebar-menu">
                      <div class="sub-menu">

                            <li >
                              <a class="sidenav-item-link" href="sign-in.html">
                                <span class="nav-text">Sign In</span>
                                
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="sign-up.html">
                                <span class="nav-text">Sign Up</span>
                                
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="reset-password.html">
                                <span class="nav-text">Reset Password</span>
                                
                              </a>
                            </li>

                      </div>
                    </ul>
                  </li>

                  <li  class="has-sub" >
                    <a class="sidenav-item-link" href="javascript:void(0)" data-toggle="collapse" data-target="#other-page"
                      aria-expanded="false" aria-controls="other-page">
                      <i class="mdi mdi-file-multiple"></i>
                      <span class="nav-text">Other pages</span> <b class="caret"></b>
                    </a>
                    <ul  class="collapse"  id="other-page"
                      data-parent="#sidebar-menu">
                      <div class="sub-menu">

                            <li >
                              <a class="sidenav-item-link" href="invoice.html">
                                <span class="nav-text">Invoice</span>
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="404.html">
                                <span class="nav-text">404 page</span>
                                
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="page-comingsoon.html">
                                <span class="nav-text">Coming Soon</span
                              </a>
                            </li>
                          
                            <li >
                              <a class="sidenav-item-link" href="page-maintenance.html">
                                <span class="nav-text">Maintenance</span>
                              </a>
                            </li>

                      </div>
                    </ul>
                  </li>

                  <li class="section-title">
                    Documentation
                  </li>

                  <li>
                    <a class="sidenav-item-link" href="getting-started.html">
                      <i class="mdi mdi-airplane"></i>
                      <span class="nav-text">Getting Started</span>
                    </a>
                  </li>

                  <li  class="has-sub" >
                    <a class="sidenav-item-link" href="javascript:void(0)" data-toggle="collapse" data-target="#customization"
                      aria-expanded="false" aria-controls="customization">
                      <i class="mdi mdi-square-edit-outline"></i>
                      <span class="nav-text">Customization</span> <b class="caret"></b>
                    </a>
                    <ul  class="collapse"  id="customization"
                      data-parent="#sidebar-menu">
                      <div class="sub-menu">

                            <li >
                              <a class="sidenav-item-link" href="navbar-customization.html">
                                <span class="nav-text">Navbar</span>
                                
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="sidebar-customization.html">
                                <span class="nav-text">Sidebar</span>
                              </a>
                            </li>

                            <li >
                              <a class="sidenav-item-link" href="styling.html">
                                <span class="nav-text">Styling</span>
                              </a>
                            </li>

                      </div>
                    </ul>
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



                    <!-- head left option -->
                    <!--<div class="head-left-options">
                    <div class="form-check">
                        <input class="form-check-input" type="checkbox" id="defaultCheck1">
                        <label class="form-check-label" for="defaultCheck1">Select All</label>
                    </div>
                    <button type="button" class="btn btn-icon btn-outline btn-rounded-circle">
                        <i class="mdi mdi-refresh"></i>
                    </button>
                    <div class="dropdown">
                        <button class="btn dropdown-toggle border rounded-pill" type="button" id="dropdownMenuButton"
                        data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">More
                        </button>
                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton" x-placement="bottom-start"
                        style="position: absolute; transform: translate3d(0px, 40px, 0px); top: 0px; left: 0px; will-change: transform;">
                        <a class="dropdown-item" href="#">Action</a>
                        <a class="dropdown-item" href="#">Another action</a>
                        <a class="dropdown-item" href="#">Something else here</a>
                        </div>
                    </div>
                    </div> -->
                    <!-- head right option -->
                    <!-- <div class="head-right-options">
                    <div class="btn-group" role="group" aria-label="Basic example">
                        <button type="button" class="btn border btn-pill">
                        <i class="mdi mdi-chevron-left"></i>
                        </button>
                        <button type="button" class="btn border btn-pill">
                        <i class="mdi mdi-chevron-right"></i>
                        </button>
                    </div>
                    </div> -->

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
                            <div class="modal-body" style="height: 400px;overflow-y: auto;">
                                
                                <div class="form-group">
                                  <label for="txt_nome">Nome do curso</label>
                                  <input type="text" class="form-control" id="txt_nome" placeholder="Insira o nome do curso" enableviewstate="true" runat="server"/>
                                </div>

                                <div class="form-group">
                                  <label for="txt_CodPost">Data de Início</label>
                                  <input type="text" class="form-control" id="txt_dataIni" placeholder="Insira o ano de inicio do curso. Ex: 2022" runat="server"/>
                                </div>
                                <div class="form-group">
                                  <label for="txt_CodPost">Data de Finalização</label>
                                  <input type="text" class="form-control" id="txt_dataFim" placeholder="Insira o ano de finalização do curso. Ex: 2023" runat="server"/>
                                </div>
                              
                            </div>
                            <div class="modal-footer">
                              <button type="button" id="btn_fechar" class="btn btn-danger btn-pill" runat="server" onserverclick="Fechar"> Cancelar</button>

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
                            <%--<button type="button" class="btn btn-danger btn-pill" onclick="" >Close</button>--%>
                            <asp:Button ID="btnCancelar" class="btn btn-danger btn-pill" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                            <%--<button type="button" class="btn btn-primary btn-pill">Save Changes</button>--%>
                            <asp:Button ID="btnDeletar" class="btn btn-primary btn-pill" runat="server" Text="Eliminar" OnClick="Comandos"  />
                          </div>
                        </div>
                      </div>
                    </div>

                        <%--TABULATOR--%>
                      </div>
                    </div>
                    <div class="border border-top-0 rounded table-responsive email-list">
                      <!-- <table class="table mb-0 table-email"> </table> -->

                      <div id="example-table" ></div>

                    </div>
                  </div>
                </div>
              </div>
            </div>
            </div>
          
        </div>
        
          <!-- Footer -->
          <footer class="footer mt-auto">
            <div class="copyright bg-white">
              <p>
                &copy; <span id="copy-year"></span> Copyright Mono Dashboard Bootstrap Template by <a class="text-primary" href="http://www.iamabdus.com/" target="_blank" >Abdus</a>.
              </p>
            </div>
            <script>
                var d = new Date();
                var year = d.getFullYear();
                document.getElementById("copy-year").innerHTML = year;
            </script>
          </footer>

      </div>





      <script>

          var stat = document.getElementById('<%= labelStats.ClientID %>');
          var cod = document.getElementById('<%= labelCod.ClientID %>');

        var table = new Tabulator("#example-table", {
          selectable: 1,
          placeholder: "Sem dados",
          height:"310px",
          layout:"fitDataStretch",
          columns:[
            {title:"Cód.", field:"codigo", width:100, resizable:false},
            {title:"Nome", field:"nome", width:230, resizable:false},
            {title:"Data de início", field:"dataini", width:207, resizable:false},
            {title:"Data de finalização", field:"datafim", width:207, resizable:false},

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

      .tabulator{ background-color: white; }
      .tabulator .tabulator-header{width: auto;}
      /* .tabulator .tabulator-tableholder{overflow-x: hidden;} */
      /* .tabulator .tabulator-row-even {} */
    </style>

    </form>
  </body>
</html>
