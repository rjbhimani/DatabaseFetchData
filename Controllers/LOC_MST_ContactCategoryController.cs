using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DatabaseFetchData.Models;

namespace DatabaseFetchData.Controllers
{
    public class LOC_MST_ContactCategoryController : Controller
    {
        #region Configuration
        private IConfiguration Configuration;
        public LOC_MST_ContactCategoryController(IConfiguration _configuration)
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
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_MST_ContactCategory_SelectAll";
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            return View("LOC_MST_ContactCategoryList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactCategoryID)
        {
            String str = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand = new SqlConnection(str);
            objCommand.Open();
            SqlCommand objCommand2 = objCommand.CreateCommand();
            objCommand2.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand2.CommandText = "PR_LOC_MST_ContactCategory_DeleteByPK";
            objCommand2.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID);
            objCommand2.ExecuteNonQuery();
            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? ContactCategoryID)
        {
            if(ContactCategoryID != null)
            {
                String str = this.Configuration.GetConnectionString("MyConnectionStrings");
                SqlConnection objCommand = new SqlConnection(str);
                objCommand.Open();
                SqlCommand cmd = objCommand.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_MST_ContactCategory_SelectByPK";
                cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = ContactCategoryID;
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);

                LOC_MST_ContactCategoryModel modelLOC_MST_ContactCategory = new LOC_MST_ContactCategoryModel();

                foreach(DataRow dr in dt.Rows)
                {
                    modelLOC_MST_ContactCategory.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelLOC_MST_ContactCategory.ContactCategoryName = dr["ContactCategoryName"].ToString();
                    modelLOC_MST_ContactCategory.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_MST_ContactCategory.Modification = Convert.ToDateTime(dr["Modification"]);

                }
                return View("LOC_MST_ContactCategoryAddEdit",modelLOC_MST_ContactCategory);

            }
            return View("LOC_MST_ContactCategoryAddEdit");
        }
        #endregion

        #region Save
        public IActionResult Save(LOC_MST_ContactCategoryModel modelLOC_MST_ContactCategory)
        {
            String str = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand = new SqlConnection(str);
            objCommand.Open();
            SqlCommand cmd = objCommand.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            if(modelLOC_MST_ContactCategory.ContactCategoryID == 0)
            {
                cmd.CommandText = "PR_LOC_MST_ContactCategory_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = modelLOC_MST_ContactCategory.CreationDate;

            }
            else
            {
                cmd.CommandText = "PR_LOC_MST_ContactCategory_UpDateByPK";
                cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelLOC_MST_ContactCategory.ContactCategoryID;

            }
            cmd.Parameters.Add("@ContactCategoryName", SqlDbType.VarChar).Value = modelLOC_MST_ContactCategory.ContactCategoryName;
            cmd.Parameters.Add("@Modification", SqlDbType.Date).Value = modelLOC_MST_ContactCategory.Modification;
            
            if(Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelLOC_MST_ContactCategory.ContactCategoryID == 0)
                {
                    TempData["message"] = "Record Inserted Successsfully";
                }
                else
                {
                    TempData["message"] = "Record Updated Successsfully";
                }
            }
            objCommand.Close();
            return RedirectToAction("Add");
        }
        #endregion
    }
}
