<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_menucontext.ascx.cs" Inherits="Parent_uc_menucontext" %>
<section id="idbreadcrumbs">
<div class="container">
<ul class="breadcrumbs">
 <li><a href="<%=Globals.UrlHot%>">Trang chủ</a></li>
<asp:Repeater ID="rptCT" runat="server">
<ItemTemplate>
<li><a href="<%#CMDU.CommanUrl.UrlGroup(""+Eval("link")+"",""+Eval("shortlink")+"")%>"><%#Eval("title")%></a></li> 
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
</section>