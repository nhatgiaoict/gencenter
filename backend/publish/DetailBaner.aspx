<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailBaner.aspx.cs" Inherits="publish_DetailBaner" %>

<%@ Register Src="../controls/style.ascx" TagName="style" TagPrefix="uc1" %>
 <uc1:style ID="Style1" runat="server" />
<body>
    <form id="form1" runat="server">
    <div style="padding-top: 30px;" align="center">
            <div style="padding-bottom:20px; padding-top: 10px;"> <asp:Label ID="lbHeaderT" runat="server"></asp:Label>
            <asp:Label ID="lbTenAnh" runat="server"></asp:Label>
            </div>
            <div>
            <asp:Literal ID="ltlImages" runat="server"></asp:Literal>
            </div>
            <hr />
            <div>
            <input id="bt" type="button"  runat="server" value="Đóng lại" class="button" onclick="window.close()" />
            </div>
    </div>
    </form>
</body>