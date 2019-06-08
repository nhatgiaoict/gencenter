<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_navbar.ascx.cs" Inherits="Home_uc_navbar" %>
<div class="rd-navbar-wrap">
    <nav class="rd-navbar rd-navbar-minimal rd-navbar-light" data-md-device-layout="rd-navbar-fixed" data-lg-device-layout="rd-navbar-static" data-md-stick-up-offset="120px" data-lg-stick-up-offset="120px" data-lg-auto-height="true" data-body-class="rd-navbar-absolute" data-md-layout="rd-navbar-fixed" data-lg-layout="rd-navbar-static" data-lg-stick-up="true">
        <div class="rd-navbar-inner">
            <div class="rd-navbar-top-panel">
                <!--Navbar Brand-->
                <div class="rd-navbar-brand veil reveal-md-inline-block"><a href="index-2.html">
                    <img width='151' height='52' class='img-responsive' src='data/images/logo-dark.png' alt='' /></a></div>
                <div>
                    <address class="contact-info reveal-sm-inline-block text-left offset-none">
                        <div class="p unit unit-spacing-xs unit-horizontal">
                            <div class="unit-left"><span class="icon icon-xs icon-circle icon-white-17 mdi mdi-phone text-primary"></span></div>
                            <div class="unit-body"><a class="text-white" href="tel:#">0973489705</a></div>
                        </div>
                    </address>
                    <address class="contact-info reveal-sm-inline-block text-left">
                        <div class="p unit unit-horizontal unit-spacing-xs">
                            <div class="unit-left"><span class="icon icon-xs icon-circle icon-white-17 mdi mdi-map-marker text-primary"></span></div>
                            <div class="unit-body"><a class="text-white" href="#">Hoàng Long, Phú xuyên, Hà Nội</a></div>
                        </div>
                    </address>
                </div>
            </div>
            <!-- RD Navbar Panel-->
            <div class="rd-navbar-panel">
                <!-- RD Navbar Toggle-->
                <button class="rd-navbar-toggle" data-rd-navbar-toggle=".rd-navbar, .rd-navbar-nav-wrap"><span></span></button>
                <!--Navbar Brand-->
                <div class="rd-navbar-brand veil-lg"><a href="index-2.html">
                    <img width='151' height='52' class='img-responsive' src='data/images/logo-dark.png' alt='' /></a></div>
                <button class="rd-navbar-top-panel-toggle" data-rd-navbar-toggle=".rd-navbar, .rd-navbar-top-panel"><span></span></button>
            </div>
            <div class="rd-navbar-menu-wrap">
                <div class="rd-navbar-nav-wrap">
                    <div class="rd-navbar-mobile-scroll">
                        <!--Navbar Brand Mobile-->
                        <div class="rd-navbar-mobile-brand"><a href="index-2.html">
                            <img width='151' height='52' class='img-responsive' src='data/images/logo-dark.png' alt='' /></a></div>
                        <div class="form-search-wrap">
                            <!-- RD Search Form-->
                            <form class="form-search rd-search" action="https://livedemo00.template-help.com/wt_prod-20176/search-results.html" method="GET">
                                <div class="form-group">
                                    <label class="form-label form-search-label form-label-sm" for="rd-navbar-form-search-widget">Search</label>
                                    <input class="form-search-input form-control #{inputClass}" id="rd-navbar-form-search-widget" type="text" name="s" autocomplete="off" />
                                </div>
                                <button class="form-search-submit" type="submit"><span class="fa fa-search text-primary"></span></button>
                            </form>
                        </div>
                        <!-- RD Navbar Nav-->
                        <ul class="rd-navbar-nav">
                            <li class="active"><a href="index.html"><span>Trang chủ</span></a>
                            </li>
                            <li><a href="#adn-about"><span>Giới thiệu</span></a>
                            </li>
                            <li><a href="#adn-service"><span>Dịch vụ</span></a>
                                <ul class="rd-navbar-dropdown">
                                    <li><a href="blog-masonry.html"><span class="text-middle">Xét nghiệm ADN huyết thống</span></a>
                                    </li>
                                    <li><a href="blog-modern.html"><span class="text-middle">Làm giấy khai sinh - Nhập tịch</span></a>
                                    </li>
                                    <li><a href="blog-classic.html"><span class="text-middle">Sàng lọc trước sinh NIPT</span></a>
                                    </li>
                                </ul>
                            </li>
                            <li><a href="#adn-expert"><span>Chuyên gia</span></a>
                            </li>
                            <li><a href="#adn-gallery"><span>Hình ảnh</span></a>
                            </li>
                            <li><a href="#adn-experience"><span>Kinh nghiệm</span></a>
                            </li>
                        </ul>
                    </div>
                </div>
                <!--RD Navbar Search-->
                <div class="rd-navbar-search">
                    <a class="rd-navbar-search-toggle mdi" data-rd-navbar-toggle=".rd-navbar-menu-wrap,.rd-navbar-search" href="#"><span></span></a>
                    <form class="rd-navbar-search-form search-form-icon-right rd-search" action="https://livedemo00.template-help.com/wt_prod-20176/search-results.html" data-search-live="rd-search-results-live" method="GET">
                        <div class="form-group">
                            <label class="form-label" for="rd-navbar-search-form-input">Type and hit enter...</label>
                            <input class="rd-navbar-search-form-input form-control form-control-gray-lightest" id="rd-navbar-search-form-input" type="text" name="s" autocomplete="off" />
                        </div>
                        <div class="rd-search-results-live" id="rd-search-results-live"></div>
                    </form>
                </div>
            </div>
        </div>
    </nav>
</div>
