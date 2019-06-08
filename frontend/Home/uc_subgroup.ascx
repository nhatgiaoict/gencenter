<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_subgroup.ascx.cs" Inherits="Home_uc_subgroup" %>
<div class="heading-catagories"> 
<h2>What we do</h2>
</div>
<div class="container">
<ul class="box-chung row">
<asp:Repeater ID="rptG" runat="server">
<ItemTemplate>
<li class="col-md-4 col-sm-4 col-xs-12 wow animated fadeIn animated" data-wow-delay="0.2s">
<h3><a href="<%#CMDU.CommanUrl.UrlGroup(""+Eval("plink")+"",""+Eval("shortlink")+"")%>"><%#Eval("ptitle")%></a></h3>
<p class="sapo"><%#Eval("summary") %></p>
<figure><a href="<%#CMDU.CommanUrl.UrlGroup(""+Eval("plink")+"",""+Eval("shortlink")+"")%>"><img src="<%#Eval("fimage")%>" alt="<%#Eval("ptitle")%>" /></a></figure>
</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>

<div class="our-process"> 
<h2>Quy trình làm việc</h2>
    <div>
        <ul>
        <li>
            <h5>MEET & AGREE</h5>
        </li>
        <li>
            <h5>IDEA & CONCEPT</h5>
        </li>
        <li>
            <h5>DESIGN & CREATE</h5>
        </li>
        <li>
            <h5>BUILD & INSTALL</h5>
        </li>
    </ul>
    </div>
        
<p>With years of experience in web and mobile design, we create engaging mobile <br> 
   experiences that are visually beautiful, eye-catching and stand out in the <br />
   marketplace. We follow client’s brand identity or help create it from the scratch.</p>
</div>