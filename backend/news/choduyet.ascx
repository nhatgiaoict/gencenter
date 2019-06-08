<%@ Control Language="C#" AutoEventWireup="true" CodeFile="choduyet.ascx.cs" Inherits="news_choduyet" %>
<%@ Register TagPrefix="cc1" Namespace="uss.pagelist" Assembly="uss.pagelist" %>
<table cellSpacing="5" cellPadding="0" width="95%" align="center" border="0">
	<TBODY>
		<tr class="Form_Header">
			<td style="PADDING-LEFT: 5px" height="22" background="<%=UrlImages%>BodyTop.gif"><asp:image id="imgIconHeader" BorderWidth="0" Runat="server"></asp:image>&nbsp;<asp:literal id="ltlHeader" Runat="server" Text="Danh sách bài biên tập"></asp:literal></td>
		</tr>
		<tr>
			<td>
				<table cellSpacing="1" cellPadding="5" width="100%" bgColor="#e8edf6" border="0">
					<tr bgColor="#ffffff">
						<td><asp:Literal ID="ltlGroup" Runat="server" Text="Thuộc nhóm"></asp:Literal></td>
						<td align=left style="padding-left: 5px" colspan="4">
                            <div style="clear: both; border-right: #0070b5 1px solid; padding-right: 5px; border-top: #0070b5 1px solid;
                                padding-left: 5px; padding-bottom: 5px; overflow: auto; border-left: #0070b5 1px solid;
                                padding-top: 5px; border-bottom: #0070b5 1px solid; width: 300px; height: 150px">
                                <asp:TreeView ID="treeview1" runat="server" OnTreeNodePopulate="treeview1_TreeNodePopulate">
                                </asp:TreeView>
                            </div>
                        </td>
					</tr>
					<tr bgColor="#ffffff">
						<td style="PADDING-LEFT: 20px; height: 20px;" width="20%"><asp:literal id="ltlKeyword" Runat="server" Text="Từ khoá"></asp:literal></td>
						<td style="PADDING-RIGHT: 20px; width: 280px; height:20px;"><asp:textbox id="txtKeyword" Runat="server"></asp:textbox></td>
						<td style="PADDING-RIGHT: 20px; height: 40px;" align="left"><asp:imagebutton id="imgBtn" runat="server" ImageUrl="http://localhost/data/images/search_new.gif" OnClick="imgBtn_Click"></asp:imagebutton></td>
						<td style="height: 40px"></td>
						<td style="height: 40px"></td>
					</tr>
				</table>
			</td>
		</tr>
		<tr height="22">
			<td>
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td align=left style="PADDING-LEFT: 10px" width="50%"><asp:literal id="ltlTotal" Runat="server" Text="Tìm thấy..."></asp:literal></td>
						<td style="PADDING-RIGHT: 10px" align="right" width="50%">
							<cc1:PageList id="PageList1" runat="server" m_pIconPath="http://localhost/data.tcdl/images/"></cc1:PageList></td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td><asp:repeater id="rptNews" Runat="server" OnItemCreated="rptNews_ItemCreated" OnItemDataBound="rptNews_ItemDataBound">
					<HeaderTemplate>
						<table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
							<tr bgcolor="#9dbae9" height="30px">
								<td>&nbsp;</td>
								<td align="center" width="30px"><strong><asp:Literal ID="ltlIndexHeader" Runat="server" Text="STT"></asp:Literal></strong></td>
								<td style="padding-left:10px"><strong><asp:Literal ID="ltlTitleHeader" Runat="server" Text="Tiêu đề"></asp:Literal></strong></td>
								<td width="130px" style="padding-left:10px"><strong><asp:Literal ID="ltlLastUpdateHeader" Runat="server" Text="Ngày cập nhật"></asp:Literal></strong></td>
								<td align="center" width="50px"><strong><asp:Literal ID="ltlSendHeader" Runat="server" Text="Gửi"></asp:Literal></strong></td>
								<td align="center" width="60px"><strong><asp:Literal ID="ltlReturnHeader" Runat="server" Text="Trả lại"></asp:Literal></strong></td>
								<td align="center" width="40px"><strong><asp:Literal ID="ltlLogHeader" Runat="server" Text="Xem Log"></asp:Literal></strong></td>
								<td align="center" width="40px"><strong><asp:Literal ID="ltlEditHeader" Runat="server" Text="Sửa"></asp:Literal></strong></td>
								
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr class="item" id="trItems" runat="server" height="25px">
							<td align="center" style="width:30px" width="30px"><asp:CheckBox ID="checkboxID" Runat="server"></asp:CheckBox>
								<asp:Literal ID="ltlIDNews" Runat="server" Visible="False"></asp:Literal>
							</td>
							<td align="center">
								<asp:Literal ID="ltlID" Runat="server"></asp:Literal>
							</td>
							<td align=left style="padding-left:10px"><asp:HyperLink ID="ltlTitle" Runat="server" Title="View detail."></asp:HyperLink></td>
							<td style="padding-left:10px"><asp:Literal ID="ltlCreated" Runat="server"></asp:Literal></td>
							<td align="center"><asp:LinkButton ID="lbtSend" Runat="server">
									<img id="imgSend" runat="server" src="http://localhost/data/images/icon_submit.gif"
										title="Send" alt="Send" border="0"></asp:LinkButton></td>
							<td align="center"><asp:LinkButton ID="lbtReturn" Runat="server">
									<img id="imgReturn" runat="server" src="http://localhost/data/images/icon_return.gif"
										title="Return" alt="Return" border="0"></asp:LinkButton></td>
							<td align="center"><img id="imgHistory" runat="server" title="ViewLog" alt="ViewLog" border="0"></td>
							<td align="center"><a href="#" id="hrEdit" runat="server" title="Edit"></a></td>							
						</tr>
					</ItemTemplate>
					<FooterTemplate>
</table>
</FooterTemplate></asp:repeater></TD></TR>
<tr bgColor="#ffffff">
	<td align=left><asp:Button id="BtDelete" Text="Delete" runat="server" OnClick="BtDelete_Click"></asp:Button>&nbsp;&nbsp;
		<asp:Button id="BtSend" Text="Gui" runat="server" OnClick="BtSend_Click"></asp:Button>&nbsp;&nbsp;
		<asp:Button id="BtReturn" Text="Tra lai" runat="server" OnClick="BtReturn_Click"></asp:Button></td>
</tr>
<tr bgColor="#ffffff" height="22">
	<td>
		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td align=left style="PADDING-LEFT: 10px" width="50%"><asp:literal id="ltlTotal1" Runat="server" Text="Tìm thấy.."></asp:literal></td>
				<td style="PADDING-RIGHT: 10px" align="right" width="50%">
					<cc1:PageList id="PageList2" runat="server" m_pIconPath="http://localhost/data.tcdl/images/"></cc1:PageList>
				</td>
			</tr>
		</table>
	</td>
</tr>
</TBODY></TABLE>

