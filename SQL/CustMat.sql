/*
 Navicat Premium Dump SQL

 Source Server         : 127.0.0.1
 Source Server Type    : SQL Server
 Source Server Version : 16001160 (16.00.1160)
 Source Host           : localhost:1433
 Source Catalog        : SystemAdmin
 Source Schema         : CustMat

 Target Server Type    : SQL Server
 Target Server Version : 16001160 (16.00.1160)
 File Encoding         : 65001

 Date: 21/12/2025 18:24:14
*/


-- ----------------------------
-- Table structure for CustomerInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[CustMat].[CustomerInfo]') AND type IN ('U'))
	DROP TABLE [CustMat].[CustomerInfo]
GO

CREATE TABLE [CustMat].[CustomerInfo] (
  [CustomerId] bigint  NOT NULL,
  [CustomerCode] nvarchar(15) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [CustomerNameCn] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [CustomerNameEn] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Description] nvarchar(200) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NULL,
  [CreatedDate] datetime DEFAULT getdate() NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
)
GO

ALTER TABLE [CustMat].[CustomerInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'客户Id',
'SCHEMA', N'CustMat',
'TABLE', N'CustomerInfo',
'COLUMN', N'CustomerId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'客户编码',
'SCHEMA', N'CustMat',
'TABLE', N'CustomerInfo',
'COLUMN', N'CustomerCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'客户名称（中文）',
'SCHEMA', N'CustMat',
'TABLE', N'CustomerInfo',
'COLUMN', N'CustomerNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'客户名称（英文）',
'SCHEMA', N'CustMat',
'TABLE', N'CustomerInfo',
'COLUMN', N'CustomerNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'客户描述',
'SCHEMA', N'CustMat',
'TABLE', N'CustomerInfo',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'CustMat',
'TABLE', N'CustomerInfo',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'CustMat',
'TABLE', N'CustomerInfo',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'CustMat',
'TABLE', N'CustomerInfo',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'CustMat',
'TABLE', N'CustomerInfo',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'客户信息表',
'SCHEMA', N'CustMat',
'TABLE', N'CustomerInfo'
GO


-- ----------------------------
-- Records of CustomerInfo
-- ----------------------------
INSERT INTO [CustMat].[CustomerInfo] ([CustomerId], [CustomerCode], [CustomerNameCn], [CustomerNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1982713161769422848', N'TM001', N'特斯拉', N'Tesla', N'', N'1903486709602062336', N'2025-10-27 15:37:09.000', NULL, NULL)
GO


-- ----------------------------
-- Table structure for ManufacturerInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[CustMat].[ManufacturerInfo]') AND type IN ('U'))
	DROP TABLE [CustMat].[ManufacturerInfo]
GO

CREATE TABLE [CustMat].[ManufacturerInfo] (
  [ManufacturerId] bigint  NOT NULL,
  [ManufacturerCode] nvarchar(15) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ManufacturerNameCn] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ManufacturerNameEn] nvarchar(60) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Email] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [Fax] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [Description] nvarchar(200) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NULL,
  [CreatedDate] datetime DEFAULT getdate() NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
)
GO

