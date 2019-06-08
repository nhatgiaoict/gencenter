<%@ Control Language="C#" AutoEventWireup="true" CodeFile="manager.ascx.cs" Inherits="member_manager" %>
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tbody>
        <tr class="Form_Header">
            <td style="padding-left: 5px" height="22" background="<%=UrlImages%>BodyTop.gif">
                <asp:Image ID="imgIconHeader" BorderWidth="0" runat="server"></asp:Image>&nbsp;<asp:Literal
                    ID="ltlHeader" runat="server" Text="Quản trị thành viên"></asp:Literal></td>
        </tr>
        <tr>
            <td>
                <asp:Repeater ID="rptMember" runat="server" OnItemCreated="rptMember_ItemCreated"
                    OnItemDataBound="rptMember_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
                            <tr bgcolor="#9dbae9" height="30px">
                                <td align="center" width="5%">
                                    <strong>
                                        <asp:Literal ID="ltlIndexHeader" runat="server" Text="STT"></asp:Literal></strong></td>
                                <td width="40%" style="padding-left: 10px">
                                    <strong>
                                        <asp:Literal ID="ltlTitleHeader" runat="server" Text="Tên truy cập"></asp:Literal></strong></td>
                                <td width="17%" style="padding-left: 10px">
                                    <strong>
                                        <asp:Literal ID="ltlLastUpdateHeader" runat="server" Text="Chức năng"></asp:Literal></strong></td>
                                <td align="center" width="10%">
                                    <strong>
                                        <asp:Literal ID="ltlStatusHeader" runat="server" Text="Trạng thái"></asp:Literal></strong></td>
                                <td align="center" width="5%">
                                    <strong>
                                        <asp:Literal ID="ltlEditHeader" runat="server" Text="Sửa"></asp:Literal></strong></td>
                                <td align="center" width="5%">
                                    <strong>
                                        <asp:Literal ID="ltlDeleteHeader" runat="server" Text="Xoá"></asp:Literal></strong></td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="item" id="trItems" runat="server" height="25px">
                            <td align="center">
                                <asp:Literal ID="ltlID" runat="server" Visible="false"></asp:Literal>
                                <asp:Literal ID="ltlID_Index" Runat="server"  ></asp:Literal>
                            </td>
                            <td style="padding-left: 10px">
                                <asp:Literal ID="ltlTitle" runat="server"></asp:Literal></td>
                            <td style="padding-left: 10px">
                                <asp:Literal ID="ltlCreated" runat="server"></asp:Literal></td>
                            <td align="center">
                                <asp:LinkButton ID="lbtOnline" runat="server"></asp:LinkButton></td>
                            <td align="center">
                                <a href="#" id="hrEdit" runat="server"></a>
                            </td>
                            <td align="center" >
                                <asp:CheckBox ID="checkboxID" runat="server"></asp:CheckBox>
                                <asp:Literal ID="ltlIDMember" runat="server" Visible="False"></asp:Literal>
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
                <asp:Button ID="BtDelete" Text="Delete" runat="server" OnClick="BtDelete_Click"></asp:Button>
            &nbsp;&nbsp;
        </tr>
    </tbody>
</table>
