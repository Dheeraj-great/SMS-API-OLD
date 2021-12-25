using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS.TenantApi.Filters
{
    public class SwaggerAuthorizationFilter //: IOperationFilter
    {
        //public void Apply(Operation operation, OperationFilterContext context)
        //{
        //    try
        //    {
        //        var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
        //        var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);
        //        var allowAnonymous = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);
        //        if (!isAuthorized || allowAnonymous) return;
        //        if (operation.Parameters == null)
        //            operation.Parameters = new List<IParameter>();
        //        var noBodyParam = new NonBodyParameter
        //        {
        //            Name = "Authorization",
        //            In = "header",
        //            Description = "access token",
        //            Required = true,
        //            Type = "string"
        //        };
        //        if (operation.OperationId.Contains("ApiTokenPost")) return;
        //        var authParameter = operation.Parameters.Where(x => x.Name == "Authorization");
        //        if (!authParameter.Any())
        //        {
        //            operation.Parameters.Add(noBodyParam);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
