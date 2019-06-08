<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Managerthuoctinh.aspx.cs" Inherits="Boloc_Managerthuoctinh" %>
<%@ Register Src="~/Boloc/uc_managerthuoctinh.ascx" TagName="uc_managerthuoctinh" TagPrefix="uc5" %>
<%@ Register Src="../controls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<%@ Register Src="../controls/header.ascx" TagName="header" TagPrefix="uc3" %>
<%@ Register Src="../controls/left.ascx" TagName="left" TagPrefix="uc4" %>
<%@ Register Src="../controls/style.ascx" TagName="style" TagPrefix="uc1" %>
 <uc1:style ID="Style1" runat="server" />
<body text="#000000" link="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" marginwidth="0"
	marginheight="0" >
	<form id="Form1" method="post" runat="server">
		<table width="100%" cellpadding="0" cellspacing="0" border="0" id="Table1">
			<tr>
				<td colspan="2">
                    <uc3:header ID="Header1" runat="server" />
				</td>
			</tr>
			<tr>
				<td width="20%" valign="top"><uc4:left ID="Left1" runat="server" />
				</td>
				<td width="80%" valign="top" style="PADDING-TOP:10px" align="center">
					<table width="100%" cellpadding="0" cellspacing="0" border="0" id="Table2">
						<tr>
							<td align="center"><uc5:uc_managerthuoctinh ID="uc_managerthuoctinh1" runat="server" /></td>
						</tr>
					</table>
                    
				</td>
			</tr>
			<tr>
				<td colspan="2">
                    <uc2:Footer ID="Footer1" runat="server" />
				</td>
			</tr>
		</table>
	</form>
</body>
