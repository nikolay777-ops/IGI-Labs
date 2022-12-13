using WEB_0535005_Vashkevich.Extensions;

namespace WEB_0535005_Vashkevich.Extensions
{
    public static class RequestExtension
    {
        public static bool IsAjax(this HttpRequest request) 
        {
            return request.Headers["x-requested-with"].Equals("XMLHttpRequest");
        }
    }
}
