<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FTer.ascx.cs" Inherits="publish_FTer" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register TagPrefix="radcln" Namespace="Telerik.WebControls" Assembly="RadCalendar" %>
<%@ Register TagPrefix="cc1" Namespace="uss.pagelist" Assembly="uss.pagelist" %>
    
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tr class="Form_Header">
        <td style="padding-left: 5px" colspan="3" height="22" background="<%=UrlImages%>BodyTop.gif">
            <asp:Image ID="imgIconHeader" BorderWidth="0" runat="server"></asp:Image>&nbsp;<asp:Literal
                ID="ltlHeader" runat="server" Text="Them footer"></asp:Literal></td>
    </tr>
    <tr style=" padding-top:30px;">
    <td colspan="3">
    <asp:Label ID="lbspace" runat="server"></asp:Label>
    </td>
    </tr>
    
    <tr>
    <td colspan="3" style=" padding-left:5px;">
    <asp:Literal ID="ltlDanhSach" runat="server"></asp:Literal>
    </td>
    </tr>
    <tr>
    <td style=" padding-left:10px; width:60%" colspan="3" align="center">
    <asp:Repeater ID="rptNews" runat="server" OnItemCreated="rptNews_ItemCreated" OnItemDataBound="rptNews_ItemDataBound">
                    <HeaderTemplate>
                        <table width="100%" border="0" cellspacing="1" cellpadding="1" bgcolor="#aaaeb5">
                            <tr bgcolor="#9dbae9" height="30px">
                                <td align="center" width="30px">
                                    &nbsp;</td>
                                <td align="center" width="30px">
                                    <strong>
                                        <asp:Literal ID="ltlIndexHeader" runat="server" Text="STT"></asp:Literal></strong></td>
                                
                                <td style="padding-left: 10px; width: 300px" width="300px">
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
                                <asp:Literal ID="ltlID" runat="server"></asp:Literal>
                            </td>
                            <td align="left" style="padding-left: 10px">
                                <asp:HyperLink ID="hlName" runat="server" Title="View detail."></asp:HyperLink></td>
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
    <tr>
    <td style=" padding-left:10px;" colspan="3">
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="padding-left: 10px" align="left" style="width: 50%">
                            <asp:Literal ID="ltlTotal1" runat="server" Text="Tìm thấy.."></asp:Literal></td>
                        <td style="padding-right: 10px" align="right" style="width: 50%">
                            <cc1:pagelist ID="PageList2" runat="server" 
                                m_pIconPath="http://quanmh/data.tcdl/images/">
                            </cc1:pagelist>
                        </td>
                    </tr>
                </table>
    </td>
    </tr>
    <tr>
    <td style=" padding-left:10px;" colspan="3">
        <asp:Button ID="BtDelete" runat="server" OnClick="BtDelete_Click" 
            Text="Delete" />
    </td>
    </tr>
    <tr style=" padding-top:10px; padding-bottom:10px;">
    <td colspan="3">
    <hr />
    </td>
    </tr>
    <tr>
    <td colspan="3" style=" padding-left:5px; padding-bottom:10px;">
    <asp:Label ID="lbTaoMoi" runat="server"></asp:Label>
    </td>
    </tr>
    <tr>
        <td width="100">
            <asp:Literal ID="ltlTitle" runat="server" Text="Tiêu de"></asp:Literal></td>
        <td align="left" style="padding-left: 5px">
            <asp:TextBox ID="tbxTitle" runat="server"></asp:TextBox></td>
                
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td align="left" style="padding-left: 5px" class="RequireField">
            <asp:Literal ID="ltlRequireTitle" runat="server" Text="Phai nhap tiêu de" Visible="False"></asp:Literal></td>
            
    </tr>
    <tr>
        <td>
            <asp:Literal ID="ltlPublishDate" runat="server" Text="Ngày phát hành"></asp:Literal></td>
        <td colspan="2" style="padding-left: 5px">
            <radcln:raddatepicker id="rdpPublishDate" Runat="server" 
                Culture="English (United States)" SelectedDate="2009-01-01" SharedCalendarID="">

                <dateinput CssClass="textbox" promptchar=" " dateformat="dd/MM/yyyy HH:mm:ss"
                    displaypromptchar="_" width="130px" displaydateformat=""></dateinput>
            </radcln:raddatepicker>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Literal ID="ltlContent" runat="server" Text="Noi dung"></asp:Literal>&nbsp;
            <font color="#ff0000">
                <asp:Literal ID="ltlRequireContent" runat="server" Text="Phai nhap noi dung" Visible="False"></asp:Literal></font></td>
    </tr>
    <!---Noi dung--->
    <tr>
        <td align="left" colspan="3">
			<CKEditor:CKEditorControl ID="txtContent" runat="server" Width="850px" Height="500px"></CKEditor:CKEditorControl>
		</td>
    </tr>
    <!---End Noi dung--->
    <tr>
        <td class="RequireField" colspan="3">
            <asp:Literal ID="ltlNote" runat="server" Text="*-Thông tin yêu cau bat buoc"></asp:Literal></td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btnRegister" Text="Cap Nhat" runat="server" OnClick="btnRegister_Click"></asp:Button>&nbsp;
            </td>
    </tr>
    <tr>
    <td colspan="3">
    </td>
    </tr>
     
</table>