using VersionOne.SDK.APIClient;

namespace V1ToADOMigration.Core.Entities
{
    public class V1ConnectionDto
    {
        public string ErrorMessage { get; set; }
        public bool IsValid { get; set; }
        public IServices Connection { get; set; }
    }
}
