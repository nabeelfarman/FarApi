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
    public class AssetTransferController : ControllerBase
    {
        dbConfig db = new dbConfig();

        [Route("api/saveAssetTransfer")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult saveAssetTransfer([FromBody] AssetTransferCreation obj)
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

                    parameters.Add("@TSubLocID", obj.TSubLocID);
                    parameters.Add("@TOfficeTypeID", obj.TOfficeTypeID);
                    parameters.Add("@TOfficeSecID", obj.TOfficeSecID);
                    parameters.Add("@TPostID", obj.TPostID);
                    parameters.Add("@RSubLocID", obj.RSubLocID);
                    parameters.Add("@ROfficeTypeID", obj.ROfficeTypeID);
                    parameters.Add("@ROfficeSecID", obj.ROfficeSecID);
                    parameters.Add("@RPostID", obj.RPostID);
                    parameters.Add("@DateofTransfer", obj.DateofTransfer);
                    parameters.Add("@TransferType", obj.TransferType);
                    parameters.Add("@TransferDescription", obj.TransferDescription);
                    parameters.Add("@EDoc", obj.EDoc);
                    parameters.Add("@EDocExtension", obj.EDocExtension);
                    parameters.Add("@TransferID", obj.TransferID);
                    parameters.Add("@ProjectID", obj.ProjectID);
                    parameters.Add("@Userid", obj.Userid);
                    parameters.Add("@SpType", obj.SpType);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.Sp_AssetTransfer", parameters, commandType: CommandType.StoredProcedure);
                    sqlResponse = parameters.Get<string>("@ResponseMessage");

                    SeqId = parameters.Get<int>("@SeqId");

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


        [Route("api/saveAssetTransferDetail")]
        [HttpPost]
        [EnableCors("CorePolicy")]
        public IActionResult saveAssetTransferDetail([FromBody] AssetTransferDetailCreation obj)
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

                    parameters.Add("@AssetID", obj.AssetID);
                    parameters.Add("@TransferId", obj.TransferId);
                    parameters.Add("@userId", obj.userId);
                    parameters.Add("@SpType", obj.SpType);
                    parameters.Add("@ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    rowAffected = con.Execute("dbo.sp_AssetTransferDetail", parameters, commandType: CommandType.StoredProcedure);
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