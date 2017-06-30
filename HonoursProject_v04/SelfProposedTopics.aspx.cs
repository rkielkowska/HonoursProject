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
    public partial class SelfProposedTopics : System.Web.UI.Page
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        protected void Page_Load(object sender, EventArgs e)
        {
            _client = new MongoClient("mongodb://rkielk200:GcuRkielk200@cluster0-shard-00-00-bt0cr.mongodb.net:27017,cluster0-shard-00-01-bt0cr.mongodb.net:27017,cluster0-shard-00-02-bt0cr.mongodb.net:27017/admin?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin");
            _database = _client.GetDatabase("HonoursProject");

            if (Context.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack) { 
                getData();
                divViewTopic.Visible = false;
            }
           

        }

        public void getData()
        {

            var collection = _database.GetCollection<BsonDocument>("selfproposedtopics");

            DataTable dt = createDataTable();

            var documents = collection.Find(new BsonDocument()).ToList();

            foreach (var document in documents)
            {
                try
                { addRowToDataTable(dt, document); }
                catch { }              
            }

            gvwSPTopics.DataSource = dt;
            gvwSPTopics.DataBind();          
        }

        public DataTable createDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SubmittedBy", typeof(string));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Supervisor", typeof(string));
            dt.Columns.Add("SubjectAreas", typeof(string));
            dt.Columns.Add("SuitableFor", typeof(string));
            dt.Columns.Add("GoalsOfProject", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("ResourcesRequired", typeof(string));
            dt.Columns.Add("BackgroundNeeded", typeof(string));
            dt.Columns.Add("RecommendedReading", typeof(string));
            dt.Columns.Add("_id", typeof(string));
            return dt;
        }

        public DataTable addRowToDataTable(DataTable dt, BsonDocument document)
        {
            dt.Rows.Add(document["submittedby"], document["title"], document["supervisor"], document["subjectareas"], document["suitablefor"],
                        document["goalsofproject"].ToString().Replace("\r\n", "<br/>"), document["description"].ToString().Replace("\r\n", "<br/>"), document["type"], document["resourcesrequired"],
                        document["backgroundneeded"], document["recommendedreading"].ToString().Replace("\r\n", "<br/>"), document["_id"]);

            return dt;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwSPTopics.PageIndex = e.NewPageIndex;
            getData();
        }

        protected void gvwSPTopics_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            var collection = _database.GetCollection<BsonDocument>("selfproposedtopics");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(gvwSPTopics.DataKeys[e.RowIndex].Value.ToString()));
            collection.DeleteOne(filter);
            getData();
        }


        protected void lnkView_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            var collection = _database.GetCollection<BsonDocument>("selfproposedtopics");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(e.CommandArgument.ToString()));
            var document = collection.Find(filter).First();

            lblTitle.Text = document["title"].ToString();
            lblSupervisor.Text = document["supervisor"].ToString();
            lblSubjectAreas.Text = document["subjectareas"].ToString();
            lblSuitableFor.Text = document["suitablefor"].ToString();
            lblGoalsOfProject.Text = document["goalsofproject"].ToString().Replace("\r\n", "<br/>");
            lblDescription.Text = document["description"].ToString().Replace("\r\n", "<br/>");
            lblType.Text = document["type"].ToString();
            lblResourcesRequired.Text = document["resourcesrequired"].ToString();
            lblBackgroundNeeded.Text = document["backgroundneeded"].ToString();
            lblRecommendedReading.Text = document["recommendedreading"].ToString().Replace("\r\n", "<br/>");

            divViewTopic.Visible = true;


        }

        protected void btnCancelView_Click(object sender, EventArgs e)
        {
            divViewTopic.Visible = false;
        }
    }
    
}