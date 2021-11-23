using Bogus;
using Dapper;
using Glimpse.AspNet.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DataSet = System.Data.DataSet;


namespace DapperAdvanced.Models
{
    public class BikesRepository
    {

        private string connectionstring;

        public BikesRepository()
        {
            connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        }

        public List<Bike> GetAll(RequestModel request)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                var results = db
                    .Query<Bike>("Bikes_GetsPage",
                    request,
                    commandType: CommandType.StoredProcedure);
                int totalRecords = results.Count();
                if (request.PageSize == -1) request.PageSize = totalRecords;
                return results.ToList();
            }
        }
        public Bike Get(int Id)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db
                        .Query<Bike>("Bikes_Get",
                            new { Id },
                            commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();
            }
        }
        public int Create(Bike bike)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                int lastInsertedId =
                    db.Query<int>("Bikes_Create",
                    bike,
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
                return lastInsertedId;
            }
        }
        public int Update(Bike bike)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db
                    .Execute("Bikes_Update",
                       bike,
                       commandType: CommandType.StoredProcedure);
            }
        }
        public int Delete(int Id)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db.Execute(
                        "Bikes_Delete",
                        new { Id },
                        commandType: CommandType.StoredProcedure);
            }
        }
       
    }
  
}
