using StartupProject_Asp.NetCore_PostGRE.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject_Asp.NetCore_PostGRE.Data.Models.AppData
{
    public class LeaveApplication : BaseModel
    {
        [Column("ApplicantId"), Display(Name = "Applicant Id", Prompt = "Please Choose Applicant Id")]
        public long ApplicantId { get; set; } = -1;
        [Column("ApplicantName"), Required(ErrorMessage = "Applicant Name should be given"), MinLength(3), MaxLength(32767), Display(Name = "Applicant Name", Prompt = "Please Give Applicant Name")]
        public string Name { get; set; }
        [Column("Designation"), Required(ErrorMessage = "Designation should be given"), MinLength(3), MaxLength(32767), Display(Name = "Designation", Prompt = "Please Give Designation")]
        public string Designation { get; set; }

        [Column("LeaveStart"), Required(ErrorMessage = "Leave start day should be given"), Display(Name = "Leave Start Day", Prompt = "Please Give Leave Start Day")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime LeaveStart { get; set; }
        [Column("LeaveEnd"), Required(ErrorMessage = "Leave end day should be given"), Display(Name = "Leave End Day", Prompt = "Please Give Leave End Day")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime LeaveEnd { get; set; }
        [Column("LeaveType"), Required(ErrorMessage = "Leave Type should be chosen"), Display(Name = "Leave Type", Prompt = "Please Choose Leave Type")]
        public ELeaveType LeaveType { get; set; }
        [Column("PurposeOfLeave"), Required(ErrorMessage = "Purpose Of Leave should be given"), MinLength(10), MaxLength(32767), Display(Name = "Purpose Of Leave", Prompt = "Please Write Purpose Of Leave")]
        public string PurposeOfLeave { get; set; }
        //(For Annual and Special)
        [Column("AddressDuringLeave"), Required(ErrorMessage = "Address During Leave should be given"), MinLength(10), MaxLength(32767), Display(Name = "Address During Leave", Prompt = "Please Give Address During Leave")]
        public string AddressDuringLeave { get; set; }
        [Column("PhoneNoDuringLeave"), Required(ErrorMessage = "Phone No During Leave should be given"), MinLength(11), MaxLength(11), Display(Name = "Phone No During Leave", Prompt = "Please Give Phone No During Leave")]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [Phone]
        public string PhoneNoDuringLeave { get; set; }
        [Column("ApplicationStatus"), Display(Name = "Application Status", Prompt = "Please Choose Application Status")]
        public EApplicationStatus ApplicationStatus { get; set; } = EApplicationStatus.Processing;

        [Column("LastSignedId"), Display(Name = "Last Signed/Unsigned File", Prompt = "Please select Previous File")]
        public long? LastSignedId { get; set; }
        //No Abstract object can be a element in this class because it is going to be serialized
        [ForeignKey("LastSignedId"), Display(Name = "Previous Signed/Unsigned File", Prompt = "Please Select Previous File")]
        public virtual XmlFile PreviousSignedFile { get; set; }
    }
}
