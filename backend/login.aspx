<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<%@ Register Src="controls/login.ascx" TagName="login" TagPrefix="uc2" %>
<%@ Register Src="controls/style.ascx" TagName="style" TagPrefix="uc1" %>
 <uc1:style ID="Style1" runat="server" />

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
	<form id="VIPLogin" method="post" runat="server">
		<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="780" align="center" border="0">
			<TR>
				<TD >
                    <uc2:login ID="Login1" runat="server" />
					
				</TD>
			</TR>
		</TABLE>
	</form>
</body>
