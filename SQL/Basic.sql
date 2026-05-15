/*
 Navicat Premium Dump SQL

 Source Server         : 127.0.0.1
 Source Server Type    : SQL Server
 Source Server Version : 17001115 (17.00.1115)
 Source Host           : 127.0.0.1:1433
 Source Catalog        : SystemAdmin
 Source Schema         : Basic

 Target Server Type    : SQL Server
 Target Server Version : 17001115 (17.00.1115)
 File Encoding         : 65001

 Date: 15/05/2026 17:18:43
*/


-- ----------------------------
-- Table structure for CurrencyInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[CurrencyInfo]') AND type IN ('U'))
	DROP TABLE [Basic].[CurrencyInfo]
GO

CREATE TABLE [Basic].[CurrencyInfo] (
  [CurrencyId] bigint  NOT NULL,
  [CurrencyCode] nvarchar(10) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [CurrencyNameCn] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [CurrencyNameEn] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [SortOrder] int  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[CurrencyInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'币别Id',
'SCHEMA', N'Basic',
'TABLE', N'CurrencyInfo',
'COLUMN', N'CurrencyId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'币别编码',
'SCHEMA', N'Basic',
'TABLE', N'CurrencyInfo',
'COLUMN', N'CurrencyCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'币别名称（中文）',
'SCHEMA', N'Basic',
'TABLE', N'CurrencyInfo',
'COLUMN', N'CurrencyNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'币别名称（英文）',
'SCHEMA', N'Basic',
'TABLE', N'CurrencyInfo',
'COLUMN', N'CurrencyNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'Basic',
'TABLE', N'CurrencyInfo',
'COLUMN', N'SortOrder'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'CurrencyInfo',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'CurrencyInfo',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'CurrencyInfo',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'CurrencyInfo',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'币别信息表',
'SCHEMA', N'Basic',
'TABLE', N'CurrencyInfo'
GO


-- ----------------------------
-- Records of CurrencyInfo
-- ----------------------------
INSERT INTO [Basic].[CurrencyInfo] ([CurrencyId], [CurrencyCode], [CurrencyNameCn], [CurrencyNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1943894445606965248', N'CNY', N'人民币', N'Chinese Yuan', N'1', N'1903486709602062336', N'2026-01-02 11:10:32.000', NULL, NULL)
GO

INSERT INTO [Basic].[CurrencyInfo] ([CurrencyId], [CurrencyCode], [CurrencyNameCn], [CurrencyNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1944768753686417408', N'USD', N'美元', N'US Dollar', N'3', N'1903486709602062336', N'2026-01-02 11:10:32.000', NULL, NULL)
GO

INSERT INTO [Basic].[CurrencyInfo] ([CurrencyId], [CurrencyCode], [CurrencyNameCn], [CurrencyNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1945119918697615360', N'VND', N'越南盾', N'Vietnamese Dong', N'6', N'1903486709602062336', N'2026-01-02 11:10:32.000', NULL, NULL)
GO

INSERT INTO [Basic].[CurrencyInfo] ([CurrencyId], [CurrencyCode], [CurrencyNameCn], [CurrencyNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1945128998946344960', N'TWD', N'新台币', N'New Taiwan Dollar', N'2', N'1903486709602062336', N'2026-01-02 11:10:32.000', NULL, NULL)
GO

INSERT INTO [Basic].[CurrencyInfo] ([CurrencyId], [CurrencyCode], [CurrencyNameCn], [CurrencyNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969072513866665984', N'MXN', N'墨西哥比索', N'Mexican Peso', N'4', N'1903486709602062336', N'2026-01-02 11:10:32.000', NULL, NULL)
GO

INSERT INTO [Basic].[CurrencyInfo] ([CurrencyId], [CurrencyCode], [CurrencyNameCn], [CurrencyNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969074225868312576', N'GBP', N'英镑', N'British Pound', N'7', N'1903486709602062336', N'2026-01-02 11:10:32.000', NULL, NULL)
GO

INSERT INTO [Basic].[CurrencyInfo] ([CurrencyId], [CurrencyCode], [CurrencyNameCn], [CurrencyNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969074544509587456', N'JPY', N'日元', N'Japanese Yen', N'8', N'1903486709602062336', N'2026-01-02 11:10:32.000', NULL, NULL)
GO

INSERT INTO [Basic].[CurrencyInfo] ([CurrencyId], [CurrencyCode], [CurrencyNameCn], [CurrencyNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2006925399635922944', N'SGD', N'新加坡元', N'Singapore Dollar', N'5', N'1903486709602062336', N'2026-01-02 11:10:32.000', NULL, NULL)
GO


-- ----------------------------
-- Table structure for DepartmentInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[DepartmentInfo]') AND type IN ('U'))
	DROP TABLE [Basic].[DepartmentInfo]
GO

CREATE TABLE [Basic].[DepartmentInfo] (
  [DepartmentId] bigint  NOT NULL,
  [DepartmentCode] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [DepartmentNameCn] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [DepartmentNameEn] nvarchar(60) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ParentId] bigint  NULL,
  [DepartmentLevelId] bigint  NOT NULL,
  [SortOrder] int DEFAULT 0 NOT NULL,
  [Landline] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [Email] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Address] nvarchar(200) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Description] nvarchar(200) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[DepartmentInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门Id',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo',
'COLUMN', N'DepartmentId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门编码',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo',
'COLUMN', N'DepartmentCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门名称（中文）',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo',
'COLUMN', N'DepartmentNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门名称（英文）',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo',
'COLUMN', N'DepartmentNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'上级部门',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo',
'COLUMN', N'ParentId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门级别Id',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo',
'COLUMN', N'DepartmentLevelId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo',
'COLUMN', N'SortOrder'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门座机',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo',
'COLUMN', N'Landline'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门邮箱',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo',
'COLUMN', N'Email'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门地址',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo',
'COLUMN', N'Address'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门描述',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门信息表',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentInfo'
GO


-- ----------------------------
-- Records of DepartmentInfo
-- ----------------------------
INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000001', N'7001', N'董事会', N'Board of Directors', N'0', N'1350917348311171072', N'1', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000002', N'7002', N'亚洲营运中心', N'Asia Operations Center', N'1950000000000000001', N'1351403528752463872', N'2', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000003', N'7003', N'伺服器产品部', N'Server Products Department', N'1950000000000000002', N'1949167957770899456', N'3', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000004', N'7004', N'汽车产品部', N'Automotive Products Department', N'1950000000000000002', N'1949167957770899456', N'4', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000005', N'7005', N'资讯部', N'Information Technology Department', N'1950000000000000002', N'1949167957770899456', N'5', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000006', N'7006', N'稽核部', N'Audit Department', N'1950000000000000002', N'1949167957770899456', N'6', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000007', N'7007', N'伺服器专案课', N'Server Project Section', N'1950000000000000003', N'1949168956883472384', N'7', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000008', N'7008', N'伺服器专案课一组', N'Server Project Section Team 一组', N'1950000000000000007', N'1949169142347206656', N'8', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000009', N'7009', N'伺服器专案课二组', N'Server Project Section Team 二组', N'1950000000000000007', N'1949169142347206656', N'9', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000010', N'7010', N'伺服器专案课三组', N'Server Project Section Team 三组', N'1950000000000000007', N'1949169142347206656', N'10', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000011', N'7011', N'伺服器专案课四组', N'Server Project Section Team 四组', N'1950000000000000007', N'1949169142347206656', N'11', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000012', N'7012', N'伺服器开发业务课', N'Server Development Business Section', N'1950000000000000003', N'1949168956883472384', N'12', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000013', N'7013', N'伺服器开发业务课一组', N'Server Development Business Section Team 一组', N'1950000000000000012', N'1949169142347206656', N'13', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000014', N'7014', N'伺服器开发业务课二组', N'Server Development Business Section Team 二组', N'1950000000000000012', N'1949169142347206656', N'14', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000015', N'7015', N'伺服器开发业务课三组', N'Server Development Business Section Team 三组', N'1950000000000000012', N'1949169142347206656', N'15', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000016', N'7016', N'伺服器开发业务课四组', N'Server Development Business Section Team 四组', N'1950000000000000012', N'1949169142347206656', N'16', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000017', N'7017', N'伺服器业务支援课', N'Server Business Support Section', N'1950000000000000003', N'1949168956883472384', N'17', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000018', N'7018', N'伺服器业务支援课一组', N'Server Business Support Section Team 一组', N'1950000000000000017', N'1949169142347206656', N'18', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000019', N'7019', N'伺服器业务支援课二组', N'Server Business Support Section Team 二组', N'1950000000000000017', N'1949169142347206656', N'19', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000020', N'7020', N'伺服器业务支援课三组', N'Server Business Support Section Team 三组', N'1950000000000000017', N'1949169142347206656', N'20', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000021', N'7021', N'伺服器业务支援课四组', N'Server Business Support Section Team 四组', N'1950000000000000017', N'1949169142347206656', N'21', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000022', N'7022', N'伺服器客户导入课', N'Server Customer Introduction Section', N'1950000000000000003', N'1949168956883472384', N'22', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000023', N'7023', N'伺服器客户导入课一组', N'Server Customer Introduction Section Team 一组', N'1950000000000000022', N'1949169142347206656', N'23', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000024', N'7024', N'伺服器客户导入课二组', N'Server Customer Introduction Section Team 二组', N'1950000000000000022', N'1949169142347206656', N'24', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000025', N'7025', N'伺服器客户导入课三组', N'Server Customer Introduction Section Team 三组', N'1950000000000000022', N'1949169142347206656', N'25', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000026', N'7026', N'伺服器客户导入课四组', N'Server Customer Introduction Section Team 四组', N'1950000000000000022', N'1949169142347206656', N'26', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000027', N'7027', N'汽车专案课', N'Automotive Project Section', N'1950000000000000004', N'1949168956883472384', N'27', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000028', N'7028', N'汽车专案课一组', N'Automotive Project Section Team 一组', N'1950000000000000027', N'1949169142347206656', N'28', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000029', N'7029', N'汽车专案课二组', N'Automotive Project Section Team 二组', N'1950000000000000027', N'1949169142347206656', N'29', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000030', N'7030', N'汽车专案课三组', N'Automotive Project Section Team 三组', N'1950000000000000027', N'1949169142347206656', N'30', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000031', N'7031', N'汽车专案课四组', N'Automotive Project Section Team 四组', N'1950000000000000027', N'1949169142347206656', N'31', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000032', N'7032', N'汽车开发业务课', N'Automotive Development Business Section', N'1950000000000000004', N'1949168956883472384', N'32', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000033', N'7033', N'汽车开发业务课一组', N'Automotive Development Business Section Team 一组', N'1950000000000000032', N'1949169142347206656', N'33', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000034', N'7034', N'汽车开发业务课二组', N'Automotive Development Business Section Team 二组', N'1950000000000000032', N'1949169142347206656', N'34', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000035', N'7035', N'汽车开发业务课三组', N'Automotive Development Business Section Team 三组', N'1950000000000000032', N'1949169142347206656', N'35', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000036', N'7036', N'汽车开发业务课四组', N'Automotive Development Business Section Team 四组', N'1950000000000000032', N'1949169142347206656', N'36', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000037', N'7037', N'汽车业务支援课', N'Automotive Business Support Section', N'1950000000000000004', N'1949168956883472384', N'37', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000038', N'7038', N'汽车业务支援课一组', N'Automotive Business Support Section Team 一组', N'1950000000000000037', N'1949169142347206656', N'38', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000039', N'7039', N'汽车业务支援课二组', N'Automotive Business Support Section Team 二组', N'1950000000000000037', N'1949169142347206656', N'39', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000040', N'7040', N'汽车业务支援课三组', N'Automotive Business Support Section Team 三组', N'1950000000000000037', N'1949169142347206656', N'40', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000041', N'7041', N'汽车业务支援课四组', N'Automotive Business Support Section Team 四组', N'1950000000000000037', N'1949169142347206656', N'41', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000042', N'7042', N'汽车客户导入课', N'Automotive Customer Introduction Section', N'1950000000000000004', N'1949168956883472384', N'42', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000043', N'7043', N'汽车客户导入课一组', N'Automotive Customer Introduction Section Team 一组', N'1950000000000000042', N'1949169142347206656', N'43', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000044', N'7044', N'汽车客户导入课二组', N'Automotive Customer Introduction Section Team 二组', N'1950000000000000042', N'1949169142347206656', N'44', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000045', N'7045', N'汽车客户导入课三组', N'Automotive Customer Introduction Section Team 三组', N'1950000000000000042', N'1949169142347206656', N'45', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000046', N'7046', N'汽车客户导入课四组', N'Automotive Customer Introduction Section Team 四组', N'1950000000000000042', N'1949169142347206656', N'46', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000047', N'7047', N'系统管理科', N'Systems Administration Section', N'1950000000000000005', N'1949168956883472384', N'47', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000048', N'7048', N'系统开发组', N'System Development Team', N'1950000000000000047', N'1949169142347206656', N'48', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000049', N'7049', N'网路管理组', N'Network Administration Team', N'1950000000000000047', N'1949169142347206656', N'49', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000050', N'7050', N'机房管理组', N'Data Center Management Team', N'1950000000000000047', N'1949169142347206656', N'50', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000051', N'7051', N'应用支援科', N'Application Support Section', N'1950000000000000005', N'1949168956883472384', N'51', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000052', N'7052', N'ERP支援组', N'ERP支援组', N'1950000000000000051', N'1949169142347206656', N'52', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000053', N'7053', N'MES支援组', N'MES支援组', N'1950000000000000051', N'1949169142347206656', N'53', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000054', N'7054', N'办公系统组', N'办公系统组', N'1950000000000000051', N'1949169142347206656', N'54', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000055', N'7055', N'内部稽核科', N'Internal Audit Section', N'1950000000000000006', N'1949168956883472384', N'55', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000056', N'7056', N'营运稽核组', N'营运稽核组', N'1950000000000000055', N'1949169142347206656', N'56', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000057', N'7057', N'专案稽核组', N'专案稽核组', N'1950000000000000055', N'1949169142347206656', N'57', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000058', N'7058', N'改善追踪组', N'改善追踪组', N'1950000000000000055', N'1949169142347206656', N'58', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000059', N'7059', N'合规管理科', N'Compliance Management Section', N'1950000000000000006', N'1949168956883472384', N'59', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000060', N'7060', N'制度管理组', N'制度管理组', N'1950000000000000059', N'1949169142347206656', N'60', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000061', N'7061', N'法遵执行组', N'法遵执行组', N'1950000000000000059', N'1949169142347206656', N'61', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000062', N'7062', N'风险控管组', N'风险控管组', N'1950000000000000059', N'1949169142347206656', N'62', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000063', N'7063', N'第一厂厂长室', N'Plant 1 Director Office', N'1950000000000000002', N'1351405026328707072', N'63', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000064', N'7064', N'第二厂厂长室', N'Plant 2 Director Office', N'1950000000000000002', N'1351405026328707072', N'64', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000065', N'7065', N'业务部', N'Sales Department', N'1950000000000000063', N'1949167957770899456', N'65', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000066', N'7066', N'业务一科', N'Sales Section 1', N'1950000000000000065', N'1949168956883472384', N'66', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000067', N'7067', N'业务一科A组', N'Sales Section 1 A', N'1950000000000000066', N'1949169142347206656', N'67', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000068', N'7068', N'业务一科B组', N'Sales Section 1 B', N'1950000000000000066', N'1949169142347206656', N'68', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000069', N'7069', N'业务一科C组', N'Sales Section 1 C', N'1950000000000000066', N'1949169142347206656', N'69', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000070', N'7070', N'业务二科', N'Sales Section 2', N'1950000000000000065', N'1949168956883472384', N'70', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000071', N'7071', N'业务二科A组', N'Sales Section 2 A', N'1950000000000000070', N'1949169142347206656', N'71', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000072', N'7072', N'业务二科B组', N'Sales Section 2 B', N'1950000000000000070', N'1949169142347206656', N'72', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000073', N'7073', N'业务二科C组', N'Sales Section 2 C', N'1950000000000000070', N'1949169142347206656', N'73', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000074', N'7074', N'客户服务科', N'Customer Service Section', N'1950000000000000065', N'1949168956883472384', N'74', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000075', N'7075', N'客户服务科A组', N'Customer Service Section A组', N'1950000000000000074', N'1949169142347206656', N'75', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000076', N'7076', N'客户服务科B组', N'Customer Service Section B组', N'1950000000000000074', N'1949169142347206656', N'76', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000077', N'7077', N'客户服务科C组', N'Customer Service Section C组', N'1950000000000000074', N'1949169142347206656', N'77', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000078', N'7078', N'资材部', N'Materials Department', N'1950000000000000063', N'1949167957770899456', N'78', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000079', N'7079', N'物料控制科', N'Material Control Section', N'1950000000000000078', N'1949168956883472384', N'79', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000080', N'7080', N'物料控制科A组', N'Material Control Section A组', N'1950000000000000079', N'1949169142347206656', N'80', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000081', N'7081', N'物料控制科B组', N'Material Control Section B组', N'1950000000000000079', N'1949169142347206656', N'81', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000082', N'7082', N'物料控制科C组', N'Material Control Section C组', N'1950000000000000079', N'1949169142347206656', N'82', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000083', N'7083', N'仓储管理科', N'Warehouse Management Section', N'1950000000000000078', N'1949168956883472384', N'83', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000084', N'7084', N'仓储管理科A组', N'Warehouse Management Section A组', N'1950000000000000083', N'1949169142347206656', N'84', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000085', N'7085', N'仓储管理科B组', N'Warehouse Management Section B组', N'1950000000000000083', N'1949169142347206656', N'85', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000086', N'7086', N'仓储管理科C组', N'Warehouse Management Section C组', N'1950000000000000083', N'1949169142347206656', N'86', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000087', N'7087', N'生管协调科', N'Production Planning Coordination Section', N'1950000000000000078', N'1949168956883472384', N'87', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000088', N'7088', N'生管协调科A组', N'Production Planning Coordination Section A组', N'1950000000000000087', N'1949169142347206656', N'88', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000089', N'7089', N'生管协调科B组', N'Production Planning Coordination Section B组', N'1950000000000000087', N'1949169142347206656', N'89', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000090', N'7090', N'生管协调科C组', N'Production Planning Coordination Section C组', N'1950000000000000087', N'1949169142347206656', N'90', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000091', N'7091', N'模修部', N'Mold Repair Department', N'1950000000000000063', N'1949167957770899456', N'91', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000092', N'7092', N'模具保养科', N'Mold Maintenance Section', N'1950000000000000091', N'1949168956883472384', N'92', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000093', N'7093', N'模具保养科A组', N'Mold Maintenance Section A组', N'1950000000000000092', N'1949169142347206656', N'93', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000094', N'7094', N'模具保养科B组', N'Mold Maintenance Section B组', N'1950000000000000092', N'1949169142347206656', N'94', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000095', N'7095', N'模具保养科C组', N'Mold Maintenance Section C组', N'1950000000000000092', N'1949169142347206656', N'95', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000096', N'7096', N'模具维修科', N'Mold Repair Section', N'1950000000000000091', N'1949168956883472384', N'96', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000097', N'7097', N'模具维修科A组', N'Mold Repair Section A组', N'1950000000000000096', N'1949169142347206656', N'97', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000098', N'7098', N'模具维修科B组', N'Mold Repair Section B组', N'1950000000000000096', N'1949169142347206656', N'98', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000099', N'7099', N'模具维修科C组', N'Mold Repair Section C组', N'1950000000000000096', N'1949169142347206656', N'99', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000100', N'7100', N'治工具管理科', N'Tooling Management Section', N'1950000000000000091', N'1949168956883472384', N'100', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000101', N'7101', N'治工具管理科A组', N'Tooling Management Section A组', N'1950000000000000100', N'1949169142347206656', N'101', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000102', N'7102', N'治工具管理科B组', N'Tooling Management Section B组', N'1950000000000000100', N'1949169142347206656', N'102', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000103', N'7103', N'治工具管理科C组', N'Tooling Management Section C组', N'1950000000000000100', N'1949169142347206656', N'103', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000104', N'7104', N'成型部', N'Forming Department', N'1950000000000000063', N'1949167957770899456', N'104', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000105', N'7105', N'成型一科', N'Forming Section 1', N'1950000000000000104', N'1949168956883472384', N'105', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000106', N'7106', N'成型一科A组', N'Forming Section 1 A组', N'1950000000000000105', N'1949169142347206656', N'106', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000107', N'7107', N'成型一科B组', N'Forming Section 1 B组', N'1950000000000000105', N'1949169142347206656', N'107', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000108', N'7108', N'成型一科C组', N'Forming Section 1 C组', N'1950000000000000105', N'1949169142347206656', N'108', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000109', N'7109', N'成型二科', N'Forming Section 2', N'1950000000000000104', N'1949168956883472384', N'109', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000110', N'7110', N'成型二科A组', N'Forming Section 2 A组', N'1950000000000000109', N'1949169142347206656', N'110', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000111', N'7111', N'成型二科B组', N'Forming Section 2 B组', N'1950000000000000109', N'1949169142347206656', N'111', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000112', N'7112', N'成型二科C组', N'Forming Section 2 C组', N'1950000000000000109', N'1949169142347206656', N'112', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000113', N'7113', N'制程技术科', N'Process Technology Section', N'1950000000000000104', N'1949168956883472384', N'113', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000114', N'7114', N'制程技术科A组', N'Process Technology Section A组', N'1950000000000000113', N'1949169142347206656', N'114', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000115', N'7115', N'制程技术科B组', N'Process Technology Section B组', N'1950000000000000113', N'1949169142347206656', N'115', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000116', N'7116', N'制程技术科C组', N'Process Technology Section C组', N'1950000000000000113', N'1949169142347206656', N'116', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000117', N'7117', N'组装部', N'Assembly Department', N'1950000000000000063', N'1949167957770899456', N'117', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000118', N'7118', N'组装一科', N'Assembly Section 1', N'1950000000000000117', N'1949168956883472384', N'118', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000119', N'7119', N'组装一科A组', N'Assembly Section 1 A组', N'1950000000000000118', N'1949169142347206656', N'119', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000120', N'7120', N'组装一科B组', N'Assembly Section 1 B组', N'1950000000000000118', N'1949169142347206656', N'120', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000121', N'7121', N'组装一科C组', N'Assembly Section 1 C组', N'1950000000000000118', N'1949169142347206656', N'121', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000122', N'7122', N'组装二科', N'Assembly Section 2', N'1950000000000000117', N'1949168956883472384', N'122', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000123', N'7123', N'组装二科A组', N'Assembly Section 2 A组', N'1950000000000000122', N'1949169142347206656', N'123', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000124', N'7124', N'组装二科B组', N'Assembly Section 2 B组', N'1950000000000000122', N'1949169142347206656', N'124', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000125', N'7125', N'组装二科C组', N'Assembly Section 2 C组', N'1950000000000000122', N'1949169142347206656', N'125', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000126', N'7126', N'包装出货科', N'Packing & Shipping Section', N'1950000000000000117', N'1949168956883472384', N'126', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000127', N'7127', N'包装出货科A组', N'Packing & Shipping Section A组', N'1950000000000000126', N'1949169142347206656', N'127', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000128', N'7128', N'包装出货科B组', N'Packing & Shipping Section B组', N'1950000000000000126', N'1949169142347206656', N'128', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000129', N'7129', N'包装出货科C组', N'Packing & Shipping Section C组', N'1950000000000000126', N'1949169142347206656', N'129', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000130', N'7130', N'品保部', N'Quality Assurance Department', N'1950000000000000063', N'1949167957770899456', N'130', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000131', N'7131', N'进料检验科', N'Incoming Inspection Section', N'1950000000000000130', N'1949168956883472384', N'131', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000132', N'7132', N'进料检验科A组', N'Incoming Inspection Section A组', N'1950000000000000131', N'1949169142347206656', N'132', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000133', N'7133', N'进料检验科B组', N'Incoming Inspection Section B组', N'1950000000000000131', N'1949169142347206656', N'133', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000134', N'7134', N'进料检验科C组', N'Incoming Inspection Section C组', N'1950000000000000131', N'1949169142347206656', N'134', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000135', N'7135', N'制程品管科', N'Process Quality Control Section', N'1950000000000000130', N'1949168956883472384', N'135', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000136', N'7136', N'制程品管科A组', N'Process Quality Control Section A组', N'1950000000000000135', N'1949169142347206656', N'136', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000137', N'7137', N'制程品管科B组', N'Process Quality Control Section B组', N'1950000000000000135', N'1949169142347206656', N'137', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000138', N'7138', N'制程品管科C组', N'Process Quality Control Section C组', N'1950000000000000135', N'1949169142347206656', N'138', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000139', N'7139', N'出货检验科', N'Outgoing Inspection Section', N'1950000000000000130', N'1949168956883472384', N'139', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000140', N'7140', N'出货检验科A组', N'Outgoing Inspection Section A组', N'1950000000000000139', N'1949169142347206656', N'140', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000141', N'7141', N'出货检验科B组', N'Outgoing Inspection Section B组', N'1950000000000000139', N'1949169142347206656', N'141', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000142', N'7142', N'出货检验科C组', N'Outgoing Inspection Section C组', N'1950000000000000139', N'1949169142347206656', N'142', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000143', N'7143', N'工程部', N'Engineering Department', N'1950000000000000063', N'1949167957770899456', N'143', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000144', N'7144', N'生技工程科', N'Manufacturing Engineering Section', N'1950000000000000143', N'1949168956883472384', N'144', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000145', N'7145', N'生技工程科A组', N'Manufacturing Engineering Section A组', N'1950000000000000144', N'1949169142347206656', N'145', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000146', N'7146', N'生技工程科B组', N'Manufacturing Engineering Section B组', N'1950000000000000144', N'1949169142347206656', N'146', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000147', N'7147', N'生技工程科C组', N'Manufacturing Engineering Section C组', N'1950000000000000144', N'1949169142347206656', N'147', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000148', N'7148', N'设备工程科', N'Equipment Engineering Section', N'1950000000000000143', N'1949168956883472384', N'148', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000149', N'7149', N'设备工程科A组', N'Equipment Engineering Section A组', N'1950000000000000148', N'1949169142347206656', N'149', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000150', N'7150', N'设备工程科B组', N'Equipment Engineering Section B组', N'1950000000000000148', N'1949169142347206656', N'150', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000151', N'7151', N'设备工程科C组', N'Equipment Engineering Section C组', N'1950000000000000148', N'1949169142347206656', N'151', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000152', N'7152', N'自动化工程科', N'Automation Engineering Section', N'1950000000000000143', N'1949168956883472384', N'152', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000153', N'7153', N'自动化工程科A组', N'Automation Engineering Section A组', N'1950000000000000152', N'1949169142347206656', N'153', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000154', N'7154', N'自动化工程科B组', N'Automation Engineering Section B组', N'1950000000000000152', N'1949169142347206656', N'154', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000155', N'7155', N'自动化工程科C组', N'Automation Engineering Section C组', N'1950000000000000152', N'1949169142347206656', N'155', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000156', N'7156', N'人事部', N'Human Resources Department', N'1950000000000000063', N'1949167957770899456', N'156', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000157', N'7157', N'招募训练科', N'Recruitment & Training Section', N'1950000000000000156', N'1949168956883472384', N'157', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000158', N'7158', N'招募训练科A组', N'Recruitment & Training Section A组', N'1950000000000000157', N'1949169142347206656', N'158', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000159', N'7159', N'招募训练科B组', N'Recruitment & Training Section B组', N'1950000000000000157', N'1949169142347206656', N'159', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000160', N'7160', N'招募训练科C组', N'Recruitment & Training Section C组', N'1950000000000000157', N'1949169142347206656', N'160', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000161', N'7161', N'薪酬绩效科', N'Compensation & Performance Section', N'1950000000000000156', N'1949168956883472384', N'161', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000162', N'7162', N'薪酬绩效科A组', N'Compensation & Performance Section A组', N'1950000000000000161', N'1949169142347206656', N'162', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000163', N'7163', N'薪酬绩效科B组', N'Compensation & Performance Section B组', N'1950000000000000161', N'1949169142347206656', N'163', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000164', N'7164', N'薪酬绩效科C组', N'Compensation & Performance Section C组', N'1950000000000000161', N'1949169142347206656', N'164', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000165', N'7165', N'员工关系科', N'Employee Relations Section', N'1950000000000000156', N'1949168956883472384', N'165', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000166', N'7166', N'员工关系科A组', N'Employee Relations Section A组', N'1950000000000000165', N'1949169142347206656', N'166', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000167', N'7167', N'员工关系科B组', N'Employee Relations Section B组', N'1950000000000000165', N'1949169142347206656', N'167', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000168', N'7168', N'员工关系科C组', N'Employee Relations Section C组', N'1950000000000000165', N'1949169142347206656', N'168', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000169', N'7169', N'采购部', N'Procurement Department', N'1950000000000000063', N'1949167957770899456', N'169', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000170', N'7170', N'原物料采购科', N'Raw Material Purchasing Section', N'1950000000000000169', N'1949168956883472384', N'170', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000171', N'7171', N'原物料采购科A组', N'Raw Material Purchasing Section A组', N'1950000000000000170', N'1949169142347206656', N'171', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000172', N'7172', N'原物料采购科B组', N'Raw Material Purchasing Section B组', N'1950000000000000170', N'1949169142347206656', N'172', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000173', N'7173', N'原物料采购科C组', N'Raw Material Purchasing Section C组', N'1950000000000000170', N'1949169142347206656', N'173', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000174', N'7174', N'外包采购科', N'Outsourcing Purchasing Section', N'1950000000000000169', N'1949168956883472384', N'174', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000175', N'7175', N'外包采购科A组', N'Outsourcing Purchasing Section A组', N'1950000000000000174', N'1949169142347206656', N'175', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000176', N'7176', N'外包采购科B组', N'Outsourcing Purchasing Section B组', N'1950000000000000174', N'1949169142347206656', N'176', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000177', N'7177', N'外包采购科C组', N'Outsourcing Purchasing Section C组', N'1950000000000000174', N'1949169142347206656', N'177', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000178', N'7178', N'供应商管理科', N'Supplier Management Section', N'1950000000000000169', N'1949168956883472384', N'178', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000179', N'7179', N'供应商管理科A组', N'Supplier Management Section A组', N'1950000000000000178', N'1949169142347206656', N'179', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000180', N'7180', N'供应商管理科B组', N'Supplier Management Section B组', N'1950000000000000178', N'1949169142347206656', N'180', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000181', N'7181', N'供应商管理科C组', N'Supplier Management Section C组', N'1950000000000000178', N'1949169142347206656', N'181', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000182', N'7182', N'经管部', N'Management Control Department', N'1950000000000000063', N'1949167957770899456', N'182', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000183', N'7183', N'成本管理科', N'Cost Management Section', N'1950000000000000182', N'1949168956883472384', N'183', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000184', N'7184', N'成本管理科A组', N'Cost Management Section A组', N'1950000000000000183', N'1949169142347206656', N'184', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000185', N'7185', N'成本管理科B组', N'Cost Management Section B组', N'1950000000000000183', N'1949169142347206656', N'185', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000186', N'7186', N'成本管理科C组', N'Cost Management Section C组', N'1950000000000000183', N'1949169142347206656', N'186', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000187', N'7187', N'预算管理科', N'Budget Management Section', N'1950000000000000182', N'1949168956883472384', N'187', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000188', N'7188', N'预算管理科A组', N'Budget Management Section A组', N'1950000000000000187', N'1949169142347206656', N'188', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000189', N'7189', N'预算管理科B组', N'Budget Management Section B组', N'1950000000000000187', N'1949169142347206656', N'189', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000190', N'7190', N'预算管理科C组', N'Budget Management Section C组', N'1950000000000000187', N'1949169142347206656', N'190', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000191', N'7191', N'经营分析科', N'Business Analysis Section', N'1950000000000000182', N'1949168956883472384', N'191', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000192', N'7192', N'经营分析科A组', N'Business Analysis Section A组', N'1950000000000000191', N'1949169142347206656', N'192', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000193', N'7193', N'经营分析科B组', N'Business Analysis Section B组', N'1950000000000000191', N'1949169142347206656', N'193', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000194', N'7194', N'经营分析科C组', N'Business Analysis Section C组', N'1950000000000000191', N'1949169142347206656', N'194', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000195', N'7195', N'冲压部', N'Stamping Department', N'1950000000000000063', N'1949167957770899456', N'195', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000196', N'7196', N'冲压生产科', N'Stamping Production Section', N'1950000000000000195', N'1949168956883472384', N'196', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000197', N'7197', N'冲压生产科A组', N'Stamping Production Section A组', N'1950000000000000196', N'1949169142347206656', N'197', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000198', N'7198', N'冲压生产科B组', N'Stamping Production Section B组', N'1950000000000000196', N'1949169142347206656', N'198', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000199', N'7199', N'模具技术科', N'Die Technology Section', N'1950000000000000195', N'1949168956883472384', N'199', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000200', N'7200', N'模具技术科A组', N'Die Technology Section A组', N'1950000000000000199', N'1949169142347206656', N'200', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000201', N'7201', N'模具技术科B组', N'Die Technology Section B组', N'1950000000000000199', N'1949169142347206656', N'201', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000202', N'7202', N'注塑部', N'Injection Molding Department', N'1950000000000000063', N'1949167957770899456', N'202', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000203', N'7203', N'注塑生产科', N'Injection Production Section', N'1950000000000000202', N'1949168956883472384', N'203', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000204', N'7204', N'注塑生产科A组', N'Injection Production Section A组', N'1950000000000000203', N'1949169142347206656', N'204', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000205', N'7205', N'注塑生产科B组', N'Injection Production Section B组', N'1950000000000000203', N'1949169142347206656', N'205', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000206', N'7206', N'模具成型科', N'Mold Forming Section', N'1950000000000000202', N'1949168956883472384', N'206', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000207', N'7207', N'模具成型科A组', N'Mold Forming Section A组', N'1950000000000000206', N'1949169142347206656', N'207', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000208', N'7208', N'模具成型科B组', N'Mold Forming Section B组', N'1950000000000000206', N'1949169142347206656', N'208', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000209', N'7209', N'涂装部', N'Coating Department', N'1950000000000000063', N'1949167957770899456', N'209', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000210', N'7210', N'前处理科', N'Pretreatment Section', N'1950000000000000209', N'1949168956883472384', N'210', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000211', N'7211', N'前处理科A组', N'Pretreatment Section A组', N'1950000000000000210', N'1949169142347206656', N'211', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000212', N'7212', N'前处理科B组', N'Pretreatment Section B组', N'1950000000000000210', N'1949169142347206656', N'212', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000213', N'7213', N'喷涂作业科', N'Painting Operations Section', N'1950000000000000209', N'1949168956883472384', N'213', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000214', N'7214', N'喷涂作业科A组', N'Painting Operations Section A组', N'1950000000000000213', N'1949169142347206656', N'214', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000215', N'7215', N'喷涂作业科B组', N'Painting Operations Section B组', N'1950000000000000213', N'1949169142347206656', N'215', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000216', N'7216', N'业务部', N'Sales Department', N'1950000000000000064', N'1949167957770899456', N'216', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000217', N'7217', N'业务一科', N'Sales Section 1', N'1950000000000000216', N'1949168956883472384', N'217', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000218', N'7218', N'业务一科A组', N'Sales Section 1 A组', N'1950000000000000217', N'1949169142347206656', N'218', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000219', N'7219', N'业务一科B组', N'Sales Section 1 B组', N'1950000000000000217', N'1949169142347206656', N'219', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000220', N'7220', N'业务一科C组', N'Sales Section 1 C组', N'1950000000000000217', N'1949169142347206656', N'220', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000221', N'7221', N'业务二科', N'Sales Section 2', N'1950000000000000216', N'1949168956883472384', N'221', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000222', N'7222', N'业务二科A组', N'Sales Section 2 A组', N'1950000000000000221', N'1949169142347206656', N'222', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000223', N'7223', N'业务二科B组', N'Sales Section 2 B组', N'1950000000000000221', N'1949169142347206656', N'223', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000224', N'7224', N'业务二科C组', N'Sales Section 2 C组', N'1950000000000000221', N'1949169142347206656', N'224', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000225', N'7225', N'客户服务科', N'Customer Service Section', N'1950000000000000216', N'1949168956883472384', N'225', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000226', N'7226', N'客户服务科A组', N'Customer Service Section A组', N'1950000000000000225', N'1949169142347206656', N'226', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000227', N'7227', N'客户服务科B组', N'Customer Service Section B组', N'1950000000000000225', N'1949169142347206656', N'227', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000228', N'7228', N'客户服务科C组', N'Customer Service Section C组', N'1950000000000000225', N'1949169142347206656', N'228', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000229', N'7229', N'资材部', N'Materials Department', N'1950000000000000064', N'1949167957770899456', N'229', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000230', N'7230', N'物料控制科', N'Material Control Section', N'1950000000000000229', N'1949168956883472384', N'230', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000231', N'7231', N'物料控制科A组', N'Material Control Section A组', N'1950000000000000230', N'1949169142347206656', N'231', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000232', N'7232', N'物料控制科B组', N'Material Control Section B组', N'1950000000000000230', N'1949169142347206656', N'232', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000233', N'7233', N'物料控制科C组', N'Material Control Section C组', N'1950000000000000230', N'1949169142347206656', N'233', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000234', N'7234', N'仓储管理科', N'Warehouse Management Section', N'1950000000000000229', N'1949168956883472384', N'234', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000235', N'7235', N'仓储管理科A组', N'Warehouse Management Section A组', N'1950000000000000234', N'1949169142347206656', N'235', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000236', N'7236', N'仓储管理科B组', N'Warehouse Management Section B组', N'1950000000000000234', N'1949169142347206656', N'236', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000237', N'7237', N'仓储管理科C组', N'Warehouse Management Section C组', N'1950000000000000234', N'1949169142347206656', N'237', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000238', N'7238', N'生管协调科', N'Production Planning Coordination Section', N'1950000000000000229', N'1949168956883472384', N'238', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000239', N'7239', N'生管协调科A组', N'Production Planning Coordination Section A组', N'1950000000000000238', N'1949169142347206656', N'239', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000240', N'7240', N'生管协调科B组', N'Production Planning Coordination Section B组', N'1950000000000000238', N'1949169142347206656', N'240', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000241', N'7241', N'生管协调科C组', N'Production Planning Coordination Section C组', N'1950000000000000238', N'1949169142347206656', N'241', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000242', N'7242', N'模修部', N'Mold Repair Department', N'1950000000000000064', N'1949167957770899456', N'242', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000243', N'7243', N'模具保养科', N'Mold Maintenance Section', N'1950000000000000242', N'1949168956883472384', N'243', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000244', N'7244', N'模具保养科A组', N'Mold Maintenance Section A组', N'1950000000000000243', N'1949169142347206656', N'244', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000245', N'7245', N'模具保养科B组', N'Mold Maintenance Section B组', N'1950000000000000243', N'1949169142347206656', N'245', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000246', N'7246', N'模具保养科C组', N'Mold Maintenance Section C组', N'1950000000000000243', N'1949169142347206656', N'246', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000247', N'7247', N'模具维修科', N'Mold Repair Section', N'1950000000000000242', N'1949168956883472384', N'247', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000248', N'7248', N'模具维修科A组', N'Mold Repair Section A组', N'1950000000000000247', N'1949169142347206656', N'248', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000249', N'7249', N'模具维修科B组', N'Mold Repair Section B组', N'1950000000000000247', N'1949169142347206656', N'249', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1950000000000000250', N'7250', N'模具维修科C组', N'Mold Repair Section C组', N'1950000000000000247', N'1949169142347206656', N'250', N'', N'', N'', NULL, N'1903486709602062336', N'2026-03-31 09:00:00.000', N'1903486709602062336', N'2026-03-31 09:00:00.000')
GO


-- ----------------------------
-- Table structure for DepartmentLevel
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[DepartmentLevel]') AND type IN ('U'))
	DROP TABLE [Basic].[DepartmentLevel]
GO

CREATE TABLE [Basic].[DepartmentLevel] (
  [DepartmentLevelId] bigint  NOT NULL,
  [DepartmentLevelCode] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [DepartmentLevelNameCn] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [DepartmentLevelNameEn] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [SortOrder] int  NULL,
  [Description] nvarchar(500) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[DepartmentLevel] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键，部门级别Id',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentLevel',
'COLUMN', N'DepartmentLevelId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门级别编号',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentLevel',
'COLUMN', N'DepartmentLevelCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门级别名称（中文）',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentLevel',
'COLUMN', N'DepartmentLevelNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门级别名称（中文）',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentLevel',
'COLUMN', N'DepartmentLevelNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'说明',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentLevel',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentLevel',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentLevel',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentLevel',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentLevel',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门等级表',
'SCHEMA', N'Basic',
'TABLE', N'DepartmentLevel'
GO


-- ----------------------------
-- Records of DepartmentLevel
-- ----------------------------
INSERT INTO [Basic].[DepartmentLevel] ([DepartmentLevelId], [DepartmentLevelCode], [DepartmentLevelNameCn], [DepartmentLevelNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1350917348311171072', N'Board', N'董事会', N'Board of Directors', N'1', N'', N'1903486709602062336', N'2025-01-22 16:28:54.000', N'1903486709602062336', N'2025-08-09 17:53:03.000')
GO

INSERT INTO [Basic].[DepartmentLevel] ([DepartmentLevelId], [DepartmentLevelCode], [DepartmentLevelNameCn], [DepartmentLevelNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351403528752463872', N'Center', N'中心级', N'Corporate Center', N'2', N'', N'1903486709602062336', N'2025-01-23 08:34:52.000', N'1903486709602062336', N'2025-08-09 17:53:07.000')
GO

INSERT INTO [Basic].[DepartmentLevel] ([DepartmentLevelId], [DepartmentLevelCode], [DepartmentLevelNameCn], [DepartmentLevelNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351405026328707072', N'Plant', N'厂长室', N'Plant Director Office', N'3', N'', N'1903486709602062336', N'2025-01-23 08:37:50.000', N'1903486709602062336', N'2025-08-09 17:53:11.000')
GO

INSERT INTO [Basic].[DepartmentLevel] ([DepartmentLevelId], [DepartmentLevelCode], [DepartmentLevelNameCn], [DepartmentLevelNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1949167957770899456', N'Dept', N'部门级', N'Department', N'4', N'', N'1903486709602062336', N'2025-07-27 02:00:29.000', N'1903486709602062336', N'2025-09-13 15:17:28.000')
GO

INSERT INTO [Basic].[DepartmentLevel] ([DepartmentLevelId], [DepartmentLevelCode], [DepartmentLevelNameCn], [DepartmentLevelNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1949168956883472384', N'Sec', N'科室级', N'Section', N'5', N'', N'1903486709602062336', N'2025-07-27 02:04:27.000', N'1903486709602062336', N'2025-09-13 15:17:31.000')
GO

INSERT INTO [Basic].[DepartmentLevel] ([DepartmentLevelId], [DepartmentLevelCode], [DepartmentLevelNameCn], [DepartmentLevelNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1949169142347206656', N'Ops', N'执行级', N'Operational Level', N'6', N'', N'1903486709602062336', N'2025-07-27 02:05:12.000', N'1903486709602062336', N'2025-09-13 15:17:34.000')
GO


-- ----------------------------
-- Table structure for DictionaryInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[DictionaryInfo]') AND type IN ('U'))
	DROP TABLE [Basic].[DictionaryInfo]
GO

CREATE TABLE [Basic].[DictionaryInfo] (
  [DicId] bigint  NOT NULL,
  [ModuleId] bigint  NULL,
  [DicType] nvarchar(25) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [DicCode] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [DicNameCn] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [DicNameEn] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [SortOrder] int  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3) DEFAULT NULL NULL
)
GO

ALTER TABLE [Basic].[DictionaryInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'字典主键Id',
'SCHEMA', N'Basic',
'TABLE', N'DictionaryInfo',
'COLUMN', N'DicId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'字典类型',
'SCHEMA', N'Basic',
'TABLE', N'DictionaryInfo',
'COLUMN', N'DicType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'字典编码',
'SCHEMA', N'Basic',
'TABLE', N'DictionaryInfo',
'COLUMN', N'DicCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'字典名称（中文）',
'SCHEMA', N'Basic',
'TABLE', N'DictionaryInfo',
'COLUMN', N'DicNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'字典名称（英文）',
'SCHEMA', N'Basic',
'TABLE', N'DictionaryInfo',
'COLUMN', N'DicNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'字典排序',
'SCHEMA', N'Basic',
'TABLE', N'DictionaryInfo',
'COLUMN', N'SortOrder'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'DictionaryInfo',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'DictionaryInfo',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'DictionaryInfo',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'DictionaryInfo',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'字典信息表',
'SCHEMA', N'Basic',
'TABLE', N'DictionaryInfo'
GO


-- ----------------------------
-- Records of DictionaryInfo
-- ----------------------------
INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1942923565422743552', N'1350161679034934501', N'MenuType', N'PrimaryMenu', N'一级菜单', N'Primary Menu', N'1', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1942923650634223616', N'1350161679034934501', N'MenuType', N'SecondaryMenu', N'二级菜单', N'Secondary menu', N'2', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1951693847235006464', N'1350161679034934501', N'LoginBehavior', N'LoginSuccessful', N'登录成功', N'Login successful', N'1', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1951694944875974656', N'1350161679034934501', N'LoginBehavior', N'IncorrectPassword', N'密码错误', N'Incorrect password', N'2', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1951695070155640832', N'1350161679034934501', N'LoginBehavior', N'AccountNotExist', N'账号不存在', N'Account does not exist', N'3', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1954124186578456576', N'1350161679034934501', N'LoginBehavior', N'LoggedOut', N'登出', N'Logged out', N'4', N'1903486709602062336', N'2026-03-05 09:12:20.000', N'1903486709602062336', NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990313419198304256', N'1968271760889614336', N'FormStatus', N'PendingSubmit', N'待送审', N'Pending Submit', N'1', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1987844250263490560', N'1968271760889614336', N'LeaveType', N'Annual', N'年休假', N'Annual Leave', N'1', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1987844559857651712', N'1968271760889614336', N'LeaveType', N'Sick', N'病假', N'Sick Leave', N'2', N'1903486709602062336', N'2026-03-05 09:12:20.000', N'1903486709602062336', NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1987844688471789568', N'1968271760889614336', N'LeaveType', N'Personal', N'事假', N'Personal Leave', N'3', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1989622758610440192', N'1968271760889614336', N'LeaveType', N'Marriage', N'婚假', N'Marriage Leave', N'4', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1989622835332648960', N'1968271760889614336', N'LeaveType', N'Maternity', N'产假', N'Maternity Leave', N'5', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1989622953901428736', N'1968271760889614336', N'LeaveType', N'Paternity', N'陪产假 / 护理假', N'Paternity Leave', N'6', N'1903486709602062336', N'2026-03-05 09:12:20.000', N'1903486709602062336', NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1989623247368491008', N'1968271760889614336', N'LeaveType', N'Nursing', N'哺乳假', N'Nursing Leave', N'7', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990313509094821888', N'1968271760889614336', N'FormStatus', N'UnderReview', N'审批中', N'Under Review', N'2', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990313656482664448', N'1968271760889614336', N'FormStatus', N'Rejected', N'已驳回', N'Rejected', N'4', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990313846581104640', N'1968271760889614336', N'FormStatus', N'Approved', N'核准完', N'Approved', N'3', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990314002714071040', N'1968271760889614336', N'FormStatus', N'Voided', N'作废', N'Voided', N'5', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009863132733902848', N'1968271760889614336', N'Assignment', N'Org', N'组织架构', N'Org Structure', N'1', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009863891806457856', N'1968271760889614336', N'Assignment', N'DeptUser', N'指定部门 & 职级', N'Dept & Position', N'2', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009864209579511808', N'1968271760889614336', N'Assignment', N'User', N'指定员工', N'Assigned Employee', N'3', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009864390316265472', N'1968271760889614336', N'Assignment', N'Custom', N'自定义', N'Custom', N'4', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009875445490782208', N'1968271760889614336', N'ReviewMode', N'Review', N'单审', N'Review', N'1', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2009875596401840128', N'1968271760889614336', N'ReviewMode', N'AndReview', N'会审', N'And Review', N'3', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2022522679818588160', N'1968271760889614336', N'AppointmentType', N'Actual', N'实', N'Actual', N'1', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2022523046727913472', N'1968271760889614336', N'AppointmentType', N'Concurrent', N'兼', N'Concurrent', N'3', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2022523134879600640', N'1968271760889614336', N'AppointmentType', N'Agent', N'代', N'Agent', N'2', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2022523379030036480', N'1968271760889614336', N'AppointmentType', N'AutoActual', N'自动指派 - 实', N'Auto - Actual', N'5', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2027616422913511424', N'1968271760889614336', N'AppointmentType', N'AutoConcurrent', N'自动指派 - 兼', N'Auto - Concurrent', N'7', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2029030380463591424', N'1968271760889614336', N'AppointmentType', N'AutoAgent', N'自动指派 - 代', N'Auto - Agent', N'6', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2022523134879600641', N'1968271760889614336', N'AppointmentType', N'ConcurrentAgent', N'兼 代', N'Concurren Agent', N'4', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2029030380463591425', N'1968271760889614336', N'AppointmentType', N'AutoConcurrentAgent', N'自动指派 - 兼 代', N'Auto - Concurrent Agent', N'8', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2030349771297918976', N'1968271760889614336', N'ReviewMode', N'OrReview', N'或审', N'Or Review', N'2', N'1903486709602062336', N'2026-03-08 02:28:02.210', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2035212012572774400', N'1968271760889614336', N'LeaveType', N'Bereavement', N'丧假', N'Bereavement Leave', N'8', N'1903486709602062336', N'2026-03-21 12:28:50.867', N'1903486709602062336', N'2026-03-21 12:29:06.873')
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990313509094821889', N'1968271760889614336', N'FormReviewResult', N'Unsigned', N'未审批', N'Unsigned', N'1', N'1903486709602062336', N'2026-04-27 00:00:00.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990313509094821890', N'1968271760889614336', N'FormReviewResult', N'UnderReview', N'审批中', N'Under Review', N'2', N'1903486709602062336', N'2026-04-27 00:00:00.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990313509094821891', N'1968271760889614336', N'FormReviewResult', N'Approve', N'已核准', N'Approve', N'3', N'1903486709602062336', N'2026-04-27 00:00:00.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990313509094821892', N'1968271760889614336', N'ReviewResult', N'Reject', N'驳回', N'Reject', N'2', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990313509094821893', N'1968271760889614336', N'ReviewResult', N'Approve', N'核准', N'Approve', N'1', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990313509094821894', N'1968271760889614336', N'ReviewType', N'Manual', N'手动', N'Manual', N'1', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990313509094821895', N'1968271760889614336', N'ReviewType', N'Automatic', N'自动', N'Automatic', N'2', N'1903486709602062336', N'2026-03-05 09:12:20.000', NULL, NULL)
GO


-- ----------------------------
-- Table structure for ExchangeRate
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[ExchangeRate]') AND type IN ('U'))
	DROP TABLE [Basic].[ExchangeRate]
GO

CREATE TABLE [Basic].[ExchangeRate] (
  [CurrencyCode] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ExchangeCurrencyCode] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [YearMonth] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ExchangeRate] decimal(18,4)  NOT NULL,
  [Remark] nvarchar(500) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[ExchangeRate] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'本币别',
'SCHEMA', N'Basic',
'TABLE', N'ExchangeRate',
'COLUMN', N'CurrencyCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'兑换币别',
'SCHEMA', N'Basic',
'TABLE', N'ExchangeRate',
'COLUMN', N'ExchangeCurrencyCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'年月',
'SCHEMA', N'Basic',
'TABLE', N'ExchangeRate',
'COLUMN', N'YearMonth'
GO

EXEC sp_addextendedproperty
'MS_Description', N'汇率',
'SCHEMA', N'Basic',
'TABLE', N'ExchangeRate',
'COLUMN', N'ExchangeRate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注',
'SCHEMA', N'Basic',
'TABLE', N'ExchangeRate',
'COLUMN', N'Remark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'ExchangeRate',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'ExchangeRate',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'ExchangeRate',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'ExchangeRate',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'汇率信息表',
'SCHEMA', N'Basic',
'TABLE', N'ExchangeRate'
GO


-- ----------------------------
-- Records of ExchangeRate
-- ----------------------------

-- ----------------------------
-- Table structure for MenuInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[MenuInfo]') AND type IN ('U'))
	DROP TABLE [Basic].[MenuInfo]
GO

CREATE TABLE [Basic].[MenuInfo] (
  [MenuId] bigint  NOT NULL,
  [ModuleId] bigint  NULL,
  [ParentMenuId] bigint  NULL,
  [MenuCode] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [MenuNameCn] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [MenuNameEn] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [MenuType] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [Path] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [MenuIcon] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [SortOrder] int  NULL,
  [IsVisible] int  NULL,
  [RoutePath] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [Redirect] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [Remark] nvarchar(500) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NULL,
  [CreatedDate] datetime2(3)  NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[MenuInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'MenuId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'父菜单 ID',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'ParentMenuId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单唯一编码',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'MenuCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单名称（中文）',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'MenuNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单名称（英文）',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'MenuNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单类型：0=目录，1=菜单，2=按钮',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'MenuType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单路径或外部链接',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'Path'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图标类名',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'MenuIcon'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'SortOrder'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否可见：1=可见，0=不可见',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'IsVisible'
GO

EXEC sp_addextendedproperty
'MS_Description', N'对应API路由',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'RoutePath'
GO

EXEC sp_addextendedproperty
'MS_Description', N'前端重定向',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'Redirect'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'Remark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单信息表',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo'
GO


-- ----------------------------
-- Records of MenuInfo
-- ----------------------------
INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1350161962451534507', N'1350161679034934501', N'0', N'SystemMgmt', N'系统管理模块', N'System Mgmt', N'PrimaryMenu', N'systembasicmgmt/system-mgmt', N'HomeFilled', N'2', N'1', N'', N'', N'', N'1903486709602062336', N'2025-01-20 16:57:54.000', N'1903486709602062336', N'2026-05-01 21:30:32.547')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1350169511264780288', N'1350161679034934501', N'1350161962451534507', N'ModuleData', N'模块信息维护', N'Module Info', N'SecondaryMenu', N'systembasicmgmt/system-mgmt/module', N'Menu', N'1', N'1', N'/api/SystemBasicMgmt/SystemMgmt/ModuleInfo', N'', NULL, N'1903486709602062336', N'2025-01-21 00:00:00.000', N'1903486709602062336', N'2025-09-12 22:09:40.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903507885518884864', N'1350161679034934501', N'1350161962451534507', N'PMenuData', N'一级菜单维护', N'PMenu Info', N'SecondaryMenu', N'systembasicmgmt/system-mgmt/pmenu', N'Operation', N'2', N'1', N'/api/SystemBasicMgmt/SystemMgmt/PMenuInfo', N'', NULL, N'1903486709602062336', N'2025-03-23 02:10:43.000', N'1903486709602062336', N'2025-09-12 22:09:46.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903507885518884865', N'1350161679034934501', N'1917998505360756736', N'PersonalInfo', N'个人信息维护', N'Personal Profile', N'SecondaryMenu', N'systembasicmgmt/system-basicdata/personal', N'Postcard', N'8', N'1', N'/api/SystemBasicMgmt/SystemBasicData/PersonalInfo', N'', NULL, N'1903486709602062336', N'2025-03-23 02:10:43.000', N'1903486709602062336', N'2025-11-09 01:30:35.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1910300381175484416', N'1350161679034934501', N'1350161962451534507', N'SMenuData', N'二级菜单维护', N'SMenu Info', N'SecondaryMenu', N'systembasicmgmt/system-mgmt/smenu', N'Share', N'3', N'1', N'/api/SystemBasicMgmt/SystemMgmt/SMenuInfo', N'', NULL, N'1903486709602062336', N'2025-04-10 19:54:37.000', N'1903486709602062336', N'2025-09-12 22:10:00.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1911747107358904320', N'1350161679034934501', N'1350161962451534507', N'RoleData', N'角色信息维护', N'Role Info', N'SecondaryMenu', N'systembasicmgmt/system-mgmt/role', N'Avatar', N'4', N'1', N'/api/SystemBasicMgmt/SystemMgmt/RoleInfo', N'', NULL, N'1903486709602062336', N'2025-04-14 19:43:23.000', N'1903486709602062336', N'2025-09-18 23:30:03.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1917998505360756736', N'1350161679034934501', N'0', N'BasicData', N'基本信息模块', N'Basic Data', N'PrimaryMenu', N'systembasicmgmt/system-basicdata', N'List', N'1', N'1', N'', N'', N'', N'1903486709602062336', N'2025-05-02 01:44:13.000', N'1903486709602062336', N'2026-05-01 21:30:22.983')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1922597528205922304', N'1350161679034934501', N'1917998505360756736', N'EmployeeInfo', N'员工信息维护', N'Employee Info', N'SecondaryMenu', N'systembasicmgmt/system-basicdata/userinfo', N'UserFilled', N'7', N'1', N'/api/SystemBasicMgmt/SystemBasicData/UserInfo', N'', NULL, N'1903486709602062336', N'2025-05-14 18:19:05.000', N'1903486709602062336', N'2025-10-03 16:16:58.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929532135392284672', N'1350161679034934501', N'1917998505360756736', N'DepartmentInfo', N'部门信息维护', N'Department Info', N'SecondaryMenu', N'systembasicmgmt/system-basicdata/departmentinfo', N'School', N'2', N'1', N'/api/SystemBasicMgmt/SystemBasicData/DepartmentInfo', N'', N'', N'1903486709602062336', N'2025-06-02 21:34:44.000', N'1903486709602062336', N'2026-05-01 21:30:37.140')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929897094655643648', N'1350161679034934501', N'1917998505360756736', N'DepartmentLevel', N'部门级别维护', N'Dept Level', N'SecondaryMenu', N'systembasicmgmt/system-basicdata/departmentlevel', N'CollectionTag', N'1', N'1', N'/api/SystemBasicMgmt/SystemBasicData/DepartmentLevel', N'', N'', N'1903486709602062336', N'2025-06-03 21:44:57.000', N'1903486709602062336', N'2026-05-01 21:30:39.620')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1930269640165036032', N'1350161679034934501', N'1917998505360756736', N'PositionInfo', N'职级信息维护', N'Position Info', N'SecondaryMenu', N'systembasicmgmt/system-basicdata/positioninfo', N'GoldMedal', N'5', N'1', N'/api/SystemBasicMgmt/SystemBasicData/PositionInfo', N'', NULL, N'1903486709602062336', N'2025-06-04 22:25:19.000', N'1903486709602062336', N'2025-08-09 10:52:23.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1932766707219304448', N'1350161679034934501', N'0', N'SystemConfig', N'系统设定模块', N'System Settings', N'PrimaryMenu', N'systembasicmgmt/system-config', N'Setting', N'4', N'1', N'', N'/systembasicmgmt/system-config', NULL, N'1903486709602062336', N'2025-06-11 19:47:46.000', N'1903486709602062336', N'2025-10-03 09:13:21.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1933581101280923648', N'1350161679034934501', N'1932766707219304448', N'DictionaryData', N'字典信息维护', N'Dictionary Info', N'SecondaryMenu', N'systembasicmgmt/system-config/dictionaryinfo', N'Reading', N'1', N'1', N'/api/SystemBasicMgmt/SystemConfig/DictionaryInfo', N'', NULL, N'1903486709602062336', N'2025-06-14 01:43:53.000', N'1903486709602062336', N'2025-09-12 22:11:52.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1933581101280923650', N'1350161679034934501', N'1917998505360756736', N'UserLabor', N'员工职业维护', N'User Labor', N'SecondaryMenu', N'systembasicmgmt/system-basicdata/userlabor', N'Postcard', N'5', N'1', N'/api/SystemBasicMgmt/SystemBasicData/UserLabor', N'', NULL, N'1903486709602062336', N'2025-06-14 01:43:53.000', N'1903486709602062336', N'2025-09-11 18:21:58.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1938565603321319424', N'1350161679034934501', N'1967176341195460608', N'UserAgent', N'员工代理维护', N'User Agent', N'SecondaryMenu', N'systembasicmgmt/user-settings/useragent', N'Handbag', N'1', N'1', N'/api/SystemBasicMgmt/UserSettings/UserAgent', N'', NULL, N'1903486709602062336', N'2025-06-27 19:50:31.000', N'1903486709602062336', N'2025-09-14 18:44:54.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1938565603321319425', N'1350161679034934501', N'1967176341195460608', N'UserPartTime', N'员工兼任维护', N'User PartTime', N'SecondaryMenu', N'systembasicmgmt/user-settings/userparttime', N'ShoppingBag', N'2', N'1', N'/api/SystemBasicMgmt/UserSettings/UserPartTime', N'', NULL, N'1903486709602062336', N'2025-06-27 19:50:31.000', N'1903486709602062336', N'2025-09-14 18:45:04.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1938565603321319426', N'1350161679034934501', N'1967176341195460608', N'UserForm', N'员工表单绑定', N'User Form', N'SecondaryMenu', N'systembasicmgmt/user-settings/userform', N'Management', N'3', N'1', N'/api/SystemBasicMgmt/UserSettings/UserForm', N'', N'', N'1903486709602062336', N'2025-06-27 19:50:31.000', N'1903486709602062336', N'2026-03-13 22:10:12.017')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1942199098723667968', N'1350161679034934501', N'1932766707219304448', N'CurrencyInfo', N'币别信息维护', N'Currency Info', N'SecondaryMenu', N'systembasicmgmt/system-config/currencyinfo', N'Money', N'2', N'1', N'/api/systemBasicMgmt/SystemConfig/CurrencyInfo', N'', NULL, N'1903486709602062336', N'2025-07-07 20:28:44.000', N'1903486709602062336', N'2025-08-09 22:39:26.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1944738755751579648', N'1350161679034934501', N'1932766707219304448', N'ExchangeRate', N'汇率信息维护', N'Exchange Rate', N'SecondaryMenu', N'systembasicmgmt/system-config/exchangerate', N'Switch', N'3', N'1', N'/api/SystemBasicMgmt/SystemConfig/ExchangeRate', N'', NULL, N'1903486709602062336', N'2025-07-14 20:40:25.000', N'1903486709602062336', N'2025-10-01 21:44:42.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1951689330179313664', N'1350161679034934501', N'1932766707219304448', N'LogOutInfo', N'员工操作日志', N'LogOut Info', N'SecondaryMenu', N'systembasicmgmt/system-config/userLoginLog', N'Tickets', N'4', N'1', N'/api/SystemBasicMgmt/SystemConfig/UserLoginLog', N'', NULL, N'1903486709602062336', N'2025-08-03 00:59:31.000', N'1903486709602062336', N'2025-08-09 22:39:50.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1967176341195460608', N'1350161679034934501', N'0', N'Userconfig', N'员工相关配置', N'System UserFig', N'PrimaryMenu', N'systembasicmgmt/user-settings', N'SetUp', N'3', N'1', N'', N'/systembasicmgmt/user-settings', NULL, N'1903486709602062336', N'2025-09-14 18:39:22.000', N'1903486709602062336', N'2025-10-03 09:13:24.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1968272763634454528', N'1968271760889614336', N'0', N'FormBasicInfo', N'表单基础信息', N'Form BasicInfo', N'PrimaryMenu', N'formbusiness/form-basicInfo', N'Tickets', N'1', N'1', N'', N'', NULL, N'1903486709602062336', N'2025-09-17 19:16:10.000', N'1903486709602062336', N'2025-09-17 19:22:18.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1968275489407766528', N'1968271760889614336', N'1968272763634454528', N'FormGroup', N'表单组别信息', N'Form Group', N'SecondaryMenu', N'formbusiness/form-basicInfo/formgroup', N'Collection', N'2', N'1', N'/api/FormBusiness/FormBasicInfo/FormGroup', N'', NULL, N'1903486709602062336', N'2025-09-17 19:27:00.000', N'1903486709602062336', N'2025-09-17 23:33:27.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1968275489407766529', N'1968271760889614336', N'1968272763634454528', N'FormType', N'表单类别信息', N'Form Type', N'SecondaryMenu', N'formbusiness/form-basicInfo/formtype', N'Postcard', N'3', N'1', N'/api/FormBusiness/FormBasicInfo/FormType', N'', NULL, N'1903486709602062336', N'2025-09-17 19:27:00.000', N'1903486709602062336', N'2025-10-19 03:00:49.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1968275489407766530', N'1968271760889614336', N'1968272763634454528', N'ControlInfo', N'控件信息维护', N'Control Info', N'SecondaryMenu', N'formbusiness/form-basicInfo/controlinfo', N'Link', N'1', N'1', N'/api/FormBusiness/FormBasicInfo/ControlInfo', N'', NULL, N'1903486709602062336', N'2025-09-17 19:27:00.000', N'1903486709602062336', N'2025-10-19 03:00:49.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1973378015064887296', N'1350161679034934501', N'1917998505360756736', N'NationalityInfo', N'国籍信息维护', N'Nationality Info', N'SecondaryMenu', N'systembasicmgmt/system-basicdata/nationalityinfo', N'DeleteLocation', N'3', N'1', N'/api/SystemBasicMgmt/SystemBasicData/NationalityInfo', N'', N'', N'1903486709602062336', N'2025-10-01 21:22:37.000', N'1903486709602062336', N'2026-01-03 21:07:57.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1982707658716745729', N'1982707658716745728', N'0', N'CustMatBasicInfo', N'相关基础信息', N'CustMat Basic', N'PrimaryMenu', N'custmat/custmat-basicinfo', N'List', N'1', N'1', N'', N'/custmat/custmat-basicinfo', NULL, N'1903486709602062336', N'2025-10-27 15:23:10.000', N'1903486709602062336', N'2025-11-03 20:44:29.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1982707658716745730', N'1982707658716745728', N'1982707658716745729', N'CustomerInfo', N'客户信息维护', N'Customer Info', N'SecondaryMenu', N'custmat/custmat-basicinfo/customer', N'DeleteLocation', N'3', N'1', N'/api/CustMat/CustMatBasicInfo/CustomerInfo', N'', NULL, N'1903486709602062336', N'2025-10-27 15:26:00.000', N'1903486709602062336', N'2025-11-02 13:02:25.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1982707658716745731', N'1982707658716745728', N'1982707658716745729', N'PartNumberInfo', N'料号信息维护', N'PartNumber Info', N'SecondaryMenu', N'custmat/custmat-basicinfo/partnumber', N'DeleteLocation', N'2', N'1', N'/api/CustMat/CustMatBasicInfo/PartNumberInfo', N'', NULL, N'1903486709602062336', N'2025-10-27 15:26:00.000', N'1903486709602062336', N'2025-10-27 15:26:00.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1984848313438048256', N'1982707658716745728', N'1982707658716745729', N'Manufacturer', N'厂商信息维护', N'Manufacturer Info', N'SecondaryMenu', N'custmat/custmat-basicinfo/manufacturerinfo', N'Van', N'1', N'1', N'/api/CustMat/CustMatBasicInfo/ManufacturerInfo', N'', NULL, N'1903486709602062336', N'2025-11-02 13:01:29.000', NULL, NULL)
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1988927293837414400', N'1968271760889614336', N'0', N'FormOperate', N'表单作业模块', N'Form Operate', N'PrimaryMenu', N'formbusiness/form-operate', N'Notebook', N'3', N'1', N'', N'', NULL, N'1903486709602062336', N'2025-11-13 19:09:53.000', N'1903486709602062336', N'2025-11-13 19:10:07.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1988927293837414401', N'1968271760889614336', N'0', N'FormWorkflow', N'表单流程配置', N'Form Workflow', N'PrimaryMenu', N'formbusiness/form-workflow', N'Expand', N'2', N'1', N'', N'', N'', N'1903486709602062336', N'2025-11-13 19:09:53.000', N'1903486709602062336', N'2026-03-17 12:35:57.703')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1988928242475732992', N'1968271760889614336', N'1988927293837414400', N'ApplicationForm', N'申请表单作业', N'Application Form', N'SecondaryMenu', N'formbusiness/form-operate/applyform', N'Document', N'1', N'1', N'/api/FormBusiness/FormOperate/ApplyForm', N'', NULL, N'1903486709602062336', N'2025-11-13 19:13:40.000', N'1903486709602062336', N'2025-11-15 09:34:45.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1988928242475732993', N'1968271760889614336', N'1988927293837414401', N'WorkflowStep', N'流程步骤详情', N'Workflow Step', N'SecondaryMenu', N'formbusiness/form-workflow/workflowstep', N'
HelpFilled', N'1', N'1', N'/api/FormBusiness/FormWorkFlow/WorkflowStep', N'', N'', N'1903486709602062336', N'2025-11-13 19:13:40.000', N'1903486709602062336', N'2026-03-17 12:36:34.490')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1988928242475732994', N'1968271760889614336', N'1988927293837414400', N'PendingReview', N'待审表单列表', N'Pending Review', N'SecondaryMenu', N'formbusiness/form-operate/pendingreview', N'Check', N'2', N'1', N'/api/FormBusiness/FormOperate/PendingReview', N'', N'', N'1903486709602062336', N'2025-11-13 19:13:40.000', N'1903486709602062336', N'2026-03-21 14:12:27.190')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1988928242475732996', N'1968271760889614336', N'1988927293837414401', N'WorkflowRule', N'流程规则维护', N'Workflow Rule', N'SecondaryMenu', N'formbusiness/form-workflow/workflowrule', N'Notification', N'3', N'1', N'/api/FormBusiness/FormWorkFlow/WorkflowRule', N'', N'', N'1903486709602062336', N'2025-11-13 19:13:40.000', N'1903486709602062336', N'2026-03-26 18:29:25.390')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ModuleId], [ParentMenuId], [MenuCode], [MenuNameCn], [MenuNameEn], [MenuType], [Path], [MenuIcon], [SortOrder], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1988928242475732997', N'1968271760889614336', N'1988927293837414401', N'WorkflowRuleStep', N'流程规则步骤', N'Workflow RuleStep', N'SecondaryMenu', N'formbusiness/form-workflow/workflowrulestep', N'Promotion', N'4', N'1', N'/api/FormBusiness/FormWorkFlow/WorkflowRuleStep', N'', N'', N'1903486709602062336', N'2025-11-13 19:13:40.000', N'1903486709602062336', N'2026-03-26 18:29:25.390')
GO


-- ----------------------------
-- Table structure for ModuleInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[ModuleInfo]') AND type IN ('U'))
	DROP TABLE [Basic].[ModuleInfo]
GO

CREATE TABLE [Basic].[ModuleInfo] (
  [ModuleId] bigint  NOT NULL,
  [ModuleCode] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ModuleNameCn] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ModuleNameEn] nvarchar(150) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Path] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [ModuleIcon] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [SortOrder] int  NOT NULL,
  [IsVisible] int  NOT NULL,
  [RemarkCh] nvarchar(1000) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [RemarkEn] nvarchar(1000) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[ModuleInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单主键Id',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo',
'COLUMN', N'ModuleId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单编码',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo',
'COLUMN', N'ModuleCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单名称（中文）',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo',
'COLUMN', N'ModuleNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单名称（英文）',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo',
'COLUMN', N'ModuleNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单路径或外部链接',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo',
'COLUMN', N'Path'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图标类名',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo',
'COLUMN', N'ModuleIcon'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序字段',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo',
'COLUMN', N'SortOrder'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否可见：1=可见，0=不可见',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo',
'COLUMN', N'IsVisible'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注（中文）',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo',
'COLUMN', N'RemarkCh'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注（英文）',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo',
'COLUMN', N'RemarkEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'模块信息表',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo'
GO


-- ----------------------------
-- Records of ModuleInfo
-- ----------------------------
INSERT INTO [Basic].[ModuleInfo] ([ModuleId], [ModuleCode], [ModuleNameCn], [ModuleNameEn], [Path], [ModuleIcon], [SortOrder], [IsVisible], [RemarkCh], [RemarkEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1350161679034934501', N'SystemBasicMgmt', N'系统基本管理', N'SystemBasic Mgmt', N'systembasicmgmt/index', N'Setting', N'1', N'1', N'系统管理模组用于统一管理系统的基本资讯、权限配置和资料字典。它支援系统参数维护、使用者和角色权限分配，以及业务字典的集中管理，提升系统的安全性与可维护性。', N'The System Management module is used to centrally manage basic system information, permission configurations, and data dictionaries. It supports system parameter maintenance, user and role permission assignment, and centralized management of business dictionaries, improving system security and maintainability.', N'1903486709602062336', N'2025-01-20 16:31:57.000', N'1903486709602062336', N'2026-02-14 13:52:23.000')
GO

INSERT INTO [Basic].[ModuleInfo] ([ModuleId], [ModuleCode], [ModuleNameCn], [ModuleNameEn], [Path], [ModuleIcon], [SortOrder], [IsVisible], [RemarkCh], [RemarkEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1968271760889614336', N'FormBusiness', N'表单签核业务', N'Form Business', N'formbusiness/index', N'Notebook', N'2', N'1', N'表单签核模块用于处理企业内部各类业务表单的审批流程。', N'The Form Approval Module is designed to manage the approval workflow of various business forms (such as leave requests, stamping applications, purchase requests, etc.) within the organization.', N'1903486709602062336', N'2025-09-17 19:12:11.000', N'1903486709602062336', N'2025-10-19 01:47:57.000')
GO

INSERT INTO [Basic].[ModuleInfo] ([ModuleId], [ModuleCode], [ModuleNameCn], [ModuleNameEn], [Path], [ModuleIcon], [SortOrder], [IsVisible], [RemarkCh], [RemarkEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1982707658716745728', N'CustMat', N'客户生产计划', N'Customer Production Plan', N'custmat/index', N'Promotion', N'3', N'1', N'生产计划模块用于根据销售订单、库存状态、物料供应及产能情况，自动生成和调整生产计划，确保生产过程高效、有序、可控。', N'The Production Planning Module is designed to automatically generate and adjust production schedules based on sales orders, inventory status, material availability, and production capacity, ensuring an efficient, organized, and controllable manufacturing process.', N'1903486709602062336', N'2025-10-27 15:15:17.000', N'1903486709602062336', N'2025-11-03 20:48:30.000')
GO


-- ----------------------------
-- Table structure for NationalityInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[NationalityInfo]') AND type IN ('U'))
	DROP TABLE [Basic].[NationalityInfo]
GO

CREATE TABLE [Basic].[NationalityInfo] (
  [NationId] bigint  NOT NULL,
  [NationNameCn] nvarchar(10) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [NationNameEn] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Remark] nvarchar(500) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NULL,
  [CreatedDate] datetime2(3)  NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[NationalityInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'国籍Id',
'SCHEMA', N'Basic',
'TABLE', N'NationalityInfo',
'COLUMN', N'NationId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'国籍名称（中文）',
'SCHEMA', N'Basic',
'TABLE', N'NationalityInfo',
'COLUMN', N'NationNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'国籍名称（英文）',
'SCHEMA', N'Basic',
'TABLE', N'NationalityInfo',
'COLUMN', N'NationNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注',
'SCHEMA', N'Basic',
'TABLE', N'NationalityInfo',
'COLUMN', N'Remark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'NationalityInfo',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'NationalityInfo',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'NationalityInfo',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'NationalityInfo',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'国籍信息表',
'SCHEMA', N'Basic',
'TABLE', N'NationalityInfo'
GO


-- ----------------------------
-- Records of NationalityInfo
-- ----------------------------
INSERT INTO [Basic].[NationalityInfo] ([NationId], [NationNameCn], [NationNameEn], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1972220728019390464', N'陆籍', N'Mainland Chinese', N'', N'1903486709602062336', N'2025-09-28 16:43:58.000', N'1903486709602062336', N'2025-11-03 21:30:34.000')
GO

INSERT INTO [Basic].[NationalityInfo] ([NationId], [NationNameCn], [NationNameEn], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1972220823855042560', N'台籍', N'Taiwanese', N'', N'1903486709602062336', N'2025-09-28 16:44:21.000', NULL, NULL)
GO

INSERT INTO [Basic].[NationalityInfo] ([NationId], [NationNameCn], [NationNameEn], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1972221275703218176', N'新加坡籍', N'Singaporean', N'', N'1903486709602062336', N'2025-09-28 16:46:08.000', NULL, NULL)
GO

INSERT INTO [Basic].[NationalityInfo] ([NationId], [NationNameCn], [NationNameEn], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1972221505962119168', N'马来西亚籍', N'Malaysian', N'', N'1903486709602062336', N'2025-09-28 16:47:03.000', NULL, NULL)
GO

INSERT INTO [Basic].[NationalityInfo] ([NationId], [NationNameCn], [NationNameEn], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1972221548811128832', N'越南籍', N'Vietnamese', N'', N'1903486709602062336', N'2025-09-28 16:47:13.000', NULL, NULL)
GO

INSERT INTO [Basic].[NationalityInfo] ([NationId], [NationNameCn], [NationNameEn], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1972221767841878016', N'墨西哥籍', N'Mexican', N'', N'1903486709602062336', N'2025-09-28 16:48:06.000', NULL, NULL)
GO

INSERT INTO [Basic].[NationalityInfo] ([NationId], [NationNameCn], [NationNameEn], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1972221819498926080', N'加拿大籍', N'Canadian', N'', N'1903486709602062336', N'2025-09-28 16:48:18.000', NULL, NULL)
GO

INSERT INTO [Basic].[NationalityInfo] ([NationId], [NationNameCn], [NationNameEn], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1972221912063021056', N'日本籍', N'Japanese', N'', N'1903486709602062336', N'2025-09-28 16:48:40.000', NULL, NULL)
GO

INSERT INTO [Basic].[NationalityInfo] ([NationId], [NationNameCn], [NationNameEn], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1972221975216656384', N'美籍', N'American', N'', N'1903486709602062336', N'2025-09-28 16:48:55.000', NULL, NULL)
GO


-- ----------------------------
-- Table structure for PositionInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[PositionInfo]') AND type IN ('U'))
	DROP TABLE [Basic].[PositionInfo]
GO

CREATE TABLE [Basic].[PositionInfo] (
  [PositionId] bigint  NOT NULL,
  [PositionNo] nvarchar(5) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [PositionNameCn] nvarchar(10) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [PositionNameEn] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [SortOrder] int  NULL,
  [Description] nvarchar(150) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[PositionInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职级Id',
'SCHEMA', N'Basic',
'TABLE', N'PositionInfo',
'COLUMN', N'PositionId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职级编码',
'SCHEMA', N'Basic',
'TABLE', N'PositionInfo',
'COLUMN', N'PositionNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职级名称（中文）',
'SCHEMA', N'Basic',
'TABLE', N'PositionInfo',
'COLUMN', N'PositionNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职级名称（英文）',
'SCHEMA', N'Basic',
'TABLE', N'PositionInfo',
'COLUMN', N'PositionNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'职级排序',
'SCHEMA', N'Basic',
'TABLE', N'PositionInfo',
'COLUMN', N'SortOrder'
GO

EXEC sp_addextendedproperty
'MS_Description', N'职级描述',
'SCHEMA', N'Basic',
'TABLE', N'PositionInfo',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'PositionInfo',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'PositionInfo',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'PositionInfo',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'PositionInfo',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职级表',
'SCHEMA', N'Basic',
'TABLE', N'PositionInfo'
GO


-- ----------------------------
-- Records of PositionInfo
-- ----------------------------
INSERT INTO [Basic].[PositionInfo] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351581732096180224', N'S12', N'董事长', N'Chairman', N'1', NULL, N'1903486709602062336', N'2025-01-23 14:28:55.000', N'1903486709602062336', N'2025-01-23 14:28:55.000')
GO

INSERT INTO [Basic].[PositionInfo] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351582085961220096', N'S11', N'总经理', N'General Manager', N'2', NULL, N'1903486709602062336', N'2025-01-23 14:29:37.000', N'1903486709602062336', N'2025-01-23 14:29:37.000')
GO

INSERT INTO [Basic].[PositionInfo] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351583500196642816', N'S10', N'副总', N'Vice President', N'3', NULL, N'1903486709602062336', N'2025-01-23 14:32:26.000', N'1903486709602062336', N'2025-01-23 14:32:26.000')
GO

INSERT INTO [Basic].[PositionInfo] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351583636813512704', N'S09', N'协理', N'Associate Vice President', N'4', NULL, N'1903486709602062336', N'2025-01-23 14:32:42.000', N'1903486709602062336', N'2025-01-23 14:32:42.000')
GO

INSERT INTO [Basic].[PositionInfo] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351584014896463872', N'S08', N'厂长', N'Plant Manager', N'5', NULL, N'1903486709602062336', N'2025-01-23 14:33:27.000', N'1903486709602062336', N'2025-01-23 14:33:27.000')
GO

INSERT INTO [Basic].[PositionInfo] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351584156689104896', N'S07', N'资深经理', N'Senior Manager', N'6', NULL, N'1903486709602062336', N'2025-01-23 14:33:44.000', N'1903486709602062336', N'2025-01-23 14:33:44.000')
GO

INSERT INTO [Basic].[PositionInfo] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351585319710883840', N'S06', N'经理', N'Manager', N'7', NULL, N'1903486709602062336', N'2025-01-23 14:36:03.000', N'1903486709602062336', N'2025-01-23 14:36:03.000')
GO

INSERT INTO [Basic].[PositionInfo] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351592278136717312', N'S05', N'副理', N'Assistant Manager', N'8', NULL, N'1903486709602062336', N'2025-01-23 14:49:52.000', N'1903486709602062336', N'2025-01-23 14:49:52.000')
GO

INSERT INTO [Basic].[PositionInfo] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351600746193223680', N'S04', N'课长', N'Section Manager', N'9', NULL, N'1903486709602062336', N'2025-01-23 15:06:42.000', N'1903486709602062336', N'2025-01-23 15:06:42.000')
GO

INSERT INTO [Basic].[PositionInfo] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351602631784529920', N'S03', N'组长', N'Team Leader', N'10', NULL, N'1903486709602062336', N'2025-01-23 15:10:27.000', N'1903486709602062336', N'2025-01-23 15:10:27.000')
GO

INSERT INTO [Basic].[PositionInfo] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351602771312246784', N'S02', N'师二', N'Engineer II', N'11', NULL, N'1903486709602062336', N'2025-01-23 15:10:43.000', N'1903486709602062336', N'2025-01-23 15:10:43.000')
GO

INSERT INTO [Basic].[PositionInfo] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351602976027836416', N'S01', N'师一', N'Engineer I', N'12', NULL, N'1903486709602062336', N'2025-01-23 15:11:08.000', N'1903486709602062336', N'2025-01-23 15:11:08.000')
GO


-- ----------------------------
-- Table structure for RoleInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[RoleInfo]') AND type IN ('U'))
	DROP TABLE [Basic].[RoleInfo]
GO

CREATE TABLE [Basic].[RoleInfo] (
  [RoleId] bigint  NOT NULL,
  [RoleCode] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [RoleNameCn] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [RoleNameEn] nvarchar(80) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Description] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [Remark] nvarchar(1000) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint DEFAULT NULL NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[RoleInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色编码',
'SCHEMA', N'Basic',
'TABLE', N'RoleInfo',
'COLUMN', N'RoleCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色名称（中文）',
'SCHEMA', N'Basic',
'TABLE', N'RoleInfo',
'COLUMN', N'RoleNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色名称（英文）',
'SCHEMA', N'Basic',
'TABLE', N'RoleInfo',
'COLUMN', N'RoleNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色说明',
'SCHEMA', N'Basic',
'TABLE', N'RoleInfo',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注',
'SCHEMA', N'Basic',
'TABLE', N'RoleInfo',
'COLUMN', N'Remark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'RoleInfo',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'RoleInfo',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'RoleInfo',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'RoleInfo',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色信息表',
'SCHEMA', N'Basic',
'TABLE', N'RoleInfo'
GO


-- ----------------------------
-- Records of RoleInfo
-- ----------------------------
INSERT INTO [Basic].[RoleInfo] ([RoleId], [RoleCode], [RoleNameCn], [RoleNameEn], [Description], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'Administrator', N'管理员', N'Administrator', N'系统管理员权限', N'', N'1903486709602062336', N'2025-03-29 01:15:16.000', N'1903486709602062336', N'2026-05-01 21:30:49.237')
GO

INSERT INTO [Basic].[RoleInfo] ([RoleId], [RoleCode], [RoleNameCn], [RoleNameEn], [Description], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1979881189825187840', N'Regular', N'普通用户', N'Regular user', N'', N'', N'1903486709602062336', N'2025-10-19 20:03:54.000', NULL, NULL)
GO


-- ----------------------------
-- Table structure for RoleMenu
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[RoleMenu]') AND type IN ('U'))
	DROP TABLE [Basic].[RoleMenu]
GO

CREATE TABLE [Basic].[RoleMenu] (
  [RoleId] bigint  NOT NULL,
  [MenuId] bigint  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint DEFAULT NULL NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[RoleMenu] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色Id
',
'SCHEMA', N'Basic',
'TABLE', N'RoleMenu',
'COLUMN', N'RoleId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'网域Id',
'SCHEMA', N'Basic',
'TABLE', N'RoleMenu',
'COLUMN', N'MenuId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'RoleMenu',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'RoleMenu',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'RoleMenu',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'RoleMenu',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色菜单表',
'SCHEMA', N'Basic',
'TABLE', N'RoleMenu'
GO


-- ----------------------------
-- Records of RoleMenu
-- ----------------------------
INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1917998505360756736', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1929897094655643648', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1929532135392284672', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1930269640165036032', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1933581101280923650', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1973378015064887296', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1922597528205922304', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1913261311937089536', N'1903486709602062336', N'2025-07-05 04:14:38.000', NULL, NULL)
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1916017930047459328', N'1903486709602062336', N'2025-07-05 04:14:38.000', NULL, NULL)
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1918006061000953856', N'1903486709602062336', N'2025-10-01 21:23:16.000', N'1903486709602062336', N'2025-10-01 21:23:16.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1903507885518884865', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1350161962451534507', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1350169511264780288', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1903507885518884864', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1910300381175484416', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1917611361962168320', N'1903486709602062336', N'2025-07-05 04:14:38.000', NULL, NULL)
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1911747107358904320', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1967176341195460608', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1938565603321319424', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1938565603321319425', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1938565603321319426', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1932766707219304448', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1933581101280923648', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1942199098723667968', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1944738755751579648', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1951689330179313664', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1979881189825187840', N'1903507885518884865', N'1903486709602062336', N'2025-10-19 20:04:48.000', N'1903486709602062336', N'2025-10-19 20:04:48.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1968272763634454528', N'1903486709602062336', N'2025-11-13 19:14:19.000', N'1903486709602062336', N'2025-11-13 19:14:19.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1968275489407766530', N'1903486709602062336', N'2025-11-13 19:14:19.000', N'1903486709602062336', N'2025-11-13 19:14:19.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1979881189825187840', N'1917998505360756736', N'1903486709602062336', N'2025-10-19 20:04:48.000', N'1903486709602062336', N'2025-10-19 20:04:48.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1979881189825187840', N'1988927293837414400', N'1903486709602062336', N'2026-05-04 14:34:37.190', N'1903486709602062336', N'2026-05-04 14:34:37.190')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1979881189825187840', N'1988928242475732992', N'1903486709602062336', N'2026-05-04 14:34:37.190', N'1903486709602062336', N'2026-05-04 14:34:37.190')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1979881189825187840', N'1988928242475732994', N'1903486709602062336', N'2026-05-04 14:34:37.190', N'1903486709602062336', N'2026-05-04 14:34:37.190')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1982707658716745729', N'1903486709602062336', N'2025-11-02 13:02:02.000', N'1903486709602062336', N'2025-11-02 13:02:02.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1982707658716745730', N'1903486709602062336', N'2025-11-02 13:02:02.000', N'1903486709602062336', N'2025-11-02 13:02:02.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1984848313438048256', N'1903486709602062336', N'2025-11-02 13:02:02.000', N'1903486709602062336', N'2025-11-02 13:02:02.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1982707658716745731', N'1903486709602062336', N'2025-11-02 13:02:02.000', N'1903486709602062336', N'2025-11-02 13:02:02.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1968275489407766528', N'1903486709602062336', N'2025-11-13 19:14:19.000', N'1903486709602062336', N'2025-11-13 19:14:19.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1968275489407766529', N'1903486709602062336', N'2025-11-13 19:14:19.000', N'1903486709602062336', N'2025-11-13 19:14:19.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1988927293837414400', N'1903486709602062336', N'2025-11-13 19:14:19.000', N'1903486709602062336', N'2025-11-13 19:14:19.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1988928242475732992', N'1903486709602062336', N'2025-11-13 19:14:19.000', N'1903486709602062336', N'2025-11-13 19:14:19.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1988927293837414401', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1988928242475732993', N'1903486709602062336', N'2025-10-19 01:24:33.000', N'1903486709602062336', N'2025-10-19 01:24:33.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1988928242475732994', N'1903486709602062336', N'2025-11-13 19:14:19.000', N'1903486709602062336', N'2025-11-13 19:14:19.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1988928242475732996', N'1903486709602062336', N'2025-11-13 19:14:19.000', N'1903486709602062336', N'2025-11-13 19:14:19.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1988928242475732997', N'1903486709602062336', N'2025-11-13 19:14:19.000', N'1903486709602062336', N'2025-11-13 19:14:19.000')
GO


-- ----------------------------
-- Table structure for RoleModule
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[RoleModule]') AND type IN ('U'))
	DROP TABLE [Basic].[RoleModule]
GO

CREATE TABLE [Basic].[RoleModule] (
  [RoleId] bigint  NOT NULL,
  [ModuleId] bigint  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint DEFAULT NULL NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[RoleModule] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色Id',
'SCHEMA', N'Basic',
'TABLE', N'RoleModule',
'COLUMN', N'RoleId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'网域Id',
'SCHEMA', N'Basic',
'TABLE', N'RoleModule',
'COLUMN', N'ModuleId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'RoleModule',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'RoleModule',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'RoleModule',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'RoleModule',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色模块绑定表',
'SCHEMA', N'Basic',
'TABLE', N'RoleModule'
GO


-- ----------------------------
-- Records of RoleModule
-- ----------------------------
INSERT INTO [Basic].[RoleModule] ([RoleId], [ModuleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1979881189825187840', N'1350161679034934501', N'1903486709602062336', N'2026-05-04 14:34:27.261', N'1903486709602062336', N'2026-05-04 14:34:27.261')
GO

INSERT INTO [Basic].[RoleModule] ([RoleId], [ModuleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1350161679034934501', N'1903486709602062336', N'2025-10-30 23:56:27.000', N'1903486709602062336', N'2025-10-30 23:56:27.000')
GO

INSERT INTO [Basic].[RoleModule] ([RoleId], [ModuleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1968271760889614336', N'1903486709602062336', N'2025-10-30 23:56:27.000', N'1903486709602062336', N'2025-10-30 23:56:27.000')
GO

INSERT INTO [Basic].[RoleModule] ([RoleId], [ModuleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1979881189825187840', N'1968271760889614336', N'1903486709602062336', N'2026-05-04 14:34:27.261', N'1903486709602062336', N'2026-05-04 14:34:27.261')
GO

INSERT INTO [Basic].[RoleModule] ([RoleId], [ModuleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1982707658716745728', N'1903486709602062336', N'2025-10-30 23:56:27.000', N'1903486709602062336', N'2025-10-30 23:56:27.000')
GO


-- ----------------------------
-- Table structure for UserAgent
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[UserAgent]') AND type IN ('U'))
	DROP TABLE [Basic].[UserAgent]
GO

CREATE TABLE [Basic].[UserAgent] (
  [SubstituteUserId] bigint  NOT NULL,
  [AgentUserId] bigint  NOT NULL,
  [StartTime] datetime2(3)  NOT NULL,
  [EndTime] datetime2(3)  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3) DEFAULT '' NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[UserAgent] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'被代理人',
'SCHEMA', N'Basic',
'TABLE', N'UserAgent',
'COLUMN', N'SubstituteUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'代理人',
'SCHEMA', N'Basic',
'TABLE', N'UserAgent',
'COLUMN', N'AgentUserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'代理开始时间',
'SCHEMA', N'Basic',
'TABLE', N'UserAgent',
'COLUMN', N'StartTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'代理结束时间',
'SCHEMA', N'Basic',
'TABLE', N'UserAgent',
'COLUMN', N'EndTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'UserAgent',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'UserAgent',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'UserAgent',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'UserAgent',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工代理信息表',
'SCHEMA', N'Basic',
'TABLE', N'UserAgent'
GO


-- ----------------------------
-- Records of UserAgent
-- ----------------------------
INSERT INTO [Basic].[UserAgent] ([SubstituteUserId], [AgentUserId], [StartTime], [EndTime], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'2050599625240481792', N'2026-05-04 00:00:00.000', N'2026-05-10 00:00:00.000', N'1903486709602062336', N'2026-05-04 23:35:50.547', NULL, NULL)
GO


-- ----------------------------
-- Table structure for UserForm
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[UserForm]') AND type IN ('U'))
	DROP TABLE [Basic].[UserForm]
GO

CREATE TABLE [Basic].[UserForm] (
  [UserId] bigint  NOT NULL,
  [FormGroupTypeId] bigint  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[UserForm] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工Id',
'SCHEMA', N'Basic',
'TABLE', N'UserForm',
'COLUMN', N'UserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单组别类型Id',
'SCHEMA', N'Basic',
'TABLE', N'UserForm',
'COLUMN', N'FormGroupTypeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'UserForm',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'UserForm',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'UserForm',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'UserForm',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工表单绑定表',
'SCHEMA', N'Basic',
'TABLE', N'UserForm'
GO


-- ----------------------------
-- Records of UserForm
-- ----------------------------
INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'1987215338470772736', N'1903486709602062336', N'2026-05-05 15:35:32.727', N'1903486709602062336', N'2026-05-05 15:35:32.727')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'1987217256446300160', N'1903486709602062336', N'2026-05-05 15:35:32.727', N'1903486709602062336', N'2026-05-05 15:35:32.727')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'1969052085492256768', N'1903486709602062336', N'2026-05-05 15:35:32.727', N'1903486709602062336', N'2026-05-05 15:35:32.727')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'1969053776929230848', N'1903486709602062336', N'2026-05-05 15:35:32.727', N'1903486709602062336', N'2026-05-05 15:35:32.727')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'1969054482025287680', N'1903486709602062336', N'2026-05-05 15:35:32.727', N'1903486709602062336', N'2026-05-05 15:35:32.727')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'1969054690842906624', N'1903486709602062336', N'2026-05-05 15:35:32.727', N'1903486709602062336', N'2026-05-05 15:35:32.727')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'1969054813085896704', N'1903486709602062336', N'2026-05-05 15:35:32.727', N'1903486709602062336', N'2026-05-05 15:35:32.727')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'1969055160932110336', N'1903486709602062336', N'2026-05-05 15:35:32.727', N'1903486709602062336', N'2026-05-05 15:35:32.727')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'1969055351626141696', N'1903486709602062336', N'2026-05-05 15:35:32.727', N'1903486709602062336', N'2026-05-05 15:35:32.727')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'1969055451307970560', N'1903486709602062336', N'2026-05-05 15:35:32.727', N'1903486709602062336', N'2026-05-05 15:35:32.727')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'1969055549681176576', N'1903486709602062336', N'2026-05-05 15:35:32.727', N'1903486709602062336', N'2026-05-05 15:35:32.727')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'1969055723409248256', N'1903486709602062336', N'2026-05-05 15:35:32.727', N'1903486709602062336', N'2026-05-05 15:35:32.727')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'1969055819815325696', N'1903486709602062336', N'2026-05-05 15:35:32.727', N'1903486709602062336', N'2026-05-05 15:35:32.727')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1987215338470772736', N'1903486709602062336', N'2026-05-03 00:23:38.514', N'1903486709602062336', N'2026-05-03 00:23:38.514')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1987217256446300160', N'1903486709602062336', N'2026-05-03 00:23:38.514', N'1903486709602062336', N'2026-05-03 00:23:38.514')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969052085492256768', N'1903486709602062336', N'2026-05-03 00:23:38.514', N'1903486709602062336', N'2026-05-03 00:23:38.514')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969053776929230848', N'1903486709602062336', N'2026-05-03 00:23:38.514', N'1903486709602062336', N'2026-05-03 00:23:38.514')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969054482025287680', N'1903486709602062336', N'2026-05-03 00:23:38.514', N'1903486709602062336', N'2026-05-03 00:23:38.514')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969054690842906624', N'1903486709602062336', N'2026-05-03 00:23:38.514', N'1903486709602062336', N'2026-05-03 00:23:38.514')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969054813085896704', N'1903486709602062336', N'2026-05-03 00:23:38.514', N'1903486709602062336', N'2026-05-03 00:23:38.514')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969055160932110336', N'1903486709602062336', N'2026-05-03 00:23:38.514', N'1903486709602062336', N'2026-05-03 00:23:38.514')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969055351626141696', N'1903486709602062336', N'2026-05-03 00:23:38.514', N'1903486709602062336', N'2026-05-03 00:23:38.514')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969055451307970560', N'1903486709602062336', N'2026-05-03 00:23:38.514', N'1903486709602062336', N'2026-05-03 00:23:38.514')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969055549681176576', N'1903486709602062336', N'2026-05-03 00:23:38.514', N'1903486709602062336', N'2026-05-03 00:23:38.514')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969055723409248256', N'1903486709602062336', N'2026-05-03 00:23:38.514', N'1903486709602062336', N'2026-05-03 00:23:38.514')
GO

INSERT INTO [Basic].[UserForm] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969055819815325696', N'1903486709602062336', N'2026-05-03 00:23:38.514', N'1903486709602062336', N'2026-05-03 00:23:38.514')
GO


-- ----------------------------
-- Table structure for UserInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[UserInfo]') AND type IN ('U'))
	DROP TABLE [Basic].[UserInfo]
GO

CREATE TABLE [Basic].[UserInfo] (
  [UserId] bigint  NOT NULL,
  [DepartmentId] bigint  NOT NULL,
  [PositionId] bigint  NOT NULL,
  [UserNo] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [UserNameCn] nvarchar(10) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [UserNameEn] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Gender] int  NOT NULL,
  [HireDate] datetime2(3)  NOT NULL,
  [Nationality] bigint  NOT NULL,
  [LaborId] bigint  NOT NULL,
  [Email] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [PhoneNumber] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [LoginNo] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [PassWord] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [PwdSalt] nvarchar(60) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [AvatarAddress] nvarchar(150) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [IsEmployed] int  NOT NULL,
  [IsFreeze] int  NOT NULL,
  [IsReview] int  NOT NULL,
  [IsRealtimeNotification] int  NOT NULL,
  [IsScheduledNotification] int  NOT NULL,
  [IsAgent] int  NOT NULL,
  [IsParttime] int  NOT NULL,
  [ExpirationDays] int  NOT NULL,
  [ExpirationTime] datetime2(3)  NOT NULL,
  [Remark] nvarchar(200) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[UserInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键，员工Id',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'UserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'所属部门Id',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'DepartmentId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'职级Id',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'PositionId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工工号',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'UserNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工姓名（中文）',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'UserNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工姓名（英文）',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'UserNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'性别',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'Gender'
GO

EXEC sp_addextendedproperty
'MS_Description', N'入职日期',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'HireDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'国籍',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'Nationality'
GO

EXEC sp_addextendedproperty
'MS_Description', N'职业Id',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'LaborId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'邮箱',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'Email'
GO

EXEC sp_addextendedproperty
'MS_Description', N'电话',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'PhoneNumber'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录账号',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'LoginNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'密码',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'PassWord'
GO

EXEC sp_addextendedproperty
'MS_Description', N'密码盐值',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'PwdSalt'
GO

EXEC sp_addextendedproperty
'MS_Description', N'头像图片地址',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'AvatarAddress'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否在职',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'IsEmployed'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否冻结',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'IsFreeze'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否审批',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'IsReview'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否实时通知邮件',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'IsRealtimeNotification'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否定时通知邮件',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'IsScheduledNotification'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否代理其他员工',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'IsAgent'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否兼职',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'IsParttime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'密码过期天数',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'ExpirationDays'
GO

EXEC sp_addextendedproperty
'MS_Description', N'密码过期时间',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'ExpirationTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'Remark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工信息表',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo'
GO


-- ----------------------------
-- Records of UserInfo
-- ----------------------------
INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1950000000000000048', N'1351602631784529920', N'E347473', N'裴小然', N'Xiaoran Pei', N'1', N'2024-07-01 00:00:00.000', N'1972220728019390464', N'1956396323422998528', N'3442105608@qq.com', N'18815384916', N'E347473', N'5kidI3cVHG5F5gRGnGDiElLQUYKfP54m5FryQDeSAlA=', N'qaThHmJcot0poPji2mMfVg==', N'/20260315/20260315005909792_70ff1e94.jpg', N'1', N'0', N'0', N'1', N'1', N'0', N'0', N'180', N'2026-09-22 18:49:27.187', N'', N'1903486709602062336', N'2025-03-23 00:39:31.000', N'1903486709602062336', N'2026-03-15 00:59:11.183')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062337', N'1950000000000000049', N'1351600746193223680', N'E347377', N'乔知强', N'Zhiqiang Qiao', N'1', N'2024-07-01 00:00:00.000', N'1972220728019390464', N'1956396323422998528', N'3442105608@qq.com', N'18815384916', N'E347377', N'5kidI3cVHG5F5gRGnGDiElLQUYKfP54m5FryQDeSAlA=', N'qaThHmJcot0poPji2mMfVg==', N'/20260315/20260315005909792_70ff1e94.jpg', N'1', N'0', N'0', N'1', N'1', N'0', N'0', N'180', N'2026-10-29 23:31:09.397', N'', N'1903486709602062336', N'2025-03-23 00:39:31.000', N'1903486709602062336', N'2026-05-02 23:31:09.397')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062338', N'1950000000000000005', N'1351592278136717312', N'E327852', N'黄仁华', N'Paul Huang', N'1', N'2024-07-01 00:00:00.000', N'1972220728019390464', N'1956395974389796864', N'3442105608@qq.com', N'18815384916', N'E327852', N'5kidI3cVHG5F5gRGnGDiElLQUYKfP54m5FryQDeSAlA=', N'qaThHmJcot0poPji2mMfVg==', N'/20260315/20260315005909792_70ff1e94.jpg', N'1', N'0', N'0', N'0', N'0', N'0', N'0', N'180', N'2026-10-29 23:52:03.773', N'', N'1903486709602062336', N'2025-03-23 00:39:31.000', N'1903486709602062336', N'2026-05-02 23:52:03.773')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'1950000000000000047', N'1351600746193223680', N'E342306', N'于长洋', N'Changyang Yu', N'1', N'2024-07-01 00:00:00.000', N'1972220728019390464', N'1956396323422998528', N'3442105608@qq.com', N'18815384916', N'E342306', N'5kidI3cVHG5F5gRGnGDiElLQUYKfP54m5FryQDeSAlA=', N'qaThHmJcot0poPji2mMfVg==', N'/20260315/20260315005909792_70ff1e94.jpg', N'1', N'0', N'1', N'1', N'1', N'0', N'0', N'180', N'2026-10-29 23:44:35.933', N'', N'1903486709602062336', N'2025-03-23 00:39:31.000', N'1903486709602062336', N'2026-05-02 23:44:35.933')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062341', N'1950000000000000156', N'1351602631784529920', N'E327853', N'郭永成', N'Yongcheng Guo', N'1', N'2024-07-01 00:00:00.000', N'1972220728019390464', N'1956381741161779200', N'3442105608@qq.com', N'18815384916', N'E327853', N'5kidI3cVHG5F5gRGnGDiElLQUYKfP54m5FryQDeSAlA=', N'qaThHmJcot0poPji2mMfVg==', N'/20260315/20260315005909792_70ff1e94.jpg', N'1', N'0', N'1', N'1', N'1', N'0', N'0', N'180', N'2026-10-29 23:52:26.237', N'', N'1903486709602062336', N'2025-03-23 00:39:31.000', N'1903486709602062336', N'2026-05-02 23:52:26.237')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062342', N'1950000000000000156', N'1351585319710883840', N'E341176', N'汤洁', N'IrisJ Tang', N'2', N'2024-07-01 00:00:00.000', N'1972220728019390464', N'1956381941884391424', N'3442105608@qq.com', N'18815384916', N'E341176', N'5kidI3cVHG5F5gRGnGDiElLQUYKfP54m5FryQDeSAlA=', N'qaThHmJcot0poPji2mMfVg==', N'/20260315/20260315005909792_70ff1e94.jpg', N'1', N'0', N'1', N'1', N'1', N'0', N'0', N'180', N'2026-10-29 23:52:18.157', N'', N'1903486709602062336', N'2025-03-23 00:39:31.000', N'1903486709602062336', N'2026-05-02 23:52:18.157')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050596970468347904', N'1950000000000000001', N'1351581732096180224', N'E215396', N'蔡嘉祥', N'Tsai', N'1', N'2026-05-02 00:00:00.000', N'1972220823855042560', N'1962083956962758656', N'3442105608@qq.com', N'', N'E215396', N'caKSbDvNRT5aX9j3KjZBauXqO42AQTrakirGgL5LPgo=', N'zoAXyNGzR/m/8jWJj6d4XQ==', N'/20260502/20260502232305849_3c77c953.jpg', N'1', N'0', N'1', N'1', N'1', N'0', N'0', N'180', N'2026-10-29 23:23:10.897', N'', N'1903486709602062336', N'2026-05-02 23:23:10.897', NULL, NULL)
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050597318784323584', N'1950000000000000005', N'1351585319710883840', N'E325994', N'王家豪', N'Sky Wang', N'1', N'2026-04-30 00:00:00.000', N'1972220823855042560', N'1956395974389796864', N'3442105608@qq.com', N'', N'E325994', N'kJbfdFn8iJnvVwl/TajyrZ/5mH14eAD8cTJhOEI4bgc=', N'i6ACUgwgmUVQdt/o5jIDvA==', N'/20260502/20260502232430063_993058a7.jpg', N'1', N'0', N'1', N'1', N'1', N'0', N'0', N'90', N'2026-08-03 12:53:50.870', N'', N'1903486709602062336', N'2026-05-02 23:24:34.023', N'1903486709602062336', N'2026-05-05 12:53:50.870')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050598466639499264', N'1950000000000000048', N'1351602631784529920', N'E347072', N'丁甲乙', N'Darren Ding', N'1', N'2026-05-03 00:00:00.000', N'1972220728019390464', N'1956396323422998528', N'3442105608@qq.com', N'', N'E347072', N'EGXT49YfoB0YYHC2DLXhmMLhnkU2cIexQDwHnhvPq7c=', N'WHPj9elZSaxZbwoGJGn3LQ==', N'/20260502/20260502232902659_862995dd.jpg', N'1', N'0', N'0', N'0', N'0', N'0', N'0', N'60', N'2026-07-01 23:45:56.223', N'', N'1903486709602062336', N'2026-05-02 23:29:07.520', N'1903486709602062336', N'2026-05-02 23:45:56.223')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050599298114129920', N'1950000000000000005', N'1351584156689104896', N'E347075', N'徐奭杰', N'Marcus Hsu', N'1', N'2026-05-02 00:00:00.000', N'1972220823855042560', N'1956395974389796864', N'3442105608@qq.com', N'', N'E347075', N'1GO31zqgCJBoSAjxM3v6QqVk7qM4oeuMy9Tb/wfMGi0=', N'vHbe1x2ukt46nFnYp4s0pg==', N'/20260502/20260502233224779_ce3375e9.jpg', N'1', N'0', N'1', N'1', N'1', N'0', N'0', N'90', N'2026-07-31 23:35:06.047', N'', N'1903486709602062336', N'2026-05-02 23:32:25.753', N'1903486709602062336', N'2026-05-02 23:35:06.047')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050599625240481792', N'1950000000000000005', N'1351585319710883840', N'E347074', N'姜佑康', N'Bryan Chiang', N'1', N'2026-05-02 00:00:00.000', N'1972220823855042560', N'1956395974389796864', N'3442105608@qq.com', N'', N'E347074', N'DzqwpeydU7tEsdwJKCSg2DYBlqTHR0Ie+wbinvK5JKk=', N'qI4aXDlPBnMpQeb1mt5CLA==', N'/20260502/20260502233341246_1ebd3e0b.jpg', N'1', N'0', N'1', N'1', N'1', N'1', N'0', N'90', N'2026-07-31 23:33:43.740', N'', N'1903486709602062336', N'2026-05-02 23:33:43.740', NULL, NULL)
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050599950684917760', N'1950000000000000048', N'1351602631784529920', N'E348184', N'谭冰莹', N'Ice Tan', N'1', N'2026-05-02 00:00:00.000', N'1972220728019390464', N'1956396323422998528', N'3442105608@qq.com', N'', N'E348184', N'VOcxuA54iDAoY392wCdKUud2YEvs/TbOcdoshMGKf54=', N'vSlis0andTslXhBow/RDuw==', N'/20260502/20260502233453693_cb561e79.jpg', N'1', N'0', N'0', N'0', N'0', N'0', N'0', N'60', N'2026-07-02 00:23:59.380', N'', N'1903486709602062336', N'2026-05-02 23:35:01.310', N'1903486709602062336', N'2026-05-03 00:23:59.380')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050600303354580992', N'1950000000000000048', N'1351602631784529920', N'E352462', N'陈亮', N'Liang Chen', N'1', N'2026-05-02 00:00:00.000', N'1972220728019390464', N'1956396323422998528', N'3442105608@qq.com', N'', N'E352462', N'SIIMV2ecNOzmeu5r5OV4A5W/t7QuuHSOoW/tfnlAqS0=', N'VnZAVbuvS0DOLOhmfex3Qg==', N'/20260502/20260502233614754_a47d69e7.jpg', N'1', N'0', N'0', N'0', N'0', N'0', N'0', N'60', N'2026-07-01 23:36:25.420', N'', N'1903486709602062336', N'2026-05-02 23:36:25.420', NULL, NULL)
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050600734554198016', N'1950000000000000048', N'1351602771312246784', N'E346899', N'龚喆浩', N'Zhehao Gong', N'1', N'2026-05-02 00:00:00.000', N'1972220728019390464', N'1956395974389796864', N'3442105608@qq.com', N'', N'E346899', N'fjCkXiHe1Mggh6Nj8s01ph8ESQVZfci89x58zgx6tb4=', N'jxSVXkWec5QGeA8IrNMj5g==', N'/20260502/20260502233806632_17cbe66a.jpg', N'1', N'0', N'0', N'0', N'0', N'0', N'0', N'60', N'2026-07-01 23:46:00.310', N'', N'1903486709602062336', N'2026-05-02 23:38:08.250', N'1903486709602062336', N'2026-05-02 23:46:00.310')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050601504599052288', N'1950000000000000002', N'1351583636813512704', N'E350428', N'李静远', N'Steven Lee', N'1', N'2026-05-02 00:00:00.000', N'1972220823855042560', N'1956386428401356800', N'3442105608@qq.com', N'', N'E350428', N'CEv3ZJ7kEn1ZU4iBQzUmCcts77B5sAeg6tLu9PEl4xs=', N'tZHyJooVI/FXg0aQHeoVpA==', N'/20260502/20260502234110894_f1c8ed75.jpg', N'1', N'0', N'1', N'1', N'1', N'0', N'1', N'180', N'2026-10-29 23:46:18.460', N'', N'1903486709602062336', N'2026-05-02 23:41:11.817', N'1903486709602062336', N'2026-05-02 23:46:18.460')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050601857436487680', N'1950000000000000063', N'1351584014896463872', N'ETW00327', N'吴建中', N'Jeffrey Wu', N'1', N'2026-05-02 00:00:00.000', N'1972220823855042560', N'2032485639995396096', N'3442105608@qq.com', N'', N'ETW00327', N'eaiWnWoOUqHKgm2ysHztSNJyB8z8zW0MEmGzUkZDFUU=', N'i28cI46VbvkjI93JTX9XAA==', N'/20260502/20260502234232468_2ac782cb.jpg', N'1', N'0', N'1', N'1', N'1', N'0', N'0', N'90', N'2026-07-31 23:46:26.270', N'', N'1903486709602062336', N'2026-05-02 23:42:35.943', N'1903486709602062336', N'2026-05-02 23:46:26.270')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050602218733834240', N'1950000000000000001', N'1351583500196642816', N'ETW00225', N'蔡佳桦', N'Jessie Tsai', N'2', N'2026-05-02 00:00:00.000', N'1972220823855042560', N'2050603571677892608', N'3442105608@qq.com', N'', N'ETW00225', N'exYbajfqHuPCPRUnC6+FS+5YdKr2ogg0GvSLwqApWI0=', N'fXK2vnFyRKLwYjpWWcKQDg==', N'/20260502/20260502234359014_7ab43fdb.jpg', N'1', N'0', N'1', N'1', N'1', N'0', N'0', N'180', N'2026-10-29 23:51:37.590', N'', N'1903486709602062336', N'2026-05-02 23:44:02.067', N'1903486709602062336', N'2026-05-02 23:51:37.590')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsFreeze], [IsReview], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050603324033601536', N'1950000000000000182', N'1351592278136717312', N'ETW00228', N'江宜蓁', N'Yichen Jiang', N'2', N'2026-05-02 00:00:00.000', N'1972220728019390464', N'2050603970254213120', N'3442105608@qq.com', N'', N'ETW00228', N'AHiyg8T1NIud1E3fph2U/NcJ8e59WM1b4A7m2CRfbgI=', N'K0akjQR3XaCNbhQmhjBaKg==', N'/20260502/20260502234810837_003f8143.jpg', N'1', N'0', N'1', N'1', N'1', N'0', N'0', N'90', N'2026-07-31 23:52:53.933', N'', N'1903486709602062336', N'2026-05-02 23:48:25.577', N'1903486709602062336', N'2026-05-02 23:52:53.933')
GO


-- ----------------------------
-- Table structure for UserLabor
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[UserLabor]') AND type IN ('U'))
	DROP TABLE [Basic].[UserLabor]
GO

CREATE TABLE [Basic].[UserLabor] (
  [LaborId] bigint  NOT NULL,
  [LaborNameCn] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [LaborNameEn] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Description] nvarchar(200) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[UserLabor] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'职业Id',
'SCHEMA', N'Basic',
'TABLE', N'UserLabor',
'COLUMN', N'LaborId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'职业名称（中文）',
'SCHEMA', N'Basic',
'TABLE', N'UserLabor',
'COLUMN', N'LaborNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'职业名称（英文）',
'SCHEMA', N'Basic',
'TABLE', N'UserLabor',
'COLUMN', N'LaborNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'职业描述',
'SCHEMA', N'Basic',
'TABLE', N'UserLabor',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'UserLabor',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'UserLabor',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'UserLabor',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'UserLabor',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户职业表',
'SCHEMA', N'Basic',
'TABLE', N'UserLabor'
GO


-- ----------------------------
-- Records of UserLabor
-- ----------------------------
INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956372741586292736', N'模具设计工程师', N'Mold Design Engineer', N'', N'1903486709602062336', N'2025-08-15 23:09:43.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956372801044746240', N'工艺工程师（冲压/塑性加工）', N'Process Engineer (Stamping/Metal Forming)', N'', N'1903486709602062336', N'2025-08-15 23:09:58.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956373542572527616', N'产品设计工程师', N'Product Design Engineer', N'', N'1903486709602062336', N'2025-08-15 23:12:54.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956373586331701248', N'数控加工中心操作工（CNC）', N'CNC Machining Center Operator', N'', N'1903486709602062336', N'2025-08-15 23:13:05.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956373639041519616', N'冲压工', N'Stamping Machine Operator', N'', N'1903486709602062336', N'2025-08-15 23:13:17.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956373676479877120', N'锻造工', N'Forging Worker', N'', N'1903486709602062336', N'2025-08-15 23:13:26.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956373733740515328', N'铣工/磨工/钳工', N'Milling/Grinding/Fitting Machinist', N'', N'1903486709602062336', N'2025-08-15 23:13:40.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956373785909268480', N'注塑机操作工', N'Injection Molding Machine Operator', N'', N'1903486709602062336', N'2025-08-15 23:13:52.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956373841055977472', N'压铸工', N'Die Casting Operator', N'', N'1903486709602062336', N'2025-08-15 23:14:06.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956373889953173504', N'质量检验员（IQC/IPQC/FQC）', N'Quality Inspector (IQC/IPQC/FQC)', N'', N'1903486709602062336', N'2025-08-15 23:14:17.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956373957489856512', N'测量员（三坐标检测）', N'CMM Measurement Technician', N'', N'1903486709602062336', N'2025-08-15 23:14:33.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956374013085356032', N'设备维修技师', N'Equipment Maintenance Technician', N'', N'1903486709602062336', N'2025-08-15 23:14:47.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956374058807463936', N'模具维修工', N'Mold Maintenance Technician', N'', N'1903486709602062336', N'2025-08-15 23:14:57.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956374113769623552', N'生产计划员（PMC）', N'Production Planner (PMC)', N'', N'1903486709602062336', N'2025-08-15 23:15:11.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956374164885606400', N'车间主管/生产主管', N'Workshop/Production Supervisor', N'', N'1903486709602062336', N'2025-08-15 23:15:23.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956377196675338240', N'内部审计师', N'Internal Auditor', N'', N'1903486709602062336', N'2025-08-15 23:27:26.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956379911082086400', N'法务专员', N'Legal Officer', N'', N'1903486709602062336', N'2025-08-15 23:38:13.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956381478782898176', N'合规经理', N'Compliance Manager', N'', N'1903486709602062336', N'2025-08-15 23:44:27.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956381569736380416', N'知识产权专员', N'Intellectual Property Specialist', N'', N'1903486709602062336', N'2025-08-15 23:44:48.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956381741161779200', N'行政助理', N'Administrative Assistant', N'', N'1903486709602062336', N'2025-08-15 23:45:29.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956381830471094272', N'人力资源专员', N'Human Resources Specialist', N'', N'1903486709602062336', N'2025-08-15 23:45:50.000', N'1903486709602062336', N'2025-10-19 02:11:01.000')
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956381885357756416', N'前台接待', N'Receptionist', N'', N'1903486709602062336', N'2025-08-15 23:46:03.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956381941884391424', N'行政经理', N'Administrative Manager', N'', N'1903486709602062336', N'2025-08-15 23:46:17.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956382070293008384', N'文秘', N'Secretary', N'', N'1903486709602062336', N'2025-08-15 23:46:48.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956386297220304896', N'司机', N'driver', N'', N'1903486709602062336', N'2025-08-16 00:03:35.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956386428401356800', N'首席技术官（CTO）', N'Chief Technology Officer (CTO)', N'', N'1903486709602062336', N'2025-08-16 00:04:07.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956386472462520320', N'首席财务官（CFO）', N'Chief Financial Officer (CFO)', N'', N'1903486709602062336', N'2025-08-16 00:04:17.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956386518260125696', N'首席运营官（COO）', N'Chief Operating Officer (COO)', N'', N'1903486709602062336', N'2025-08-16 00:04:28.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956386570793783296', N'首席执行官（CEO）', N'Chief Executive Officer (CEO)', N'', N'1903486709602062336', N'2025-08-16 00:04:41.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956395639919218688', N'设备经理', N'Equipment Manager', N'', N'1903486709602062336', N'2025-08-16 00:40:43.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956395708672249856', N'质量经理', N'Quality Manager', N'', N'1903486709602062336', N'2025-08-16 00:40:59.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956395778259947520', N'生产经理', N'Production Manager', N'', N'1903486709602062336', N'2025-08-16 00:41:16.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956395917238210560', N'信息安全工程师', N'Information Security Engineer', N'', N'1903486709602062336', N'2025-08-16 00:41:49.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956395974389796864', N'IT项目经理', N'IT Project Manager', N'', N'1903486709602062336', N'2025-08-16 00:42:03.000', N'1903486709602062336', N'2026-03-08 03:19:45.190')
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956396031587520512', N'网络与系统管理员', N'Network & Systems Administrator', N'', N'1903486709602062336', N'2025-08-16 00:42:16.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956396264467861504', N'系统架构师', N'System Architect', N'', N'1903486709602062336', N'2025-08-16 00:43:12.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956396323422998528', N'软件开发工程师', N'Software Development Engineer', N'', N'1903486709602062336', N'2025-08-16 00:43:26.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1962083956962758656', N'董事长', N'Chairman', N'', N'1903486709602062336', N'2025-08-31 17:24:03.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1962084148730531840', N'副董事长', N'Vice Chairman', N'', N'1903486709602062336', N'2025-08-31 17:24:49.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1962091425965412352', N'网络安全工程师', N'Network Security Engineer', N'', N'1903486709602062336', N'2025-08-31 17:53:44.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969082012346224640', N'总经理', N'General Manager', N'', N'1903486709602062336', N'2025-09-20 00:51:50.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969082186200125440', N'稽核会计师', N'Audit Accountant', N'', N'1903486709602062336', N'2025-09-20 00:52:31.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969082846354214912', N'稽核', N'Audit', N'', N'1903486709602062336', N'2025-09-20 00:55:09.000', N'1903486709602062336', N'2025-09-20 01:03:07.000')
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969084707446591488', N'财务', N'Financial', N'', N'1903486709602062336', N'2025-09-20 01:02:32.000', N'1903486709602062336', N'2025-09-20 01:03:02.000')
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2032485382708400128', N'行政厂长', N'Operations Plant Manager', N'负责工厂综合运营
Responsible for overall plant operations', N'1903486709602062336', N'2026-03-13 23:54:11.647', N'1903486709602062336', N'2026-03-13 23:58:31.953')
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2032485639995396096', N'生产厂长', N'Production Plant Manager', N'负责生产制造
Responsible for manufacturing operations', N'1903486709602062336', N'2026-03-13 23:55:12.990', N'1903486709602062336', N'2026-03-13 23:58:21.943')
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050603571677892608', N'副总', N'Vice President', N'', N'1903486709602062336', N'2026-05-02 23:49:24.517', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050603970254213120', N'经管', N'Manufacturing Manager', N'', N'1903486709602062336', N'2026-05-02 23:50:59.547', NULL, NULL)
GO

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050604062319185920', N'采购', N'Procurement', N'', N'1903486709602062336', N'2026-05-02 23:51:21.497', NULL, NULL)
GO


-- ----------------------------
-- Table structure for UserLock
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[UserLock]') AND type IN ('U'))
	DROP TABLE [Basic].[UserLock]
GO

CREATE TABLE [Basic].[UserLock] (
  [UserId] bigint  NOT NULL,
  [NumberErrors] int  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL
)
GO

ALTER TABLE [Basic].[UserLock] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工Id',
'SCHEMA', N'Basic',
'TABLE', N'UserLock',
'COLUMN', N'UserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'密码错误次数',
'SCHEMA', N'Basic',
'TABLE', N'UserLock',
'COLUMN', N'NumberErrors'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'UserLock',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工锁定记录',
'SCHEMA', N'Basic',
'TABLE', N'UserLock'
GO


-- ----------------------------
-- Records of UserLock
-- ----------------------------

-- ----------------------------
-- Table structure for UserLogOut
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[UserLogOut]') AND type IN ('U'))
	DROP TABLE [Basic].[UserLogOut]
GO

CREATE TABLE [Basic].[UserLogOut] (
  [UserId] bigint  NULL,
  [IP] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [LoginType] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [LoginDate] datetime2(3)  NOT NULL
)
GO

ALTER TABLE [Basic].[UserLogOut] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工登录日志表',
'SCHEMA', N'Basic',
'TABLE', N'UserLogOut'
GO


-- ----------------------------
-- Records of UserLogOut
-- ----------------------------
INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'198.18.0.1', N'LoginSuccessful', N'2025-12-27 14:30:08.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'198.18.0.1', N'LoginSuccessful', N'2025-12-27 15:42:52.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'198.18.0.1', N'LoginSuccessful', N'2025-12-27 15:11:35.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'198.18.0.1', N'LoggedOut', N'2025-12-27 15:32:39.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2025-12-30 15:00:51.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2025-12-30 16:28:28.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-01-02 10:20:45.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:20:52.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:20:53.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:20:54.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:20:54.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:20:55.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:22:15.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:22:18.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-01-02 10:22:29.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:24:28.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:24:29.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:24:29.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:24:30.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:24:30.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-01-02 10:25:33.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-01-02 13:05:15.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 14:52:54.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 14:52:57.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 14:52:58.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 14:52:58.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 14:52:59.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 16:47:32.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-01-02 16:47:39.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 16:49:42.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 16:49:43.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 16:49:44.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 16:49:44.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 16:49:45.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'198.18.0.1', N'LoginSuccessful', N'2026-01-03 20:41:08.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-01-05 14:43:05.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-01-05 14:43:13.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-01-10 13:41:08.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-01-10 15:59:12.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-01-12 16:40:06.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.144.86.241', N'LoginSuccessful', N'2026-01-12 18:29:52.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.144.86.241', N'LoginSuccessful', N'2026-01-12 18:54:07.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-02-03 16:46:59.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.57.28.84', N'LoginSuccessful', N'2026-02-05 10:56:10.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.57.28.84', N'LoginSuccessful', N'2026-02-05 11:05:27.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-02-05 11:08:55.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-02-05 11:11:06.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-02-06 10:35:42.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-02-06 10:38:00.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-02-06 11:11:24.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-02-06 11:28:00.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-02-06 11:28:02.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-02-06 11:28:03.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-02-06 11:28:03.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-02-06 11:28:04.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-02-06 15:30:47.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-02-06 15:31:15.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'IncorrectPassword', N'2026-02-06 15:57:12.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-02-06 15:57:21.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-02-06 16:16:17.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-02-06 16:16:35.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-02-09 15:53:48.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-02-09 15:53:56.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-02-10 09:04:14.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-02-11 09:14:07.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-02-11 09:14:17.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-02-11 14:19:32.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-02-11 14:28:01.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-02-11 14:28:10.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-02-11 14:29:58.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'IncorrectPassword', N'2026-02-11 16:49:00.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-02-11 16:49:25.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'127.0.0.1', N'LoginSuccessful', N'2026-02-14 12:02:40.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'IncorrectPassword', N'2026-02-14 18:03:48.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-02-14 18:03:54.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-02-14 18:16:14.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-02-18 11:10:21.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-02-18 11:15:21.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-02-18 11:18:25.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-02-27 15:24:26.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-02-28 13:16:22.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.23', N'LoginSuccessful', N'2026-02-28 16:24:03.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1969079079705645056', N'::1', N'LoginSuccessful', N'2026-03-04 11:01:29.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-04 11:02:45.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-05 10:31:07.570')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-05 13:19:33.767')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-06 08:40:21.707')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-02-28 16:27:04.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-06 11:31:14.390')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-06 13:04:57.020')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-06 16:27:44.673')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-07 23:23:27.497')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-08 03:24:58.093')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-08 03:25:51.087')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-08 02:02:55.687')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-08 02:26:37.237')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-08 03:10:55.530')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.23', N'LoginSuccessful', N'2026-03-08 23:13:51.523')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'192.168.252.1', N'LoginSuccessful', N'2026-03-08 23:15:23.993')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-09 09:40:04.043')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-09 13:01:57.783')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-09 13:27:53.800')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-09 16:08:44.910')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.23', N'LoginSuccessful', N'2026-03-10 15:06:34.180')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-13 13:42:45.750')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-13 14:44:39.860')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-13 21:47:12.457')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-13 21:51:22.273')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-13 21:57:26.793')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-13 23:57:54.973')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-14 21:03:33.423')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-14 21:03:41.290')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-14 22:33:09.870')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-14 22:39:37.227')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-15 00:58:53.477')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-03-15 00:59:18.117')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-15 16:32:47.977')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-15 18:34:02.087')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-15 20:34:39.660')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-15 00:59:24.230')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'127.0.0.1', N'LoginSuccessful', N'2026-03-16 10:00:16.273')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-16 16:26:22.880')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-16 18:17:55.190')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-17 10:32:08.493')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-16 20:21:25.647')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-17 12:33:56.470')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'127.0.0.1', N'LoginSuccessful', N'2026-03-19 16:42:46.320')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'127.0.0.1', N'LoginSuccessful', N'2026-03-19 16:42:50.813')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-20 13:05:30.443')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-20 21:16:14.450')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-21 13:29:33.003')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-22 00:56:37.083')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-22 01:43:27.527')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-22 18:18:37.340')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-03-22 18:22:04.583')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-22 18:22:33.140')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-03-22 18:23:28.137')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-03-22 18:55:18.123')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'0', N'::1', N'AccountNotExist', N'2026-03-22 18:56:48.347')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1962091248886091776', N'::1', N'LoginSuccessful', N'2026-03-22 18:57:31.713')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1962091248886091776', N'::1', N'LoggedOut', N'2026-03-22 19:08:08.160')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-21 11:17:59.910')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-03-22 03:03:24.893')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1961651950017712128', N'::1', N'LoginSuccessful', N'2026-03-22 03:03:37.853')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1961651950017712128', N'::1', N'LoggedOut', N'2026-03-22 03:03:57.580')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-22 03:04:03.107')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-03-22 03:04:28.987')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-22 03:04:35.247')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-03-22 03:04:42.877')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1961651950017712128', N'::1', N'LoginSuccessful', N'2026-03-22 03:04:53.377')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1961651950017712128', N'::1', N'LoggedOut', N'2026-03-22 03:16:22.980')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-22 03:16:28.523')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-03-22 03:16:52.483')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1961651950017712128', N'::1', N'LoginSuccessful', N'2026-03-22 03:17:01.293')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1961651950017712128', N'::1', N'LoggedOut', N'2026-03-22 03:17:59.140')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-22 03:18:11.130')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-03-22 03:18:36.713')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-22 03:18:42.167')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-22 18:31:23.993')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-22 19:14:28.820')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-23 13:05:36.810')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-23 14:55:36.113')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-23 19:30:26.463')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-23 20:42:59.003')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'127.0.0.1', N'LoginSuccessful', N'2026-03-23 22:08:05.760')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'127.0.0.1', N'LoggedOut', N'2026-03-23 22:10:00.940')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'127.0.0.1', N'LoginSuccessful', N'2026-03-23 22:10:11.403')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'127.0.0.1', N'LoggedOut', N'2026-03-23 22:14:03.133')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'127.0.0.1', N'LoginSuccessful', N'2026-03-23 22:14:42.827')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-24 17:00:05.363')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-24 18:17:04.997')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-24 18:29:05.970')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-24 20:30:51.907')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-25 15:42:47.823')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-26 18:28:06.743')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-03-26 18:36:26.147')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-26 18:49:36.540')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'IncorrectPassword', N'2026-03-27 15:45:40.570')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-27 15:45:49.973')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'IncorrectPassword', N'2026-03-30 14:53:20.450')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'IncorrectPassword', N'2026-03-30 14:53:31.873')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-30 14:53:40.407')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'IncorrectPassword', N'2026-03-31 10:50:52.717')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-31 10:51:02.033')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-03-31 16:45:52.890')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-03 14:52:50.700')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-07 15:09:15.013')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-17 15:41:44.710')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-17 23:19:23.360')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-18 10:16:11.180')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-18 13:05:09.630')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-21 14:42:17.030')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-22 14:02:11.437')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'IncorrectPassword', N'2026-04-23 11:37:36.170')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-23 11:37:46.820')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-24 10:56:03.160')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-27 09:34:38.910')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-27 13:02:55.280')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'IncorrectPassword', N'2026-04-28 15:01:56.407')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'IncorrectPassword', N'2026-04-28 15:02:02.030')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-28 15:02:14.927')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-29 11:38:40.580')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-30 11:29:19.963')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-30 13:14:39.233')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-27 14:10:55.990')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-27 14:38:37.783')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'127.0.0.1', N'LoginSuccessful', N'2026-04-28 16:11:28.573')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-29 15:11:57.443')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-04-30 15:26:29.830')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-02 22:43:34.753')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-03 19:20:36.387')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062337', N'127.0.0.1', N'LoginSuccessful', N'2026-05-04 14:35:00.957')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-01 20:26:19.993')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'127.0.0.1', N'LoginSuccessful', N'2026-05-04 14:18:37.280')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'127.0.0.1', N'LoggedOut', N'2026-05-04 14:34:49.327')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062337', N'127.0.0.1', N'LoggedOut', N'2026-05-04 14:35:09.330')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'127.0.0.1', N'LoginSuccessful', N'2026-05-04 14:35:15.150')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-04 19:14:45.913')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-04 23:37:08.483')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-04 23:37:13.347')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 11:29:00.567')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 11:29:05.687')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-05 12:31:18.670')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 12:31:25.593')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 12:35:13.453')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 12:35:19.943')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-05 12:35:57.763')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062341', N'::1', N'LoginSuccessful', N'2026-05-05 12:36:20.437')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062341', N'::1', N'LoggedOut', N'2026-05-05 12:51:30.627')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 12:51:54.983')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-05 12:52:12.533')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062341', N'::1', N'LoginSuccessful', N'2026-05-05 12:52:16.737')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 12:54:45.510')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'0', N'::1', N'AccountNotExist', N'2026-05-05 12:54:54.087')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'0', N'::1', N'AccountNotExist', N'2026-05-05 12:54:58.503')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'0', N'::1', N'AccountNotExist', N'2026-05-05 12:55:05.173')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoginSuccessful', N'2026-05-05 12:55:51.033')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-05 13:27:24.067')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 13:27:29.470')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 13:28:04.863')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 13:28:11.303')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-05 13:32:43.013')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 13:33:03.420')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 13:35:41.483')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 14:01:23.853')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 14:01:42.363')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-05 14:21:19.363')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 14:21:25.867')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 14:22:07.850')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 14:22:12.270')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-05 14:22:37.937')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoginSuccessful', N'2026-05-05 14:22:41.940')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoggedOut', N'2026-05-05 14:23:03.863')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062341', N'::1', N'LoginSuccessful', N'2026-05-05 14:23:10.173')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062341', N'::1', N'LoggedOut', N'2026-05-05 14:23:57.177')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 14:24:02.480')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 14:27:33.010')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoginSuccessful', N'2026-05-05 14:27:38.077')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoggedOut', N'2026-05-05 14:38:24.710')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoginSuccessful', N'2026-05-05 14:38:33.287')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062341', N'::1', N'LoggedOut', N'2026-05-05 14:47:14.337')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 14:47:19.933')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 14:48:54.627')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 14:48:58.347')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-05 14:49:36.013')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoginSuccessful', N'2026-05-05 14:49:40.133')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoggedOut', N'2026-05-05 15:50:43.213')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoginSuccessful', N'2026-05-05 15:50:47.687')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoggedOut', N'2026-05-05 15:55:34.710')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoginSuccessful', N'2026-05-05 15:55:38.907')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-04 21:40:51.597')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 11:27:35.813')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-05 11:45:39.103')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062341', N'::1', N'LoginSuccessful', N'2026-05-05 11:45:49.677')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-05 11:49:13.573')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 11:49:19.063')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 11:50:46.817')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 11:52:28.287')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 11:52:32.307')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 11:52:47.330')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-04 23:43:24.020')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062341', N'::1', N'LoginSuccessful', N'2026-05-04 23:43:28.803')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-05 11:43:56.847')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'IncorrectPassword', N'2026-05-05 11:44:02.487')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'IncorrectPassword', N'2026-05-05 11:44:07.313')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 11:44:13.483')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 11:44:42.573')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062341', N'::1', N'LoggedOut', N'2026-05-05 12:53:05.507')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 12:53:11.287')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoggedOut', N'2026-05-05 13:02:47.717')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 13:02:54.083')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 13:06:11.533')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'0', N'::1', N'AccountNotExist', N'2026-05-05 13:06:15.587')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoginSuccessful', N'2026-05-05 13:06:29.093')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoggedOut', N'2026-05-05 13:07:59.193')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoginSuccessful', N'2026-05-05 13:08:05.237')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoggedOut', N'2026-05-05 13:08:16.277')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 13:08:35.497')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 13:38:30.097')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 13:38:38.560')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoggedOut', N'2026-05-05 14:37:08.983')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 14:37:14.157')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 14:37:45.327')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoginSuccessful', N'2026-05-05 14:37:49.193')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoggedOut', N'2026-05-05 14:38:52.760')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 14:38:57.283')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-05 14:39:16.610')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062341', N'::1', N'LoginSuccessful', N'2026-05-05 14:39:20.883')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoggedOut', N'2026-05-05 14:50:20.713')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062341', N'::1', N'IncorrectPassword', N'2026-05-05 14:51:06.300')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062341', N'::1', N'LoginSuccessful', N'2026-05-05 14:51:10.547')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062341', N'::1', N'LoggedOut', N'2026-05-05 15:32:50.877')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 15:32:58.407')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoggedOut', N'2026-05-05 15:36:03.013')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 15:36:10.413')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 15:36:56.727')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoginSuccessful', N'2026-05-05 15:37:03.223')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoggedOut', N'2026-05-05 15:54:32.543')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoginSuccessful', N'2026-05-05 15:54:39.747')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoggedOut', N'2026-05-05 15:55:06.037')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoginSuccessful', N'2026-05-05 15:55:09.840')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoggedOut', N'2026-05-05 15:55:52.117')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 15:55:55.813')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-05 16:06:30.527')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 16:06:34.390')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-05 16:07:02.827')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599950684917760', N'::1', N'LoginSuccessful', N'2026-05-05 16:07:21.433')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599950684917760', N'::1', N'LoggedOut', N'2026-05-05 16:08:02.200')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599298114129920', N'::1', N'LoginSuccessful', N'2026-05-05 16:08:06.040')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599298114129920', N'::1', N'LoggedOut', N'2026-05-05 16:11:44.267')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062341', N'::1', N'LoginSuccessful', N'2026-05-05 16:11:57.357')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoginSuccessful', N'2026-05-05 21:28:07.883')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoggedOut', N'2026-05-05 21:28:48.077')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 21:29:03.483')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 11:45:10.963')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062341', N'::1', N'LoggedOut', N'2026-05-05 11:47:08.830')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 11:47:14.667')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 11:47:45.243')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-05 11:47:49.597')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 15:34:51.220')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoginSuccessful', N'2026-05-05 15:35:06.763')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoggedOut', N'2026-05-05 15:35:14.007')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-05 15:35:19.233')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-05 15:35:35.437')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoginSuccessful', N'2026-05-05 15:35:39.420')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoginSuccessful', N'2026-05-06 10:15:19.273')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-07 14:14:48.590')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-07 10:29:44.707')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-07 13:23:36.513')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoginSuccessful', N'2026-05-07 13:43:19.677')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-07 13:46:33.307')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-07 21:29:17.683')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-07 22:31:15.187')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-07 22:31:28.033')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-07 22:36:25.713')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoginSuccessful', N'2026-05-07 22:36:31.530')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoggedOut', N'2026-05-07 22:36:35.977')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-07 22:36:50.513')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoggedOut', N'2026-05-07 22:37:23.273')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-07 22:37:36.900')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoggedOut', N'2026-05-07 22:40:39.320')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoginSuccessful', N'2026-05-07 22:40:51.477')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoggedOut', N'2026-05-07 22:46:06.120')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoginSuccessful', N'2026-05-07 22:46:11.963')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-09 11:18:12.237')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-09 14:48:07.420')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-09 14:51:33.883')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-09 14:39:34.377')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062340', N'::1', N'LoginSuccessful', N'2026-05-09 15:03:26.700')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599625240481792', N'::1', N'LoginSuccessful', N'2026-05-09 15:25:03.590')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoginSuccessful', N'2026-05-09 15:26:15.307')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050601504599052288', N'::1', N'LoginSuccessful', N'2026-05-09 15:30:00.210')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599298114129920', N'::1', N'LoginSuccessful', N'2026-05-09 15:49:11.830')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-13 15:06:45.483')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050599298114129920', N'::1', N'LoginSuccessful', N'2026-05-13 16:45:04.413')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'2050597318784323584', N'::1', N'LoginSuccessful', N'2026-05-14 15:03:37.153')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [LoginType], [LoginDate]) VALUES (N'1903486709602062336', N'::1', N'LoginSuccessful', N'2026-05-15 17:15:55.707')
GO


