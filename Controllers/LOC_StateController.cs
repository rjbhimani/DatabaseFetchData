﻿using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DatabaseFetchData.Models;

namespace DatabaseFetchData.Controllers
{
    public class LOC_StateController : Controller
    {
        #region Configuration
        private IConfiguration Configuration;
        public LOC_StateController(IConfiguration _configuration)
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
            cmd.CommandText = "PR_LOC_State_SelectAll";
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            return View("LOC_StateList", dt);
            
        }
        #endregion

        #region Delete
        public IActionResult Delete(int StateID)
        {
            String str = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand = new SqlConnection(str);
            objCommand.Open();
            SqlCommand objCommand2 = objCommand.CreateCommand();
            objCommand2.CommandType = System.Data.CommandType.StoredProcedure;
            objCommand2.CommandText = "PR_LOC_State_DeleteByPK";
            objCommand2.Parameters.AddWithValue("@StateID", StateID);
            objCommand2.ExecuteNonQuery();
            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? StateID)
        {
            #region DropDown

            String str1 = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand1 = new SqlConnection(str1);
            objCommand1.Open();
            SqlCommand cmd1 = objCommand1.CreateCommand();
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.CommandText = "LOC_Country_DropDown";
            DataTable dt1 = new DataTable();
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt1.Load(sdr1);
            objCommand1.Close();

            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();  
            foreach(DataRow dr in dt1.Rows)
            {
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName = dr["CountryName"].ToString();
                list.Add(vlst);
            }
            ViewBag.CountryList = list;
            #endregion

            #region Select By PK
            if (StateID != null)
            {
                String str = this.Configuration.GetConnectionString("MyConnectionStrings");
                SqlConnection objCommand = new SqlConnection(str);
                objCommand.Open();
                SqlCommand cmd = objCommand.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_State_SelectByPK";
                cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);

                LOC_StateModel modelLOC_State = new LOC_StateModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_State.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_State.StateName = dr["StateName"].ToString();
                    modelLOC_State.StateCode = dr["StateCode"].ToString();
                    modelLOC_State.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_State.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_State.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
               
                 return View("LOC_StateAddEdit", modelLOC_State);
                
            }
            #endregion 
            return View("LOC_StateAddEdit");
        }
        #endregion

        #region Save
        public IActionResult Save(LOC_StateModel modelLOC_State)
        {
            String str = this.Configuration.GetConnectionString("MyConnectionStrings");
            SqlConnection objCommand = new SqlConnection(str);
            objCommand.Open();
            SqlCommand cmd = objCommand.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            if(modelLOC_State.StateID == null)
            {
                cmd.CommandText = "PR_LOC_State_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = modelLOC_State.CreationDate;

            }
            else
            {
                cmd.CommandText = "PR_LOC_State_UpdateByPK";
                cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_State.StateID;


            }

            cmd.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = modelLOC_State.StateName;
            cmd.Parameters.Add("@StateCode", SqlDbType.NVarChar).Value = modelLOC_State.StateCode;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_State.CountryID;
            cmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = modelLOC_State.ModificationDate;


            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelLOC_State.StateID == null)
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
