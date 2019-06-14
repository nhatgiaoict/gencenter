<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_menucontext.ascx.cs" Inherits="Parent_uc_menucontext" %>
<section class="breadcrumb-modern context-dark text-md-left">
    <div class="shell section-34 section-md-top-110 section-md-bottom-41">
        <h2>
            <asp:Literal ID="ltltitleG" runat="server"></asp:Literal></h2>
        <ul class="list-inline list-inline-arrows p offset-top-34 offset-md-top-70">
            <li><a class="text-white" href="<%=Globals.UrlHot%>">Trang chủ</a></li>
            <asp:Repeater ID="rptCT" runat="server">
                <ItemTemplate>
                    <li><a class="text-white" href="<%#CMDU.CommanUrl.UrlGroup(""+Eval("link")+"",""+Eval("shortlink")+"")%>"><%#Eval("title")%></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</section>
