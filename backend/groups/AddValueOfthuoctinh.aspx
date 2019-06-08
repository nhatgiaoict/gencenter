<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddValueOfthuoctinh.aspx.cs" Inherits="groups_AddValueOfthuoctinh" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Thêm giá trị cho nhóm thuộc tính</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width:100%;" cellpadding="0" cellspacing="0" border="0">
   <tr><td style="width:100px"><strong>Các giá trị</strong></td>
		<td>
		<asp:Repeater ID="rptGaitri" runat="server" onitemcommand="rptGaitri_ItemCommand">
		<HeaderTemplate>
        <asp:LinkButton ID="lnkAddRow" runat="server" CommandName="AddRow">Thêm giá trị</asp:LinkButton>
        </HeaderTemplate>
        <ItemTemplate>
        <p>
        <asp:TextBox ID="txtValue" Enabled="false" runat="server" Text='<%# Eval("title")%>'></asp:TextBox>
        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="edit" CommandArgument='<%#Eval("id")%>'>Edit</asp:LinkButton>
        <asp:LinkButton Visible="false" ID="lnkUpdate" runat="server" CommandName="update" CommandArgument='<%#Eval("id")%>'>Update</asp:LinkButton>
        <asp:LinkButton Visible="false" ID="lnkCancel" runat="server" CommandName="cancel" CommandArgument='<%#Eval("id")%>'>Cancel</asp:LinkButton>
        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" OnClientClick='javascript:return confirm("Are you sure you want to delete?")'
            CommandArgument='<%#Eval("id")%>'>Delete</asp:LinkButton>
        </p>
        </ItemTemplate>
        <FooterTemplate><asp:LinkButton ID="ltbAdd" CommandName="AddNew" runat="server" Visible="false">Thêm mới</asp:LinkButton></FooterTemplate>
		</asp:Repeater>
		</td>
		</tr>
		<tr><td colspan="2"><span id="spanTB" runat="server" visible="false" style="color:Red">Bạn cần nhập giá trị</span></td></tr>
    </table>
    </div>
    </form>
</body>
</html>