ALTER TABLE [CustMat].[ManufacturerInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'厂商Id',
'SCHEMA', N'CustMat',
'TABLE', N'ManufacturerInfo',
'COLUMN', N'ManufacturerId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'厂商编码',
'SCHEMA', N'CustMat',
'TABLE', N'ManufacturerInfo',
'COLUMN', N'ManufacturerCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'厂商名称（中文）',
'SCHEMA', N'CustMat',
'TABLE', N'ManufacturerInfo',
'COLUMN', N'ManufacturerNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'厂商名称（英文）',
'SCHEMA', N'CustMat',
'TABLE', N'ManufacturerInfo',
'COLUMN', N'ManufacturerNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'厂商邮箱',
'SCHEMA', N'CustMat',
'TABLE', N'ManufacturerInfo',
'COLUMN', N'Email'
GO

EXEC sp_addextendedproperty
'MS_Description', N'厂商传真',
'SCHEMA', N'CustMat',
'TABLE', N'ManufacturerInfo',
'COLUMN', N'Fax'
GO

EXEC sp_addextendedproperty
'MS_Description', N'厂商描述',
'SCHEMA', N'CustMat',
'TABLE', N'ManufacturerInfo',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'CustMat',
'TABLE', N'ManufacturerInfo',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'CustMat',
'TABLE', N'ManufacturerInfo',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'CustMat',
'TABLE', N'ManufacturerInfo',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'CustMat',
'TABLE', N'ManufacturerInfo',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'厂商信息表',
'SCHEMA', N'CustMat',
'TABLE', N'ManufacturerInfo'
GO


-- ----------------------------
-- Records of ManufacturerInfo
-- ----------------------------
INSERT INTO [CustMat].[ManufacturerInfo] ([ManufacturerId], [ManufacturerCode], [ManufacturerNameCn], [ManufacturerNameEn], [Email], [Fax], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1', N'QD001A', N'千代达电子制造(苏州)有限公司', N'Chidida Electronics Manufacturing (Suzhou) Co., Ltd.', N'', N'', N'', NULL, N'2025-11-02 13:04:26.720', N'1903486709602062336', N'2025-11-03 20:39:25.000')
GO

INSERT INTO [CustMat].[ManufacturerInfo] ([ManufacturerId], [ManufacturerCode], [ManufacturerNameCn], [ManufacturerNameEn], [Email], [Fax], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1984873153440124928', N'VYS0015RM0', N'江苏永硕电子有限公司', N'Jiangsu Yongshuo Electronics Co., Ltd.', N'', N'', N'', N'1903486709602062336', N'2025-11-02 14:40:11.000', NULL, NULL)
GO

INSERT INTO [CustMat].[ManufacturerInfo] ([ManufacturerId], [ManufacturerCode], [ManufacturerNameCn], [ManufacturerNameEn], [Email], [Fax], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1985007595626303488', N'VRD0001RM0', N'苏州日东迈特科思电子有限公司', N'Suzhou Nitto Mitecos Electronics Co., Ltd.', N'', N'', N'', N'1903486709602062336', N'2025-11-02 23:34:25.000', NULL, NULL)
GO

INSERT INTO [CustMat].[ManufacturerInfo] ([ManufacturerId], [ManufacturerCode], [ManufacturerNameCn], [ManufacturerNameEn], [Email], [Fax], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1985007732314476544', N'JX017A', N'江苏集鑫成精密工业有限公司', N'Jiangsu Jixincheng Precision Industry Co., Ltd.', N'', N'', N'', N'1903486709602062336', N'2025-11-02 23:34:57.000', NULL, NULL)
GO

INSERT INTO [CustMat].[ManufacturerInfo] ([ManufacturerId], [ManufacturerCode], [ManufacturerNameCn], [ManufacturerNameEn], [Email], [Fax], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1985007849155203072', N'VJY0003RM0', N'余姚市久源液压技术有限公司', N'Yuyao Jiuyuan Hydraulic Technology Co., Ltd.', N'', N'', N'', N'1903486709602062336', N'2025-11-02 23:35:25.000', NULL, NULL)
GO

INSERT INTO [CustMat].[ManufacturerInfo] ([ManufacturerId], [ManufacturerCode], [ManufacturerNameCn], [ManufacturerNameEn], [Email], [Fax], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1985013683859886080', N'VSY0010RM0', N'昆山圣焱电子有限公司', N'Kunshan Shengyan Electronics Co., Ltd.', N'', N'', N'', N'1903486709602062336', N'2025-11-02 23:58:36.000', NULL, NULL)
GO

INSERT INTO [CustMat].[ManufacturerInfo] ([ManufacturerId], [ManufacturerCode], [ManufacturerNameCn], [ManufacturerNameEn], [Email], [Fax], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1985014168448798720', N'VRW0002RM0', N'无锡仁伟电子科技有限公司', N'Wuxi Renwei Electronic Technology Co., Ltd.', N'', N'', N'', N'1903486709602062336', N'2025-11-03 00:00:32.000', NULL, NULL)
GO

INSERT INTO [CustMat].[ManufacturerInfo] ([ManufacturerId], [ManufacturerCode], [ManufacturerNameCn], [ManufacturerNameEn], [Email], [Fax], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1985014255228948480', N'13885268Q0', N'苏州弗洛蒙斯电子科技有限公司', N'Suzhou Flormons Electronic Technology Co., Ltd.', N'', N'', N'', N'1903486709602062336', N'2025-11-03 00:00:52.000', NULL, NULL)
GO

INSERT INTO [CustMat].[ManufacturerInfo] ([ManufacturerId], [ManufacturerCode], [ManufacturerNameCn], [ManufacturerNameEn], [Email], [Fax], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1985014345150631936', N'VPJ0001RM0', N'苏州品基电子科技有限公司', N'Suzhou Pinji Electronic Technology Co., Ltd.', N'', N'', N'', N'1903486709602062336', N'2025-11-03 00:01:14.000', NULL, NULL)
GO

INSERT INTO [CustMat].[ManufacturerInfo] ([ManufacturerId], [ManufacturerCode], [ManufacturerNameCn], [ManufacturerNameEn], [Email], [Fax], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1985014460510769152', N'5969850220', N'苏州诚鸿煜精密制造有限公司', N'Suzhou Chenghongyu Precision Manufacturing Co., Ltd.', N'', N'', N'', N'1903486709602062336', N'2025-11-03 00:01:41.000', NULL, NULL)
GO

INSERT INTO [CustMat].[ManufacturerInfo] ([ManufacturerId], [ManufacturerCode], [ManufacturerNameCn], [ManufacturerNameEn], [Email], [Fax], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1985014560221958144', N'ZL004A', N'昆山振良电子塑胶五金有限公司', N'Kunshan Zhenliang Electronic Plastic Hardware Co., Ltd.', N'', N'', N'', N'1903486709602062336', N'2025-11-03 00:02:05.000', NULL, NULL)
GO

INSERT INTO [CustMat].[ManufacturerInfo] ([ManufacturerId], [ManufacturerCode], [ManufacturerNameCn], [ManufacturerNameEn], [Email], [Fax], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1985014997176160256', N'VSJ0001RM0', N'宁波生久科技有限公司', N'Ningbo Shengjiu Technology Co., Ltd.', N'', N'', N'', N'1903486709602062336', N'2025-11-03 00:03:49.000', NULL, NULL)
GO


-- ----------------------------
-- Table structure for PartNumberInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[CustMat].[PartNumberInfo]') AND type IN ('U'))
	DROP TABLE [CustMat].[PartNumberInfo]
GO

CREATE TABLE [CustMat].[PartNumberInfo] (
  [PartNumberId] bigint  NOT NULL,
  [ManufacturerId] bigint  NOT NULL,
  [PartNumberNo] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ProductName] nvarchar(70) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Specifications] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [CreatedBy] bigint  NULL,
  [CreatedDate] datetime DEFAULT getdate() NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
)
GO

ALTER TABLE [CustMat].[PartNumberInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'料号Id',
'SCHEMA', N'CustMat',
'TABLE', N'PartNumberInfo',
'COLUMN', N'PartNumberId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'所属厂商Id',
'SCHEMA', N'CustMat',
'TABLE', N'PartNumberInfo',
'COLUMN', N'ManufacturerId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'料号编码',
'SCHEMA', N'CustMat',
'TABLE', N'PartNumberInfo',
'COLUMN', N'PartNumberNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'品名',
'SCHEMA', N'CustMat',
'TABLE', N'PartNumberInfo',
'COLUMN', N'ProductName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'规格',
'SCHEMA', N'CustMat',
'TABLE', N'PartNumberInfo',
'COLUMN', N'Specifications'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'CustMat',
'TABLE', N'PartNumberInfo',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'CustMat',
'TABLE', N'PartNumberInfo',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'CustMat',
'TABLE', N'PartNumberInfo',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'CustMat',
'TABLE', N'PartNumberInfo',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'料号信息表',
'SCHEMA', N'CustMat',
'TABLE', N'PartNumberInfo'
GO


-- ----------------------------
-- Records of PartNumberInfo
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table CustomerInfo
-- ----------------------------
ALTER TABLE [CustMat].[CustomerInfo] ADD CONSTRAINT [PK__Customer__06678520568DEE5F] PRIMARY KEY CLUSTERED ([CustomerId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table ManufacturerInfo
-- ----------------------------
ALTER TABLE [CustMat].[ManufacturerInfo] ADD CONSTRAINT [PK__Manufact__357E5CC10DB4EAF9] PRIMARY KEY CLUSTERED ([ManufacturerId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table PartNumberInfo
-- ----------------------------
ALTER TABLE [CustMat].[PartNumberInfo] ADD CONSTRAINT [PK__PartNumb__FD9D7FB2AA81EFB9] PRIMARY KEY CLUSTERED ([PartNumberId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

