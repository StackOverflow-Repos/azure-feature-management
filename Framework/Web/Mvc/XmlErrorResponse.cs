using System.Collections.Generic;

namespace Framework.Web
{
    public class XmlErrorResponse
    {
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
