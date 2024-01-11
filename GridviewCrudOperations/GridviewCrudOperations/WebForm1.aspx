<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="GridviewCrudOperations.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: center;
        }
        .auto-style4 {
            margin-right: 0px;
            margin:0 auto;
        }
        .auto-style5 {
            font-size: large;
        }
        
    </style>
</head>
<body>
    <h1 style="text-align:center">Employee Details</h1>
    <form id="form1" runat="server">
        
        <table class="auto-style1 table">
            
            <tr>
                <td class="auto-style2" colspan="2">
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label7" runat="server" CssClass="auto-style5"></asp:Label>
                    <br />
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="auto-style4" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"  ShowFooter="True" Width="1317px" BorderColor="LightGray" Height="293px" HorizontalAlign="Center" BackColor="White" >
                        <Columns>
                            <asp:TemplateField HeaderText="Emp_ID">
                                <EditItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("emp_id") %>'></asp:Label>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtaddid" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <HeaderTemplate>
                                    Emp_ID
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("emp_id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp_name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("emp_name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("emp_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp_ email">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Eval("emp_email") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("emp_email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp_phone">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Eval("emp_phone") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("emp_phone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp_Address">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Eval("emp_address") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("emp_address") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update">Update</asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Cancel">Cancel</asp:LinkButton>
                                    &nbsp;
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Insert" OnClick="LinkButton5_Click">Insert</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit">Edit</asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <br />
&nbsp;&nbsp;&nbsp; </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MyDatabaseConnectionString %>" DeleteCommand="DELETE FROM [employee] WHERE [emp_id] = @emp_id" InsertCommand="INSERT INTO [employee] ([emp_name], [emp_email], [emp_phone], [emp_address]) VALUES (@emp_name, @emp_email, @emp_phone, @emp_address)" ProviderName="<%$ ConnectionStrings:MyDatabaseConnectionString.ProviderName %>" SelectCommand="SELECT [emp_id], [emp_name], [emp_email], [emp_phone], [emp_address] FROM [employee]" UpdateCommand="UPDATE [employee] SET [emp_name] = @emp_name, [emp_email] = @emp_email, [emp_phone] = @emp_phone, [emp_address] = @emp_address WHERE [emp_id] = @emp_id">
            <DeleteParameters>
                <asp:Parameter Name="emp_id" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="emp_name" Type="String" />
                <asp:Parameter Name="emp_email" Type="String" />
                <asp:Parameter Name="emp_phone" Type="String" />
                <asp:Parameter Name="emp_address" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="emp_name" Type="String" />
                <asp:Parameter Name="emp_email" Type="String" />
                <asp:Parameter Name="emp_phone" Type="String" />
                <asp:Parameter Name="emp_address" Type="String" />
                <asp:Parameter Name="emp_id" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />
        <br />
    </form>
</body>
</html>
