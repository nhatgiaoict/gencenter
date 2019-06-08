<%@ Control Language="C#" AutoEventWireup="true" CodeFile="auto_con.ascx.cs" Inherits="webmanager_auto_con" %>
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tr class="Form_Header">
        <td height="22px" style="padding-left: 5px;height:22px;background-image:url(<%=UrlImages%>BodyTop.gif)" colspan="2">
            <asp:Image ID="imgIconHeader" BorderWidth="0" runat="server"></asp:Image>&nbsp;<asp:Literal
                ID="ltlHeader" runat="server" Text="Auto Create"></asp:Literal></td>
    </tr>
    <tr>
        <td colspan="2" style="height:15px"></td>        
    </tr>
    
    <tr>
        <td style="width:180px"></td>
        <td align="left" ><span style="color:Red;size:2"><asp:Literal ID="ltlError" runat="server"></asp:Literal><asp:Literal ID="ltlSuccess" runat="server"></asp:Literal></span></td>
    </tr>
    <tr>
        <td style="width:180px"></td>
        <td align="left" style="padding-left: 5px"><u><b>Localhost</b></u></td>
    </tr>
    <tr>
        <td style="width:180px">Domain name(Backend):</td>
        <td align="left" style="padding-left: 5px"><asp:TextBox ID="tbxDomain_Backend" runat="server" Width="250px"></asp:TextBox>&nbsp;<span style="color:Red;size:2">*</span></td>
    </tr>
    <tr>
        <td style="width:180px">Domain name(Frontend):</td>
        <td align="left" style="padding-left: 5px"><asp:TextBox ID="tbxDomain_Frontend" runat="server" Width="250px"></asp:TextBox>&nbsp;<span style="color:Red;size:2">*</span></td>
    </tr>
    <tr>
        <td style="width:180px">Directory name:</td>
        <td align="left" style="padding-left: 5px"><asp:TextBox ID="tbxDirect" runat="server" Width="250px"></asp:TextBox>&nbsp;<span style="color:Red;size:2">*</span></td>
    </tr>
    <tr>
        <td style="width:150px"></td>
        <td align="left" style="padding-left: 5px"><u><b>DataBase</b></u></td>
    </tr>
    <tr>
        <td style="width:150px">Database name:</td>
        <td align="left" style="padding-left: 5px"><asp:TextBox ID="tbxDatabasename" runat="server" Width="250px"></asp:TextBox>&nbsp;<span style="color:Red;size:2">*</span></td>
    </tr>
    <tr>
        <td style="width:150px">Server(IP):</td>
        <td align="left" style="padding-left: 5px"><asp:TextBox ID="tbxServer" runat="server" Width="250px"></asp:TextBox>&nbsp;<span style="color:Red;size:2">*</span></td>
    </tr>
    <tr>
        <td style="width:150px">uid:</td>
        <td align="left" style="padding-left: 5px"><asp:TextBox ID="tbxuid" runat="server" Width="250px"></asp:TextBox>&nbsp;<span style="color:Red;size:2">*</span></td>
    </tr>
    <tr>
        <td style="width:150px">password:</td>
        <td align="left" style="padding-left: 5px"><asp:TextBox ID="tbxPass" runat="server" Width="250px"></asp:TextBox></td>
    </tr>        
    <tr>
        <td colspan="2" style="height:10px"></td>        
    </tr>
     <tr>
        <td style="width:180px"></td>
        <td align="left" >
            <asp:Button ID="btOK" runat="server" Text="Auto Create" OnClick="btOK_Click" /></td>
    </tr>
</table>