<%@ Control Language="C#" AutoEventWireup="true" CodeFile="logfile.ascx.cs" Inherits="news_logfile" %>
<%@ Register TagPrefix="cc1" Namespace="uss.pagelist" Assembly="uss.pagelist" %>
<table class="text" cellSpacing="0" cellPadding="3" width="98%" border="0">
	<TBODY>
		
		<tr>
			<td height=10px></td>
		</tr>
		<tr>
			<td>
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD class="title"><asp:literal id="ltlLabTitle" Runat="server" Text="NHẬT KÝ BÀI VIẾT - HISTORY"></asp:literal>
						
						</TD>
					</TR>
					<tr>
						<td width=100% background="<%=UrlImages%>title1.gif" >&nbsp;</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td height=5px></td>
		</tr>
		<tr>
			<td><asp:repeater id="rptLogFile" Runat="server" OnItemDataBound="rptLogFile_ItemDataBound">
					<HeaderTemplate>
						<table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
							<tr bgcolor="#acc4eb" height="30px">
								<td align="center" width="10px"><strong>
										<asp:Literal ID="ltlIndexHeader" Runat="server" Text="STT"></asp:Literal></strong></td>
										
								<td align=center><strong>
										<asp:Literal ID="ltlTitleHeader" Runat="server" Text="Tiêu đề"></asp:Literal></strong></td>
								
								<td align="center" width="100px"><strong>
										<asp:Literal ID="ltlStatusHeader" Runat="server" Text="Trạng Thái"></asp:Literal></strong></td>
								<td align="center" width="80px"><strong>
										<asp:Literal ID="ltlUserHeader" Runat="server" Text="Người Dùng"></asp:Literal></strong></td>
								<td align="center" width="90px"><strong>
										<asp:Literal ID="ltlTimeHeader" Runat="server" Text="Ngày Tháng"></asp:Literal></strong></td>
								<td align="center" width="150px"><strong>
										<asp:Literal ID="ltlNoteHeader" Runat="server" Text="Ghi Chú"></asp:Literal></strong></td>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr class="item" id="trItems" runat="server" height="25px" >
							<td align="center" >
								<asp:Literal ID="ltlID" Runat="server"></asp:Literal>
							</td>
							<td align=left>
								<asp:Literal ID="ltlTitle" Runat="server"></asp:Literal></td>
							
							<td align="center" width=100px>
								<asp:Literal ID="ltlStatus" Runat="server"></asp:Literal></td>
							<td align="center" width=40px>
								<asp:Literal ID="ltlUsers" Runat="server"></asp:Literal></td>
							<td align="center" width=125px>
								<asp:Literal ID="ltlTime" Runat="server"></asp:Literal></td>
							<td align="center" width=150px>
								<asp:Literal ID="ltlNote" Runat="server"></asp:Literal></td>
						</tr>
					</ItemTemplate>
					<FooterTemplate>
</table>
</FooterTemplate> </asp:repeater></TD></TR>
<tr bgColor="#ffffff" height="22">
	<td>
		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td style="PADDING-LEFT: 10px" width="50%"><asp:literal id="ltlTotal1" Runat="server" Text=""></asp:literal></td>
				<td style="PADDING-RIGHT: 10px" align="right" width="50%"><cc1:pagelist id="PageList2" m_pIconPath="http://localhost/data.tcdl/images/" runat="server"></cc1:pagelist></td>
			</tr>
		</table>
	</td>
</tr>

<tr>
<td align=center> <input id="back"  type=button name="back" onclick='window.close()' value="Quay lại"></td>
</tr>
</TBODY></TABLE>

