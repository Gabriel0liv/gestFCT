<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GestaoFCT.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login</title>
    <meta charset="UTF-8"/>
    <link rel="stylesheet" href="css/style-login.css"/>

  <script src="https://cdnjs.cloudflare.com/ajax/libs/particlesjs/2.2.3/particles.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <!-- partial:index.partial.html -->
        <div class="login-root">
            <div class="box-root flex-flex flex-direction--column" style="min-height: 100vh;flex-grow: 1;">

                <canvas class="background" style="width:100%; height: 100%"></canvas>
      
                <div class="box-root padding-top--24 flex-flex flex-direction--column" style="flex-grow: 1; z-index: 9;">
                <div class="box-root padding-top--48 padding-bottom--24 flex-flex flex-justifyContent--center">
                    <h1><a href="#" rel="dofollow">GestFCT</a></h1>
                </div>
                <div class="formbg-outer">
                    <div class="formbg">
                    <div class="formbg-inner padding-horizontal--48">
                        <span class="padding-bottom--15">Faça login em sua conta</span>
                        <div id="stripe-login">
                            <div class="field padding-bottom--24">
                                <label for="email">Email</label>
                                <input id="txt_email" type="email" name="email" required="required" runat="server"/>
                            </div>
                            <div class="field padding-bottom--24">
                                <div class="grid--50-50">
                                <label for="password">Password</label>
                                <div class="reset-pass">
                                    <a href="#">Esqueceu a senha?</a>
                                </div>
                                </div>
                                <input id="txt_pass" type="password" name="password" required="required" runat="server"/>
                            </div>
                            <div class="field field-checkbox padding-bottom--24 flex-flex align-center">
                            </div>
                            <div class="field ">
                                <%--<input type="submit" name="submit" value="Continue"/>--%>
                                <asp:Button ID="btn_login" runat="server" Text="Continue" OnClick="btn_login_Click" />
                            </div>

                        </div>
                    </div>
                    </div>
                </div>
                </div>
            </div>
        </div>
    </form>
</body>

    <!-- partial -->
    <script>
        window.onload = function () {
            Particles.init({
                selector: ".background"
            });
        };
        var particles = Particles.init({
            selector: ".background",
            color: ["#700494", "#9e6de0", "#000000"],
            connectParticles: true,
            responsive: [
                {
                    breakpoint: 768,
                    options: {
                        color: ["#700494", "#9e6de0", "#e602ff"],
                        maxParticles: 70,
                        connectParticles: false
                    }
                }
            ]
        });

    </script>
</html>
