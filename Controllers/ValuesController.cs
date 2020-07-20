using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FarApi.Controllers
{
    public class ValuesController : ControllerBase
    {

        /*** DB Connection ***/
        static string dbCon = "Server=tcp:95.217.206.195,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=sa;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";





        [Route("api/resetpw")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult resetPassword([FromBody] userProfile obj)
        {

            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserName", obj.UserName);
                    parameters.Add("@HashPassword", obj.HashPassword);
                    parameters.Add("@UpdatedBY", obj.UpdatedBY);
                    parameters.Add("@SPtype", obj.SPType);

                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.SP_resetpassword", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }





        [Route("api/activatelogin")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult activateLogin([FromBody] activateLogin obj)
        {

            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ID", obj.ID);
                    parameters.Add("@ActivationStatus", obj.ActivationStatus); //'ACTIVATED' OR 'DEACTIVATED'
                    parameters.Add("@LoginID", obj.LoginID);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_LoginActivation", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }





        [Route("api/mainlocation")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult mainLocation([FromBody] mainLocation obj)
        {

            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ProvinceID", obj.ProvinceID);
                    parameters.Add("@MainLocation", obj.MainLocation);
                    parameters.Add("@MainLocationCode", obj.MainLocationCode);
                    parameters.Add("@MainLocationID", obj.MainLocationID);
                    parameters.Add("@SPType", obj.SPType);                  //'INSERT', 'UPDATE, 'DELETE'
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_MainLocations", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }





        [Route("api/sublocation")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult subLocation([FromBody] subLocation obj)
        {

            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MainLocID", obj.MainLocID);
                    parameters.Add("@SubLocation", obj.SubLocation);
                    parameters.Add("@subLocationCode", obj.subLocationCode);
                    parameters.Add("@SubLocationID", obj.SubLocationID);
                    parameters.Add("@OfficeTypeID", obj.OfficeTypeID);
                    parameters.Add("@UserId", obj.UserId);
                    parameters.Add("@SPType", obj.SPType);                      //'INSERT', 'UPDATE, 'DELETE'
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_SubLocations", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }





        [Route("api/ofcsection")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult ofcSection([FromBody] ofcSection obj)
        {

            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@OfficeCode", obj.OfficeCode);
                    parameters.Add("@OfficeDescription", obj.OfficeDescription);
                    parameters.Add("@OfficeSecID", obj.OfficeSecID);
                    parameters.Add("@OfficeTypeID", obj.OfficeTypeID);
                    parameters.Add("@WingID", obj.WingID);
                    parameters.Add("@Userid", obj.UserId);
                    parameters.Add("@SPType", obj.SPType);                  //'INSERT', 'UPDATE, 'DELETE'
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_OfficeSections", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }





        [Route("api/ofctype")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult ofcType([FromBody] ofcType obj)
        {

            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@OfficeTypeCode", obj.OfficeTypeCode);
                    parameters.Add("@OfficeType", obj.OfficeType);
                    parameters.Add("@OfficeTypeID", obj.OfficeTypeID);
                    parameters.Add("@Userid", obj.Userid);
                    parameters.Add("@SPType", obj.SPType);                  //'INSERT', 'UPDATE, 'DELETE'
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_OfficeTypes", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }





        [Route("api/reguser")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult regUser([FromBody] userProfile obj)
        {

            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@LoginName", obj.LoginName);
                    parameters.Add("@HashPassword", obj.HashPassword);
                    parameters.Add("@Name", obj.Name);
                    parameters.Add("@PostID", obj.PostID);
                    parameters.Add("@PhoneNo", obj.PhoneNo);
                    parameters.Add("@CellNo", obj.CellNo);
                    parameters.Add("@Email", obj.Email);
                    parameters.Add("@Address", obj.Address);
                    parameters.Add("@LoginID", obj.LoginID);
                    parameters.Add("@SPType", obj.SPType);
                    parameters.Add("@Pincode", obj.Pincode);
                    //'INSERT', 'UPDATE, 'DELETE'
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.SP_UserS", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }





        /***** Getting Sub Locations *****/
        [Route("api/getsubloc")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<subLocationsDetail> getSubLocations(int IsActivated)
        {
            List<subLocationsDetail> rows = new List<subLocationsDetail>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (IsActivated == 0)
                {
                    rows = con.Query<subLocationsDetail>("select * from View_SubLocations ").ToList();
                }
                else
                {
                    rows = con.Query<subLocationsDetail>("select * from View_SubLocations Where ISActivated = " + IsActivated + "").ToList();
                }
                
            }

            return rows;
        }





        /***** Getting ofc type *****/
        [Route("api/getofctype")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<ofcType> getOfcTypes()
        {
            List<ofcType> rows = new List<ofcType>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<ofcType>("select * from View_OfficeTypes ").ToList();
            }

            return rows;
        }






        /***** Getting sections *****/
        [Route("api/getwingsec")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<wingSection> getWingSection(int OfficeTypeID, int IsActivated)
        {
            List<wingSection> rows = new List<wingSection>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (IsActivated == 0)
                {
                    rows = con.Query<wingSection>("select * from view_OfficeSections").ToList();
                }
                else
                {
                    rows = con.Query<wingSection>("select * from view_OfficeSections WHERE OfficeTypeID= " + OfficeTypeID + " AND IsActivated = " + IsActivated + "").ToList();
                }

            }

            return rows;
        }




        /***** Getting sections *****/
        [Route("api/getwing")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<wingSection> getWing()
        {
            List<wingSection> rows = new List<wingSection>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<wingSection>("select * from View_Wings").ToList();

            }

            return rows;
        }






        /***** Getting asset catagory *****/
        [Route("api/getassetcat")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetCategory> getAssetCatagory(int IsActivated)
        {
            List<assetCategory> rows = new List<assetCategory>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if(IsActivated == 0)
                {
                    rows = con.Query<assetCategory>("select * from View_AssetCatagories ").ToList();
                }
                else
                {
                    rows = con.Query<assetCategory>("select * from View_AssetCatagories Where IsActivated = " + IsActivated + " ").ToList();

                }
                
            }

            return rows;
        }





        /***** Getting Posts *****/
        [Route("api/getposts")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<custody> getPosts(int IsActivated)
        {
            List<custody> rows = new List<custody>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                if(IsActivated == 0)
                {
                    rows = con.Query<custody>("select * from view_Posts ").ToList();
                }
                else
                {
                    rows = con.Query<custody>("select * from view_Posts WHERE IsActivated = " + IsActivated + "").ToList();
                }
                
            }

            return rows;
        }





        /***** Getting Projects *****/
        [Route("api/getprojects")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<project> getProjects(int IsActivated)
        {
            List<project> rows = new List<project>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if(IsActivated == 0)
                {
                    rows = con.Query<project>("select * from View_Projects ").ToList();
                }
                else
                {
                    rows = con.Query<project>("select * from View_Projects Where IsActivated = " + IsActivated + " ").ToList();
                }
                
            }

            return rows;
        }




        /***** Getting Main Locations *****/
        [Route("api/getmainLoc")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<location> getMainLoc()
        {
            List<location> rows = new List<location>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<location>("select * from View_MainLocations").ToList();
            }

            return rows;
        }




        /***** Getting Main Locations *****/
        [Route("api/getmonthlytottags")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<monthlyTags> getMonthlyTotTags(int month, int year)
        {
            List<monthlyTags> rows = new List<monthlyTags>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<monthlyTags>("select * from View_NoofTags_Monthly_UNPIVOTDASHBOARD where Month1 = " + month + " and year1 = " + year).ToList();
            }

            return rows;
        }



        /***** Getting Asset Condition *****/
        [Route("api/getassetcondition")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<condition> getAssetCondition()
        {
            List<condition> rows = new List<condition>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<condition>("select * from View_AssetConditions ").ToList();
            }

            return rows;
        }






        /***** Getting Vehicles *****/
        [Route("api/getvehicles")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<vehicle> getVehicles()
        {
            List<vehicle> rows = new List<vehicle>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<vehicle>("select * from View_Vehicles ").ToList();
            }

            return rows;
        }





        /***** Getting vehicle makes *****/
        [Route("api/getvehiclemake")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<vehicleMake> getVehicleMake()
        {
            List<vehicleMake> rows = new List<vehicleMake>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<vehicleMake>("select * from View_VehiclesMake ").ToList();
            }

            return rows;
        }






        /***** Getting vehicle model *****/
        [Route("api/getvehiclemodel")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<vehicleModel> getVehicleModel()
        {
            List<vehicleModel> rows = new List<vehicleModel>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<vehicleModel>("select * from View_VehiclesModel ").ToList();
            }

            return rows;
        }






        /***** Getting vehicle type *****/
        [Route("api/getvehicletype")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<vehicleType> getVehicleType()
        {
            List<vehicleType> rows = new List<vehicleType>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<vehicleType>("select * from View_VehiclesType ").ToList();
            }

            return rows;
        }





        /***** Save Vehicle *****/
        [Route("api/savevehicle")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult saveVehicle([FromBody] vehicle obj)
        {

            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@VehID", obj.VehID);
                    parameters.Add("@Make", obj.Make);
                    parameters.Add("@Model", obj.Model);
                    parameters.Add("@Type", obj.Type);
                    parameters.Add("@ChasisNum", obj.ChasisNum);
                    parameters.Add("@EngineNum", obj.EngineNum);
                    parameters.Add("@Remarks", obj.Remarks);
                    parameters.Add("@ID", obj.ID);
                    parameters.Add("@Userid", obj.UserId);
                    parameters.Add("@SPType", obj.SpType);                 //'INSERT', 'UPDATE, 'DELETE'
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_Vehicles", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }






        /***** Save Asset *****/
        [Route("api/saveasset")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult saveAsset([FromBody] asset obj)
        {

            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@SubLocID", obj.SubLocID);
                    parameters.Add("@OfficeTypeID", obj.OfficeTypeID);
                    parameters.Add("@AssetCatID", obj.AssetCatID);
                    parameters.Add("@AssetNo", obj.AssetNo);
                    parameters.Add("@OfficeSecID", obj.OfficeSecID);
                    parameters.Add("@PostID", obj.PostID);
                    parameters.Add("@AssetLocation", obj.AssetLocation);
                    parameters.Add("@AssetDescription", obj.AssetDescription);
                    parameters.Add("@otherIdentification", obj.otherIdentification);
                    parameters.Add("@SerialNo", obj.SerialNo);
                    parameters.Add("@VehicleID", obj.VehicleID);
                    parameters.Add("@ProjectID", obj.ProjectID);
                    parameters.Add("@PreviousTag", obj.PreviousTag);
                    parameters.Add("@costAmount", obj.costAmount);
                    parameters.Add("@NetBookAmount", obj.NetBookAmount);
                    parameters.Add("@PurchaseDate", obj.PurchaseDate);
                    parameters.Add("@IPCRef", obj.IPCRef);
                    parameters.Add("@AssetCondition", obj.AssetCondition);
                    parameters.Add("@IsUseable", obj.IsUseable);
                    parameters.Add("@IsSurplus", obj.IsSurplus);
                    parameters.Add("@IsServiceAble", obj.IsServiceAble);
                    parameters.Add("@IsCondemned", obj.IsCondemned);
                    parameters.Add("@IsMissing", obj.IsMissing);
                    parameters.Add("@Remarks", obj.Remarks);
                    parameters.Add("@IsDeleted", obj.IsDeleted);
                    parameters.Add("@DeletionDate", obj.DeletionDate);
                    parameters.Add("@DeleteBy", obj.DeleteBy);
                    parameters.Add("@IsUpdated", obj.IsUpdated);
                    parameters.Add("@UpdatedDate", obj.UpdatedDate);
                    parameters.Add("@Updatedby", obj.Updatedby);
                    parameters.Add("@AssetID", obj.AssetID);

                    parameters.Add("@EDoc", obj.EDoc);
                    parameters.Add("@EdocExtension", obj.EDocExtension);
                    parameters.Add("@QTY", obj.Qty);
                    parameters.Add("@isTransfer", obj.isTransfer);
                    parameters.Add("@TransferID", obj.TransferID);

                    parameters.Add("@Userid", obj.UserId);
                    parameters.Add("@SPType", obj.SpType);                 //'INSERT', 'UPDATE, 'DELETE'
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.SP_Assets", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                    int SeqId = parameters.Get<int>("@SeqId");

                    if (obj.imgFile != null && sqlResponse.ToUpper() == "SUCCESS")
                    {
                        String path = obj.EDoc; //Path

                        //Check if directory exist
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                        }

                        string imageName = SeqId + "." + obj.EDocExtension;

                        //set the image path
                        string imgPath = Path.Combine(path, imageName);

                        byte[] imageBytes = Convert.FromBase64String(obj.imgFile);

                        System.IO.File.WriteAllBytes(imgPath, imageBytes);
                    }
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }






        /***** get asset no *****/
        [Route("api/getassetno")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult getAssetNo([FromBody] asset obj)
        {

            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                Int32 sqlResponse;
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@SubLocID", obj.SubLocID);
                    parameters.Add("@OfficeTypeID", obj.OfficeTypeID);
                    parameters.Add("@AssetCatID", obj.AssetCatID);
                    parameters.Add("@OfficeSecID", obj.OfficeSecID);
                    parameters.Add("@AssetNo", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_GetAssetNo", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<Int32>("@AssetNo");
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }






        /***** Getting assets detail *****/
        [Route("api/getassetdetail")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetDetail> getAssetDetail(long UserId, long SubLocID, long OfficeTypeID)
        {
            List<assetDetail> rows = new List<assetDetail>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();


                if (UserId != 0)
                {
                    rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + " AND SubLocID= " + SubLocID + " AND OfficeTypeID= " + OfficeTypeID + "").ToList();
                }
                else
                {
                    if (SubLocID == 0 && OfficeTypeID == 0)
                    {
                        rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm").ToList();
                    }
                    else if (SubLocID != 0 && OfficeTypeID == 0)
                    {
                        rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE SubLocID= " + SubLocID + "").ToList();
                    }
                    else if (SubLocID != 0 && OfficeTypeID != 0)
                    {
                        rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE SubLocID= " + SubLocID + " AND OfficeTypeID= " + OfficeTypeID + "").ToList();
                    }
                }
            }

            return rows;
        }






        /***** Getting assets detail *****/
        [Route("api/getuserassetdetail")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetDetail> getAssetDetailUserWise(long UserId)
        {
            List<assetDetail> rows = new List<assetDetail>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + " ").ToList();
            }

            return rows;
        }





        /***** Getting tags list *****/
        [Route("api/gettags")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<tags> getTag(long UserId)
        {
            List<tags> rows = new List<tags>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<tags>("select * from VIEW_tagsforprints WHERE Userid= " + UserId + "").ToList();

            }

            return rows;
        }






        /***** Getting all locations *****/
        [Route("api/getallloc")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<int> getAllLocation()
        {
            List<int> rows = new List<int>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<int>("select * from View_AllLocations").ToList();

            }

            return rows;
        }






        /***** Getting complete locations *****/
        [Route("api/getcmploc")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<int> getCompleteLocation()
        {
            List<int> rows = new List<int>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<int>("select * from View_CompletedLocations").ToList();

            }

            return rows;
        }






        /***** Getting remaining locations *****/
        [Route("api/getremloc")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<int> getRemainingLocation()
        {
            List<int> rows = new List<int>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<int>("select * from View_INCompleteLocations").ToList();

            }

            return rows;
        }







        /***** Getting dashboard tags summary *****/
        [Route("api/gettagssummary")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<tagsSummary> getDashboardTagsSummary()
        {
            List<tagsSummary> rows = new List<tagsSummary>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<tagsSummary>("select * from View_LocationsSummaryTagsDASHBOARD").ToList();

            }

            return rows;
        }






        /***** Getting all locations detail *****/
        [Route("api/getlocdetail")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<subLocationsDetail> getAllLocationsDetail()
        {
            List<subLocationsDetail> rows = new List<subLocationsDetail>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<subLocationsDetail>("select * from VIEW_AllLocationsDetail ").ToList();

            }

            return rows;
        }






        /***** Getting complete location detail *****/
        [Route("api/getcomplocdetail")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<subLocationsDetail> getCompLocDetail()
        {
            List<subLocationsDetail> rows = new List<subLocationsDetail>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<subLocationsDetail>("select * from View_CompletedLocationsDetail ").ToList();

            }

            return rows;
        }






        /***** Getting in complete location detail *****/
        [Route("api/getincomplocdetail")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<subLocationsDetail> getInCompLocDetail()
        {
            List<subLocationsDetail> rows = new List<subLocationsDetail>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<subLocationsDetail>("select * from View_INCompleteLocationsDetail ").ToList();

            }

            return rows;
        }






        /***** Getting tags detail dashboard *****/
        [Route("api/gettagsdetaildb")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<subLocationsDetail> getTagsDetailBashboard()
        {
            List<subLocationsDetail> rows = new List<subLocationsDetail>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<subLocationsDetail>("select * from VIEW_NoofTags_LocationWise_DASHBOARD ").ToList();

            }

            return rows;
        }






        /***** Getting date wise all tags *****/
        [Route("api/getalltagsdetaildatewise")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<tagsDetail> getAllTagsDetailDateWise()
        {
            List<tagsDetail> rows = new List<tagsDetail>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<tagsDetail>("select * from View_NoofTagsALL_DateWise_DASHBOARD").ToList();

            }

            return rows;
        }





        /***** Getting date wise and location wise tags *****/
        [Route("api/gettagsdetaillocwise")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<tagsDetailDatewise> getTagsDetailDateWise(long LocationID)
        {
            List<tagsDetailDatewise> rows = new List<tagsDetailDatewise>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<tagsDetailDatewise>("select * from View_NoofTags_DateWise_LocationWise_DASHBOARD WHERE SubLocID = '" + LocationID + " ' ").ToList();

            }

            return rows;
        }






        /***** Getting date wise and location wise tags *****/
        [Route("api/gettagsdetaildatewise")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<tagsDetailDatewise> getTagsDetailDateWise(string reqDate, long LocationID)
        {
            List<tagsDetailDatewise> rows = new List<tagsDetailDatewise>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();


                if (LocationID == 0)
                {
                    rows = con.Query<tagsDetailDatewise>("select * from View_NoofTags_DateWise_LocationWise_DASHBOARD WHERE CreatedDate = '" + reqDate + " ' ").ToList();
                }
                else
                {
                    rows = con.Query<tagsDetailDatewise>("select * from View_NoofTags_DateWise_LocationWise_OffTypeWise_DASHBOARD WHERE CreatedDate = '" + reqDate + "' AND SubLocID= " + LocationID + " ").ToList();
                }
            }

            return rows;
        }






        /***** Getting all assets catagorys for dashboard *****/
        [Route("api/getallassetdashboard")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetCatDashboard> getAllAssetDashboard()
        {
            List<assetCatDashboard> rows = new List<assetCatDashboard>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<assetCatDashboard>("select * from View_NoofTagsALL_AccountsCatagory_PIVOTDASHBOARD").ToList();

            }

            return rows;
        }






        /***** Getting all assets catagorys location wise for dashboard *****/
        [Route("api/getallassetlocwisedashboard")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetCatLocDashboard> getAllAssetLocationWiseDashboard(int LocationID)
        {
            List<assetCatLocDashboard> rows = new List<assetCatLocDashboard>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<assetCatLocDashboard>("select * from View_NoofTags_CatagoryWise_locationWise_PIVOTDASHBOARD WHERE SubLocID=" + LocationID).ToList();

            }

            return rows;
        }






        /***** Getting asset catagory detail dashboard *****/
        [Route("api/getallassetdetaildashboard")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetCatDetailDashboard> getAssetDetailForDashboard()
        {
            List<assetCatDetailDashboard> rows = new List<assetCatDetailDashboard>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<assetCatDetailDashboard>("select * from View_NoofTagsALL_AssetCatagory_DASHBOARD ").ToList();

            }

            return rows;
        }






        /***** Getting all assets catagorys detail location wise for dashboard *****/
        [Route("api/getallassetdetaillocwisedashboard")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetCatLocDetailDashboard> getAssetDetailLocationWiseForDashboard(int AccCatID)
        {
            List<assetCatLocDetailDashboard> rows = new List<assetCatLocDetailDashboard>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (AccCatID != 0)
                {
                    rows = con.Query<assetCatLocDetailDashboard>("select * from View_NoofTags_AssetCatagory_Location_DASHBOARD WHERE AccountsCatID=" + AccCatID).ToList();
                }
                else
                {
                    rows = con.Query<assetCatLocDetailDashboard>("select * from View_NoofTags_AssetCatagory_Location_DASHBOARD").ToList();
                }


            }

            return rows;
        }






        /***** Getting asset catagory detail dashboard *****/
        [Route("api/getoldtagdata")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<oldTagData> getOldTagData()
        {
            List<oldTagData> rows = new List<oldTagData>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<oldTagData>("select * from View_OldTagData").ToList();

            }

            return rows;
        }






        [Route("api/sudassetcatagory")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult saveAssetCatagory([FromBody] assetCategory obj)
        {

            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@AccountsCatID", obj.AccountsCatID);
                    parameters.Add("@AssetCatCode", obj.AssetCatCode);
                    parameters.Add("@AssetCatDescription", obj.AssetCatDescription);
                    parameters.Add("@Edoc", obj.Edoc);
                    parameters.Add("@EDocExtension", obj.EDocExtension);
                    parameters.Add("@AssetCatID", obj.AssetCatID);
                    parameters.Add("@UserId", obj.UserId);
                    parameters.Add("@SPType", obj.SpType);                      //'INSERT', 'UPDATE, 'DELETE'
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    parameters.Add("@SeqId", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_AssetCatagories", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                    int SeqId = parameters.Get<int>("@SeqId");

                    if (obj.imgFile != null && sqlResponse.ToUpper() == "SUCCESS")
                    {
                        String path = obj.Edoc; //Path

                        //Check if directory exist
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                        }

                        string imageName = SeqId + "." + obj.EDocExtension;

                        //set the image path
                        string imgPath = Path.Combine(path, imageName);

                        byte[] imageBytes = Convert.FromBase64String(obj.imgFile);

                        System.IO.File.WriteAllBytes(imgPath, imageBytes);
                    }

                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }






        [Route("api/sudpost")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult savePost([FromBody] post obj)
        {

            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@PostName", obj.PostName);
                    parameters.Add("@BS", obj.BS);
                    parameters.Add("@nameofCompany", obj.NameofCompany);
                    parameters.Add("@PostID", obj.PostID);
                    parameters.Add("@UserId", obj.UserId);
                    parameters.Add("@SPType", obj.SpType);                      //'INSERT', 'UPDATE, 'DELETE'
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_POSTS", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }






        [Route("api/sudproject")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult saveProject([FromBody] project obj)
        {

            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ProjectShortName", obj.ProjectShortName);
                    parameters.Add("@projectName", obj.ProjectName);
                    parameters.Add("@OfficeSecID", obj.OfficeSecID);
                    parameters.Add("@AccountCode", obj.AccountCode);
                    parameters.Add("@ProjectID", obj.ProjectID);
                    parameters.Add("@UserId", obj.UserId);
                    parameters.Add("@SPType", obj.SpType);                      //'INSERT', 'UPDATE, 'DELETE'
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.SP_Projects", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }





        [Route("api/resettaglist")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult resetTagList([FromBody] project obj)
        {

            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserId", obj.UserId);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_RESETTagsLIST", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }






        /***** Getting tags detail month wise *****/
        [Route("api/gettagsmonthwise")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<tagsMonthWise> getTagsMonthWise(int reqMonth, int reqYear)
        {
            List<tagsMonthWise> rows = new List<tagsMonthWise>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<tagsMonthWise>("select Month1, Year1, Tags, [1] as t1, [2] as t2, [3] as t3, [4] as t4, [5] as t5, [6] as t6, [7] as t7, [8] as t8, [9] as t9, [10] as t10, [11] as t11, [12] as t12, [13] as t13, [14] as t14, [15] as t15, [16] as t16, [17] as t17, [18] as t18, [19] as t19, [20] as t20, [21] as t21, [22] as t22, [23] as t23, [24] as t24, [25] as t25, [26] as t26, [27] as t27, [28] as t28, [29] as t29, [30] as t30 from View_NoofTags_MONTHLY_PIVOTDASHBOARD WHERE Month1=" + reqMonth + " AND Year1 = " + reqYear + " ").ToList();
                //rows = con.Query<tagsMonthWise>("select Month1, Year1, Tags, [1] as T1, [29] as T29, [30] as T30 from View_NoofTags_MONTHLY_PIVOTDASHBOARD ").ToList();
            }

            return rows;
        }





        /***** Getting tags section wise *****/
        [Route("api/gettagssectionwise")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<tagsSection> getTagsSectionWise()
        {
            List<tagsSection> rows = new List<tagsSection>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                //if (OfficeSecID == 0)
                //{
                rows = con.Query<tagsSection>("select * FROM View_NoofTagsALL_SectionWise").ToList();
                //}
                //else
                //{
                //    rows = con.Query<tagsSection>("select * FROM View_NoofTagsALL_SectionWise WHERE OfficeSecID=" + OfficeSecID + " ").ToList();
                //}

            }

            return rows;
        }






        /***** Getting tags section and Location wise *****/
        [Route("api/gettagslocationwise")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<tagsLocation> getTagsLocationWise(int OfficeSecID)
        {
            List<tagsLocation> rows = new List<tagsLocation>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (OfficeSecID == 0)
                {
                    rows = con.Query<tagsLocation>("select * FROM View_NoofTags_SectionWise_LocationWise").ToList();
                }
                else
                {
                    rows = con.Query<tagsLocation>("select * FROM View_NoofTags_SectionWise_LocationWise WHERE OfficeSecID=" + OfficeSecID + " ").ToList();
                }

            }

            return rows;
        }






        /***** Getting tags number wise *****/
        [Route("api/gettagsnumberwise")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<tagsUserWise> getTagsNumberWise()
        {
            List<tagsUserWise> rows = new List<tagsUserWise>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<tagsUserWise>("select * FROM View_NoofTagsbyLoginName").ToList();

            }

            return rows;
        }





        /***** Getting tags user wise *****/
        [Route("api/gettagsuserwise")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<tagsUserWise> getTagsUserWise(int UserId)
        {
            List<tagsUserWise> rows = new List<tagsUserWise>();

            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<tagsUserWise>("select * FROM View_NoofTagsByLoginName_DateWise Where UserId = " + UserId + "").ToList();

            }

            return rows;
        }






        /***** Getting tags user wise and date wise  *****/
        [Route("api/gettagsuserdatewise")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<tagsUserWise> getTagsUserDateWise(int UserId, string reqDate)
        {
            List<tagsUserWise> rows = new List<tagsUserWise>();


            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (UserId == 0)
                {
                    rows = con.Query<tagsUserWise>("select * FROM View_NoofTagsByLoginName_DateWise_LocationWise Where CreatedDate = '" + reqDate + "' ").ToList();
                }
                else
                {
                    rows = con.Query<tagsUserWise>("select * FROM View_NoofTagsByLoginName_DateWise_LocationWise Where UserId = " + UserId + " AND CreatedDate = '" + reqDate + "' ").ToList();
                }


            }

            return rows;
        }
























        [Route("api/sudipcref")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult IpcReference([FromBody] ipcreference obj)
        {
            //
            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ProjectID", obj.ProjectID);
                    parameters.Add("@ProjectPackage", obj.ProjectPackage);
                    parameters.Add("@IPCNo", obj.IPCNo);
                    parameters.Add("@IPCRefDescription", obj.IPCRefDescription);
                    parameters.Add("@EDoc", obj.EDoc);
                    parameters.Add("@EDocExtension", obj.EDocExtension);
                    parameters.Add("@IPCRefID", obj.IPCRefID);
                    parameters.Add("@UserId", obj.UserId);
                    parameters.Add("@SPType", obj.SPType);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    parameters.Add("@SeqId", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.SP_IPCReferences", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                    int SeqId = parameters.Get<int>("@SeqId");

                    if (obj.imgFile != null && sqlResponse.ToUpper() == "SUCCESS")
                    {
                        String path = obj.EDoc; //Path

                        //Check if directory exist
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                        }

                        string imageName = SeqId + "." + obj.EDocExtension;

                        //set the image path
                        string imgPath = Path.Combine(path, imageName);

                        byte[] imageBytes = Convert.FromBase64String(obj.imgFile);

                        System.IO.File.WriteAllBytes(imgPath, imageBytes);
                    }


                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }
        






        [Route("api/getipc")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<ipc> getIPCReferences(int ProjectId)
        {
            List<ipc> rows = new List<ipc>();


            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (ProjectId == 0)
                {
                    rows = con.Query<ipc>("select * FROM View_IPCReferences").ToList();
                }
                else
                {
                    rows = con.Query<ipc>("select * FROM View_IPCReferences Where projectid = " + ProjectId + " ").ToList();
                }


            }

            return rows;
        }






        [Route("api/pincode")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult Pincode([FromBody] pincode obj)
        {
            //
            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserName", obj.UserName);
                    parameters.Add("@Pincode", obj.Pincode);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_VerifyPINCODE", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }






        [Route("api/sudassettransfer")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult AssetTransfer([FromBody] assetTransfer obj)
        {
            //
            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TPostID", obj.TPostID);
                    parameters.Add("@RPostID", obj.RPostID);
                    parameters.Add("@DateofTransfer", obj.DateofTransfer);
                    parameters.Add("@TransferType", obj.TransferType);
                    parameters.Add("@TransferDescription", obj.TransferDescription);
                    parameters.Add("@EDoc", obj.EDoc);
                    parameters.Add("@EDocExtension", obj.EDocExtension);
                    parameters.Add("@TransferID", obj.TransferID);
                    parameters.Add("@ProjectID", obj.ProjectID);
                    parameters.Add("@UserId", obj.UserId);
                    parameters.Add("@SpType", obj.SpType);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    parameters.Add("@SeqId", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_AssetTransfer", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                    int SeqId = parameters.Get<int>("@SeqId");

                    if (obj.imgFile != null && sqlResponse.ToUpper() == "SUCCESS")
                    {
                        String path = obj.EDoc; //Path

                        //Check if directory exist
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                        }

                        string imageName = SeqId + "." + obj.EDocExtension;

                        //set the image path
                        string imgPath = Path.Combine(path, imageName);

                        byte[] imageBytes = Convert.FromBase64String(obj.imgFile);

                        System.IO.File.WriteAllBytes(imgPath, imageBytes);
                    }


                }

                    

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }







        [Route("api/sudipcrefdetail")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult IPCReferancedetail([FromBody] ipcrefdetail obj)
        {
            //
            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@IPCRefID", obj.IPCRefID);
                    parameters.Add("@AssetCatID", obj.AssetCatID);
                    parameters.Add("@Qty", obj.Qty);
                    parameters.Add("@Description", obj.Description);
                    parameters.Add("@IPCRefDetailID", obj.IPCRefDetailID);
                    parameters.Add("@UserId", obj.UserId);
                    parameters.Add("@SpType", obj.SpType);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_IPCReferenceDetail", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");
                }

                response = Ok(new { msg = sqlResponse });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }







        [Route("api/getaccountcat")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<accCatagory> getAccountCatagory()
        {
            List<accCatagory> rows = new List<accCatagory>();


            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<accCatagory>("select * FROM View_AccountsCatagories").ToList();

            }

            return rows;
        }

        




        [Route("api/getipcdetail")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<ipcrefdetail> getIpcRefDetail(int IPCRefID)
        {
            List<ipcrefdetail> rows = new List<ipcrefdetail>();


            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if(IPCRefID == 0)
                {
                    rows = con.Query<ipcrefdetail>("select * FROM View_IPCReferenceDetail").ToList();
                }
                else
                {
                    rows = con.Query<ipcrefdetail>("select * FROM View_IPCReferenceDetail WHERE IPCRefID = " + IPCRefID + "").ToList();
                }

            }

            return rows;
        }






        [Route("api/getassettransferdetail")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetTransfer> getAssetTransferDetail()
        {
            List<assetTransfer> rows = new List<assetTransfer>();


            using (IDbConnection con = new SqlConnection(dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<assetTransfer>("select * FROM View_AssetTransfer").ToList();

            }

            return rows;
        }












    }
}
