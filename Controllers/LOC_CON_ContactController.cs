using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DatabaseFetchData.Models;

namespace DatabaseFetchData.Controllers
{
    public class LOC_CON_ContactController : Controller
    {
        #region Configuration
        private IConfiguration Configuration;
        public LOC_CON_ContactController(IConfiguration _configuration)
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
            cmd.CommandText = "PR_LOC_CON_Contact_SelectAll";
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            return View("LOC_CON_ContactList", dt);
        }
        #endregion .

        #region Delete
        public IActionResult Delete(int ContactID)
        {
            String str = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand = new SqlConnection(str);
            objCommand.Open();
            SqlCommand objCommand2 = objCommand.CreateCommand();
            objCommand2.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand2.CommandText = "PR_LOC_CON_Contact_DeleteByPK";
            objCommand2.Parameters.AddWithValue("@ContactID", ContactID);
            objCommand2.ExecuteNonQuery();
            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? ContactID)
        {
            #region DropDown -> Country
            String str1 = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand1 = new SqlConnection(str1);
            objCommand1.Open();
            SqlCommand cmd1 = objCommand1.CreateCommand();
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.CommandText = "LOC_Country_DropDown";
            /*cmd1.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;*/
            DataTable dt1 = new DataTable();
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt1.Load(sdr1);
            objCommand1.Close();
            List<LOC_CountryDropDownModel> vlst = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_CountryDropDownModel v = new LOC_CountryDropDownModel();
                v.CountryID = Convert.ToInt32(dr1["CountryID"]);
                v.CountryName = Convert.ToString(dr1["CountryName"]);
                vlst.Add(v);
            }
            ViewBag.LOC_CountryList = vlst;
            #endregion

            #region DropDown -> State
            /*String str2 = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand2 = new SqlConnection(str2);
            objCommand2.Open();
            SqlCommand cmd2 = objCommand2.CreateCommand();
            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
            cmd2.CommandText = "LOC_State_DropDown";
            DataTable dt2 = new DataTable();
            SqlDataReader sdr2 = cmd2.ExecuteReader();
            dt2.Load(sdr2);
            objCommand2.Close();*/
            List<LOC_StateDropDownModel> vlst1 = new List<LOC_StateDropDownModel>();
            /*foreach (DataRow dr2 in dt2.Rows)
            {
                LOC_StateDropDownModel v1 = new LOC_StateDropDownModel();
                v1.StateID = Convert.ToInt32(dr2["StateID"]);
                v1.StateName = Convert.ToString(dr2["StateName"]);
                vlst1.Add(v1);
            }*/
            ViewBag.LOC_StateList = vlst1;
            #endregion

            #region DropDown -> City
            /*String str3 = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand3 = new SqlConnection(str3);
            objCommand3.Open();
            SqlCommand cmd3 = objCommand3.CreateCommand();
            cmd3.CommandType = System.Data.CommandType.StoredProcedure;
            cmd3.CommandText = "LOC_City_DropDown";
            DataTable dt3 = new DataTable();
            SqlDataReader sdr3 = cmd3.ExecuteReader();
            dt3.Load(sdr3);
            objCommand3.Close();*/
            List<LOC_CityDropDownModel> vlst2 = new List<LOC_CityDropDownModel>();
            /*foreach (DataRow dr2 in dt3.Rows)
            {
                LOC_CityDropDownModel v1 = new LOC_CityDropDownModel();
                v1.CityID = Convert.ToInt32(dr2["CityID"]);
                v1.CityName = Convert.ToString(dr2["CityName"]);
                vlst2.Add(v1);
            }*/
            ViewBag.LOC_CityList = vlst2;
            #endregion

            #region DropDown -> ContactCategory
            String str4 = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand4 = new SqlConnection(str4);
            objCommand4.Open();
            SqlCommand cmd4 = objCommand4.CreateCommand();
            cmd4.CommandType = System.Data.CommandType.StoredProcedure;
            cmd4.CommandText = "LOC_MST_ContactCategory_DropDown";
            DataTable dt4 = new DataTable();
            SqlDataReader sdr4 = cmd4.ExecuteReader();
            dt4.Load(sdr4);
            objCommand4.Close();
            List<LOC_MST_ContactCategoryDropDownModel> vlst3 = new List<LOC_MST_ContactCategoryDropDownModel>();
            foreach (DataRow dr2 in dt4.Rows)
            {
                LOC_MST_ContactCategoryDropDownModel v1 = new LOC_MST_ContactCategoryDropDownModel();
                v1.ContactCategoryID = Convert.ToInt32(dr2["ContactCategoryID"]);
                v1.ContactCategoryName = Convert.ToString(dr2["ContactCategoryName"]);
                vlst3.Add(v1);
            }
            ViewBag.LOC_ContactCategoryList = vlst3;
            #endregion

            #region Select By PK
            if (ContactID != null)
            {
                String str = this.Configuration.GetConnectionString("MyConnectionStrings");
                SqlConnection objCommand = new SqlConnection(str);
                objCommand.Open();
                SqlCommand cmd = objCommand.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_CON_Contact_SelectByPK";
                cmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = ContactID;
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);

