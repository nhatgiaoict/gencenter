<%@ Control Language="C#" AutoEventWireup="true" CodeFile="add_con.ascx.cs" Inherits="advert_add_con"%>
<table cellSpacing="5" cellPadding="0" width="95%" align="center" border="0">
	<tr class="Form_Header">
		<td align="left" style="PADDING-LEFT: 5px" height="22" background="<%=UrlImages%>BodyTop.gif"><asp:image id="imgIconHeader" Runat="server" BorderWidth="0"></asp:image>&nbsp;<asp:literal id="ltlHeader" Runat="server" Text="Thêm mới quảng cáo"></asp:literal></td>
	</tr>
	<tr><td>
	<table cellpadding="0" cellspacing="0" width="50%">
	<tr><td>Loại quảng cáo : </td><td>
                        <asp:RadioButton ID="RdAnh" runat="server" 
                            Text="Ảnh, Flash" AutoPostBack="True" 
                            oncheckedchanged="RdAnh_CheckedChanged" Checked="True" /></td><td><asp:RadioButton ID="rdHtml" 
                                runat="server" Text="HTML" AutoPostBack="True" 
                                oncheckedchanged="rdHtml_CheckedChanged" /> </td></tr>
	</table>
	</td></tr>
	<tr>
		<td>
			<table cellpadding="0" cellspacing="0" width="100%" border="0">
				<tr valign="top">
					<td style="width:60%">
						<table cellpadding="3" cellspacing="3" width="100%" align="right" border="0" id="tblAnh" runat="server">
							<tr>
								<td align="left"><asp:literal id="ltlTitle" Runat="server" Text="Tiêu đề"></asp:literal></td>
								<td align="left"><asp:TextBox ID="tbxTitle" Runat="server"  Width="250"></asp:TextBox></td>
								<td align="left" class="RequireField"><asp:literal id="ltlRequiredTitle" Runat="server" Text="Phải nhập tiêu đề" Visible="False"></asp:literal></td>
							</tr>
							<tr>
								<td align="left"><asp:Literal ID="ltlLink" Runat="server" Text="Liên kết đến"></asp:Literal></td>
								<td align="left"><asp:TextBox ID="tbxLink" Runat="server" Width="250"></asp:TextBox></td>
								<td align="left" class="RequireField"><asp:literal id="ltlRequiredLink" Runat="server" Text="Phải nhập liên kết đến" Visible="False"></asp:literal></td>
							</tr>
							<tr>
								<td align="left">Ảnh quảng cáo</td>
								<td align="left"><asp:TextBox ID="txtFileImages" Runat="server" Width="250px"></asp:TextBox>
									<img id="imgSelect" runat="server" scr="http://localhost/data/imgages/folder.gif"></td>
								<td align="left" class="RequireField"><asp:Literal ID="ltlRequiredLogo" runat="server"></asp:Literal></td>
							</tr>
							<tr>
								<td align="left"><asp:Literal ID="ltlSize" Runat="server" Text="Kích thước"></asp:Literal></td>
								<td align="left">Rộng: <asp:TextBox ID="txtWidth" Runat="server" Width="40px" Text="302"></asp:TextBox>&nbsp;Cao: <asp:TextBox ID="txtHeight" Runat="server" Width="40px" Text="150"></asp:TextBox></td>
								<td align="left" class="RequireField">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                        ControlToValidate="txtHeight" ErrorMessage="Nhập sai chiều cao" 
                                        ValidationExpression="^\d+"></asp:RegularExpressionValidator>
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                        ErrorMessage="Nhập sai chiều rộng" ControlToValidate="txtWidth" 
                                        ValidationExpression="^\d+"></asp:RegularExpressionValidator>
                                </td>
							</tr>
							<tr>
								<td align="left"><asp:Literal ID="ltlSummary" Runat="server" Text="Mô tả tóm tắt"></asp:Literal></td>
								<td align="left" colspan="2"><asp:TextBox ID="tbxSummary" Runat="server" TextMode="MultiLine"></asp:TextBox></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<table cellpadding="0" cellspacing="0" width="100%" border="0" id="tblFlash" runat="server" visible="false">
				<tr valign="top">
					<td style="width:60%">
						<table cellpadding="3" cellspacing="3" width="100%" align="right" border="0">
							<tr>
								<td align="left">Tên quảng cáo</td>
								<td align="left"><asp:TextBox ID="txttitlehtml" Runat="server"  Width="250"></asp:TextBox></td>
								<td align="left" class="RequireField"><asp:literal id="ltlReten" Runat="server" Text="Phải nhập tiêu đề" Visible="False"></asp:literal></td>
							</tr>
							<tr>
								<td align="left">Nội dung (mã HTML)</td>
								<td align="left"><asp:TextBox ID="txtContenthtml" Runat="server" Width="400" Height="200" TextMode="MultiLine"></asp:TextBox></td>
								<td align="left" class="RequireField"><asp:literal id="ltlReNoidunghtm" Runat="server" Text="Phải nhập nội dung quảng cáo html" Visible="False"></asp:literal></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="left" class="RequireField"><asp:literal id="ltlNote" Runat="server" Text="*-Thông tin yêu cầu bắt buộc"></asp:literal></td>
	</tr>
	<tr>
		<td align="center"><asp:button id="btnAddnew" Text="Cập nhật" runat="server" OnClick="btnAddnew_Click"></asp:button></td>
	</tr>
</table>