<%@ Control Language="C#" AutoEventWireup="true" CodeFile="register.ascx.cs" Inherits="news_register" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register TagPrefix="radcln" Namespace="Telerik.WebControls" Assembly="RadCalendar" %>
<script src="../data/js/jquery.min.js" type="text/javascript"></script>
<script src="../ckfinder/ckfinder.js" type="text/javascript"></script>
<style type="text/css">
.Check
{
	color:Red;
	font-weight:bold;
	font-size:small;
	padding-left:5px;
	}
</style>
<script type="text/javascript" language="javascript">
var $j = jQuery.noConflict();
 $j(document).ready(function() {
  if (this.timer) clearTimeout(this.timer);
  $j("#<%=txtShotlink.ClientID %>").blur(function() { //khi nhấn phím thì kiểm tra
   $j("#checkReturn").addClass("Check").html('<img src="/groups/ajax-loader.gif" height="16" width="16" /> Đang kiểm tra đường dẫn của bài viết..');
    this.timer = setTimeout(function () { //set time
   $j.ajax({
             type: "POST",
             url: "/news/register.aspx/KiemTraShortlink",
             data: "{'Title':'" + $j("#<%=txtShotlink.ClientID %>").val() + "'}",
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function(message) {
                 if (message.d == false) {
                     $j("#checkReturn").css({
                         "color": "red",
                         "font-weight": "bold",
                         "font-size": "small",
                         "padding-left": "5px"
                     });
                     $j("#checkReturn").text("Shortlink này đã tồn tại, hãy nhập shortlink khác");
                 }
                 else {
                     $j("#checkReturn").css({
                         "color": "green",
                         "font-weight": "bold",
                         "font-size": "small",
                         "padding-left": "5px"
                     });
                     $j("#checkReturn").text("Shortlink này chưa có bạn có thể sử dụng")
                 }
             },
             error: function(errormessage) {
                 //Hiển thị lỗi nếu xảy ra
                 $j("#checkReturn").text(errormessage.responseText);
             }
           });
      },1500);
  });
     // Make the AJAX call to the WebMethod when the textbox loses focus
  $j("#<%=txtShotlink.ClientID %>").blur(function() {
     });
 });
 </script>
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tr class="Form_Header">
        <td style="padding-left: 5px" colspan="3" height="22" background="<%=UrlImages%>BodyTop.gif">
            <asp:Image ID="imgIconHeader" BorderWidth="0" runat="server"></asp:Image>&nbsp;<asp:Literal
                ID="ltlHeader" runat="server" Text="Them moi bai viet"></asp:Literal></td>
    </tr>
    <tr>
        <td width="100">Tiêu đề <span style="color:Red;">*</span></td>
        <td align="left"><asp:TextBox ID="tbxTitle" runat="server" Width="450"></asp:TextBox></td>
        <td>&nbsp;</td>        
    </tr>
    <tr>
        <td width="100">Shot link <span style="color:Red;">*</span></td>
        <td align="left"><input id="txtShotlink" runat="server" style="width:450px;" onclick="GetText()" />
        <span id="checkReturn"></span>
        </td>
        <td>&nbsp;</td>        
    </tr>
    <tr>
    <td>Title Meta <span style="color:Red;">*</span></td><td><asp:TextBox ID="txtTitleMeta" runat="server" Width="450"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Keyword Meta</td><td><asp:TextBox ID="txtkeyword" runat="server" TextMode="MultiLine" Height="50" Width="450"></asp:TextBox></td>
    </tr>
     <tr>
    <td>Description Meta</td><td><asp:TextBox ID="txtDescripton" runat="server" TextMode="MultiLine" Height="50" Width="450"></asp:TextBox></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td align="left" style="padding-left: 5px" class="RequireField">
            <asp:Literal ID="ltlRequireTitle" runat="server" Text="Phai nhap tiêu de" Visible="False"></asp:Literal></td>
        <td>&nbsp;</td>    
    </tr>
    <tr>
        <td>Ngày phát hành</td>
        <td colspan="2" style="padding-left: 5px">
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
    <td>Chuyên mục <span style="color:Red;">*</span></td>
    <td><div style="clear: both; border-right: #0070b5 1px solid; padding-right: 5px; border-top: #0070b5 1px solid;
                padding-left: 5px; padding-bottom: 5px; overflow: auto; border-left: #0070b5 1px solid;
                padding-top: 5px; border-bottom: #0070b5 1px solid; width: 300px; height: 150px">
                <asp:TreeView ID="treeview1" runat="server" OnTreeNodePopulate="treeview1_TreeNodePopulate">
                </asp:TreeView>
            </div></td>
    </tr>
    <tr><td>Tóm tắt</td>
    <td><asp:TextBox ID="tbxSummary" runat="server" Height="100px" TextMode="MultiLine" Width="750"></asp:TextBox></td>
    </tr>
    <tr>
    <td>Ảnh minh họa</td>
    <td>
     <input id="txtFileImages" runat="server" name="FilePath" type="text" size="70" /><img alt="Chọn ảnh minh họa" src="/data/images/folder.gif" onclick="BrowseServer();" />
  
   </td>
    </tr>
    <tr>
        <td colspan="3">Nội dung chi tiết&nbsp; <span style="color:Red;">*</span>
            <font color="#ff0000">
                <asp:Literal ID="ltlRequireContent" runat="server" Text="Phai nhap noi dung" Visible="False"></asp:Literal></font></td>
    </tr>
    <!---Noi dung--->
    <tr>
        <td align="left" colspan="3">
			<CKEditor:CKEditorControl ID="txtContent" runat="server" Width="850px" Height="400px"></CKEditor:CKEditorControl>
		</td>
    </tr>
    <!---End Noi dung--->
    <tr>
        <td class="RequireField" colspan="3">
            <asp:Literal ID="ltlNote" runat="server" Text="*-Thông tin yêu cau bat buoc"></asp:Literal></td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btnRegister" Text="Lưu tạm" runat="server" OnClick="btnRegister_Click"></asp:Button>&nbsp;
            <asp:Button ID="BtPost" Text="Post Thang" runat="server" OnClick="BtPost_Click"></asp:Button></td>
    </tr>
    <script language="javascript" type="text/javascript">
    function locdau(str) {
      str= str.toLowerCase();  
      str= str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g,"a");  
      str= str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g,"e");  
      str= str.replace(/ì|í|ị|ỉ|ĩ/g,"i");  
      str= str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g,"o");  
      str= str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g,"u");  
      str= str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g,"y");  
      str= str.replace(/đ/g,"d");  
      str= str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g,"-"); 
      str= str.replace(/-+-/g,"-"); //thay thế 2- thành 1- 
      str= str.replace(/^\-+|\-+$/g,"");  
      return str;  
      }   
	  function GetText()
	  {
		var TXT = document.getElementById("Register1_tbxTitle").value;
		document.getElementById("Register1_txtShotlink").value = locdau(TXT);
		}
</script>
<script type="text/javascript">
function BrowseServer()
{
	var finder = new CKFinder();
	finder.basePath = '../';	// The path for the installation of CKFinder (default = "/ckfinder/").
	finder.selectActionFunction = SetFileField;
	finder.popup();
}

function SetFileField( fileUrl )
{
	document.getElementById('Register1_txtFileImages').value = fileUrl;
}
</script>
</table>
