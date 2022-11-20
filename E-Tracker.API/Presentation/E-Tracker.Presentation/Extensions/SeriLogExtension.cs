using System.Reflection.PortableExecutable;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog;

namespace E_Tracker.Presentation.Extensions
{
    public static class SeriLogExtension
    {
        public static WebApplicationBuilder AddLogger( this WebApplicationBuilder builder)
        {
            var loger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/logs.txt")
                .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),"logs")
                .CreateLogger();
            builder.Host.UseSerilog(loger);
            return builder;
        }
    }
}
