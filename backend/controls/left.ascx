<%@ Control Language="C#" AutoEventWireup="true" CodeFile="left.ascx.cs" Inherits="controls_left" %>
<script src="/data/js/jquery.min.js" type="text/javascript"></script>
<script language="javascript">
function heartBeat() {
    $.get("/KeepAlice.ashx?", function (data) {});
}
$(function () {
    setInterval("heartBeat()", 1000 * 30); // 30s gửi request một lần
});
</script>
<table class="MenuLeft" id="tblLanguage" style="border-collapse: collapse" bordercolor="#ffffff"
    cellspacing="2" cellpadding="3" width="100%" align="center" bgcolor="#f7f7f7"
    border="1" runat="server">
    <tr>
        <td class="Title_danhsach" style="padding-left: 8px">
            <asp:Literal ID="ltlWelcome" runat="server"></asp:Literal></td>
    </tr>
   <%--<tr class="MenuLeft_Header">
        <td style="padding-left: 5px"><img style="border-width:0px;" src="/data/images/icon_menu.gif" />&nbsp;Ngôn ngữ</td>
    </tr>
   <tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif" />&nbsp;
        <asp:DropDownList ID="ddlLang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged">
            </asp:DropDownList></td>
    </tr>--%>    
</table>
<table class="MenuLeft" id="tblProfile" style="border-collapse: collapse" bordercolor="#ffffff"
    cellspacing="2" cellpadding="3" width="100%" align="center" bgcolor="#f7f7f7"
    border="1" runat="server">
    <tr class="MenuLeft_Header">
        <td style="padding-left: 5px"><img style="border-width:0px;" src="/data/images/icon_menu.gif" />&nbsp;<asp:Literal ID="ltlProfile" runat="server" Text="Cá nhân"></asp:Literal></td>
    </tr>
    <tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif"><asp:HyperLink ID="hlProfile" runat="server">Thông tin cá nhân</asp:HyperLink></td>
    </tr>
    <tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif"><asp:HyperLink ID="hlChangePwd" runat="server">Đổi mật khẩu</asp:HyperLink></td>
    </tr>   
    <tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif"><asp:HyperLink ID="hlSignout" runat="server">Thoát</asp:HyperLink></td>
    </tr>
</table>
<table class="MenuLeft" id="tblAdministrator" style="border-collapse: collapse" bordercolor="#ffffff"
    cellspacing="2" cellpadding="3" width="100%" align="center" bgcolor="#f7f7f7"
    border="1" runat="server">
    <tr class="MenuLeft_Header">
        <td style="padding-left: 5px"><img style="border-width:0px;" src="/data/images/icon_menu.gif" />&nbsp;
            <asp:Literal ID="ltlAdministrator" runat="server" Text="Administrator"></asp:Literal></td>
    </tr>
    <tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif" />&nbsp;<asp:HyperLink ID="hlInforWeb" runat="server">Thông tin WebSite</asp:HyperLink></td>
    </tr>
</table>

<table class="MenuLeft" id="tblMember" style="border-collapse: collapse" bordercolor="#ffffff"
    cellspacing="2" cellpadding="3" width="100%" align="center" bgcolor="#f7f7f7"
    border="1" runat="server">
    <tr class="MenuLeft_Header">
        <td style="padding-left: 5px"><img style="border-width:0px;" src="/data/images/icon_menu.gif" />&nbsp;<asp:Literal ID="ltlMember" runat="server" Text="Thành viên"></asp:Literal></td>
    </tr>
    <tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif"><asp:HyperLink ID="hlRegMember" runat="server">Thêm mới thành viên</asp:HyperLink></td>
    </tr>
    <tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif"><asp:HyperLink ID="hlMemberManager" runat="server">Quản trị thành viên</asp:HyperLink></td>
    </tr>
