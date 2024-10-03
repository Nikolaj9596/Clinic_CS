using AutoMapper;

namespace Clinic.Models.Mappings.DTO.DiagnosisDto
{

    public class GetDiagnosisListDto
    {
        public IList<GetDiagnosisDto> Diagnosis { get; set; }
    }
}
