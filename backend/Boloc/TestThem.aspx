<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestThem.aspx.cs" Inherits="Boloc_TestThem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
<asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="upPanael" runat="server">
    <ContentTemplate>
        <div><div>
            <table>
                <tr>
                    <th>
                        &nbsp;
                    </th>
                    <th>
                        Employee Name
                    </th>
                    <th>
                        Adderss
                    </th>
                    <th>
                        City
                    </th>
                    <th>
                        Email
                    </th>
                </tr>
                 <asp:Repeater ID="rptEmployee" runat="server">
                        <ItemTemplate>
                          <tr>
                            <td>
                                 &nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" 
                                 Width="150px" MaxLength="50" Text='<%#Eval("EmpName") %>'>
</asp:TextBox>
                            </td>
                            <td>
                                 <asp:TextBox ID="TextBox2" runat="server" 
                                  Width="150px" MaxLength="50" Text='<%#Eval("Address") %>'>
</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" 
                                Width="150px" MaxLength="50" Text='<%#Eval("City") %>'>
</asp:TextBox>
                            </td>
                            <td>
                                 <asp:TextBox ID="TextBox4" runat="server" 
                                  Width="150px" MaxLength="50" Text='<%#Eval("Email") %>'>
</asp:TextBox>
                            </td>
                        </tr>
                     </ItemTemplate>
                   </asp:Repeater>
                <tr>
                    <td>
                       <asp:Button ID="btnAdd" runat="server" 
                       Text="Add New" onclick="btnAdd_Click" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmpName" runat="server" 
                        Width="150px" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                         <asp:TextBox ID="txtAddress" runat="server" 
                         Width="150" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCity" runat="server" 
                        Width="150px" MaxLength="50"></asp:TextBox>
                    </td>
                    <td>
                         <asp:TextBox ID="txtEmail" runat="server" 
                         Width="150px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                  
            </table>
        </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
    </form>
</body>
</html>
