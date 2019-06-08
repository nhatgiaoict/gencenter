<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Yhaoo_con.ascx.cs" Inherits="YahooHelp_Yhaoo_con" %>
<style type="text/css">
    .style1{height: 28px;}
</style>
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tbody>
        <tr class="Form_Header">
            <td style="padding-left: 5px" height="22" background="<%=UrlImages%>BodyTop.gif" align="left">
                <asp:Image ID="imgIconHeader" BorderWidth="0" runat="server"></asp:Image>&nbsp;<asp:Literal ID="ltlHeader" runat="server" Text="Quản trị nhom yahoo"></asp:Literal>
            </td>
        </tr>
        <tr>
        <td style="padding-left:10px; padding-bottom:5px;" align="left"><strong>Danh sách</strong></td>
        </tr>
        <tr>
            <td align="left">
                <asp:Repeater ID="rptYahoo" runat="server" onitemcreated="rptYahoo_ItemCreated" 
                    onitemdatabound="rptYahoo_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
                            <tr bgcolor="#9dbae9" height="30px">
                                <td align="center" width="5%">
                                    <strong>
                                        <asp:Literal ID="ltlIndexHeader" runat="server" Text="STT"></asp:Literal></strong>
                                </td>
                                <td width="20%" style="padding-left: 10px">
                                    <strong>
                                        <asp:Literal ID="ltlTitleHeader" runat="server" Text="Họ tên (fullname)"></asp:Literal></strong>
                                </td>
                                <td align="center" width="20%">
                                    <strong>
                                        <asp:Literal ID="ltlNote" runat="server" Text="Nick Yahoo"></asp:Literal></strong>
                                </td>
                                <td align="center" width="20%">
                                    <strong>
                                        <asp:Literal ID="Literal1" runat="server" Text="Email"></asp:Literal></strong>
                                </td>
                                 <td align="center" width="10%">
                                    <strong>
                                        <asp:Literal ID="ltlTrangthai" runat="server" Text="Trạng thái"></asp:Literal></strong>
                                </td>
                                <td align="center" width="5%">
                                    <strong>
                                        <asp:Literal ID="ltlEditHeader" runat="server" Text="Sửa"></asp:Literal></strong>
                                </td>
                                <td align="center" width="5%">
                                    <strong>
                                        <asp:Literal ID="ltlDeleteHeader" runat="server" Text="Xoá"></asp:Literal></strong>
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
                                <%#Eval("fullname")%>
                            </td>
                            <td align="left" style="padding-left: 10px">
                                <%#Eval("YahooName")%>
                            </td>
                            <td align="left" style="padding-left: 10px">
                                <%#Eval("Email")%>
                            </td>
                            <td align="center">
                            <asp:LinkButton ID="lblStatus" runat="server"></asp:LinkButton>
                            </td>
                            <td align="center">
                            <asp:LinkButton ID="lblEdit" runat="server"></asp:LinkButton>
                            </td>
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
    <td colspan="3" align="left" style="padding-left:5px; padding-bottom:10px;"><strong>Thông tin thêm mới</strong></td>
    </tr>
    <tr>
    <td style="width:150px;" align="left">Họ tên (FullName)</td>
        <td align="left">
            <asp:TextBox ID="txtFullname" runat="server" Width="250px"></asp:TextBox>&nbsp;<span style="color:Red;size:2">*</span>
        </td>
        <td width="45%" class="RequireField">
            <asp:Literal ID="ltlRequireTitle" runat="server" Text="Phải nhập các thông tin có dấu *" Visible="False"></asp:Literal>
        </td>
    </tr>
   <%-- <tr><td>Hình ảnh</td><td><asp:TextBox ID="txtfimage" runat="server" Width="235px"></asp:TextBox><img id="imgSelect" runat="server" scr="http://localhost/data/imgages/folder.gif"></td></tr>
    <tr><td>Lĩnh vực tư vấn</td><td><asp:TextBox ID="txtlinhvuc" runat="server" Width="250px"></asp:TextBox></td></tr>--%>
    <tr>
    <td align="left">Điện thoại</td>
    <td align="left"><asp:TextBox ID="txtPhone" runat="server" 
            Width="250px"></asp:TextBox>&nbsp;<span style="color:Red;size:2">*</span></td>
    </tr>
    <tr>
    <td align="left">Nick Yahoo</td>
    <td align="left"><asp:TextBox ID="txtNickYahoo" runat="server" 
            Width="250px"></asp:TextBox>&nbsp;<span style="color:Red;size:2">*</span></td>
    <td class="RequireField"></td>       
    </tr>
    <tr>
    <td align="left">Email</td>
    <td align="left"><asp:TextBox ID="txtEmail" runat="server" 
            Width="250px"></asp:TextBox>&nbsp;<span style="color:Red;size:2">*</span></td>
     <td class="RequireField"></td>
    </tr>
   <%-- <tr>
    <td align="left" class="style1">Chọn nhóm</td>
    <td align="left" class="style1">
        <asp:DropDownList ID="DropGroupYahoo" runat="server" Height="21px" 
            Width="250px" onselectedindexchanged="DropGroupYahoo_SelectedIndexChanged">
        </asp:DropDownList>
                </td>
     <td class="style1"></td>                   
    </tr>--%>
    <tr>
    <td></td>
    <td align="left" style="padding-top:10px; margin-left: 40px;">
        <asp:Button ID="btlAdd" runat="server" Text="Thêm mới" Width="75px" 
            onclick="btlAdd_Click" />
        &nbsp;<asp:Button ID="btlOk" runat="server" Text="Cập nhật" Width="75px" onclick="btlOk_Click" /></td>  
    </tr>
    </tbody>
</table>