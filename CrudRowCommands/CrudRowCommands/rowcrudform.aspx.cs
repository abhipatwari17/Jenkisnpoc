using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace CrudRowCommands
{
    public partial class rowcrudform : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        string EmployeeConnectionString = "Data Source=DESKTOP-TQ6GMES\\SQLEXPRESS;Initial Catalog=ShoppingCart;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();

            }
        }
        public void BindData()
        {
            CrudRowGrid.DataSource = SqlDataSource1;
            CrudRowGrid.DataBind();
        }
        private static void ShowAlertMessage(string error)
        {
            System.Web.UI.Page page = System.Web.HttpContext.Current.Handler as System.Web.UI.Page;
            if (page != null)
            {
                error = error.Replace("'", "\'");
                System.Web.UI.ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);
            }
        }
        
        public void CreateConnection()
        {
            con = new SqlConnection(EmployeeConnectionString);
            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SP_EmpCrudProcedure";
            cmd.CommandType = CommandType.StoredProcedure;
        }

        protected void CrudRowGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String com = e.CommandName;
            if(com == "Add")
            {
                try
                {
                    TextBox name = CrudRowGrid.FooterRow.FindControl("TextBox6") as TextBox;
                    TextBox email = CrudRowGrid.FooterRow.FindControl("TextBox7") as TextBox;
                    TextBox phone = CrudRowGrid.FooterRow.FindControl("TextBox8") as TextBox;
                    TextBox address = CrudRowGrid.FooterRow.FindControl("TextBox9") as TextBox;
                    TextBox salary = CrudRowGrid.FooterRow.FindControl("TextBox10") as TextBox;

                    //String insertdata = "EXEC SP_EmpCrudProcedure @EmpId='" + null + "', @Name='" + name.Text + "',@Email='" + email.Text + "',@PhoneNumber='" + phone.Text + "',@Address='" + address.Text + "',@Salary='" + Convert.ToInt32(salary.Text) + "',@Event='" + com + "'";

                    CreateConnection();

                    cmd.Parameters.AddWithValue("@Event", "Add");
                    cmd.Parameters.AddWithValue("@EmpId", 0);
                    cmd.Parameters.AddWithValue("@Name", name.Text);
                    cmd.Parameters.AddWithValue("@Email", email.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phone.Text);
                    cmd.Parameters.AddWithValue("@Address", address.Text);
                    cmd.Parameters.AddWithValue("@Salary", Convert.ToInt32(salary.Text));
                    cmd.ExecuteNonQuery();

                    BindData();
                }
                catch(Exception ex)
                {
                    Response.Write("<script type=\"text/javascript\">alert('All Fields Required!!!');</script>");

                }
            }
            else if (com == "EditRow")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                CrudRowGrid.EditIndex = index;

                BindData();

            }
            else if(com == "UpdateRow")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);

                    Label emp_id = CrudRowGrid.Rows[index].FindControl("Label7") as Label;
                    TextBox name = CrudRowGrid.Rows[index].FindControl("TextBox1") as TextBox;
                    TextBox email = CrudRowGrid.Rows[index].FindControl("TextBox2") as TextBox;
                    TextBox phone = CrudRowGrid.Rows[index].FindControl("TextBox3") as TextBox;
                    TextBox address = CrudRowGrid.Rows[index].FindControl("TextBox4") as TextBox;
                    TextBox salary = CrudRowGrid.Rows[index].FindControl("TextBox5") as TextBox;

                    //String updatedata = "EXEC SP_EmpCrudProcedure @EmpId='" + Convert.ToInt32(emp_id.Text) + "', @Name='" + name.Text + "',@Email='" + email.Text + "',@PhoneNumber='" + phone.Text + "',@Address='" + address.Text + "',@Salary='" + Convert.ToInt32(salary.Text) + "',@Event='Update'";
                    CreateConnection();
                    cmd.Parameters.AddWithValue("@Event", "Update");
                    cmd.Parameters.AddWithValue("@EmpId", Convert.ToInt32(emp_id.Text));
                    cmd.Parameters.AddWithValue("@Name", name.Text);
                    cmd.Parameters.AddWithValue("@Email", email.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phone.Text);
                    cmd.Parameters.AddWithValue("@Address", address.Text);
                    cmd.Parameters.AddWithValue("@Salary", Convert.ToInt32(salary.Text));
                    cmd.ExecuteNonQuery();
                    CrudRowGrid.EditIndex = -1;

                    BindData();
                }
                catch(Exception ex)
                {
                    ShowAlertMessage("All fields required");
                }

            }
            else if (com == "Canceledit")
            {
                CrudRowGrid.EditIndex = -1;
                BindData();
            }
            else if (com == "DeleteRow")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Label emp_id = CrudRowGrid.Rows[index].FindControl("Label1") as Label;

                CreateConnection();

                cmd.Parameters.AddWithValue("@Event", "Delete");
                cmd.Parameters.AddWithValue("@EmpId", Convert.ToInt32(emp_id.Text));
                cmd.ExecuteNonQuery();

                CrudRowGrid.EditIndex = -1;

                BindData();
            }
        }




        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'CrudRowGrid' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        protected void download_click(object sender, EventArgs e)
        {
            generateExportXls();
        }
        public void generateExportXls() 
        {
            string attachment = "attachment;filename=data.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            HtmlForm frm = new HtmlForm();
            CrudRowGrid.Parent.Controls.Add(frm);
            frm.Attributes["runat"] = "server";
            frm.Controls.Add(CrudRowGrid);
            
            frm.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        protected void CrudRowGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Checking the RowType of the Row  
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //If Salary is less than 10000 than set the row Background Color to Cyan  

                /*if (Convert.ToInt32(e.Row.Cells[5].Text) < 10000)
                {
                    e.Row.BackColor = Color.Cyan;
                }*/
                Response.Write("<script type=\"text/javascript\">alert(e.Row.Cells[5]);</script>");

            }
        }

    }
    
}