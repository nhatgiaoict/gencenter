<%@ Control Language="C#" AutoEventWireup="true" CodeFile="register.ascx.cs" Inherits="member_register" %>

<table cellSpacing="5" cellPadding="0" width="95%" align="center" border="0">
	<tr class="Form_Header">
		<td style="PADDING-LEFT: 5px" colSpan="3" height="22" background="<%=UrlImages%>BodyTop.gif"><asp:image id="imgIconHeader" Runat="server" BorderWidth="0"></asp:image>&nbsp;<asp:literal id="ltlHeader" Runat="server" Text="Thêm mới thành viên"></asp:literal></td>
	</tr>
	<tr>
		<td colspan="3" style="height:10px">&nbsp;</td>
	</tr>	
	<tr>
		<td width="20%"><asp:literal id="ltlUsername" Runat="server" Text="Tên truy cập"></asp:literal></td>
		<td width="20%"><asp:textbox id="tbxUsername" Runat="server"></asp:textbox></td>
		<td class="RequireField" width="60%"><asp:literal id="ltlRequireUsername" Runat="server" Text="Phải nhập tên truy cập" Visible="False"></asp:literal></td>
	</tr>
	<tr>
		<td><asp:literal id="ltlPassword" Runat="server" Text="Mật khẩu"></asp:literal></td>
		<td><asp:textbox id="tbxPassword" Runat="server" TextMode="Password"></asp:textbox></td>
		<td class="RequireField"><asp:literal id="ltlRequirePassword" Runat="server" Text="Phải nhập mật khẩu" Visible="False"></asp:literal></td>
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
		<td colSpan="3"><strong><asp:literal id="ltlRole" Runat="server" Text="Chức năng quản trị"></asp:literal></strong>&nbsp;
			<font color="#ff0000">
				<asp:literal id="ltlRequireRole" Runat="server" Text="Phải chọn ít nhất 1 chức năng quản trị"
					Visible="False"></asp:literal></font></td>
	</tr>
	<tr>
		<td style="PADDING-LEFT: 20px" colSpan="3">
			<table cellSpacing="5" cellPadding="0" width="100%" border="0">
				<!------>
				<tr>
					<td><asp:checkbox id="cbxAdministrator" Text="Quản trị hệ thống" runat="server"></asp:checkbox></td>
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
					<td><asp:checkbox id="cbxTochuc" Text="Quản trị tổc chuc tin tức" runat="server"></asp:checkbox></td>
				</tr>
				<tr>
					<td><asp:checkbox id="cbxPostOn" Text="Post bai len thang" runat="server"></asp:checkbox></td>
				</tr> 
				<!------>
				
			</table>
		</td>
	</tr>
	<tr>
		<td class="RequireField" colSpan="3"><asp:literal id="ltlNote" Runat="server" Text="*-Thông tin yêu cầu bắt buộc"></asp:literal></td>
	</tr>
	<tr>
		<td align="center" ></td>
		<td align="center" ><asp:button id="btnRegister" Text="Cập nhật" runat="server" OnClick="btnRegister_Click"></asp:button></td>
		<td align="center" ></td>
	</tr>
</table>