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

 Date: 22/12/2025 17:52:04
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
-- Table structure for FormCounting
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[FormCounting]') AND type IN ('U'))
	DROP TABLE [Form].[FormCounting]
GO

CREATE TABLE [Form].[FormCounting] (
  [FormTypeId] bigint  NOT NULL,
  [YM] char(6) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Total] int DEFAULT 0 NOT NULL,
  [Draft] int DEFAULT 0 NOT NULL,
  [Submitted] int DEFAULT 0 NOT NULL,
  [Approved] int DEFAULT 0 NOT NULL,
  [Rejected] int DEFAULT 0 NOT NULL,
  [Canceled] int DEFAULT 0 NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
)
GO

ALTER TABLE [Form].[FormCounting] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单类型Id',
'SCHEMA', N'Form',
'TABLE', N'FormCounting',
'COLUMN', N'FormTypeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'年月（yyyyMM）',
'SCHEMA', N'Form',
'TABLE', N'FormCounting',
'COLUMN', N'YM'
GO

EXEC sp_addextendedproperty
'MS_Description', N'当前表单数量',
'SCHEMA', N'Form',
'TABLE', N'FormCounting',
'COLUMN', N'Total'
GO

EXEC sp_addextendedproperty
'MS_Description', N'草稿数量',
'SCHEMA', N'Form',
'TABLE', N'FormCounting',
'COLUMN', N'Draft'
GO

EXEC sp_addextendedproperty
'MS_Description', N'提交数量',
'SCHEMA', N'Form',
'TABLE', N'FormCounting',
'COLUMN', N'Submitted'
GO

EXEC sp_addextendedproperty
'MS_Description', N'送审数量',
'SCHEMA', N'Form',
'TABLE', N'FormCounting',
'COLUMN', N'Approved'
GO

EXEC sp_addextendedproperty
'MS_Description', N'驳回数量',
'SCHEMA', N'Form',
'TABLE', N'FormCounting',
'COLUMN', N'Rejected'
GO

EXEC sp_addextendedproperty
'MS_Description', N'作废数量',
'SCHEMA', N'Form',
'TABLE', N'FormCounting',
'COLUMN', N'Canceled'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'FormCounting',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'FormCounting',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'FormCounting',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'FormCounting',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单审计信息表',
'SCHEMA', N'Form',
'TABLE', N'FormCounting'
GO


-- ----------------------------
-- Records of FormCounting
-- ----------------------------
INSERT INTO [Form].[FormCounting] ([FormTypeId], [YM], [Total], [Draft], [Submitted], [Approved], [Rejected], [Canceled], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1987217256446300160', N'2511  ', N'0', N'0', N'0', N'0', N'0', N'0', N'0', N'1900-01-01 00:00:00.000', NULL, NULL)
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
-- Table structure for FormInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[FormInfo]') AND type IN ('U'))
	DROP TABLE [Form].[FormInfo]
GO

CREATE TABLE [Form].[FormInfo] (
  [FormId] bigint  NOT NULL,
  [FormTypeId] bigint  NOT NULL,
  [FormNo] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Description] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [ImportanceCode] int  NULL,
  [FormStatus] int  NULL,
  [FormOpenTime] datetime  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime  NULL
)
GO

