﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="AutoTest.Main" %>

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
        去程航線：起
        <asp:DropDownList ID="ddl1" runat="server">
            <asp:ListItem Text="松山(TSA)">松山(TSA)</asp:ListItem>
            <asp:ListItem Text="台中(RMQ)">台中(RMQ)</asp:ListItem>
            <asp:ListItem Text="嘉義(CYI)">嘉義(CYI)</asp:ListItem>
            <asp:ListItem Text="台南(TNN)">台南(TNN)</asp:ListItem>
            <asp:ListItem Text="高雄(KHH)" Selected="True">高雄(KHH)</asp:ListItem>
            <asp:ListItem Text="花蓮(HUN)">花蓮(HUN)</asp:ListItem>
            <asp:ListItem Text="台東(TTT)">台東(TTT)</asp:ListItem>
            <asp:ListItem Text="金門(KNH)">金門(KNH)</asp:ListItem>
            <asp:ListItem Text="澎湖(MZG)">澎湖(MZG)</asp:ListItem>
            <asp:ListItem Text="南竿(LZN)">南竿(LZN)</asp:ListItem>
            <asp:ListItem Text="北竿(MFK)">北竿(MFK)</asp:ListItem>
        </asp:DropDownList>&nbsp;&nbsp;
         
        迄
        <asp:DropDownList ID="ddl2" runat="server">
            <asp:ListItem Text="松山(TSA)">松山(TSA)</asp:ListItem>
            <asp:ListItem Text="台中(RMQ)">台中(RMQ)</asp:ListItem>
            <asp:ListItem Text="嘉義(CYI)">嘉義(CYI)</asp:ListItem>
            <asp:ListItem Text="台南(TNN)">台南(TNN)</asp:ListItem>
            <asp:ListItem Text="高雄(KHH)">高雄(KHH)</asp:ListItem>
            <asp:ListItem Text="花蓮(HUN)">花蓮(HUN)</asp:ListItem>
            <asp:ListItem Text="台東(TTT)">台東(TTT)</asp:ListItem>
            <asp:ListItem Text="金門(KNH)" Selected="True">金門(KNH)</asp:ListItem>
            <asp:ListItem Text="澎湖(MZG)">澎湖(MZG)</asp:ListItem>
            <asp:ListItem Text="南竿(LZN)">南竿(LZN)</asp:ListItem>
            <asp:ListItem Text="北竿(MFK)">北竿(MFK)</asp:ListItem>
        </asp:DropDownList>&nbsp;&nbsp;
        班次<asp:TextBox ID="flyno" runat="server" Text="8917"></asp:TextBox><br />
        AGT：所屬區域<asp:TextBox ID="agtArea" runat="server" Text="台東(TTT)"></asp:TextBox>&nbsp;&nbsp;
        AGT名稱<asp:TextBox ID="agtName" runat="server" Text="RITA旅行社"></asp:TextBox><br />
        人數：<asp:TextBox ID="pnum" runat="server" Text="5"></asp:TextBox>&nbsp;&nbsp;
        開團數<asp:TextBox ID="gnum" runat="server" Text="1"></asp:TextBox><br />
        迄日期：年<asp:TextBox ID="txt_y" runat="server" Text="2020"></asp:TextBox>&nbsp;&nbsp;
        月<asp:TextBox ID="txt_m" runat="server" Text="10"></asp:TextBox>&nbsp;&nbsp;
        日<asp:TextBox ID="txt_d" runat="server" Text="30"></asp:TextBox><br />
        特定PNR：<asp:TextBox ID="pnr" runat="server" Text="TAERYO"></asp:TextBox>&nbsp;&nbsp;手輸驗證碼：<asp:TextBox ID="vcode" runat="server"></asp:TextBox>
        <hr />
        <asp:Button ID="Button2" runat="server" Text="前台登入" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="一條龍訂位" OnClick="Button3_Click"/>
        <asp:Button ID="Button1" runat="server" Text="團體>名單送審" OnClick="Button1_Click"/>
        <asp:Button ID="Button7" runat="server" Text="團體>名單儲存" OnClick="Button7_Click"/>
        <asp:Button ID="Button4" runat="server" Text="確認開票" OnClick="Button4_Click"/>
        <hr /> 
        <asp:Button ID="Button8" runat="server" Text ="後台登入" OnClick="Button8_Click" />
        <asp:Button ID="Button9" runat="server" Text="SERIES新增(整月)" OnClick="Button9_Click"/> 
        <asp:Button ID="grpRefund" runat="server" Text="團體退票" OnClick="grpRefund_Click"/> 
        <asp:Button ID="Button6" runat="server" Text="adhoc" OnClick="Button6_Click"/>
        <asp:Button ID="Button11" runat="server" Text="adhoc審核" OnClick="Button11_Click"/>
        <asp:Button ID="Button5" runat="server" Text="計價" OnClick="Button5_Click"/>
        <asp:Button ID="Button13" runat="server" Text="人工開票" OnClick="Button13_Click"/>
        <asp:Button ID="Button12" runat="server" Text="進行開票" OnClick="Button12_Click"/><br />
        <asp:Button ID="chkAGTkeyword" runat="server" Text="AGT關鍵字，各功能頁驗證" OnClick="chkAGTkeyword_Click"/>
        <hr /> 
        <asp:Button ID="Button10" runat="server" Text="團體開票smokeTest" OnClick="Button10_Click"/>
        <asp:Button ID="SERIESbyday" runat="server" Text="SERIES新增smokeTest" OnClick="SERIESbyday_Click"/><br /><br />
        <asp:TextBox ID="logs" runat="server" ReadOnly="true" Width="650px" BorderColor="White"  Font-Size="Smaller"></asp:TextBox>
    </form>
</body>
</html>
