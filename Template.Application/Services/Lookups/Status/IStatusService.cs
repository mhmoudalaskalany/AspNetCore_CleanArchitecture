﻿using System.Threading.Tasks;
using Application.Services.Base;
using Common.Core;
using Common.DTO.Lookup.Status;

namespace Application.Services.Lookups.Status
{
    public interface IStatusService : IBaseService<Domain.Entities.Lookup.Status , AddStatusDto , StatusDto , int , int?>
    {
        Task<IFinalResult> GetStatusesAsync();
    }
}