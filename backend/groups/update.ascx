<%@ Control Language="C#" AutoEventWireup="true" CodeFile="update.ascx.cs" Inherits="groups_update" %>
<table cellspacing="5" cellpadding="0" width="95%" align="center" border="0">
    <tr class="Form_Header">
        <td colspan="3" height="22" style="padding-left: 5px" background="<%=UrlImages%>BodyTop.gif">
            <asp:Image ID="imgIconHeader" runat="server" BorderWidth="0"></asp:Image>&nbsp;<asp:Literal
                ID="ltlHeader" runat="server" Text="Thêm mới chuyên mục"></asp:Literal></td>
    </tr>
    <tr>
        <td colspan="3" style="padding-left: 20px">
            <table width="100%" cellpadding="0" cellspacing="5" border="0">
                <tr>
                    <td width="10%">Tiêu đề</td>
                    <td width="35%" style="padding-left: 5px"><asp:TextBox ID="tbxTitle" Width="450" runat="server"></asp:TextBox></td>
                    <td width="45%" class="RequireField"><asp:Literal ID="ltlRequireTitle" runat="server" Text="Phải nhập tiêu đề" Visible="False"></asp:Literal></td>
                </tr>
                <tr>
                    <td>Liên kết đến</td>
                    <td colspan="2" style="padding-left: 5px"><asp:TextBox ID="tbxLink" Width="450" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Short Url</td>
                    <td colspan="2" style="padding-left: 5px"><input id="txtShotlink" style="width:450px;" runat="server" onclick="GetText()" /></td>
                </tr>
               <tr>
                    <td>Loại chuyên mục</td>
                    <td colspan="2" style="padding-left: 5px"><asp:DropDownList ID="dropkind" 
                            runat="server" Width="120px">
                        <asp:ListItem Value="0">Tin tức</asp:ListItem>
                        <asp:ListItem Value="1">Sản phẩm</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Chuyên mục cha</td>
                    <td style="padding-left: 5px" colspan="2">
                        <div style="clear: both; border-right: #0070b5 1px solid; padding-right: 5px; border-top: #0070b5 1px solid;
                            padding-left: 5px; padding-bottom: 5px; overflow: auto; border-left: #0070b5 1px solid;
                            padding-top: 5px; border-bottom: #0070b5 1px solid; width: 300px; height: 150px">
                            <asp:TreeView ID="treeview1" runat="server" OnTreeNodePopulate="treeview1_TreeNodePopulate">
                            </asp:TreeView>
                        </div>
                        <asp:Label ID="LabMessage" runat="server" Font-Bold="False" Font-Size="X-Small" ForeColor="Red"></asp:Label></td>
                </tr>
                <tr>
                    <td>Ảnh minh hoạ
                    </td>
                    <td style="padding-left: 5px" colspan="2">
                        <table cellSpacing="0" cellPadding="0" width="100%" border="0">
						<tr>
							<td style="PADDING-TOP: 10px">
								<asp:TextBox ID="txtFileImages" Runat="server" Width="250px"></asp:TextBox>
								<img id="imgSelect" runat="server" scr="http://localhost/data/imgages/folder.gif">
							</td>
						</tr>
						<tr>
							<td><asp:Literal ID="ltlViewSize" Runat="server" Text="Kích thước hiển thị:"></asp:Literal>(pixels):&nbsp;</td>
						</tr>
						<tr>
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
					</table>
                    </td>
                </tr>
                <tr>
                    <td>Tóm tắt</td>
                    <td colspan="2" style="padding-left: 5px">
                        <asp:TextBox ID="tbxSummary" runat="server" Width="450" Height="60" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>Title Meta</td>
                    <td colspan="2" style="padding-left: 5px">
                        <asp:TextBox ID="txttitlemeta" runat="server" Width="450px"></asp:TextBox>
                    </td>
                </tr>
                  <tr>
                    <td>Keyword Meta</td>
                    <td colspan="2" style="padding-left: 5px">
                        <asp:TextBox ID="txtkeywords" runat="server" TextMode="MultiLine" Height="50px" 
                            Width="450px"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>Description Meta</td>
                    <td colspan="2" style="padding-left: 5px">
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Height="50px" 
                            Width="450px"></asp:TextBox>
                    </td>
                </tr>
                <tr><td>Số sản phẩm hiển thị</td><td colspan="2">
                <asp:DropDownList ID="dropSL" runat="server" Width="50px">
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        </asp:DropDownList></td></tr>
                <tr>
                    <td colspan="3" class="RequireField">
                        <asp:Literal ID="ltlNote" runat="server" Text="*-Thông tin yêu cầu bắt buộc"></asp:Literal></td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Button ID="btnRegister" Text="Cập nhật" runat="server" OnClick="btnRegister_Click">
                        </asp:Button></td>
                </tr>
            </table>
        </td>
    </tr>
    <script language="javascript" type="text/javascript">
    function locdau(str) {
      str= str.toLowerCase();  
      str= str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g,"a");  
      str= str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g,"e");  
      str= str.replace(/ì|í|ị|ỉ|ĩ/g,"i");  
      str= str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g,"o");  
      str= str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g,"u");  
      str= str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g,"y");  
      str= str.replace(/đ/g,"d");  
      str= str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g,"-"); 
      str= str.replace(/-+-/g,"-"); //thay thế 2- thành 1- 
      str= str.replace(/^\-+|\-+$/g,"");  
      return str;  
      }   
	  function GetText()
	  {
		var TXT = document.getElementById("Update1_tbxTitle").value;
		document.getElementById("Update1_txtShotlink").value = locdau(TXT);
		}
</script>
</table>

