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
        static string dbCon = "Server=tcp:58.27.164.136,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        // static string dbCon = "Server=tcp:125.1.1.244,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        /***** Getting Sub Locations *****/
        [Route("api/getLocationCheckList")]
        [HttpGet]
        [EnableCors("CorePolicy")]

        public IEnumerable<subLocationCheckList> getLocationCheckList(int subLocID, int officeTypeID)
        {
            List<subLocationCheckList> rows = new List<subLocationCheckList>();

            using (IDbConnection con = new SqlConnection(dbCon))
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

                using (IDbConnection con = new SqlConnection(dbCon))
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

    }
}