ALTER TABLE [Form].[FormInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单Id',
'SCHEMA', N'Form',
'TABLE', N'FormInfo',
'COLUMN', N'FormId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单类型Id',
'SCHEMA', N'Form',
'TABLE', N'FormInfo',
'COLUMN', N'FormTypeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单编号',
'SCHEMA', N'Form',
'TABLE', N'FormInfo',
'COLUMN', N'FormNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'简短叙述',
'SCHEMA', N'Form',
'TABLE', N'FormInfo',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'重要程度',
'SCHEMA', N'Form',
'TABLE', N'FormInfo',
'COLUMN', N'ImportanceCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单状态',
'SCHEMA', N'Form',
'TABLE', N'FormInfo',
'COLUMN', N'FormStatus'
GO

EXEC sp_addextendedproperty
'MS_Description', N'开单时间',
'SCHEMA', N'Form',
'TABLE', N'FormInfo',
'COLUMN', N'FormOpenTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'FormInfo',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'FormInfo',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'FormInfo',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'FormInfo',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单基础信息表',
'SCHEMA', N'Form',
'TABLE', N'FormInfo'
GO


-- ----------------------------
-- Records of FormInfo
-- ----------------------------

-- ----------------------------
-- Table structure for FormStep
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[FormStep]') AND type IN ('U'))
	DROP TABLE [Form].[FormStep]
GO

CREATE TABLE [Form].[FormStep] (
  [StepId] bigint  NOT NULL,
  [FormTypeId] bigint  NOT NULL,
  [StepNameCn] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [StepNameEn] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Assignment] int  NOT NULL,
  [Description] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime  NULL
)
GO

ALTER TABLE [Form].[FormStep] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤Id',
'SCHEMA', N'Form',
'TABLE', N'FormStep',
'COLUMN', N'StepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单类型Id',
'SCHEMA', N'Form',
'TABLE', N'FormStep',
'COLUMN', N'FormTypeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤名称（中文）',
'SCHEMA', N'Form',
'TABLE', N'FormStep',
'COLUMN', N'StepNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤名称（英文）',
'SCHEMA', N'Form',
'TABLE', N'FormStep',
'COLUMN', N'StepNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批人选取方式（依部门人员组织结构、指定员工、自定义）',
'SCHEMA', N'Form',
'TABLE', N'FormStep',
'COLUMN', N'Assignment'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤描述',
'SCHEMA', N'Form',
'TABLE', N'FormStep',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'FormStep',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'FormStep',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'FormStep',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'FormStep',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单签核步骤表',
'SCHEMA', N'Form',
'TABLE', N'FormStep'
GO


-- ----------------------------
-- Records of FormStep
-- ----------------------------

-- ----------------------------
-- Table structure for FormStepApproverRule
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[FormStepApproverRule]') AND type IN ('U'))
	DROP TABLE [Form].[FormStepApproverRule]
GO

CREATE TABLE [Form].[FormStepApproverRule] (
  [StepApproverRuleId] bigint  NOT NULL,
  [Mark] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [LogicalExplanation] nvarchar(150) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime  NULL
)
GO

ALTER TABLE [Form].[FormStepApproverRule] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'签核步骤自定义Id',
'SCHEMA', N'Form',
'TABLE', N'FormStepApproverRule',
'COLUMN', N'StepApproverRuleId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'代码标记',
'SCHEMA', N'Form',
'TABLE', N'FormStepApproverRule',
'COLUMN', N'Mark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'逻辑说明',
'SCHEMA', N'Form',
'TABLE', N'FormStepApproverRule',
'COLUMN', N'LogicalExplanation'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'FormStepApproverRule',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'FormStepApproverRule',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'FormStepApproverRule',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'FormStepApproverRule',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单签核步骤人员来源表-依照自定义',
'SCHEMA', N'Form',
'TABLE', N'FormStepApproverRule'
GO


-- ----------------------------
-- Records of FormStepApproverRule
-- ----------------------------

-- ----------------------------
-- Table structure for FormStepDeptCriteria
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[FormStepDeptCriteria]') AND type IN ('U'))
	DROP TABLE [Form].[FormStepDeptCriteria]
GO

CREATE TABLE [Form].[FormStepDeptCriteria] (
  [StepDeptUserId] int  NOT NULL,
  [StepId] bigint  NOT NULL,
  [DeptIds] nvarchar(max) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [PositionIds] nvarchar(max) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [LaborIds] nvarchar(max) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime  NULL
)
GO

