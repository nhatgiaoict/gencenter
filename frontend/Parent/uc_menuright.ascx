<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_menuright.ascx.cs" Inherits="Parent_uc_menuright" %>
<aside class="c-sidebar"> <h2>Danh mục dịch vụ</h2>
<ul class="xemnhieu"> 
<asp:Repeater ID="rptDM" runat="server">
<ItemTemplate>
<li><i class="fa fa-chevron-circle-right"></i><a href="<%#CMDU.CommanUrl.UrlGroup(""+Eval("plink")+"",""+Eval("shortlink")+"")%>"><%#Eval("ptitle")%></a></li>
</ItemTemplate>
</asp:Repeater>
</ul> 
</aside>