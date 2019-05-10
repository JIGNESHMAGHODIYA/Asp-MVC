using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using First_Level_Test.Models;

namespace First_Level_Test.Controllers
{
    public class FirstTestController : Controller
    {
       /*this is sachin demo */
        string CNNSTR = "Data Source=MYPC-PC;Initial Catalog=dd_SDTest;Integrated Security=True";
        SqlConnection CNN;
        SqlCommand CMD;
        SqlDataAdapter DA;
        string QUR = string.Empty;
        MDFirst MDF;

        [HttpGet]
        public ActionResult Index()
        {   
            DataTable DT = new DataTable();
            using (CNN = new SqlConnection(CNNSTR))
            {
                CNN.Open();
                DA = new SqlDataAdapter("select DM.ID,DM.Name,DM.Address,CM.CityName,DM.EmailID from tbl_Detail DM,tbl_City CM where DM.City_ID=CM.City_ID ORDER BY DM.ID", CNN);
                DA.Fill(DT);
            }
            return View(DT);
        }

        [HttpGet]
        public ActionResult Create()
        {
            City();
            return View(new MDFirst());
        }

        //
        // POST: /FirstTest/Create

        [HttpPost]
        public ActionResult Create(MDFirst MDFST)
        {
            using (CNN = new SqlConnection(CNNSTR))
            {
                CNN.Open();
                QUR = "INSERT INTO tbl_Detail ";
                QUR += "SELECT MAX(ID) + 1,";
                QUR += "@Name,";
                QUR += "@Address,";
                QUR += "@City_ID,";
                QUR += "@EmailID ";
                QUR += "FROM tbl_Detail";
                CMD = new SqlCommand(QUR, CNN);

                CMD.Parameters.AddWithValue("@Name", MDFST.Name);
                CMD.Parameters.AddWithValue("@Address", MDFST.Address);
                CMD.Parameters.AddWithValue("@City_ID", MDFST.City_ID);
                CMD.Parameters.AddWithValue("@EmailID", MDFST.EmailID);
                CMD.ExecuteNonQuery();
            }
            return RedirectToAction("Index");

        }
        //
        // GET: /FirstTest/Edit/5

        public ActionResult Edit(int id)
        {
            City();
            MDFirst MDFST = new MDFirst();
            DataTable DT = new DataTable();
            using (CNN = new SqlConnection(CNNSTR))
            {
                CNN.Open();
                QUR = "SELECT * FROM tbl_Detail WHERE ID=@ID ";
                DA = new SqlDataAdapter(QUR,CNN);
                DA.SelectCommand.Parameters.AddWithValue("@ID",id);
                DA.Fill(DT);
            }
            if (DT.Rows.Count == 1)
            {
                MDFST.ID = Convert.ToInt32(DT.Rows[0][0].ToString());
                MDFST.Name = DT.Rows[0][1].ToString();
                MDFST.Address = DT.Rows[0][2].ToString();
                MDFST.City_ID = Convert.ToInt32(DT.Rows[0][3].ToString());
                MDFST.EmailID = DT.Rows[0][4].ToString();
               // MDFST.Is_Act = DT.Rows[0][5].ToString();
                return View(MDFST);
            }
            else
            return RedirectToAction("Index");
        }

        //
        // POST: /FirstTest/Edit/5

        [HttpPost]
        public ActionResult Edit(MDFirst MDFDT)
        {
            using (CNN = new SqlConnection(CNNSTR))
            {
                CNN.Open();
                QUR = "UPDATE tbl_Detail SET Name=@Name,Address=@Address,City_ID=@City_ID,EmailID=@EmailID WHERE ID=@ID";
                CMD = new SqlCommand(QUR,CNN);
                CMD.Parameters.AddWithValue("@ID",MDFDT.ID);
                CMD.Parameters.AddWithValue("@Name",MDFDT.Name);
                CMD.Parameters.AddWithValue("@Address",MDFDT.Address);
                CMD.Parameters.AddWithValue("@City_ID",MDFDT.City_ID);
                CMD.Parameters.AddWithValue("@EmailID",MDFDT.EmailID);
                CMD.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /FirstTest/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (CNN = new SqlConnection(CNNSTR))
            {
                CNN.Open();
                QUR = "DELETE FROM tbl_Detail WHERE ID=@ID";
                CMD = new SqlCommand(QUR,CNN);
                CMD.Parameters.AddWithValue("@ID",id);
                CMD.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public ActionResult City()
        {
            List<MDFirst> list = new List<MDFirst>();
            QUR = "SELECT * FROM tbl_City";
            CNN = new SqlConnection(CNNSTR);
            CMD = new SqlCommand(QUR,CNN);
            CNN.Open();
            SqlDataReader DR = CMD.ExecuteReader();
            while(DR.Read())
            {
                MDF = new MDFirst();
                MDF.City_ID =Convert.ToInt32(DR.GetValue(0).ToString());
                MDF.CityNAme = DR.GetValue(1).ToString();
                list.Add(MDF);
            }
            ViewBag.City = new SelectList(list, "City_ID", "CityNAme");
            DR.Close();
            CNN.Close();
            return View();
        }
    }
}
