<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_detailnews.ascx.cs" Inherits="Detail_uc_detailnews" %>
<%@ Register Src="~/Home/uc_header.ascx" TagName="hd" TagPrefix="uc1" %>
<%@ Register Src="~/Parent/uc_menucontext.ascx" TagPrefix="uc1" TagName="uc_menucontext" %>
<%@ Register Src="~/Parent/uc_menunew.ascx" TagPrefix="uc1" TagName="uc_menunew" %>
<header class="page-head">
    <uc1:hd ID="hd1" runat="server" />
</header>
<uc1:uc_menucontext runat="server" ID="uc_menucontext1" />
<main class="page-content">
    <div id="fb-root"></div>
    <section class="section-98 section-sm-110 text-left">
        <div class="shell">
            <div class="range range-xs-center range-lg-right">
                <div class="cell-sm-10 cell-md-8">
                    <h1 class="text-bold text-primary small"><asp:Literal ID="ltltitle" runat="server"></asp:Literal></h1>
                    <ul class="list list-inline list-inline-dashed offset-top-4">
                        <li>
                            <asp:Literal ID="ltldate" runat="server"></asp:Literal></li>
                    </ul>
                    <div class="offset-top-30">
                        <asp:Literal ID="ltlcontent" runat="server"></asp:Literal>
                    </div>
                    <div class="offset-top-30 offset-md-top-66">
                        <div class="pull-sm-right" style="margin-top: -5px;">
                            <div class="reveal-inline-block inset-right-20">
                                <p class="text-dark">Share:</p>
                            </div>
                            <ul class="list-inline list-inline-xs reveal-inline-block offset-top-14 offset-sm-top-0">
                                <li><a class="icon icon-xxs icon-circle icon-gray-light fa fa-facebook" href="#"></a></li>
                                <li><a class="icon icon-xxs icon-circle icon-gray-light fa fa-twitter" href="#"></a></li>
                                <li><a class="icon icon-xxs icon-circle icon-gray-light fa fa-google-plus" href="#"></a></li>
                                <li><a class="icon icon-xxs icon-circle icon-gray-light fa fa-rss" href="#"></a></li>
                            </ul>
                        </div>
                        <div class="clearfix"></div>
                        <div class="offset-top-66">
                            <h6>Bài viết cùng chuyên mục</h6>
                            <hr class="text-subline">
                        </div>
                        <div class="range offset-top-30">
                            <asp:Repeater ID="rptNew" runat="server">
                                <ItemTemplate>
                                    <div class="cell-sm-6">
                                        <article class="post post-modern post-modern-classic">
                                            <div class="post-media">
                                                <a class="link-image" href="/<%#Eval("shortlink")%>.html">
                                                    <img class="img-responsive img-cover" width="370" height="240" src="<%#Eval("fimage")%>" alt="<%#Eval("title") %>" /></a>
                                            </div>
                                            <section class="post-content text-left">
                                                <div class="post-title offset-top-8">
                                                    <h5 class="text-bold"><a href="/<%#Eval("shortlink")%>.html"><%#Eval("title") %></a></h5>
                                                </div>
                                                <ul class="list-inline list-inline-dashed">
                                                    <li><%# Convert.ToDateTime(Eval("created")).ToString("MMM dd, yyyy") %></li>
                                                </ul>
                                                <div class="post-body">
                                                    <p><%#Eval("summary")%></p>
                                                </div>
                                            </section>
                                        </article>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div class="cell-sm-10 cell-md-4 offset-top-66 offset-md-top-0">
                    <uc1:uc_menunew runat="server" ID="uc_menunew" />
                </div>
            </div>
        </div>
    </section>
</main>
<script>(function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/vi_VN/sdk.js#xfbml=1&version=v2.7";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));</script>
<script type="text/javascript" src="https://apis.google.com/js/plusone.js"></script>