-- ----------------------------
-- Table structure for UserPartTime
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[UserPartTime]') AND type IN ('U'))
	DROP TABLE [Basic].[UserPartTime]
GO

CREATE TABLE [Basic].[UserPartTime] (
  [UserId] bigint  NOT NULL,
  [PartTimeDeptId] bigint  NOT NULL,
  [PartTimePositionId] bigint  NOT NULL,
  [StartTime] datetime2(7)  NOT NULL,
  [EndTime] datetime2(7)  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[UserPartTime] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工Id',
'SCHEMA', N'Basic',
'TABLE', N'UserPartTime',
'COLUMN', N'UserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门Id',
'SCHEMA', N'Basic',
'TABLE', N'UserPartTime',
'COLUMN', N'PartTimeDeptId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'兼任职级Id',
'SCHEMA', N'Basic',
'TABLE', N'UserPartTime',
'COLUMN', N'PartTimePositionId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'兼任开始时间',
'SCHEMA', N'Basic',
'TABLE', N'UserPartTime',
'COLUMN', N'StartTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'兼任结束时间',
'SCHEMA', N'Basic',
'TABLE', N'UserPartTime',
'COLUMN', N'EndTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'UserPartTime',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'UserPartTime',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'UserPartTime',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'UserPartTime',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工兼任表',
'SCHEMA', N'Basic',
'TABLE', N'UserPartTime'
GO


-- ----------------------------
-- Records of UserPartTime
-- ----------------------------
INSERT INTO [Basic].[UserPartTime] ([UserId], [PartTimeDeptId], [PartTimePositionId], [StartTime], [EndTime], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050601504599052288', N'1950000000000000005', N'1351584156689104896', N'2026-05-09 14:44:55.0000000', N'2026-05-14 14:44:59.0000000', N'1903486709602062336', N'2026-05-09 14:45:11.000', NULL, NULL)
GO


-- ----------------------------
-- Table structure for UserRole
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[UserRole]') AND type IN ('U'))
	DROP TABLE [Basic].[UserRole]
GO

CREATE TABLE [Basic].[UserRole] (
  [UserId] bigint  NOT NULL,
  [RoleId] bigint  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime2(3)  NOT NULL,
  [ModifiedBy] bigint DEFAULT NULL NULL,
  [ModifiedDate] datetime2(3)  NULL
)
GO

ALTER TABLE [Basic].[UserRole] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户Id',
'SCHEMA', N'Basic',
'TABLE', N'UserRole',
'COLUMN', N'UserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色Id',
'SCHEMA', N'Basic',
'TABLE', N'UserRole',
'COLUMN', N'RoleId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'UserRole',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'UserRole',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'UserRole',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'UserRole',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工角色对照表',
'SCHEMA', N'Basic',
'TABLE', N'UserRole'
GO


-- ----------------------------
-- Records of UserRole
-- ----------------------------
INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1905670034215276544', N'1903486709602062336', N'2025-03-04 16:50:11.000', N'1903486709602062336', N'2026-03-15 00:59:11.183')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062337', N'1979881189825187840', N'1903486709602062336', N'2025-03-04 16:50:11.000', N'1903486709602062336', N'2026-05-02 23:31:09.400')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062338', N'1979881189825187840', N'1903486709602062336', N'2026-04-27 13:28:17.000', N'1903486709602062336', N'2026-05-02 23:52:03.777')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062340', N'1979881189825187840', N'1903486709602062336', N'2026-04-27 13:28:17.000', N'1903486709602062336', N'2026-05-02 23:44:35.937')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062341', N'1979881189825187840', N'1903486709602062336', N'2026-04-27 14:10:17.000', N'1903486709602062336', N'2026-05-02 23:52:26.240')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062342', N'1979881189825187840', N'1903486709602062336', N'2026-04-28 16:03:49.000', N'1903486709602062336', N'2026-05-02 23:52:18.160')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050596970468347904', N'1979881189825187840', N'1903486709602062336', N'2026-05-02 23:23:10.913', NULL, NULL)
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050597318784323584', N'1979881189825187840', N'1903486709602062336', N'2026-05-02 23:24:34.027', N'1903486709602062336', N'2026-05-05 12:53:50.877')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050598466639499264', N'1979881189825187840', N'1903486709602062336', N'2026-05-02 23:29:07.520', N'1903486709602062336', N'2026-05-02 23:45:56.227')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050599298114129920', N'1979881189825187840', N'1903486709602062336', N'2026-05-02 23:32:25.753', N'1903486709602062336', N'2026-05-02 23:35:06.050')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050599625240481792', N'1979881189825187840', N'1903486709602062336', N'2026-05-02 23:33:43.743', NULL, NULL)
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050599950684917760', N'1979881189825187840', N'1903486709602062336', N'2026-05-02 23:35:01.313', N'1903486709602062336', N'2026-05-03 00:23:59.413')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050600303354580992', N'1979881189825187840', N'1903486709602062336', N'2026-05-02 23:36:25.423', NULL, NULL)
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050600734554198016', N'1905670034215276544', N'1903486709602062336', N'2026-05-02 23:38:08.250', N'1903486709602062336', N'2026-05-02 23:46:00.310')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050601504599052288', N'1979881189825187840', N'1903486709602062336', N'2026-05-02 23:41:11.820', N'1903486709602062336', N'2026-05-02 23:46:18.463')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050601857436487680', N'1979881189825187840', N'1903486709602062336', N'2026-05-02 23:42:35.947', N'1903486709602062336', N'2026-05-02 23:46:26.273')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050602218733834240', N'1979881189825187840', N'1903486709602062336', N'2026-05-02 23:44:02.070', N'1903486709602062336', N'2026-05-02 23:51:37.593')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'2050603324033601536', N'1979881189825187840', N'1903486709602062336', N'2026-05-02 23:48:25.580', N'1903486709602062336', N'2026-05-02 23:52:53.937')
GO


