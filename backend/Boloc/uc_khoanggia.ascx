<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_khoanggia.ascx.cs" Inherits="Boloc_uc_khoanggia" %>
<table width="95%" cellspacing="5" cellpadding="0" border="0" align="center">
<tbody>
		<tr class="Form_Header">
			<td height="22" background="/data/images/BodyTop.gif" style="PADDING-LEFT: 5px" colspan="3">
			<img src="/data/images/icon_menu1.gif" style="border-width:0px;" />&nbsp;Quản trị các hãng sản xuất</td>
		</tr>
		<tr clas="item" style="height:30"><td><strong>Thông tin thêm mới</strong></td></tr>
		<tr>
		<td>
		<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tbody>
		<tr><td style="width:100px;">Tiêu đề</td><td style="width:250"><asp:TextBox ID="txtTitle" runat="server" Width="200"></asp:TextBox>
		</td>
		<td>Min</td><td><asp:TextBox ID="txtMin" runat="server"></asp:TextBox></td>
		<td>Max</td><td><asp:TextBox ID="txtMax" runat="server"></asp:TextBox></td>
		<td style="width:100"><asp:Button ID="btnOk" runat="server" Text="Cập nhật" 
                onclick="btnOk_Click" /></td>
		<td><span style="color:Red;" id="spanmess" runat="server" visible="false"> Bạn cần nhập thông tin</span></td>
		</tr>
		
		</tbody>
		</table>
		</td>
		</tr>
		<tr><td></td></tr>
		<tr>
		<td>
		<table width="100%" cellspacing="1" cellpadding="1" border="0" bgcolor="#aaaeb5"><tbody>
		<asp:Repeater ID="rptHang" runat="server" onitemcommand="rptHang_ItemCommand">
		<HeaderTemplate>
           <tr height="30px" bgcolor="#9dbae9">
            <td width="40px" align="center"><strong>STT</strong></td>
            <td align="left" style="padding-left: 20px"><strong>Tiêu đề</strong></td>
            <td align="left" style="padding-left: 20px"><strong>Giá trị Min</strong></td>
            <td align="left" style="padding-left: 20px"><strong>Giá trị Max</strong></td>
            <td width="100px" align="center"><strong>Thao tác</strong></td>
           </tr>
		</HeaderTemplate>
		<ItemTemplate>
<tr height="30px" class="item">
<td width="40px" align="center"><asp:TextBox Text='<%#Eval("idx")%>' ID="txtSTT" runat="server" Enabled="false" Width="50px"></asp:TextBox></td>
<td align="left" style="padding-left: 20px"><asp:TextBox Text='<%#Eval("title")%>' ID="txttitle" Enabled="false" runat="server" Width="200px"></asp:TextBox></td>
<td><asp:TextBox ID="txtMin" runat="server" Enabled="false" Text='<%#Eval("Min")%>'></asp:TextBox></td>
<td><asp:TextBox ID="txtMax" runat="server" Enabled="false" Text='<%#Eval("Max")%>'></asp:TextBox></td>
<td width="40px" align="center">
<asp:LinkButton ID="lnkEdit" runat="server" CommandName="edit" CommandArgument='<%#Eval("id")%>'>Edit</asp:LinkButton>
<asp:LinkButton ID="lnkUpdate" runat="server" CommandName="update" Visible="false" CommandArgument='<%#Eval("id")%>'>Update</asp:LinkButton>
<asp:LinkButton Visible="false" ID="lnkCancel" runat="server" CommandName="cancel" CommandArgument='<%#Eval("id")%>'>Cancel</asp:LinkButton>
<asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" OnClientClick='javascript:return confirm("Bạn có chắc chắn xoá không?")'
CommandArgument='<%#Eval("id")%>'>Delete</asp:LinkButton>
</td>
</tr>
		</ItemTemplate>
		</asp:Repeater>
                            </tbody>
                            </table>
		</td>
		</tr>
</tbody>
</table>