                LOC_CON_ContactModel modelLOC_CON_Contact = new LOC_CON_ContactModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_CON_Contact.ContactID = Convert.ToInt32(dr["ContactID"]);
                    modelLOC_CON_Contact.ContactName = dr["ContactName"].ToString();
                    modelLOC_CON_Contact.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_CON_Contact.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_CON_Contact.CityID = Convert.ToInt32(dr["CityID"]);
                    modelLOC_CON_Contact.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelLOC_CON_Contact.Address = dr["Address"].ToString();
                    modelLOC_CON_Contact.PinCode = Convert.ToInt32(dr["PinCode"]);
                    modelLOC_CON_Contact.Mobile = Convert.ToInt32(dr["Mobile"]);
                    modelLOC_CON_Contact.AlternetContact = Convert.ToInt32(dr["AlternetContact"]);
                    modelLOC_CON_Contact.Email = dr["Email"].ToString();
                    modelLOC_CON_Contact.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
                    modelLOC_CON_Contact.LinkedIn = dr["LinkedIn"].ToString();
                    modelLOC_CON_Contact.Twitter = dr["Twitter"].ToString();
                    modelLOC_CON_Contact.Insta = dr["Insta"].ToString();
                    modelLOC_CON_Contact.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_CON_Contact.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }

                return View("LOC_CON_ContactAddEdit", modelLOC_CON_Contact);

            }
            #endregion

            return View("LOC_CON_ContactAddEdit");
        }
        #endregion

        #region Save
        public IActionResult Save(LOC_CON_ContactModel modelLOC_CON_Contact)
        {
            String str = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand = new SqlConnection(str);
            objCommand.Open();
            SqlCommand cmd = objCommand.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            if(modelLOC_CON_Contact.ContactID == null)
            {
                cmd.CommandText = "PR_LOC_CON_Contact_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = modelLOC_CON_Contact.CreationDate;

            }
            else
            {
                cmd.CommandText = "PR_LOC_CON_Contact_UpdateByPK";
                cmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = modelLOC_CON_Contact.ContactID;
            }
            cmd.Parameters.Add("@ContactName", SqlDbType.VarChar).Value = modelLOC_CON_Contact.ContactName;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_CON_Contact.CountryID;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_CON_Contact.StateID;
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelLOC_CON_Contact.CityID;
            cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelLOC_CON_Contact.ContactCategoryID;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = modelLOC_CON_Contact.Address;
            cmd.Parameters.Add("@PinCode", SqlDbType.Int).Value = modelLOC_CON_Contact.PinCode;
            cmd.Parameters.Add("@Mobile", SqlDbType.Int).Value = modelLOC_CON_Contact.Mobile;
            cmd.Parameters.Add("@AlternetContact", SqlDbType.Int).Value = modelLOC_CON_Contact.AlternetContact;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = modelLOC_CON_Contact.Email;
            cmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = modelLOC_CON_Contact.BirthDate;
            cmd.Parameters.Add("@LinkedIn", SqlDbType.VarChar).Value = modelLOC_CON_Contact.LinkedIn;
            cmd.Parameters.Add("@Twitter", SqlDbType.VarChar).Value = modelLOC_CON_Contact.Twitter;
            cmd.Parameters.Add("@Insta", SqlDbType.VarChar).Value = modelLOC_CON_Contact.Insta;
            cmd.Parameters.Add("@ModificationDate", SqlDbType.DateTime).Value = modelLOC_CON_Contact.ModificationDate;
            
            
            if(Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if(modelLOC_CON_Contact.ContactID == null)
                TempData["message"] = "Record Inserted Successsfully";
                else
                    TempData["message"] = "Record Updated Successsfully";

            }
            objCommand.Close();
            return RedirectToAction("Add");
        }
        #endregion

        #region cascaded DropDownByState => State
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

            List<LOC_StateDropDownModel> list1 = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_StateDropDownModel model = new LOC_StateDropDownModel();
                model.StateID = Convert.ToInt32(dr1["StateID"]);
                model.StateName = Convert.ToString(dr1["StateName"]);
                list1.Add(model);
            }

            var VModel = list1;
            return Json(VModel);
        }
        #endregion

        #region cascaded DropDownByCity => City
        public IActionResult DropDownByCity(int StateID)
        {
            String str1 = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand1 = new SqlConnection(str1);
            objCommand1.Open();
            SqlCommand cmd1 = objCommand1.CreateCommand();
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.CommandText = "LOC_City_DropDown";
            cmd1.Parameters.AddWithValue("@StateID", StateID);
            DataTable dt1 = new DataTable();
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt1.Load(sdr1);
            objCommand1.Close();

            List<LOC_CityDropDownModel> vlst2 = new List<LOC_CityDropDownModel>();
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_CityDropDownModel model = new LOC_CityDropDownModel();
                model.CityID = Convert.ToInt32(dr1["CityID"]);
                model.CityName = Convert.ToString(dr1["CityName"]);
                vlst2.Add(model);
            }

            var VModel = vlst2;
            return Json(VModel);
        }
        #endregion
    }
}
