using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DBContext
    {
        //private ShootingCompetitionRequestsEntities _db;

        //public DBContext()
        //{
        //    _db = new ShootingCompetitionRequestsEntities();
        //}

        //private static string _conString = @"Data Source=DENIS-ПК\SQLEXPRESS;Initial Catalog=ShootingCompetitionRequests;Integrated Security=True";

        public static ShootingCompetitionRequestsEntities GetContext()
        {
            var dbContext = new ShootingCompetitionRequestsEntities();
            //dbContext.Database.Connection.ConnectionString = @"Data Source=DENIS-ПК\SQLEXPRESS;Initial Catalog=ShootingCompetitionRequests;Integrated Security=True";
            return dbContext;
        }
    }
}
