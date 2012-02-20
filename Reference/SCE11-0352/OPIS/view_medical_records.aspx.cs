using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_medical_records : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void selectedbutton_Click(object sender, EventArgs e)
    { 
        string PKname = "";  int k = 0;
        foreach (GridViewRow row in this.selectMedGridView.Rows)
        {
           
            CheckBox cbox = (CheckBox)row.FindControl("selrecord");
            if (cbox.Checked)
            {
                PKname += "ID="+this.selectMedGridView.DataKeys[row.RowIndex].Value.ToString().Trim()+"&" ;
            }
            k++;
        }
        Response.Redirect("view_selected_med_records.aspx?" + PKname);
     
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowIndex != -1)
        {
            int id = e.Row.RowIndex + 1;
            e.Row.Cells[0].Text = id.ToString();
        }

    }


}
