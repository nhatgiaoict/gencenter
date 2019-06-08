<%@ Control Language="C#" AutoEventWireup="true" CodeFile="login.ascx.cs" Inherits="controls_login" %>
<br>
<br>
<br>
<br>
<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
	<tr>
		<td align="center">
			<br>
			<TABLE id="Table1" height="10" cellSpacing="4" cellPadding="1" width="360" border="0">
				<TBODY>
					<TR>
						<TD class="header3"><TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TBODY>
									<TR>
										<TD class="header3"><TABLE id="Table3" cellSpacing="0" cellPadding="4" width="100%" border="0">
												<TBODY>
													<TR>
														<TD class="header3" width="2"></TD>
														<TD class="header3">Xin chào mừng bạn</TD>
													</TR>
												</TBODY>
											</TABLE>
										</TD>
									</TR>
									<TR>
										<TD><TABLE class="innerTabBg" id="Table4" height="100%" cellSpacing="0" cellPadding="1" width="100%"
												border="0">
												<TBODY>
													<TR>
														<TD align="center" height="20"><TABLE width="94%">
																<TBODY>
																	<TR>
																		<TD align="center"><TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<TBODY>
																					<TR>
																						<TD><img id="imgKey" runat="server" src="http://quanmh/data/images//key.gif" width="70"
																								height="70"></TD>
																						<TD class="header4" vAlign="middle" align="center" width="262"><asp:Literal id="ltlTitle" runat="server"></asp:Literal></TD>
																					</TR>
																					<TR>
																						<TD colSpan="2" height="1"><IMG height="1" src="" width="1"></TD>
																					</TR>
																					<TR>
																						<TD colSpan="2"><div align="justify">Điền chính xác tên truy cập và mật khẩu của bạn 
																								vào các trường tương ứng là: username &amp; password. Sau đó nhấn chuột vào nút 
																								đăng nhập để vào trang quản lý hệ thống</div>
																						</TD>
																					</TR>
																					<TR>
																						<TD align="center" colSpan="2"></TD>
																					</TR>
																					<TR>
																						<TD colSpan="2" height="10"><IMG height="1" src="" width="1"></TD>
																					</TR>
																					<TR>
																						<TD class="Line" colSpan="2" height="1"><IMG height="1" src="" width="1"></TD>
																					</TR>
																					<TR>
																					<TR>
																						<TD colSpan="2" height="20"><IMG height="1" src="Hosting Controller_files/spacerLight.gif" width="1"></TD>
																					</TR>
																					<TR>
																						<TD colSpan="2"><TABLE id="Table6" cellSpacing="0" cellPadding="2" width="100%" border="0">
																								<TBODY>
																									<TR>
																										<TD align="right" width="110" height="22"><STRONG><asp:Literal id="ltlID" runat="server"></asp:Literal>
																												: </STRONG>
																										</TD>
																										<TD width="227" height="22"><INPUT id="txtUserID" style="WIDTH: 150px" type="text" maxLength="100" name="txtUserID"
																												runat="server"></TD>
																									</TR>
																									<TR>
																										<TD colSpan="2" height="10"><IMG height="1" src="Hosting Controller_files/spacerLight.gif" width="1"></TD>
																									</TR>
																									<TR>
																										<TD align="right" width="110" height="22"><STRONG><asp:Literal id="ltlPwd" runat="server"></asp:Literal>
																												: </STRONG>
																										</TD>
																										<TD width="227" height="22"><INPUT id="txtPwd" style="WIDTH: 150px" type="password" maxLength="100" name="txtPwd" runat="server"></TD>
																									</TR>
																									<TR>
																										<TD colSpan="2" align="center"><IMG height="1" src="Hosting Controller_files/spacerLight.gif" width="1">&nbsp;
																											<asp:Button id="btnSubmit" runat="server" Text="Button" OnClick="btnSubmit_Click"></asp:Button>&nbsp;&nbsp;
																											<asp:Button id="btnPassLost" runat="server" Text="Quen mat khau" OnClick="btnPassLost_Click"></asp:Button>
																										</TD>
																									</TR>
																									<tr>
																										<td colspan="2" class="RequireField"><asp:Literal ID="ltlError" Runat="server" Visible="False"></asp:Literal></td>
																									</tr>
																								</TBODY>
																							</TABLE>
																						</TD>
																					</TR>
																				</TBODY>
																			</TABLE>
																		</TD>
																	</TR>
																</TBODY>
															</TABLE>
														</TD>
													</TR>
												</TBODY>
											</TABLE>
										</TD>
									</TR>
								</TBODY>
							</TABLE>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</td>
	</tr>
</table>

