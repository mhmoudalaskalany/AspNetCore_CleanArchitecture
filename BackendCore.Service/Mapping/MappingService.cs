using AutoMapper;

namespace BackendCore.Service.Mapping
{
    public partial class MappingService : Profile
    {
        public MappingService()
        {
            #region Identity Profiles

            MapUser();
            MapPermission();

            #endregion

            #region Lookups Profiles

            MapAction();
            MapStatus();

            #endregion

            #region Business Profiles

            MapAttachment();

            #endregion

        }
    }
}