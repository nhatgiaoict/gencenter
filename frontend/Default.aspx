<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page" MasterPageFile="~/MasterPage.master" %>
<%@ Register Src="~/Home/uc_header.ascx" TagName="hd" TagPrefix="uc1" %>
<%@ Register Src="~/Home/uc_slide.ascx" TagName="sld" TagPrefix="uc1" %>
<%@ Register Src="~/Home/uc_service.ascx" TagName="sv" TagPrefix="uc1" %>
<%@ Register Src="~/Home/uc_product.ascx" TagName="pr" TagPrefix="uc1" %>
<%@ Register Src="~/Home/uc_gallery.ascx" TagName="gl" TagPrefix="uc1" %>
<%@ Register Src="~/Home/uc_customer_comment.ascx" TagName="cc" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <header class="page-head page-head-main">
        <uc1:hd ID="hd1" runat="server" />
        <uc1:sld ID="sld1" runat="server" />
    </header>
    <uc1:sv ID="sv1" runat="server" />
    <uc1:pr ID="pr1" runat="server" />
    <uc1:gl ID="gl1" runat="server" />
    <uc1:cc ID="cc1" runat="server" />
</asp:Content>
