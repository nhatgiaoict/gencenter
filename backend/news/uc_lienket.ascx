<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_lienket.ascx.cs" Inherits="news_uc_lienket" %>
<%@ Register TagPrefix="cc1" Namespace="uss.pagelist" Assembly="uss.pagelist" %>
<style type="text/css">
    .style1 { width: 299px;}
</style>
<table cellSpacing="5" cellPadding="0" width="95%" align="center" border="0">
	<TBODY>
		<tr class="Form_Header"><td align="left" height="22" background="/data/images/BodyTop.gif" style="PADDING-LEFT: 5px"><img style="border-width:0px;" src="/data/images/icon_menu1.gif" />&nbsp;Tạo liên kết cho bài viết</td>
		</tr>
		<tr><td>
		<table cellSpacing="1" cellPadding="5" width="100%" bgColor="#e8edf6" border="0">
		<tr bgColor="#ffffff">
		<td style="width:20%">Tiêu đề</td>
		<td  colspan="2"><asp:Literal ID="ltltieude" runat="server"> Tiêu đề bài viết</asp:Literal></td>
		</tr>
		<tr bgColor="#ffffff">
		<td colspan="3"><span style="color:Red;"><strong>DANH SÁCH CÁC BÀI VIẾT ĐÃ LỰA CHỌN</strong></span></td>
		</tr>
		<tr bgColor="#ffffff">
			<td colspan="3">
			<asp:repeater id="RptIN" Runat="server" onitemcreated="RptIN_ItemCreated" 
                    onitemdatabound="RptIN_ItemDataBound">
					<HeaderTemplate>
						<table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
							<tr bgcolor="#9dbae9" height="30px">
								<td align="center" width="30px"><strong>STT</strong></td>
								<td style="padding-left:10px"><strong>Tiêu đề bài viết</strong></td>
								<td align="center" width="80px"><strong>Xóa</strong></td>							
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr class="item" id="trItems" runat="server" height="25px">
							<td align="center">
							<asp:Literal ID="ltlIDN" Runat="server"></asp:Literal>
							<asp:Literal ID="ltlIDNewsN" Runat="server" Visible="False"></asp:Literal></td>
							<td align="left" style="padding-left:10px" class="Title_Detail_BE"><a id="ltlTitleN" Runat="server" Title="Xem chi tiết."></a></td>
							<td align="center"><asp:LinkButton ID="lbtDelete" Runat="server">Xóa</asp:LinkButton></td>							
						</tr>
					</ItemTemplate>
					<FooterTemplate></table></FooterTemplate>
					</asp:repeater>
</td></tr>
<tr><td colspan="2" style="text-align:right;"><asp:Button ID="btnlkcheo" 
        runat="server" Text="Cập nhật liên kết chéo" onclick="btnlkcheo_Click" />(<span style="color:Red;">Thông tin bắt buộc khi tạo xong liên kết cho một bài viết</span>)</td></tr>
		</table>
		</td>
		</tr>
		
		<tr>
			<td>
				<table cellSpacing="1" cellPadding="5" width="100%" bgColor="#e8edf6" border="0">
					<tr bgColor="#ffffff">
						<td><asp:Literal ID="ltlGroup" Runat="server" Text="Thuộc nhóm"></asp:Literal></td>
						<td align=left style="padding-left: 5px" colspan="2">
                            <div style="clear: both; border-right: #0070b5 1px solid; padding-right: 5px; border-top: #0070b5 1px solid;
                                padding-left: 5px; padding-bottom: 5px; overflow: auto; border-left: #0070b5 1px solid;
                                padding-top: 5px; border-bottom: #0070b5 1px solid; width: 300px; height: 150px">
                                <asp:TreeView ID="treeview1" runat="server" OnTreeNodePopulate="treeview1_TreeNodePopulate">
                                </asp:TreeView>
                            </div>
                        </td>
					</tr>
					<tr bgColor="#ffffff">
						<td style="PADDING-LEFT: 20px" width="20%"><asp:literal id="ltlKeyword" Runat="server" Text="Từ khoá"></asp:literal></td>
						<td style="PADDING-RIGHT: 20px" class="style1"><asp:textbox id="txtKeyword" Runat="server" Width="310px"></asp:textbox></td>
						<td align="left"><asp:imagebutton id="imgBtn" runat="server" ImageUrl="/data/images/search_new.gif" OnClick="imgBtn_Click"></asp:imagebutton></td>
					</tr>
					<tr bgColor="#ffffff">
					<td colspan="3"><span style="color:Red;"><strong>DANH SÁCH CÁC BÀI VIẾT TÌM THẤY</strong></span></td>
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
			<td colspan="3">
			<asp:repeater id="rptNews" Runat="server" OnItemCreated="rptNews_ItemCreated" OnItemDataBound="rptNews_ItemDataBound">
					<HeaderTemplate>
						<table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
							<tr bgcolor="#9dbae9" height="30px">
								<td align="center" width="30px"><strong>STT</strong></td>
								<td style="padding-left:10px"><strong>Tiêu đề bài viết</strong></td>
							    <td align="left" style="width:120px; padding-left:10px"><strong>Ngày cập nhật</strong></td>
								<td align="center" width="80px"><strong>Chọn</strong></td>							
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr class="item" id="trItems" runat="server" height="25px">
							<td align="center"><asp:Literal ID="ltlID" Runat="server"></asp:Literal>
							<asp:Literal ID="ltlIDNews" Runat="server" Visible="False"></asp:Literal></td>
							<td align="left" style="padding-left:10px" class="Title_Detail_BE"><a id="ltlTitle" Runat="server" Title="Xem chi tiết."></a></td>
							<td align="left" style="padding-left:5px"><asp:Literal ID="ltlCreated" Runat="server"></asp:Literal></td>
							<td align="center"><asp:LinkButton ID="lbtSelect" Runat="server">Chọn</asp:LinkButton></td>							
						</tr>
					</ItemTemplate>
					<FooterTemplate></table></FooterTemplate>
					</asp:repeater>
</td></tr>
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