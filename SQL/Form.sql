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

 Date: 07/04/2026 18:29:16
*/


-- ----------------------------
-- Table structure for ControlInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[ControlInfo]') AND type IN ('U'))
	DROP TABLE [Form].[ControlInfo]
GO

CREATE TABLE [Form].[ControlInfo] (
  [ControlCode] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ControlName] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Description] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime  NULL
)
GO

ALTER TABLE [Form].[ControlInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'控件类型编码',
'SCHEMA', N'Form',
'TABLE', N'ControlInfo',
'COLUMN', N'ControlCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'控件类型名称',
'SCHEMA', N'Form',
'TABLE', N'ControlInfo',
'COLUMN', N'ControlName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'控件类型描述',
'SCHEMA', N'Form',
'TABLE', N'ControlInfo',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'ControlInfo',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'ControlInfo',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'ControlInfo',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'ControlInfo',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'控件信息表',
'SCHEMA', N'Form',
'TABLE', N'ControlInfo'
GO


-- ----------------------------
-- Records of ControlInfo
-- ----------------------------
INSERT INTO [Form].[ControlInfo] ([ControlCode], [ControlName], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'el-button', N'el-button', N'按钮', N'1903486709602062336', N'2025-11-05 16:02:49.000', NULL, NULL)
GO

INSERT INTO [Form].[ControlInfo] ([ControlCode], [ControlName], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'el-checkbox', N'el-checkbox', N'单个多选框，返回true、false', N'1903486709602062336', N'2025-11-05 13:26:29.000', NULL, NULL)
GO

INSERT INTO [Form].[ControlInfo] ([ControlCode], [ControlName], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'el-checkbox-group', N'el-checkbox-group', N'组多选框，返回数组', N'1903486709602062336', N'2025-11-05 13:27:29.000', NULL, NULL)
GO

INSERT INTO [Form].[ControlInfo] ([ControlCode], [ControlName], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'el-date-picker', N'el-date-picker', N'日期选择框', N'1903486709602062336', N'2025-11-05 13:30:56.000', NULL, NULL)
GO

INSERT INTO [Form].[ControlInfo] ([ControlCode], [ControlName], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'el-input', N'el-input', N'输入框', N'1903486709602062336', N'2025-11-04 16:33:36.000', NULL, NULL)
GO

INSERT INTO [Form].[ControlInfo] ([ControlCode], [ControlName], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'el-input-number', N'el-input-number', N'数值输入框', N'1903486709602062336', N'2025-11-04 16:33:38.000', NULL, NULL)
GO

INSERT INTO [Form].[ControlInfo] ([ControlCode], [ControlName], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'el-radio-group', N'el-radio-group', N'组单选框', N'1903486709602062336', N'2025-11-05 13:26:17.000', NULL, NULL)
GO

INSERT INTO [Form].[ControlInfo] ([ControlCode], [ControlName], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'el-select', N'el-select', N'下拉框', N'1903486709602062336', N'2025-11-05 11:58:47.000', NULL, NULL)
GO

INSERT INTO [Form].[ControlInfo] ([ControlCode], [ControlName], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'el-switch', N'el-switch', N'开关', N'1903486709602062336', N'2025-11-09 00:13:35.000', NULL, NULL)
GO


-- ----------------------------
-- Table structure for FormAttachment
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[FormAttachment]') AND type IN ('U'))
	DROP TABLE [Form].[FormAttachment]
GO

CREATE TABLE [Form].[FormAttachment] (
  [AttachmentId] bigint  NOT NULL,
  [FormId] bigint  NOT NULL,
  [AttachmentName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [AttachmentPath] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [AttachmentSize] int  NOT NULL,
  [CreatedBy] bigint  NULL,
  [CreatedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Form].[FormAttachment] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'附件Id',
'SCHEMA', N'Form',
'TABLE', N'FormAttachment',
'COLUMN', N'AttachmentId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单Id',
'SCHEMA', N'Form',
'TABLE', N'FormAttachment',
'COLUMN', N'FormId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'附件文件名',
'SCHEMA', N'Form',
'TABLE', N'FormAttachment',
'COLUMN', N'AttachmentName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'附件相对路径',
'SCHEMA', N'Form',
'TABLE', N'FormAttachment',
'COLUMN', N'AttachmentPath'
GO

EXEC sp_addextendedproperty
'MS_Description', N'附件大小（kb）',
'SCHEMA', N'Form',
'TABLE', N'FormAttachment',
'COLUMN', N'AttachmentSize'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'FormAttachment',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'FormAttachment',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单附件表',
'SCHEMA', N'Form',
'TABLE', N'FormAttachment'
GO


-- ----------------------------
-- Records of FormAttachment
-- ----------------------------
INSERT INTO [Form].[FormAttachment] ([AttachmentId], [FormId], [AttachmentName], [AttachmentPath], [AttachmentSize], [CreatedBy], [CreatedDate]) VALUES (N'2035202614592933888', N'2035198908409450496', N'新建 Microsoft Excel 工作表.xlsx', N'/20260321/20260321115130210_5bb1825d.xlsx', N'6', N'1903486709602062336', N'2026-03-21 11:51:30.213')
GO

INSERT INTO [Form].[FormAttachment] ([AttachmentId], [FormId], [AttachmentName], [AttachmentPath], [AttachmentSize], [CreatedBy], [CreatedDate]) VALUES (N'2035204154561007616', N'2035203421577023488', N'217735420260115094308932 (1).docx', N'/20260321/20260321115737351_e21f2351.docx', N'375', N'1903486709602062336', N'2026-03-21 11:57:37.370')
GO

INSERT INTO [Form].[FormAttachment] ([AttachmentId], [FormId], [AttachmentName], [AttachmentPath], [AttachmentSize], [CreatedBy], [CreatedDate]) VALUES (N'2035204154590367744', N'2035203421577023488', N'新建 Microsoft Excel 工作表.xlsx', N'/20260321/20260321115737372_3e8bf46b.xlsx', N'6', N'1903486709602062336', N'2026-03-21 11:57:37.377')
GO

INSERT INTO [Form].[FormAttachment] ([AttachmentId], [FormId], [AttachmentName], [AttachmentPath], [AttachmentSize], [CreatedBy], [CreatedDate]) VALUES (N'2035215659046146048', N'2035212726183268352', N'FOUpdate20260311114210.xls', N'/20260321/20260321124320219_b74999c3.xls', N'181', N'1903486709602062336', N'2026-03-21 12:43:20.253')
GO

INSERT INTO [Form].[FormAttachment] ([AttachmentId], [FormId], [AttachmentName], [AttachmentPath], [AttachmentSize], [CreatedBy], [CreatedDate]) VALUES (N'2035215660669341696', N'2035212726183268352', N'PO FAS-PC0015  updated.pdf', N'/20260321/20260321124320634_9d5577ae.pdf', N'80', N'1903486709602062336', N'2026-03-21 12:43:20.640')
GO

INSERT INTO [Form].[FormAttachment] ([AttachmentId], [FormId], [AttachmentName], [AttachmentPath], [AttachmentSize], [CreatedBy], [CreatedDate]) VALUES (N'2035215660702896128', N'2035212726183268352', N'说明.docx', N'/20260321/20260321124320642_073ccff2.docx', N'375', N'1903486709602062336', N'2026-03-21 12:43:20.647')
GO

INSERT INTO [Form].[FormAttachment] ([AttachmentId], [FormId], [AttachmentName], [AttachmentPath], [AttachmentSize], [CreatedBy], [CreatedDate]) VALUES (N'2035456066451542016', N'2035455708547387392', N'PO FAS-PC0015  updated.pdf', N'/20260322/20260322043837840_10e97abc.pdf', N'80', N'1903486709602062336', N'2026-03-22 04:38:37.843')
GO

INSERT INTO [Form].[FormAttachment] ([AttachmentId], [FormId], [AttachmentName], [AttachmentPath], [AttachmentSize], [CreatedBy], [CreatedDate]) VALUES (N'2035456066489290752', N'2035455708547387392', N'说明.docx', N'/20260322/20260322043837846_49b596b8.docx', N'375', N'1903486709602062336', N'2026-03-22 04:38:37.853')
GO


-- ----------------------------
-- Table structure for FormGroup
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[FormGroup]') AND type IN ('U'))
	DROP TABLE [Form].[FormGroup]
GO

CREATE TABLE [Form].[FormGroup] (
  [FormGroupId] bigint  NOT NULL,
  [FormGroupNameCn] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [FormGroupNameEn] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [SortOrder] int  NOT NULL,
  [DescriptionCn] nvarchar(300) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [DescriptionEn] nvarchar(500) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime  NULL
)
GO

ALTER TABLE [Form].[FormGroup] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单组别Id',
'SCHEMA', N'Form',
'TABLE', N'FormGroup',
'COLUMN', N'FormGroupId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单组别名称（中文）',
'SCHEMA', N'Form',
'TABLE', N'FormGroup',
'COLUMN', N'FormGroupNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单组别名称（英文）',
'SCHEMA', N'Form',
'TABLE', N'FormGroup',
'COLUMN', N'FormGroupNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'Form',
'TABLE', N'FormGroup',
'COLUMN', N'SortOrder'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单组别描述（中文）',
'SCHEMA', N'Form',
'TABLE', N'FormGroup',
'COLUMN', N'DescriptionCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单组别描述（英文）',
'SCHEMA', N'Form',
'TABLE', N'FormGroup',
'COLUMN', N'DescriptionEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'FormGroup',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'FormGroup',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'FormGroup',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'FormGroup',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单组别表',
'SCHEMA', N'Form',
'TABLE', N'FormGroup'
GO


-- ----------------------------
-- Records of FormGroup
-- ----------------------------
INSERT INTO [Form].[FormGroup] ([FormGroupId], [FormGroupNameCn], [FormGroupNameEn], [SortOrder], [DescriptionCn], [DescriptionEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969052085492256768', N'工程类', N'Engineering Category', N'2', N'', NULL, N'1903486709602062336', N'2025-09-25 13:51:23.000', NULL, NULL)
GO

INSERT INTO [Form].[FormGroup] ([FormGroupId], [FormGroupNameCn], [FormGroupNameEn], [SortOrder], [DescriptionCn], [DescriptionEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969053776929230848', N'管理类', N'Management Category', N'3', N'', NULL, N'1903486709602062336', N'2025-09-25 13:51:23.000', NULL, NULL)
GO

INSERT INTO [Form].[FormGroup] ([FormGroupId], [FormGroupNameCn], [FormGroupNameEn], [SortOrder], [DescriptionCn], [DescriptionEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969054482025287680', N'总务类', N'General Affairs Category', N'4', N'', NULL, N'1903486709602062336', N'2025-09-25 13:51:23.000', NULL, NULL)
GO

INSERT INTO [Form].[FormGroup] ([FormGroupId], [FormGroupNameCn], [FormGroupNameEn], [SortOrder], [DescriptionCn], [DescriptionEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969054690842906624', N'业务类', N'Business Category', N'5', N'', NULL, N'1903486709602062336', N'2025-09-25 13:51:23.000', NULL, NULL)
GO

INSERT INTO [Form].[FormGroup] ([FormGroupId], [FormGroupNameCn], [FormGroupNameEn], [SortOrder], [DescriptionCn], [DescriptionEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969054813085896704', N'采购类', N'Procurement Category', N'6', N'', NULL, N'1903486709602062336', N'2025-09-25 13:51:23.000', NULL, NULL)
GO

INSERT INTO [Form].[FormGroup] ([FormGroupId], [FormGroupNameCn], [FormGroupNameEn], [SortOrder], [DescriptionCn], [DescriptionEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969055160932110336', N'法务类', N'Legal Affairs Category', N'7', N'', NULL, N'1903486709602062336', N'2025-09-25 13:51:23.000', NULL, NULL)
GO

INSERT INTO [Form].[FormGroup] ([FormGroupId], [FormGroupNameCn], [FormGroupNameEn], [SortOrder], [DescriptionCn], [DescriptionEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969055351626141696', N'资讯类', N'Information Category', N'8', N'', NULL, N'1903486709602062336', N'2025-09-25 13:51:23.000', NULL, NULL)
GO

INSERT INTO [Form].[FormGroup] ([FormGroupId], [FormGroupNameCn], [FormGroupNameEn], [SortOrder], [DescriptionCn], [DescriptionEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969055451307970560', N'品保类', N'Quality Assurance Category', N'9', N'', NULL, N'1903486709602062336', N'2025-09-25 13:51:23.000', NULL, NULL)
GO

INSERT INTO [Form].[FormGroup] ([FormGroupId], [FormGroupNameCn], [FormGroupNameEn], [SortOrder], [DescriptionCn], [DescriptionEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969055549681176576', N'关务类', N'Customs Category', N'10', N'', NULL, N'1903486709602062336', N'2025-09-25 13:51:23.000', NULL, NULL)
GO

INSERT INTO [Form].[FormGroup] ([FormGroupId], [FormGroupNameCn], [FormGroupNameEn], [SortOrder], [DescriptionCn], [DescriptionEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969055723409248256', N'模具类', N'Mold Category', N'11', N'', NULL, N'1903486709602062336', N'2025-09-25 13:51:23.000', NULL, NULL)
GO

INSERT INTO [Form].[FormGroup] ([FormGroupId], [FormGroupNameCn], [FormGroupNameEn], [SortOrder], [DescriptionCn], [DescriptionEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969055819815325696', N'财务类', N'Finance Category', N'12', N'', NULL, N'1903486709602062336', N'2025-09-25 13:51:23.000', NULL, NULL)
GO

INSERT INTO [Form].[FormGroup] ([FormGroupId], [FormGroupNameCn], [FormGroupNameEn], [SortOrder], [DescriptionCn], [DescriptionEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1987215338470772736', N'人事类', N'Human Resources', N'1', N'', N'', N'1903486709602062336', N'2025-11-09 01:47:12.000', NULL, NULL)
GO


-- ----------------------------
-- Table structure for FormInstance
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[FormInstance]') AND type IN ('U'))
	DROP TABLE [Form].[FormInstance]
GO

CREATE TABLE [Form].[FormInstance] (
  [FormId] bigint  NOT NULL,
  [FormTypeId] bigint  NOT NULL,
  [FormNo] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [FormStatus] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ApplicantUserId] bigint  NOT NULL,
  [BranchId] bigint  NOT NULL,
  [CurrentStepId] bigint  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Form].[FormInstance] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单Id',
'SCHEMA', N'Form',
'TABLE', N'FormInstance',
'COLUMN', N'FormId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单类型Id',
'SCHEMA', N'Form',
'TABLE', N'FormInstance',
'COLUMN', N'FormTypeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单编号',
'SCHEMA', N'Form',
'TABLE', N'FormInstance',
'COLUMN', N'FormNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单状态',
'SCHEMA', N'Form',
'TABLE', N'FormInstance',
'COLUMN', N'FormStatus'
GO

EXEC sp_addextendedproperty
'MS_Description', N'申请人Id',
'SCHEMA', N'Form',
'TABLE', N'FormInstance',
'COLUMN', N'ApplicantUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'所属分支',
'SCHEMA', N'Form',
'TABLE', N'FormInstance',
'COLUMN', N'BranchId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'当前步骤Id',
'SCHEMA', N'Form',
'TABLE', N'FormInstance',
'COLUMN', N'CurrentStepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'FormInstance',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'FormInstance',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'FormInstance',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'FormInstance',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单基础信息表',
'SCHEMA', N'Form',
'TABLE', N'FormInstance'
GO


-- ----------------------------
-- Records of FormInstance
-- ----------------------------
INSERT INTO [Form].[FormInstance] ([FormId], [FormTypeId], [FormNo], [FormStatus], [ApplicantUserId], [BranchId], [CurrentStepId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2041417324984143872', N'1987217256446300160', N'LVR-2026040001', N'PendingSubmission', N'1903486709602062336', N'2036711225970266112', N'2009890853346217984', N'1903486709602062336', N'2026-04-07 15:26:32.603', N'1903486709602062336', N'2026-04-07 16:11:15.253')
GO


-- ----------------------------
-- Table structure for FormLog
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[FormLog]') AND type IN ('U'))
	DROP TABLE [Form].[FormLog]
GO

CREATE TABLE [Form].[FormLog] (
  [FormId] bigint  NOT NULL,
  [StepId] bigint  NOT NULL,
  [OperationStatus] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [RejectedStepId] bigint  NULL,
  [Comment] nvarchar(500) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [OperationType] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [OperationUserId] bigint  NOT NULL,
  [OperationDate] datetime2(3)  NOT NULL
)
GO

ALTER TABLE [Form].[FormLog] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单Id',
'SCHEMA', N'Form',
'TABLE', N'FormLog',
'COLUMN', N'FormId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤Id',
'SCHEMA', N'Form',
'TABLE', N'FormLog',
'COLUMN', N'StepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'操作状态（签核、驳回）',
'SCHEMA', N'Form',
'TABLE', N'FormLog',
'COLUMN', N'OperationStatus'
GO

EXEC sp_addextendedproperty
'MS_Description', N'驳回至步骤',
'SCHEMA', N'Form',
'TABLE', N'FormLog',
'COLUMN', N'RejectedStepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批意见',
'SCHEMA', N'Form',
'TABLE', N'FormLog',
'COLUMN', N'Comment'
GO

EXEC sp_addextendedproperty
'MS_Description', N'操作类型（手动操作、系统自动）',
'SCHEMA', N'Form',
'TABLE', N'FormLog',
'COLUMN', N'OperationType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'操作员工Id',
'SCHEMA', N'Form',
'TABLE', N'FormLog',
'COLUMN', N'OperationUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'操作时间',
'SCHEMA', N'Form',
'TABLE', N'FormLog',
'COLUMN', N'OperationDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单日志信息',
'SCHEMA', N'Form',
'TABLE', N'FormLog'
GO


-- ----------------------------
-- Records of FormLog
-- ----------------------------

-- ----------------------------
-- Table structure for FormSequence
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[FormSequence]') AND type IN ('U'))
	DROP TABLE [Form].[FormSequence]
GO

CREATE TABLE [Form].[FormSequence] (
  [FormTypeId] bigint  NOT NULL,
  [Ym] char(6) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Total] int DEFAULT 0 NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
)
GO

ALTER TABLE [Form].[FormSequence] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单类型Id',
'SCHEMA', N'Form',
'TABLE', N'FormSequence',
'COLUMN', N'FormTypeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'年月（yyyyMM）',
'SCHEMA', N'Form',
'TABLE', N'FormSequence',
'COLUMN', N'Ym'
GO

EXEC sp_addextendedproperty
'MS_Description', N'当前表单数量',
'SCHEMA', N'Form',
'TABLE', N'FormSequence',
'COLUMN', N'Total'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'FormSequence',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'FormSequence',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'FormSequence',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'FormSequence',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单审计信息表',
'SCHEMA', N'Form',
'TABLE', N'FormSequence'
GO


-- ----------------------------
-- Records of FormSequence
-- ----------------------------
INSERT INTO [Form].[FormSequence] ([FormTypeId], [Ym], [Total], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1987217256446300160', N'202604', N'1', N'1903486709602062336', N'2026-04-07 15:26:32.603', NULL, NULL)
GO


-- ----------------------------
-- Table structure for FormType
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[FormType]') AND type IN ('U'))
	DROP TABLE [Form].[FormType]
GO

CREATE TABLE [Form].[FormType] (
  [FormTypeId] bigint  NOT NULL,
  [FormGroupId] bigint  NOT NULL,
  [FormTypeNameCn] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [FormTypeNameEn] nvarchar(70) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Prefix] nvarchar(5) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ApprovalPath] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [ViewPath] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [SortOrder] int  NOT NULL,
  [DescriptionCn] nvarchar(300) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [DescriptionEn] nvarchar(500) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime  NULL
)
GO

ALTER TABLE [Form].[FormType] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单类型Id',
'SCHEMA', N'Form',
'TABLE', N'FormType',
'COLUMN', N'FormTypeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'所属表单组别Id',
'SCHEMA', N'Form',
'TABLE', N'FormType',
'COLUMN', N'FormGroupId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单类型名称（中文）',
'SCHEMA', N'Form',
'TABLE', N'FormType',
'COLUMN', N'FormTypeNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单类型名称（英文）',
'SCHEMA', N'Form',
'TABLE', N'FormType',
'COLUMN', N'FormTypeNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'前缀',
'SCHEMA', N'Form',
'TABLE', N'FormType',
'COLUMN', N'Prefix'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单审批路径',
'SCHEMA', N'Form',
'TABLE', N'FormType',
'COLUMN', N'ApprovalPath'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单视图路径',
'SCHEMA', N'Form',
'TABLE', N'FormType',
'COLUMN', N'ViewPath'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'Form',
'TABLE', N'FormType',
'COLUMN', N'SortOrder'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单类别描述（中文）',
'SCHEMA', N'Form',
'TABLE', N'FormType',
'COLUMN', N'DescriptionCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单类别描述（英文）',
'SCHEMA', N'Form',
'TABLE', N'FormType',
'COLUMN', N'DescriptionEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'FormType',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'FormType',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'FormType',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'FormType',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单类别表',
'SCHEMA', N'Form',
'TABLE', N'FormType'
GO


-- ----------------------------
-- Records of FormType
-- ----------------------------
INSERT INTO [Form].[FormType] ([FormTypeId], [FormGroupId], [FormTypeNameCn], [FormTypeNameEn], [Prefix], [ApprovalPath], [ViewPath], [SortOrder], [DescriptionCn], [DescriptionEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1987217256446300160', N'1987215338470772736', N'请假单', N'Leave Request Form', N'LVR', N'formbusiness/forms/leaveform/leaveform_r', N'formbusiness/forms/leaveform/leaveform_r', N'1', N'请假单用于员工因个人事由、病假、事假、年假等原因需要离开工作岗位时，向所属部门及管理层提出请假申请、审批与备案的业务单据。该单据记录请假类型、请假时间、时长、事由以及审批流程，用于确保人员安排合理、流程合规与人事数据准确。', N'A Leave Request Form is used when an employee needs to be absent from work due to personal reasons, sickness, annual leave, or other approved leave types. The form is submitted to the employee’s department and management for approval and record-keeping. It captures the leave type, leave period, duration, reason, and approval workflow, ensuring proper staffing, compliance, and accurate HR records.', N'1903486709602062336', N'2025-11-09 01:54:49.000', N'1903486709602062336', N'2025-11-09 02:16:46.000')
GO


-- ----------------------------
-- Table structure for LeaveForm
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[LeaveForm]') AND type IN ('U'))
	DROP TABLE [Form].[LeaveForm]
GO

CREATE TABLE [Form].[LeaveForm] (
  [FormId] bigint  NOT NULL,
  [ApplicantUserId] bigint  NULL,
  [LeaveType] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Reason] nvarchar(150) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [StartTime] datetime2(7)  NULL,
  [EndTime] datetime2(7)  NULL,
  [Days] decimal(6,2)  NOT NULL,
  [AgentUserNo] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Form].[LeaveForm] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'请假单Id',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'FormId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'申请人Id',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'ApplicantUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'请假假别编码',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'LeaveType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'请假事由',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'Reason'
GO

EXEC sp_addextendedproperty
'MS_Description', N'请假开始时间',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'StartTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'请假结束时间',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'EndTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'请假时数',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'Days'
GO

EXEC sp_addextendedproperty
'MS_Description', N'代理人工号',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'AgentUserNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'请假单信息表',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm'
GO


-- ----------------------------
-- Records of LeaveForm
-- ----------------------------
INSERT INTO [Form].[LeaveForm] ([FormId], [ApplicantUserId], [LeaveType], [Reason], [StartTime], [EndTime], [Days], [AgentUserNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2041417324984143872', N'1903486709602062336', N'Annual', N'1', N'2026-09-29 00:00:00.0000000', N'2026-09-30 23:59:59.0000000', N'2.00', N'E347473', N'1903486709602062336', N'2026-04-07 15:26:56.487', N'1903486709602062336', N'2026-04-07 16:11:08.583')
GO


-- ----------------------------
-- Table structure for PendingApproval
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[PendingApproval]') AND type IN ('U'))
	DROP TABLE [Form].[PendingApproval]
GO

CREATE TABLE [Form].[PendingApproval] (
  [FormId] bigint  NOT NULL,
  [CurrentStepId] bigint  NOT NULL,
  [ApproveUserId] bigint  NOT NULL
)
GO

ALTER TABLE [Form].[PendingApproval] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单Id',
'SCHEMA', N'Form',
'TABLE', N'PendingApproval',
'COLUMN', N'FormId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'当前步骤Id',
'SCHEMA', N'Form',
'TABLE', N'PendingApproval',
'COLUMN', N'CurrentStepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'待签核人员Id',
'SCHEMA', N'Form',
'TABLE', N'PendingApproval',
'COLUMN', N'ApproveUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单待签核人员表',
'SCHEMA', N'Form',
'TABLE', N'PendingApproval'
GO


-- ----------------------------
-- Records of PendingApproval
-- ----------------------------
INSERT INTO [Form].[PendingApproval] ([FormId], [CurrentStepId], [ApproveUserId]) VALUES (N'2041417324984143872', N'2009890853346217984', N'1903486709602062336')
GO


-- ----------------------------
-- Table structure for WorkflowBranch
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[WorkflowBranch]') AND type IN ('U'))
	DROP TABLE [Form].[WorkflowBranch]
GO

CREATE TABLE [Form].[WorkflowBranch] (
  [BranchId] bigint  NOT NULL,
  [FormTypeId] bigint  NOT NULL,
  [BranchNameCn] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [BranchNameEn] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [HandlerKey] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [IsDefault] int  NULL,
  [Description] nvarchar(500) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NULL,
  [CreatedDate] datetime2(3) DEFAULT getdate() NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Form].[WorkflowBranch] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'分支Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranch',
'COLUMN', N'BranchId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单类型Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranch',
'COLUMN', N'FormTypeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'分支名称（中文）',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranch',
'COLUMN', N'BranchNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'分支名称（英文）',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranch',
'COLUMN', N'BranchNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'代码标记',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranch',
'COLUMN', N'HandlerKey'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否默认分支',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranch',
'COLUMN', N'IsDefault'
GO

EXEC sp_addextendedproperty
'MS_Description', N'逻辑说明',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranch',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranch',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranch',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranch',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranch',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'流程审批条件表',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranch'
GO


-- ----------------------------
-- Records of WorkflowBranch
-- ----------------------------
INSERT INTO [Form].[WorkflowBranch] ([BranchId], [FormTypeId], [BranchNameCn], [BranchNameEn], [HandlerKey], [IsDefault], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2035949443819376640', N'1987217256446300160', N'请假天数超过5天', N'Leave of absence exceeding 5 days', N'IsLeaveDaysOver3', N'0', NULL, N'1903486709602062336', N'2026-03-31 09:38:27.000', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowBranch] ([BranchId], [FormTypeId], [BranchNameCn], [BranchNameEn], [HandlerKey], [IsDefault], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2036711225970266112', N'1987217256446300160', N'默认', N'default', NULL, N'1', NULL, N'1903486709602062336', N'2026-03-31 09:38:30.000', NULL, NULL)
GO


-- ----------------------------
-- Table structure for WorkflowBranchStep
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[WorkflowBranchStep]') AND type IN ('U'))
	DROP TABLE [Form].[WorkflowBranchStep]
GO

CREATE TABLE [Form].[WorkflowBranchStep] (
  [BranchId] bigint  NOT NULL,
  [StepId] bigint  NOT NULL,
  [NextStepId] bigint  NOT NULL,
  [SortOrder] int  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Form].[WorkflowBranchStep] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'分支Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranchStep',
'COLUMN', N'BranchId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranchStep',
'COLUMN', N'StepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'下个步骤Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranchStep',
'COLUMN', N'NextStepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranchStep',
'COLUMN', N'SortOrder'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranchStep',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranchStep',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranchStep',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowBranchStep',
'COLUMN', N'ModifiedDate'
GO


-- ----------------------------
-- Records of WorkflowBranchStep
-- ----------------------------
INSERT INTO [Form].[WorkflowBranchStep] ([BranchId], [StepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2036711225970266112', N'2009890853346217984', N'2009892923604340736', N'1', N'1903486709602062336', N'2026-04-03 14:54:22.853', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowBranchStep] ([BranchId], [StepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2036711225970266112', N'2009892923604340736', N'2009897830268932096', N'2', N'1903486709602062336', N'2026-04-03 14:54:46.243', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowBranchStep] ([BranchId], [StepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2036711225970266112', N'2009897830268932096', N'2009898117243211776', N'3', N'1903486709602062336', N'2026-04-03 14:56:34.553', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowBranchStep] ([BranchId], [StepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2036711225970266112', N'2009898117243211776', N'2029389483455156224', N'4', N'1903486709602062336', N'2026-04-03 14:56:45.750', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowBranchStep] ([BranchId], [StepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2036711225970266112', N'2029389483455156224', N'2036076248547069952', N'5', N'1903486709602062336', N'2026-04-03 15:01:40.203', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowBranchStep] ([BranchId], [StepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2036711225970266112', N'2036076248547069952', N'2032353104544010240', N'6', N'1903486709602062336', N'2026-04-03 15:02:41.370', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowBranchStep] ([BranchId], [StepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2036711225970266112', N'2032353104544010240', N'-1', N'7', N'1903486709602062336', N'2026-04-03 15:18:12.113', N'1903486709602062336', N'2026-04-03 15:21:38.980')
GO


-- ----------------------------
-- Table structure for WorkflowStep
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[WorkflowStep]') AND type IN ('U'))
	DROP TABLE [Form].[WorkflowStep]
GO

CREATE TABLE [Form].[WorkflowStep] (
  [StepId] bigint  NOT NULL,
  [FormGroupId] bigint  NULL,
  [FormTypeId] bigint  NULL,
  [StepNameCn] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [StepNameEn] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [IsStartStep] int  NULL,
  [Assignment] nvarchar(15) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [ApproveMode] nvarchar(15) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [IsReminderEnabled] int  NULL,
  [ReminderIntervalMinutes] int  NULL,
  [SortOrder] int  NULL,
  [CreatedBy] bigint  NULL,
  [CreatedDate] datetime2(3)  NULL,
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
'MS_Description', N'表单组别Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'FormGroupId'
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
'MS_Description', N'排序',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStep',
'COLUMN', N'SortOrder'
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
INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ApproveMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009890853346217984', N'1987215338470772736', N'1987217256446300160', N'发起人', N'Applicant', N'1', N'Org', N'Single', N'0', N'0', N'1', N'1903486709602062336', N'2026-01-10 15:31:41.000', N'1903486709602062336', N'2026-03-16 10:07:04.660')
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ApproveMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009892923604340736', N'1987215338470772736', N'1987217256446300160', N'执 - 组长', N'Ops - Team leader', N'0', N'Org', N'Single', N'1', N'1', N'2', N'1903486709602062336', N'2026-01-10 15:39:49.000', N'1903486709602062336', N'2026-03-16 19:19:50.857')
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ApproveMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009897830268932096', N'1987215338470772736', N'1987217256446300160', N'科 - 科长', N'Sec - Section Chief', N'0', N'Org', N'Single', N'1', N'1', N'3', N'1903486709602062336', N'2026-01-10 15:59:19.000', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ApproveMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009898117243211776', N'1987215338470772736', N'1987217256446300160', N'部 - 经理', N'Dept - Manager', N'0', N'Org', N'Single', N'1', N'1', N'4', N'1903486709602062336', N'2026-01-10 16:00:27.000', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ApproveMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2029389483455156224', N'1987215338470772736', N'1987217256446300160', N'部 - 资深经理', N'Dept - Senior Manager', N'0', N'Org', N'AndSingle', N'1', N'1', N'5', N'1903486709602062336', N'2026-03-05 10:52:11.747', N'1903486709602062336', N'2026-03-24 19:13:21.687')
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ApproveMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2032353104544010240', N'1987215338470772736', N'1987217256446300160', N'人事专员', N'Human Specialist', N'0', N'DeptUser', N'Single', N'1', N'0', N'7', N'1903486709602062336', N'2026-03-13 15:08:34.077', N'1903486709602062336', N'2026-03-23 21:43:46.587')
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ApproveMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2032353104544010286', N'1987215338470772736', N'1987217256446300160', N'人事经理', N'Human  Manager', N'0', N'DeptUser', N'Single', N'1', N'0', N'8', N'1903486709602062336', N'2026-03-13 15:08:34.077', N'1903486709602062336', N'2026-03-16 19:26:34.510')
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ApproveMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2036076248547069952', N'1987215338470772736', N'1987217256446300160', N'厂 - 厂长', N'Plant - Plant Director', N'0', N'Org', N'AndSingle', N'0', N'0', N'6', N'1903486709602062336', N'2026-03-23 21:43:00.780', N'1903486709602062336', N'2026-03-24 20:33:50.487')
GO


-- ----------------------------
-- Table structure for WorkflowStepCustom
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[WorkflowStepCustom]') AND type IN ('U'))
	DROP TABLE [Form].[WorkflowStepCustom]
GO

CREATE TABLE [Form].[WorkflowStepCustom] (
  [StepId] bigint  NOT NULL,
  [HandlerKey] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [LogicalExplanation] nvarchar(150) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL
)
GO

ALTER TABLE [Form].[WorkflowStepCustom] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批步骤Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepCustom',
'COLUMN', N'StepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'代码标记',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepCustom',
'COLUMN', N'HandlerKey'
GO

EXEC sp_addextendedproperty
'MS_Description', N'逻辑说明',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepCustom',
'COLUMN', N'LogicalExplanation'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepCustom',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepCustom',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤人员来源表-依照自定义',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepCustom'
GO


-- ----------------------------
-- Records of WorkflowStepCustom
-- ----------------------------

-- ----------------------------
-- Table structure for WorkflowStepDeptUser
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[WorkflowStepDeptUser]') AND type IN ('U'))
	DROP TABLE [Form].[WorkflowStepDeptUser]
GO

CREATE TABLE [Form].[WorkflowStepDeptUser] (
  [StepId] bigint  NOT NULL,
  [DepartmentId] bigint  NOT NULL,
  [PositionId] bigint  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL
)
GO

ALTER TABLE [Form].[WorkflowStepDeptUser] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批步骤Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepDeptUser',
'COLUMN', N'StepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepDeptUser',
'COLUMN', N'DepartmentId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职级Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepDeptUser',
'COLUMN', N'PositionId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepDeptUser',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepDeptUser',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤人员来源表-依照指定部门员工职级职业',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepDeptUser'
GO


-- ----------------------------
-- Records of WorkflowStepDeptUser
-- ----------------------------
INSERT INTO [Form].[WorkflowStepDeptUser] ([StepId], [DepartmentId], [PositionId], [CreatedBy], [CreatedDate]) VALUES (N'2032353104544010240', N'1929535196076576783', N'1351602771312246784', N'1903486709602062336', N'2026-03-23 21:43:46.597')
GO

INSERT INTO [Form].[WorkflowStepDeptUser] ([StepId], [DepartmentId], [PositionId], [CreatedBy], [CreatedDate]) VALUES (N'2032353104544010286', N'1929535196076576783', N'1351600746193223680', N'1903486709602062336', N'2026-03-16 19:26:34.520')
GO


-- ----------------------------
-- Table structure for WorkflowStepOrg
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[WorkflowStepOrg]') AND type IN ('U'))
	DROP TABLE [Form].[WorkflowStepOrg]
GO

CREATE TABLE [Form].[WorkflowStepOrg] (
  [StepId] bigint  NOT NULL,
  [DeptLeaveId] bigint  NOT NULL,
  [PositionId] bigint  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL
)
GO

ALTER TABLE [Form].[WorkflowStepOrg] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批步骤Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepOrg',
'COLUMN', N'StepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门级别Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepOrg',
'COLUMN', N'DeptLeaveId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职级Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepOrg',
'COLUMN', N'PositionId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepOrg',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepOrg',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤人员来源表-依照组织架构',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepOrg'
GO


-- ----------------------------
-- Records of WorkflowStepOrg
-- ----------------------------
INSERT INTO [Form].[WorkflowStepOrg] ([StepId], [DeptLeaveId], [PositionId], [CreatedBy], [CreatedDate]) VALUES (N'2009892923604340736', N'1949169142347206656', N'1351602631784529920', N'1903486709602062336', N'2026-03-16 19:19:50.867')
GO

INSERT INTO [Form].[WorkflowStepOrg] ([StepId], [DeptLeaveId], [PositionId], [CreatedBy], [CreatedDate]) VALUES (N'2009897830268932096', N'1949168956883472384', N'1351600746193223680', N'1903486709602062336', N'2026-01-10 15:59:19.000')
GO

INSERT INTO [Form].[WorkflowStepOrg] ([StepId], [DeptLeaveId], [PositionId], [CreatedBy], [CreatedDate]) VALUES (N'2009898117243211776', N'1949167957770899456', N'1351585319710883840', N'1903486709602062336', N'2026-01-10 16:00:27.000')
GO

INSERT INTO [Form].[WorkflowStepOrg] ([StepId], [DeptLeaveId], [PositionId], [CreatedBy], [CreatedDate]) VALUES (N'2029389483455156224', N'1949167957770899456', N'1351584156689104896', N'1903486709602062336', N'2026-03-24 19:13:21.707')
GO

INSERT INTO [Form].[WorkflowStepOrg] ([StepId], [DeptLeaveId], [PositionId], [CreatedBy], [CreatedDate]) VALUES (N'2036076248547069952', N'1351405026328707072', N'1351584014896463872', N'1903486709602062336', N'2026-03-24 20:33:50.497')
GO


-- ----------------------------
-- Table structure for WorkflowStepUser
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[WorkflowStepUser]') AND type IN ('U'))
	DROP TABLE [Form].[WorkflowStepUser]
GO

CREATE TABLE [Form].[WorkflowStepUser] (
  [StepId] bigint  NOT NULL,
  [DepartmentId] bigint  NOT NULL,
  [UserId] bigint  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL
)
GO

ALTER TABLE [Form].[WorkflowStepUser] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepUser',
'COLUMN', N'StepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepUser',
'COLUMN', N'DepartmentId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepUser',
'COLUMN', N'UserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepUser',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepUser',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤人员来源表-依照指定员工',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepUser'
GO


-- ----------------------------
-- Records of WorkflowStepUser
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ControlInfo
-- ----------------------------
ALTER TABLE [Form].[ControlInfo] ADD CONSTRAINT [PK__FormCont__3399DDEB13F7B41D] PRIMARY KEY CLUSTERED ([ControlCode])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table FormAttachment
-- ----------------------------
ALTER TABLE [Form].[FormAttachment] ADD CONSTRAINT [PK__FormFile__6F0F98BF2AF1086B] PRIMARY KEY CLUSTERED ([AttachmentId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table FormGroup
-- ----------------------------
ALTER TABLE [Form].[FormGroup] ADD CONSTRAINT [PK__FormClas__0201AC43C1A619E2] PRIMARY KEY CLUSTERED ([FormGroupId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table FormInstance
-- ----------------------------
ALTER TABLE [Form].[FormInstance] ADD CONSTRAINT [PK__FormInfo__FB05B7DDADCFEA72] PRIMARY KEY CLUSTERED ([FormId], [FormNo])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table FormType
-- ----------------------------
ALTER TABLE [Form].[FormType] ADD CONSTRAINT [PK__FormType__902E30B3CC4BDD80] PRIMARY KEY CLUSTERED ([FormTypeId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table LeaveForm
-- ----------------------------
ALTER TABLE [Form].[LeaveForm] ADD CONSTRAINT [PK__LeaveIns__796DB959B422B703] PRIMARY KEY CLUSTERED ([FormId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table WorkflowBranch
-- ----------------------------
ALTER TABLE [Form].[WorkflowBranch] ADD CONSTRAINT [PK__Workflow__37F5C0CF755E1DC7] PRIMARY KEY CLUSTERED ([BranchId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table WorkflowStep
-- ----------------------------
ALTER TABLE [Form].[WorkflowStep] ADD CONSTRAINT [PK__FormAppr__243433573D582151] PRIMARY KEY CLUSTERED ([StepId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table WorkflowStepCustom
-- ----------------------------
ALTER TABLE [Form].[WorkflowStepCustom] ADD CONSTRAINT [PK__FormStep__0A0392F2D13B9AA7] PRIMARY KEY CLUSTERED ([StepId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table WorkflowStepDeptUser
-- ----------------------------
ALTER TABLE [Form].[WorkflowStepDeptUser] ADD CONSTRAINT [PK__Workflow__24343357AD16EE5C] PRIMARY KEY CLUSTERED ([StepId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table WorkflowStepOrg
-- ----------------------------
ALTER TABLE [Form].[WorkflowStepOrg] ADD CONSTRAINT [PK__Workflow__243433574BF4D0F4] PRIMARY KEY CLUSTERED ([StepId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table WorkflowStepUser
-- ----------------------------
ALTER TABLE [Form].[WorkflowStepUser] ADD CONSTRAINT [PK__FormStep__8AB6E1C4820E75E2] PRIMARY KEY CLUSTERED ([StepId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

