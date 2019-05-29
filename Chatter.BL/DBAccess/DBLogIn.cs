using System;
using System.Collections.Generic;
using System.Text;
using Chatter.BL.DTO.LogIn;
using Chatter.DAL.Data;
using Chatter.DAL.Model;
using Chatter.BL.DBAccess.Interfaces;
using System.Linq;
using Chatter.Common.Enum;
using Chatter.DAL.Factories;

namespace Chatter.BL.DBAccess
{
    public class DBLogIn : IDBLogIn
    {
        private readonly IDBContextFactory chatterDbContextFactory;

        public DBLogIn(IDBContextFactory chatterDbContextFactory)
        {
             this.chatterDbContextFactory = chatterDbContextFactory;
        }
        readonly Mapper mapper = new Mapper();

        public LogInDetailModelDTO AddLog(LogInDetailModelDTO log)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var newLog = mapper.MapDetailToEntity(log);

                newLog.Id = Guid.NewGuid();
                newLog.SignInLogTime = DateTime.Now;

                connection.SignInLogs.Add(newLog);
                connection.SaveChanges();
                return mapper.MapEntityToDetailModel(newLog);
            }
        }

        public LogInDetailModelDTO GetLogById(Guid Id)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                var log_id = connection.SignInLogs.FirstOrDefault(x => x.Id == Id);
                return mapper.MapEntityToDetailModel(log_id);
            }
        }

        public int GetDeniedRow(Guid id)
        {
            var res = GetLogListByUser(id);
            var ress = res.OrderByDescending(x => x.SignInLogTime);
            var resss = ress.Take(3);
            var ee = resss.Where(x => x.SignLogCode == SignLogCode.AccessDenied);
            var tt = ee.Count();

            return GetLogListByUser(id).OrderByDescending(x => x.SignInLogTime).Take(3).Where(x => x.SignLogCode == SignLogCode.AccessDenied).Count();
        }

        public List<LogInListModelDTO> GetLogList()
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return connection.SignInLogs.Select(mapper.MapEntityToListModel).ToList();
            }
        }

        public List<LogInDetailModelDTO> GetLogFullList()
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return connection.SignInLogs.Select(mapper.MapEntityToDetailModel).ToList();
            }
        }

        public List<LogInDetailModelDTO> GetLogListByUser(Guid Id)
        {
            using (var connection = chatterDbContextFactory.CreateDbContext())
            {
                return connection.SignInLogs.Where(x => x.UserId == Id).Select(mapper.MapEntityToDetailModel).ToList();
            }
        }
    }
}
