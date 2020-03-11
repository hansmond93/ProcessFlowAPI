using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessFlowSProj.API.Dtos;
using ProcessFlowSProj.API.Interface;

namespace ProcessFlowSProj.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IWorkFlow _workFlow;
        public TestController(IWorkFlow workFlow)
        {
            _workFlow = workFlow;
        }

        //api/test/goforapproval
        [HttpPost("goForApproval", Name ="GetCArs") ]
        public IActionResult TestGoForApproval(TestGoForApprovalDto testApprovalDto)
        {
            if (testApprovalDto == null)
                return BadRequest();

            //var result = _workFlow.GoForApproval(testApprovalDto.OperationId, testApprovalDto.TargetId, testApprovalDto.ToStaffId, testApprovalDto.FromStaffId);
            
            return Ok();
        }


    }
}