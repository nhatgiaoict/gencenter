<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_SPTabHome.ascx.cs" Inherits="Home_uc_SPTabHome" %>
<section id="product">
<div class="tabs tabs-style-line">
<nav>
<ul>
<asp:Repeater ID="rptMenu" runat="server">
<ItemTemplate>
<li class=""><a href="#section-iconbox-<%#Eval("bidx")%>"><span><%#Eval("ptitle")%></span></a></li>
</ItemTemplate>
</asp:Repeater>
</ul>
</nav>
<div class="content-wrap">
<asp:Repeater ID="rptGNew" runat="server" onitemdatabound="rptGNew_ItemDataBound">
<ItemTemplate>
<section id="section-iconbox-<%#Eval("bidx")%>" class="">
     <div class="container">
            <div class="grid-image">
                <asp:Repeater ID="rptNew" runat="server">
                <ItemTemplate>
					<figure class="effect-ming">
						<img src="<%#Eval("fimage") %>" alt="<%#Eval("title") %>">
						<figcaption>
							<a href="<%#Eval("shortlink")%>.html"<%-- data-toggle="modal" data-target="#myModal"--%>></a>
						</figcaption>			
					</figure>
                    </ItemTemplate>
                    </asp:Repeater>
			</div><!--End-gid-image-->
     </div>
</section>
</ItemTemplate>
</asp:Repeater>
</div>
</div>
</section>

<script type="text/javascript" src="/data/js/cbpFWTabs.js"></script>
<script type="text/javascript">
(function () {

    [].slice.call(document.querySelectorAll('.tabs')).forEach(function (el) {
        new CBPFWTabs(el);
    });

})();
</script>

