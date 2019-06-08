using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Boloc_TestThem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dtEmployee = new DataTable();
        dtEmployee = GetEmployee();
        if (ViewState["EmployeeTable"] != null)
        {
            dtEmployee = (DataTable)ViewState["EmployeeTable"];
            rptEmployee.DataSource = dtEmployee;
            rptEmployee.DataBind();
            rptEmployee.Visible = true;
        }
        else
        {
            rptEmployee.DataSource = null;
            rptEmployee.DataBind();
            rptEmployee.Visible = false;
        }
        txtEmpName.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtCity.Text = string.Empty;
        txtAddress.Text = string.Empty;
    }
    private DataTable GetEmployee()
    {
        DataTable dtEmployee = null;
        if (ViewState["EmpID"] != null)
        {
            int EmpID = Convert.ToInt32((ViewState["EmpID"]));
            EmpID++;
            ViewState["EmpID"] = EmpID;
        }
        else
        {
            ViewState["EmpID"] = 1;
        }

        if (ViewState["EmployeeTable"] == null)
        {
            dtEmployee = new DataTable("EmployeeTable");
            dtEmployee.Columns.Add(new DataColumn("EmpID", typeof(int)));
            dtEmployee.Columns.Add(new DataColumn("EmpName", typeof(string)));
            dtEmployee.Columns.Add(new DataColumn("Address", typeof(string)));
            dtEmployee.Columns.Add(new DataColumn("City", typeof(string)));
            dtEmployee.Columns.Add(new DataColumn("Email", typeof(string)));

            ViewState["EmployeeTable"] = dtEmployee;
        }
        else
        {
            dtEmployee = (DataTable)ViewState["EmployeeTable"];
        }
        DataRow dtRow = dtEmployee.NewRow();

        dtRow["EmpID"] = Convert.ToInt32(ViewState["EmpID"]);
        dtRow["EmpName"] = txtEmpName.Text.Trim();
        dtRow["Address"] = txtAddress.Text.Trim();
        dtRow["City"] = txtCity.Text.Trim();
        dtRow["Email"] = txtEmail.Text.Trim();
        dtEmployee.Rows.Add(dtRow);
        ViewState["EmployeeTable"] = dtEmployee;
        return dtEmployee;
    }
}
