<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_detailnews.ascx.cs" Inherits="Detail_uc_detailnews" %>
<section id="news-cate">
    <div class="heading-catagories" style="<%=sBg%>">
         <h2><asp:Literal ID="ltltitlegroup" runat="server"></asp:Literal></h2>
    </div><!--End-->
    <div class="container">
         <div class="row">    
          <div class="sidebar-left col-md-9 col-sm-8 col-xs-12"> 
              <div class="bg-news"> 
                <article class="thumbnail-news-view">
                     <h1><asp:Literal ID="ltltitle" runat="server"></asp:Literal></h1>
                     <div class="block_timer_share">
                           <div class="block_timer pull-left"><i class="fa fa-clock-o"></i> <asp:Literal ID="ltldate" runat="server"></asp:Literal></div>
                           <div class="block_share pull-right">
                           <div class="fb-like" style="float:left" data-href="<%=Globals.UrlHot %>/<%=Request.RawUrl %>" data-layout="button_count" data-action="like" data-show-faces="true" data-share="true"></div>
<div class="g-plusone" data-size="medium" data-width="50" data-annotation="bubble" data-align="right"></div>
<div class="g-plus" data-action="share" data-annotation="bubble" data-align="right">&nbsp;&nbsp;</div>
                          </div>
                    </div>
                    <div class="post_content"> 
                    <asp:Literal ID="ltlcontent" runat="server"></asp:Literal>
                    </div>
                    
                    <ul class="other-news-detail">
                         <h2>Tin tức khác</h2>
                         <asp:Repeater ID="rptNew" runat="server">
<ItemTemplate>
 <li>
                              <figure><a href="/<%#Eval("shortlink")%>.html"><img src="<%#Eval("fimage")%>" alt="<%#Eval("title")%>"/></a></figure>
                              <figcaption><a href="/<%#Eval("shortlink")%>.html"><%#Eval("title")%></a></figcaption>
                         </li>
</ItemTemplate>
</asp:Repeater>
<asp:Repeater ID="rptOld" runat="server">
<ItemTemplate>
<li>
<figure><a href="/<%#Eval("shortlink")%>.html"><img src="<%#Eval("fimage")%>" alt="<%#Eval("title")%>"/></a></figure>
<figcaption><a href="/<%#Eval("shortlink")%>.html"><%#Eval("title")%></a></figcaption>
</li>
</ItemTemplate>
</asp:Repeater>
                    </ul>  
               </article>
              </div><!--End-->
          </div><!--End-sidebar-left-->
          
          <div class="sidebar-right col-md-3 col-sm-4 col-xs-12">
             <aside class="c-sidebar"> 
                    <h2>Dự án nổi bật</h2>
                    <ul class="xemnhieu">
                    <asp:Literal ID="ltlNoidung" runat="server"></asp:Literal>
                   </ul> 
              </aside><!--End-->
          </div><!--End-sidebar-right-->
                
     </div>
    </div>   
</section>
<script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/vi_VN/sdk.js#xfbml=1&version=v2.7";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
<script type="text/javascript" src="https://apis.google.com/js/plusone.js"></script>
