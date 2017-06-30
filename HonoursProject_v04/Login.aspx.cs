using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using FluentAssertions;
using System.Web.Security;

namespace HonoursProject_v04
{
    public partial class Login : System.Web.UI.Page
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        protected void Page_Load(object sender, EventArgs e)
        {
            _client = new MongoClient("mongodb://rkielk200:GcuRkielk200@cluster0-shard-00-00-bt0cr.mongodb.net:27017,cluster0-shard-00-01-bt0cr.mongodb.net:27017,cluster0-shard-00-02-bt0cr.mongodb.net:27017/admin?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin");
            _database = _client.GetDatabase("HonoursProject");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Value;
            string pwd = txtPwd.Value;

            var filter = Builders<BsonDocument>.Filter.Eq("username", username);
            filter = filter & (Builders<BsonDocument>.Filter.Eq("password", pwd));

            var collection = _database.GetCollection<BsonDocument>("users");
            var document = collection.Find(filter).FirstOrDefault();

            if (document != null)
            {
                Session["usertype"] = document["type"].ToString();
                lblError.InnerText = "";
                FormsAuthentication.SetAuthCookie(username, false);
                Response.Redirect("Default.aspx"); 
            }
            else
            {
                lblError.InnerText = "Invalid username or password.";
            }
        }
    }
}