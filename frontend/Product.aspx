<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Product" Title="Untitled Page" %>
<%@ Register Src="~/product/uc_checkkind.ascx" TagName="product" TagPrefix="uc1"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:product ID="pr1" runat="server" />
</asp:Content>