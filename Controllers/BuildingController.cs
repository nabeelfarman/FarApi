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
    public class BuildingController : ControllerBase
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

        [Route("api/getBuildingDetail")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<buildDetail> getBuildingDetail()
        {
            List<buildDetail> rows = new List<buildDetail>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<buildDetail>("select * from View_BuildingData1").ToList();

            }

            return rows;
        }

        [Route("api/sudBuilding")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult sudBuilding([FromBody] sudRoad obj)
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

                    parameters.Add("@AccountsCatID", obj.AccountsCatID);
                    parameters.Add("@OfficeSecID", obj.OfficeSecID);
                    parameters.Add("@ProjectID", obj.ProjectID);
                    parameters.Add("@RoadId", null);
                    parameters.Add("@BuildingID", obj.BuildingID);
                    parameters.Add("@PackageName", null);
                    parameters.Add("@DateofNationalization", obj.DateofNationalization);
                    parameters.Add("@PurposeofPurchase", null);
                    parameters.Add("@PresentUse", null);
                    parameters.Add("@ConstructionFrom", obj.ConstructionFrom);
                    parameters.Add("@ConstructionTo", obj.ConstructionTo);
                    parameters.Add("@ConstructionCost", obj.ConstructionCost);
                    parameters.Add("@LandMeasureTypeID", obj.LandMeasureTypeID);
                    parameters.Add("@AreaAcquiredKanal", obj.AreaAcquiredKanal);
                    parameters.Add("@AreaAcquiredMarla", obj.AreaAcquiredMarla);
                    parameters.Add("@AreaTransferedKanal", obj.AreaTransferedKanal);
                    parameters.Add("@AreaTransferedMarla", obj.AreaTransferedMarla);
                    parameters.Add("@CostOfLand", obj.CostOfLand);
                    parameters.Add("@Remarks", obj.Remarks);
                    parameters.Add("@FixedAssetID", obj.FixedAssetID);
                    parameters.Add("@UserId", obj.UserId);
                    parameters.Add("@SpType", obj.SpType);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_FixedAssets", parameters, commandType: CommandType.StoredProcedure);
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