<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Lienhe_sub.ascx.cs" Inherits="controls_Lienhe_sub" %> 

<section id="s2home-contact">  
    <div class="heading-catagories">
         <h2>Liên hệ</h2>
    </div><!--End--> 
    <div class="container">
        <h2 class="title-item">Mẫu nhà đẹp 365 - Thiết kế kiến trúc, nội thất, cảnh quan</h2>
           <section>
               <iframe frameborder="0" height="350" marginheight="0" marginwidth="0" scrolling="no" src="https://www.google.com/maps/d/embed?mid=1_HDh1lemnYdd2JlQbFMUzRPjZoY" width="100%"></iframe>
           </section>
         <div class="row">     
             <div class="form-contact">
               <div class="contact-info col-md-6 col-sm-6 col-xs-12"><h2 class="title-item">RUBIK ARCHITECT</h2>
                        <asp:Literal ID="ltlAddCity" runat="server"></asp:Literal>
                        
                 </div>
                   <div class="col-md-6 col-sm-6 col-xs-12">
                       <h2 class="title-item">Gửi Email cho chúng tôi</h2>
                       <div class="row">
                       <form action="" method="post" class="s2home-form">
                            <p class="col-md-12 field"><input type="text" name="txtHoten" id="txtHoten" size="40" placeholder="Họ tên:" /></p>
                            <p class="col-md-12 field"><input type="text" name="txtEmail" id="txtEmail" value="" size="40" placeholder="Email:" /></p> 
                            <p class="col-md-12 field"><input type="text" name="txtTel" id="txtTel" value="" size="40" placeholder="Điện thoại:" /></p> 
                            <p class="col-md-12 field"><textarea name="your-message" id="txtNoidung" name="txtNoidung" cols="40" rows="10" placeholder="Message:"></textarea></p>
                            <p class="col-md-12 field"><input type="text" name="txtCode" id="txtCode" value="" style="width:150px" placeholder="Mã xác nhận" /><img src="../radom_images.aspx" /></p> 
                            <p class="col-md-12 submit-wrap ">
                                <input type="button" onclick="GuiYeuCau()" value="Gửi email" class="btn-gui btn-primary"> 
                            </p> 
                       </form>
                       <div id="divloadding" style="display:none"><img src="../images/loading.gif" /> Đang gửi yêu cầu xin vui lòng chờ giây lát!</div>
                       </div>
                   </div>
           </div>
         </div>
    </div>   
</section>
<script src="../data/js/common.js" type="text/javascript"></script>
 