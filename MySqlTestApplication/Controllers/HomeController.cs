using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
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

        public ActionResult Carbondioxide()
        {
            try
            {
                string query = "SELECT * FROM carbondioxide";
                SplineChart(query, "Carbondioxide");
            }
            catch (Exception e)
            {
                ViewBag.Message = "nope";
                Console.WriteLine(e);
                throw;
            }

            return View();
        }

        public ActionResult Humidity()
        {
            try
            {
                string query = "SELECT * FROM humidity";
                SplineChart(query, "Humidity");
            }
            catch (Exception e)
            {
                ViewBag.Message = "nope";
                Console.WriteLine(e);
                throw;
            }

            return View();
        }

        public ActionResult Temperature(string button)
        {
            try
            {
                string query = string.Empty;
                if (button == "buttonSearch")
                {
                    query = "SELECT * FROM temperature WHERE MeasuredDateTime between '2020-09-29 10:44:00' and '2020-09-29 11:00:50' ";
                }
                else
                {
                    query = "SELECT * FROM temperature";

                }
                SplineChart(query, "Temperature");
            }
            catch (Exception e)
            {
                ViewBag.Message = "nope";
                Console.WriteLine(e);
                throw;
            }

            return View();
        }

        private void SplineChart(string query, string attributeName)
        {
            DataTable dt = MySqlConnectionTest(query);
            List<DataPoint> dataPoints = new List<DataPoint>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataPoints.Add(new DataPoint( dt.Rows[i]["MeasuredDateTime"].ToString(), (decimal)dt.Rows[i][attributeName]));
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
        }

        private DataTable MySqlConnectionTest(string query)
        {
            string connection = "SERVER= 127.0.0.1;PORT=3306;DATABASE=air_live;UID=root;PASSWORD=juventus";
            DataTable dataTable = new DataTable();

            try
            {
                mySqlConnection = new MySqlConnection();
                mySqlConnection.ConnectionString = connection;
                mySqlConnection.Open();

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