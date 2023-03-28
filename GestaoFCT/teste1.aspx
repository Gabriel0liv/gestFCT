<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="teste1.aspx.cs" Inherits="GestaoFCT.teste1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

<asp:Panel ID="ModalPanel" runat="server" Visible="false">
   // Conteúdo do modal aqui
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
</asp:Panel>

<asp:Button ID="ShowModalButton" runat="server" Text="Mostrar Modal" OnClick="ShowModal_Click" />

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="ShowModalButton" PopupControlID="ModalPanel" BackgroundCssClass="modal"></ajaxToolkit:ModalPopupExtender>

<script type="text/javascript">
<%--   function ShowModal() {
      $find('<%= ModalPopupExtender1.ClientID %>').show();
   }--%>
</script>




        </div>
    </form>
</body>
</html>
