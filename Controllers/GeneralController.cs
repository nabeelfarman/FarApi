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
        // static string dbCon = "Server=tcp:58.27.164.136,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        // static string dbCon = "Server=tcp:125.1.1.244,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        // static string dbCon = "Server=tcp:10.1.1.1,1433;Initial Catalog=FAR;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        // Production Database
        // static string dbCon = "Server=tcp:58.27.164.136,1433;Initial Catalog=FARProd;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        // static string dbCon = "Server=tcp:125.1.1.244,1433;Initial Catalog=FARProd;Persist Security Info=False;User ID=far;Password=telephone@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        dbConfig db = new dbConfig();

        /***** Getting assets detail *****/
        [Route("api/getRegions")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<RegionsList> getRegions(int userId)
        {
            List<RegionsList> rows = new List<RegionsList>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<RegionsList>("select distinct mainLocID, mainLocationDescription from view_userRegions where userid = " + userId + "").ToList();

            }
            return rows;
        }

        [Route("api/getLocations")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<LocationsList> getLocations(int userId)
        {
            List<LocationsList> rows = new List<LocationsList>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<LocationsList>("select mainLocID, subLocID, subLocationDescription from view_userLocationsH where userid = " + userId + "").ToList();

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

            }
            return rows;
        }
    }
}


public class RegionsList
{
    public long mainLocID { get; set; }
    public string mainLocationDescription { get; set; }
}
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