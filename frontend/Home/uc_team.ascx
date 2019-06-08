<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_team.ascx.cs" Inherits="Home_uc_team" %>
<div class="heading-catagories" id="team"> 
<h2>Đội ngũ nhân viên</h2>
</div>
<div class="arc-container">
<div class="container">
<ul class="slide_team box-chung row">
<asp:Repeater ID="rptTeam" runat="server">
<ItemTemplate>
    <li class="col-md-6 col-sm-6 col-xs-12 wow animated fadeIn animated" data-wow-delay="0.2s" id="yk">
        <div class="arc-team">
            <div class="arc-team-img">
                <img src="<%#Eval("fimage")%>" alt="<%#Eval("title")%>" />
            </div>
            <div class="arc-team-info">
                <h3><%#Eval("title")%></h3>
                <p class="sapo"><%#Eval("summary") %></p>
                <p class="sapo"><%#sContent(""+Eval("id")+"")%></p>
            </div>
        </div>
    </li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
</div>