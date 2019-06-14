<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_product.ascx.cs" Inherits="product_uc_product" %>
<%@ Register Src="~/Home/uc_header.ascx" TagName="hd" TagPrefix="uc1" %>
<%@ Register Src="~/Parent/uc_menucontext.ascx" TagPrefix="uc1" TagName="uc_menucontext" %>

<header class="page-head">
    <uc1:hd ID="hd1" runat="server" />
</header>
<uc1:uc_menucontext runat="server" ID="uc_menucontext1" />
<main class="page-content">
    <div id="fb-root"></div>
    <section class="section-98 section-sm-110">
        <div class="shell">
            <div class="range range-xs-center">
                <div class="cell-md-10 cell-lg-8">
                    <div class="range range-xs-center">
                        <asp:Repeater ID="rptProduct" runat="server">
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
                                            <%--<div class="tags group group-sm">
                                                <a class="btn-tag btn btn-default" href="#">Health</a><a class="btn-tag btn btn-default" href="#">News</a><a class="btn-tag btn btn-default" href="#">Diagnostics</a>
                                            </div>--%>
                                        </section>
                                    </article>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="offset-top-50 offset-lg-top-60 text-lg-left">
                        <nav>
                            <ul class="pagination-classic" id="ulPage" runat="server">
                                <asp:Literal ID="ltlPage" runat="server"></asp:Literal>
                            </ul>
                        </nav>
                    </div>
                </div>
                <div class="cell-sm-10 cell-md-8 cell-lg-4 offset-top-66 offset-md-top-90 offset-lg-top-0">
                </div>
            </div>
        </div>
    </section>
</main>