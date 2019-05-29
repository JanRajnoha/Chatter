using System;
using System.Collections.Generic;
using System.Text;
using Chatter.BL.DTO.LogIn;
namespace Chatter.BL.DBAccess.Interfaces
{
    public interface IDBLogIn
    {
        LogInDetailModelDTO AddLog(LogInDetailModelDTO log);
        LogInDetailModelDTO GetLogById(Guid Id);
        List<LogInListModelDTO> GetLogList();
        int GetDeniedRow(Guid id);
        List<LogInDetailModelDTO> GetLogFullList();
        List<LogInDetailModelDTO> GetLogListByUser(Guid Id);
    }
}
