<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_grouphome.ascx.cs" Inherits="Home_uc_grouphome" %>
<%@ Register Src="~/Home/uc_subgroup.ascx" TagName="subg" TagPrefix="uc1" %>
<div class="heading-catagories" style="background:url(/data/data/img_slide/bg-project.jpg)"> 
<h2>Tin tức sự kiện</h2>
<%--<div class="heading-catagories" style="background:url(/data/data/img_slide/bg-project.jpg)">
         <h2>Tin tức sự kiện</h2>
    </div>--%>
<asp:Repeater ID="rptG" runat="server">
<ItemTemplate>
<section id="col-block-bv"> 
<div class="container">
<header class="td_heading"><h2><a href="<%#CMDU.CommanUrl.UrlGroup(""+Eval("plink")+"",""+Eval("shortlink")+"")%>"><%#Eval("ptitle")%></a><i class="fa fa-angle-right"></i></h2></header>
<uc1:subg ID="sb1" runat="server" GruoupID='<%#Eval("pid")%>' />
</div>
</section>
</ItemTemplate>
</asp:Repeater>
</div>

