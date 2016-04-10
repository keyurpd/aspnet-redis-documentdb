using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using StackExchange.Redis;
using Newtonsoft.Json;
using Microsoft.Extensions.OptionsModel;

namespace Keyur.AspNet.Sample
{
    public class AccountController : Controller
    {
        private IOptions<AppSettings> _appSettings;
        private IDocumentDbRepo _data;

        public AccountController(IOptions<AppSettings> appSettings, IDocumentDbRepo data)
        {
            _appSettings = appSettings;
            _data = data;
        }

        private IDatabase _cache;
        private IDatabase Cache
        {
            get
            {
                if (null == _cache)
                {
                    string conn = string.Format("{0}, ssl=true,abortConnect=false, password={1}",
                                    _appSettings.Value.RedisUrl,
                                    _appSettings.Value.RedisKey);
                    ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(conn);
                    _cache = connection.GetDatabase();
                }
                return _cache;
            }
        }

        public IActionResult Index()
        {
            List<Account> accounts;
            string strAccounts;

            //Check cache first		
            strAccounts = Cache.StringGet("accounts2");

            if (null != strAccounts)
            {
                accounts = JsonConvert.DeserializeObject<List<Account>>(strAccounts);
                ViewData["Message"] = "Retrieved from Redis Cache";
            }
            else
            {
                accounts = _data.GetAccounts().ToList();
                strAccounts = JsonConvert.SerializeObject(accounts);
                Cache.StringSet("accounts2", strAccounts, TimeSpan.FromMinutes(1));
                ViewData["Message"] = "Retrieved from DocumentDb";
            }

            return View(accounts);
        }
    }
}