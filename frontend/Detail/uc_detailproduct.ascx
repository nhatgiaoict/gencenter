<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_detailproduct.ascx.cs" Inherits="Detail_uc_detailproduct" %>
<%@ Register Src="~/Parent/uc_menuright.ascx" TagName="mnr" TagPrefix="uc1" %>
<%--<%@ Register Src="~/Home/uc_grouphome.ascx" TagName="ghom" TagPrefix="uc1" %>--%>
<section id="news-cate">
    <div class="heading-catagories" style="<%=sBg%>">
         <h2><asp:Literal ID="ltltitlegroup" runat="server"></asp:Literal></h2>
    </div>
    <div class="container">
         <div class="row">    
          <div class="sidebar-left col-md-9 col-sm-8 col-xs-12"> 
              <div class="bg-news"> 
                <article class="thumbnail-news-view">
                     <h1><asp:Literal ID="ltltitle" runat="server"></asp:Literal></h1>
                     <div class="block_timer_share">
                           <div class="block_timer pull-left"><i class="fa fa-clock-o"></i> <asp:Literal ID="ltlData" runat="server"></asp:Literal></div>
                           <div class="block_share pull-right">
<div class="fb-like" style="float:left" data-href="<%=Globals.UrlHot %>/<%=Request.RawUrl %>" data-layout="button_count" data-action="like" data-show-faces="true" data-share="true"></div>
<div class="g-plusone" data-size="medium" data-width="50" data-annotation="bubble" data-align="right"></div>
<div class="g-plus" data-action="share" data-annotation="bubble" data-align="right">&nbsp;&nbsp;</div>
                          </div>
                    </div>
                     
                    <div class="post_content"> 
                        <p><strong><asp:Literal ID="ltlSummary" runat="server"></asp:Literal></strong></p>
                        <div class="fotorama" data-autoplay="true" data-allowfullscreen="true" data-nav="thumbs" data-keyboard="true"  data-width="100%">    
                        <asp:Literal ID="ltlImage" runat="server"></asp:Literal>
                        </div>
                         <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
                    </div>
               </article>
              </div><!--End-->
          </div><!--End-sidebar-left-->
          <div class="sidebar-right col-md-3 col-sm-4 col-xs-12">
          <uc1:mnr ID="mn1" runat="server" />
             <aside class="c-sidebar"> 
                    <h2>Thiết kế nổi bật</h2>
                    <ul class="xemnhieu">
                    <asp:Literal ID="ltlNoidungnb" runat="server"></asp:Literal>
                   </ul> 
              </aside>
          </div>
     </div>
    </div>   
</section><!--End-news-->    
<section id="project-da">
    <div class="container">
        <ul class="project-lq"><h2>Dự án liên quan</h2>
        <asp:Repeater ID="rptNew" runat="server">
             <ItemTemplate>
              <li>
                  <figure><a href="/<%#Eval("shortlink")%>.html"><img src="<%#Eval("fimage")%>" alt="<%#Eval("title")%>" /></a></figure>
                  <figcaption><a href="/<%#Eval("shortlink")%>.html"><%#Eval("title")%></a></figcaption>
             </li>
             </ItemTemplate>
             </asp:Repeater>
            <asp:Repeater ID="rptSpK" runat="server">
             <ItemTemplate>
              <li>
                  <figure><a href="/<%#Eval("shortlink")%>.html"><img src="<%#Eval("fimage")%>" alt="<%#Eval("title")%>" /></a></figure>
                  <figcaption><a href="/<%#Eval("shortlink")%>.html"><%#Eval("title")%></a></figcaption>
             </li>
             </ItemTemplate>
             </asp:Repeater>
             
        </ul>  
    </div>
</section>
<%--<uc1:ghom ID="ghom1" runat="server" />--%>
<script> (function(d, s, id) {
     var js, fjs = d.getElementsByTagName(s)[0];
     if (d.getElementById(id)) return;
     js = d.createElement(s); js.id = id;
     js.src = "//connect.facebook.net/vi_VN/sdk.js#xfbml=1&version=v2.7";
     fjs.parentNode.insertBefore(js, fjs);
 }(document, 'script', 'facebook-jssdk'));</script>
<script type="text/javascript" src="https://apis.google.com/js/plusone.js"></script>