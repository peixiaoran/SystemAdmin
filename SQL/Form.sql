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

 Date: 06/05/2026 17:00:29
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
INSERT INTO [Form].[FormAttachment] ([AttachmentId], [FormId], [AttachmentName], [AttachmentPath], [AttachmentSize], [CreatedBy], [CreatedDate]) VALUES (N'2051554379810607104', N'2051554334327574528', N'FOUpdate20260311114210.xls', N'/20260505/20260505144734837_eade959a.xls', N'110', N'1903486709602062336', N'2026-05-05 14:47:34.843')
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
  [CreatedBy] bigint  NULL,
  [CreatedDate] datetime DEFAULT getdate() NULL,
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
  [RuleId] bigint  NOT NULL,
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
'MS_Description', N'所属规则Id',
'SCHEMA', N'Form',
'TABLE', N'FormInstance',
'COLUMN', N'RuleId'
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
'MS_Description', N'表单实例表',
'SCHEMA', N'Form',
'TABLE', N'FormInstance'
GO


-- ----------------------------
-- Records of FormInstance
-- ----------------------------
INSERT INTO [Form].[FormInstance] ([FormId], [FormTypeId], [FormNo], [FormStatus], [ApplicantUserId], [RuleId], [CurrentStepId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051551790285066240', N'1987217256446300160', N'LVR-2026050001', N'Approved', N'1903486709602062336', N'2051185303204532224', N'2032353104544010240', N'1903486709602062336', N'2026-05-05 14:37:17.447', N'1903486709602062341', N'2026-05-05 14:39:25.647')
GO

INSERT INTO [Form].[FormInstance] ([FormId], [FormTypeId], [FormNo], [FormStatus], [ApplicantUserId], [RuleId], [CurrentStepId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051554334327574528', N'1987217256446300160', N'LVR-2026050002', N'Approved', N'1903486709602062336', N'2051185303204532224', N'2032353104544010240', N'1903486709602062336', N'2026-05-05 14:47:23.993', N'1903486709602062341', N'2026-05-05 14:51:18.950')
GO

INSERT INTO [Form].[FormInstance] ([FormId], [FormTypeId], [FormNo], [FormStatus], [ApplicantUserId], [RuleId], [CurrentStepId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051571275339534336', N'1987217256446300160', N'LVR-2026050004', N'UnderReview', N'1903486709602062340', N'2051186400535121920', N'2032353104544010240', N'1903486709602062340', N'2026-05-05 15:54:43.043', N'2050599298114129920', N'2026-05-05 16:08:14.560')
GO

INSERT INTO [Form].[FormInstance] ([FormId], [FormTypeId], [FormNo], [FormStatus], [ApplicantUserId], [RuleId], [CurrentStepId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051655239114821632', N'1987217256446300160', N'LVR-2026050005', N'UnderReview', N'1903486709602062340', N'2051186271467999232', N'2032353104544010240', N'1903486709602062340', N'2026-05-05 21:28:21.533', N'2050599625240481792', N'2026-05-05 21:33:06.947')
GO


-- ----------------------------
-- Table structure for FormReviewRecord
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[FormReviewRecord]') AND type IN ('U'))
	DROP TABLE [Form].[FormReviewRecord]
GO

CREATE TABLE [Form].[FormReviewRecord] (
  [FormId] bigint  NOT NULL,
  [StepId] bigint  NOT NULL,
  [ReviewResult] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [RejectStepId] bigint  NULL,
  [Comment] nvarchar(500) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [ReviewType] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ReviewAppointment] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [OriginalUserId] bigint  NOT NULL,
  [ReviewUserId] bigint  NOT NULL,
  [ReviewDateTime] datetime2(3)  NOT NULL
)
GO

ALTER TABLE [Form].[FormReviewRecord] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单Id',
'SCHEMA', N'Form',
'TABLE', N'FormReviewRecord',
'COLUMN', N'FormId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤Id',
'SCHEMA', N'Form',
'TABLE', N'FormReviewRecord',
'COLUMN', N'StepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批结果（签核、驳回）',
'SCHEMA', N'Form',
'TABLE', N'FormReviewRecord',
'COLUMN', N'ReviewResult'
GO

EXEC sp_addextendedproperty
'MS_Description', N'驳回步骤Id',
'SCHEMA', N'Form',
'TABLE', N'FormReviewRecord',
'COLUMN', N'RejectStepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批意见',
'SCHEMA', N'Form',
'TABLE', N'FormReviewRecord',
'COLUMN', N'Comment'
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批类型（手动操作、系统自动）',
'SCHEMA', N'Form',
'TABLE', N'FormReviewRecord',
'COLUMN', N'ReviewType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批身份（实、兼）',
'SCHEMA', N'Form',
'TABLE', N'FormReviewRecord',
'COLUMN', N'ReviewAppointment'
GO

EXEC sp_addextendedproperty
'MS_Description', N'原审批员工Id',
'SCHEMA', N'Form',
'TABLE', N'FormReviewRecord',
'COLUMN', N'OriginalUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批员工Id',
'SCHEMA', N'Form',
'TABLE', N'FormReviewRecord',
'COLUMN', N'ReviewUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批时间',
'SCHEMA', N'Form',
'TABLE', N'FormReviewRecord',
'COLUMN', N'ReviewDateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单审批记录表',
'SCHEMA', N'Form',
'TABLE', N'FormReviewRecord'
GO


-- ----------------------------
-- Records of FormReviewRecord
-- ----------------------------
INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051551790285066240', N'2009897830268932096', N'Approve', NULL, N'', N'Manual', N'Actual', N'1903486709602062340', N'1903486709602062340', N'2026-05-05 14:37:55.357')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051551790285066240', N'2032353104544010240', N'Approve', NULL, N'', N'Manual', N'Actual', N'1903486709602062341', N'1903486709602062341', N'2026-05-05 14:39:25.640')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051554334327574528', N'2009890853346217984', N'Approve', NULL, N'', N'Manual', N'Actual', N'1903486709602062336', N'1903486709602062336', N'2026-05-05 14:47:49.603')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051554334327574528', N'2009897830268932096', N'Approve', NULL, N'', N'Manual', N'Agent', N'1903486709602062340', N'2050599625240481792', N'2026-05-05 14:49:04.113')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051554334327574528', N'2009898117243211776', N'Approve', NULL, N'', N'Automatic', N'Actual', N'2050599625240481792', N'2050599625240481792', N'2026-05-05 14:49:04.183')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051554334327574528', N'2032353104544010240', N'Approve', NULL, N'', N'Manual', N'Actual', N'1903486709602062341', N'1903486709602062341', N'2026-05-05 14:51:18.940')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051566492788592640', N'2009890853346217984', N'Approve', NULL, N'', N'Manual', N'Actual', N'1903486709602062340', N'1903486709602062340', N'2026-05-05 15:50:30.720')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051571275339534336', N'2009898117243211776', N'Approve', NULL, N'', N'Manual', N'Actual', N'2050597318784323584', N'2050597318784323584', N'2026-05-05 15:55:21.747')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051571275339534336', N'2009898117243211776', N'Approve', NULL, N'好的。', N'Manual', N'Actual', N'2050599625240481792', N'2050599625240481792', N'2026-05-05 16:06:45.373')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051571275339534336', N'2029389483455156224', N'Approve', NULL, N'', N'Manual', N'Actual', N'2050599298114129920', N'2050599298114129920', N'2026-05-05 16:08:14.553')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051551790285066240', N'2009890853346217984', N'Approve', NULL, N'', N'Manual', N'Actual', N'1903486709602062336', N'1903486709602062336', N'2026-05-05 14:37:31.503')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051551790285066240', N'2009898117243211776', N'Approve', NULL, N'', N'Manual', N'Actual', N'2050597318784323584', N'2050597318784323584', N'2026-05-05 14:38:38.830')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051551790285066240', N'2009898117243211776', N'Approve', NULL, N'', N'Manual', N'Actual', N'2050599625240481792', N'2050599625240481792', N'2026-05-05 14:39:02.670')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051554334327574528', N'2009898117243211776', N'Approve', NULL, N'', N'Manual', N'Actual', N'2050597318784323584', N'2050597318784323584', N'2026-05-05 14:49:46.553')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051571275339534336', N'2009890853346217984', N'Approve', NULL, N'', N'Manual', N'Actual', N'1903486709602062340', N'1903486709602062340', N'2026-05-05 15:54:56.220')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051655239114821632', N'2009890853346217984', N'Approve', NULL, N'', N'Manual', N'Agent', N'1903486709602062340', N'2050599625240481792', N'2026-05-05 21:33:06.860')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051655239114821632', N'2009898117243211776', N'Approve', NULL, N'', N'Automatic', N'Actual', N'2050599625240481792', N'2050599625240481792', N'2026-05-05 21:33:06.977')
GO

