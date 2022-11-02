using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ViewState_Sample
{
    public partial class Viewstate02 : System.Web.UI.Page
    {
        string sqlconn = ConfigurationManager.AppSettings["dbINKSYS"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.GetMaxId();
                this.GetPersons();
            }
        }

        private void GetPersons()
        {
           
            using (SqlConnection con = new SqlConnection(sqlconn))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Person", con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            this.GridView1.DataSource = dt;
                            this.GridView1.DataBind();
                            ViewState["CurrentTable"] = dt;
                        }


                    }
                }
            }
        }

        private void GetMaxId()
        {
           // string constr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(sqlconn))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT MAX(Id) AS ID FROM Person", con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            string str = string.Empty;
                            if (str != dt.Rows[0]["ID"].ToString())
                            {
                                int id = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                                ViewState["Id"] = id;
                            }
                        }
                    }
                }
            }
        }

        protected void Add(object sender, EventArgs e)
        {
            if (ViewState["CurrentTable"] == null)
            {
                int id = Convert.ToInt32(ViewState["Id"]) + 1;
                ViewState["Id"] = id;
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("City", typeof(string));
                dt.Rows.Add(id, this.txtName.Text, this.txtCity.Text);
                ViewState["CurrentTable"] = dt;
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
            }
            else
            {
                int id = Convert.ToInt32(ViewState["Id"]) + 1;
                ViewState["Id"] = id;
                DataTable dt2 = (DataTable)ViewState["CurrentTable"];
                dt2.Rows.Add(this.txtName.Text, id, this.txtCity.Text);
                this.GridView1.DataSource = dt2;
                this.GridView1.DataBind();
                ViewState["CurrentTable"] = dt2;
            }
        }

        protected void Save(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            string name, city;
            int id;
            foreach (DataRow row in dt.Rows)
            {
                name = row["Name"].ToString();
                city = row["City"].ToString();
                id = int.Parse(row["Id"].ToString());
                this.InsertRows(name, city, id);
            }
        }

        private void InsertRows(string name, string city, int id)
        {
           // string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(sqlconn))
            {
                using (SqlCommand cmd = new SqlCommand("Insert_Person", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@City", city);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
