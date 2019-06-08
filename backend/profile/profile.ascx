<%@ Control Language="C#" AutoEventWireup="true" CodeFile="profile.ascx.cs" Inherits="profile_profile" %>
<table cellSpacing="5" cellPadding="0" width="95%" align="center" border="0">
	<tr class="Form_Header">
		<td style="PADDING-LEFT: 5px" colSpan="3" height="22" background="<%=UrlImages%>BodyTop.gif"><asp:image id="imgIconHeader" BorderWidth="0" Runat="server"></asp:image>&nbsp;<asp:literal id="ltlHeader" Runat="server" Text="Thông tin cá nhân"></asp:literal></td>
	</tr>
	<tr>
		<td colspan="3" align="center">
			<table width="600"  border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td colspan="3"  style="height:10px">&nbsp;</td>
				</tr>	
				<tr>
					<td width="40%"><asp:literal id="ltlUsername" Runat="server" Text="Tên truy cập"></asp:literal></td>
					<td width="20%"><asp:textbox id="tbxUsername" Runat="server" Enabled="False"></asp:textbox></td>
					<td class="RequireField" width="40%"><asp:literal id="ltlRequireUsername" Runat="server" Text="Phải nhập tên truy cập" Visible="False"></asp:literal></td>
				</tr>
				<tr>
					<td><asp:literal id="ltlFullname" Runat="server" Text="Họ tên"></asp:literal></td>
					<td><asp:textbox id="tbxFullname" Runat="server"></asp:textbox></td>
					<td class="RequireField"><asp:literal id="ltlRequireFullname" Runat="server" Text="Phải nhập họ tên" Visible="False"></asp:literal></td>
				</tr>
				<tr>
					<td><asp:literal id="ltlEmail" Runat="server" Text="Email"></asp:literal></td>
					<td><asp:textbox id="tbxEmail" Runat="server"></asp:textbox></td>
					<td class="RequireField"><asp:literal id="ltlRequireEmail" Runat="server" Text="Phải nhập Email" Visible="False"></asp:literal></td>
				</tr>
				<tr>
					<td><asp:literal id="ltlTel" Runat="server" Text="Điện thoại"></asp:literal></td>
					<td colSpan="2"><asp:textbox id="tbxTel" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:literal id="ltlAddress" Runat="server" Text="Địa chỉ"></asp:literal></td>
					<td colSpan="2"><asp:textbox id="tbxAddress" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:literal id="ltlJobtitle" Runat="server" Text="Chức vụ"></asp:literal></td>
					<td colSpan="2"><asp:textbox id="tbxJobtitle" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="RequireField" colSpan="3"><asp:literal id="ltlNote" Runat="server" Text="*-Thông tin yêu cầu bắt buộc"></asp:literal></td>
				</tr>
				<tr>
					<td align="center" colSpan="3"><asp:button id="btnRegister" Text="Cập nhật" runat="server" OnClick="btnRegister_Click"></asp:button></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
