using AutoMapper;
using rpglms_backend.src.models;
using rpglms_backend.DTOs;

namespace rpglms
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUserDto, AppUser>();
            CreateMap<Chapter, ChapterDto>();
            CreateMap<ChapterDto, Chapter>();
            CreateMap<Chronicle, ChronicleDto>();
            CreateMap<ChronicleDto, Chronicle>();
            CreateMap<Enrollment, EnrollmentDto>();
            CreateMap<EnrollmentDto, Enrollment>();
            CreateMap<Performance, PerformanceDto>();
            CreateMap<PerformanceDto, Performance>();
            CreateMap<PerformanceAttempt, PerformanceAttemptDto>();
            CreateMap<PerformanceAttemptDto, PerformanceAttempt>();
            CreateMap<PerformanceRecord, PerformanceRecordDto>();
            CreateMap<PerformanceRecordDto, PerformanceRecord>();
            CreateMap<Practice, PracticeDto>();
            CreateMap<PracticeDto, Practice>();
            CreateMap<Quest, QuestDto>();
            CreateMap<QuestDto, Quest>();
            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionDto, Question>();
            CreateMap<Reflection, ReflectionDto>();
            CreateMap<ReflectionDto, Reflection>();
            CreateMap<ReflectionForm, ReflectionFormDto>();
            CreateMap<ReflectionFormDto, ReflectionForm>();
            CreateMap<ReflectionResponse, ReflectionResponseDto>();
            CreateMap<ReflectionResponseDto, ReflectionResponse>();
        }
    }
}
