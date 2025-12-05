
namespace Building_MinimalAPIsMoviesApp.Filters
{
    public class TestFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            //this is the code that will execute Before the endpoint
            var results = await next(context);
            //this is the code that will execute After the endpoint

            return results;
        }
    }
}
