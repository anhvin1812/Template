using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace App.Services.EntityMapping
{
    internal static class PostMapping
    {
        public static void ConfigureMapping()
        {
            //Mapper.CreateMap<CustomForm, CustomFormSummaryDto>()
            //    .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Title))
            //    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.CustomFormId));

            //Mapper.CreateMap<CustomFormSummaryDto, CustomForm>()
            //    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Label))
            //    .ForMember(dest => dest.CustomFormId, opt => opt.MapFrom(src => src.Value));

            //Mapper.CreateMap<EmploymentClassification, EmploymentClassificationSummaryDto>()
            // .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Description))
            // .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.EmploymentClassificationId));
            //Mapper.CreateMap<EmploymentClassificationSummaryDto, EmploymentClassification>()
            // .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Label))
            // .ForMember(dest => dest.EmploymentClassificationId, opt => opt.MapFrom(src => src.Value));

            //Mapper.CreateMap<LocationLookUp, LocationLookUpDetail>()
            //    .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
            //Mapper.CreateMap<LocationLookUpDetail, LocationLookUp>();

            //Mapper.CreateMap<CustomForm, CustomFormDetail>();
            //Mapper.CreateMap<CustomForm, CustomFormSummary>();


            //Mapper.CreateMap<PerformanceAssessment, PerformanceAssessmentDetail>();
            //Mapper.CreateMap<PerformanceAssessment, PerformanceAssessmentSummary>()
            //    .ForMember(dest => dest.CustomFormSummary, opt => opt.MapFrom(src => src.CustomForm))
            //    .ForMember(dest => dest.StatusLabel, opt => opt.MapFrom(src => ((PerformanceAssessmentStatus)src.Status).ToString()));

            //Mapper.CreateMap<OnboardDocument, OnboardDocumentDetail>();
            //Mapper.CreateMap<OnboardDocumentDetail, OnboardDocument>();

            //Mapper.CreateMap<MemberPaperDoc, MemberPaperDocDetail>()
            //    .ForMember(dest => dest.PaperDocTypeLabel, opt => opt.MapFrom(src => src.PaperDocType.Description));
            //Mapper.CreateMap<MemberPaperDocDetail, MemberPaperDoc>();
            //Mapper.CreateMap<PaperDocType, PaperDocTypeSummary>();
        }
    }
}
