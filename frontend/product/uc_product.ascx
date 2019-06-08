<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_product.ascx.cs" Inherits="product_uc_product" %>
<section id="s2home-bietthu">
<div class="heading-duan" style="<%=sBg%>">
 <h2><asp:Literal ID="ltltitleG" runat="server"></asp:Literal></h2>
</div><!--End-->
<div class="elip-box-s2home"> 
<div class="container">
    <section id="idbreadcrumbs">
     <div class="container">
         <ul class="breadcrumbs">
            <li><a href="<%=Globals.UrlHot%>">Trang chủ</a></li>
            <asp:Literal ID="ltlMenu" runat="server"></asp:Literal>
         </ul>
      </div>
</section>
  <ul class="box-chung row">
  <asp:Repeater ID="rptProduct" runat="server">
<ItemTemplate>
<li class="col-md-3 col-sm-6 wow animated fadeIn animated animated" data-wow-delay="0.2s" style="visibility: visible; animation-delay: 0.2s; animation-name: fadeIn;">
          <figure><a href="/<%#Eval("shortlink")%>.html"><img src="<%#Eval("fimage")%>" alt="<%#Eval("title") %>" /></a></figure>
          <h3><a href="/<%#Eval("shortlink")%>.html"><%#Eval("title")%></a></h3> 
      </li>
</ItemTemplate>
</asp:Repeater>
  </ul> 
</div>
</div>
<ul class="page pageA pageA06" id="ulPage" runat="server">
  <asp:Literal ID="ltlPage" runat="server"></asp:Literal>
</ul>
</section>
