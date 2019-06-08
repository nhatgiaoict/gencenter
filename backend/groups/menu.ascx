<%@ Control Language="C#" AutoEventWireup="true" CodeFile="menu.ascx.cs" Inherits="groups_menu" %>
<table cellSpacing="5" cellPadding="0" width="95%" align="center" border="0">
	<TBODY>
		<tr class="Form_Header">
			<td style="PADDING-LEFT: 5px" height="22" background="<%=UrlImages%>BodyTop.gif"><asp:image id="imgIconHeader" Runat="server" BorderWidth="0"></asp:image>&nbsp;<asp:dropdownlist id="ddlMenu" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged"></asp:dropdownlist></td>
		</tr>
		<tr>
			<td><asp:repeater id="rptMenu" Runat="server" OnItemCreated="rptMenu_ItemCreated" OnItemDataBound="rptMenu_ItemDataBound" >
					<HeaderTemplate>
						<table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
							<tr bgcolor="#9dbae9" height="30px">
								<td align="center" width="5%"><strong><asp:Literal ID="ltlIndexHeader" Runat="server" Text="STT"></asp:Literal></strong></td>
								<td width="45%" style="padding-left:10px"><strong><asp:Literal ID="ltlTitleHeader" Runat="server" Text="Tiêu đề"></asp:Literal></strong></td>
								<td width="45%" style="padding-left:10px"><strong><asp:Literal ID="ltlSummaryHeader" Runat="server" Text="Mô tả"></asp:Literal></strong></td>
								<td align="center" width="5%"><strong><asp:Literal ID="ltlDeleteHeader" Runat="server" Text="Xoá"></asp:Literal></strong></td>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr class="item" id="trItems1" runat="server" height="25px">
							<td align="center">
								<asp:Literal ID="ltlID" Runat="server" Visible="False"></asp:Literal>
								<asp:TextBox ID="txtIdx" Runat="server" Width="30px"></asp:TextBox>
							</td>
							<td style="padding-left:10px"><asp:Literal ID="ltlTitle" Runat="server"></asp:Literal></td>
							<td style="padding-left:10px"><asp:Literal ID="ltlSummary" Runat="server"></asp:Literal></td>
							<td align="center"><asp:LinkButton ID="lbtDelete" Runat="server">
									<img id="imgDelete" runat="server" src="http://localhost/data/images/delete.gif" alt="Delete"
										width="13" height="13" border="0"></asp:LinkButton></td>
						</tr>
					</ItemTemplate>
					<FooterTemplate>
</table>
</FooterTemplate></asp:repeater></TD></TR>
<tr bgColor="#ffffff">
	<td align="center"><asp:button id="Button1" Text="Cập nhật số thứ tự" runat="server" OnClick="Button1_Click"></asp:button></td>
</tr>
</TBODY></TABLE>
<table cellSpacing="5" cellPadding="0" width="95%" align="center" border="0">
	<TBODY>
		<tr>
			<td style="PADDING-LEFT: 5px"><strong><asp:literal id="ltlHeader1" Text="Danh sách chuyên mục tự do" Runat="server"></asp:literal></strong></td>
		</tr>
		<tr>
			<td><asp:repeater id="rptGroup" Runat="server" OnItemCreated="rptGroup_ItemCreated" OnItemDataBound="rptGroup_ItemDataBound">
					<HeaderTemplate>
						<table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
							<tr bgcolor="#9dbae9" height="30px">
								<td align="center" width="5%"><strong><asp:Literal ID="ltlIdxHeader1" Runat="server" Text="STT"></asp:Literal></strong></td>
								<td width="45%" style="padding-left:10px"><strong><asp:Literal ID="ltlTitleHeader1" Runat="server" Text="Tiêu đề"></asp:Literal></strong></td>
								<td width="45%" style="padding-left:10px"><strong><asp:Literal ID="ltlSummaryHeader1" Runat="server" Text="Mô tả"></asp:Literal></strong></td>
								<td align="center" width="5%"><strong><asp:Literal ID="ltlSelectHeader1" Runat="server" Text="Chọn"></asp:Literal></strong></td>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr class="item" id="trItems2" runat="server" height="25px">
							<td align="center">
								<asp:Literal ID="ltlIndex1" Runat="server"></asp:Literal>
							</td>
							<td style="padding-left:10px"><asp:HyperLink ID="hlTitle1" Runat="server"></asp:HyperLink></td>
							<td style="padding-left:10px"><asp:Literal ID="ltlSummary1" Runat="server"></asp:Literal></td>
							<td align="center"><asp:LinkButton ID="lbtSelect1" Runat="server">Chọn</asp:LinkButton></td>
						</tr>
					</ItemTemplate>
					<FooterTemplate>
</table>
</FooterTemplate></asp:repeater></TD></TR></TBODY></TABLE>

