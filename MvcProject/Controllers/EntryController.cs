using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MvcProject.Models;

namespace MvcProject.Controllers
{
    public class EntryController : Controller
    {
        public IConfiguration Configuration { get; }
        public EntryController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(TLib userDetail)
        {


            string connectionString = Configuration["ConnectionStrings:LibManageConnection"];
            using (var con = new SqlConnection(connectionString))
            {

                //     string sql = $"Insert Into NewTable (acc_no, pwd) Values ('{inventory.AccNo}', '{inventory.Pwd}')";

                using (var cmd = new SqlCommand("select dbo.userlogincheck(@username,@pwd)", con))
                {
                    cmd.Parameters.AddWithValue("@username", userDetail.Username);
                    cmd.Parameters.AddWithValue("@pwd", userDetail.Pwd);


                    con.Open();
                    int c = Convert.ToInt32(cmd.ExecuteScalar());

                    if (c == 1)
                    {

                        Console.WriteLine("welcome User");
                        return RedirectToAction("Index", "LibManage");


                    }
                    else
                    {
                        Console.WriteLine("Account No or Password is wrong!!!");


                    }
                }

            }
            return View(userDetail);

        }
    }
    
}
