<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rowcrudform.aspx.cs" EnableEventValidation="false" Inherits="CrudRowCommands.rowcrudform" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.4.1/dist/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous" />

    <style type="text/css">
        
        
        #Button1{
           margin-left:45%;
            text-align:center;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="table">
            <tr>
                <td style="font-family: Arial; font-weight: bold; text-align: center;">Employee Details displayed using RowCommands<br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="font-size: large; font-weight: bold; font-style: inherit; text-align: center; background-color: #C0C0C0">Employee Details</td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <br />
                    <br />
                    <br />
                    <asp:GridView ID="CrudRowGrid" CssClass= "table table-striped table-bordered table-condensed" runat="server" AutoGenerateColumns="False" ShowFooter="True"  HorizontalAlign="Left"  Height="89px" OnRowCommand="CrudRowGrid_RowCommand" OnRowDataBound="CrudRowGrid_RowDataBound" >
                        <Columns>
                            <asp:TemplateField HeaderText="Emp_id">
                                <EditItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("emp_id") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("emp_id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp_Name">
                                <EditItemTemplate>
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" Text='<%# Eval("emp_name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox CssClass="form-control" placeholder="Enter Employee name" ID="TextBox6" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("emp_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp_email">
                                <EditItemTemplate>
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" Text='<%# Eval("emp_email") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox CssClass="form-control" placeholder="Enter EmailID" ID="TextBox7" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("emp_email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp_phone">
                                <EditItemTemplate>
                                    <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" Text='<%# Eval("emp_phone") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox CssClass="form-control" placeholder="Enter Phonenumber" ID="TextBox8" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("emp_phone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp_address">
                                <EditItemTemplate>
                                    <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" Text='<%# Eval("emp_address") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox CssClass="form-control" placeholder="Enter Address" ID="TextBox9" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("emp_address") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emp_salary">
                                <EditItemTemplate>
                                    <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" Text='<%# Eval("emp_salary") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox CssClass="form-control" placeholder="Enter Salary" ID="TextBox10" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("emp_salary") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Action">
                                
                                 <EditItemTemplate>
                                     <asp:Button CssClass="btn btn-primary" ID="Updatebutton" runat="server" CommandName="UpdateRow" CommandArgument="<%# Container.DataItemIndex %>" Text="Update" />
                                     &nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:Button CssClass="btn btn-warning" ID="Cancelbutton" runat="server" CommandName="Canceledit" Text="Cancel" />
                                     &nbsp;&nbsp;&nbsp;&nbsp;
                                 </EditItemTemplate>
                                 <FooterTemplate>
                                     <asp:Button CssClass="btn btn-success" ID="Addbutton" runat="server" CommandName="Add" Text="Add Employee" />
                                 </FooterTemplate>
                                 <ItemTemplate>
                                     <asp:Button CssClass="btn btn-info" ID="Editbutton" runat="server" CommandName="EditRow" CommandArgument="<%# Container.DataItemIndex %>" Text="Edit" />
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:Button CssClass="btn btn-danger" ID="Deletebutton" runat="server" CommandName="DeleteRow" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return confirm('Are you sure you want to delete this row?')" Text="Delete" />
                                 </ItemTemplate>
                                
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        </div>
        
        &nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="download_click" Text="Download Data" Width="175px" CssClass="btn btn-primary" />
        <br />
        <br />
        
        <br />
        
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EmployeeConnectionString %>" DeleteCommand="DELETE FROM [employee_details] WHERE [emp_id] = @emp_id" InsertCommand="INSERT INTO [employee_details] ([emp_name], [emp_email], [emp_phone], [emp_address], [emp_salary]) VALUES (@emp_name, @emp_email, @emp_phone, @emp_address, @emp_salary)" SelectCommand="SELECT * FROM [employee_details]" UpdateCommand="UPDATE [employee_details] SET [emp_name] = @emp_name, [emp_email] = @emp_email, [emp_phone] = @emp_phone, [emp_address] = @emp_address, [emp_salary] = @emp_salary WHERE [emp_id] = @emp_id">
            <DeleteParameters>
                <asp:Parameter Name="emp_id" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="emp_name" Type="String" />
                <asp:Parameter Name="emp_email" Type="String" />
                <asp:Parameter Name="emp_phone" Type="String" />
                <asp:Parameter Name="emp_address" Type="String" />
                <asp:Parameter Name="emp_salary" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="emp_name" Type="String" />
                <asp:Parameter Name="emp_email" Type="String" />
                <asp:Parameter Name="emp_phone" Type="String" />
                <asp:Parameter Name="emp_address" Type="String" />
                <asp:Parameter Name="emp_salary" Type="Int32" />
                <asp:Parameter Name="emp_id" Type="Int32" />
            </UpdateParameters> 
        </asp:SqlDataSource>
    </form>
</body>




<script src="https://code.jquery.com/jquery-3.4.1.slim.min.js" integrity="sha384-J6qa4849blE2+poT4WnyKhv5vZF5SrPo0iEjwBvKU7imGFAV0wwj1yYfoRSJoZ+n" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.4.1/dist/js/bootstrap.min.js" integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6" crossorigin="anonymous"></script>
</html>
