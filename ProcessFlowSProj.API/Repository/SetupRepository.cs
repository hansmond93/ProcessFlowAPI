using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProcessFlowSProj.API.Common;
using ProcessFlowSProj.API.Data;
using ProcessFlowSProj.API.Dtos;
using ProcessFlowSProj.API.Entities;
using ProcessFlowSProj.API.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Repository
{
    public class SetupRepository : ISetupRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenDecryptionHelper _tokenHelper;

        public SetupRepository(DataContext context, IMapper mapper, ITokenDecryptionHelper tokenHelper)
        {
            _context = context;
            _mapper = mapper;
           _tokenHelper = tokenHelper;
        }
        public async Task<bool> AddLevelToSetup(AddLevelForSetupDto addLevel)
        {
            var levelToAdd = new ApprovalLevelEntity();
            //do checks to see if OperationId exists

            var levels = await _context.ApprovalLevelEntities.Where(x => x.OperationId == addLevel.OperationId ).OrderByDescending(x => x.Position).ToArrayAsync();

            if(levels == null)
            {

                levelToAdd.OperationId = addLevel.OperationId;
                levelToAdd.Position = addLevel.Position != 1 ? 1 : addLevel.Position;   //Default Position because setup doesnot exist
                levelToAdd.Active = addLevel.Active;
                levelToAdd.RoleId = addLevel.RoleId;

                await _context.ApprovalLevelEntities.AddAsync(levelToAdd);
                await _context.SaveChangesAsync();

                return true;
            }

            if((levels[0].Position + 1) == addLevel.Position  )  //this means that they wanna add a level jst after the last level
            {

                levelToAdd.OperationId = addLevel.OperationId;
                levelToAdd.Position = addLevel.Position;
                levelToAdd.Active = addLevel.Active;
                levelToAdd.RoleId = addLevel.RoleId;

                await _context.ApprovalLevelEntities.AddAsync(levelToAdd);
                await _context.SaveChangesAsync();

                return true;
            }

            for(int i = 0; i < levels.Length; i++)
            {
                if(addLevel.Position == levels[i].Position)
                {
                    var j = i - 1;
                    int position = levels[j].Position;

                    levelToAdd.OperationId = addLevel.OperationId;
                    levelToAdd.Position = addLevel.Position;
                    levelToAdd.Active = addLevel.Active;
                    levelToAdd.RoleId = addLevel.RoleId;

                    while (i >= 0)
                    {
                        levels[i].Position = position;
                        i--; position++;
                    }

                    break;
                }

            }

            if (levelToAdd == null) throw new CustomException("An Error Ocurred, Please Contatct the System Adminstrator");

            await _context.ApprovalLevelEntities.AddAsync(levelToAdd);

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> RemoveLevelFromSetup(int approvalLevelId, int operationId)
        {
            ApprovalLevelEntity levelToRemove = null;
            bool loopCheck = false;

            var levels = await _context.ApprovalLevelEntities.Where(x => x.OperationId == operationId).OrderByDescending(x => x.Position).ToArrayAsync();

            if(levels == null) throw new CustomException("An Error Ocurred, Please Contatct the System Adminstrator");

            if (levels[0].ApprovalLevelId == approvalLevelId)
            {
                levelToRemove = await _context.ApprovalLevelEntities.FirstOrDefaultAsync(x => x.ApprovalLevelId == approvalLevelId);
                _context.ApprovalLevelEntities.Remove(levelToRemove);

                return await _context.SaveChangesAsync() > 0;
            }

            for(int i = 1; i < levels.Length; i++ ) //we are starting i from 1 cuz we already checked it's not the last guy on the setup
            {
                if(levels[i].ApprovalLevelId == approvalLevelId)
                {
                    var j = i - 1;
                    int position = levels[i].Position;
                    loopCheck = true;

                    while ( i > 0)
                    {
                        levels[j].Position = position;
                        i--; j--; position++;
                    }
                    break;
                }
            }

            if(loopCheck)
            {
                levelToRemove = await _context.ApprovalLevelEntities.FirstOrDefaultAsync(x => x.ApprovalLevelId == approvalLevelId);
                _context.Remove(levelToRemove);

                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                throw new CustomException("An Error Ocurred, Please Contact the System Adminstrator");
            }
        }

        public async Task<bool> EditApprovalLevel(ApprovalLevelForEditDto editLevel)
        {
            //validate operationId
            //validate RoleId also
            var levels = await _context.ApprovalLevelEntities.Where(x => x.OperationId == editLevel.OperationId).OrderByDescending(x => x.Position).ToArrayAsync();

            if (levels == null) throw new CustomException("An Error Ocurred, Please Contatct the System Adminstrator");

            var levelToEdit = await _context.ApprovalLevelEntities.FirstOrDefaultAsync(x => x.ApprovalLevelId == editLevel.ApprovalLevelId);

            if (levelToEdit == null) throw new CustomException("An Error Ocurred, Please Contatct the System Adminstrator");

            if (levelToEdit.Position == editLevel.Position)
            {
                _mapper.Map(editLevel, levelToEdit);    //make sure u can change ApprovalLevel, in the mapping, make sure u ignore that property

                return await _context.SaveChangesAsync() > 0;
            }

            var positionToEdit = levels.FirstOrDefault(x => x.Position == editLevel.Position);

            if(positionToEdit == null) throw new CustomException("An Error Ocurred, Please Contatct the System Adminstrator");

            ApprovalLevelEntity levelToEdit2 = null;

            for (int i = 0; i < levels.Length; i++)
            {
                if(levels[i].ApprovalLevelId == editLevel.ApprovalLevelId)
                {
                    levelToEdit2 = levels[i];
                    positionToEdit.Position = levelToEdit2.Position;
                    _mapper.Map(editLevel, levelToEdit2);
                    
                    break;
                }
                
            }

            if(levelToEdit2 == null) throw new CustomException("An Error Ocurred, Please Contatct the System Adminstrator");

            return await _context.SaveChangesAsync() > 0 ;

        }

        public async Task<IEnumerable<GetDetailedApprovalLevelDto>> GetApprovallevelsByOperationId(int operationId)
        {
            var data = await _context.ApprovalLevelEntities.Where(x => x.OperationId == operationId).OrderBy(x => x.Position).ToListAsync();

            var dataToReturn = _mapper.Map<IEnumerable<GetDetailedApprovalLevelDto>>(data); //if empty, say that nothing has been setup for this operation

            return dataToReturn;
        }

        public async Task<bool> CheckIfOperationExists(int operationId)
        {
            var result = await _context.OperationEntities.AnyAsync(o => o.OperationId == operationId);

            return result;
        }

        public async Task<bool> CheckIfRoleExists(int roleId)
        {
            var result = await _context.StaffRoleEntities.AnyAsync(r => r.RoleId == roleId);

            return result;
        }


    }
}
