<%@ Control Language="C#" AutoEventWireup="true" CodeFile="changepwd.ascx.cs" Inherits="profile_changepwd" %>
<table cellSpacing="5" cellPadding="0" width="95%" align="center" border="0">
	<tr class="Form_Header">
		<td style="PADDING-LEFT: 5px" colSpan="3" height="22" background="<%=UrlImages%>BodyTop.gif"><asp:image id="imgIconHeader" BorderWidth="0" Runat="server"></asp:image>&nbsp;<asp:literal id="ltlHeader" Runat="server" Text="Đổi mật khẩu"></asp:literal></td>
	</tr>
	<tr>
		<td colspan="3">
			<table width="600" align="center" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td height="10px" colSpan="3"></td>
				</tr>
				<tr>
					<td align=left class="RequireField" colSpan="3"><asp:literal id="ltlNote" Runat="server" Text="(*)- Thông tin yêu cầu bắt buộc."></asp:literal></td>
				</tr>
				<tr>
					<td height="10px" colSpan="3"></td>
				</tr>
				<tr>
					<td width=30%><asp:literal id="ltlPassword" Runat="server" Text="Mật khẩu cũ "></asp:literal></td>
					<td align= center width=30%><asp:textbox id="tbxPassword" Runat="server" TextMode="Password"></asp:textbox></td>
					<td align= left width=30% class="RequireField">&nbsp;<asp:literal id="ltlRequirePassword" Runat="server" Text="Phải nhập mật khẩu !" Visible="False"></asp:literal></td>
				</tr>
				<tr>
					<td ><asp:literal id="ltlNewPassword" Runat="server" Text="Mật khẩu mới"></asp:literal></td>
					<td align= center><asp:textbox id="tbxNewPassword" Runat="server" TextMode="Password"></asp:textbox></td>
					<td class="RequireField">&nbsp;<asp:literal id="ltlRequireNewPassword" Runat="server" Text="Phải nhập mật khẩu mới !" Visible="False"></asp:literal></td>
				</tr>
				<tr>
					<td ><asp:literal id="ltlRetypePassword" Runat="server" Text="Nhập lại mật khẩu !"></asp:literal></td>
					<td align= center><asp:textbox id="tbxRetypePasword" Runat="server" TextMode="Password"></asp:textbox></td>
					<td class="RequireField">&nbsp;<asp:literal id="ltlRequireRetypePassword" Runat="server" Text="Phải nhập lại mật khẩu mới !" Visible="False"></asp:literal></td>
				</tr>
				<tr>
					<td height="10px" colSpan="3"></td>
				</tr>
				<tr>
					<td align="center" colSpan="3"><asp:button id="btnRegister" Text="Cập nhật" runat="server" OnClick="btnRegister_Click"></asp:button></td>
				</tr>
			</table>
		</td>
	</tr>
</table>

