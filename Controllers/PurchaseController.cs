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
    public class PurchaseConController : ControllerBase
    {

        /*** DB Connection ***/
        // static string dbCon = "Server=tcp:95.217.206.195,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=sa;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";        
        // static string dbCon = "Server=tcp:95.217.206.195,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=sa;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        // live server
        // static string dbCon = "Server=tcp:58.27.164.136,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        // static string dbCon = "Server=tcp:125.1.1.244,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        // static string dbCon = "Server=tcp:10.1.1.1,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        // Production Database
        // static string dbCon = "Server=tcp:58.27.164.136,1433;Initial Catalog=FARProd;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        // static string dbCon = "Server=tcp:125.1.1.244,1433;Initial Catalog=FARProd;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        dbConfig db = new dbConfig();

        [Route("api/getPurchase")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<purchaseList> getPurchase(int userID)
        {
            List<purchaseList> rows = new List<purchaseList>();


            //     if(fromDate == null){
            //         rows = con.Query<purchaseList>("select * from View_Purchases order by purchaseID desc").ToList();
            //     }else{
            //         rows = con.Query<purchaseList>("select * from View_Purchases where PurchaseDate between CONVERT(varchar(10), '" + fromDate + "', 101) AND CONVERT(varchar(10), '" + toDate + "', 101) order by purchaseID desc").ToList();
                


            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                // rows = con.Query<subLocationsDetail>("select * from View_INCompleteLocationsDetail order by provinceName, MainLocationDescription, subLocationDescription desc ").ToList();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Userid", userID);
                // parameters.Add("@SubLOcId", LocationID);

                rows = con.Query<purchaseList>("dbo.sp_view_Purchases", parameters, commandType: CommandType.StoredProcedure).ToList();

            }

            return rows;
        }

        [Route("api/getPurchaseAsset")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<purchaseAssetList> getPurchaseAsset(int purchaseID)
        {
            List<purchaseAssetList> rows = new List<purchaseAssetList>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<purchaseAssetList>("select * from View_PurchaseAssetDetail where PurchaseID=" + purchaseID + " order by assetID desc").ToList();

            }

            return rows;
        }

        /***** crud(Create Read Update Delete ) Purchase *****/
        [Route("api/crudPurchase")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult crudPurchase([FromBody] purchaseList obj)
        {

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
                    parameters.Add("@SubLocID", obj.subLocID);
                    parameters.Add("@OfficesecID", obj.officeSecID);
                    parameters.Add("@ProjectID", obj.projectID);
                    parameters.Add("@IPcRef", obj.iPcRef);
                    parameters.Add("@PurchaseDate", obj.purchaseDate);
                    parameters.Add("@TotalAmount", obj.totalAmount);
                    parameters.Add("@Description", obj.description);
                    parameters.Add("@MemoNo", obj.memoNo);
                    parameters.Add("@MemoDate", obj.memoDate);
                    parameters.Add("@MemoIssuedBy", obj.memoIssuedBy);
                    parameters.Add("@MemoEDoc", obj.memoEDoc);
                    parameters.Add("@MemoEDocExtension", obj.memoEDocExtension);
                    parameters.Add("@Supplier", obj.supplier);
                    parameters.Add("@SuppluerInvNo", obj.supplierInvNo);
                    parameters.Add("@SupplierInVDate", obj.supplierInVDate);
                    parameters.Add("@SupplierInvEDoc", obj.supplierInvEDoc);
                    parameters.Add("@SupplierEDocExtension", obj.supplierEDocExtension);
                    parameters.Add("@ModeofAcq", obj.modeofAcq);
                    parameters.Add("@VoucherNo", obj.voucherNo);
                    parameters.Add("@SpType", obj.spType);
                    parameters.Add("@Userid", obj.userid);
                    parameters.Add("@PurchaseID", obj.purchaseID);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    parameters.Add("@PID", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_Purchases", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");

                    int SeqId = 0;
                    if (sqlResponse.ToUpper() == "SUCCESS")
                        SeqId = parameters.Get<int>("@PID");

                    //first image 
                    if (obj.memoImgFile != null && SeqId != 0)
                    {
                        String path = obj.memoEDoc; //Path

                        //Check if directory exist
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                        }

                        string imageName = SeqId + "_memo." + obj.memoEDocExtension;

                        //set the image path
                        string imgPath = Path.Combine(path, imageName);

                        //delete image portion start
                        if (System.IO.File.Exists(Path.Combine(path, imageName)))
                        {
                            System.IO.File.Delete(Path.Combine(path, imageName));
                        }
                        //delete image portion end

                        byte[] imageBytes = Convert.FromBase64String(obj.memoImgFile);

                        System.IO.File.WriteAllBytes(imgPath, imageBytes);
                    }

                    //2nd image 
                    if (obj.supplierImgFile != null && sqlResponse.ToUpper() == "SUCCESS")
                    {
                        SeqId = parameters.Get<int>("@PID");

                        String path = obj.supplierInvEDoc; //Path

                        //Check if directory exist
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                        }

                        string imageName = SeqId + "_supplier." + obj.supplierEDocExtension;

                        //set the image path
                        string imgPath = Path.Combine(path, imageName);

                        //delete image portion start
                        if (System.IO.File.Exists(Path.Combine(path, imageName)))
                        {
                            System.IO.File.Delete(Path.Combine(path, imageName));
                        }
                        //delete image portion end

                        byte[] imageBytes = Convert.FromBase64String(obj.supplierImgFile);

                        System.IO.File.WriteAllBytes(imgPath, imageBytes);
                    }

                    response = Ok(new { msg = sqlResponse, purID = SeqId });

                }


                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }



        /***** crud(Create Read Update Delete) Purchase Asset *****/
        [Route("api/crudPurchaseAsset")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult crudPurchaseAsset([FromBody] purchaseAssetList obj)
        {

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
                    parameters.Add("@SubLocID", obj.subLocID);
                    parameters.Add("@OfficeTypeID", obj.officeTypeID);
                    parameters.Add("@AssetCatID", obj.assetCatID);
                    parameters.Add("@AssetNo", obj.assetNo);
                    parameters.Add("@OfficeSecID", obj.officeSecID);
                    parameters.Add("@PostID", obj.postID);
                    parameters.Add("@AssetLocation", obj.assetLocation);
                    parameters.Add("@AssetDescription", obj.assetDescription);
                    parameters.Add("@VehicleID", obj.vehicleID);
                    parameters.Add("@ProjectID", obj.projectID);
                    parameters.Add("@costAmount", obj.costAmount);
                    parameters.Add("@PurchaseID", obj.purchaseID);
                    parameters.Add("@PurchaseDate", obj.purchaseDate);
                    parameters.Add("@IPCRef", obj.iPCRef);
                    parameters.Add("@Make", obj.make);
                    parameters.Add("@Model", obj.model);
                    parameters.Add("@Size", obj.size);
                    parameters.Add("@Generation", obj.generation);
                    parameters.Add("@Processor", obj.processor);
                    parameters.Add("@RAM", obj.ram);
                    parameters.Add("@DriveType1", obj.driveType1);
                    parameters.Add("@HD1", obj.HD1);
                    parameters.Add("@DriveType2", obj.driveType2);
                    parameters.Add("@HD2", obj.HD2);
                    parameters.Add("@Author", obj.Author);
                    parameters.Add("@Publisher", obj.publisher);
                    parameters.Add("@Edition", obj.edition);
                    parameters.Add("@Volume", obj.volume);
                    parameters.Add("@otherIdentification", obj.identification);
                    parameters.Add("@SerialNo", obj.serialNo);
                    parameters.Add("@SpType", obj.spType);
                    parameters.Add("@Userid", obj.userid);
                    parameters.Add("@Qty", obj.qty);
                    parameters.Add("@AssetID", obj.assetID);
                    parameters.Add("@Remarks", obj.remarks);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    parameters.Add("@SeqId", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_PurchaseAddAssets", parameters, commandType: CommandType.StoredProcedure);

                    sqlResponse = parameters.Get<string>("@ResponseMessage");


                    response = Ok(new { msg = sqlResponse });

                    return response;

                }
            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }


    }
}