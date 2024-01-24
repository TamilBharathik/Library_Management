using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyModel;
using MvcProject.Models;
using System.IO.Compression;

namespace MvcProject.Controllers
{
    public class LibManageController : Controller

    {

        public IConfiguration Configuration { get; }
        public LibManageController(IConfiguration config)
        {
            Configuration = config;
        }
        // GET: LibManageController
        public ActionResult Index()
        {
            List<LibManage> pm = new List<LibManage>();
            string connectionString = Configuration["ConnectionStrings:LibManageConnection"];
            //string connectionString = "Data Source=ICPU0076\\SQLEXPRESS;Initial Catalog=Tlibrary;Persist Security Info=False;User ID=sa;Password=sql@123;Pooling=False;Multiple Active Result Sets=False;Encrypt=False;Trust Server Certificate=False;Command Timeout=0";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "select * from books";
                SqlCommand cmd = new SqlCommand(sql, con);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        LibManage libs = new LibManage();
                        libs.BookId = Convert.ToInt32(sdr["BookId"]);
                        libs.Title = Convert.ToString(sdr["Title"]);
                        libs.Author = Convert.ToString(sdr["Author"]);
                        libs.CopiesAvailable = Convert.ToInt32(sdr["CopiesAvailable"]);
                        libs.TotalCopies = Convert.ToInt32(sdr["TotalCopies"]);
                        pm.Add(libs);
                    }
                    con.Close();
                }

            }
            return View(pm);
        }

        // GET: LibManageController/Details/5
        public ActionResult Details(int id)
        {
            LibManage libs = new LibManage();
            string connectionString = Configuration["ConnectionStrings:LibManageConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"select * from books where BookID='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        libs.BookId = Convert.ToInt32(sdr["BookId"]);
                        libs.Title = Convert.ToString(sdr["Title"]);
                        libs.Author = Convert.ToString(sdr["Author"]);
                        libs.CopiesAvailable = Convert.ToInt32(sdr["CopiesAvailable"]);
                        libs.TotalCopies = Convert.ToInt32(sdr["TotalCopies"]);

                    }
                    connection.Close();
                }

            }
            return View(libs);
            
        }

        // GET: LibManageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LibManageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LibManage manage)
        {
            try
            {
                string connectionString = Configuration["ConnectionStrings:LibManageConnection"];
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = $"insert into books(Title,Author,CopiesAvailable,TotalCopies)values('{manage.Title}','{manage.Author}','{manage.CopiesAvailable}','{manage.TotalCopies}')";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                }
                ViewBag.result = "Success";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LibManageController/Edit/5
        public ActionResult Update(int id)
        {
            LibManage libs = new LibManage();
            string connectionString = Configuration["ConnectionStrings:LibManageConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"select * from books where BookID='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        libs.BookId = Convert.ToInt32(sdr["BookId"]);
                        libs.Title = Convert.ToString(sdr["Title"]);
                        libs.Author = Convert.ToString(sdr["Author"]);
                        libs.CopiesAvailable = Convert.ToInt32(sdr["CopiesAvailable"]);
                        libs.TotalCopies = Convert.ToInt32(sdr["TotalCopies"]);

                    }
                    connection.Close();
                }

            }
            return View(libs);

        }

        // POST: LibManageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(LibManage libs, int id)
        {
            string connectionString = Configuration["ConnectionStrings:LibManageConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update Books SET Title='{libs.Title}',Author='{libs.Author}',CopiesAvailable='{libs.CopiesAvailable}',TotalCopies='{libs.TotalCopies}' where BookId='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }


        // GET: LibManageController/Delete/5
        public ActionResult Delete(int id)
        {
            LibManage libs = new LibManage();
            string connectionString = Configuration["ConnectionStrings:LibManageConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"select * from books where BookID='{id}'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        libs.BookId = Convert.ToInt32(sdr["BookId"]);
                        libs.Title = Convert.ToString(sdr["Title"]);
                        libs.Author = Convert.ToString(sdr["Author"]);
                        libs.CopiesAvailable = Convert.ToInt32(sdr["CopiesAvailable"]);
                        libs.TotalCopies = Convert.ToInt32(sdr["TotalCopies"]);

                    }
                    connection.Close();
                }

            }
            return View(libs);
           
        }

        // POST: LibManageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, LibManage BookName)
        {
            string connectionString = Configuration["ConnectionStrings:LibManageConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete from books where BookId='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
        }
    }
}
