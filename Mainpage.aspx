<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mainpage.aspx.cs" Inherits="F78539.Mainpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="Button1" runat="server" Text="Clear log" OnClick="Button1_Click" />
        <asp:Button runat="server" ID="RUN" Text="RUN AutoTest for EBMS" OnClick="RUN_Click"></asp:Button> <br />
        <asp:Label runat="server" ID="result_msg" Text=""></asp:Label>
    </form>
</body>
</html>
