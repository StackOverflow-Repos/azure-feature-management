using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using System.Threading.Tasks;

namespace TestingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureManager _featureManager;

        public FeatureController(IFeatureManagerSnapshot featureManager)
        {
            _featureManager = featureManager;
        }

        [HttpGet]
        [Route("name")]
        public async Task<IActionResult> GetByName(string featureName)
        {
            var res = await _featureManager.IsEnabledAsync(featureName);
            return Ok(res);
        }
    }
}
