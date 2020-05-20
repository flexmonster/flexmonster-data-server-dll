using Flexmonster.DataServer.Core;
using Flexmonster.DataServer.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoDataServerCore.Controllers
{
    [Route("api")]
    [ApiController]
    public class FlexmonsterAPIController : ControllerBase
    {
        private static Dictionary<string, List<object>> _userPermissions = new Dictionary<string, List<object>>()
        {
            {"AAAA", null },
            {"BBBB",  new List<object>(){ "Germany","France" } },
            {"CCCC",  new List<object>(){ "USA","Canada" } },
            {"DDDD", new List<object>(){ "Australia" } },
        };

        private readonly IApiService _apiService;

        public FlexmonsterAPIController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [Route("fields")]
        [HttpPost]
        public async Task<IActionResult> PostFields([FromBody]FieldsRequest request)
        {
            var response = await _apiService.GetFieldsAsync(request);
            return new JsonResult(response);
        }

        [Route("members")]
        [HttpPost]
        public async Task<IActionResult> PostMembers([FromBody]MembersRequest request)
        {
            var response = await _apiService.GetMembersAsync(request, GetServerFilter());
            return new JsonResult(response);
        }

        [Route("select")]
        [HttpPost]
        public async Task<IActionResult> PostSelect([FromBody]SelectRequest request)
        {
            var response = await _apiService.GetAggregatedDataAsync(request, GetServerFilter());
            return new JsonResult(response);
        }

        //server side filter to disable some data for user
        private ServerFilter GetServerFilter()
        {
            HttpContext.Request.Headers.TryGetValue("UserToken", out StringValues userRole);
            if (userRole.Count == 1)
            {
                ServerFilter serverFilter = new ServerFilter();
                serverFilter.Field = new Field() { UniqueName = "Country", Type = ColumnType.stringType };
                serverFilter.Include = _userPermissions[userRole[0]];
                return serverFilter;
            }
            return null;
        }
    }
}