<%@ Control Language="C#" AutoEventWireup="true" CodeFile="update.ascx.cs" Inherits="news_update" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register TagPrefix="radcln" Namespace="Telerik.WebControls" Assembly="RadCalendar" %>
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tr class="Form_Header">
        <td background="/data/images/BodyTop.gif" height="22" colspan="3" style="padding-left: 5px">
            <img style="border-width:0px;" src="/data/images/icon_menu1.gif" id="Register1_imgIconHeader">&nbsp;Sửa đổi nội dung bài viết</td>
    </tr>
     <tr>
        <td width="100">
            <asp:Literal ID="ltlTitle" runat="server" Text="Tiêu de"></asp:Literal></td>
        <td align="left"><asp:TextBox ID="tbxTitle" runat="server" Width="450"></asp:TextBox></td>
        <td>&nbsp;</td>        
    </tr>
    <tr>
    <td>Short Url</td><td><input id="txtShotlink" runat="server" style="width:450px;" /></td>
    </tr>
    <tr>
    <td>Title Meta</td><td><asp:TextBox ID="txtTitleMeta" runat="server" Width="450"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Keyword Meta</td><td><asp:TextBox ID="txtkeyword" TextMode="MultiLine" runat="server" Height="50" Width="450"></asp:TextBox></td>
    </tr>
     <tr>
    <td>Description Meta</td><td><asp:TextBox ID="txtDescripton" TextMode="MultiLine" runat="server" Height="50" Width="450"></asp:TextBox></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td align="left" style="padding-left: 5px" class="RequireField">
            <asp:Literal ID="ltlRequireTitle" runat="server" Text="Phai nhap tiêu de" Visible="False"></asp:Literal></td>
        <td>&nbsp;</td>    
    </tr>
    
    <tr>
        <td colspan="3" align=left>
            <asp:Literal ID="ltlGroup" runat="server" Text="Thuoc nhóm"></asp:Literal>&nbsp;
            <font color="#ff0000">
                <asp:Literal ID="ltlRequireGroup" runat="server" Visible="False" Text="Phai chon nhóm"></asp:Literal></font>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 5px" align=left colspan="3">
            <div style="clear: both; border-right: #0070b5 1px solid; padding-right: 5px; border-top: #0070b5 1px solid;
                padding-left: 5px; padding-bottom: 5px; overflow: auto; border-left: #0070b5 1px solid;
                padding-top: 5px; border-bottom: #0070b5 1px solid; width: 300px; height: 150px">
                <asp:TreeView ID="treeview1" runat="server" OnTreeNodePopulate="treeview1_TreeNodePopulate">
                </asp:TreeView>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="3" align=left>
            <asp:Literal ID="ltlSummary" runat="server" Text="Tóm tat noi dung"></asp:Literal>&nbsp;
            <font color="#ff0000">
                <asp:Literal ID="ltlRequireSummary" runat="server" Text="Phai nhap tóm tat" Visible="False"></asp:Literal></font></td>
    </tr>
    <tr>
        <td colspan="3" align=left>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td style="padding-left: 5px" width="200">
                        <asp:TextBox ID="tbxSummary" runat="server" Height="130px" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                    <td style="padding-left: 10px">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                             <tr>
								<td><asp:Image Width="200px" ID="imgLogo" Runat="server" BorderWidth="0"></asp:Image></td>
							</tr>
                            <tr>
                                <td align="left">
                                    <asp:HyperLink ID="hlLogo" runat="server"></asp:HyperLink></td>
                            </tr>
                            <tr>
                                <td style="padding-top: 10px" align=left>
                                    <asp:TextBox ID="txtFileImages" runat="server" Width="250px"></asp:TextBox>
                                    <img id="imgSelect" runat="server" scr="http://localhost/data/imgages/folder.gif">
                                </td>
                            </tr>
                            <tr>
                                <td height="10">
                                    &nbsp;</td>
                            </tr>
                           <%-- <tr>
                                <td>
                                    <asp:Literal ID="ltlViewSize" runat="server" Text="Kích thước hiển thị:"></asp:Literal>(pixels):&nbsp;</td>
                            </tr>
                            <tr>
                                <td height="10">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="text">
                                        <tr>
                                            <td width="80" align="right">
                                                <asp:Literal ID="ltlWidth" runat="server" Text="Chiều rộng:"></asp:Literal>:&nbsp;</td>
                                            <td width="50">
                                                <asp:TextBox Width="50" ID="tbxWidth" runat="server">100</asp:TextBox></td>
                                            <td width="80" align="right">
                                                <asp:Literal ID="ltlHeight" runat="server" Text="Chiều cao:"></asp:Literal>:&nbsp;</td>
                                            <td>
                                                <asp:TextBox ID="tbxHeight" Width="50" runat="server">100</asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>--%>
                             <tr>
                                <td height="10">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                            
                            </tr>
                            
                        </table>
                    </td>
                </tr>
                </table>
        </td>
    </tr>
    <tr>
        <td colspan="3" align="left">
            <asp:Literal ID="ltlContent" runat="server" Text="Noi dung bài viet"></asp:Literal>&nbsp;
            <font color="#ff0000">
                <asp:Literal ID="ltlRequireContent" runat="server" Text="Phai nhap noi dung" Visible="False"></asp:Literal></font></td>
    </tr>
    <!---Noi dung--->
    <tr>
        <td align="left" colspan="3">
			<CKEditor:CKEditorControl ID="txtContent" runat="server" Width="850px" Height="500px"></CKEditor:CKEditorControl>
		</td>
    </tr>
    <tr>
		<td width="10%"><asp:Literal ID="ltlGhichu" runat="server"/>&nbsp;:&nbsp;</td>
		<td align="left"><asp:TextBox id="txtGhichu" Width="350px" Height="50px" TextMode="MultiLine" runat="server"></asp:TextBox></td>
		<td width="10%"></td>
	</tr>
    <!---End Noi dung--->
    <tr>
        <td class="RequireField" colspan="3" align=left>
            <asp:Literal ID="ltlNote" runat="server" Text="*-Thông tin yêu cau bat buoc"></asp:Literal></td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btnRegister" Text="Cap Nhat" runat="server" OnClick="btnRegister_Click"></asp:Button>&nbsp;
         </td>         
    </tr>
</table>

