using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code.Login;
using App_Code.User_Mapping;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using App_Code.Utility;


public partial class CreateMapUser : System.Web.UI.Page
{
    protected void CheckLogin()
    {
        if (Session["AdminUserName"] == null || Session["AdminUserName"] == "")
        {
            Response.Redirect("adminLogin.aspx");
        }
        else
        {
           
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckLogin();

        generateSideBarItems();
        if (IsPostBack == false)
        {

        }
        else
        {
            mapUser.Style.Add("display", "block");

        }

    }
    protected void store_Click(object sender, EventArgs e)
    {

        Guid id = new Guid(Hidden1.Value);
        UserMapping oldMap = UserMapping_S.MapUser(id);
        if (oldMap == null)
        {

            UserMapping map = new UserMapping();
            map.MeterId = Convert.ToInt32(metId.Text);
            map.DeviceId = devId.Text;
            map.UserId = id;

            UserMapping_S.InsertMap(map);
            Session["DeviceID"] = map.DeviceId;
            Session["MeterID"] = map.MeterId.ToString();

            green0.Text = "Added";
            Hidden1.Value = "";
        }
        else
        {
            green0.Text = "Already Entered";
        }
        devId.Text = ""; metId.Text = "";
    }


    protected void generateSideBarItems()
    {

        List<UserLogin> AllUsers = UserLogin_S.ListOfAllUsers();
        if (AllUsers != null)
        {
            Table sideTable = new Table();
            sideTable.ID = "sideTable";

            for (int i = 0; i < AllUsers.Count; i++)
            {

                TableRow wrapper = new TableRow();
                wrapper.ID = "wrapper" + i;

                TableCell cell = new TableCell();
                cell.ID = "cell" + i;
                cell.Style.Add("width", "250px");
                cell.Style.Add("height", "40px");
                cell.Style.Add("border-bottom-style", "groove");

                HtmlGenericControl nameLabel = new HtmlGenericControl("label");
                nameLabel.ID = "nameLabel" + i;
                nameLabel.InnerText = AllUsers[i].FullName;
                nameLabel.Style.Add("font-size", "large");
                nameLabel.Attributes.Add("class", "clicker");
                nameLabel.Style.Add("cursor", "pointer");
                nameLabel.Attributes.Add("UID", AllUsers[i].UserId.ToString());
              
                nameLabel.Attributes.Add("onclick", "JavaScript:CopyHidden(this)");

                cell.Controls.Add(nameLabel);
                wrapper.Cells.Add(cell);
                sideTable.Rows.Add(wrapper);

            }
            sideBar.Controls.Add(sideTable);
        }
    }

}