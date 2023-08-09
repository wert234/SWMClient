using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SWMClient.Models
{
    public class SWMHttpClientHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
