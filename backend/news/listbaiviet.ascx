<%@ Control Language="C#" AutoEventWireup="true" CodeFile="listbaiviet.ascx.cs" Inherits="news_listbaiviet" %>
<%@ Register TagPrefix="cc1" Namespace="uss.pagelist" Assembly="uss.pagelist" %>

<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tbody>
        <tr class="Form_Header" >
            <td align=left style="padding-left: 5px" height="22" background="<%=UrlImages%>BodyTop.gif">
                <asp:Image ID="imgIconHeader" BorderWidth="0" runat="server"></asp:Image>&nbsp;<asp:Literal
                    ID="ltlHeader" runat="server" Text="Danh sách bài viết"></asp:Literal>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table cellspacing="1" cellpadding="5" width="100%" bgcolor="#e8edf6" border="0">
                    <tr bgcolor="#ffffff">
                        <td>
                            <asp:Literal ID="ltlGroup" runat="server" Text="Thuộc nhóm"></asp:Literal>
                         </td>
                        <td align=left style="padding-left: 5px" colspan="4">
                            <div style="clear: both; border-right: #0070b5 1px solid; padding-right: 5px; border-top: #0070b5 1px solid;
                                padding-left: 5px; padding-bottom: 5px; overflow: auto; border-left: #0070b5 1px solid;
                                padding-top: 5px; border-bottom: #0070b5 1px solid; width: 300px; height: 150px">
                                <asp:TreeView ID="treeview1" runat="server" OnTreeNodePopulate="treeview1_TreeNodePopulate">
                                </asp:TreeView>
                            </div>
                        </td>
                    </tr>
                    <tr bgcolor="#ffffff">
                        <td style="padding-left: 20px" width="20%">
                            <asp:Literal ID="ltlKeyword" runat="server" Text="Từ khoá"></asp:Literal></td>
                        <td style="padding-right: 20px">
                            <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox></td>
                        <td align="right">
                            <asp:Literal ID="ltlStatus" runat="server" Text="Trạng thái"></asp:Literal></td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" Width="170px" runat="server">
                            </asp:DropDownList></td>
                        <td style="padding-right: 20px" align="right">
                            <asp:ImageButton ID="imgBtn" runat="server" ImageUrl="http://localhost/data/images/search_new.gif" OnClick="imgBtn_Click">
                            </asp:ImageButton></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="22">
            <td align=left>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="padding-left: 10px" width="50%">
                            <asp:Literal ID="ltlTotal" runat="server" Text="Tìm thấy..."></asp:Literal></td>
                        <td style="padding-right: 10px" align="right" width="50%">
                            <cc1:PageList ID="PageList1" runat="server" m_pIconPath="http://localhost/data.tcdl/images/">
                            </cc1:PageList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Repeater ID="rptNews" runat="server" OnItemCreated="rptNews_ItemCreated" OnItemDataBound="rptNews_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
                            <tr bgcolor="#9dbae9" height="30px">
                                <td>
                                    &nbsp;</td>
                                <td align="center" width="40px">
                                    <strong>
                                        <asp:Literal ID="ltlIndexHeader" runat="server" Text="STT"></asp:Literal></strong></td>
                                <td style="padding-left: 20px"  align=left>
                                    <strong>
                                        <asp:Literal ID="ltlTitleHeader" runat="server" Text="Tiêu đề"></asp:Literal></strong></td>
                                <td width="100px" style="padding-left: 10px">
                                    <strong><asp:Literal ID="ltlLastUpdateHeader" runat="server" Text="Ngày cập nhật"></asp:Literal></strong></td>
                                <td align="center" width="60px">
                                    <strong>Trạng thái</strong></td>
                                <td align="center" width="60px">
                                    <strong>Trang chủ</strong></td>
                                <td align="center" width="40px">
                                    <strong><asp:Literal ID="ltlLogHeader" runat="server" Text="Xem Log"></asp:Literal></strong></td>
                                <td align="center" width="40px">
                                    <strong>
                                        <asp:Literal ID="ltlEditHeader" runat="server" Text="Sửa"></asp:Literal></strong></td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="item" id="trItems" runat="server" height="25px">
                            <td align="center" style="width: 30px" width="30px">
                                <asp:CheckBox ID="checkboxID" runat="server"></asp:CheckBox>
                                <asp:Literal ID="ltlIDNews" runat="server" Visible="False"></asp:Literal>
                            </td>
                            <td align="center"><asp:TextBox ID="txtSTT" runat="server" Width="40"></asp:TextBox>
                            </td>
                            
                            <td style="padding-left: 10px" align=left>
                                <asp:HyperLink ID="ltlTitle" Title="View detail." runat="server"></asp:HyperLink></td>
                            <td style="padding-left: 10px">
                                <asp:Literal ID="ltlCreated" runat="server"></asp:Literal></td>
                            <td align="center">
                                <asp:LinkButton ID="lbtSend" runat="server"></asp:LinkButton></td>
                            <td align="center">
                                <asp:LinkButton ID="lbtHotlike" runat="server"></asp:LinkButton></td>
                            <%--<td align="center">
                                <asp:LinkButton ID="lbtHotnew" runat="server"></asp:LinkButton></td>--%>
                            <td align="center">
                                <img id="imgHistory" runat="server" title="ViewLog" alt="ViewLog" border="0"></td>
                            <td align="center">
                                <a href="#" id="hrEdit" runat="server" title="Edit"></a>
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
            <td align=left>
                <asp:Button ID="BtDelete" Text="Delete" runat="server" OnClick="BtDelete_Click"></asp:Button>
                <asp:Button ID="BtSend" Text="Cập nhật STT" runat="server" OnClick="BtSend_Click"></asp:Button>&nbsp;&nbsp;</td>
        </tr>
        <tr bgcolor="#ffffff" height="22">
            <td align=left>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td  style="padding-left: 10px" width="50%">
                            <asp:Literal ID="ltlTotal1" runat="server" Text="Tìm thấy.."></asp:Literal></td>
                        <td style="padding-right: 10px" align="right" width="50%">
                            <cc1:PageList ID="PageList2" runat="server" m_pIconPath="http://localhost/data.tcdl/images/">
                            </cc1:PageList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </tbody>
</table>
