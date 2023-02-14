using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FarApi.Controllers
{

    public class ReportController : ControllerBase
    {
        /*** DB Connection ***/
        // static string dbCon = "Server=tcp:95.217.206.195,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=sa;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";        
        // static string dbCon = "Server=tcp:95.217.206.195,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=sa;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        // live server
        // static string dbCon = "Server=tcp:58.27.164.136,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        // static string dbCon = "Server=tcp:125.1.1.244,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        // Production Database
        // static string dbCon = "Server=tcp:58.27.164.136,1433;Initial Catalog=FARProd;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        // static string dbCon = "Server=tcp:125.1.1.244,1433;Initial Catalog=FARProd;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        dbConfig db = new dbConfig();

        /***** Getting assets detail *****/
        [Route("api/getMoveableAssetDetailRpt")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetDetail> getMoveableAssetDetailRpt(long UserId, long mainLocID, long subLocID, long officeTypeID, long projectID, long accountsCatID, long assetCatID, string type, string status, string fromDate, string toDate)
        {
            // where clause for the query
            string whereClause = "";

            // status where clause i.e. useable, serviceable etc.
            if (status == "useable")
            {
                whereClause += " and IsUseable = 1";
            }
            else if (status == "serviceable")
            {
                whereClause += " and IsServiceAble = 1";
            }
            else if (status == "surplus")
            {
                whereClause += " and IsSurplus = 1";
            }
            else if (status == "condemned")
            {
                whereClause += " and IsCondemned = 1";
            }
            else if (status == "missing")
            {
                whereClause += " and IsMissing = 1";
            }

            // location filters
            if (mainLocID != 0)
            {
                whereClause += " and mainLocID = " + mainLocID;
            }
            if (officeTypeID != 0)
            {
                whereClause += " and officeTypeID= " + officeTypeID + " and subLocID = " + subLocID;
            }
            if (projectID != 0)
            {
                whereClause += " and projectID = " + projectID;
            }
            if (accountsCatID != 0)
            {
                whereClause += " and accountsCatID = " + accountsCatID;
            }
            if (assetCatID != 0)
            {
                whereClause += " and assetCatID = " + assetCatID;
            }

            List<assetDetail> rows = new List<assetDetail>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (type == "book")
                {
                    if(fromDate == "null"){
                        rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + " AND accountsCatID= 5  " + whereClause + " order by AssetID desc").ToList();
                    }else{
                        rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + " AND accountsCatID= 5  " + whereClause + " AND RevaluationDate between CONVERT(varchar(10), '" + fromDate + "', 101) AND CONVERT(varchar(10), '" + toDate + "', 101) order by AssetID desc").ToList();
                    }
                }
                else if (type == "computer")
                {
                    if(fromDate == "null"){
                        rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + " AND accountsCatID = 1  " + whereClause + " order by AssetID desc").ToList();
                    }else{
                        rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + " AND accountsCatID = 1  " + whereClause + " AND RevaluationDate between CONVERT(varchar(10), '" + fromDate + "', 101) AND CONVERT(varchar(10), '" + toDate + "', 101) order by AssetID desc").ToList();
                    }
                }
                else if (type == "vehicle")
                {
                    if(fromDate == "null"){
                        rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + " and accountsCatID= 9  " + whereClause + " order by AssetID desc").ToList();
                    }else{
                        rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + " and accountsCatID= 9  " + whereClause + " AND RevaluationDate between CONVERT(varchar(10), '" + fromDate + "', 101) AND CONVERT(varchar(10), '" + toDate + "', 101) order by AssetID desc").ToList();
                    }
                }
                else if (type == "general")
                {    
                    if(fromDate == "null"){
                        rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + " and (accountsCatID = 2 or accountsCatID = 3 or accountsCatID = 4 or accountsCatID = 6 or accountsCatID = 7 or accountsCatID = 8 )  " + whereClause + " order by AssetID desc").ToList();
                    }else{
                        rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + " and (accountsCatID = 2 or accountsCatID = 3 or accountsCatID = 4 or accountsCatID = 6 or accountsCatID = 7 or accountsCatID = 8 )  " + whereClause + " AND RevaluationDate between CONVERT(varchar(10), '" + fromDate + "', 101) AND CONVERT(varchar(10), '" + toDate + "', 101) order by AssetID desc").ToList();
                    }
                }
                else if (type == null)
                {
                    if(fromDate == "null"){
                        rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + "  " + whereClause + " order by AssetID desc").ToList();
                    }else{
                        rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + "  " + whereClause + " AND RevaluationDate between CONVERT(varchar(10), '" + fromDate + "', 101) AND CONVERT(varchar(10), '" + toDate + "', 101) order by AssetID desc").ToList();
                    }
                }
            }

            return rows;
        }

        /***** Getting assets detail *****/
        [Route("api/getAssetDetailComputers")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetDetail> getAssetDetailComputers(long UserId, long mainLocID, long officeTypeID, long projectID, long accountsCatID, long assetCatID)
        {

            // where clause for the query
            string whereClause = "";

            if (mainLocID != 0 && officeTypeID == 0 && projectID == 0 && accountsCatID == 0 && assetCatID == 0)
            {
                whereClause = " mainLocID = " + mainLocID + " order by AssetID desc";
            }
            else if (mainLocID == 0 && officeTypeID != 0 && projectID == 0 && accountsCatID == 0 && assetCatID == 0)
            {
                whereClause = " officeTypeID= " + officeTypeID + " order by AssetID desc";
            }
            else if (mainLocID == 0 && officeTypeID == 0 && projectID != 0 && accountsCatID == 0 && assetCatID == 0)
            {
                whereClause = " projectID = " + projectID + " order by AssetID desc";
            }
            else if (mainLocID == 0 && officeTypeID == 0 && projectID == 0 && accountsCatID != 0 && assetCatID == 0)
            {
                whereClause = " accountsCatID = " + accountsCatID + " order by AssetID desc";
            }
            else if (mainLocID == 0 && officeTypeID == 0 && projectID == 0 && accountsCatID == 0 && assetCatID != 0)
            {
                whereClause = " assetCatID = " + assetCatID + " order by AssetID desc";
            }

            List<assetDetail> rows = new List<assetDetail>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();


                rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + " and accountsCatID = 1 and " + whereClause).ToList();

            }

            return rows;
        }

        /********** Vehicle Register Report ***********/
        [Route("api/getAssetDetailVehicles")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetDetail> getAssetDetailVehicles(long UserId, long mainLocID, long officeTypeID, long projectID, long accountsCatID, long assetCatID)
        {

            // where clause for the query
            string whereClause = "";

            if (mainLocID != 0 && officeTypeID == 0 && projectID == 0 && accountsCatID == 0 && assetCatID == 0)
            {
                whereClause = " and mainLocID = " + mainLocID + " order by AssetID desc";
            }
            else if (mainLocID == 0 && officeTypeID != 0 && projectID == 0 && accountsCatID == 0 && assetCatID == 0)
            {
                whereClause = " and officeTypeID= " + officeTypeID + " order by AssetID desc";
            }
            else if (mainLocID == 0 && officeTypeID == 0 && projectID != 0 && accountsCatID == 0 && assetCatID == 0)
            {
                whereClause = " and projectID = " + projectID + " order by AssetID desc";
            }
            else if (mainLocID == 0 && officeTypeID == 0 && projectID == 0 && accountsCatID != 0 && assetCatID == 0)
            {
                whereClause = " and accountsCatID = " + accountsCatID + " order by AssetID desc";
            }
            else if (mainLocID == 0 && officeTypeID == 0 && projectID == 0 && accountsCatID == 0 && assetCatID != 0)
            {
                whereClause = " and assetCatID = " + assetCatID + " order by AssetID desc";
            }

            List<assetDetail> rows = new List<assetDetail>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();


                rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + " and accountsCatID= 9 " + whereClause).ToList();

            }

            return rows;
        }

        /********** General Register Report ***********/
        [Route("api/getAssetDetailGeneral")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetDetail> getAssetDetailGeneral(long UserId, long mainLocID, long officeTypeID, long projectID, long accountsCatID, long assetCatID)
        {

            // where clause for the query
            string whereClause = "";

            if (mainLocID != 0 && officeTypeID == 0 && projectID == 0 && accountsCatID == 0 && assetCatID == 0)
            {
                whereClause = " mainLocID = " + mainLocID + " order by AssetID desc";
            }
            else if (mainLocID == 0 && officeTypeID != 0 && projectID == 0 && accountsCatID == 0 && assetCatID == 0)
            {
                whereClause = " officeTypeID= " + officeTypeID + " order by AssetID desc";
            }
            else if (mainLocID == 0 && officeTypeID == 0 && projectID != 0 && accountsCatID == 0 && assetCatID == 0)
            {
                whereClause = " projectID = " + projectID + " order by AssetID desc";
            }
            else if (mainLocID == 0 && officeTypeID == 0 && projectID == 0 && accountsCatID != 0 && assetCatID == 0)
            {
                whereClause = " accountsCatID = " + accountsCatID + " order by AssetID desc";
            }
            else if (mainLocID == 0 && officeTypeID == 0 && projectID == 0 && accountsCatID == 0 && assetCatID != 0)
            {
                whereClause = " assetCatID = " + assetCatID + " order by AssetID desc";
            }

            List<assetDetail> rows = new List<assetDetail>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();


                rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + " and (accountsCatID = 2 or accountsCatID = 3 or accountsCatID = 4 or accountsCatID = 6 or accountsCatID = 7 or accountsCatID = 8 )  and " + whereClause).ToList();

            }

            return rows;
        }

        /***** Getting Sub Locations *****/
        [Route("api/getLocationCheckList")]
        [HttpGet]
        [EnableCors("CorePolicy")]

        public IEnumerable<subLocationCheckList> getLocationCheckList(int subLocID, int officeTypeID)
        {
            List<subLocationCheckList> rows = new List<subLocationCheckList>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                rows = con.Query<subLocationCheckList>("select * from View_SubLocationsCheckList where subLocID = " + subLocID + " and officeTypeID = " + officeTypeID + "").ToList();
            }
            return rows;
        }

        [Route("api/updatechecklist")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult updatechecklist([FromBody] comLocation obj)
        {
            //
            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                using (IDbConnection con = new SqlConnection(db.dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@LocCheckListID", obj.LocCheckListID);
                    parameters.Add("@Description", obj.Description);
                    parameters.Add("@Edoc", obj.EDoc);
                    parameters.Add("@EDocExtension", obj.EDocExtension);
                    parameters.Add("@Status", obj.status);
                    parameters.Add("@Userid", obj.UserId);
                    parameters.Add("@SubLocCompletionID", obj.SubLocCompletionID);
                    parameters.Add("@SpType", obj.SpType);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.SP_SubLocationsCompletions", parameters, commandType: CommandType.StoredProcedure);
                    sqlResponse = parameters.Get<string>("@ResponseMessage");

                    //first image 
                    if (obj.imgFile != null && sqlResponse.ToUpper() == "SUCCESS")
                    {

                        String path = obj.EDoc; //Path

                        //Check if directory exist
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                        }

                        string imageName = obj.SubLocCompletionID + "." + obj.EDocExtension;

                        //set the image path
                        string imgPath = Path.Combine(path, imageName);

                        //delete image portion start
                        if (System.IO.File.Exists(Path.Combine(path, imageName)))
                        {
                            System.IO.File.Delete(Path.Combine(path, imageName));
                        }
                        //delete image portion end

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

        /*** Asset Category Summary Report ***/
        [Route("api/getAssetCatSumRpt")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetCatSum> getAssetCatSumRpt(int mainLocId, int subLocId)
        {
            List<assetCatSum> rows = new List<assetCatSum>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (mainLocId != 0)
                {
                    rows = con.Query<assetCatSum>("select * from view_AssetCategorySummaryRpt where MainLocId = " + mainLocId + " order by MainLocId, subLocId").ToList();
                }
                else if (subLocId != 0)
                {
                    rows = con.Query<assetCatSum>("select * from view_AssetCategorySummaryRpt where subLocId = " + subLocId + " order by MainLocId, subLocId").ToList();
                }
                else if (mainLocId != 0 && subLocId != 0)
                {
                    rows = con.Query<assetCatSum>("select * from view_AssetCategorySummaryRpt where MainLocId = " + mainLocId + " and subLocId = " + subLocId + " order by MainLocId, subLocId").ToList();
                }
                else
                {
                    rows = con.Query<assetCatSum>("select * from view_AssetCategorySummaryRpt order by MainLocId, subLocId").ToList();
                }
            }
            return rows;
        }

        /*** Form 47 without vehicle report Report ***/
        [Route("api/getForm47WithoutVehicle")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<oe_ff_com_egi_47> getForm47WithoutVehicle(long mainLocId, int accountCatID, string fromDate, string toDate)
        {
            List<oe_ff_com_egi_47> rows = new List<oe_ff_com_egi_47>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (mainLocId != 0)
                {
                    rows = con.Query<oe_ff_com_egi_47>("select * from View_OE_FF_COM_EGI_47 where MainLocId = " + mainLocId + " and accountsCatID = " + accountCatID + " AND DateofPurchase between CONVERT(varchar(10), '" + fromDate + "', 101) AND CONVERT(varchar(10), '" + toDate + "', 101) order by MainLocId").ToList();
                }
                else
                {
                    rows = con.Query<oe_ff_com_egi_47>("select * from View_OE_FF_COM_EGI_47 where accountsCatID = " + accountCatID + " AND DateofPurchase between CONVERT(varchar(10), '" + fromDate + "', 101) AND CONVERT(varchar(10), '" + toDate + "', 101) order by MainLocId, subLocId").ToList();
                }
            }
            return rows;
        }

        /*** Form 47 vehicle report Report ***/
        [Route("api/getForm47Vehicle")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<vehicle_47> getForm47Vehicle(long mainLocId, string fromDate, string toDate)
        {
            List<vehicle_47> rows = new List<vehicle_47>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (mainLocId != 0)
                {
                    rows = con.Query<vehicle_47>("select * from View_Vehicles_47 where MainLocId = " + mainLocId + " and PurchaseDate between CONVERT(varchar(10), '" + fromDate + "', 101) AND CONVERT(varchar(10), '" + toDate + "', 101) order by MainLocId").ToList();
                }
                else
                {
                    rows = con.Query<vehicle_47>("select * from View_Vehicles_47 where PurchaseDate between CONVERT(varchar(10), '" + fromDate + "', 101) AND CONVERT(varchar(10), '" + toDate + "', 101) order by MainLocId, subLocId").ToList();
                }
            }
            return rows;
        }
		
		/** Form 8B record of Disposed assets Report **/
        [Route("api/getDisposedAssetRpt")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<disposalOfAssets8BRpt> getDisposedAssetRpt(long mainLocId, long subLocId, string fromDate, string toDate)
        {
            List<disposalOfAssets8BRpt> rows = new List<disposalOfAssets8BRpt>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (mainLocId != 0 && subLocId == 0)
                {
                    rows = con.Query<disposalOfAssets8BRpt>("select * from View_DisposalOfAssets_8B where MainLocId = " + mainLocId + " AND PurchaseDate CONVERT(varchar(10), '" + fromDate + "', 101) AND CONVERT(varchar(10), '" + toDate + "', 101) order by MainLocId").ToList();
                }
                else if (mainLocId != 0 && subLocId != 0)
                {
                    rows = con.Query<disposalOfAssets8BRpt>("select * from View_DisposalOfAssets_8B where MainLocId = " + mainLocId + " and SubLocId = " + subLocId + " AND PurchaseDate CONVERT(varchar(10), '" + fromDate + "', 101) AND CONVERT(varchar(10), '" + toDate + "', 101) order by MainLocId, subLocId").ToList();
                }
                else
                {
                    rows = con.Query<disposalOfAssets8BRpt>("select * from View_DisposalOfAssets_8B where PurchaseDate CONVERT(varchar(10), '" + fromDate + "', 101) AND CONVERT(varchar(10), '" + toDate + "', 101) order by MainLocId, subLocId").ToList();
                }
            }
            return rows;
        }

        
    }

    
}