ALTER TABLE [Form].[FormStepDeptCriteria] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'签核步骤指定部门员工级别Id',
'SCHEMA', N'Form',
'TABLE', N'FormStepDeptCriteria',
'COLUMN', N'StepDeptUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤Id',
'SCHEMA', N'Form',
'TABLE', N'FormStepDeptCriteria',
'COLUMN', N'StepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门Ids',
'SCHEMA', N'Form',
'TABLE', N'FormStepDeptCriteria',
'COLUMN', N'DeptIds'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职级Ids',
'SCHEMA', N'Form',
'TABLE', N'FormStepDeptCriteria',
'COLUMN', N'PositionIds'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职业Ids',
'SCHEMA', N'Form',
'TABLE', N'FormStepDeptCriteria',
'COLUMN', N'LaborIds'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'FormStepDeptCriteria',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'FormStepDeptCriteria',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'FormStepDeptCriteria',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'FormStepDeptCriteria',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单签核步骤人员来源表-依照指定部门员工职级职业',
'SCHEMA', N'Form',
'TABLE', N'FormStepDeptCriteria'
GO


-- ----------------------------
-- Records of FormStepDeptCriteria
-- ----------------------------

-- ----------------------------
-- Table structure for FormStepOrg
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[FormStepOrg]') AND type IN ('U'))
	DROP TABLE [Form].[FormStepOrg]
GO

CREATE TABLE [Form].[FormStepOrg] (
  [StepOrgId] bigint  NOT NULL,
  [StepId] bigint  NOT NULL,
  [DeptLeaveIds] nvarchar(max) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [PositionIds] nvarchar(max) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [LaborIds] nvarchar(max) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime  NULL
)
GO

ALTER TABLE [Form].[FormStepOrg] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'签核步骤依照组织架构Id',
'SCHEMA', N'Form',
'TABLE', N'FormStepOrg',
'COLUMN', N'StepOrgId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤Id',
'SCHEMA', N'Form',
'TABLE', N'FormStepOrg',
'COLUMN', N'StepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门级别Ids',
'SCHEMA', N'Form',
'TABLE', N'FormStepOrg',
'COLUMN', N'DeptLeaveIds'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职级Ids',
'SCHEMA', N'Form',
'TABLE', N'FormStepOrg',
'COLUMN', N'PositionIds'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职业Ids',
'SCHEMA', N'Form',
'TABLE', N'FormStepOrg',
'COLUMN', N'LaborIds'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'FormStepOrg',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'FormStepOrg',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'FormStepOrg',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'FormStepOrg',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单签核步骤人员来源表-依照组织架构',
'SCHEMA', N'Form',
'TABLE', N'FormStepOrg'
GO


-- ----------------------------
-- Records of FormStepOrg
-- ----------------------------

-- ----------------------------
-- Table structure for FormStepUser
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[FormStepUser]') AND type IN ('U'))
	DROP TABLE [Form].[FormStepUser]
GO

CREATE TABLE [Form].[FormStepUser] (
  [StepUserId] bigint  NOT NULL,
  [StepId] bigint  NOT NULL,
  [UserIds] nvarchar(max) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime  NULL
)
GO

ALTER TABLE [Form].[FormStepUser] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'签核步骤指定员工Id',
'SCHEMA', N'Form',
'TABLE', N'FormStepUser',
'COLUMN', N'StepUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤Id',
'SCHEMA', N'Form',
'TABLE', N'FormStepUser',
'COLUMN', N'StepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工Ids',
'SCHEMA', N'Form',
'TABLE', N'FormStepUser',
'COLUMN', N'UserIds'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'FormStepUser',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'FormStepUser',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'FormStepUser',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'FormStepUser',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单签核步骤人员来源表-依照指定员工',
'SCHEMA', N'Form',
'TABLE', N'FormStepUser'
GO


