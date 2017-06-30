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
    public partial class Default : System.Web.UI.Page
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        protected static string buttonClickType;
        protected static string usertype;
        protected static BsonDocument user;
        protected static BsonArray interestedTopics;

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                _client = new MongoClient("mongodb://rkielk200:GcuRkielk200@cluster0-shard-00-00-bt0cr.mongodb.net:27017,cluster0-shard-00-01-bt0cr.mongodb.net:27017,cluster0-shard-00-02-bt0cr.mongodb.net:27017/admin?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin");
                _database = _client.GetDatabase("HonoursProject");

                if (Context.User.Identity.IsAuthenticated == false)
                {
                    Response.Redirect("Login.aspx");
                }


                if (Session["usertype"].ToString() == null)
                {
                    Response.Redirect("Logout.aspx");
                }
                else
                {
                    usertype = Session["usertype"].ToString();
                }

                if (usertype == "Student")
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("username", Context.User.Identity.Name);
                    var collection = _database.GetCollection<BsonDocument>("users");
                    user = collection.Find(filter).First();
                    interestedTopics = user["interested_topics"].AsBsonArray;

                    btnAddNewTopic.Visible = false;
                }
                else
                {
                    gvwTopics.Columns[6].Visible = false;
                    btnSubmitSelfProposedTopic.Visible = false;
                }

                if (usertype == "Project Co-ordinator")
                { gvwTopics.Columns[7].Visible = true; }
                else { gvwTopics.Columns[7].Visible = false; }
            }
            catch { Response.Redirect("Logout.aspx"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { 
                getData();
                divViewTopic.Visible = false;
                divAddNewTopic.Visible = false;
            }

        }

        public void getData()
        {

            var collection = _database.GetCollection<BsonDocument>("topics");

            DataTable dt = createDataTable();

            var documents = collection.Find(new BsonDocument()).ToList();

            foreach (var document in documents)
            {
                try
                { dt = addRowToDataTable(dt, document); }
                catch { }              
            }

            gvwTopics.DataSource = dt;
            gvwTopics.DataBind();          
        }

        public DataTable createDataTable()
        {
            DataTable dt = new DataTable();
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
            dt.Rows.Add(document["title"], document["supervisor"], document["subjectareas"], document["suitablefor"],
                        document["goalsofproject"].ToString().Replace("\r\n", "<br/>"), document["description"].ToString().Replace("\r\n", "<br/>"), document["type"], document["resourcesrequired"],
                        document["backgroundneeded"], document["recommendedreading"].ToString().Replace("\r\n", "<br/>"), document["_id"]);

            return dt;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwTopics.PageIndex = e.NewPageIndex;
            getData();
        }


        public void insert(string title, string supervisor, string subjectareas, string suitablefor, string goalsofproject, string description,
            string type, string resourcesrequired, string backgroundneeded, string recommendedreading)
        {

            string studentID = "";

            if (buttonClickType == "selfproposed")
            {
                var collectionUsers = _database.GetCollection<BsonDocument>("users");
                var filter = Builders<BsonDocument>.Filter.Eq("username", Context.User.Identity.Name);
                var user = collectionUsers.Find(filter).FirstOrDefault();
                studentID = user["student_id"].ToString();
            }

            var document = new BsonDocument
            {
                
                { "title", title },
                { "supervisor", supervisor },
                { "subjectareas", subjectareas },
                { "suitablefor", suitablefor },
                { "goalsofproject", goalsofproject },
                { "description", description },
                { "type", type },
                { "resourcesrequired", resourcesrequired },
                { "backgroundneeded", backgroundneeded },
                { "recommendedreading", recommendedreading },
                { "submittedby", studentID }
            };

            if(buttonClickType == "addnew")
            {
                var collection = _database.GetCollection<BsonDocument>("topics");
                collection.InsertOne(document);
            }
            else if (buttonClickType == "selfproposed")
            {
                var collection = _database.GetCollection<BsonDocument>("selfproposedtopics");
                collection.InsertOne(document);
            }
            
            
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text;
            string supervisor = txtSupervisor.Text;
            string subjectareas = txtSubjectAreas.Text;
            string suitablefor = txtSuitableFor.Text;
            string goalsofproject = txtGoalsOfProject.Text;
            string description = txtDescription.Text;
            string type = txtType.Text;
            string resourcesrequired = txtResourcesRequired.Text;
            string backgroundneeded = txtBackgroundNeeded.Text;
            string recommendedreading = txtRecommendedReading.Text;

            if (title.Length > 0 && type.Length > 0 && supervisor.Length > 0)
            {
                insert(title, supervisor, subjectareas, suitablefor, goalsofproject, description, type, resourcesrequired, backgroundneeded,
                    recommendedreading);

                lblError.InnerText = "";
                lblError.Visible = false;

                txtTitle.Text = null; txtDescription.Text = null; txtType.Text = null; txtSupervisor.Text = null; txtSubjectAreas.Text = null;
                txtSuitableFor.Text = null; txtGoalsOfProject.Text = null; txtResourcesRequired.Text = null; txtBackgroundNeeded.Text = null;
                txtRecommendedReading.Text = null;

                getData();

                divAddNewTopic.Visible = false;
                btnAddNewTopic.Visible = true;
            }
            else {
                lblError.Visible = true;
                lblError.InnerText = "Title, Type and Supervisor fields are mandatory.";
                divAddNewTopic.Visible = true;
                btnAddNewTopic.Visible = false;
            }

            

            
        }

        protected void gvwTopics_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            var collection = _database.GetCollection<BsonDocument>("topics");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(gvwTopics.DataKeys[e.RowIndex].Value.ToString()));
            collection.DeleteOne(filter);
            getData();
        }


        protected void lnkView_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            var collection = _database.GetCollection<BsonDocument>("topics");
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

        protected void btnAddNewTopic_Click(object sender, EventArgs e)
        {
            divAddNewTopic.Visible = true;
            btnAddNewTopic.Visible = false;
            buttonClickType = "addnew";
            lblHeader.Text = "Add New Topic";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divAddNewTopic.Visible = false;
            btnAddNewTopic.Visible = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string title = txtTitleSearch.Text;
            string supervisor = txtSupervisorSearch.Text;
            string projecttype = txtTypeSearch.Text;

            var filter = Builders<BsonDocument>.Filter.Regex("title", new BsonRegularExpression(title, "i")) &
                    Builders<BsonDocument>.Filter.Regex("supervisor", new BsonRegularExpression(supervisor, "i")) &
                    Builders<BsonDocument>.Filter.Regex("type", new BsonRegularExpression(projecttype, "i"));

            var collection = _database.GetCollection<BsonDocument>("topics");

            DataTable dt = createDataTable();

            var cursor = collection.Find(filter).ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                try
                { addRowToDataTable(dt, document); }
                catch { }
            }

            gvwTopics.DataSource = dt;
            gvwTopics.DataBind(); 
        }

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtTitleSearch.Text = "";
            txtSupervisorSearch.Text = "";
            txtTypeSearch.Text = "";

            getData();
        }

        protected void btnSubmitSelfProposedTopic_Click(object sender, EventArgs e)
        {
            divAddNewTopic.Visible = true;
            btnSubmitSelfProposedTopic.Visible = false;
            buttonClickType = "selfproposed";
            lblHeader.Text = "Submit Self Proposed Topic";
        }

        protected void lnkAdd_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            string topicID = e.CommandArgument.ToString();

            var collectionUsers = _database.GetCollection<BsonDocument>("users");
            var filter = Builders<BsonDocument>.Filter.Eq("username", Context.User.Identity.Name);
            var user = collectionUsers.Find(filter).FirstOrDefault();
            BsonArray topics = user["interested_topics"].AsBsonArray;

            var topicDocument = new BsonDocument
                        {
                            { "topicID", topicID },
                            { "priority", 0 }
                        };

            topics.Add(topicDocument);

            var filterUser = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(user["_id"].ToString()));
            var update = Builders<BsonDocument>.Update.Set("interested_topics", topics);

            collectionUsers.UpdateOne(filter, update);
            interestedTopics = topics;
            getData();
        }

        protected void gvwTopics_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (usertype == "Student")
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string topicID = DataBinder.Eval(e.Row.DataItem, "_id").ToString();

                    bool topicAlreadyAdded = false;

                    foreach (var doc in interestedTopics)
                    {
                        if (doc["topicID"].ToString() == topicID)
                        {
                            topicAlreadyAdded = true;
                        }
                    }

                    if (topicAlreadyAdded)
                    {
                        ((LinkButton)e.Row.Cells[6].FindControl("lnkAdd")).Visible = false;
                    }
                }
            }
        }


    }
    
}