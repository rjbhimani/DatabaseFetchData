using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DatabaseFetchData.Models;
using DatabaseFetchData.Dal;

namespace DatabaseFetchData.Controllers
{
    public class LOC_CityController : Controller
    {
        #region Configuration
        private IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region SelectAll
        public IActionResult Index()
        {

            String str = this.Configuration.GetConnectionString("MyConnectionStrings");
            LOC_Dal dalLOC = new LOC_Dal();
            DataTable dt = dalLOC.dbo_PR_LOC_City_SelectAll(str);
            return View("LOC_CityList", dt);

            /*SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_SelectAll";
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);*/


        }
        #endregion

        #region Delete
        public IActionResult Delete(int CityID)
        {
            String str = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand = new SqlConnection(str);
            objCommand.Open();
            SqlCommand objCommand2 = objCommand.CreateCommand();
            objCommand2.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand2.CommandText = "PR_LOC_City_DeleteByPK";
            objCommand2.Parameters.AddWithValue("@CityID", CityID);
            objCommand2.ExecuteNonQuery();
            return RedirectToAction("Index");
        }
        #endregion

        #region AddEdit
        public IActionResult Add(int? CityID)
        {


            #region DropDown => Country
            String str2 = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand2 = new SqlConnection(str2);
            objCommand2.Open();
            SqlCommand cmd2 = objCommand2.CreateCommand();
            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
            cmd2.CommandText = "LOC_Country_DropDown";
            DataTable dt2 = new DataTable();
            SqlDataReader sdr2 = cmd2.ExecuteReader();
            dt2.Load(sdr2);

            List<LOC_CountryDropDownModel> list1 = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr1 in dt2.Rows)
            {
                LOC_CountryDropDownModel model = new LOC_CountryDropDownModel();
                model.CountryID = Convert.ToInt32(dr1["CountryID"]);
                model.CountryName = Convert.ToString(dr1["CountryName"]);
                list1.Add(model);
            }
            ViewBag.CountryList = list1;
            #endregion

            #region DropDown => State
            /*String str1 = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand1 = new SqlConnection(str1);
            objCommand1.Open();
            SqlCommand cmd1 = objCommand1.CreateCommand();
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.CommandText = "LOC_State_DropDown";
            *//*cmd1.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID*//*
            ;
            DataTable dt1 = new DataTable();
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt1.Load(sdr1);*/

            List<LOC_StateDropDownModel> list = new List<LOC_StateDropDownModel>();
            /*foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_StateDropDownModel model = new LOC_StateDropDownModel();
                model.StateID = Convert.ToInt32(dr1["StateID"]);
                model.StateName = Convert.ToString(dr1["StateName"]);
                list.Add(model);
            }*/
            ViewBag.StateList = list;
            #endregion

            #region Select By PK
            if (CityID != null)
            {
                String str = this.Configuration.GetConnectionString("MyConnectionStrings");
                SqlConnection objCommand = new SqlConnection(str);
                objCommand.Open();
                SqlCommand cmd = objCommand.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_City_SelectByPK";
                cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);

                LOC_CityModel modelLOC_City = new LOC_CityModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_City.CityID = Convert.ToInt32(dr["CityID"]);
                    modelLOC_City.CityName = dr["CityName"].ToString();
                    modelLOC_City.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_City.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_City.PinCode = Convert.ToInt32(dr["PinCode"]);
                    modelLOC_City.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("LOC_CityAddEdit", modelLOC_City);

            }
            #endregion

            return View("LOC_CityAddEdit");
        }
        #endregion

        #region Save
        public IActionResult Save(LOC_CityModel modelLOC_City)
        {
            if (ModelState.IsValid)
            {
                String str = this.Configuration.GetConnectionString("MyConnectionStrings");
                SqlConnection objCommand = new SqlConnection(str);
                objCommand.Open();
                SqlCommand cmd = objCommand.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (modelLOC_City.CityID == null)
                {
                    cmd.CommandText = "PR_LOC_City_Insert";
                    cmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = modelLOC_City.CreationDate;


                }
                else
                {
                    cmd.CommandText = "PR_LOC_City_UpDateByPK";
                    cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelLOC_City.CityID;


                }
                cmd.Parameters.Add("@CityName", SqlDbType.VarChar).Value = modelLOC_City.CityName;
                cmd.Parameters.Add("@PinCode", SqlDbType.Int).Value = modelLOC_City.PinCode;
                cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_City.CountryID;
                cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_City.StateID;
                cmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = modelLOC_City.ModificationDate;

                if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
                {
                    if (modelLOC_City.CityID == null)
                    {
                        TempData["message"] = "Record Inserted Successsfully";
                    }
                    else
                    {
                        TempData["message"] = "Record Updated Successsfully";
                    }
                }
                objCommand.Close();
            }
            return RedirectToAction("Add");
        }
        #endregion

        #region cascaded DropDownByState => state
        public IActionResult DropDownByState(int CountryID)
        {
            String str1 = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand1 = new SqlConnection(str1);
            objCommand1.Open();
            SqlCommand cmd1 = objCommand1.CreateCommand();
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.CommandText = "LOC_State_DropDown";
            cmd1.Parameters.AddWithValue("@CountryID", CountryID);
            DataTable dt1 = new DataTable();
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt1.Load(sdr1);
            objCommand1.Close();

            List<LOC_StateDropDownModel> list = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_StateDropDownModel model = new LOC_StateDropDownModel();
                model.StateID = Convert.ToInt32(dr1["StateID"]);
                model.StateName = Convert.ToString(dr1["StateName"]);
                list.Add(model);
            }

            var VModel = list;
            return Json(VModel);
        }
        #endregion
    }
}
