using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridviewCrudOperations
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataSource = SqlDataSource1;
                GridView1.DataBind();
            }
            
        }

        

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GridView1.DataSource = SqlDataSource1;
            GridView1.DataBind();
            Label7.Text = "";
            GridView1.EditRowStyle.BackColor = System.Drawing.Color.LightGray;
        }

        

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.DataSource = SqlDataSource1;
            GridView1.DataBind();
            Label7.Text = "";
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label emp_id = GridView1.Rows[e.RowIndex].FindControl("Label2") as Label;
            TextBox emp_name = GridView1.Rows[e.RowIndex].FindControl("TextBox1") as TextBox;
            TextBox emp_email = GridView1.Rows[e.RowIndex].FindControl("TextBox3") as TextBox;
            TextBox emp_phone = GridView1.Rows[e.RowIndex].FindControl("TextBox5") as TextBox;
            TextBox emp_address = GridView1.Rows[e.RowIndex].FindControl("TextBox7") as TextBox;
            String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=MyDatabase;Integrated Security=True";
            String updatedata = "UPDATE employee set emp_name='" + emp_name.Text + "',emp_email='" + emp_email.Text + "',emp_phone='" + emp_phone.Text + "',emp_address='" + emp_address.Text + "' where emp_id=" + Convert.ToInt32(emp_id.Text);
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = updatedata;
            cmd.ExecuteNonQuery();
            Label7.Text = "Row data updated successfully";
            GridView1.EditIndex = -1;
            GridView1.DataSource = SqlDataSource1;
            GridView1.DataBind();
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            TextBox emp_id = GridView1.FooterRow.FindControl("txtaddid") as TextBox;
            TextBox emp_name = GridView1.FooterRow.FindControl("TextBox2") as TextBox;
            TextBox emp_email = GridView1.FooterRow.FindControl("TextBox4") as TextBox;
            TextBox emp_phone = GridView1.FooterRow.FindControl("TextBox6") as TextBox;
            TextBox emp_address = GridView1.FooterRow.FindControl("TextBox8") as TextBox;
            String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=MyDatabase;Integrated Security=True";
            String insertdata = "INSERT INTO employee(emp_name,emp_email,emp_phone,emp_address) VALUES ('"+emp_name.Text+"','"+emp_email.Text+"','"+emp_phone.Text+"','"+emp_address.Text+"')";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = insertdata;
            cmd.ExecuteNonQuery();
            Label7.Text = "Row data Inserted successfully";
            SqlDataSource1.DataBind();  
            GridView1.DataSource = SqlDataSource1;
            GridView1.DataBind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label emp_id = GridView1.Rows[e.RowIndex].FindControl("Label1") as Label;
            String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=MyDatabase;Integrated Security=True";
            String deletedata = "DELETE FROM employee WHERE emp_id="+emp_id.Text;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = deletedata;
            cmd.ExecuteNonQuery();
            Label7.Text = "Row data deleted successfully";
            GridView1.EditIndex = -1;
            GridView1.DataSource = SqlDataSource1;
            GridView1.DataBind();

        }

        
    }
}