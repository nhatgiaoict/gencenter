<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_hotproduct.ascx.cs" Inherits="Home_uc_hotproduct" %>
<section class="project-slide">
   <h3>Thiết kế nổi bật</h3>
   <div class="container">
       <div id="slide-pd" class="slide-project">
       <asp:Repeater ID="rptNB" runat="server">
       <ItemTemplate>
       <div class="pd"><a href="/<%#Eval("shortlink")%>.html"><figure><img src="<%#Eval("fimage")%>" alt="<%#Eval("title")%>" /></figure></a></div>
       </ItemTemplate>
       </asp:Repeater>
       </div>
   </div>
</section>