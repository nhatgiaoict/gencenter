<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_managerthuoctinh.ascx.cs" Inherits="Boloc_uc_managerthuoctinh" %>
<table width="95%" cellspacing="5" cellpadding="0" border="0" align="center">
	<tbody>
		<tr class="Form_Header">
			<td colspan="3" height="22" background="/data/images/BodyTop.gif" style="PADDING-LEFT: 5px">
			<img style="border-width:0px;" src="/data/images/icon_menu1.gif" />&nbsp;Quản trị các thuộc tính của chuyên mục</td>
		</tr>
		<tr class="item"><td height="10" colspan="3"></td></tr>
		<tr><td colspan="3"><strong>Danh sách các thuộc tính của chuyên mục : &nbsp;</strong> <asp:Literal ID="ltltitlegroup" runat="server"></asp:Literal></td></tr>
		</tbody>
		<tr><td colspan="3">
		<table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
			<tr bgcolor="#9dbae9" height="30px">
				<td align="center" width="5%"><strong>STT</strong></td>
				<td width="20%" style="padding-left:10px"><strong>Tên nhóm thuộc tính</strong></td>
				<td align="center" width="10%"><strong>Trạng thái</strong></td>
				<td align="center" width="5%"><strong>Sửa</strong></td>
				<td align="center" width="5%"><strong>Xoá</strong></td>
			</tr>
			<asp:Repeater ID="rptList" runat="server" onitemdatabound="rptList_ItemDataBound" onitemcreated="rptList_ItemCreated">
			<ItemTemplate>
			<tr height="25px" class="item" id="trItems" runat="server">
				<td align="center" width="5%"><asp:Literal ID="ltlID" Runat="server"  Visible="false"></asp:Literal><%#Eval("row_index")%></td>
				<td width="20%" style="padding-left:10px"><%#Eval("title")%></td>
				<td align="center" width="10%"><asp:LinkButton ID="ltbstatus" runat="server" Text="Sửa"></asp:LinkButton></td>
				<td align="center" width="5%"><asp:LinkButton ID="lblEdit" runat="server" Text="Sửa"></asp:LinkButton></td>
				<td align="center" width="5%"><asp:LinkButton ID="lbtDelete" runat="server" Text="Xoá"></asp:LinkButton></td>
			</tr>
			</ItemTemplate>
			</asp:Repeater>
		</table>
		</td></tr>
		<tr><td colspan="3" style="height:25px"></td></tr>
		<tr><td colspan="3" style="height:25px"><strong>Thông tin thêm mới</strong></td></tr>
		<tr style="height:30px;">
		<td style="width:150px;"><strong>Tên nhóm thuộc tính</strong></td>
		<td><asp:TextBox ID="txtTitle" Width="450px" runat="server"></asp:TextBox></td>
		<td><span style="color:Red;" id="spanTB" runat="server" visible="false">Bạn cần nhập tên nhóm !</span></td>
		</tr>
		<tr style="height:30px;">
		<td style="width:150px;"><strong>Các chuyên mục được áp dụng nhóm thuộc tính</strong></td>
		<td>
		<div style="clear: both; border-right: #0070b5 1px solid; padding-right: 5px; border-top: #0070b5 1px solid;
                padding-left: 5px; padding-bottom: 5px; overflow: auto; border-left: #0070b5 1px solid;
                padding-top: 5px; border-bottom: #0070b5 1px solid; width: 300px; height: 350px">
                <asp:TreeView ID="treeview1" runat="server" OnTreeNodePopulate="treeview1_TreeNodePopulate">
                </asp:TreeView>
            </div>
		</td>
		<td><span style="color:Red;" id="ltlRequireGroup" runat="server" visible="false">Cần chọn ít nhất 1 chuyên mục</span></td>
		</tr>
		<tr style="height:30px;">
		<td style="width:150px;"><strong></strong></td>
		<td><asp:Button ID="btnThem" runat="server" Text="Thêm mới" onclick="btnThem_Click" />&nbsp;
		<asp:Button ID="btnOk" runat="server" Text="Cập nhật" onclick="btnOk_Click" /></td>
		</tr>
</table>