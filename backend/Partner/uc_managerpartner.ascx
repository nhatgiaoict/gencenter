<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_managerpartner.ascx.cs" Inherits="Partner_uc_managerpartner" %>
<%@ Register TagPrefix="cc1" Namespace="uss.pagelist" Assembly="uss.pagelist" %>
<script src="../data/js/jquery.min.js" type="text/javascript"></script>
<script src="../ckfinder/ckfinder.js" type="text/javascript"></script>
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tbody>
        <tr class="Form_Header">
            <td style="padding-left: 5px" height="22" background="/data/images/BodyTop.gif" align="left">
            <img style="border-width:0px;" src="/data/images/icon_menu1.gif" />&nbsp;Quản trị logo đối tác</td>
        </tr>
        <tr><td>
<table width="100%" cellspacing="1" cellpadding="5" border="0" bgcolor="#e8edf6">
            <tbody>
                <tr bgcolor="#ffffff">
                    <td width="10%" style="padding-left: 20px"><%=Language.GetTextByID(38)%></td>
                    <td style="padding-right: 20px"><asp:TextBox ID="txtKeyword" runat="server" Width="250px"></asp:TextBox></td>
                    <td align="right"><%=Language.GetTextByID(39) %></td>
                    <td><asp:dropdownlist id="ddlStatus" runat="server"></asp:dropdownlist></td>
                    <td align="right" style="padding-right: 20px"><asp:imagebutton id="imgBtn" ImageUrl="/data/images/search_new.gif" runat="server" OnClick="imgBtn_Click" /></asp:imagebutton></td>
                </tr>
            </tbody>
</table>
        </td></tr>
        <tr>
            <td align="left">
                <asp:Repeater ID="rptGroup" runat="server" OnItemCreated="rptGroup_ItemCreated" OnItemDataBound="rptGroup_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
                            <tr bgcolor="#9dbae9" height="30px">
                                <td align="center" width="5%"><strong><asp:Literal ID="ltlIndexHeader" runat="server" Text="STT"></asp:Literal></strong></td>
                                <td width="37%" style="padding-left: 10px"><strong><%=Language.GetTextByID(29)%></strong></td>
                                <td align="center" width="10%"><strong>Website</strong></td>
                                <td align="center" width="10%"><strong>Logo</strong></td>
                                <td align="center" width="10%"><strong><%=Language.GetTextByID(39)%></strong></td>
                                <td align="center" width="5%"><strong><%=Language.GetTextByID(42)%></strong></td>
                                <td align="center" width="5%"><strong><%=Language.GetTextByID(43)%></strong></td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="item" id="trItems" runat="server" height="25px">
                            <td align="center"><asp:Literal ID="ltlID" runat="server" Visible="false"></asp:Literal>
                                <asp:TextBox ID="ltlID_Index" runat="server" Width="30px"></asp:TextBox></td>
                            <td style="padding-left: 10px"><%#Eval("title")%></td>
                             <td style="padding-left: 10px"><%#Eval("url")%></td>
                            <td align="center"><img src="<%#Eval("fimage")%>" style="width:100px" /></td>
                            <td align="center"><asp:LinkButton ID="lbtStatus" Runat="server"></asp:LinkButton></td>
                            <td align="center">
                            <asp:LinkButton ID="lblEdit" runat="server"></asp:LinkButton>
                            </td >
                            <td align="center"><asp:CheckBox ID="checkboxID" runat="server"></asp:CheckBox><asp:Literal ID="ltlIDChuyenmuc" runat="server" Visible="False"></asp:Literal>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr bgcolor="#ffffff">
            <td align="right">
            <asp:Button ID="BtSTT" Text="Cap Nhat STT" runat="server" OnClick="BtSTT_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtDelete" Text="Delete" runat="server" OnClick="BtDelete_Click" />
            </td>
        </tr>
        <tr bgColor="#ffffff" height="22">
	<td colspan="2">
		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td style="PADDING-LEFT: 10px" width="50%"></td>
				<td style="PADDING-RIGHT: 10px" align="right" width="50%">
					<cc1:PageList id="PageList" runat="server" m_pIconPath="http://localhost/data/images/"></cc1:PageList>
				</td>
			</tr>
		</table>
	</td>
</tr>
    </tbody>
</table>
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tbody>
    <tr>
    <td colspan="3" align="left" style="padding-bottom:10px;"><strong><%= Language.GetTextByID(360)%></strong> </td>
    </tr>
    <tr>
    <td style="width:160px;" align="left"><%=Language.GetTextByID(29)%></td>
        <td align="left"><asp:TextBox ID="txtTenNhom" runat="server" Width="450px"></asp:TextBox></td>
        <td width="20%" class="RequireField"><asp:Literal ID="ltlRequireTitle" runat="server" Text="Phải nhập tên" Visible="False"></asp:Literal></td>
    </tr>
    <tr>
    <td style="width:160px;" align="left">Logo</td>
    <td align="left"><input id="txtFileImages" runat="server" name="FilePath" type="text" size="65" />
    <img alt="Chọn ảnh minh họa" src="/data/images/folder.gif" onclick="BrowseServer();" /></td>
    <td width="20%" class="RequireField"></td>
    </tr>
     <tr>
    <td style="width:160px;" align="left">Website</td>
        <td align="left"><asp:TextBox ID="txtUrrl" runat="server" Width="450px"></asp:TextBox></td>
        <td width="20%" class="RequireField"></td>
    </tr>
    <tr>
    <td align="left"><%=Language.GetTextByID(5)%></td>
    <td align="left" colspan="2"><asp:TextBox ID="txtMoto" runat="server" TextMode="MultiLine" Height="84px" Width="450px"></asp:TextBox></td>  
    </tr>
    <tr>
    <td></td>
    <td align="left" style="padding-top:10px; margin-left: 40px;">
        <asp:Button ID="Button1" runat="server" Text="Thêm mới" Width="75px" 
            onclick="Button1_Click" />&nbsp;&nbsp;
            <asp:Button ID="btlOk" runat="server" Text="Cập nhật" Width="75px" 
            onclick="btlOk_Click" /></td>
    </tr>
    </tbody>
</table>
<script type="text/javascript">
    function BrowseServer() {
        var finder = new CKFinder();
        finder.basePath = '../'; // The path for the installation of CKFinder (default = "/ckfinder/").
        finder.selectActionFunction = SetFileField;
        finder.popup();
    }

    function SetFileField(fileUrl) {
        document.getElementById('Register1_txtFileImages').value = fileUrl;
    }
</script>