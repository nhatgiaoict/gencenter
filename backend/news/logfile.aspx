<%@ Page Language="C#" AutoEventWireup="true" CodeFile="logfile.aspx.cs" Inherits="_news_logfile" %>
<%@ Register Src="logfile.ascx" TagName="logfile" TagPrefix="uc1" %>

<%@ Register Src="../controls/style.ascx" TagName="style" TagPrefix="uc1" %>
 <uc1:style ID="Style1" runat="server" />
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<body>
    <form id="form1" runat="server">
    <table class="msnB" id="tbFrame" cellSpacing="0" cellPadding="0" width="100%" align="center"
			bgColor="#ffffff" border="0" name="tbFrame">
			<tr>
				<td align="center">
                    <uc1:logfile ID="Logfile1" runat="server" />
					
				</td>
			</tr>
		</table>
    </form>
</body>

