using StartupProject_Asp.NetCore_PostGRE.Data.Enums;
using StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace StartupProject_Asp.NetCore_PostGRE.Data.Models.AppData
{
    public class XmlFile : BaseModel
    {
        public XmlFile() { }
        public XmlFile(string xmlString, string fileName, long dbEntryId, ETableName tableName)
        {
            FileContent = xmlString;
            DbEntryId = dbEntryId;
            FileRealName = fileName;
            TableName = tableName;
            //DownloadUploadTokens = new List<DownloadUploadToken>();
        }

        [IgnoreDataMember]
        [ConcurrencyCheck]  //2 file should not be same
        [Column("FileContent", TypeName = "text"), Required(ErrorMessage = "File Content should be given"), MinLength(5), Display(Name = "File Content", Prompt = "Please Give File Content")]
        public string FileContent { get; set; }
        [Column("FileRealName"), Required(ErrorMessage = "Real Name should be given"), MinLength(5), MaxLength(32767), Display(Name = "File Real Name", Prompt = "Please Give File Real Name")]
        public string FileRealName { get; set; }

        public ETableName TableName { get; set; } = ETableName.XmlFile;
        public long DbEntryId { get; set; }
        public bool IsAlreadyUsed { get; set; } = false;

        [IgnoreDataMember]
        [Column("SignerId"), Display(Name = "Signer Id", Prompt = "Please Give Signer Id")]
        public Guid? SignerId { get; set; }
        [ForeignKey("SignerId"), Display(Name = "Previous Signed/Unsigned File", Prompt = "Please Select Previous File")]
        public virtual User Signer { get; set; }
        [Column("PreviousFileId"), Display(Name = "Previous Signed/Unsigned File", Prompt = "Please select Previous File")]
        public Guid? PreviousFileId { get; set; }
        [ForeignKey("PreviousFileId"), Display(Name = "Previous Signed/Unsigned File", Prompt = "Please Select Previous File")]
        public virtual XmlFile PreviousSignedFile { get; set; }

        //public ICollection<DownloadUploadToken> DownloadUploadTokens { get; set; }
    }
}
