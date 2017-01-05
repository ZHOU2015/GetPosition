using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class searchArea : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        getString();
        
    }
    public void getString()
    {
        string aa = TextBox1.Text;
        string bb = TextBox2.Text;
    }
}