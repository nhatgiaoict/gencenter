<%@ Control Language="C#" AutoEventWireup="true" CodeFile="update.ascx.cs" Inherits="uchoivien_update" %>
<%@ Register TagPrefix="radcln" Namespace="Telerik.WebControls" Assembly="RadCalendar" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="../ckfinder/ckfinder.js" type="text/javascript"></script>
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tr class="Form_Header">
        <td background="/data/images/BodyTop.gif" height="22" colspan="3" style="padding-left: 5px">
            <img style="border-width:0px;" src="/data/images/icon_menu1.gif" id="Register1_imgIconHeader">&nbsp;Chỉnh sửa văn bản đã cập nhật</td>
    </tr>
    <tr>
        <td width="100">Tên hội viên<span style="color:Red">*</span></td>
        <td align="left"><asp:TextBox ID="tbxTitle" runat="server" Width="450"></asp:TextBox></td>
        <td><asp:Literal ID="ltlRequireTitle" runat="server" Text="Phai nhap tiêu de" Visible="False"></asp:Literal></td>        
    </tr>
    <tr>
        <td width="100">Cấp bậc</td>
        <td align="left"><asp:TextBox ID="txtCapbac" runat="server" Width="450"></asp:TextBox></td>
        <td></td>        
    </tr>
    <tr>
    <td>Logo</td>
    <td>
    <input id="txtFileImages" runat="server" name="FilePath" type="text" size="70" /><img alt="Chọn ảnh minh họa" src="/data/images/folder.gif" onclick="BrowseServer();" />
     </td>      
    </tr>
    <tr>
        <td>Ngày đăng</td>
        <td colspan="2">
            <radcln:raddatepicker id="rdpPublishDate" Runat="server">
                <dateinput CssClass="textbox" promptchar=" " dateformat="dd/MM/yyyy HH:mm:ss"
                    displaypromptchar="_" width="130px" displaydateformat=""></dateinput>
            </radcln:raddatepicker>
        </td>
    </tr>
    <tr>
    <td></td>
        <td>
            <font color="#ff0000">
                <asp:Literal ID="ltlRequireGroup" runat="server" Visible="False" Text="Phai chon nhóm"></asp:Literal></font>
        </td>
    </tr>
    <tr>
    <td><asp:Literal ID="ltlGroup" runat="server" Text="Thuoc nhóm"></asp:Literal></td>
    <td><div style="clear: both; border-right: #0070b5 1px solid; padding-right: 5px; border-top: #0070b5 1px solid;
                padding-left: 5px; padding-bottom: 5px; overflow: auto; border-left: #0070b5 1px solid;
                padding-top: 5px; border-bottom: #0070b5 1px solid; width: 300px; height: 150px">
                <asp:TreeView ID="treeview1" runat="server" OnTreeNodePopulate="treeview1_TreeNodePopulate">
                </asp:TreeView>
            </div></td>
    </tr>
    <tr><td>Tóm tắt</td>
    <td><asp:TextBox ID="tbxSummary" runat="server" Height="80px" TextMode="MultiLine" Width="450"></asp:TextBox></td>
    </tr>
    <tr>
     <td>Giới thiệu</td>
        <td align="left" colspan="2">
			<CKEditor:CKEditorControl ID="txtContent" runat="server" Width="850px" Height="400px"></CKEditor:CKEditorControl>
		</td>
    </tr>
    <tr>
        <td class="RequireField" colspan="3" align=left>
            <asp:Literal ID="ltlNote" runat="server" Text="*-Thông tin yêu cau bat buoc"></asp:Literal></td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btnRegister" Text="Cap Nhat" runat="server" OnClick="btnRegister_Click"></asp:Button>&nbsp;
         </td>         
    </tr>
    <script language="javascript" type="text/javascript">
        function locdau(str) {
            str = str.toLowerCase();
            str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
            str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
            str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
            str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
            str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
            str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
            str = str.replace(/đ/g, "d");
            str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, "-");
            str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1- 
            str = str.replace(/^\-+|\-+$/g, "");
            return str;
        }
        function GetText() {
            var TXT = document.getElementById("Update1_tbxTitle").value;
            document.getElementById("Update1_txtShotlink").value = locdau(TXT);
        }
</script>
<script type="text/javascript">
    function BrowseServer() {
        var finder = new CKFinder();
        finder.basePath = '../';	// The path for the installation of CKFinder (default = "/ckfinder/").
        finder.selectActionFunction = SetFileField;
        finder.popup();
    }

    function SetFileField(fileUrl) {
        document.getElementById('Update1_txtFileImages').value = fileUrl;
    }
    function BrowseServerKhac() {
        var finder = new CKFinder();
        finder.basePath = '../';	// The path for the installation of CKFinder (default = "/ckfinder/").
        finder.selectActionFunction = SetFileFieldKhac;
        finder.popup();
    }

    function SetFileFieldKhac(fileUrl) {
        document.getElementById('Update1_txtFilekhac').value = fileUrl;
    }
    function Chuyentext() {
        var txtContent = document.getElementById("Update1_txtMultiIMG");
        var txtTemp = document.getElementById("Update1_txtFilekhac");
        if (txtTemp.value != null && txtTemp.value != "") {
            txtContent.value += txtTemp.value + ",";
            txtTemp.value = "";
        }
    }
</script>
</table>