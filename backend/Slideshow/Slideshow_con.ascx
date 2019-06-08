<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Slideshow_con.ascx.cs" Inherits="Slideshow_Slideshow_con" %>
<%@ Register TagPrefix="cc1" Namespace="uss.pagelist" Assembly="uss.pagelist" %>
<script language="javascript" type="text/javascript">
// <!CDATA[

function imgSelect_onclick() {

}

// ]]>
</script>
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tbody>
        <tr class="Form_Header">
            <td style="padding-left: 5px" height="22" background="<%=UrlImages%>BodyTop.gif" align="left">
                <asp:Image ID="imgIconHeader" BorderWidth="0" runat="server"></asp:Image>&nbsp;<asp:Literal
                    ID="ltlHeader" runat="server" Text="Quản trị Slide "></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px; padding-bottom:5px;" height="22" align="left"><strong>Danh sách các ảnh slide</strong>
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
                                    <strong><asp:Literal ID="ltlTitleHeader" runat="server" Text="Tên ảnh"></asp:Literal></strong>
                                </td>
                                <td align="center" width="40%">
                                    <strong><asp:Literal ID="ltlNote" runat="server" Text="Đường Dẫn"></asp:Literal></strong>
                                </td>
                                <td align="center" width="10%">
                                <strong><asp:Literal ID="ltlStatus" Runat="server" Text="Trạng thái"></asp:Literal></strong>
                                </td>
                                <%--<td align="center" width="20%">
                                <strong><asp:Literal ID="ltlAnh" Runat="server" Text="Ảnh"></asp:Literal></strong>
                                </td>--%>
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
                            <td align="left">
                                <%#Eval("filename")%>
                            </td>
                            	<td align="center"><asp:LinkButton ID="lbtStatus" Runat="server"></asp:LinkButton></td>
                            	<%--<td align="center"><asp:LinkButton ID="lbtAnh" Runat="server"></asp:LinkButton></td>--%>
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
        <tr bgColor="#ffffff" height="22">
	<td colspan="2">
		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td style="PADDING-LEFT: 10px" width="50%"><asp:literal id="ltlTotal" Runat="server" Text="Tìm thấy.."></asp:literal></td>
				<td style="PADDING-RIGHT: 10px" align="right" width="50%">
					<cc1:PageList id="PageList" runat="server" m_pIconPath="http://localhost/data/images/"></cc1:PageList>
				</td>
			</tr>
		</table>
	</td>
</tr>
    </tbody>
</table>
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tbody>
    <tr>
    <td colspan="3" align="left" style="padding-bottom:10px;"><strong><asp:Literal ID="ltltitleblog" runat="server">Thông tin thêm ảnh mới</asp:Literal></strong> </td>
    </tr>
    <tr>
    <td style="width:150px;" align="left">
        <asp:Literal ID="ltlTenVideo" runat="server">Tên ảnh:</asp:Literal></td>
        <td align="left">
            <asp:TextBox ID="txtTenNhom" runat="server" Width="450px"></asp:TextBox>
        </td>
        <td width="45%" class="RequireField">
            <asp:Literal ID="ltlRequireTitle" runat="server" Text="Phải nhập tiêu đề" Visible="False"></asp:Literal>
        </td>
    </tr>
    <tr>
    <td style="width:150px;" align="left"><asp:Literal ID="ltlUrl" runat="server">Url(đường dẫn ảnh):</asp:Literal></td>
               <td width="45%">
           <table cellpadding="0" cellspacing="0" width="100%" border="0">
							<tr>
								<td align="left" style="PADDING-TOP: 10px">
									<asp:TextBox ID="txtFileImages" Runat="server" Width="450px"></asp:TextBox>
									<img id="imgSelect" runat="server" scr="http://localhost/data/imgages/folder.gif">
								</td>
							</tr>
							<tr>
								<td align="left" class="RequireField"><asp:Literal ID="ltlRequiredLogo" Runat="server" Text="Phải chọn đường dẫn video" Visible="False"></asp:Literal></td>
							</tr>
							
						</table>
        </td>
    </tr>
    <tr>
    <td>Liên kết đến</td>
    <td><asp:TextBox ID="txtUrl" runat="server" Width="450"></asp:TextBox></td>
    </tr>
    <tr>
    <td align="left"><asp:Literal ID="ltlMoTa" runat="server">Mô tả:</asp:Literal></td>
    <td align="left" colspan="2"><asp:TextBox ID="txtMoto" runat="server" TextMode="MultiLine" Height="84px" Width="450px"></asp:TextBox>
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