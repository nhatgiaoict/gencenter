﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="keyword_con.ascx.cs" Inherits="keyword_keyword_con" %>
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tbody>
        <tr class="Form_Header">
            <td style="padding-left: 5px" height="22" background="<%=UrlImages%>BodyTop.gif" align="left">
                <asp:Image ID="imgIconHeader" BorderWidth="0" runat="server"></asp:Image>&nbsp;<asp:Literal
                    ID="ltlHeader" runat="server" Text="Quản trị từ khóa"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px; padding-bottom:5px;" height="22" align="left"><strong>Danh sách nhóm</strong>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Repeater ID="rptGroup" runat="server" OnItemCreated="rptGroup_ItemCreated" OnItemDataBound="rptGroup_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
                            <tr bgcolor="#9dbae9" height="30px">
                                <td align="center" width="5%">
                                    <strong><asp:Literal ID="ltlIndexHeader" runat="server" Text="STT"></asp:Literal></strong>
                                </td>
                                <td width="37%" style="padding-left: 10px">
                                    <strong><asp:Literal ID="ltlTitleHeader" runat="server" Text="Tên Từ Khóa"></asp:Literal></strong>
                                </td>
                                <td align="center" width="50%">
                                    <strong><asp:Literal ID="ltlNote" runat="server" Text="Trang Thái"></asp:Literal></strong>
                                </td>
                                <td align="center" width="5%">
                                    <strong><asp:Literal ID="ltlEditHeader" runat="server" Text="Sửa"></asp:Literal></strong>
                                </td>
                                <td align="center" width="5%">
                                    <strong><asp:Literal ID="ltlDeleteHeader" runat="server" Text="Xoá"></asp:Literal></strong>
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="item" id="trItems" runat="server" height="25px">
                            <td align="center">
                                <asp:Literal ID="ltlID" runat="server" Visible="false"></asp:Literal>
                                <asp:TextBox ID="ltlID_Index" runat="server" Width="30px"></asp:TextBox>
                            </td>
                            <td style="padding-left: 10px">
                                <%#Eval("title")%>
                            </td>
                           <td align="center"><asp:LinkButton ID="lbtOnline" Runat="server"></asp:LinkButton></td>
                            <td align="center">
                            <asp:LinkButton ID="lblEdit" runat="server"></asp:LinkButton>
                            </td >
                            <td align="center">
                                <asp:CheckBox ID="checkboxID" runat="server"></asp:CheckBox>
                                <asp:Literal ID="ltlIDChuyenmuc" runat="server" Visible="False"></asp:Literal>
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
                <asp:Button ID="BtSTT" Text="Cap Nhat STT" runat="server" OnClick="BtSTT_Click">
                </asp:Button>
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtDelete" Text="Delete" runat="server" OnClick="BtDelete_Click">
                </asp:Button>
                &nbsp;&nbsp;
            </td>
        </tr>
    </tbody>
</table>
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tbody>
    <tr>
    <td colspan="3" align="left" style="padding-bottom:10px;"><strong>Thông tin thêm từ khóa</strong> </td>
    </tr>
    <tr>
    <td style="width:150px;" align="left">Têm từ khóa</td>
        <td align="left">
            <asp:TextBox ID="txtTenKeyWord" runat="server" Width="250px"></asp:TextBox>
        </td>
        <td width="45%" class="RequireField">
            <asp:Literal ID="ltlRequireTitle" runat="server" Text="Phải nhập từ khóa" Visible="False"></asp:Literal>
        </td>
    </tr>
    <tr>
    <td style="width:150px;" align="left">Link (url):</td>
        <td align="left">
            <asp:TextBox ID="txtlink" runat="server" Width="250px"></asp:TextBox>
        </td>
        <td width="45%" class="RequireField">
            <asp:Literal ID="Literal1" runat="server" Text="Phải nhập tiêu đề" Visible="False"></asp:Literal>
        </td>
    </tr>
    
    <tr>
    <td></td>
    <td align="left" style="padding-top:10px; margin-left: 40px;">
        <asp:Button ID="btlAdd" runat="server" Text="Thêm mới" Width="75px" 
            onclick="btlAdd_Click" />
        &nbsp;<asp:Button ID="btlOk" runat="server" Text="Cập nhật" Width="75px" 
            onclick="btlOk_Click" /></td>  
    </tr>
    </tbody>
</table>
