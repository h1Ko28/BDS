using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebAPI.Models.IEntity;
using WebAPI.Services;

namespace WebAPI.Interceptor
{
    public class UpdateTracking : SaveChangesInterceptor
    {
        private readonly TokenUserService _tokenUserService;
        public UpdateTracking(TokenUserService tokenUserService)
        {
            _tokenUserService = tokenUserService;
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var userId = _tokenUserService.GetCurrentUserId();
            var changeTracker = eventData.Context.ChangeTracker;

            var dateList = changeTracker.Entries<IUpdateTracking>()
            .Where(x => x.State == EntityState.Modified).ToList();
            foreach(var entityEntry in dateList){
                entityEntry.Property(x => x.LastUpdatedOn).CurrentValue = DateTime.Now;
                entityEntry.Property(x => x.LastUpdatedBy).CurrentValue = int.Parse(userId);
            }
            return base.SavingChanges(eventData, result);
        }
    }
}