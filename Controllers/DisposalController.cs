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
    public class DisposalController : ControllerBase
    {

        dbConfig db = new dbConfig();
        /////////////////Disposal Form
        [Route("api/assetdisposallist")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetDisposal> getAssetDisposalList(int UserId)
        {
            List<assetDisposal> rows = new List<assetDisposal>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<assetDisposal>("select * from View_AssetsDisposal Where UserID = " + UserId + "").ToList();

            }

            return rows;
        }



        [Route("api/assetfordisposallist")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetsForDisposal> getAssetForDisposalList(int LocId, string VehicleId)
        {
            List<assetsForDisposal> rows = new List<assetsForDisposal>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (VehicleId == "O")
                {
                    rows = con.Query<assetsForDisposal>("select * from View_AssetsforDisposal Where SubLocID = " + LocId + " AND VehID IS NULL").ToList();
                }
                else
                {
                    rows = con.Query<assetsForDisposal>("select * from View_AssetsforDisposal Where SubLocID = " + LocId + " AND VehID IS NOT NULL").ToList();
                }



            }

            return rows;
        }




        [Route("api/disposaldetail")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetsForDisposal> getDisposalDetail(int DisposalID)
        {
            List<assetsForDisposal> rows = new List<assetsForDisposal>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<assetsForDisposal>("select * from View_AssetsDisposalDetail Where DisposalID = " + DisposalID + "").ToList();

            }

            return rows;
        }




        [Route("api/suddisposal")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult sudDisposal([FromBody] sudDisposal obj)
        {
            //
            //***** Try Block
            try
            {
                //****** Declaration
                int rowAffected = 0;
                string sqlResponse = "";
                IActionResult response = Unauthorized();

                int SeqId = 0;

                using (IDbConnection con = new SqlConnection(db.dbCon))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@DisposalID", obj.DisposalID);
                    parameters.Add("@DisposalDate", obj.DisposalDate);
                    parameters.Add("@SubLocID", obj.SubLocID);
                    parameters.Add("@PurchaserName", obj.PurchaserName);
                    parameters.Add("@PartyCode", obj.PartyCode);
                    parameters.Add("@PurchaseAddress", obj.PurchaseAddress);
                    parameters.Add("@PurchaserNTN", obj.PurchaserNTN);
                    parameters.Add("@PurchaserCNIC", obj.PurchaserCNIC);
                    parameters.Add("@AmountPaid", obj.AmountPaid);
                    parameters.Add("@TaxAmount", obj.TaxAmount);
                    parameters.Add("@Remarks", obj.Remarks);
                    parameters.Add("@Edoc", obj.Edoc);
                    parameters.Add("@EDocExtension", obj.EDocExtension);
                    parameters.Add("@UserId", obj.UserId);
                    parameters.Add("@SpType", obj.SpType);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    parameters.Add("@SeqId", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.SP_Disposal", parameters, commandType: CommandType.StoredProcedure);
                    sqlResponse = parameters.Get<string>("@ResponseMessage");

                    SeqId = parameters.Get<int>("@SeqId");

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

                response = Ok(new { msg = sqlResponse, id = SeqId });

                return response;

            }
            //***** Exception Block
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
        }



        [Route("api/suddisposaldetail")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult sudDisposalDetail([FromBody] sudDisposalDetail obj)
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

                    parameters.Add("@DisposalPaymentID", obj.DisposalPaymentID);
                    parameters.Add("@DisposalID", obj.DisposalID);
                    parameters.Add("@AssetID", obj.AssetID);
                    parameters.Add("@Bidamount", obj.DisposalValue);
                    parameters.Add("@ReservePrice", obj.ReservePrice);
                    parameters.Add("@CurrentMarketvalue", obj.CurrentMarketvalue);
                    parameters.Add("@NetbookAmount", obj.BookValue);
                    parameters.Add("@GainLoss", obj.GainLoss);
                    parameters.Add("@DisposalRemarks", obj.Remarks);
                    parameters.Add("@Edoc", obj.Edoc);
                    parameters.Add("@EDocExtension", obj.EDocExtension);
                    parameters.Add("@UserId", obj.UserId);
                    parameters.Add("@SpType", obj.SpType);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    parameters.Add("@SeqId", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.SP_DisposalDetail", parameters, commandType: CommandType.StoredProcedure);
                    sqlResponse = parameters.Get<string>("@ResponseMessage");

                    if (obj.imgFile != null && sqlResponse.ToUpper() == "SUCCESS")
                    {
                        int SeqId = parameters.Get<int>("@SeqId");

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
    }
}