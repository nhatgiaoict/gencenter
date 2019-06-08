<%@ Page language="c#" CodeFile="default.aspx.cs" AutoEventWireup="False" Inherits="filemanager._default" %>
    <link href="data/css/default.css" rel="stylesheet" type="text/css" />
	<link href="data/css/uploadify.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="data/scripts/jquery-1.3.2.min.js"></script>
	<script type="text/javascript" src="data/scripts/swfobject.js"></script>
	<script type="text/javascript" src="data/scripts/jquery.uploadify.v2.1.0.min.js"></script> 
	<script type = "text/javascript">
	    $(document).ready(function() {
	        $("#<%=FileUpload1.ClientID%>").uploadify({
	            'uploader': 'data/scripts/uploadify.swf',
	            'script': 'Upload.ashx',
	            'cancelImg': 'data/images/cancel.png',
	            'folder': '../<%= UrlPathUploadJquery %>',
	            'multi': true
	        });
	        $("#startUploadLink").click(function() {
	            $('#<%=FileUpload1.ClientID%>').uploadifyUpload();
	            return false;
	        });

	        $("#clearQueueLink").click(function() {
	            $("#<%=FileUpload1.ClientID%>").uploadifyClearQueue();
	            return false;
	        });

	    });

</script>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>File Manager</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="css/styles.css" type="text/css" rel="stylesheet">
		<script language='JavaScript' type='text/JavaScript' src='js/tooltip.js'></script>
		<script language='JavaScript' type='text/JavaScript' src='js/filemanager.js'></script>
	</HEAD>
	<body bgColor="#e7e7ef">
		<form id="Form1" runat="server">
			<TABLE cellSpacing="0" cellPadding="1" width="100%" border="0">
				<tr>
					<td>&nbsp;
						<asp:imagebutton id="UpBtn" runat="server" ToolTip="Up one level (Alt-U)" ImageUrl="pics/icons/up.gif"></asp:imagebutton>&nbsp;
						<asp:imagebutton id="GoRoot" runat="server" ToolTip="Back to Root Directory (Alt-H)" ImageUrl="pics/icons/home.gif"></asp:imagebutton>&nbsp;
						<asp:imagebutton id="RefreshBtn" runat="server" ToolTip="Refresh this page (Alt-R)" ImageUrl="pics/icons/refresh.gif"></asp:imagebutton>&nbsp;
						<a href='<%= "javascript:newFolder(\"" + this.path + "\")" %>' 
      ><IMG alt="New Folder" src="pics/icons/newFolder.gif" border="0"></a>&nbsp; <a 
      href='<%= "javascript:newFile(\"" + this.path + "\")" %>' 
      ><IMG alt="New File" src="pics/icons/newFile.gif" border="0"></a>&nbsp; <a 
      href='<%= "javascript:popUp(\"upload.aspx?Path=" + this.path + "\")"%>' 
      ><IMG alt="Upload a file to this directory" src="pics/icons/upload.gif" border="0"></a> &nbsp; <a 
      href='<%= "default.aspx?Thumbnails=1&Path=" + this.path %>' 
      ><IMG alt="View the images in this directory as thumbnails" src="pics/icons/thumbs.gif"
								border="0" /></a>
					</td>
				</tr>
				<tr>
				<td>
				<!--Upload nhieu file-->
				<div class="demo">
                <asp:FileUpload ID="FileUpload1" runat="server" />
				<br />
				<a href="#" id="startUploadLink">Upload</a>&nbsp; |&nbsp;
				<a href="#" id="clearQueueLink">Delete</a> 
                </div>
				</td>
				</tr>
			</TABLE>
			<hr>
			<P><asp:label id="MessageLbl" runat="server" Font-Bold="True" ForeColor="#0000C0"></asp:label><asp:placeholder id="imagesHolder" runat="server"></asp:placeholder><asp:panel id="filesFoldersPanel" Runat="server">
					<TABLE cellSpacing="0" cellPadding="3" width="100%" border="0">
						<TR class="header">
							<TH align="left"> <!-- icon --> &nbsp;</TH>
							<TH align="left">
								Name</TH>
							<TD align="left">
								<asp:label id="pathHeading" ForeColor="White" Font-Bold="True" Runat="server"></asp:label></TD>
							<TH align="left" width="50"> <!-- edit --> &nbsp;</TH>
							<TH align="left" width="16"> <!-- delete --> &nbsp;</TH>
							<TH align="left" width="50"> <!-- rename --> &nbsp;</TH>
							<TH align="left" width="75">
								Size
							</TH>
							<TH align="left">
								Last Modified
							</TH>
						</TR>
						<asp:placeholder id="FilesFolders" runat="server"></asp:placeholder></TABLE>
				</asp:panel></P>
			<P></P>
			<P>&nbsp;</P>
		</form>
	</body>
</HTML>
