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

namespace HonoursProject_v04
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

    }
}