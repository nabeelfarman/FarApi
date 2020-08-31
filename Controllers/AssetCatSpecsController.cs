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
    public class AssetCatSpecsController : ControllerBase
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

        [Route("api/getAssetsSpecificationsList")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetSpecList> getAssetsSpecificationsList()
        {
            List<assetSpecList> rows = new List<assetSpecList>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<assetSpecList>("select * from View_AssetsSpecificationsList order by specID desc").ToList();

            }

            return rows;
        }

        [Route("api/getAssetCatagoriesSpecifications")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetSpecList> getAssetCatagoriesSpecifications(int assetCatID)
        {
            List<assetSpecList> rows = new List<assetSpecList>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<assetSpecList>("select * from View_AssetCatagoriesSpecifications where assetCatID=" + assetCatID + " order by specID desc").ToList();

            }

            return rows;
        }

        [Route("api/getAssetCatagoriesSpecificationDATA")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetSpecList> getAssetCatagoriesSpecificationDATA(int assetCatID, int specID)
        {
            List<assetSpecList> rows = new List<assetSpecList>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<assetSpecList>("select * from View_AssetCatagoriesSpecificationDATA where assetCatID=" + assetCatID + " AND specID =" + specID + " order by specID desc").ToList();

            }

            return rows;
        }

        [Route("api/sudAssetCatSpecDetail")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult sudAssetCatSpecDetail([FromBody] assetSpecList obj)
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

                    parameters.Add("@SpecID", obj.specID);
                    parameters.Add("@AssetCatID", obj.assetCatID);
                    parameters.Add("@SpecificationTitle", obj.specificationTitle);
                    parameters.Add("@Type", obj.type);
                    parameters.Add("@Userid", obj.userID);
                    parameters.Add("@SpecDetailID", obj.specDetailID);
                    parameters.Add("@SpType", obj.spType);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_AssetCatagoriesSpecificationDetail", parameters, commandType: CommandType.StoredProcedure);
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

        [Route("api/sudAssetCatSpecData")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult sudAssetCatSpecData([FromBody] assetSpecList obj)
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

                    parameters.Add("@SpecID", obj.specID);
                    parameters.Add("@AssetCatID", obj.assetCatID);
                    parameters.Add("@Title", obj.specificationTitle);
                    parameters.Add("@UserID", obj.userID);
                    parameters.Add("@MakeID", obj.makeID);
                    parameters.Add("@SpType", obj.spType);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_View_AssetCatagoriesSpecificationDATA", parameters, commandType: CommandType.StoredProcedure);
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

    }
}