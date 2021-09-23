using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseApp.Service.Basket.Services.Concrete
{
    public class RedisService
    {
        private readonly string _host;

        private readonly int _port;

        private ConnectionMultiplexer _connectionMultiplexer;

        public RedisService(string host, int port)
        {

            _host = host;
            _port = port;
        }

        public void Connect()
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
        }

        //we have more than one db on redis but i choosed 1 if we want we can use more than one db for production ,test  or development ect.
        public IDatabase GetDb(int db = 1)
        {
           return _connectionMultiplexer.GetDatabase(db);
        }
    }
}
