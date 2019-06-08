<%@ Page Language="C#" AutoEventWireup="true" CodeFile="detail.aspx.cs" Inherits="hoivien_detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../controls/style.ascx" TagName="style" TagPrefix="uc1" %>
 <uc1:style ID="Style1" runat="server" />
<boby >
    <form id="form1" runat="server">
    
    <table width="98%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td colspan="2" style="height:1"></td>
			</tr>
			<tr>
				<td colspan="2">
					<table cellpadding="0" cellspacing="0" width="100%" border="0">
						<tr>
							<td class="title">&nbsp;&nbsp;&nbsp;<asp:literal id="ltlLabTitle" Runat="server" Text="Detail"></asp:literal>
							</td>
						</tr>
						<tr>
							<td  style="width:100%;background-image:url(<%=UrlImages%>title1.gif)" >&nbsp;</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td colspan="2" style="height:15px"></td>
			</tr>
			<tr>
				<td style="width:150px">&nbsp;
				     <B><font color=#C46200><asp:label id="LabTieude" runat="server"></asp:label>&nbsp;:</B></font>
				 </td>
				<td><font color=#953100><B><asp:label id="LabTenTieude" runat="server"></asp:label></B></font></td>
			</tr>
			<tr>
				<td colspan="2" style="height:5"></td>
			</tr>
			<tr>
				<td width="150">&nbsp;<font color=#C46200><B><asp:label id="LabHeaderCmuc" runat="server"></asp:label>&nbsp;:</B></font></td>
				<td><font color=#953100><B><asp:label id="LabChuyenmuc" runat="server"></asp:label></B></font></td>
			</tr>
			<tr>
				<td colspan="2" style="height:5"></td>
			</tr>
			<TR>
				<TD>&nbsp;<font color=#C46200><B><asp:label id="LabImg" runat="server"></asp:label>&nbsp;:</B></font></TD>
				<TD><img id="fImage" border="0" runat="server" width="90" height="60" src="http://localhost/viglacera/data/images/no-image.gif"
						align="middle"></TD>
			</TR>
			<tr>
				<td colspan="2" style="height:5"></td>
			</tr>
			<TR>
				<TD>&nbsp;<font color=#FF8000><font color=#C46200><B><asp:label id="LabTomtat" runat="server"></asp:label>&nbsp;:</B></font></TD>
				<TD><asp:label id="LabND_tomtat" runat="server"></asp:label></font></TD>
			</TR>
			<tr>
				<td colspan="2" style="height:5"></td>
			</tr>
			<TR>
				<TD>&nbsp;</TD>
				<TD><font color=#953100><B><U><asp:label id="LabNoidung" runat="server"></asp:label>&nbsp;:</U></B></font></TD>
			</TR>
			<TR>
				<TD>&nbsp;</TD>
				<TD><asp:label id="LabNoidungbai" runat="server"></asp:label></TD>
			</TR>
			<tr>
				<td colspan="2" style="height:5"></td>
			</tr>
			<TR>
				<TD align="center" colspan="2">
					<input class="button" id="bt" onclick="window.close()" type="button" value="Đóng lại"></input>
				</TD>
			</TR>
			<tr>
				<td colspan="2" style="height:10"></td>
			</tr>
		</table>    
        
    </form>
</body>

