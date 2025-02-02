using System.Net;
using System;

namespace uDesktopMascot.Web.Domain.Interfaces
{
    public interface IRequestHandler
    {
        bool CanHandle(HttpListenerRequest request);
    }
}