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
    }
}