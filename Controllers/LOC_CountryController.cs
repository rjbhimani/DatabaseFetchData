using DatabaseFetchData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseFetchData.Controllers
{
    public class LOC_CountryController : Controller
    {
        #region Configuration
        private IConfiguration Configuration;
        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region SelectAll
        public IActionResult Index()
        {
            String str = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType =System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_SelectAll";
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            return View("LOC_CountryList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CountryID)
        {
            String str = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand = new SqlConnection(str);
            objCommand.Open();
            SqlCommand objCommand2 = objCommand.CreateCommand();
            objCommand2.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand2.CommandText = "PR_LOC_Country_DeleteByPK";
            objCommand2.Parameters.AddWithValue("@CountryID", CountryID);
            objCommand2.ExecuteNonQuery();
            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? CountryID)
        {
            if(CountryID != null)
            {

                String str = this.Configuration.GetConnectionString("MyConnectionStrings");
                SqlConnection objCommand = new SqlConnection(str);
                objCommand.Open();
                SqlCommand cmd = objCommand.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_Country_SelectByPK";
                cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);

                LOC_CountryModel modelLOC_Country = new LOC_CountryModel();

                foreach(DataRow dr in dt.Rows)
                {
                    modelLOC_Country.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_Country.CountryName = dr["CountryName"].ToString();
                    modelLOC_Country.CountryCode = dr["CountryCode"].ToString();
                    modelLOC_Country.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_Country.Modification = Convert.ToDateTime(dr["Modification"]);
                }

                return View("LOC_CountryAddEdit",modelLOC_Country);
            }
            
            return View("LOC_CountryAddEdit");
        }

        #endregion

        #region Save
        public IActionResult Save(LOC_CountryModel modelLOC_Country)
        {
            String str = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand = new SqlConnection(str);
            objCommand.Open();
            SqlCommand cmd = objCommand.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            if(modelLOC_Country.CountryID == null)
            {
                cmd.CommandText = "PR_LOC_Country_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = modelLOC_Country.CreationDate;

            }
            else
            {
                cmd.CommandText = "PR_LOC_Country_UpdateByPK";
                cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_Country.CountryID;
            }

            cmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = modelLOC_Country.CountryName;
            cmd.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = modelLOC_Country.CountryCode;
            cmd.Parameters.Add("@Modification", SqlDbType.Date).Value = modelLOC_Country.Modification;



            if(Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelLOC_Country.CountryID == null)
                {
                    TempData["CountryInsertMsg"] = "Record Inserted Successsfully";
                }
                else 
                {
                    TempData["CountryInsertMsg"] = "Record Updated Successsfully";
                }
            }
            objCommand.Close();
            return RedirectToAction("Add");
        }
        #endregion

    }
}
