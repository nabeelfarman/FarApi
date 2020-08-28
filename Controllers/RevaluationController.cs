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
    public class RevaluationController : ControllerBase
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

        [Route("api/getSubLocationforRevaluator")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<subLocReval> getSubLocationforRevaluator()
        {
            List<subLocReval> rows = new List<subLocReval>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<subLocReval>("select * from View_SubLocationforRevaluator").ToList();

            }

            return rows;
        }

        [Route("api/getAccountsCatagoriesforRevaluator")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<subLocReval> getAccountsCatagoriesforRevaluator()
        {
            List<subLocReval> rows = new List<subLocReval>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<subLocReval>("select * from View_accountscatagories").ToList();

            }

            return rows;
        }

        [Route("api/getMoveableAssetListforRevaluation")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<movableAssetReval> getMoveableAssetListforRevaluation(int locID)
        {
            List<movableAssetReval> rows = new List<movableAssetReval>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<movableAssetReval>("select * from View_MoveableAssetListforRevaluation where subLocID=" + locID).ToList();

            }

            return rows;
        }

        [Route("api/sudAssetDetail")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult sudAssetDetail([FromBody] movableAssetReval obj)
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

                    parameters.Add("@AssetID", obj.assetID);
                    parameters.Add("@RevaluationAmount", obj.revaluationAmount);
                    parameters.Add("@Year", obj.year);
                    parameters.Add("@FinYear", obj.finYear);
                    parameters.Add("@UserID", obj.userID);
                    parameters.Add("@SpType", obj.spType);
                    parameters.Add("@AssetDetailid", obj.assetDetailID);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_AssetsDetail", parameters, commandType: CommandType.StoredProcedure);
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