<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_hotnew.ascx.cs" Inherits="Home_uc_hotnew" %>
<section id="project">
<div class="container">
<div class="row">
<asp:Repeater ID="rptG" runat="server" onitemdatabound="rptG_ItemDataBound">
<ItemTemplate>
<div class="heading-box col-md-12"><h2><%#Eval("ptitle")%></h2></div>
<div id="grid-wrap">     
<div id="p-slider" class="slide-spbc owl-carousel owl-theme">
<asp:Repeater ID="rptHotnew" runat="server">
<ItemTemplate>
    <div class="item">
		<div class="product-img">
			<img src="<%#Eval("fimage") %>" class="img-responsive" alt="<%#Eval("title") %>">
		</div>
		<div class="row">
			<div class="col-md-12">
				<div class="product-title"><a href="/<%#Eval("shortlink") %>.html"><%#Eval("title") %></a></div>
			</div>
		</div>
	</div>
</ItemTemplate>
</asp:Repeater>
</div>       
</div>
</ItemTemplate>
</asp:Repeater>
</div> 
</div>
</section>
