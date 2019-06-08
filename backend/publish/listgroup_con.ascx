﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="listgroup_con.ascx.cs"
    Inherits="publish_listgroup_con" %>
<%@ Register TagPrefix="cc1" Namespace="uss.pagelist" Assembly="uss.pagelist" %>

<table cellspacing="0" cellpadding="0" width="95%" border="0">
    <tr class="Form_Header">
        <td align="left" colspan="2" height="22" style="padding-left: 5px" background="<%=UrlImages%>BodyTop.gif">
            <asp:Image ID="imgIconHeader" BorderWidth="0" runat="server"></asp:Image>&nbsp;<asp:Literal
                ID="ltlGroupTitle" runat="server"></asp:Literal></td>
    </tr>
    <tr><td colspan=2 style="height:20px">&nbsp;</td></tr>
    <tr valign="top">
        <td valign="top" style="width: 200px">
            <table style="border-collapse: collapse" bordercolor="#3399cc" cellspacing="0" cellpadding="2"
                width="100%" border="1">
                <tr>
                    <td height="20" background="<%=UrlImages%>BodyTop.gif">
                        <font color="#ffff66"><strong>
                            <asp:Literal ID="ltlHeader" runat="server" Text="Tổ chức theo chuyên mục"></asp:Literal>
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="clear: both; border-right: #0070b5 1px solid; padding-right: 5px; border-top: #0070b5 1px solid;
                            padding-left: 5px; padding-bottom: 5px; overflow: auto; border-left: #0070b5 1px solid;
                            padding-top: 5px; border-bottom: #0070b5 1px solid;">
                            <asp:TreeView ID="treeview1" runat="server" OnTreeNodePopulate="treeview1_TreeNodePopulate" >
                            </asp:TreeView>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
        <td >
            <table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
                <tbody>
                    <tr>
                        <td>
                            <asp:Repeater ID="rptGroup" runat="server" OnItemCreated="rptGroup_ItemCreated" OnItemDataBound="rptGroup_ItemDataBound">
                                <HeaderTemplate>
                                    <table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
                                        <tr bgcolor="#9dbae9" height="30px">
                                            <td align="center" width="5%">
                                                <strong>
                                                    <asp:Literal ID="ltlIndexHeader" runat="server" Text="STT"></asp:Literal></strong></td>
                                            <td width="90%" style="padding-left: 10px">
                                                <strong>
                                                    <asp:Literal ID="ltlTitleHeader" runat="server" Text="Tiêu đề"></asp:Literal></strong></td>
                                            <td align="center" width="5%">
                                                <strong>
                                                    <asp:Literal ID="ltlDeleteHeader" runat="server" Text="Xoá"></asp:Literal></strong></td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="item" id="trItems1" runat="server" height="25px">
                                        <td align="center">
                                            <asp:Literal ID="ltlID" runat="server" Visible="False"></asp:Literal>
                                            <asp:TextBox ID="txtIdx" runat="server" Width="30px"></asp:TextBox>
                                        </td>
                                        <td style="padding-left: 10px">
                                            <asp:Literal ID="ltlTitle" runat="server"></asp:Literal></td>
                                        <td align="center">
                                            <asp:LinkButton ID="lbtDelete" runat="server">
                                                <img id="imgDelete" runat="server" src="http://quanmh/data/images/delete.gif" alt="Delete"
                                                    width="13" height="13" border="0"></asp:LinkButton></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr bgcolor="#ffffff">
                        <td align="center">
                            <asp:Button ID="Button1" runat="server" Text="Cập nhật số thứ tự" OnClick="Button1_Click">
                            </asp:Button></td>
                    </tr>
                </tbody>
            </table>
            <table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
                <tbody>
                    <tr>
                        <td>
                            <strong>
                                <asp:Literal ID="ltlHeader1" runat="server" Text="Danh sách chuyên mục tự do"></asp:Literal></strong>
                            
                        </td>
                    </tr>
                    <tr bgcolor="#ffffff" height="22">
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td align="left" style="padding-left: 10px" width="50%">
                                        <asp:Literal ID="ltlTotal1" runat="server" Text=""></asp:Literal></td>
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
                            <asp:Repeater ID="rptFree" runat="server" OnItemCreated="rptFree_ItemCreated" OnItemDataBound="rptFree_ItemDataBound">
                                <HeaderTemplate>
                                    <table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
                                        <tr bgcolor="#9dbae9" height="30px">
                                            <td align="center" width="5%">
                                                <strong>
                                                    <asp:Literal ID="ltlIdxHeader1" runat="server" Text="STT"></asp:Literal></strong></td>
                                            <td width="90%" style="padding-left: 10px">
                                                <strong>
                                                    <asp:Literal ID="ltlTitleHeader1" runat="server" Text="Tiêu đề"></asp:Literal></strong></td>
                                            <td align="center" width="5%">
                                                <strong>
                                                    <asp:Literal ID="ltlSelectHeader1" runat="server" Text="Chọn"></asp:Literal></strong></td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="item" id="trItems2" runat="server" height="25px">
                                        <td align="center">
                                            <asp:Literal ID="ltlIndex1" runat="server"></asp:Literal>
                                        </td>
                                        <td style="padding-left: 10px">
                                            <asp:Literal ID="ltlTitle1" runat="server"></asp:Literal></td>
                                        <td align="center">
                                            <asp:LinkButton ID="lbtSelect1" runat="server">Chọn</asp:LinkButton></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>
    </tr>
</table>
