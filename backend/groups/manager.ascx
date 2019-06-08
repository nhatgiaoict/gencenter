<%@ Control Language="C#" AutoEventWireup="true" CodeFile="manager.ascx.cs" Inherits="groups_manager" %>
<%@ Register TagPrefix="cc1" Namespace="uss.pagelist" Assembly="uss.pagelist" %>
<table cellSpacing="5" cellPadding="0" width="95%" align="center" border="0">
	<TBODY>
		<tr class="Form_Header">
			<td style="PADDING-LEFT: 5px" height="22" background="<%=UrlImages%>BodyTop.gif"><asp:image id="imgIconHeader" BorderWidth="0" Runat="server"></asp:image>&nbsp;<asp:literal id="ltlHeader" Runat="server" Text="Quản trị chuyên mục"></asp:literal></td>
		</tr>
		<tr class="item"><td height="22"><asp:HyperLink ID="hlNavigator" Runat="server">Tin tức</asp:HyperLink></td></tr>
		<tr>
			<td><asp:repeater id="rptGroup" Runat="server" OnItemCreated="rptGroup_ItemCreated" OnItemDataBound="rptGroup_ItemDataBound">
					<HeaderTemplate>
						<table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
							<tr bgcolor="#9dbae9" height="30px">
								<td align="center" width="5%"><strong><asp:Literal ID="ltlIndexHeader" Runat="server" Text="STT"></asp:Literal></strong></td>
								<td width="37%" style="padding-left:10px"><strong><asp:Literal ID="ltlTitleHeader" Runat="server" Text="Tiêu đề"></asp:Literal></strong></td>
								<td align="center" width="10%"><strong><asp:Literal ID="ltlStatushome" Runat="server" Text="Home"></asp:Literal></strong></td>
								<td align="center" width="10%"><strong><asp:Literal ID="ltlStatusHeader" Runat="server" Text="Trạng thái"></asp:Literal></strong></td>
								<td align="center" width="10%"><strong>Thuộc tính</strong></td>
								<td align="center" width="10%"><strong><asp:Literal ID="ltlInquiryHeader" Runat="server" Text="Liên hệ"></asp:Literal></strong></td>
								<td align="center" width="10%"><strong><asp:Literal ID="ltlKind" Runat="server" Text="Loại"></asp:Literal></strong></td>
								<td align="center" width="5%"><strong><asp:Literal ID="ltlEditHeader" Runat="server" Text="Sửa"></asp:Literal></strong></td>
								<td align="center" width="5%"><strong><asp:Literal ID="ltlDeleteHeader" Runat="server" Text="Xoá"></asp:Literal></strong></td>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr class="item" id="trItems" runat="server" height="25px">
							<td align="center">
								<asp:Literal ID="ltlID" Runat="server"  Visible=false></asp:Literal>
								<asp:TextBox ID="ltlID_Index" Runat="server" Width="30px" ></asp:TextBox>
							</td>
							<td style="padding-left:10px"><asp:HyperLink ID="hlTitle" Runat="server"></asp:HyperLink></td>
							<td align="center"><asp:LinkButton ID="lbtHome" Runat="server"></asp:LinkButton></td>
							<td align="center"><asp:LinkButton ID="lbtOnline" Runat="server"></asp:LinkButton></td>
							<td align="center" width="10%"><strong><a id="hlinkthuoctinh" runat="server" href="#"></a></strong></td>
							<td align="center"><asp:LinkButton ID="lbtInquiry" Runat="server"></asp:LinkButton></td>
	                        <td align="center"><asp:Literal ID="ltlkind" runat="server"></asp:Literal></td>
							<td align="center"><a href="#" id="hrEdit" runat="server"></a></td>
							<td align="center" >
                                <asp:CheckBox ID="checkboxID" runat="server"></asp:CheckBox>
                                <asp:Literal ID="ltlIDChuyenmuc" runat="server" Visible="False"></asp:Literal>
                            </td>
							
						</tr>
					</ItemTemplate>
					<FooterTemplate>
</table>
</FooterTemplate></asp:repeater></TD></TR>
<tr bgColor="#ffffff">
	<td align="right">
	<asp:button id="BtSTT" Text="Cap Nhat STT" runat="server" OnClick="BtSTT_Click" ></asp:button>
	&nbsp;&nbsp;&nbsp;
	<asp:button id="BtDelete" Text="Delete" runat="server" OnClick="BtDelete_Click"></asp:button>
	&nbsp;&nbsp;
	</td>
</tr>
<tr bgColor="#ffffff" height="22">
	<td>
		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td style="PADDING-LEFT: 10px" width="50%"><asp:literal id="ltlTotal" Runat="server" Text="Tìm thấy.."></asp:literal></td>
				<td style="PADDING-RIGHT: 10px" align="right" width="50%">
					<cc1:PageList id="PageList" runat="server" m_pIconPath="http://localhost/data/images/"></cc1:PageList>
				</td>
			</tr>
		</table>
	</td>
</tr>

</TBODY></TABLE>

