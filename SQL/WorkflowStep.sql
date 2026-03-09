/*
 Navicat Premium Dump SQL

 Source Server         : 127.0.0.1
 Source Server Type    : SQL Server
 Source Server Version : 16001000 (16.00.1000)
 Source Host           : 127.0.0.1:1433
 Source Catalog        : SystemAdmin
 Source Schema         : Form

 Target Server Type    : SQL Server
 Target Server Version : 16001000 (16.00.1000)
 File Encoding         : 65001

 Date: 09/03/2026 13:04:28
*/


-- ----------------------------
-- Table structure for WorkflowStep
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[WorkflowStep]') AND type IN ('U'))
	DROP TABLE [Form].[WorkflowStep]
GO

CREATE TABLE [Form].[WorkflowStep] (
  [StepId] bigint  NOT NULL,
  [FormTypeId] bigint  NOT NULL,
  [StepNameCn] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [StepNameEn] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [IsStartStep] int  NOT NULL,
  [ArchitectureLevel] nvarchar(15) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Assignment] nvarchar(15) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ApproveMode] nvarchar(15) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [IsReminderEnabled] int  NOT NULL,
  [ReminderIntervalMinutes] int  NULL,
  [Description] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Form].[WorkflowStep] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批步骤Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'StepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单类型Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'FormTypeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批步骤名称（中文）',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'StepNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批步骤名称（英文）',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'StepNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否为开始步骤',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'IsStartStep'
GO

EXEC sp_addextendedproperty
'MS_Description', N'架构级别（组织架构、执行级）',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'ArchitectureLevel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批人选取方式（依部门人员组织架构、指定员工、自定义）',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'Assignment'
GO

EXEC sp_addextendedproperty
'MS_Description', N'签核方式（单签、会签）',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'ApproveMode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否催签',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'IsReminderEnabled'
GO

EXEC sp_addextendedproperty
'MS_Description', N'催签间隔分钟',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'ReminderIntervalMinutes'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤描述',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'流程步骤表',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep'
GO


-- ----------------------------
-- Records of WorkflowStep
-- ----------------------------
INSERT INTO [Form].[WorkflowStep] ([StepId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [ArchitectureLevel], [Assignment], [ApproveMode], [IsReminderEnabled], [ReminderIntervalMinutes], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009890853346217984', N'1987217256446300160', N'发起人', N'Applicant', N'1', N'OrgLevel', N'Org', N'Single', N'0', N'1', N'', N'1903486709602062336', N'2026-01-10 15:31:41.000', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [ArchitectureLevel], [Assignment], [ApproveMode], [IsReminderEnabled], [ReminderIntervalMinutes], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009892923604340736', N'1987217256446300160', N'执 - 组长', N'Operational- Team leader', N'0', N'OrgLevel', N'Org', N'Single', N'1', N'1', N'', N'1903486709602062336', N'2026-01-10 15:39:49.000', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [ArchitectureLevel], [Assignment], [ApproveMode], [IsReminderEnabled], [ReminderIntervalMinutes], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009897830268932096', N'1987217256446300160', N'科 - 科长', N'Section - Section Chief', N'0', N'OrgLevel', N'Org', N'Single', N'1', N'1', N'', N'1903486709602062336', N'2026-01-10 15:59:19.000', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [ArchitectureLevel], [Assignment], [ApproveMode], [IsReminderEnabled], [ReminderIntervalMinutes], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009898117243211776', N'1987217256446300160', N'部 - 经理签核', N'Department - Manager', N'0', N'OrgLevel', N'Org', N'Single', N'1', N'1', N'', N'1903486709602062336', N'2026-01-10 16:00:27.000', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [ArchitectureLevel], [Assignment], [ApproveMode], [IsReminderEnabled], [ReminderIntervalMinutes], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2029389483455156224', N'1987217256446300160', N'部 - 资深经理签核', N'Department - Senior Manager', N'0', N'OrgLevel', N'Org', N'Single', N'1', N'1', N'', N'1903486709602062336', N'2026-03-05 10:52:11.747', NULL, NULL)
GO


-- ----------------------------
-- Primary Key structure for table WorkflowStep
-- ----------------------------
ALTER TABLE [Form].[WorkflowStep] ADD CONSTRAINT [PK__FormAppr__243433573D582151] PRIMARY KEY CLUSTERED ([StepId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

