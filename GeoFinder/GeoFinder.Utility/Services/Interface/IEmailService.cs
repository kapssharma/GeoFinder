using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFinder.Utility.Services.Interface
{
    public interface IEmailService
    {
        Task  SendEmailAsync(IConfiguration configuration, string receiverEmail, string body, string Subject);
    }
}
 