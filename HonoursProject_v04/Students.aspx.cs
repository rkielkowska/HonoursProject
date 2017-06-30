using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Threading.Tasks;
using FluentAssertions;
using System.Data;
using System.Web.Script.Serialization;

namespace HonoursProject_v04
{
    public partial class Students : System.Web.UI.Page
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

            if (!IsPostBack) { 
                getData();

                var collection = _database.GetCollection<BsonDocument>("topics");

                DataTable dt = new DataTable();
                dt.Columns.Add("Title", typeof(string));
                dt.Columns.Add("_id", typeof(string));

                var documents = collection.Find(new BsonDocument()).ToList();

                foreach (var document in documents)
                {
                    try
                    { dt.Rows.Add(document["title"], document["_id"]); }
                    catch { }
                }

                txtTopics.DataSource = dt;
                txtTopics.DataTextField = "Title";
                txtTopics.DataValueField = "Title";
                txtTopics.DataBind();  
            }
           

        }

        public void getData()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("type", "Student");
            var collection = _database.GetCollection<BsonDocument>("users");

            DataTable dt = createDataTable();

            var documents = collection.Find(new BsonDocument()).ToList();

            var cursor = collection.Find(filter).ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                dt = addRowToTable(dt, document); 
            }

            gvwStudents.DataSource = dt;
            gvwStudents.DataBind();           
        }

        public DataTable createDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentID", typeof(string));
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("Course", typeof(string));
            dt.Columns.Add("AllocatedTopic", typeof(string));
            dt.Columns.Add("IsSelfProposed", typeof(string));
            dt.Columns.Add("ProjectProposal", typeof(bool));
            dt.Columns.Add("EthicsForm", typeof(bool));
            dt.Columns.Add("InterimReport", typeof(bool));
            dt.Columns.Add("FinalReport", typeof(bool));
            dt.Columns.Add("PosterPresentation", typeof(bool));
            dt.Columns.Add("_id", typeof(string));
            return dt;
        }

        public DataTable addRowToTable(DataTable dt, BsonDocument document)
        {
            try
            {
                dt.Rows.Add(document["student_id"], document["full_name"], document["course"], document["allocated_topic"], document["is_self_proposed"],
                    document["project_proposal"], document["ethics_form"], document["interim_report"], document["final_report"],
                    document["poster_presentation"], document["_id"]);
            }
            catch { }

            return dt;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwStudents.PageIndex = e.NewPageIndex;
            getData();
        }


        protected void btnFilterNoProject_Click(object sender, EventArgs e)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("type", "Student") & Builders<BsonDocument>.Filter.Eq("allocated_topic", "");
            var collection = _database.GetCollection<BsonDocument>("users");

            DataTable dt = createDataTable();

            var documents = collection.Find(new BsonDocument()).ToList();

            var cursor = collection.Find(filter).ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                dt = addRowToTable(dt, document);
            }

            gvwStudents.DataSource = dt;
            gvwStudents.DataBind();
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            getData();
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelect = sender as CheckBox;
            GridViewRow row = chkSelect.NamingContainer as GridViewRow;

            if (chkSelect.Checked) {  gvwStudents.SelectedIndex = row.RowIndex;  }
            else { gvwStudents.SelectedIndex = -1; }
            
            if (chkSelect != null)
            {
                var isChecked = chkSelect.Checked;
                var tempCheckBox = new CheckBox();
                foreach (GridViewRow gvRow in gvwStudents.Rows)
                {
                    tempCheckBox = gvRow.FindControl("chkSelect") as CheckBox;
                    if (tempCheckBox != null)
                    {
                        tempCheckBox.Checked = false;
                    }
                }
                if (isChecked)
                {
                    chkSelect.Checked = true;
                }
            }
        }

        protected void btnAllocate_Click(object sender, EventArgs e)
        {
            if (gvwStudents.SelectedRow != null)
            {
                string studentid = gvwStudents.SelectedDataKey.Value.ToString();

                if (txtTopics.SelectedValue != null)
                {
                    string topic = txtTopics.Text;

                    lblError.Text = "";

                    var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(studentid));

                    var collection = _database.GetCollection<BsonDocument>("users");
                    var document = collection.Find(filter).FirstOrDefault();

                    if (document != null)
                    {
                        var update = Builders<BsonDocument>.Update.Set("allocated_topic", topic)
                            .Set("is_self_proposed", "No");

                        collection.UpdateOne(filter, update);

                        getData();
                    }
                }
                else
                {
                    lblError.Text = "Topic not selected";
                }
            }
            else
            {
                lblError.Text = "Student not selected";
            }
        }


        protected void chkProjProposal_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelect = sender as CheckBox;
            updateCheckbox(chkSelect, "project_proposal");            
        }

        protected void chkEthicsForm_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelect = sender as CheckBox;
            updateCheckbox(chkSelect, "ethics_form");  
        }

        protected void chkInterimReport_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelect = sender as CheckBox;
            updateCheckbox(chkSelect, "interim_report");  
        }

        protected void chkFinalReport_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelect = sender as CheckBox;
            updateCheckbox(chkSelect, "final_report");  
        }

        protected void chkPosterPresentation_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelect = sender as CheckBox;
            updateCheckbox(chkSelect, "poster_presentation");  
        }

        public void updateCheckbox(CheckBox chkSelect, string fieldname)
        {
            try
            {
                GridViewRow row = chkSelect.NamingContainer as GridViewRow;
                string studentid = gvwStudents.DataKeys[row.RowIndex].Value.ToString();

                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(studentid));

                var collection = _database.GetCollection<BsonDocument>("users");
                var document = collection.Find(filter).FirstOrDefault();

                if (document != null)
                {
                    var update = Builders<BsonDocument>.Update.Set(fieldname, chkSelect.Checked);

                    collection.UpdateOne(filter, update);

                    getData();
                }
            }
            catch { }
        }


    }
    
}