INSERT INTO [Form].[FormReviewRecord] ([FormId], [StepId], [ReviewResult], [RejectStepId], [Comment], [ReviewType], [ReviewAppointment], [OriginalUserId], [ReviewUserId], [ReviewDateTime]) VALUES (N'2051655239114821632', N'2009898117243211776', N'Approve', NULL, N'111', N'Manual', N'Actual', N'2050597318784323584', N'2050597318784323584', N'2026-05-06 10:20:59.690')
GO


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
'MS_Description', N'表单审计表',
'SCHEMA', N'Form',
'TABLE', N'FormSequence'
GO


-- ----------------------------
-- Records of FormSequence
-- ----------------------------
INSERT INTO [Form].[FormSequence] ([FormTypeId], [Ym], [Total], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1987217256446300160', N'202605', N'5', N'1903486709602062336', N'2026-05-05 14:37:17.447', N'1903486709602062340', N'2026-05-05 21:28:21.533')
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
  [LeaveTypeCode] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [LeaveReason] nvarchar(150) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [LeaveStartTime] datetime2(7)  NULL,
  [LeaveEndTime] datetime2(7)  NULL,
  [LeaveDays] decimal(6,2)  NOT NULL,
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
'MS_Description', N'请假假别编码',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'LeaveTypeCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'请假事由',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'LeaveReason'
GO

EXEC sp_addextendedproperty
'MS_Description', N'请假开始时间',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'LeaveStartTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'请假结束时间',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'LeaveEndTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'请假时数',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'LeaveDays'
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
'MS_Description', N'请假单表',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm'
GO


-- ----------------------------
-- Records of LeaveForm
-- ----------------------------
INSERT INTO [Form].[LeaveForm] ([FormId], [LeaveTypeCode], [LeaveReason], [LeaveStartTime], [LeaveEndTime], [LeaveDays], [AgentUserNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051551790285066240', N'Annual', N'1', N'2026-05-01 00:00:00.0000000', N'2026-05-08 00:00:00.0000000', N'8.00', N'11', N'1903486709602062336', N'2026-05-05 14:37:17.453', N'1903486709602062336', N'2026-05-05 14:37:25.427')
GO

INSERT INTO [Form].[LeaveForm] ([FormId], [LeaveTypeCode], [LeaveReason], [LeaveStartTime], [LeaveEndTime], [LeaveDays], [AgentUserNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051554334327574528', N'Sick', N'1', N'2026-05-09 00:00:00.0000000', N'2026-05-16 00:00:00.0000000', N'8.00', N'1', N'1903486709602062336', N'2026-05-05 14:47:24.000', N'1903486709602062336', N'2026-05-05 14:47:36.290')
GO

INSERT INTO [Form].[LeaveForm] ([FormId], [LeaveTypeCode], [LeaveReason], [LeaveStartTime], [LeaveEndTime], [LeaveDays], [AgentUserNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051571275339534336', N'Annual', N'w', N'2026-05-01 00:00:00.0000000', N'2026-05-09 00:00:00.0000000', N'9.00', N'e', N'1903486709602062340', N'2026-05-05 15:54:43.053', N'1903486709602062340', N'2026-05-05 15:54:51.560')
GO

INSERT INTO [Form].[LeaveForm] ([FormId], [LeaveTypeCode], [LeaveReason], [LeaveStartTime], [LeaveEndTime], [LeaveDays], [AgentUserNo], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051655239114821632', N'Annual', N'1', N'2026-05-01 00:00:00.0000000', N'2026-05-02 00:00:00.0000000', N'2.00', N'1', N'1903486709602062340', N'2026-05-05 21:28:21.593', N'1903486709602062340', N'2026-05-05 21:28:29.173')
GO


-- ----------------------------
-- Table structure for PendingReview
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[PendingReview]') AND type IN ('U'))
	DROP TABLE [Form].[PendingReview]
GO

CREATE TABLE [Form].[PendingReview] (
  [FormId] bigint  NOT NULL,
  [StepId] bigint  NOT NULL,
  [ReviewUserId] bigint  NOT NULL
)
GO

ALTER TABLE [Form].[PendingReview] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单Id',
'SCHEMA', N'Form',
'TABLE', N'PendingReview',
'COLUMN', N'FormId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤Id',
'SCHEMA', N'Form',
'TABLE', N'PendingReview',
'COLUMN', N'StepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'待签核人员Id',
'SCHEMA', N'Form',
'TABLE', N'PendingReview',
'COLUMN', N'ReviewUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单待签核人员表',
'SCHEMA', N'Form',
'TABLE', N'PendingReview'
GO


-- ----------------------------
-- Records of PendingReview
-- ----------------------------
INSERT INTO [Form].[PendingReview] ([FormId], [StepId], [ReviewUserId]) VALUES (N'2051571275339534336', N'2032353104544010240', N'1903486709602062341')
GO

INSERT INTO [Form].[PendingReview] ([FormId], [StepId], [ReviewUserId]) VALUES (N'2051655239114821632', N'2032353104544010240', N'1903486709602062341')
GO


-- ----------------------------
-- Table structure for WorkflowRule
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[WorkflowRule]') AND type IN ('U'))
	DROP TABLE [Form].[WorkflowRule]
GO

CREATE TABLE [Form].[WorkflowRule] (
  [RuleId] bigint  NOT NULL,
  [RuleNameCn] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [RuleNameEn] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [FormTypeId] bigint  NOT NULL,
  [PositionId] bigint  NOT NULL,
  [Guidance] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [SortOrder] int  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Form].[WorkflowRule] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'规则Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRule',
'COLUMN', N'RuleId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'规则名称（中文）',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRule',
'COLUMN', N'RuleNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'规则名称（英文）',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRule',
'COLUMN', N'RuleNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单类别Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRule',
'COLUMN', N'FormTypeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'职级Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRule',
'COLUMN', N'PositionId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'导向',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRule',
'COLUMN', N'Guidance'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRule',
'COLUMN', N'SortOrder'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRule',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRule',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRule',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRule',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'流程规则表',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRule'
GO


-- ----------------------------
-- Records of WorkflowRule
-- ----------------------------
INSERT INTO [Form].[WorkflowRule] ([RuleId], [RuleNameCn], [RuleNameEn], [FormTypeId], [PositionId], [Guidance], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2046850267772751872', N'组长 - 小于2天', N'Team Leader - Less than 2 days', N'1987217256446300160', N'1351602631784529920', N'LessOver2', N'1', N'1903486709602062336', N'2026-04-22 15:15:07.133', N'1903486709602062336', N'2026-05-04 14:19:51.580')
GO

INSERT INTO [Form].[WorkflowRule] ([RuleId], [RuleNameCn], [RuleNameEn], [FormTypeId], [PositionId], [Guidance], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2046860073602519040', N'其他', N'Other', N'1987217256446300160', N'0', N'', N'50', N'1903486709602062336', N'2026-04-22 15:54:05.027', N'1903486709602062336', N'2026-05-04 19:28:16.463')
GO

INSERT INTO [Form].[WorkflowRule] ([RuleId], [RuleNameCn], [RuleNameEn], [FormTypeId], [PositionId], [Guidance], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051185303204532224', N'组长 - 大于2天', N'Team Leader - More than 2 days', N'1987217256446300160', N'1351602631784529920', N'MoreOver2', N'2', N'1903486709602062336', N'2026-05-04 14:21:00.120', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRule] ([RuleId], [RuleNameCn], [RuleNameEn], [FormTypeId], [PositionId], [Guidance], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051186271467999232', N'科长 - 小于2天', N'Section Chief - Less than 2 days', N'1987217256446300160', N'1351600746193223680', N'LessOver2', N'3', N'1903486709602062336', N'2026-05-04 14:24:50.973', N'1903486709602062336', N'2026-05-04 14:32:53.470')
GO

INSERT INTO [Form].[WorkflowRule] ([RuleId], [RuleNameCn], [RuleNameEn], [FormTypeId], [PositionId], [Guidance], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051186400535121920', N'科长 - 大于2天', N'Section Chief - More than 2 days', N'1987217256446300160', N'1351600746193223680', N'MoreOver2', N'4', N'1903486709602062336', N'2026-05-04 14:25:21.747', N'1903486709602062336', N'2026-05-04 19:17:29.790')
GO

INSERT INTO [Form].[WorkflowRule] ([RuleId], [RuleNameCn], [RuleNameEn], [FormTypeId], [PositionId], [Guidance], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051259795381555200', N'副理 - 小于2天', N'Assistant Manager - Less than 2 days', N'1987217256446300160', N'1351592278136717312', N'LessOver2', N'5', N'1903486709602062336', N'2026-05-04 19:17:00.440', N'1903486709602062336', N'2026-05-04 19:17:17.373')
GO

INSERT INTO [Form].[WorkflowRule] ([RuleId], [RuleNameCn], [RuleNameEn], [FormTypeId], [PositionId], [Guidance], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051260041411039232', N'副理 -大于2天', N'Assistant Manager - More than 2 days', N'1987217256446300160', N'1351592278136717312', N'MoreOver2', N'6', N'1903486709602062336', N'2026-05-04 19:17:59.100', NULL, NULL)
GO


-- ----------------------------
-- Table structure for WorkflowRuleStep
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[WorkflowRuleStep]') AND type IN ('U'))
	DROP TABLE [Form].[WorkflowRuleStep]
GO

CREATE TABLE [Form].[WorkflowRuleStep] (
  [RuleId] bigint  NOT NULL,
  [CurrentStepId] bigint  NOT NULL,
  [NextStepId] bigint  NOT NULL,
  [SortOrder] int  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Form].[WorkflowRuleStep] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'规则Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRuleStep',
'COLUMN', N'RuleId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'当前步骤Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRuleStep',
'COLUMN', N'CurrentStepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'下一步骤Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRuleStep',
'COLUMN', N'NextStepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRuleStep',
'COLUMN', N'SortOrder'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRuleStep',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRuleStep',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRuleStep',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRuleStep',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'规则步骤表',
'SCHEMA', N'Form',
'TABLE', N'WorkflowRuleStep'
GO


-- ----------------------------
-- Records of WorkflowRuleStep
-- ----------------------------
INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009897830268932096', N'2009890853346217984', N'2009892923604340736', N'1', N'1903486709602062336', N'2026-04-23 11:39:44.170', N'1903486709602062336', N'2026-05-02 23:20:36.180')
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2046850267772751872', N'2009892923604340736', N'2009897830268932096', N'2', N'1903486709602062336', N'2026-04-23 11:40:04.650', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2046850267772751872', N'2009897830268932096', N'2032353104544010240', N'3', N'1903486709602062336', N'2026-04-23 11:57:27.710', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2046850267772751872', N'2032353104544010240', N'0', N'4', N'1903486709602062336', N'2026-04-23 11:58:54.023', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2046860073602519040', N'2009890853346217984', N'2009892923604340736', N'1', N'1903486709602062336', N'2026-05-03 19:55:38.247', N'1903486709602062336', N'2026-05-03 19:55:44.140')
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2046860073602519040', N'2009892923604340736', N'2009897830268932096', N'2', N'1903486709602062336', N'2026-05-03 19:55:53.800', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2046860073602519040', N'2009897830268932096', N'2009898117243211776', N'3', N'1903486709602062336', N'2026-05-03 19:59:09.177', N'1903486709602062336', N'2026-05-03 19:59:13.263')
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2046860073602519040', N'2009898117243211776', N'2029389483455156224', N'4', N'1903486709602062336', N'2026-05-03 19:59:22.433', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051185303204532224', N'2009890853346217984', N'2009892923604340736', N'1', N'1903486709602062336', N'2026-05-04 14:25:44.423', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051185303204532224', N'2009892923604340736', N'2009897830268932096', N'2', N'1903486709602062336', N'2026-05-04 14:25:55.033', N'1903486709602062336', N'2026-05-04 14:25:58.350')
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051185303204532224', N'2009897830268932096', N'2009898117243211776', N'3', N'1903486709602062336', N'2026-05-04 14:26:07.183', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051185303204532224', N'2009898117243211776', N'2032353104544010240', N'4', N'1903486709602062336', N'2026-05-04 14:26:13.737', N'1903486709602062336', N'2026-05-04 14:26:18.407')
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051186271467999232', N'2009890853346217984', N'2009892923604340736', N'1', N'1903486709602062336', N'2026-05-04 14:35:44.930', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051186271467999232', N'2009892923604340736', N'2009897830268932096', N'2', N'1903486709602062336', N'2026-05-04 14:35:55.927', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051186271467999232', N'2009897830268932096', N'2009898117243211776', N'3', N'1903486709602062336', N'2026-05-04 14:36:13.763', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051186271467999232', N'2032353104544010240', N'0', N'5', N'1903486709602062336', N'2026-05-04 14:36:34.560', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2046850267772751872', N'2009890853346217984', N'2009892923604340736', N'1', N'1903486709602062336', N'2026-05-05 12:34:36.897', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051186400535121920', N'2009890853346217984', N'2009892923604340736', N'1', N'1903486709602062336', N'2026-05-05 15:34:08.673', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051186400535121920', N'2009897830268932096', N'2009898117243211776', N'3', N'1903486709602062336', N'2026-05-05 15:34:24.080', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051186400535121920', N'2032353104544010240', N'0', N'6', N'1903486709602062336', N'2026-05-05 15:36:52.850', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2046860073602519040', N'2029389483455156224', N'2032353104544010240', N'5', N'1903486709602062336', N'2026-05-03 19:59:28.767', N'1903486709602062336', N'2026-05-03 19:59:33.143')
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2046860073602519040', N'2032353104544010240', N'2032353104544010286', N'6', N'1903486709602062336', N'2026-05-03 19:59:42.087', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2046860073602519040', N'2032353104544010286', N'0', N'7', N'1903486709602062336', N'2026-05-03 19:59:47.373', N'1903486709602062336', N'2026-05-03 19:59:52.553')
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051185303204532224', N'2032353104544010240', N'0', N'5', N'1903486709602062336', N'2026-05-04 14:26:28.063', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051186271467999232', N'2009898117243211776', N'2032353104544010240', N'4', N'1903486709602062336', N'2026-05-04 14:36:24.513', N'1903486709602062336', N'2026-05-04 14:36:42.110')
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051186400535121920', N'2009892923604340736', N'2009897830268932096', N'2', N'1903486709602062336', N'2026-05-05 15:34:15.690', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051186400535121920', N'2009898117243211776', N'2029389483455156224', N'4', N'1903486709602062336', N'2026-05-05 15:34:32.307', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowRuleStep] ([RuleId], [CurrentStepId], [NextStepId], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2051186400535121920', N'2029389483455156224', N'2032353104544010240', N'5', N'1903486709602062336', N'2026-05-05 15:34:40.330', N'1903486709602062336', N'2026-05-05 15:36:43.663')
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
  [ReviewMode] nvarchar(15) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
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
'COLUMN', N'ReviewMode'
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
INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ReviewMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009890853346217984', N'1987215338470772736', N'1987217256446300160', N'发起人', N'Applicant', N'1', N'Org', N'Single', N'0', N'0', N'1', N'1903486709602062336', N'2026-01-10 15:31:41.000', N'1903486709602062336', N'2026-03-16 10:07:04.660')
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ReviewMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009892923604340736', N'1987215338470772736', N'1987217256446300160', N'执 - 组长', N'Ops - Team leader', N'0', N'Org', N'Single', N'1', N'1', N'2', N'1903486709602062336', N'2026-01-10 15:39:49.000', N'1903486709602062336', N'2026-03-16 19:19:50.857')
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ReviewMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009897830268932096', N'1987215338470772736', N'1987217256446300160', N'科 - 科长', N'Sec - Section Chief', N'0', N'Org', N'Single', N'1', N'1', N'3', N'1903486709602062336', N'2026-01-10 15:59:19.000', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ReviewMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009898117243211776', N'1987215338470772736', N'1987217256446300160', N'部 - 经理', N'Dept - Manager', N'0', N'Org', N'AndSingle', N'1', N'1', N'4', N'1903486709602062336', N'2026-01-10 16:00:27.000', N'1903486709602062336', N'2026-05-05 13:38:24.357')
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ReviewMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2029389483455156224', N'1987215338470772736', N'1987217256446300160', N'部 - 资深经理', N'Dept - Senior Manager', N'0', N'Org', N'Single', N'1', N'1', N'5', N'1903486709602062336', N'2026-03-05 10:52:11.747', N'1903486709602062336', N'2026-03-24 19:13:21.687')
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ReviewMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2032353104544010240', N'1987215338470772736', N'1987217256446300160', N'人事专员', N'Human Specialist', N'0', N'DeptUser', N'Single', N'1', N'0', N'7', N'1903486709602062336', N'2026-03-13 15:08:34.077', N'1903486709602062336', N'2026-03-23 21:43:46.587')
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ReviewMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2032353104544010286', N'1987215338470772736', N'1987217256446300160', N'人事经理', N'Human  Manager', N'0', N'DeptUser', N'Single', N'1', N'0', N'8', N'1903486709602062336', N'2026-03-13 15:08:34.077', N'1903486709602062336', N'2026-03-16 19:26:34.510')
GO

INSERT INTO [Form].[WorkflowStep] ([StepId], [FormGroupId], [FormTypeId], [StepNameCn], [StepNameEn], [IsStartStep], [Assignment], [ReviewMode], [IsReminderEnabled], [ReminderIntervalMinutes], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2036076248547069952', N'1987215338470772736', N'1987217256446300160', N'厂 - 厂长', N'Plant - Plant Director', N'0', N'Org', N'Single', N'0', N'0', N'6', N'1903486709602062336', N'2026-03-23 21:43:00.780', N'1903486709602062336', N'2026-03-24 20:33:50.487')
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
INSERT INTO [Form].[WorkflowStepDeptUser] ([StepId], [DepartmentId], [PositionId], [CreatedBy], [CreatedDate]) VALUES (N'2032353104544010240', N'1950000000000000156', N'1351602631784529920', N'1903486709602062336', N'2026-03-23 21:43:46.597')
GO

INSERT INTO [Form].[WorkflowStepDeptUser] ([StepId], [DepartmentId], [PositionId], [CreatedBy], [CreatedDate]) VALUES (N'2032353104544010286', N'1950000000000000156', N'1351585319710883840', N'1903486709602062336', N'2026-03-16 19:26:34.520')
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

INSERT INTO [Form].[WorkflowStepOrg] ([StepId], [DeptLeaveId], [PositionId], [CreatedBy], [CreatedDate]) VALUES (N'2009898117243211776', N'1949167957770899456', N'1351585319710883840', N'1903486709602062336', N'2026-05-05 13:38:24.407')
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
ALTER TABLE [Form].[FormInstance] ADD CONSTRAINT [PK__FormInfo__FB05B7DDADCFEA72] PRIMARY KEY CLUSTERED ([FormId])
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
-- Primary Key structure for table WorkflowRule
-- ----------------------------
ALTER TABLE [Form].[WorkflowRule] ADD CONSTRAINT [PK__Workflow__110458E282CDD336] PRIMARY KEY CLUSTERED ([RuleId])
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

