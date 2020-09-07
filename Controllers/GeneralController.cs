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

                rows = con.Query<RegionsList>("select distinct mainLocID, mainLocationDescription from view_userRegions where userid = " + userId + " order by mainLocationDescription").ToList();

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

                rows = con.Query<LocationsList>("select mainLocID, subLocID, subLocationDescription, officeTypeId, officeTypeDescription from view_userLocations where userid = " + userId + " order by subLocationDescription, officeTypeDescription").ToList();

            }
            return rows;
        }

        [Route("api/getAccountCategories")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<AccountsCatList> getAccountCategories()
        {
            List<AccountsCatList> rows = new List<AccountsCatList>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<AccountsCatList>("select distinct accountsCatID, accountsCatagory from view_AssetCatagories order by accountsCatagory").ToList();

            }
            return rows;
        }

        [Route("api/getAssetCategories")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<AssetCatList> getAssetCategories()
        {
            List<AssetCatList> rows = new List<AssetCatList>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                rows = con.Query<AssetCatList>("select accountsCatID, assetCatID, assetCatDescription from view_AssetCatagories order by assetCatDescription").ToList();

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
    public long officeTypeId { get; set; }
    public string officeTypeDescription { get; set; }
}

public class OfficeTypeList
{
    public long officeTypeID { get; set; }
    public string officeTypeDescription { get; set; }
}

public class AccountsCatList
{
    public int accountsCatID { get; set; }
    public string accountsCatagory { get; set; }
}

public class AssetCatList
{
    public long accountsCatID { get; set; }
    public long assetCatID { get; set; }
    public string assetCatDescription { get; set; }
}