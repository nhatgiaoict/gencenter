<%@ Page language="c#" CodeFile="upload.aspx.cs" AutoEventWireup="true" Inherits="filemanager.upload" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Upload File</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" encType="multipart/form-data" runat="server">
			<P>
				<TABLE class="borderTableLight" id="Table1" cellSpacing="0" cellPadding="7" border="0">
					<TR>
						<TD bgcolor="#2f759f">&nbsp;&nbsp;<STRONG><FONT color="#ffffff">Upload a file</FONT></STRONG></TD>
					</TR>
					<TR>
						<TD valign=top bgcolor="#2f759f">&nbsp;&nbsp;<INPUT id="fileToUpload" size=30 style="HEIGHT:22px; size:30" type="file" name="fileToUpload"
								runat="server">&nbsp;
							<asp:button id="UploadBtn" runat="server" Text="Upload" Height="22px" Width="61px"></asp:button></TD>
					</TR>
				</TABLE>
			</P>
		</form>
	</body>
</HTML>
