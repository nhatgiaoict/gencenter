<%@ Control Language="C#" AutoEventWireup="true" CodeFile="update.ascx.cs" Inherits="member_update" %>
<table cellSpacing="5" cellPadding="0" width="95%" align="center" border="0">
	<tr class="Form_Header">
		<td align=left style="PADDING-LEFT: 5px" colSpan="3" height="22" background="<%=UrlImages%>BodyTop.gif"><asp:image id="imgIconHeader" BorderWidth="0" Runat="server"></asp:image>&nbsp;<asp:literal id="ltlHeader" Runat="server" Text="Cập nhật quyền thành viên"></asp:literal></td>
	</tr>
	<tr>
		<td height="5">&nbsp;</td>
	</tr>
	<tr>
		<td align=left colspan="3" style="PADDING-LEFT:15px"><b><asp:Literal ID="ltlMember" Runat="server" Text="Quanmh"></asp:Literal></b></td>
	</tr>
	<tr>
		<td height="10">&nbsp;</td>
	</tr>
	<tr>
		<td align=left colSpan="2"><strong><asp:literal id="ltlRole" Runat="server" Text="Chức năng quản trị"></asp:literal></strong></td>
		<td class="RequireField"><asp:literal id="ltlRequireRole" Runat="server" Text="Phải chọn ít nhất 1 chức năng quản trị"
				Visible="False"></asp:literal></td>
	</tr>
	<tr>
		<td style="PADDING-LEFT: 30px" colSpan="3" align=left>
			<table cellSpacing="5" cellPadding="0" width="100%" border="0">
				
				<tr>
					<td ><asp:checkbox id="cbxAdministrator" Text="Quản trị hệ thống" runat="server"></asp:checkbox></td>
				</tr>
				<tr>
					<td><asp:checkbox id="cbxThanhvien" Text="Quản trị thành viên" runat="server"></asp:checkbox></td>
				</tr>
				<tr>
					<td><asp:checkbox id="cbxChuyenmuc" Text="Quản trị chuyên mục" runat="server"></asp:checkbox></td>
				</tr>
				<tr>
					<td><asp:checkbox id="cbxQTNoidung" Text="Quản trị noi dung" runat="server"></asp:checkbox></td>
				</tr>
				<tr>
					<td><asp:checkbox id="cbxPostOn" Text="Post bai len thang" runat="server"></asp:checkbox></td>
				</tr> 
			</table>
		</td>
	</tr>
	<tr>
		<td align=left class="RequireField" colSpan="3"><asp:literal id="ltlNote" Runat="server" Text="*-Thông tin yêu cầu bắt buộc"></asp:literal></td>
	</tr>
	<tr>
		<td align=left style="PADDING-LEFT: 50px" colSpan="3"><asp:button id="btnRegister" Text="Cập nhật" runat="server" OnClick="btnRegister_Click"></asp:button></td>
	</tr>
</table>

