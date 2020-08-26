<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="AutoTest.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Label ID="log" runat="server" Text="log:"></asp:Label><br />
        <asp:Button ID="Button1" runat="server" Text="執行登入" OnClick="Button1_Click" Width="70px" /><br />
        <asp:Button ID="Button2" runat="server" Text="檢查登入" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="一條龍訂位" OnClick="Button3_Click"/>
    </form>
</body>
</html>
