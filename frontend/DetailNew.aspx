<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DetailNew.aspx.cs" Inherits="DetailNew" Title="Untitled Page" %>
<%@ Register Src="~/Detail/uc_detailkind.ascx" TagName="uc_detailkind" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:uc_detailkind ID="checkdetail" runat="server" />
</asp:Content>