-- ----------------------------
-- Records of FormStepUser
-- ----------------------------

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
INSERT INTO [Form].[FormType] ([FormTypeId], [FormGroupId], [FormTypeNameCn], [FormTypeNameEn], [Prefix], [ApprovalPath], [ViewPath], [SortOrder], [DescriptionCn], [DescriptionEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1987217256446300160', N'1987215338470772736', N'请假单', N'Leave Request Form', N'LVR', N'formbusiness/forms/leaveform/leaveform_r', N'formbusiness/forms/leaveform/leaveform_v', N'1', N'请假单用于员工因个人事由、病假、事假、年假等原因需要离开工作岗位时，向所属部门及管理层提出请假申请、审批与备案的业务单据。该单据记录请假类型、请假时间、时长、事由以及审批流程，用于确保人员安排合理、流程合规与人事数据准确。', N'A Leave Request Form is used when an employee needs to be absent from work due to personal reasons, sickness, annual leave, or other approved leave types. The form is submitted to the employee’s department and management for approval and record-keeping. It captures the leave type, leave period, duration, reason, and approval workflow, ensuring proper staffing, compliance, and accurate HR records.', N'1903486709602062336', N'2025-11-09 01:54:49.000', N'1903486709602062336', N'2025-11-09 02:16:46.000')
GO


-- ----------------------------
-- Table structure for LeaveForm
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[LeaveForm]') AND type IN ('U'))
	DROP TABLE [Form].[LeaveForm]
GO

CREATE TABLE [Form].[LeaveForm] (
  [FormId] bigint  NOT NULL,
  [FormNo] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ApplicantTime] datetime2(7)  NOT NULL,
  [ApplicantUserNo] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ApplicantUserName] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ApplicantDeptId] bigint  NOT NULL,
  [ApplicantDeptName] nvarchar(40) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [LeaveTypeCode] int  NOT NULL,
  [LeaveReason] nvarchar(150) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [LeaveStartTime] datetime2(7)  NULL,
  [LeaveEndTime] datetime2(7)  NULL,
  [LeaveHours] decimal(6,2)  NOT NULL,
  [LeaveHandoverUserName] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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
'MS_Description', N'请假单单号',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'FormNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'申请时间',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'ApplicantTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'申请人工号',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'ApplicantUserNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'申请人姓名',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'ApplicantUserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'申请人部门Id',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'ApplicantDeptId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'申请人部门名称',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'ApplicantDeptName'
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
'COLUMN', N'LeaveHours'
GO

EXEC sp_addextendedproperty
'MS_Description', N'工作交接人姓名',
'SCHEMA', N'Form',
'TABLE', N'LeaveForm',
'COLUMN', N'LeaveHandoverUserName'
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

-- ----------------------------
-- Primary Key structure for table ControlInfo
-- ----------------------------
ALTER TABLE [Form].[ControlInfo] ADD CONSTRAINT [PK__FormCont__3399DDEB13F7B41D] PRIMARY KEY CLUSTERED ([ControlCode])
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
-- Primary Key structure for table FormInfo
-- ----------------------------
ALTER TABLE [Form].[FormInfo] ADD CONSTRAINT [PK__FormInfo__FB05B7DDADCFEA72] PRIMARY KEY CLUSTERED ([FormId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table FormStep
-- ----------------------------
ALTER TABLE [Form].[FormStep] ADD CONSTRAINT [PK__FormAppr__243433573D582151] PRIMARY KEY CLUSTERED ([StepId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table FormStepApproverRule
-- ----------------------------
ALTER TABLE [Form].[FormStepApproverRule] ADD CONSTRAINT [PK__FormStep__0A0392F2D13B9AA7] PRIMARY KEY CLUSTERED ([StepApproverRuleId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table FormStepDeptCriteria
-- ----------------------------
ALTER TABLE [Form].[FormStepDeptCriteria] ADD CONSTRAINT [PK__FormStep__0E5287F3D58490B5] PRIMARY KEY CLUSTERED ([StepDeptUserId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table FormStepOrg
-- ----------------------------
ALTER TABLE [Form].[FormStepOrg] ADD CONSTRAINT [PK__ss__B8A87239617A131F] PRIMARY KEY CLUSTERED ([StepOrgId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table FormStepUser
-- ----------------------------
ALTER TABLE [Form].[FormStepUser] ADD CONSTRAINT [PK__FormStep__8AB6E1C4820E75E2] PRIMARY KEY CLUSTERED ([StepUserId])
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

