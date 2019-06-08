<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_parentnews.ascx.cs" Inherits="control_uc_parentnews" %>

<section id="news-cate"> 
    <div class="heading-catagories"><h2>Tin tức</h2>
    </div><!--End--> 
    <div class="container">
         <div class="row">    
          <div class="sidebar-left col-md-9 col-sm-8 col-xs-12"> 
              <div class="bg-news">
           <asp:Repeater ID="rptN" runat="server">
           <ItemTemplate>
              <div class="thumbnail-news wow animated fadeIn animated" data-wow-delay="0.2s">
                        <figure><img src="<%#Eval("fimage")%>" alt="<%#Eval("title")%>" /></figure>
                        <div class="caption">
                            <h3><a href="/<%#Eval("shortlink")%>.html"><%#Eval("title")%></a></h3>
                            <p class="date"><i class="fa fa-clock-o"></i> <%#DateTime.Parse(""+Eval("created")+"").ToString("dd/MM/yyyy")%></p>
                            <p class="summary"><%#Eval("summary")%></p> 
                        </div>  
                    </div>
              </ItemTemplate>
              </asp:Repeater> 
               <ul class="page pageA pageA06">
                   <asp:Literal ID="ltlPage" runat="server"></asp:Literal>
               </ul>
            </div><!--End-->
          </div><!--End-sidebar-left-->
          <div class="sidebar-right col-md-3 col-sm-4 col-xs-12">
             <aside class="c-sidebar"> 
                    <h2>Dự án nổi bật</h2>
                    <ul class="xemnhieu">
                    <asp:Literal ID="ltlNoidung" runat="server"></asp:Literal>
                   </ul> 
              </aside><!--End-->
          </div><!--End-sidebar-right-->
     </div>
    </div>   
</section>



