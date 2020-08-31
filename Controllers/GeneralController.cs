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
    public class GeneralController : ControllerBase
    {
        /**/
        /*** DB Connection ***/
        // static string dbCon = "Server=tcp:95.217.206.195,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=sa;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";        
        // static string dbCon = "Server=tcp:95.217.206.195,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=sa;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        // live server
        static string dbCon = "Server=tcp:58.27.164.136,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        // static string dbCon = "Server=tcp:125.1.1.244,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        // static string dbCon = "Server=tcp:10.1.1.1,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        // Production Database
        // static string dbCon = "Server=tcp:58.27.164.136,1433;Initial Catalog=FARProd;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        // static string dbCon = "Server=tcp:125.1.1.244,1433;Initial Catalog=FARProd;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        dbConfig db = new dbConfig();

        /***** Getting assets detail *****/
        [Route("api/getAssetDetailBooks")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<RegionsList> getRegions(int userId)
        {
            List<RegionsList> rows = new List<RegionsList>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                if (userId == 0)
                {
                    rows = con.Query<RegionsList>("select mainLocID, mainLocationDescription from MainLocations").ToList();
                }
                else
                {
                    rows = con.Query<RegionsList>("select mainLocID, mainLocationDescription from MainLocations").ToList();
                }


<<<<<<< HEAD
=======
            return rows;
        }

        /***** Getting assets detail *****/
        [Route("api/getAssetDetailComputers")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetDetail> getAssetDetailComputers(long UserId, long SubLocID, long OfficeTypeID)
        {
            List<assetDetail> rows = new List<assetDetail>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (UserId != 0 && SubLocID == 0 && OfficeTypeID == 0)
                {
                    rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + " and accountsCatID= 1 order by AssetID desc ").ToList();
                }
                else
                {
                    rows = con.Query<assetDetail>("select * from View_MoveableAssetsListforTagForm WHERE Userid= " + UserId + " AND SubLocID= " + SubLocID + " AND OfficeTypeID= " + OfficeTypeID + " and accountsCatID= 1 order by AssetID desc ").ToList();
                }
>>>>>>> 7ab02ca3bf52f19578f6793a23ea5a86ab052c89
            }
            return rows;
        }

        [Route("api/getLocations")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<LocationsList> getLocations()
        {
            List<LocationsList> rows = new List<LocationsList>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<LocationsList>("select mainLocID, subLocID, subLocationDescription from SubLocations").ToList();

            }
            return rows;
        }

        [Route("api/getOfficeTypes")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<OfficeTypeList> getOfficeTypes()
        {
            List<OfficeTypeList> rows = new List<OfficeTypeList>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<OfficeTypeList>("select officeTypeID, officeTypeDescription from OfficeTypes").ToList();

<<<<<<< HEAD
=======
            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                rows = con.Query<subLocationCheckList>("select * from View_SubLocationsCheckList where subLocID = " + subLocID + " and officeTypeID = " + officeTypeID + "").ToList();
>>>>>>> 7ab02ca3bf52f19578f6793a23ea5a86ab052c89
            }
            return rows;
        }
    }
}

<<<<<<< HEAD
public class RegionsList
{
    public long mainLocID { get; set; }
    public string mainLocationDescription { get; set; }
}
=======
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
>>>>>>> 7ab02ca3bf52f19578f6793a23ea5a86ab052c89

public class LocationsList
{
    public long mainLocID { get; set; }
    public long subLocID { get; set; }
    public string subLocationDescription { get; set; }
}

public class OfficeTypeList
{
    public long officeTypeID { get; set; }
    public string officeTypeDescription { get; set; }
}