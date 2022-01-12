using CurdMvcWEF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurdMvcWEF.Controllers
{
    public class ProductController : Controller
    {
        string connectionString = @"Data Source= SQLDBS; Initial Catalog= test_DB; uid=sa; Password=miscot; Integrated Security=false; ";
	
        // GET: Product
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblProduct = new DataTable();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "select * from Product  order by PorductID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, con);
                sqlDa.Fill(dtblProduct);
            }
                return View(dtblProduct);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "insert into Product values(@ProductName, @Price, @Count)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                cmd.Parameters.AddWithValue("@Price",productModel.Price);
                cmd.Parameters.AddWithValue("@Count", productModel.Count);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("index");

        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel productModel = new ProductModel();
            DataTable dtblProduct = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "select * from Product where PorductID=@ProductID";
                SqlDataAdapter sqlda = new SqlDataAdapter(query, con);
                sqlda.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                sqlda.Fill(dtblProduct);
            }
            if (dtblProduct.Rows.Count==1)
            {
                productModel.ProductID= Convert.ToInt32(dtblProduct.Rows[0][0].ToString());
                productModel.ProductName = dtblProduct.Rows[0][1].ToString();
                productModel.Price = Convert.ToDecimal(dtblProduct.Rows[0][2].ToString());
                productModel.Count = Convert.ToInt32(dtblProduct.Rows[0][3].ToString());
                return View(productModel);
            }
            else
            
                return RedirectToAction("Index");
            
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productModel)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "update Product set ProductName=@ProductName, Price=@Price, Count=@Count where PorductID=@ProductID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductID",productModel.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                cmd.Parameters.AddWithValue("@Price", productModel.Price);
                cmd.Parameters.AddWithValue("@Count", productModel.Count);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "delete from Product where PorductID=@ProductID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ProductID",id);
        
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("index");
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
