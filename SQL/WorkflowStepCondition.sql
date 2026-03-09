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

 Date: 09/03/2026 13:04:37
*/


-- ----------------------------
-- Table structure for WorkflowStepCondition
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[WorkflowStepCondition]') AND type IN ('U'))
	DROP TABLE [Form].[WorkflowStepCondition]
GO

CREATE TABLE [Form].[WorkflowStepCondition] (
  [StepId] bigint  NOT NULL,
  [ConditionId] bigint  NULL,
  [ExecuteMatched] int  NOT NULL,
  [NextStepId] bigint  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime  NULL
)
GO

ALTER TABLE [Form].[WorkflowStepCondition] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批步骤Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepCondition',
'COLUMN', N'StepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'审批条件Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepCondition',
'COLUMN', N'ConditionId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'同时符合多条件时，是否执行此条件',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepCondition',
'COLUMN', N'ExecuteMatched'
GO

EXEC sp_addextendedproperty
'MS_Description', N'下一审批步骤Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepCondition',
'COLUMN', N'NextStepId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepCondition',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepCondition',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepCondition',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepCondition',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'步骤条件分支表',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepCondition'
GO


-- ----------------------------
-- Records of WorkflowStepCondition
-- ----------------------------
INSERT INTO [Form].[WorkflowStepCondition] ([StepId], [ConditionId], [ExecuteMatched], [NextStepId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009890853346217984', N'-1', N'1', N'2009892923604340736', N'1', N'2026-01-10 16:02:15.000', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowStepCondition] ([StepId], [ConditionId], [ExecuteMatched], [NextStepId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009892923604340736', N'-1', N'1', N'2009897830268932096', N'1', N'2026-01-10 16:02:40.000', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowStepCondition] ([StepId], [ConditionId], [ExecuteMatched], [NextStepId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009897830268932096', N'-1', N'1', N'2009898117243211776', N'1', N'2026-01-10 16:03:21.000', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowStepCondition] ([StepId], [ConditionId], [ExecuteMatched], [NextStepId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009898117243211776', N'-1', N'1', N'2029389483455156224', N'1', N'2026-01-12 15:00:50.723', NULL, NULL)
GO

INSERT INTO [Form].[WorkflowStepCondition] ([StepId], [ConditionId], [ExecuteMatched], [NextStepId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2029389483455156224', N'-1', N'1', N'-1', N'1', N'2026-01-12 15:00:50.723', NULL, NULL)
GO

