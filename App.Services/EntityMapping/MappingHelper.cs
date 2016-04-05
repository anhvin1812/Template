using AutoMapper;

namespace App.Services.EntityMapping
{
    public class MappingHelper
    {
        private MappingHelper()
        {
            ConfigureMapping();
        }

        public static void InitializeMapping()
        {
            Instance = new MappingHelper();
        }

        static MappingHelper()
        {
            Instance = new MappingHelper();
        }

        public static MappingHelper Instance { get; set; }

        private static void ConfigureMapping()
        {
            #region Community

            //Mapper.CreateMap<ContactInfo, Dtos.Community.ContactDetail>();
            //Mapper.CreateMap<Dtos.Community.ContactDetail, ContactInfo>();

            #endregion
            // There should be one mapping configuration class per leaf namespace.
            PostMapping.ConfigureMapping();

            #region Network
            //Mapper.CreateMap<NetworkGroup, NetworkGroupDetail>().IgnoreUnmapped();
            //Mapper.CreateMap<NetworkGroup, NetworkGroupSummary>().ForMember(ngs => ngs.Type, ng => ng.MapFrom(opt => opt.NetworkGroupProfile.Type))
            //.IgnoreUnmapped();
            //Mapper.CreateMap<NetworkGroup, NetworkGroupSummaryDto>()
            //    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.GroupName))
            //    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.NetworkGroupId));
            //Mapper.CreateMap<Distribution, DistributionSummary>().IgnoreUnmapped();
            //#endregion

            //#region Message
            //Mapper.CreateMap<MessageTemplateSystem, MessageTemplateSystemDetail>()
            //    .ForMember(dest => dest.NotificationMethod, opt => opt.MapFrom(src => src.DefaultTemplate.NotificationMethod))
            //    .ForMember(dest => dest.TemplateTitle, opt => opt.MapFrom(src => src.DefaultTemplate.TemplateTitle))
            //    .ForMember(dest => dest.TemplateSubject, opt => opt.MapFrom(src => src.TemplateSubject ?? src.DefaultTemplate.TemplateSubject))
            //    .ForMember(dest => dest.TemplateDescription, opt => opt.MapFrom(src => src.DefaultTemplate.TemplateDescription));
            //Mapper.CreateMap<MessageTemplateSystemDefault, MessageTemplateSystemDefaultDetail>()
            //    .IgnoreUnmapped();
            //Mapper.CreateMap<MessageTemplateUser, MessageTemplateUserDetail>()
            //    .IgnoreUnmapped();
            //Mapper.CreateMap<MessageTemplateSystemDetail, MessageTemplateSystem>()
            //    .IgnoreUnmapped();
            //Mapper.CreateMap<MessageTemplateSystemDefaultDetail, MessageTemplateSystemDefault>()
            //    .IgnoreUnmapped();
            //Mapper.CreateMap<MessageTemplateUserDetail, MessageTemplateUser>()
            //    .IgnoreUnmapped();
            //Mapper.CreateMap<MessageInfo, MessageInfoDetail>()
            //    .IgnoreUnmapped();
            //Mapper.CreateMap<MessageInfoDetail, MessageInfo>()
            //    .IgnoreUnmapped();
            //Mapper.CreateMap<ProcessMessageQueue, ProcessMessageQueueResult>()
            //    .IgnoreUnmapped();
            //Mapper.CreateMap<Entities.Messaging.NotificationMemberPreference, Dtos.Messaging.NotificationMemberPreference>()
            //    .IgnoreUnmapped();
            //#endregion

            //#region FileStorage
            //Mapper.CreateMap<FileStore, FileStoreDetail>()
            //    .IgnoreUnmapped();
            //Mapper.CreateMap<FileStore, FileStoreSummary>()
            //    .IgnoreUnmapped();
            //Mapper.CreateMap<FileStoreSave, FileStore>()
            //    .IgnoreUnmapped();
            //Mapper.CreateMap<FileStore, FileUploadUrl>()
            //    .IgnoreUnmapped();
            //FileStoreMapping.ConfigureMapping();
            //#endregion

            //#region Roster

            //Mapper.CreateMap<RosterShift, UpcomingShifts>()
            // .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Roster.RosterNetworkGrp.GroupName))
            // .ForMember(dest => dest.SubLocation, opt => opt.MapFrom(src => src.NetworkGroupLocation.Location))
            // .IgnoreUnmapped();

            //Mapper.CreateMap<RosterShiftUpdate, RosterShift>().IgnoreUnmapped();

            //Mapper.CreateMap<RosterPeriodUpdate, RosterPeriod>().IgnoreUnmapped();
            //Mapper.CreateMap<EmploymentTypeDto, EmploymentType>().IgnoreUnmapped();
            //Mapper.CreateMap<EmploymentType, EmploymentTypeDto>().IgnoreUnmapped();
            //#endregion

            //Mapper.CreateMap<Entities.IdentityManagement.ResumeSearch, Dtos.IdentityManagement.ResumeSearch>();

            //Mapper.CreateMap<OnboardingCandidateSummary, MemberPersonalInfo>();
            //Mapper.CreateMap<OnboardingCandidateSummary, MemberContactSummary>()
            //    .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.MobilePhone))
            //    .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.Country));
            //Mapper.CreateMap<ApplicantDetail, MemberContactSummary>()
            //    .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.MobilePhone))
            //    .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId));

            //Mapper.CreateMap<Sherpa.Entities.IdentityManagement.Member, MemberPersonalInfo>();



            //Mapper.CreateMap<MemberProfile, Dtos.Community.Birthdays>()
            //    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Member.FirstName))
            //    .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Member.Surname))
            //    .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.BirthDay))
            //    .ForMember(dest => dest.NetworkId, opt => opt.MapFrom(src => src.Member.NetworkId))
            //    .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.MemberId));

            ////Mapper.CreateMap<Entities.Community.Event, Dtos.Community.Event>()
            ////    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            ////    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            ////    .ForMember(dest => dest.NetworkId, opt => opt.MapFrom(src => src.NetworkId))
            ////    .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.OwnderId))
            ////    .IgnoreUnmapped();

            //Mapper.CreateMap<AgentTask, AgentTaskDetail>()
            //    .IgnoreUnmapped();

            //Mapper.CreateMap<ActivityItem, ActivityItemDetail>()
            //    .ForMember(x => x.PublishState, x => x.ResolveUsing(y => (int)y.PublishState))
            //    .ForMember(x => x.ActivityType, x => x.ResolveUsing(y => (int)y.ActivityType))
            //    .IgnoreUnmapped();

            //ThirdPartyDataMapping.ConfigureMapping();
            //Mapper.CreateMap<AutoComplete, AutoCompleteExtension>();

            //#region ProfileManagement
            //Mapper.CreateMap<EmploymentType, SalaryTypeSummary>();
            //Mapper.CreateMap<Role, NetworkGroupRoleSummary>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(z => z.Description));

            #endregion

        }
    }
}
