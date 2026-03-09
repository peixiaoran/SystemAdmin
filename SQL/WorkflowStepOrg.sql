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

 Date: 09/03/2026 13:04:18
*/


-- ----------------------------
-- Table structure for WorkflowStepOrg
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Form].[WorkflowStepOrg]') AND type IN ('U'))
	DROP TABLE [Form].[WorkflowStepOrg]
GO

CREATE TABLE [Form].[WorkflowStepOrg] (
  [StepOrgId] bigint  NOT NULL,
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
'MS_Description', N'审批步骤依照组织架构Id',
'SCHEMA', N'Form',
'TABLE', N'WorkflowStepOrg',
'COLUMN', N'StepOrgId'
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
INSERT INTO [Form].[WorkflowStepOrg] ([StepOrgId], [StepId], [DeptLeaveId], [PositionId], [CreatedBy], [CreatedDate]) VALUES (N'2009892923646283776', N'2009892923604340736', N'1949169142347206656', N'1351602631784529920', N'1903486709602062336', N'2026-01-10 15:39:49.000')
GO

INSERT INTO [Form].[WorkflowStepOrg] ([StepOrgId], [StepId], [DeptLeaveId], [PositionId], [CreatedBy], [CreatedDate]) VALUES (N'2009897830289903616', N'2009897830268932096', N'1949168956883472384', N'1351600746193223680', N'1903486709602062336', N'2026-01-10 15:59:19.000')
GO

INSERT INTO [Form].[WorkflowStepOrg] ([StepOrgId], [StepId], [DeptLeaveId], [PositionId], [CreatedBy], [CreatedDate]) VALUES (N'2009898117259988992', N'2009898117243211776', N'1949167957770899456', N'1351585319710883840', N'1903486709602062336', N'2026-01-10 16:00:27.000')
GO

INSERT INTO [Form].[WorkflowStepOrg] ([StepOrgId], [StepId], [DeptLeaveId], [PositionId], [CreatedBy], [CreatedDate]) VALUES (N'2029389483677454336', N'2029389483455156224', N'1949167957770899456', N'1351584156689104896', N'1903486709602062336', N'2026-03-05 10:52:11.800')
GO


-- ----------------------------
-- Primary Key structure for table WorkflowStepOrg
-- ----------------------------
ALTER TABLE [Form].[WorkflowStepOrg] ADD CONSTRAINT [PK__ss__B8A87239617A131F] PRIMARY KEY CLUSTERED ([StepOrgId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