-- ----------------------------
-- Primary Key structure for table CurrencyInfo
-- ----------------------------
ALTER TABLE [Basic].[CurrencyInfo] ADD CONSTRAINT [PK__Currency__14470AF0F60CC698] PRIMARY KEY CLUSTERED ([CurrencyId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table DepartmentInfo
-- ----------------------------
ALTER TABLE [Basic].[DepartmentInfo] ADD CONSTRAINT [PK_DepartmentInfo] PRIMARY KEY CLUSTERED ([DepartmentId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table DepartmentLevel
-- ----------------------------
ALTER TABLE [Basic].[DepartmentLevel] ADD CONSTRAINT [PK_DepartmentLevel] PRIMARY KEY CLUSTERED ([DepartmentLevelId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table MenuInfo
-- ----------------------------
ALTER TABLE [Basic].[MenuInfo] ADD CONSTRAINT [PK__MenuInfo__C99ED23073DEC746] PRIMARY KEY CLUSTERED ([MenuId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table ModuleInfo
-- ----------------------------
ALTER TABLE [Basic].[ModuleInfo] ADD CONSTRAINT [PK__DomainIn__2498D75048722D1E] PRIMARY KEY CLUSTERED ([ModuleId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table NationalityInfo
-- ----------------------------
ALTER TABLE [Basic].[NationalityInfo] ADD CONSTRAINT [PK__National__211B9BBE8AA806D4] PRIMARY KEY CLUSTERED ([NationId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table PositionInfo
-- ----------------------------
ALTER TABLE [Basic].[PositionInfo] ADD CONSTRAINT [PK__UserPosi__60BB9A7952FCAD62] PRIMARY KEY CLUSTERED ([PositionId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table RoleInfo
-- ----------------------------
ALTER TABLE [Basic].[RoleInfo] ADD CONSTRAINT [PK__RoleInfo__8AFACE1A81A70B73] PRIMARY KEY CLUSTERED ([RoleId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table UserInfo
-- ----------------------------
ALTER TABLE [Basic].[UserInfo] ADD CONSTRAINT [PK__UserInfo__1788CC4C15412757] PRIMARY KEY CLUSTERED ([UserId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table UserLabor
-- ----------------------------
ALTER TABLE [Basic].[UserLabor] ADD CONSTRAINT [PK__UserLazy__369CEBB6063C3DE7] PRIMARY KEY CLUSTERED ([LaborId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table UserRole
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_UserRole_RoleId_UserId]
ON [Basic].[UserRole] (
  [RoleId] ASC,
  [UserId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table UserRole
-- ----------------------------
ALTER TABLE [Basic].[UserRole] ADD CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([UserId], [RoleId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

