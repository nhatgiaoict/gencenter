<%@ Control Language="C#" AutoEventWireup="true" CodeFile="update_con.ascx.cs" Inherits="publish_update_con" %>
<table cellspacing="0" cellpadding="0" width="95%" align="center" border="0">
    <tbody>
        <tr class="Form_Header">
            <td align="left" align="left" style="padding-left: 5px; height: 22px; background-image: url(<%=UrlImages%>BodyTop.gif)">
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
						<td>
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
    </tbody>
</table>