</table>
<table class="MenuLeft" id="tblGroups" style="border-collapse: collapse" bordercolor="#ffffff"
    cellspacing="2" cellpadding="3" width="100%" align="center" bgcolor="#f7f7f7"
    border="1" runat="server">
    <tr class="MenuLeft_Header">
        <td style="padding-left: 5px"><img style="border-width:0px;" src="/data/images/icon_menu.gif" />&nbsp;<asp:Literal
                ID="ltlGroup" runat="server" Text="Quản trị chuyên mục"></asp:Literal></td>
    </tr>
    <tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif"><asp:HyperLink ID="hlRegGroup" runat="server">Thêm mới chuyên mục</asp:HyperLink></td>
    </tr>
    <tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif"><asp:HyperLink ID="hlGroupManager" runat="server">Quản trị chuyên mục</asp:HyperLink></td>
    </tr>
    <tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif"><asp:HyperLink ID="hlMenus" runat="server">Tổ chức Menu</asp:HyperLink></td>
    </tr>
</table>

<%--<table class="MenuLeft" id="tblHoivien" style="border-collapse: collapse" bordercolor="#ffffff"
    cellspacing="2" cellpadding="3" width="100%" align="center" bgcolor="#f7f7f7"
    border="1" runat="server">
<tr class="MenuLeft_Header">
<td style="padding-left: 5px"><img style="border-width:0px;" src="/data/images/icon_menu.gif" />&nbsp;<asp:Literal
        ID="Literal1" Text="Quản trị hội viên" runat="server"></asp:Literal></td>
</tr>
<tr>
<td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif"><a href="../hoivien/register.aspx">Thêm mới hội viên</a></td>
</tr>
<tr>
<td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif"><a href="../hoivien/manager.aspx">Quản trị hội viên</a></td>
</tr>
</table>--%>

<table class="MenuLeft" id="tblCreate_new" style="border-collapse: collapse" bordercolor="#ffffff"
    cellspacing="2" cellpadding="3" width="100%" align="center" bgcolor="#f7f7f7"
    border="1" runat="server">
<tr class="MenuLeft_Header">
<td style="padding-left: 5px"><img style="border-width:0px;" src="/data/images/icon_menu.gif" />&nbsp;<asp:Literal
        ID="ltlCreate_content" Text="Tạo nội dung" runat="server"></asp:Literal></td>
</tr>
<tr>
<td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif"><asp:HyperLink ID="hlCreate" runat="server">Viết bài</asp:HyperLink></td>
</tr>
<tr>
<td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif">&nbsp;<asp:HyperLink ID="hlList" runat="server">Danh sách bài viết</asp:HyperLink></td>
</tr>
<%--<tr>
<td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif"><a href="/news/addvanban.aspx"> Thêm mới bài viết dự án</a></td>
</tr>
<tr>
<td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif"><a href="/news/listvanban.aspx">Quản trị bài viết dự án</a></td>
</tr>--%>
</table>
<!---------------->
<table class="MenuLeft" id="tblOrgNews" style="border-collapse: collapse" bordercolor="#ffffff"
    cellspacing="2" cellpadding="3" width="100%" align="center" bgcolor="#f7f7f7"
    border="1" runat="server">
    <tr class="MenuLeft_Header">
        <td style="padding-left: 5px"><img style="border-width:0px;" src="/data/images/icon_menu.gif" />&nbsp;<asp:Literal
                ID="ltlOrgNews" runat="server" Text="Tổ chức "></asp:Literal></td>
    </tr>
    <%--<tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif" />&nbsp;<a href="/publish/Moinhat_home.aspx">Tổ chức Team</a></td>
    </tr>
    <tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif" />&nbsp;<a href="/Partner/Managerpartner.aspx">Quản lý đối tác</a></td>
    </tr>
    <tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif" />&nbsp;<a href="/publish/Ykienkhachhang.aspx">Tổ chức tin ý kiến khách hàng</a></td>
    </tr>--%>
    <%--<tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif" />&nbsp;
        <a href="/YahooHelp/ManagerYhaoo.aspx">Quản lý yahoo</a></td>
    </tr>--%>
    <tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif" />&nbsp;<asp:HyperLink ID="hrBanner" runat="server">Thay doi banner</asp:HyperLink></td>
    </tr>
    <tr>
        <td style="padding-left: 8px"><img style="border-width:0px;" src="/data/images/icon_new.gif" />&nbsp;<asp:HyperLink ID="hrfooter" runat="server">Thay footer</asp:HyperLink></td>
    </tr>
       
</table>