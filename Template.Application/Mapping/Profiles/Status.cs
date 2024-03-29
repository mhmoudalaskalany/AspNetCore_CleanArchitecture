﻿using Template.Common.DTO.Lookup.Status;
using Template.Domain.Entities.Lookup;


// ReSharper disable once CheckNamespace
namespace Template.Application.Mapping
{
    public partial class MappingService
    {
        public void MapStatus()
        {
            CreateMap<Status, AddStatusDto>()
                .ReverseMap();

            CreateMap<Status, StatusDto>()
                .ReverseMap();

            CreateMap<Status, EditStatusDto>()
                .ReverseMap();
        }
    }
}