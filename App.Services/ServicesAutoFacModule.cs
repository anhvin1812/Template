using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositories;
using App.Services.IdentityManagement;
using App.Services.ProductManagement;
using Autofac;

namespace App.Services
{
    public class ServicesAutoFacModule : Module
    {
        public ServicesAutoFacModule(bool supportInstancePerRequest = true)
        {
            SupportInstancePerRequest = supportInstancePerRequest;
        }

        private bool SupportInstancePerRequest { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerDependency();
            builder.RegisterType<SecurityService>().As<ISecurityService>().InstancePerDependency();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerDependency();

            //builder.RegisterType<RedCatMemberFactory>().As<IThirdPartyProviderMemberFactory<RedCatMember>>().InstancePerLifetimeScope();
            //builder.RegisterType<Talent2MemberFactory>().As<IThirdPartyProviderMemberFactory<Talent2Member>>().InstancePerLifetimeScope();
            //builder.RegisterType<KeyPayMemberFactory>().As<IThirdPartyProviderMemberFactory<KeyPayMember>>().InstancePerLifetimeScope();
            //builder.RegisterType<SherpaPayrollMemberFactory>().As<IThirdPartyProviderMemberFactory<SherpaPayrollMember>>().InstancePerLifetimeScope();
            //builder.RegisterType<ThirdPartyMemberPayrollClient<RedCatMember>>().Keyed<IThirdPartySyncService>(ThirdPartyType.RedCat).InstancePerLifetimeScope();
            //builder.RegisterType<ThirdPartyMemberPayrollClient<RedCatMember>>().Keyed<IPayRollRepository>(ThirdPartyType.RedCat).InstancePerLifetimeScope();
            //builder.RegisterType<ThirdPartyMemberPayrollClient<Talent2Member>>().Keyed<IThirdPartySyncService>(ThirdPartyType.Talent2).InstancePerLifetimeScope();
            //builder.RegisterType<ThirdPartyMemberPayrollClient<Talent2Member>>().Keyed<IPayRollRepository>(ThirdPartyType.Talent2).InstancePerLifetimeScope();
            //builder.RegisterType<ThirdPartyMemberPayrollClient<SherpaPayrollMember>>().Keyed<IThirdPartySyncService>(ThirdPartyType.Sherpa).InstancePerLifetimeScope();
            //builder.RegisterType<ThirdPartyMemberPayrollClient<SherpaPayrollMember>>().Keyed<IPayRollRepository>(ThirdPartyType.Sherpa).InstancePerLifetimeScope();
            //builder.RegisterType<KeyPayClient>().Keyed<IThirdPartySyncService>(ThirdPartyType.KeyPay).InstancePerLifetimeScope();
            //builder.RegisterType<KeyPayClient>().Keyed<IPayRollRepository>(ThirdPartyType.KeyPay).InstancePerLifetimeScope();

            //builder.RegisterType<LearningSeatRepository>().Keyed<IThirdPartySyncService>(ThirdPartyType.LearningSeat).InstancePerLifetimeScope();
            //builder.RegisterType<LearningSeatRepository>().Keyed<ILearningRepository>(ThirdPartyType.LearningSeat).InstancePerLifetimeScope();

            //builder.RegisterType<TrainingService>().As<ITrainingService>().InstancePerDependency();
            //builder.RegisterType<ThirdPartyProviderService>().As<IThirdPartyProviderService>().InstancePerLifetimeScope();
            //builder.RegisterType<FileStorageService>().As<IFileStorageService>().InstancePerDependency();
            //builder.RegisterType<PresentationService>().As<IPresentationService>().InstancePerDependency();
            //builder.RegisterType<ActivityService>().As<IActivityService>().InstancePerLifetimeScope();
            //builder.RegisterType<BlogService>().As<IBlogService>().InstancePerDependency();
            //builder.RegisterType<NetworkService>().As<INetworkService>().InstancePerDependency();
            //builder.RegisterType<SecurityService>().As<ISecurityService>().InstancePerDependency();
            //builder.RegisterType<RosterService>().As<IRosterService>().InstancePerDependency();
            //builder.RegisterType<LocationService>().As<ILocationService>().InstancePerDependency();
            //builder.RegisterType<MemberService>().As<IMemberService>().InstancePerDependency();
            //builder.RegisterType<NetworkGroupService>().As<INetworkGroupService>().InstancePerDependency();
            //builder.RegisterType<NotificationService>().As<INotificationService>().InstancePerDependency();
            //builder.RegisterType<NotificationReceiveService>().As<INotificationReceiveService>().InstancePerDependency();
            //builder.RegisterType<MessageService>().As<IMessageService>().InstancePerDependency();
            //builder.RegisterType<MessageQueueService>().As<IMessageQueueService>().InstancePerDependency();
            //builder.RegisterType<ProfileManagementService>().As<IProfileManagementService>().InstancePerDependency();
            //builder.RegisterType<JobService>().As<IJobService>().InstancePerDependency();
            //builder.RegisterType<JobTemplateService>().As<IJobTemplateService>().InstancePerDependency();
            //builder.RegisterType<JobApplicationService>().As<IJobApplicationService>().InstancePerLifetimeScope();
            //// builder.RegisterType<JobTemplateService>().As<IJobTemplateService>().InstancePerLifetimeScope();
            //builder.RegisterType<JobPostService>().As<IJobPostService>().InstancePerLifetimeScope();
            //builder.RegisterType<JobAdvertisementService>().As<IJobAdvertisementService>().InstancePerLifetimeScope();
            //builder.RegisterType<PerformanceAssessmentService>().As<IPerformanceAssessmentService>().InstancePerDependency();
            //builder.RegisterType<WorkflowService>().As<IWorkflowService>().InstancePerDependency();
            //builder.RegisterType<RecruimentService>().As<IRecruimentService>().InstancePerDependency();
            //builder.RegisterType<DocumentService>().As<IDocumentService>().InstancePerDependency();
            //builder.RegisterType<PerformanceAssessmentService>().As<IPerformanceAssessmentService>().InstancePerDependency();
            //builder.RegisterType<ProfileContentService>().As<IProfileContentService>().InstancePerDependency();
            //builder.RegisterType<ContainerService>().As<IContainerService>().InstancePerDependency();
            //builder.RegisterType<CandidateService>().As<ICandidateService>().InstancePerDependency();
            //builder.RegisterType<ResolverService>().As<IResolverService>().InstancePerDependency();
            //builder.RegisterType<JobAdService>().As<IJobAdService>().InstancePerDependency();
            //builder.RegisterType<JobTypeService>().As<IJobTypeService>().InstancePerDependency();

            //builder.RegisterType<FileContentEncryption>().Named<IEncryption>("FileContentEncryption").InstancePerLifetimeScope();
            //builder.RegisterType<EmailFileContentEncryption>().Named<IEncryption>("EmailFileContentEncryption").InstancePerLifetimeScope();

            //builder.RegisterType<EmailClient>().As<IEmailClient>().InstancePerLifetimeScope();

            //builder.RegisterGeneric(typeof(CsvFtpPersistence<>)).Keyed(ThirdPartyDataPersistenceType.CsvFtp, typeof(IIntegrationPersistence<>))
            //    .Keyed(ThirdPartyDataPersistenceType.CsvEmail, typeof(IIntegrationPersistence<>))
            //    .WithParameter(ResolvedParameter.ForNamed<IEncryption>("FileContentEncryption"))
            //    .InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(CsvSFtpPersistence<>)).Keyed(ThirdPartyDataPersistenceType.CsvSftp, typeof(IIntegrationPersistence<>)).InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(CsvEmailPersistence<>))
            //    .Keyed(ThirdPartyDataPersistenceType.CsvEmail, typeof(IIntegrationPersistence<>))
            //    .WithParameter(ResolvedParameter.ForNamed<IEncryption>("EmailFileContentEncryption"))
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<PayFrequencyService>().As<IPayFrequencyService>().InstancePerLifetimeScope();
            //builder.RegisterType<TaxCodeService>().As<ITaxCodeService>().InstancePerLifetimeScope();
            //builder.RegisterType<LearningService>().As<ILearningService>().InstancePerLifetimeScope();
            //builder.RegisterType<DESEncryption>().As<IEncryption>().InstancePerLifetimeScope();
            //builder.RegisterType<DESEncryption>().Keyed<IEncryption>(EncryptionType.DES).InstancePerLifetimeScope();
            //builder.RegisterType<EventService>().As<IEventService>().InstancePerLifetimeScope();
            //builder.RegisterType<GoogleApiService>().As<IGoogleApiService>().InstancePerLifetimeScope();
            //builder.RegisterType<AgentService>().As<IAgentService>().InstancePerLifetimeScope();
            //builder.RegisterType<Smtp>().As<IEmailProvider>().InstancePerLifetimeScope();
            //builder.RegisterType<DirectDb>().As<IInboxProvider>().InstancePerLifetimeScope();
            //builder.RegisterType<EmailChannelService>().As<IEmailChannelService>().InstancePerLifetimeScope();
            //builder.RegisterType<SmsChannelService>().As<ISmsChannelService>().InstancePerLifetimeScope();
            //builder.RegisterType<InboxChannelService>().As<IInboxChannelService>().InstancePerLifetimeScope();
            //builder.RegisterType<ResolverService>().As<IResolverService>().InstancePerLifetimeScope();
            //builder.RegisterType<JobBoardIntegrationService>().As<IJobBoardIntegrationService>().InstancePerLifetimeScope();
            //builder.RegisterType<JobBoardIntegrationFactory>().As<IJobBoardIntegrationFactory>().InstancePerLifetimeScope();
            //builder.RegisterType<JobBoardIntegrationBase>().As<IJobBoardIntegration>().InstancePerLifetimeScope();
            //builder.RegisterType<OneShiftIntegration>().As<IJobBoardIntegration>().InstancePerLifetimeScope();
            //builder.RegisterType<JobBoardIntegrationDataProvider>().As<IJobBoardIntegrationDataProvider>().WithParameter("baseApiUrl", ConfigurationManager.AppSettings["WebApiBaseUrl"]).InstancePerDependency();

            //builder.RegisterType<RosterShiftConfirmationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_ROSTER_SHIFT_CONFIRMED_BY_EMPLOYEE).InstancePerLifetimeScope();
            //builder.RegisterType<RosterShiftRejectionHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_ROSTER_SHIFT_REJECTED_BY_EMPLOYEE).InstancePerLifetimeScope();
            //builder.RegisterType<MemberStatusLikeHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_MEMBER_THUMBS_UP).InstancePerLifetimeScope();
            //builder.RegisterType<MemberStatusCommentHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_MEMBER_STATUS_COMMENT).InstancePerLifetimeScope();
            //builder.RegisterType<MemberPostCommentHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_MEMBER_POST_COMMENT).InstancePerLifetimeScope();
            //builder.RegisterType<MemberNewsPostCommentHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_MEMBER_NEWS_COMMENT).InstancePerLifetimeScope();
            //builder.RegisterType<MemberActivityCommentHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_MEMBER_ACTIVITY_COMMENT).InstancePerLifetimeScope();
            //builder.RegisterType<GroupThumbsUpHandler>().Keyed<IMessageHandler>
            //    (NotificationType.NOTIFICATION_GROUP_THUMBS_UP).InstancePerLifetimeScope();
            //builder.RegisterType<GroupStatusUpdateHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_GROUP_UPDATESTATUS).InstancePerLifetimeScope();
            //builder.RegisterType<GroupStatusCommentHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_GROUP_STATUS_COMMENT).InstancePerLifetimeScope();
            //builder.RegisterType<GroupActivityCommentHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_GROUP_ACTIVITY_COMMENT).InstancePerLifetimeScope();
            //builder.RegisterType<ReferAFriendToAJobHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_REFER_A_FRIEND_TO_A_JOB).InstancePerLifetimeScope();
            //builder.RegisterType<ReferAFriendToNetworkHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_REFER_A_FRIEND_TO_THE_NETWORK).InstancePerLifetimeScope();
            //builder.RegisterType<GroupNewsPostHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_GROUP_NEWSPOSTED).InstancePerLifetimeScope();
            //builder.RegisterType<GroupPrifilePostHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_GROUP_PROFILEPOST).InstancePerLifetimeScope();
            //builder.RegisterType<NotifyApplicantHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_APPLY_JOB_APPLICATION).InstancePerLifetimeScope();
            //builder.RegisterType<JobSeekerInquiryHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_JOBSEEKER_ENQUIRY).InstancePerLifetimeScope();
            //builder.RegisterType<JobSeekerInquiryConfirmHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_JOB_SEEKER_CONFIRM).InstancePerLifetimeScope();
            //builder.RegisterType<NotifyJobApplyToAdminHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_APPLY_JOB_APPLICATION_ADMIN).InstancePerLifetimeScope();
            //builder.RegisterType<LibraryService>().As<ILibraryService>().InstancePerDependency();
            //builder.RegisterType<OnboardingService>().As<IOnboardingService>().InstancePerLifetimeScope();
            //builder.RegisterType<OnboardingReadService>().As<IOnboardingReadService>().InstancePerLifetimeScope();
            //builder.RegisterType<PayRatesService>().As<IPayRatesService>().InstancePerLifetimeScope();
            //builder.RegisterType<CsvDataReader<OnboardingCandidateSummary>>().As<ICsvDataReader<OnboardingCandidateSummary>>().InstancePerLifetimeScope();
            //builder.RegisterType<CsvDataReader<MemberCsvEntry>>().As<ICsvDataReader<MemberCsvEntry>>().InstancePerLifetimeScope();

            //builder.RegisterType<XmlDataReader>().As<IXmlDataReader>().InstancePerLifetimeScope();
            //builder.RegisterType<OnboardingService.FileHelper>().As<OnboardingService.IFileWrapper>().InstancePerLifetimeScope();

            //builder.RegisterType<GroupPayrollInfoChangeHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_GROUP_PROFILE_BANKINGINFOCHANGE).InstancePerLifetimeScope();
            //builder.RegisterType<GroupBasicProfileInfoChangeHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_GROUP_PROFILECHANGE).InstancePerLifetimeScope();
            //builder.RegisterType<GroupEmploymentInfoOnboardChangeHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_GROUP_PROFILE_EMPLOYCHANGEONBOARD).InstancePerLifetimeScope();
            //builder.RegisterType<GroupEmploymentInfoChangeHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_GROUP_PROFILE_EMPLOYCHANGEPROMOTION).InstancePerLifetimeScope();
            //builder.RegisterType<MemberEmploymentInfoChangeHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_MEMBER_ONBAORD_REQUEST).InstancePerLifetimeScope();
            //builder.RegisterType<WorkflowDelegationStartHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_WORKFLOW_DELEGATION_STARTED).InstancePerLifetimeScope();
            //builder.RegisterType<WorkflowDelegationStoppedHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_WORKFLOW_DELEGATION_STOPPED).InstancePerLifetimeScope();

            //builder.RegisterType<EventInviteHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_MEMBER_EVENTINVITE).InstancePerLifetimeScope();
            //builder.RegisterType<EventReplyHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_MEMBER_EVENTREPLY).InstancePerLifetimeScope();
            //builder.RegisterType<EventReminderHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_MEMBER_EVENT_RSVP_REMINDER).InstancePerLifetimeScope();
            //builder.RegisterType<EmailRequestService>().As<IEmailRequestService>().InstancePerLifetimeScope();

            //#region Survey
            //builder.RegisterType<SurveyService>().As<ISurveyService>().InstancePerLifetimeScope();
            //builder.RegisterType<SurveyServicePermission>().As<ISurveyServicePermission>().InstancePerLifetimeScope();
            //#endregion

            //builder.RegisterType<ProfilesService>().As<IProfilesService>().InstancePerDependency();

            //builder.RegisterType<CreateNotificationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_APPROVER_AFTER_ONBOARD_CREATED).InstancePerLifetimeScope();
            //builder.RegisterType<RejectNotificationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_CREATOR_WHEN_APPROVER_REJECT_ONBOARD).InstancePerLifetimeScope();
            //builder.RegisterType<ApprovalNotificationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_CREATOR_WHEN_APPROVER_APPROVED_ONBOARD).InstancePerLifetimeScope();
            //builder.RegisterType<LaunchNotiHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_CANDIDATE_WHEN_APPROVER_APPROVED).InstancePerLifetimeScope();
            //builder.RegisterType<LaunchNotiHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_CANDIDATE_WHEN_ONBOARD_ACTIVED).InstancePerLifetimeScope();
            //builder.RegisterType<CompleteNotificationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_CONFIRMER_WHEN_ONBOARD_NOT_AUTO_ACTIVED).InstancePerLifetimeScope();
            //builder.RegisterType<AcceptedNotificationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_CREATOR_AND_MANAGER_WHEN_ONBOARD_AUTO_ACTIVED).InstancePerLifetimeScope();
            //builder.RegisterType<ApprovalNotificationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_APPROVER_WHEN_ONBOARD_BE_ROLLBACK).InstancePerLifetimeScope();
            //builder.RegisterType<RollbackToCadidateNotiHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_CANDIDATE_WHEN_ONBOARD_BE_ROLLBACK).InstancePerLifetimeScope();
            //builder.RegisterType<LaunchNotiHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_RESEND_EMAIL_TO_CANDDIATE).InstancePerLifetimeScope();
            //builder.RegisterType<ApprovalNotificationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_CREATOR_AND_APPROVER_WHEN_CONFIRMER_REJECT).InstancePerLifetimeScope();
            //builder.RegisterType<ApprovalNotificationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_CREATOR_AND_APPROVER_WHEN_CONFIRMER_CONFIRMED).InstancePerLifetimeScope();
            //builder.RegisterType<RejectToCadidateNotiHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_CANDIDATE_WHEN_CONFIRMER_REJECT).InstancePerLifetimeScope();
            //builder.RegisterType<ConfirmToCadidateNotiHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_CANDIATE_WHEN_CONFIRMER_CONFIRMED).InstancePerLifetimeScope();

            //builder.RegisterType<CreateNotificationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_APPROVAL_AFTER_BULKONBOARD_CREATED).InstancePerLifetimeScope();
            //builder.RegisterType<RejectNotificationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_CREATOR_WHEN_APPROVAL_REJECT_BULKONBOARD).InstancePerLifetimeScope();
            //builder.RegisterType<ApprovalNotificationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_CREATOR_WHEN_APPROVAL_APPROVED_BULKONBOARD).InstancePerLifetimeScope();
            //builder.RegisterType<OfferMadeJobApplicationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_GROUP_JOBAPPLICANTWRITTENOFFERSENT).InstancePerLifetimeScope();

            //builder.RegisterType<JobAdRequisistionService>().As<IJobAdRequisitionService>().InstancePerDependency();
            //builder.RegisterType<Logger>().As<ILogger>().InstancePerDependency();
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            //#region Merge Content
            //builder.RegisterType<ContentTemplateService>().As<IContentTemplateService>().InstancePerDependency();
            //builder.RegisterType<BusinessObjectService>().As<IBusinessObjectService>().InstancePerDependency();
            //builder.RegisterType<BusinessPropertyService>().As<IBusinessPropertyService>().InstancePerDependency();
            //builder.RegisterType<TemplateTypeService>().As<ITemplateTypeService>().InstancePerDependency();
            //builder.RegisterType<MergeContentService>().As<IMergeContentService>().InstancePerDependency();
            //builder.RegisterType<FeatureService>().As<IFeatureService>().InstancePerDependency();
            //#endregion


            //builder.RegisterType<RejectByCandidateNotificationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_CREATOR_AND_MANAGER_WHEN_CANDIDATE_REJECT_ONBOARD).InstancePerLifetimeScope();
            //builder.RegisterType<AcceptedNotificationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_TO_CREATOR_AND_MANAGER_WHEN_BULKONBOARD_AUTO_ACTIVED).InstancePerLifetimeScope();
            //builder.RegisterType<RackSpaceFileTransfer>().As<IFileTransfer>();
            //builder.RegisterType<RackSpaceFileTransfer>().Keyed<IFileTransfer>(FileTransferInstance.RackSpace);
            //builder.RegisterType<AzureFileTransfer>().Keyed<IFileTransfer>(FileTransferInstance.Azure);

            //builder.RegisterType<GroupJobApplicationHandler>().Keyed<IMessageHandler>(NotificationType.NOTIFICATION_GROUP_JOBAPPLICATION).InstancePerLifetimeScope();

            builder.RegisterModule(new RepositoryAutofacModule());
        }
    }
}
