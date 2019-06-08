<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sendpassword.aspx.cs" Inherits="sendpassword" %>

<%@ Register Src="controls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<%@ Register Src="controls/header.ascx" TagName="header" TagPrefix="uc3" %>
<%@ Register Src="controls/style.ascx" TagName="style" TagPrefix="uc1" %>
<uc1:style ID="Style1" runat="server" />
<body  >
    <form id="Form1" method="post" runat="server">
        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="Table1">
            <tr>
                <td colspan="2">
                    <uc3:header ID="Header1" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="20%">
                    &nbsp;
                </td>
                <td width="80%" valign="middle" align="center">
                    <!--Begin-->
                    <table cellpadding="0" cellspacing="0" border="0" width="1012" style="width: 1012px;
                        height: 136px">
                        <tr>
                            <td height="5">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="5">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="90">
                                &nbsp;</td>
                            <td align="left">
                                <span class="text">
                                    <asp:Label ID="Lab_message" runat="server" ForeColor="Red" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="10">
                            </td>
                        </tr>
                        <tr>
                            <td width="160">
                                &nbsp; <span class="text">
                                    <asp:Literal ID="ltlUsername" runat="server"></asp:Literal><font color="#ff0000"
                                        size="2"> *</font> :</span></td>
                            <td>
                                <asp:TextBox ID="txtUsername" runat="server" Width="200px"></asp:TextBox></td>
                            </tr>
                        <tr>
                            <td colspan="2" style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td width="160">
                                &nbsp; <span class="text">
                                    <asp:Literal ID="ltlEmail" runat="server"></asp:Literal><font color="#ff0000" size="2">
                                        *</font> :</span></td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Width="300px"></asp:TextBox></td>
                            </tr>
                        <tr>
                            <td colspan="2" height="10">
                            </td>
                        </tr>
                        <tr>
                            <td width="100">
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="btSend" runat="server" CssClass="button" Text="Gửi" OnClick="btSend_Click"></asp:Button>
                                <asp:Button ID="btBack" runat="server" CssClass="button" Text="Quay lại" OnClick="btBack_Click"></asp:Button></td>
                        </tr>
                        <tr>
                            <td colspan="2" height="10">
                            </td>
                        </tr>
                        <tr>
                            <td height="150">
                            </td>
                        </tr>
                    </table>
                    <!--End-->
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;<uc2:Footer ID="Footer1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
