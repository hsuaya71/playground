<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="F78539.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="dogroup"/>
        <asp:GridView ID="GridView2" runat="server"></asp:GridView>
        Browser:　<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
        IsMobileDevice?:　<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label><br />
        UserAgent:  <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label><br />
        IP:  <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>
