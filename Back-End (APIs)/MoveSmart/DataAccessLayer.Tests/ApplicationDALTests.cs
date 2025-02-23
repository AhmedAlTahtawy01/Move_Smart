using DataAccessLayer.Repositories;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DataAccessLayer.Tests
{
    public class ApplicationDALTests
    {
        private readonly ApplicationDAL _applicationDAL;

        public ApplicationDALTests()
        {
            _applicationDAL = new ApplicationDAL();
        }

        
    }
}
