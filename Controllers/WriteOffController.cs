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
    public class WriteOffController : ControllerBase
    {
        dbConfig db = new dbConfig();

        [Route("api/getWriteOffList")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<writeOff> getWriteOffList()
        {
            List<writeOff> rows = new List<writeOff>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<writeOff>("select * from View_WriteOff").ToList();

            }

            return rows;
        }


        [Route("api/getWriteOffDetailList")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetWriteOffDetail> getWriteOffDetailList(int writeOffID)
        {
            List<assetWriteOffDetail> rows = new List<assetWriteOffDetail>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<assetWriteOffDetail>("select * from View_WriteOffDetail where WriteOffID = " + writeOffID + "").ToList();

            }

            return rows;
        }


        [Route("api/getAssetWriteOff")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<assetForWriteOff> getAssetWriteOff(int LocId, string VehicleId)
        {
            List<assetForWriteOff> rows = new List<assetForWriteOff>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (VehicleId == "O")
                {
                    rows = con.Query<assetForWriteOff>("select * from View_AssetList_forWriteOff Where SubLocID = " + LocId + " AND VehID IS NULL").ToList();
                }
                else
                {
                    rows = con.Query<assetForWriteOff>("select * from View_AssetList_forWriteOff Where SubLocID = " + LocId + " AND VehID IS NOT NULL").ToList();
                }



            }

            return rows;
        }


        [Route("api/getEmpDetail")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<empDetail> getEmpDetail(int userID)
        {
            List<empDetail> rows = new List<empDetail>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<empDetail>("select * from View_TempEmployee_WriteOffLossShare Where userid = " + userID + "").ToList();

            }

            return rows;
        }


        [Route("api/saveWriteOff")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult saveWriteOff([FromBody] saveWriteOff obj)
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

                    parameters.Add("@LossDate", obj.LossDate);
                    parameters.Add("@SubLocID", obj.SubLocID);
                    parameters.Add("@DescriptionofLoss", obj.DescriptionofLoss);
                    parameters.Add("@ValueofLoss", obj.ValueofLoss);
                    parameters.Add("@PostID_InquiryConducted", obj.PostID);
                    parameters.Add("@InquiryRemarks", obj.InquiryRemarks);
                    parameters.Add("@InquiryEdoc", obj.InquiryEdoc);
                    parameters.Add("@InquiryEDocExtension", obj.InquiryEDocExtension);
                    parameters.Add("@AuthorityApprovedWriteOff", obj.AuthorityApprovedWriteOff);
                    parameters.Add("@WriteOffAmountApproved", obj.WriteOffAmountApproved);
                    parameters.Add("@ValueofLossToRecover", obj.ValueofLossToRecover);
                    parameters.Add("@ApprovalEdoc", obj.ApprovalEdoc);
                    parameters.Add("@ApprovalEDocExtension", obj.ApprovalEDocExtension);
                    parameters.Add("@ReferenceNo", obj.ReferenceNo);
                    parameters.Add("@ReferenceDate", obj.ReferenceDate);
                    parameters.Add("@Remarks", obj.Remarks);
                    parameters.Add("@Userid", obj.UserId);
                    parameters.Add("@WriteOffID", obj.WriteOffID);
                    parameters.Add("@SpType", obj.SpType);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    parameters.Add("@SeqId", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.SP_WriteOff", parameters, commandType: CommandType.StoredProcedure);
                    sqlResponse = parameters.Get<string>("@ResponseMessage");

                    SeqId = parameters.Get<int>("@SeqId");

                    if (obj.InquiryImgFile != null && sqlResponse.ToUpper() == "SUCCESS")
                    {

                        String path = obj.InquiryEdoc; //Path

                        //Check if directory exist
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                        }

                        string imageName = SeqId + "." + obj.InquiryEDocExtension;

                        //set the image path
                        string imgPath = Path.Combine(path, imageName);

                        byte[] imageBytes = Convert.FromBase64String(obj.InquiryImgFile);

                        System.IO.File.WriteAllBytes(imgPath, imageBytes);
                    }

                    if (obj.ApprovedImgFile != null && sqlResponse.ToUpper() == "SUCCESS")
                    {

                        String path = obj.ApprovalEdoc; //Path

                        //Check if directory exist
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                        }

                        string imageName = SeqId + "." + obj.ApprovalEDocExtension;

                        //set the image path
                        string imgPath = Path.Combine(path, imageName);

                        byte[] imageBytes = Convert.FromBase64String(obj.ApprovedImgFile);

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


        [Route("api/saveWriteOffDetail")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult saveWriteOffDetail([FromBody] writeOffDetail obj)
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

                    parameters.Add("@WriteOffID", obj.WriteOffID);
                    parameters.Add("@AssetID", obj.AssetID);
                    parameters.Add("@ValueofLossApproved", obj.ValofLossApp);
                    parameters.Add("@TotalValueofLossShare", obj.TotValofLossShr);
                    parameters.Add("@ReservePrice", obj.ResPrice);
                    parameters.Add("@Userid", obj.UserId);
                    parameters.Add("@Remarks", obj.Remarks);
                    parameters.Add("@SpType", obj.SpType);
                    parameters.Add("@AssetWriteOffDetail", obj.AssetDetID);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    parameters.Add("@SeqId", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.SP_WriteOffDetail", parameters, commandType: CommandType.StoredProcedure);
                    sqlResponse = parameters.Get<string>("@ResponseMessage");

                    SeqId = parameters.Get<int>("@SeqId");

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


        [Route("api/saveEmployee")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult saveEmployee([FromBody] empDetail obj)
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

                    parameters.Add("@Name", obj.Name);
                    parameters.Add("@PostID", obj.PostID);
                    parameters.Add("@ValueofLossShare", obj.ValueofLossShare);
                    parameters.Add("@Remarks", obj.Remarks);
                    parameters.Add("@Userid", obj.UserId);
                    parameters.Add("@SpType", obj.SpType);
                    parameters.Add("@ID", obj.ID);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    parameters.Add("@SeqId", dbType: DbType.Int32, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.SP_TempEmployee_WriteOffLossShare", parameters, commandType: CommandType.StoredProcedure);
                    sqlResponse = parameters.Get<string>("@ResponseMessage");

                    SeqId = parameters.Get<int>("@SeqId");

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

    }
}
