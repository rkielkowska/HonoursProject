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
    public partial class MyTopics : System.Web.UI.Page
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
            updatePriorities();

            var collectionAllTopics = _database.GetCollection<BsonDocument>("topics");
            var collectionUsers = _database.GetCollection<BsonDocument>("users");

            var filter = Builders<BsonDocument>.Filter.Eq("username", Context.User.Identity.Name);
            var user = collectionUsers.Find(filter).First();

            var interestedTopics = user["interested_topics"].AsBsonArray.OrderBy(x => x.AsBsonDocument["priority"]);

            DataTable dt = createDataTable();

            foreach (BsonDocument userTopic in interestedTopics)
            {
                try
                {
                    var filterTopic = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(userTopic["topicID"].ToString()));
                    var topic = collectionAllTopics.Find(filterTopic).FirstOrDefault();

                    addRowToDataTable(dt, topic);
                }
                catch { }
                
            }

            gvwMyTopics.DataSource = dt;
            gvwMyTopics.DataBind();          
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
            gvwMyTopics.PageIndex = e.NewPageIndex;
            getData();
        }

        protected void gvwMyTopics_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            var collectionAllTopics = _database.GetCollection<BsonDocument>("topics");
            var collectionUsers = _database.GetCollection<BsonDocument>("users");

            var filter = Builders<BsonDocument>.Filter.Eq("username", Context.User.Identity.Name);
            var user = collectionUsers.Find(filter).First();

            var interestedTopics = user["interested_topics"].AsBsonArray;

            string topicID = gvwMyTopics.DataKeys[e.RowIndex].Value.ToString();

            foreach (var doc in interestedTopics.ToList())
            {
                if (doc["topicID"].ToString() == topicID)
                {
                    interestedTopics.Remove(doc);
                }
            }

            var filterUser = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(user["_id"].ToString()));
            var update = Builders<BsonDocument>.Update.Set("interested_topics", interestedTopics);

            collectionUsers.UpdateOne(filter, update);

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

        protected void Prioritise_Command(object sender, CommandEventArgs e)
        {
            string topicID = e.CommandArgument.ToString();

            var collectionUsers = _database.GetCollection<BsonDocument>("users");
            var filter = Builders<BsonDocument>.Filter.Eq("username", Context.User.Identity.Name);
            var user = collectionUsers.Find(filter).First();

            var interestedTopics = user["interested_topics"].AsBsonArray;
            var topic = interestedTopics.Where(x => x.AsBsonDocument["topicID"].ToString() == topicID).FirstOrDefault();

            if (e.CommandName=="Up")
            {
                if (topic["priority"] != "0")
                {
                    foreach (BsonDocument doc in interestedTopics.Where(x => (Convert.ToInt32(x.AsBsonDocument["priority"]) == (Convert.ToInt32(topic["priority"]) - 1))).ToList())
                    {
                        doc["priority"] = Convert.ToInt32(doc["priority"]) + 1;
                    }

                    topic["priority"] = Convert.ToInt32(topic["priority"]) - 1;
                }
            }
            else if (e.CommandName == "Down")
            {
                foreach (BsonDocument doc in interestedTopics.Where(x => (Convert.ToInt32(x.AsBsonDocument["priority"]) == (Convert.ToInt32(topic["priority"]) + 1))).ToList())
                {
                    doc["priority"] = Convert.ToInt32(doc["priority"]) - 1;
                }

                topic["priority"] = Convert.ToInt32(topic["priority"]) + 1;
            }


            var filterUser = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(user["_id"].ToString()));
            var update = Builders<BsonDocument>.Update.Set("interested_topics", interestedTopics);

            collectionUsers.UpdateOne(filter, update);

            getData();
        }

        protected void gvwMyTopics_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                {
                    ((ImageButton)e.Row.Cells[5].FindControl("btnUp")).Visible = false;
                }
                else if (e.Row.RowIndex == gvwMyTopics.Rows.Count)
                {
                    ((ImageButton)gvwMyTopics.Rows[gvwMyTopics.Rows.Count - 1].Cells[5].FindControl("btnDown")).Visible = true;
                    ((ImageButton)e.Row.Cells[5].FindControl("btnDown")).Visible = false;
                }
            }
        }

        public void updatePriorities()
        {
            var collectionAllTopics = _database.GetCollection<BsonDocument>("topics");
            var collectionUsers = _database.GetCollection<BsonDocument>("users");

            var filter = Builders<BsonDocument>.Filter.Eq("username", Context.User.Identity.Name);
            var user = collectionUsers.Find(filter).First();

            var interestedTopics = user["interested_topics"].AsBsonArray.OrderBy(x => Convert.ToInt32(x.AsBsonDocument["priority"]));
            BsonArray interestedTopicsNew = new BsonArray();

            int i = 0;

            foreach (var doc in interestedTopics)
            {
                doc["priority"] = i.ToString();
                interestedTopicsNew.Add(doc);
                i++;
            }

            var filterUser = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(user["_id"].ToString()));
            var update = Builders<BsonDocument>.Update.Set("interested_topics", interestedTopicsNew);

            collectionUsers.UpdateOne(filter, update);
        }

    }
    
}