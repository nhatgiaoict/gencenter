<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_duanhoanthanh.ascx.cs" Inherits="Home_uc_duanhoanthanh" %>
<section class="project-slide">
       <h3>Dự án hoàn thành</h3>
       <div class="container">
           <div id="slide-pd" class="slide-project">
           <asp:Repeater ID="rptSPF" runat="server">
           <ItemTemplate>
           <div class="pd"><h4><a href="/<%#Eval("shortlink")%>.html"><%#Eval("title")%></a></h4><a href="/<%#Eval("shortlink")%>.html"><figure><img src="<%#Eval("fimage")%>" alt="<%#Eval("title")%>" /></figure></a></div>
           </ItemTemplate>
           </asp:Repeater>
           </div>
       </div>
</section>