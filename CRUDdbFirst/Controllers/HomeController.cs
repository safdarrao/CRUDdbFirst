using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using CRUDdbFirst.Models;

namespace CRUDdbFirst.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = @"Data Source =DESKTOP-VK79UPG\SQLEXPRESSSERVER; Initial Catalog  = Product; Trusted_Connection = true";

        // GET: Home
        public ActionResult Index() { 
          DataTable dtProduct = new DataTable();
            using(SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlad = new SqlDataAdapter("select * from Product" , sqlcon);
                sqlad.Fill(dtProduct);
            }
            return View(dtProduct);
        }

      
        // GET: Home/Create
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(ProductModel productmodel)
        {
            using(SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "insert into Product values(@ProductName, @Price, @Count)";
                SqlCommand sqlcmd = new SqlCommand(query,sqlCon);
                sqlcmd.Parameters.AddWithValue("@ProductName", productmodel.ProductName);
                sqlcmd.Parameters.AddWithValue("@Price", productmodel.Price);
                sqlcmd.Parameters.AddWithValue("@Count", productmodel.count);
                sqlcmd.ExecuteNonQuery();

            }
            return RedirectToAction("Index");
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel productmodel = new ProductModel();
            DataTable edittable = new DataTable();
            using(SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "select * from Product where ProductId = @ProductID";
                SqlDataAdapter editAdpt = new SqlDataAdapter(query, sqlcon);
                editAdpt.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                editAdpt.Fill(edittable);

            }
            if(edittable.Rows.Count == 1)
            {
                productmodel.ProductID = Convert.ToInt32(edittable.Rows[0][0].ToString());
                productmodel.ProductName = edittable.Rows[0][1].ToString();
                productmodel.Price = Convert.ToDecimal(edittable.Rows[0][2].ToString());
                productmodel.count = Convert.ToInt32(edittable.Rows[0][3].ToString());
                return View(productmodel);
            }
            else 
                return RedirectToAction("Index"); 
            

            
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productmodel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "update Product set ProductName = @ProductName , Price = @Price , Count = @Count where ProductId = @ProductId";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.Parameters.AddWithValue("@ProductId", productmodel.ProductID);
                sqlcmd.Parameters.AddWithValue("@ProductName", productmodel.ProductName);
                sqlcmd.Parameters.AddWithValue("@Price", productmodel.Price);
                sqlcmd.Parameters.AddWithValue("@Count", productmodel.count);
                sqlcmd.ExecuteNonQuery();

            }
            return RedirectToAction("index");
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "delete from Product where ProductId = @ProductId";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.Parameters.AddWithValue("@ProductId",id);
                sqlcmd.ExecuteNonQuery();

            }
            return RedirectToAction("Index");
        }

    }
}
