<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Thuoctinhsp.aspx.cs" Inherits="news_Thuoctinhsp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cập nhật thuộc tính sản phẩm đã đưa</title>
    <link href="/data/css/stylethuoctinhsp.css" rel="stylesheet" type="text/css" />
    <script src="/data/js/jquery.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
function ShowHideTab(controlID) {
    if (controlID == 'Tab0') {
        $("#Tab0").show();
        $("#Header0").removeClass().addClass("active");
        $("#Tab1").hide();
        $("#Header1").removeClass().addClass("");
        $("#Tab2").hide();
        $("#Header2").removeClass().addClass("");
    }
    if (controlID == 'Tab1') {
        $("#Tab0").hide();
        $("#Header0").removeClass().addClass("");
        $("#Tab1").show();
        $("#Header1").removeClass().addClass("active");
        $("#Tab2").hide();
        $("#Header2").removeClass().addClass("");
    }
    if (controlID == 'Tab2') {
        $("#Tab0").hide();
        $("#Header0").removeClass().addClass("");
        $("#Tab2").show();
        $("#Header2").removeClass().addClass("active");
        $("#Tab1").hide();
        $("#Header1").removeClass().addClass("");
    }
}
</script>
<style type="text/css">
.tt { font-size:12px; }
.tt .l 
{
	width:20%;
	float:left;
}
.tt .r { width:80%; float:left;}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <p style="font-weight:bold">Cập nhật các thuộc tính của sản phẩm</p><hr />
    <div class="module_larger" style="overflow:hidden;">
        <ul class="tab-view-pro"> 
            <li id="Header0" onclick="ShowHideTab('Tab0')" class="active"><a href="#"><span>Thuộc tính</span></a></li> 
            <li id="Header1" onclick="ShowHideTab('Tab1')"><a href="#"><span>Nhà sản xuất</span></a></li> 
            <li id="Header2" onclick="ShowHideTab('Tab2')"><a href="#"><span>Khoảng giá</span></a></li> 
        </ul><!-- End of menu-tb -->
       <div style="clear:both;">
       <div id="Tab0">
       <table style="width:100%;" cellpadding="0" cellspacing="0" border="0">
       <tbody>
       <tr><td><strong>Khoảng giá</strong></td><td><asp:DropDownList ID="DropKhoanggia" DataValueField='id' DataTextField='title' runat="server" Width="180"></asp:DropDownList></td>
       <td><strong>Nhà sản xuất</strong></td><td><asp:DropDownList ID="dropNSX"  DataValueField='id' DataTextField='title' runat="server" Width="180"></asp:DropDownList></td>
       </tr>
       </tbody>
       </table>
       </div>
       </div>
       </div>
       <div id="Tab1" style="display:none;">
       </div>
       <div id="Tab2" style="display:none;">
       </div>
       </div>
    </div>
    </form>
</body>
</html>
