<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_menunew.ascx.cs" Inherits="Parent_uc_menunew" %>
<div class="blog-grid-sidebar inset-lg-left-30">
    <aside class="text-left">
        <div class="">
            <h6>Thư viện ảnh</h6>
            <hr class="text-subline">
        </div>
        <div class="offset-top-14 offset-md-top-20">
            <div class="group-sm" data-lightgallery="group">
                <a class="thumbnail-classic" data-lightgallery="item" href="/data/images/gallery-01_original.jpg">
                    <img width="165" height="165" src="/data/images/sidebar-img-01-165x165.jpg" alt="">
                </a>
                <a class="thumbnail-classic" data-lightgallery="item" href="/data/images/gallery-02_original.jpg">
                    <img width="165" height="165" src="/data/images/sidebar-img-02-165x165.jpg" alt="">
                </a>
                <a class="thumbnail-classic" data-lightgallery="item" href="/data/images/gallery-03_original.jpg">
                    <img width="165" height="165" src="/data/images/sidebar-img-03-165x165.jpg" alt="">
                </a>
                <a class="thumbnail-classic" data-lightgallery="item" href="/data/images/gallery-04_original.jpg">
                    <img width="165" height="165" src="/data/images/sidebar-img-04-165x165.jpg" alt="">
                </a>
                <a class="thumbnail-classic" data-lightgallery="item" href="/data/images/gallery-05_original.jpg">
                    <img width="165" height="165" src="/data/images/sidebar-img-05-165x165.jpg" alt="">
                </a>
                <a class="thumbnail-classic" data-lightgallery="item" href="/data/images/gallery-06_original.jpg">
                    <img width="165" height="165" src="/data/images/sidebar-img-06-165x165.jpg" alt="">
                </a>
            </div>
        </div>
        <div class="offset-top-30 offset-md-top-60">
            <h6>Bài viết gần đây</h6>
            <hr class="text-subline">
        </div>
        <div class="offset-top-14 offset-md-top-20">
            <ul class="list list-marked list-marked-icon text-dark inset-left-0 list-marked-gray">
                <asp:Repeater ID="rptRecentNews" runat="server">
                    <ItemTemplate>
                        <li><a class="text-bold" href="/<%#Eval("shortlink")%>.html"><%#Eval("title") %></a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </aside>
</div>
