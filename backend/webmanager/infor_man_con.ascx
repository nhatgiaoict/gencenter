<%@ Control Language="C#" AutoEventWireup="true" CodeFile="infor_man_con.ascx.cs" Inherits="webmanager_infor_man_con" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tr class="Form_Header">
        <td height="22px" style="padding-left: 5px;height:22px;background-image:url(<%=UrlImages%>BodyTop.gif)" colspan="2">
            <asp:Image ID="imgIconHeader" BorderWidth="0" runat="server"></asp:Image>&nbsp;<asp:Literal ID="ltlHeader" runat="server" Text="Infor Manager"></asp:Literal></td>
    </tr>
    <tr>
        <td colspan="2" style="height:15px"></td>        
    </tr>
    <tr>
        <td style="width:80px"></td>
        <td align="left" ><span style="color:Red;size:2"><asp:Literal ID="ltlError" runat="server"></asp:Literal><asp:Literal ID="ltlSuccess" runat="server"></asp:Literal></span></td>
    </tr>
    <tr>
        <td style="width:80px">MailContact:</td>
        <td align="left" style="padding-left: 5px"><asp:TextBox ID="tbxMailContact" runat="server" Width="250px"></asp:TextBox>&nbsp;<span style="color:Red;size:2">*</span></td>
    </tr>
    <tr>
        <td style="width:80px">Backend Title:</td>
        <td align="left" style="padding-left: 5px"><asp:TextBox ID="tbxBackend" runat="server" Width="450px"></asp:TextBox>&nbsp;<span style="color:Red;size:2">*</span></td>
    </tr>
    <tr>
        <td style="width:80px">Frontend Title:</td>
        <td align="left" style="padding-left: 5px"><asp:TextBox ID="tbxFrontend" runat="server" Width="450px"></asp:TextBox>&nbsp;<span style="color:Red;size:2">*</span></td>
    </tr>
     <tr>
        <td style="width:80px">Keywords:</td>
        <td align="left" style="padding-left: 5px"><asp:TextBox ID="txtkeywords" TextMode="MultiLine" runat="server" Width="450px" Height="70px"></asp:TextBox></td>
    </tr>
      <tr>
        <td style="width:80px">Description:</td>
        <td align="left" style="padding-left: 5px"><asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" Width="450px" Height="70px"></asp:TextBox></td>
    </tr>
      <tr>
        <td style="width:80px">Nội dung thẻ H1 trang chủ:</td>
        <td align="left" style="padding-left: 5px"><asp:TextBox ID="txtH1" runat="server" Width="450px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width:80px">Tên công ty:</td>
        <td align="left" style="padding-left: 5px"><asp:TextBox ID="txtNameCO" runat="server" Width="450px"></asp:TextBox></td>
    </tr>
     <tr>
        <td style="width:80px">Hotline:</td>
        <td align="left" style="padding-left: 5px"><asp:TextBox ID="txtSlogan" runat="server" Width="450px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width:80px">Giới thiệu dự án mới</td>
        <td align="left" style="padding-left: 5px"><CKEditor:CKEditorControl ID="txtContent" runat="server" Width="600px" Height="300px"></CKEditor:CKEditorControl></td>
    </tr>
    <tr>
        <td style="width:80px">Thông tin liên hệ</td>
        <td align="left" style="padding-left: 5px"><CKEditor:CKEditorControl ID="txtContact" runat="server" Width="600px" Height="300px"></CKEditor:CKEditorControl></td>
    </tr>
    <tr>
        <td colspan="2" style="height:10px"></td>        
    </tr>
    
     <tr>
        <td style="width:100px"></td>
        <td align="left" >
            <asp:Button ID="btOK" runat="server" Text="Cap nhat" OnClick="btOK_Click" /></td>
    </tr>
</table>