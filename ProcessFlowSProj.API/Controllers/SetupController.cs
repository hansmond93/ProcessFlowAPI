using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessFlowSProj.API.Common;
using ProcessFlowSProj.API.Dtos;
using ProcessFlowSProj.API.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    [Route("api/setup/{operationId}")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly ITokenDecryptionHelper _token;
        private readonly ISetupRepository _setup;
        public SetupController(ITokenDecryptionHelper token, ISetupRepository setup)
        {
            _token = token;
            _setup = setup;
        }

        [HttpGet("getLevel")]
        public async Task<IActionResult> GetApprovalLevelsByOperationId(int operationId)
        {
            try
            {
                int staffId = _token.GetStaffId();  //use this guy for validation and make sure u authorize this controller for admin role only
                var checkValidOperation = await _setup.CheckIfOperationExists(operationId);

                if (!checkValidOperation)
                    return BadRequest("Operation does not exist");

                var levels = await _setup.GetApprovallevelsByOperationId(operationId);
                if (levels == null)
                    return Ok("No Setup for this Operation");

                return Ok(levels);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("addLevel")]
        public async Task<IActionResult> AddLevelToSetup(AddLevelForSetupDto level, int operationId)
        {
            try
            {
                if (operationId != level.OperationId)
                    return BadRequest();

                var checkValidOperation = await _setup.CheckIfOperationExists(level.OperationId);

                if (!checkValidOperation)
                    return BadRequest("Operation does not exist");

                var checkValidRole = await _setup.CheckIfRoleExists(level.RoleId);
                if (!checkValidRole)
                    return BadRequest("Operation does not exist");

                var result = await _setup.AddLevelToSetup(level);
                if (!result)
                    return BadRequest();

                return Ok("Level Added Successfully");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpDelete("removeLevel/{levelId}")]
        public async Task<IActionResult> RemoveLevelFromSetup(int levelId, int operationId)   //change this guy to httpDelete and dont pass the DTO, jst pass approvalLevelId
        {
            try
            {
                var checkValidOperation = await _setup.CheckIfOperationExists(operationId);

                if (!checkValidOperation)
                    return BadRequest("Operation does not exist");

                var result = await _setup.RemoveLevelFromSetup(levelId, operationId);
                if (!result)
                    return BadRequest();

                return Ok("Level Removed Successfully");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("editLevel")]
        public async Task<IActionResult> ModifyLevel(ApprovalLevelForEditDto modifiedLevel, int operationId)
        {
            try
            {
                if (operationId != modifiedLevel.OperationId)
                    return BadRequest();

                var checkValidOperation = await _setup.CheckIfOperationExists(modifiedLevel.OperationId);

                if (!checkValidOperation)
                    return BadRequest("Operation does not exist");

                var checkValidRole = await _setup.CheckIfRoleExists(modifiedLevel.RoleId);

                if (!checkValidRole)
                    return BadRequest("Operation does not exist");

                var result = await _setup.EditApprovalLevel(modifiedLevel);
                if (!result)
                    return BadRequest();

                return Ok("Level modified Successfully");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
