<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FterUpdate_con.ascx.cs" Inherits="publish_FterUpdate_con" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register TagPrefix="radcln" Namespace="Telerik.WebControls" Assembly="RadCalendar" %>
<%@ Register TagPrefix="cc1" Namespace="uss.pagelist" Assembly="uss.pagelist" %>
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tr class="Form_Header">
        <td style="padding-left: 5px" colspan="3" height="22" background="<%=UrlImages%>BodyTop.gif">
            <asp:Image ID="imgIconHeader" BorderWidth="0" runat="server"></asp:Image>&nbsp;<asp:Literal
                ID="ltlHeader" runat="server" Text="Them footer"></asp:Literal></td>
    </tr>
    <tr style=" padding-top:30px;">
    <td colspan="3">
    <asp:Label ID="lbspace" runat="server"></asp:Label>
    </td>
    </tr>
    <tr>
        <td width="100">
            <asp:Literal ID="ltlTitle" runat="server" Text="Tiêu de"></asp:Literal></td>
        <td align="left" style="padding-left: 5px">
            <asp:TextBox ID="tbxTitle" runat="server"></asp:TextBox></td>
                
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td align="left" style="padding-left: 5px" class="RequireField">
            <asp:Literal ID="ltlRequireTitle" runat="server" Text="Phai nhap tiêu de" Visible="False"></asp:Literal></td>
            
    </tr>
    <tr>
        <td>
            <asp:Literal ID="ltlPublishDate" runat="server" Text="Ngày phát hành"></asp:Literal></td>
        <td colspan="2" style="padding-left: 5px">
            <radcln:raddatepicker id="rdpPublishDate" Runat="server">
                <dateinput CssClass="textbox" promptchar=" " dateformat="dd/MM/yyyy HH:mm:ss"
                    displaypromptchar="_" width="130px" displaydateformat=""></dateinput>
            </radcln:raddatepicker>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Literal ID="ltlContent" runat="server" Text="Noi dung"></asp:Literal>&nbsp;
            <font color="#ff0000">
                <asp:Literal ID="ltlRequireContent" runat="server" Text="Phai nhap noi dung" Visible="False"></asp:Literal></font></td>
    </tr>
    <!---Noi dung--->
    <tr>
        <td align="left" colspan="3">
			<CKEditor:CKEditorControl ID="txtContent12" runat="server" Width="850px" Height="500px"></CKEditor:CKEditorControl>
		</td>
    </tr>
    <!---End Noi dung--->
    <tr>
        <td class="RequireField" colspan="3">
            <asp:Literal ID="ltlNote" runat="server" Text="*-Thông tin yêu cau bat buoc"></asp:Literal></td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btnRegister" Text="Cap Nhat" runat="server" OnClick="btnRegister_Click"></asp:Button>&nbsp;
            </td>
    </tr>
    
    <tr>
    <td style=" padding-left:10px;" colspan="3">
    
    </td>
    </tr>
</table>