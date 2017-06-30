using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HonoursProject_v04
{
    public partial class Users : System.Web.UI.Page
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        protected void Page_Load(object sender, EventArgs e)
        {
            _client = new MongoClient("mongodb://rkielk200:GcuRkielk200@cluster0-shard-00-00-bt0cr.mongodb.net:27017,cluster0-shard-00-01-bt0cr.mongodb.net:27017,cluster0-shard-00-02-bt0cr.mongodb.net:27017/admin?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin");
            _database = _client.GetDatabase("HonoursProject");

            if (Context.User.Identity.IsAuthenticated == false)
            {
                var collection = _database.GetCollection<BsonDocument>("users");
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                getData();
            }
        }

        public void getData()
        {
            var collection = _database.GetCollection<BsonDocument>("users");

            DataTable dt = createDataTable();

            var documents = collection.Find(new BsonDocument()).ToList();

            foreach (var document in documents)
            {
                try
                { dt = addRowToTable(dt, document); }
                catch { }
            }

            gvwUsers.DataSource = dt;
            gvwUsers.DataBind();
        }

        public DataTable createDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("UserType", typeof(string));
            dt.Columns.Add("_id", typeof(string));
            return dt;
        }

        public DataTable addRowToTable(DataTable dt, BsonDocument document)
        {
            try
            {
                dt.Rows.Add(document["username"], document["full_name"], document["email"], document["type"], document["_id"]);
            }
            catch { }

            return dt;
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Value;
            string pwd = txtPassword.Value;
            string pwd_repeat = txtPasswordRepeat.Value;
            string email = txtEmail.Value;
            string full_name = txtFullName.Value;
            string type = txtUserType.SelectedValue;
            string course = txtCourse.Value;
            string studentid = txtStudentID.Value;

            if (username.Length < 1) { lblError.InnerText = "Enter Username"; }
            else if (pwd.Length < 1) { lblError.InnerText = "Enter Password"; }
            else if (pwd_repeat.Length < 1) { lblError.InnerText = "Repeat Password"; }
            else if (email.Length < 1) { lblError.InnerText = "Enter Email"; }
            else if (email.Length < 1) { lblError.InnerText = "Enter Full Name"; }
            else if (type.Length < 1) { lblError.InnerText = "Select User Type"; }
            else
            {
                insert(username, pwd, email, full_name, type, course, studentid);

                lblError.InnerText = "Success";
                txtUsername.Value = "";
                txtPassword.Value = "";
                txtPasswordRepeat.Value = "";
                txtEmail.Value = "";
                txtFullName.Value = "";
                txtUserType.SelectedValue = "";
                txtCourse.Value = "";
                txtStudentID.Value = "";
            }

            getData();
            lblError.InnerText = "";
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwUsers.PageIndex = e.NewPageIndex;
            getData();
        }

        protected void gvwUsers_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            var collection = _database.GetCollection<BsonDocument>("users");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(gvwUsers.DataKeys[e.RowIndex].Value.ToString()));
            collection.DeleteOne(filter);
            getData();
        }

        public async void insert(string username, string pwd, string email, string full_name, string type, string course, string studentid)
        {
            var document = new BsonDocument
            {
                { "username", username },
                { "password", pwd },
                { "email", email },
                { "full_name", full_name },
                { "type", type },
                { "course", course },
                { "student_id", studentid },
                { "interested_topics", new BsonArray() },
                { "allocated_topic", "" },
                { "is_self_proposed", "" },
                { "project_proposal", false },
                { "ethics_form", false },
                { "interim_report", false },
                { "final_report", false },
                { "poster_presentation", false }
            };

            var collection = _database.GetCollection<BsonDocument>("users");
            await collection.InsertOneAsync(document);
        }
    }
}