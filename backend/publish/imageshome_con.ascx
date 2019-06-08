<%@ Control Language="C#" AutoEventWireup="true" CodeFile="imageshome_con.ascx.cs"   Inherits="publish_imageshome_con" %>
<%@ Register TagPrefix="cc1" Namespace="uss.pagelist" Assembly="uss.pagelist" %>
<table cellspacing="0" cellpadding="0" width="95%" align="center" border="0">
    <tbody>
        <tr class="Form_Header">
            <td align="left" style="padding-left: 5px; height: 22px; background-image: url(<%=UrlImages%>BodyTop.gif)">
                <asp:Image ID="imgIconHeader" BorderWidth="0" runat="server"></asp:Image>&nbsp;<asp:Literal
                    ID="ltlHeader" runat="server" Text="Quan tri anh"></asp:Literal></td>
        </tr>
        <tr>
            <td style="height: 20px">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="width: 10%">
                            &nbsp;</td>
                        <td align="left" class="RequireField">
                            <asp:Literal ID="ltlRequireTitle" runat="server" Text="Phai nhap tiêu de" Visible="False"></asp:Literal></td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            <asp:Literal ID="ltlAddImages" runat="server" Text="Them moi anh"></asp:Literal>
                            <span style="font-size: 9pt; color: #ff0000">*</span>:
                        </td>
                        <td align="Left" class="RequireField">
                            <asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            &nbsp;</td>
                        <td align="left" style="padding-top: 10px">
                            <asp:TextBox ID="txtFileImages" runat="server" Width="250px"></asp:TextBox>
                            <img alt="" id="imgSelect" runat="server" scr="http://localhost/data/imgages/folder.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            &nbsp;</td>
                        <td align="left">
                            <asp:Literal ID="ltlViewSize" runat="server" Text="Kích thước hiển thị:"></asp:Literal>(967x157 pixels):&nbsp;</td>
                    </tr>
                    <tr>
                      <td style="width: 10%">
                            &nbsp;</td>
                       	<td >
							<table width="100%" cellpadding="0" cellspacing="0" border="0" class="text">
								<tr>
									<td width="80" align="right"><asp:Literal ID="ltlWidth" Runat="server" Text="Chiều rộng:"></asp:Literal>:&nbsp;</td>
									<td width="50"><asp:TextBox Width="50" ID="tbxWidth" Runat="server">100</asp:TextBox></td>
									<td width="80" align="right"><asp:Literal ID="ltlHeight" Runat="server" Text="Chiều cao:"></asp:Literal>:&nbsp;</td>
									<td><asp:TextBox ID="tbxHeight" Width="50" Runat="server">100</asp:TextBox></td>
								</tr>
							</table>
						</td>
					</tr>
                    <tr>
                        <td style="width: 10%">
                            &nbsp;</td>
                        <td align="left" style="height: 10">
                            &nbsp;<asp:Button ID="BtAdd" runat="server" Text="BtAdd" OnClick="BtAdd_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 10">
                            &nbsp;</td>
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
                                <td align="center" width="30px">
                                    &nbsp;</td>
                                <td align="center" width="30px">
                                    <strong>
                                        <asp:Literal ID="ltlIndexHeader" runat="server" Text="STT"></asp:Literal></strong></td>
                                <td style="padding-left: 10px">
                                    <strong>
                                        <asp:Literal ID="ltlTitleName" runat="server" Text="Ten anh"></asp:Literal></strong></td>
                                <td style="padding-left: 10px; width: 100px" width="100px">
                                    <strong>
                                        <asp:Literal ID="ltlTitleHeader" runat="server" Text="Tiêu đề"></asp:Literal></strong></td>
                                <td align="center" style="width: 100px" width="100px">
                                    <strong>
                                        <asp:Literal ID="ltlStatusHeader" runat="server" Text="Trạng thái"></asp:Literal></strong></td>
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
                            <td align="center">
                                <asp:Literal ID="ltlID" runat="server" Visible="false"></asp:Literal>
                                      <asp:TextBox ID="txtSTT" runat="server" Width="30px"></asp:TextBox>
                            </td>
                            <td align="left" style="padding-left: 10px">
                                <asp:HyperLink ID="hlName" runat="server" Title="View detail."></asp:HyperLink></td>
                            <td align="left" style="padding-left: 10px">
                                <asp:Literal ID="ltlTitle" runat="server"></asp:Literal></td>
                            <td align="center">
                                <asp:LinkButton ID="lbtOnline" runat="server"></asp:LinkButton></td>
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
        <tr bgcolor="#ffffff" height="22">
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="padding-left: 10px" align="left" style="width: 50%">
                            <asp:Literal ID="ltlTotal1" runat="server" Text="Tìm thấy.."></asp:Literal></td>
                        <td style="padding-right: 10px" align="right" style="width: 50%">
                            <cc1:PageList ID="PageList2" runat="server" m_pIconPath="http://quanmh/data.tcdl/images/">
                            </cc1:PageList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr bgcolor="#ffffff">
            <td align="left">
                <asp:Button ID="BtDelete" Text="Delete" runat="server" OnClick="BtDelete_Click"></asp:Button>
                <asp:Button
                    ID="btSTT" runat="server" Text="Cật nhật STT" onclick="btSTT_Click" />
        </tr>
    </tbody>
</table>
