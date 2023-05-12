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
    public class DepreciationChargedController : ControllerBase
    {
        dbConfig db = new dbConfig();
    
        [Route("api/getDepreciationCharged")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<DepreciationCharged> getDepreciationCharged(int projectID,string finYear)
        {
            List<DepreciationCharged> rows = new List<DepreciationCharged>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                if (projectID == 0)
                {
                    rows = con.Query<DepreciationCharged>("select * from view_DepreciationCharged_ProjectWiseSummary_2000 where FinYear = '" + finYear + "' ").ToList();
                }
                else
                {
                    rows = con.Query<DepreciationCharged>("select * from view_DepreciationCharged_ProjectWiseSummary_2000 where FinYear = '" + finYear + "' and ProjectId = " + projectID + " ").ToList();
                }
                

            }

            return rows;
        }


        [Route("api/getDepreciationChargedDetail")]
        [HttpGet]
        [EnableCors("CorePolicy")]
        public IEnumerable<DepreciationChargedDetail> getDepreciationChargedDetail(int projectID,string finYear,int AccountsCatId)
        {
            List<DepreciationChargedDetail> rows = new List<DepreciationChargedDetail>();

            using (IDbConnection con = new SqlConnection(db.dbCon))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                
                
                rows = con.Query<DepreciationChargedDetail>("select * from view_depreciationCharged_ProjectWisedetail_2001 where FinYear = '" + finYear + "' and ProjectId = " + projectID + " and AccountsCatId = " + AccountsCatId + " ").ToList();
                
                

            }

            return rows;
        }

    }
}