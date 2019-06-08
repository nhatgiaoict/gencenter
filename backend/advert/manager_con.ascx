<%@ Control Language="C#" AutoEventWireup="true" CodeFile="manager_con.ascx.cs" Inherits="advert_manager_con" %>
<%@ Register TagPrefix="cc1" Namespace="uss.pagelist" Assembly="uss.pagelist" %>
<table cellpadding="5" cellspacing="0" width="95%" align="center" border="0">
	<TBODY>
		<tr class="Form_Header">
			<td align="left" style="PADDING-LEFT: 5px" style="height:22" background="<%=UrlImages%>BodyTop.gif"><asp:image id="imgIconHeader" BorderWidth="0" Runat="server"></asp:image>&nbsp;<asp:literal id="ltlHeader" Runat="server" Text="Quản trị quảng cáo"></asp:literal></td>
		</tr>
		<tr height="22">
			<td>
				<table cellspacing="0" cellpadding="0" width="100%" border="0">
					<tr>
						<td align="left" style="PADDING-LEFT: 10px" width="50%"><asp:literal id="ltlTotal" Runat="server" Text="Tìm thấy..."></asp:literal></td>
						<td style="PADDING-RIGHT: 10px" align="right" width="50%">
							<cc1:PageList id="PageList1" runat="server" m_pIconPath="http://hieudt/data.tcdl/images/"></cc1:PageList></td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td><asp:repeater id="rptAdvert" Runat="server" OnItemCreated="rptAdvert_ItemCreated" OnItemDataBound="rptAdvert_ItemDataBound">
					<HeaderTemplate>
						<table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
							<tr bgcolor="#9dbae9" height="30px">
								<td align="center" width="5%"><strong><asp:Literal ID="ltlIndexHeader" Runat="server" Text="STT"></asp:Literal></strong></td>
								<td width="30%" align="left" style="padding-left:10px"><strong><asp:Literal ID="ltlTitleHeader" Runat="server" Text="Tiêu đề"></asp:Literal></strong></td>
								<td width="150px" align="center"><strong><asp:Literal ID="ltlLogoHeader" Text="Ảnh" Runat="server"></asp:Literal></strong></td>
								<td width="17%" align="center"  style="padding-left:10px"><strong><asp:Literal ID="ltlLastUpdateHeader" Runat="server" Text="Ngày cập nhật"></asp:Literal></strong></td>
								<td align="center"><strong><asp:Literal ID="ltlClickCountHeader" Runat="server" Text="Số lần xem"></asp:Literal></strong></td>
								<td align="center" width="8%"><strong><asp:Literal ID="ltlStatusHeader" Runat="server" Text="Trạng thái"></asp:Literal></strong></td>
								<td align="center" width="5%"><strong><asp:Literal ID="ltlEditHeader" Runat="server" Text="Sửa"></asp:Literal></strong></td>
								<td align="center" width="5%"><strong><asp:Literal ID="ltlDeleteHeader" Runat="server" Text="Xoá"></asp:Literal></strong></td>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr class="item" id="trItems" runat="server" height="25px">
							<td align="center"><asp:Literal ID="ltlID" Runat="server"></asp:Literal></td>
							<td align="left" style="padding-left:10px"><a id="Hlinktitle" runat="server"></a></td>
							<td align="center"><asp:Literal ID="ltlLogo" Runat="server"></asp:Literal></td>
							<td align="center" style="padding-left:10px"><asp:Literal ID="ltlCreated" Runat="server"></asp:Literal></td>
							<td align="center"><asp:Literal ID="ltlClickCount" Runat="server"></asp:Literal></td>
							<td align="center"><asp:LinkButton ID="lbtOnline" Runat="server"></asp:LinkButton></td>
							<td align="center"><a href="#" id="hrEdit" runat="server"></a></td>
							<td align="center"><asp:LinkButton ID="lbtDelete" Runat="server">
									<img id="imgDelete" runat="server" src="http://quanmh/dat.bktt/images/delete.gif" alt="Delete"
										width="13" height="13" border="0"></asp:LinkButton></td>
						</tr>
					</ItemTemplate>
					<FooterTemplate>
</table>
</FooterTemplate></asp:repeater></TD></TR>
<tr bgColor="#ffffff" height="22">
	<td>
		<table cellspacing="0" cellpadding="0" width="100%" border="0">
			<tr>
				<td align="left" style="PADDING-LEFT: 10px" width="50%"><asp:literal id="ltlTotal1" Runat="server" Text="Tìm thấy.."></asp:literal></td>
				<td align="right" style="PADDING-RIGHT: 10px" align="right" width="50%">
					<cc1:PageList id="PageList2" runat="server" m_pIconPath="http://hieudt/data.tcdl/images/"></cc1:PageList>
				</td>
			</tr>
		</table>
	</td>
</tr>
</TBODY></table>
