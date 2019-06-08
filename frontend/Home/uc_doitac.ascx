<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_doitac.ascx.cs" Inherits="Home_uc_doitac" %>
<section id="partner">
<div class="container">
<div id="slide-partner" class="our-listing slide-brand owl-carousel owl-theme">
<asp:Repeater ID="rptDT" runat="server">
<ItemTemplate>
<div><a href="<%#Eval("url") %>" title="<%#Eval("title") %>"><img class="grayscale grayscale-fade" src="<%#Eval("fimage") %>" alt="<%#Eval("title") %>"></a></div>
</ItemTemplate>
</asp:Repeater>          
</div>
</div>
</section>
