using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Helpers
{
    public partial class Startup
    {
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly Dictionary<string, List<Exception>> _exceptions;
        public IConfigurationRoot Configuration { get; }
    }
}
