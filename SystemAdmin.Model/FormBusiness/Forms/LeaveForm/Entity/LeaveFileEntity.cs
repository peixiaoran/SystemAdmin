using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace SystemAdmin.Model.FormBusiness.Forms.LeaveForm.Entity
{
    /// <summary>
    /// 请假表文件表
    /// </summary>
    [SugarTable("[Form].[LeaveFile]")]
    public class LeaveFileEntity
    {
        /// <summary>
        /// 表单Id
        /// </summary>
        public long FormId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 文件相对路径
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public long? ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
