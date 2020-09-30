using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using MySqlTestApplication.Models;
using Newtonsoft.Json;

namespace MySqlTestApplication.Controllers
{
    public class HomeController : Controller
    {
        private MySqlConnection mySqlConnection;
        private string connectionString;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            try
            {
                SplineChart();
                //var val = MySqlConnectionTest();
                //ViewBag.Message = val;
            }
            catch (Exception e)
            {
                ViewBag.Message = "nope";
                Console.WriteLine(e);
                throw;
            }

            return View();
        }

        private void SplineChart()
        {
            DataTable dt = MySqlConnectionTest();
            List<DataPoint> dataPoints = new List<DataPoint>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataPoints.Add(new DataPoint( dt.Rows[i]["MeasuredDateTime"].ToString(), (decimal)dt.Rows[i]["Temperature"]));
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
        }

        private DataTable MySqlConnectionTest()
        {
            string connection = "SERVER= 127.0.0.1;PORT=3306;DATABASE=air_live;UID=root;PASSWORD=juventus";
            DataTable dataTable = new DataTable();

            try
            {
                mySqlConnection = new MySqlConnection();
                mySqlConnection.ConnectionString = connection;
                mySqlConnection.Open();

                string query = "SELECT * FROM temperature";

                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataTable.Load(dataReader, LoadOption.OverwriteChanges);
                mySqlConnection.Close();
            }
            catch (MySqlException sqlException)
            {
                Console.WriteLine(sqlException);
                throw;
            }

            return dataTable;
        }
    }
}