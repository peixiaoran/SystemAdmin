/*
 Navicat Premium Dump SQL

 Source Server         : 127.0.0.1
 Source Server Type    : SQL Server
 Source Server Version : 16001000 (16.00.1000)
 Source Host           : 127.0.0.1:1433
 Source Catalog        : SystemAdmin
 Source Schema         : Basic

 Target Server Type    : SQL Server
 Target Server Version : 16001000 (16.00.1000)
 File Encoding         : 65001

 Date: 02/01/2026 17:00:55
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
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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
  [DepartmentNameCn] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [DepartmentNameEn] nvarchar(40) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [ParentId] bigint  NULL,
  [DepartmentLevelId] bigint  NOT NULL,
  [SortOrder] int DEFAULT 0 NOT NULL,
  [Landline] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [Email] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [Address] nvarchar(200) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [Description] nvarchar(200) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NULL,
  [CreatedDate] datetime DEFAULT getdate() NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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
INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576768', N'0001', N'董事长室', N'Chairman''s Office', N'0', N'1350917348311171072', N'1', N'02-1234-1000', N'Chairman@eson.tw', N'台北市信義區松仁路100號', N'', NULL, NULL, N'1903486709602062336', N'2025-11-03 21:30:41.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576769', N'0002', N'副董事长室', N'Vice Chairman''s Office', N'1929535196076576768', N'1350917348311171072', N'1', N'02-1234-1001', N'vicechairman@company.com', N'台北市信義區松仁路102號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576770', N'0101', N'总经理室', N'General Manager''s Office', N'1929535196076576768', N'1351403528752463872', N'2', N'02-1234-1002', N'gm@company.com', N'台北市信義區松仁路104號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576773', N'0201', N'厂长室', N'Factory Director''s Office', N'1929535196076576770', N'1351405026328707072', N'1', N'02-1234-1003', N'factorydir@company.com', N'新北市新店區寶橋路200號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576774', N'0202', N'副厂长室1', N'Vice Factory Dir Office 1', N'1929535196076576773', N'1351405026328707072', N'2', N'02-1234-1004', N'vicefactory1@company.com', N'新北市新店區寶橋路202號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576775', N'0203', N'副厂长室2', N'Vice Factory Dir Office 2', N'1929535196076576773', N'1351405026328707072', N'3', N'02-1234-1005', N'vicefactory2@company.com', N'新北市新店區寶橋路204號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576776', N'0204', N'厂长助理室1', N'Factory Assistant Office 1', N'1929535196076576773', N'1351405026328707072', N'4', N'02-1234-1006', N'factoryasst1@company.com', N'新北市新店區寶橋路206號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576777', N'0205', N'厂长助理室2', N'Factory Assistant Office 2', N'1929535196076576773', N'1351405026328707072', N'5', N'02-1234-1007', N'factoryasst2@company.com', N'新北市新店區寶橋路208號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576778', N'0301', N'生产中心', N'Production Center', N'1929535196076576773', N'1949166428666073088', N'1', N'03-2345-1000', N'production@company.com', N'桃園市中壢區中華路300號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576779', N'0302', N'品质中心', N'Quality Center', N'1929535196076576773', N'1949166428666073088', N'2', N'03-2345-1001', N'quality@company.com', N'桃園市中壢區中華路302號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576780', N'0303', N'研发中心', N'R&D Center', N'1929535196076576773', N'1949166428666073088', N'3', N'03-2345-1002', N'rd@company.com', N'新竹市東區光復路400號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576781', N'0304', N'供应链中心', N'Supply Chain Center', N'1929535196076576770', N'1949166428666073088', N'4', N'03-2345-1003', N'supplychain@company.com', N'新竹市東區光復路402號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576782', N'0305', N'行销中心', N'Marketing Center', N'1929535196076576770', N'1949166428666073088', N'5', N'04-3456-1000', N'marketing@company.com', N'台中市西屯區台灣大道500號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576783', N'0306', N'人力资源中心', N'HR Center', N'1929535196076576770', N'1949166428666073088', N'6', N'04-3456-1001', N'hr@company.com', N'台中市西屯區台灣大道502號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576784', N'0307', N'财务中心', N'Finance Center', N'1929535196076576770', N'1949166428666073088', N'7', N'06-4567-1000', N'finance@company.com', N'台南市東區中華東路600號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576785', N'0308', N'稽核中心', N'Audit Center', N'1929535196076576768', N'1949166428666073088', N'8', N'06-4567-1001', N'audit@company.com', N'台南市東區中華東路602號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576786', N'0309', N'资讯中心', N'IT Center', N'1929535196076576770', N'1949166428666073088', N'9', N'07-5678-1000', N'it@company.com', N'高雄市前鎮區中山二路700號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576787', N'0401', N'电子事业部', N'Electronics Division', N'1929535196076576778', N'1949166690151567360', N'1', N'07-5678-1001', N'electronics@company.com', N'高雄市前鎮區中山二路702號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576788', N'0402', N'机械事业部', N'Machinery Division', N'1929535196076576778', N'1949166690151567360', N'2', N'02-1234-1008', N'machinery@company.com', N'台北市南港區經貿一路800號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576789', N'0403', N'汽车零件事业部', N'Auto Parts Division', N'1929535196076576778', N'1949166690151567360', N'3', N'02-1234-1009', N'autoparts@company.com', N'台北市南港區經貿一路802號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576790', N'0404', N'品质检验事业部', N'Quality Inspection Division', N'1929535196076576779', N'1949166690151567360', N'4', N'02-1234-1010', N'qualityinsp@company.com', N'新北市板橋區文化路900號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576791', N'0405', N'产品测试事业部', N'Product Testing Division', N'1929535196076576779', N'1949166690151567360', N'5', N'02-1234-1011', N'producttest@company.com', N'新北市板橋區文化路902號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576792', N'0406', N'新产品开发事业部', N'New Product Dev Division', N'1929535196076576780', N'1949166690151567360', N'6', N'03-2345-1004', N'newproduct@company.com', N'桃園市桃園區復興路1000號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576793', N'0407', N'技术研究事业部', N'Tech Research Division', N'1929535196076576780', N'1949166690151567360', N'7', N'03-2345-1005', N'techresearch@company.com', N'桃園市桃園區復興路1002號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576794', N'0408', N'采购事业部', N'Procurement Division', N'1929535196076576781', N'1949166690151567360', N'8', N'03-2345-1006', N'procurement@company.com', N'新竹縣竹北市光明六路1100號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576795', N'0409', N'物流事业部', N'Logistics Division', N'1929535196076576781', N'1949166690151567360', N'9', N'03-2345-1007', N'logistics@company.com', N'新竹縣竹北市光明六路1102號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576796', N'0410', N'市场推广事业部', N'Market Promotion Division', N'1929535196076576782', N'1949166690151567360', N'10', N'04-3456-1002', N'marketpromo@company.com', N'台中市南區復興路1200號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576797', N'0411', N'销售事业部', N'Sales Division', N'1929535196076576782', N'1949166690151567360', N'11', N'04-3456-1003', N'sales@company.com', N'台中市南區復興路1202號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576798', N'0412', N'招聘事业部', N'Recruitment Division', N'1929535196076576783', N'1949166690151567360', N'12', N'06-4567-1002', N'recruitment@company.com', N'台南市北區公園路1300號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576799', N'0413', N'培训事业部', N'Training Division', N'1929535196076576783', N'1949166690151567360', N'13', N'06-4567-1003', N'training@company.com', N'台南市北區公園路1302號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576800', N'0414', N'会计事业部', N'Accounting Division', N'1929535196076576784', N'1949166690151567360', N'14', N'07-5678-1002', N'accounting@company.com', N'高雄市三民區建國路1400號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576801', N'0415', N'财务规划事业部', N'Financial Planning Division', N'1929535196076576784', N'1949166690151567360', N'15', N'07-5678-1003', N'finplanning@company.com', N'高雄市三民區建國路1402號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576802', N'0501', N'生产部门', N'Production Department', N'1929535196076576787', N'1949167957770899456', N'1', N'02-1234-1012', N'proddept@company.com', N'台北市大安區和平東路1500號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576803', N'0502', N'品质控制部门', N'Quality Control Department', N'1929535196076576790', N'1949167957770899456', N'2', N'02-1234-1013', N'qcdept@company.com', N'台北市大安區和平東路1502號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576804', N'0503', N'研发部门', N'R&D Department', N'1929535196076576792', N'1949167957770899456', N'3', N'02-1234-1014', N'rddept@company.com', N'新北市永和區永和路1600號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576805', N'0504', N'采购部门', N'Procurement Department', N'1929535196076576794', N'1949167957770899456', N'4', N'02-1234-1015', N'procdept@company.com', N'新北市永和區永和路1602號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576806', N'0505', N'物流部门', N'Logistics Department', N'1929535196076576795', N'1949167957770899456', N'5', N'03-2345-1008', N'logdept@company.com', N'桃園市八德區介壽路1700號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576807', N'0506', N'市场部门', N'Marketing Department', N'1929535196076576796', N'1949167957770899456', N'6', N'03-2345-1009', N'marketdept@company.com', N'桃園市八德區介壽路1702號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576808', N'0507', N'销售部门', N'Sales Department', N'1929535196076576797', N'1949167957770899456', N'7', N'03-2345-1010', N'salesdept@company.com', N'新竹市北區中正路1800號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576809', N'0508', N'人力资源部门', N'HR Department', N'1929535196076576798', N'1949167957770899456', N'8', N'03-2345-1011', N'hrdept@company.com', N'新竹市北區中正路1802號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576810', N'0509', N'财务部门', N'Finance Department', N'1929535196076576800', N'1949167957770899456', N'9', N'04-3456-1004', N'findept@company.com', N'台中市北屯區崇德路1900號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576811', N'0510', N'稽核部门', N'Audit Department', N'1929535196076576785', N'1949167957770899456', N'10', N'04-3456-1005', N'auditdept@company.com', N'台中市北屯區崇德路1902號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576812', N'0511', N'资讯部门', N'IT Department', N'1929535196076576786', N'1949167957770899456', N'11', N'06-4567-1004', N'itdept@company.com', N'台南市中西區民生路2000號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576813', N'0512', N'安全部门', N'Safety Department', N'1929535196076576773', N'1949167957770899456', N'12', N'06-4567-1005', N'safetydept@company.com', N'台南市中西區民生路2002號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576814', N'0513', N'环保部门', N'Environmental Department', N'1929535196076576773', N'1949167957770899456', N'13', N'07-5678-1004', N'envdept@company.com', N'高雄市左營區自由路2100號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576815', N'0514', N'行政部门', N'Admin Department', N'1929535196076576770', N'1949167957770899456', N'14', N'07-5678-1005', N'admindept@company.com', N'高雄市左營區自由路2102號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576816', N'0515', N'法务部门', N'Legal Department', N'1929535196076576770', N'1949167957770899456', N'15', N'02-1234-1016', N'legaldept@company.com', N'台北市士林區中山北路2200號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576817', N'0516', N'公关部门', N'PR Department', N'1929535196076576782', N'1949167957770899456', N'16', N'02-1234-1017', N'prdept@company.com', N'台北市士林區中山北路2202號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576818', N'0517', N'客户服务部门', N'Customer Service Department', N'1929535196076576797', N'1949167957770899456', N'17', N'02-1234-1018', N'custservdept@company.com', N'新北市新莊區中正路2300號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576819', N'0518', N'产品管理部门', N'Product Management Department', N'1929535196076576780', N'1949167957770899456', N'18', N'02-1234-1019', N'prodmgmtdept@company.com', N'新北市新莊區中正路2302號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576820', N'0519', N'工程部门', N'Engineering Department', N'1929535196076576780', N'1949167957770899456', N'19', N'03-2345-1012', N'engdept@company.com', N'桃園市蘆竹區南崁路2400號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576821', N'0520', N'维护部门', N'Maintenance Department', N'1929535196076576778', N'1949167957770899456', N'20', N'03-2345-1013', N'maintdept@company.com', N'桃園市蘆竹區南崁路2402號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576822', N'0521', N'供应商管理部门', N'Supplier Management Department', N'1929535196076576794', N'1949167957770899456', N'21', N'03-2345-1014', N'suppliermgmt@company.com', N'新竹市香山區牛埔路2500號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576823', N'0522', N'仓储部门', N'Warehouse Department', N'1929535196076576795', N'1949167957770899456', N'22', N'03-2345-1015', N'warehousedept@company.com', N'新竹市香山區牛埔路2502號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576824', N'0523', N'运输部门', N'Transportation Department', N'1929535196076576795', N'1949167957770899456', N'23', N'04-3456-1006', N'transportdept@company.com', N'台中市南屯區五權西路2600號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576825', N'0524', N'员工关系部门', N'Employee Relations Department', N'1929535196076576798', N'1949167957770899456', N'24', N'04-3456-1007', N'emprel@company.com', N'台中市南屯區五權西路2602號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576826', N'0525', N'薪酬部门', N'Compensation Department', N'1929535196076576799', N'1949167957770899456', N'25', N'06-4567-1006', N'compdept@company.com', N'台南市安平區健康路2700號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576827', N'0526', N'税务部门', N'Tax Department', N'1929535196076576800', N'1949167957770899456', N'26', N'06-4567-1007', N'taxdept@company.com', N'台南市安平區健康路2702號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576828', N'0527', N'预算部门', N'Budget Department', N'1929535196076576801', N'1949167957770899456', N'27', N'07-5678-1006', N'budgetdept@company.com', N'高雄市苓雅區四維路2800號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576829', N'0528', N'风险管理部门', N'Risk Management Department', N'1929535196076576785', N'1949167957770899456', N'28', N'07-5678-1007', N'riskmgmt@company.com', N'高雄市苓雅區四維路2802號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576830', N'0529', N'系统开发部门', N'System Development Department', N'1929535196076576812', N'1949167957770899456', N'29', N'02-1234-1020', N'sysdev@company.com', N'台北市文山區木柵路2900號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576831', N'0530', N'网路部门', N'Network Department', N'1929535196076576812', N'1949167957770899456', N'30', N'02-1234-1021', N'networkdept@company.com', N'台北市文山區木柵路2902號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576832', N'0601', N'生产科', N'Production Section', N'1929535196076576802', N'1949168956883472384', N'1', N'02-1234-1022', N'prodsec@company.com', N'新北市土城區中央路3000號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576833', N'0602', N'品质检验科', N'Quality Inspection Section', N'1929535196076576803', N'1949168956883472384', N'2', N'02-1234-1023', N'qcinspsec@company.com', N'新北市土城區中央路3002號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576834', N'0603', N'研发科', N'R&D Section', N'1929535196076576804', N'1949168956883472384', N'3', N'03-2345-1016', N'rdsec@company.com', N'桃園市大溪區員林路3100號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576835', N'0604', N'采购科', N'Procurement Section', N'1929535196076576805', N'1949168956883472384', N'4', N'03-2345-1017', N'procsec@company.com', N'桃園市大溪區員林路3102號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576836', N'0605', N'物流科', N'Logistics Section', N'1929535196076576806', N'1949168956883472384', N'5', N'03-2345-1018', N'logsec@company.com', N'新竹縣新豐鄉新興路3200號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576837', N'0606', N'市场科', N'Marketing Section', N'1929535196076576807', N'1949168956883472384', N'6', N'03-2345-1019', N'marketsec@company.com', N'新竹縣新豐鄉新興路3202號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576838', N'0607', N'销售科', N'Sales Section', N'1929535196076576808', N'1949168956883472384', N'7', N'04-3456-1008', N'salessec@company.com', N'台中市大里區中興路3300號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576839', N'0608', N'人力资源科', N'HR Section', N'1929535196076576809', N'1949168956883472384', N'8', N'04-3456-1009', N'hrsec@company.com', N'台中市大里區中興路3302號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576840', N'0609', N'财务科', N'Finance Section', N'1929535196076576810', N'1949168956883472384', N'9', N'06-4567-1008', N'finsec@company.com', N'台南市永康區中華路3400號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576841', N'0610', N'稽核科', N'Audit Section', N'1929535196076576811', N'1949168956883472384', N'10', N'06-4567-1009', N'auditsec@company.com', N'台南市永康區中華路3402號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576842', N'0611', N'资讯科', N'IT Section', N'1929535196076576812', N'1949168956883472384', N'11', N'07-5678-1008', N'itsec@company.com', N'高雄市楠梓區德民路3500號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576843', N'0612', N'安全科', N'Safety Section', N'1929535196076576813', N'1949168956883472384', N'12', N'07-5678-1009', N'safetysec@company.com', N'高雄市楠梓區德民路3502號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576844', N'0613', N'环保科', N'Environmental Section', N'1929535196076576814', N'1949168956883472384', N'13', N'02-1234-1024', N'envsec@company.com', N'台北市北投區中央北路3600號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576845', N'0614', N'行政科', N'Admin Section', N'1929535196076576815', N'1949168956883472384', N'14', N'02-1234-1025', N'adminsec@company.com', N'台北市北投區中央北路3602號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576846', N'0615', N'法务科', N'Legal Section', N'1929535196076576816', N'1949168956883472384', N'15', N'02-1234-1026', N'legalsec@company.com', N'新北市三重區重新路3700號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576847', N'0701', N'生产执行组', N'Production Execution Team', N'1929535196076576832', N'1949169142347206656', N'1', N'02-1234-1027', N'prodexec@company.com', N'新北市三重區重新路3702號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576848', N'0702', N'品质检查组', N'Quality Check Team', N'1929535196076576833', N'1949169142347206656', N'2', N'03-2345-1020', N'qccheck@company.com', N'桃園市平鎮區民族路3800號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576849', N'0703', N'研发组', N'R&D Team', N'1929535196076576834', N'1949169142347206656', N'3', N'03-2345-1021', N'rdteam@company.com', N'桃園市平鎮區民族路3802號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576850', N'0704', N'采购组', N'Procurement Team', N'1929535196076576835', N'1949169142347206656', N'4', N'03-2345-1022', N'procteam@company.com', N'新竹市東區建功路3900號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576851', N'0705', N'物流组', N'Logistics Team', N'1929535196076576836', N'1949169142347206656', N'5', N'03-2345-1023', N'logteam@company.com', N'新竹市東區建功路3902號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576852', N'0706', N'市场组', N'Marketing Team', N'1929535196076576837', N'1949169142347206656', N'6', N'04-3456-1010', N'marketteam@company.com', N'台中市西區美村路4000號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576853', N'0707', N'销售组', N'Sales Team', N'1929535196076576838', N'1949169142347206656', N'7', N'04-3456-1011', N'salesteam@company.com', N'台中市西區美村路4002號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576854', N'0708', N'人力资源组', N'HR Team', N'1929535196076576839', N'1949169142347206656', N'8', N'06-4567-1010', N'hrteam@company.com', N'台南市新化區中正路4100號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576855', N'0709', N'财务组', N'Finance Team', N'1929535196076576840', N'1949169142347206656', N'9', N'06-4567-1011', N'finteam@company.com', N'台南市新化區中正路4102號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576856', N'0710', N'稽核组', N'Audit Team', N'1929535196076576841', N'1949169142347206656', N'10', N'07-5678-1010', N'auditteams@company.com', N'高雄市鳳山區青年路4200號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576857', N'0711', N'资讯组', N'IT Team', N'1929535196076576842', N'1949169142347206656', N'11', N'07-5678-1011', N'itteam@company.com', N'高雄市鳳山區青年路4202號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576858', N'0712', N'安全组', N'Safety Team', N'1929535196076576843', N'1949169142347206656', N'12', N'02-1234-1028', N'safetyteam@company.com', N'台北市松山區南京東路4300號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576859', N'0713', N'环保组', N'Environmental Team', N'1929535196076576844', N'1949169142347206656', N'13', N'02-1234-1029', N'envteam@company.com', N'台北市松山區南京東路4302號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576860', N'0714', N'行政组', N'Admin Team', N'1929535196076576845', N'1949169142347206656', N'14', N'02-1234-1030', N'adminteam@company.com', N'新北市汐止區大同路4400號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576861', N'0715', N'法务组', N'Legal Team', N'1929535196076576846', N'1949169142347206656', N'15', N'02-1234-1031', N'legalteam@company.com', N'新北市汐止區大同路4402號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576862', N'0716', N'公关组', N'PR Team', N'1929535196076576817', N'1949169142347206656', N'16', N'03-2345-1024', N'prteam@company.com', N'桃園市蘆竹區南崁路4500號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576863', N'0717', N'客户服务组', N'Customer Service Team', N'1929535196076576818', N'1949169142347206656', N'17', N'03-2345-1025', N'custservteam@company.com', N'桃園市蘆竹區南崁路4502號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576864', N'0718', N'产品管理组', N'Product Management Team', N'1929535196076576819', N'1949169142347206656', N'18', N'03-2345-1026', N'prodmgtteam@company.com', N'新竹市北區中正路4600號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576865', N'0719', N'工程组', N'Engineering Team', N'1929535196076576820', N'1949169142347206656', N'19', N'03-2345-1027', N'engteam@company.com', N'新竹市北區中正路4602號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576866', N'0720', N'维护组', N'Maintenance Team', N'1929535196076576821', N'1949169142347206656', N'20', N'04-3456-1012', N'maintteam@company.com', N'台中市南區復興路4700號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576867', N'0721', N'供应商管理组', N'Supplier Management Team', N'1929535196076576822', N'1949169142347206656', N'21', N'04-3456-1013', N'suppliermgmtteam@company.com', N'台中市南區復興路4702號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576868', N'0722', N'仓储组', N'Warehouse Team', N'1929535196076576823', N'1949169142347206656', N'22', N'06-4567-1012', N'warehouseteam@company.com', N'台南市北區公園路4800號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576869', N'0723', N'运输组', N'Transportation Team', N'1929535196076576824', N'1949169142347206656', N'23', N'06-4567-1013', N'transportteam@company.com', N'台南市北區公園路4802號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576870', N'0724', N'员工关系组', N'Employee Relations Team', N'1929535196076576825', N'1949169142347206656', N'24', N'07-5678-1012', N'emprelteam@company.com', N'高雄市三民區建國路4900號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576871', N'0725', N'薪酬组', N'Compensation Team', N'1929535196076576826', N'1949169142347206656', N'25', N'07-5678-1013', N'compteam@company.com', N'高雄市三民區建國路4902號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576872', N'0726', N'税务组', N'Tax Team', N'1929535196076576827', N'1949169142347206656', N'26', N'02-1234-1032', N'taxteam@company.com', N'台北市大安區和平東路5000號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576873', N'0727', N'预算组', N'Budget Team', N'1929535196076576828', N'1949169142347206656', N'27', N'02-1234-1033', N'budgetteam@company.com', N'台北市大安區和平東路5002號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576874', N'0728', N'风险管理组', N'Risk Management Team', N'1929535196076576829', N'1949169142347206656', N'28', N'02-1234-1034', N'riskteam@company.com', N'新北市永和區永和路5100號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576875', N'0729', N'系统开发组', N'System Development Team', N'1929535196076576830', N'1949169142347206656', N'29', N'02-1234-1035', N'sysdevteam@company.com', N'新北市永和區永和路5102號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576876', N'0730', N'网路组', N'Network Team', N'1929535196076576831', N'1949169142347206656', N'30', N'03-2345-1028', N'networkteam@company.com', N'桃園市八德區介壽路5200號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576877', N'0731', N'生产线1', N'Production Line 1', N'1929535196076576832', N'1949169142347206656', N'2', N'03-2345-1029', N'line1@company.com', N'桃園市八德區介壽路5202號', N'', NULL, NULL, N'1903486709602062336', N'2025-10-01 19:58:48.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576878', N'0732', N'生产线2', N'Production Line 2', N'1929535196076576832', N'1949169142347206656', N'3', N'03-2345-1030', N'line2@company.com', N'新竹市北區中正路5300號', N'', NULL, NULL, N'1903486709602062336', N'2025-10-01 19:58:54.000')
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576879', N'0733', N'品质测试组', N'Quality Testing Team', N'1929535196076576833', N'1949169142347206656', N'33', N'03-2345-1031', N'qctest@company.com', N'新竹市北區中正路5302號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576880', N'0734', N'创新研发组', N'Innovation R&D Team', N'1929535196076576834', N'1949169142347206656', N'34', N'04-3456-1014', N'innovrd@company.com', N'台中市北屯區崇德路5400號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576881', N'0735', N'供应商评估组', N'Supplier Evaluation Team', N'1929535196076576835', N'1949169142347206656', N'35', N'04-3456-1015', N'suppeval@company.com', N'台中市北屯區崇德路5402號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576882', N'0736', N'库存管理组', N'Inventory Management Team', N'1929535196076576836', N'1949169142347206656', N'36', N'06-4567-1014', N'inventory@company.com', N'台南市中西區民生路5500號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576883', N'0737', N'市场分析组', N'Market Analysis Team', N'1929535196076576837', N'1949169142347206656', N'37', N'06-4567-1015', N'marketanalysis@company.com', N'台南市中西區民生路5502號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576884', N'0738', N'销售支援组', N'Sales Support Team', N'1929535196076576838', N'1949169142347206656', N'38', N'07-5678-1014', N'salessupport@company.com', N'高雄市左營區自由路5600號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576885', N'0739', N'培训组', N'Training Team', N'1929535196076576839', N'1949169142347206656', N'39', N'07-5678-1015', N'trainingteam@company.com', N'高雄市左營區自由路5602號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576886', N'0740', N'会计组', N'Accounting Team', N'1929535196076576840', N'1949169142347206656', N'40', N'02-1234-1036', N'accountingteam@company.com', N'台北市士林區中山北路5700號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576887', N'0741', N'内部审计组', N'Internal Audit Team', N'1929535196076576841', N'1949169142347206656', N'41', N'02-1234-1037', N'internalaudit@company.com', N'台北市士林區中山北路5702號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576888', N'0742', N'资料分析组', N'Data Analysis Team', N'1929535196076576842', N'1949169142347206656', N'42', N'02-1234-1038', N'dataanalysis@company.com', N'新北市新莊區中正路5800號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576889', N'0743', N'安全巡检组', N'Safety Inspection Team', N'1929535196076576843', N'1949169142347206656', N'43', N'02-1234-1039', N'safetyinsp@company.com', N'新北市新莊區中正路5802號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576890', N'0744', N'环保监测组', N'Environmental Monitoring Team', N'1929535196076576844', N'1949169142347206656', N'44', N'03-2345-1032', N'envmonitor@company.com', N'桃園市蘆竹區南崁路5900號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576891', N'0745', N'行政支援组', N'Admin Support Team', N'1929535196076576845', N'1949169142347206656', N'45', N'03-2345-1033', N'adminsupport@company.com', N'桃園市蘆竹區南崁路5902號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576892', N'0746', N'合约审查组', N'Contract Review Team', N'1929535196076576846', N'1949169142347206656', N'46', N'03-2345-1034', N'contractreview@company.com', N'新竹市香山區牛埔路6000號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576893', N'0747', N'品牌推广组', N'Brand Promotion Team', N'1929535196076576817', N'1949169142347206656', N'47', N'03-2345-1035', N'brandpromo@company.com', N'新竹市香山區牛埔路6002號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576894', N'0748', N'客户关系组', N'Client Relations Team', N'1929535196076576818', N'1949169142347206656', N'48', N'04-3456-1016', N'clientrel@company.com', N'台中市南屯區五權西路6100號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576895', N'0749', N'产品设计组', N'Product Design Team', N'1929535196076576819', N'1949169142347206656', N'49', N'04-3456-1017', N'proddesign@company.com', N'台中市南屯區五權西路6102號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576896', N'0750', N'工艺优化组', N'Process Optimization Team', N'1929535196076576820', N'1949169142347206656', N'50', N'06-4567-1016', N'processopt@company.com', N'台南市安平區健康路6200號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576897', N'0751', N'设备维护组', N'Equipment Maintenance Team', N'1929535196076576821', N'1949169142347206656', N'51', N'06-4567-1017', N'equipmaint@company.com', N'台南市安平區健康路6202號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576898', N'0752', N'供应链协调组', N'Supply Chain Coordination Team', N'1929535196076576822', N'1949169142347206656', N'52', N'07-5678-1016', N'supplycoord@company.com', N'高雄市苓雅區四維路6300號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576899', N'0753', N'库存盘点组', N'Inventory Audit Team', N'1929535196076576823', N'1949169142347206656', N'53', N'07-5678-1017', N'inventoryaudit@company.com', N'高雄市苓雅區四維路6302號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576900', N'0754', N'配送组', N'Distribution Team', N'1929535196076576824', N'1949169142347206656', N'54', N'', N'distribution@company.com', N'台北市文山區木柵路6400號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576901', N'0755', N'员工发展组', N'Employee Development Team', N'1929535196076576825', N'1949169142347206656', N'55', N'', N'empdev@company.com', N'台北市文山區木柵路6402號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576902', N'0756', N'福利组', N'Benefits Team', N'1929535196076576826', N'1949169142347206656', N'56', N'', N'benefitsteam@company.com', N'新北市土城區中央路6500號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576903', N'0757', N'财务分析组', N'Financial Analysis Team', N'1929535196076576827', N'1949169142347206656', N'57', N'', N'finanalysis@company.com', N'新北市土城區中央路6502號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576904', N'0758', N'预算控制组', N'Budget Control Team', N'1929535196076576828', N'1949169142347206656', N'58', N'', N'budgetcontrol@company.com', N'桃園市大溪區員林路6600號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576905', N'0759', N'合规组', N'Compliance Team', N'1929535196076576829', N'1949169142347206656', N'59', N'', N'compliance@company.com', N'桃園市大溪區員林路6602號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576906', N'0760', N'软体开发组', N'Software Development Team', N'1929535196076576830', N'1949169142347206656', N'60', N'', N'softdev@company.com', N'新竹縣新豐鄉新興路6700號', NULL, NULL, NULL, NULL, NULL)
GO

INSERT INTO [Basic].[DepartmentInfo] ([DepartmentId], [DepartmentCode], [DepartmentNameCn], [DepartmentNameEn], [ParentId], [DepartmentLevelId], [SortOrder], [Landline], [Email], [Address], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929535196076576907', N'0761', N'硬体维护组', N'Hardware Maintenance Team', N'1929535196076576831', N'1949169142347206656', N'61', N'', N'hardmaint@company.com', N'新竹縣新豐鄉新興路6702號', NULL, NULL, NULL, NULL, NULL)
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
  [CreatedDate] datetime DEFAULT getdate() NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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
INSERT INTO [Basic].[DepartmentLevel] ([DepartmentLevelId], [DepartmentLevelCode], [DepartmentLevelNameCn], [DepartmentLevelNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1350917348311171072', N'Board', N'董事会', N'Board of Directors', N'1', N'', N'0', N'2025-01-22 16:28:54.000', N'1903486709602062336', N'2025-08-09 17:53:03.000')
GO

INSERT INTO [Basic].[DepartmentLevel] ([DepartmentLevelId], [DepartmentLevelCode], [DepartmentLevelNameCn], [DepartmentLevelNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351403528752463872', N'Gm', N'总经理', N'General Manager', N'2', N'', N'0', N'2025-01-23 08:34:52.000', N'1903486709602062336', N'2025-08-09 17:53:07.000')
GO

INSERT INTO [Basic].[DepartmentLevel] ([DepartmentLevelId], [DepartmentLevelCode], [DepartmentLevelNameCn], [DepartmentLevelNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351405026328707072', N'Plant', N'厂长室', N'Plant Director Office', N'3', N'', N'0', N'2025-01-23 08:37:50.000', N'1903486709602062336', N'2025-08-09 17:53:11.000')
GO

INSERT INTO [Basic].[DepartmentLevel] ([DepartmentLevelId], [DepartmentLevelCode], [DepartmentLevelNameCn], [DepartmentLevelNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1949166428666073088', N'Center', N'中心级', N'Corporate Center', N'4', N'', N'1903486709602062336', N'2025-07-27 01:54:25.000', N'1903486709602062336', N'2025-08-09 17:53:14.000')
GO

INSERT INTO [Basic].[DepartmentLevel] ([DepartmentLevelId], [DepartmentLevelCode], [DepartmentLevelNameCn], [DepartmentLevelNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1949166690151567360', N'Bu', N'事业级', N'Business Unit', N'5', N'', N'1903486709602062336', N'2025-07-27 01:55:27.000', N'1903486709602062336', N'2025-08-09 17:53:18.000')
GO

INSERT INTO [Basic].[DepartmentLevel] ([DepartmentLevelId], [DepartmentLevelCode], [DepartmentLevelNameCn], [DepartmentLevelNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1949167957770899456', N'Dept', N'部门级', N'Department', N'6', N'', N'1903486709602062336', N'2025-07-27 02:00:29.000', N'1903486709602062336', N'2025-09-13 15:17:28.000')
GO

INSERT INTO [Basic].[DepartmentLevel] ([DepartmentLevelId], [DepartmentLevelCode], [DepartmentLevelNameCn], [DepartmentLevelNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1949168956883472384', N'Sec', N'科室级', N'Section', N'7', N'', N'1903486709602062336', N'2025-07-27 02:04:27.000', N'1903486709602062336', N'2025-09-13 15:17:31.000')
GO

INSERT INTO [Basic].[DepartmentLevel] ([DepartmentLevelId], [DepartmentLevelCode], [DepartmentLevelNameCn], [DepartmentLevelNameEn], [SortOrder], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1949169142347206656', N'Ops', N'执行级', N'Operational Level', N'8', N'', N'1903486709602062336', N'2025-07-27 02:05:12.000', N'1903486709602062336', N'2025-09-13 15:17:34.000')
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
  [CreatedBy] bigint  NULL,
  [CreatedDate] datetime DEFAULT getdate() NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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
INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1942923565422743552', N'1350161679034934501', N'MenuType', N'PrimaryMenu', N'一级菜单', N'Primary Menu', N'1', N'1903486709602062336', N'2025-11-06 14:04:35.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1942923650634223616', N'1350161679034934501', N'MenuType', N'SecondaryMenu', N'二级菜单', N'Secondary menu', N'2', N'1903486709602062336', N'2025-11-06 14:04:35.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1951693847235006464', N'1350161679034934501', N'LoginBehavior', N'LoginSuccessful', N'登入成功', N'Login successful', N'1', N'1903486709602062336', N'2025-11-06 14:04:35.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1951694944875974656', N'1350161679034934501', N'LoginBehavior', N'IncorrectPassword', N'密码错误', N'Incorrect password', N'2', N'1903486709602062336', N'2025-11-06 14:04:35.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1951695070155640832', N'1350161679034934501', N'LoginBehavior', N'AccountNotExist', N'账号不存在', N'Account does not exist', N'3', N'1903486709602062336', N'2025-11-06 14:04:35.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1954124186578456576', N'1350161679034934501', N'LoginBehavior', N'LoggedOut', N'登出', N'Logged out', N'4', N'1903486709602062336', N'2025-11-06 14:04:35.000', N'1903486709602062336', N'2025-12-27 15:25:01.000')
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990313419198304256', N'1968271760889614336', N'FormStatus', N'PendingSubmission', N'待送审', N'Pending Submission', N'1', N'1903486709602062336', N'2025-11-17 14:57:52.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1987844250263490560', N'1968271760889614336', N'LeaveType', N'AnnualLeave', N'年休假', N'Annual Leave', N'1', N'1903486709602062336', N'2025-11-10 19:26:16.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1987844559857651712', N'1968271760889614336', N'LeaveType', N'SickLeave', N'病假', N'Sick Leave', N'2', N'1903486709602062336', N'2025-11-10 19:27:30.000', N'1903486709602062336', N'2025-11-10 19:28:06.000')
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1987844688471789568', N'1968271760889614336', N'LeaveType', N'PersonalLeave', N'事假', N'Personal Leave', N'3', N'1903486709602062336', N'2025-11-10 19:28:00.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1987845000968409088', N'1968271760889614336', N'ImportanceType', N'Normal', N'一般', N'Normal', N'1', N'1903486709602062336', N'2025-11-10 19:29:15.000', N'1903486709602062336', N'2025-11-15 15:48:20.000')
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1987845208037003264', N'1968271760889614336', N'ImportanceType', N'Important', N'重要', N'Important', N'2', N'1903486709602062336', N'2025-11-10 19:30:04.000', N'1903486709602062336', N'2025-11-15 16:13:13.000')
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1989622758610440192', N'1968271760889614336', N'LeaveType', N'4', N'婚假', N'Marriage Leave', N'4', N'1903486709602062336', N'2025-11-15 17:13:25.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1989622835332648960', N'1968271760889614336', N'LeaveType', N'5', N'产假', N'Maternity Leave', N'5', N'1903486709602062336', N'2025-11-15 17:13:44.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1989622953901428736', N'1968271760889614336', N'LeaveType', N'6', N'陪产假 / 护理假', N'Paternity Leave', N'6', N'1903486709602062336', N'2025-11-15 17:14:12.000', N'1903486709602062336', N'2025-11-15 17:14:23.000')
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1989623247368491008', N'1968271760889614336', N'LeaveType', N'7', N'哺乳假', N'Nursing Leave', N'7', N'1903486709602062336', N'2025-11-15 17:15:22.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990313509094821888', N'1968271760889614336', N'FormStatus', N'UnderReview', N'审批中', N'Under Review', N'2', N'1903486709602062336', N'2025-11-17 14:58:13.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990313656482664448', N'1968271760889614336', N'FormStatus', N'Rejected', N'已驳回', N'Rejected', N'3', N'1903486709602062336', N'2025-11-17 14:58:48.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990313846581104640', N'1968271760889614336', N'FormStatus', N'Approved', N'已批准', N'Approved', N'4', N'1903486709602062336', N'2025-11-17 14:59:33.000', NULL, NULL)
GO

INSERT INTO [Basic].[DictionaryInfo] ([DicId], [ModuleId], [DicType], [DicCode], [DicNameCn], [DicNameEn], [SortOrder], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1990314002714071040', N'1968271760889614336', N'FormStatus', N'Cancelled', N'作废', N'Cancelled', N'5', N'1903486709602062336', N'2025-11-17 15:00:11.000', NULL, NULL)
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
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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
  [ParentMenuId] bigint  NULL,
  [ModuleId] bigint  NULL,
  [MenuNameCn] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [MenuNameEn] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [MenuCode] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [MenuType] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [Path] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [MenuIcon] nvarchar(30) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [SortOrder] int  NOT NULL,
  [IsEnabled] int  NOT NULL,
  [IsVisible] int  NOT NULL,
  [RoutePath] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [Redirect] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [Remark] nvarchar(500) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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
'MS_Description', N'菜单唯一编码',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'MenuCode'
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
'MS_Description', N'排序字段',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'SortOrder'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否启用：1=启用，0=不启用',
'SCHEMA', N'Basic',
'TABLE', N'MenuInfo',
'COLUMN', N'IsEnabled'
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
INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1350161962451534507', N'0', N'1350161679034934501', N'系统管理模块', N'System Mgmt', N'SystemMgmt', N'PrimaryMenu', N'systembasicmgmt/system-mgmt', N'HomeFilled', N'2', N'1', N'1', N'', N'/systembasicmgmt/system-mgmt', N'', N'1903486709602062336', N'2025-01-20 16:57:54.000', N'1903486709602062336', N'2025-08-09 22:36:26.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1350169511264780288', N'1350161962451534507', N'1350161679034934501', N'模块信息维护', N'Module Info', N'ModuleData', N'SecondaryMenu', N'systembasicmgmt/system-mgmt/module', N'Menu', N'1', N'1', N'1', N'/api/SystemBasicMgmt/SystemMgmt/ModuleInfo', N'', N'', N'1903486709602062336', N'2025-01-21 00:00:00.000', N'1903486709602062336', N'2025-09-12 22:09:40.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903507885518884864', N'1350161962451534507', N'1350161679034934501', N'一级菜单维护', N'PMenu Info', N'PMenuData', N'SecondaryMenu', N'systembasicmgmt/system-mgmt/pmenu', N'Operation', N'2', N'1', N'1', N'/api/SystemBasicMgmt/SystemMgmt/PMenuInfo', N'', N'', N'1903486709602062336', N'2025-03-23 02:10:43.000', N'1903486709602062336', N'2025-09-12 22:09:46.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903507885518884865', N'1917998505360756736', N'1350161679034934501', N'个人信息维护', N'Personal Profile', N'PersonalInfo', N'SecondaryMenu', N'systembasicmgmt/system-basicdata/personal', N'Postcard', N'8', N'1', N'1', N'/api/SystemBasicMgmt/SystemBasicData/PersonalInfo', N'', N'', N'1903486709602062336', N'2025-03-23 02:10:43.000', N'1903486709602062336', N'2025-11-09 01:30:35.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1910300381175484416', N'1350161962451534507', N'1350161679034934501', N'二级菜单维护', N'SMenu Info', N'SMenuData', N'SecondaryMenu', N'systembasicmgmt/system-mgmt/smenu', N'Share', N'3', N'1', N'1', N'/api/SystemBasicMgmt/SystemMgmt/SMenuInfo', N'', N'', N'1903486709602062336', N'2025-04-10 19:54:37.000', N'1903486709602062336', N'2025-09-12 22:10:00.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1911747107358904320', N'1350161962451534507', N'1350161679034934501', N'角色信息维护', N'Role Info', N'RoleData', N'SecondaryMenu', N'systembasicmgmt/system-mgmt/role', N'Avatar', N'4', N'1', N'1', N'/api/SystemBasicMgmt/SystemMgmt/RoleInfo', N'', N'', N'1903486709602062336', N'2025-04-14 19:43:23.000', N'1903486709602062336', N'2025-09-18 23:30:03.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1917998505360756736', N'0', N'1350161679034934501', N'基本信息模块', N'Basic Data', N'BasicData', N'PrimaryMenu', N'systembasicmgmt/system-basicdata', N'List', N'1', N'1', N'1', N'', N'/systembasicmgmt/system-basicdata', N'', N'1903486709602062336', N'2025-05-02 01:44:13.000', N'1903486709602062336', N'2025-11-03 21:42:10.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1922597528205922304', N'1917998505360756736', N'1350161679034934501', N'员工信息维护', N'Employee Info', N'EmployeeInfo', N'SecondaryMenu', N'systembasicmgmt/system-basicdata/userinfo', N'UserFilled', N'7', N'1', N'1', N'/api/SystemBasicMgmt/SystemBasicData/UserInfo', N'', N'', N'1903486709602062336', N'2025-05-14 18:19:05.000', N'1903486709602062336', N'2025-10-03 16:16:58.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929532135392284672', N'1917998505360756736', N'1350161679034934501', N'部门信息维护', N'Department Info', N'DepartmentInfo', N'SecondaryMenu', N'systembasicmgmt/system-basicdata/departmentinfo', N'School', N'3', N'1', N'1', N'/api/SystemBasicMgmt/SystemBasicData/DepartmentInfo', N'', N'', N'1903486709602062336', N'2025-06-02 21:34:44.000', N'1903486709602062336', N'2025-10-19 02:01:45.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1929897094655643648', N'1917998505360756736', N'1350161679034934501', N'部门层级维护', N'Department Level', N'DepartmentLevel', N'SecondaryMenu', N'systembasicmgmt/system-basicdata/departmentlevel', N'CollectionTag', N'2', N'1', N'1', N'/api/SystemBasicMgmt/SystemBasicData/DepartmentLevel', N'', N'', N'1903486709602062336', N'2025-06-03 21:44:57.000', N'1903486709602062336', N'2025-10-19 02:01:48.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1930269640165036032', N'1917998505360756736', N'1350161679034934501', N'职级信息维护', N'User Positions', N'UserPositions', N'SecondaryMenu', N'systembasicmgmt/system-basicdata/userposition', N'GoldMedal', N'5', N'1', N'1', N'/api/SystemBasicMgmt/SystemBasicData/UserPosition', N'', N'', N'1903486709602062336', N'2025-06-04 22:25:19.000', N'1903486709602062336', N'2025-08-09 10:52:23.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1932766707219304448', N'0', N'1350161679034934501', N'系统设定模块', N'System Settings', N'SystemConfig', N'PrimaryMenu', N'systembasicmgmt/system-config', N'Setting', N'4', N'1', N'1', N'', N'/systembasicmgmt/system-config', N'', N'1903486709602062336', N'2025-06-11 19:47:46.000', N'1903486709602062336', N'2025-10-03 09:13:21.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1933581101280923648', N'1932766707219304448', N'1350161679034934501', N'字典信息维护', N'Dictionary Info', N'DictionaryData', N'SecondaryMenu', N'systembasicmgmt/system-config/dictionaryinfo', N'Reading', N'1', N'1', N'1', N'/api/SystemBasicMgmt/SystemConfig/DictionaryInfo', N'', N'', N'1903486709602062336', N'2025-06-14 01:43:53.000', N'1903486709602062336', N'2025-09-12 22:11:52.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1933581101280923650', N'1917998505360756736', N'1350161679034934501', N'员工职业维护', N'User Labor', N'UserLabor', N'SecondaryMenu', N'systembasicmgmt/system-basicdata/userlabor', N'Postcard', N'5', N'1', N'1', N'/api/SystemBasicMgmt/SystemBasicData/UserLabor', N'', N'', N'1903486709602062336', N'2025-06-14 01:43:53.000', N'1903486709602062336', N'2025-09-11 18:21:58.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1938565603321319424', N'1967176341195460608', N'1350161679034934501', N'员工代理维护', N'User Agent', N'UserAgent', N'SecondaryMenu', N'systembasicmgmt/user-settings/useragent', N'Handbag', N'1', N'1', N'1', N'/api/SystemBasicMgmt/UserSettings/UserAgent', N'', N'', N'1903486709602062336', N'2025-06-27 19:50:31.000', N'1903486709602062336', N'2025-09-14 18:44:54.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1938565603321319425', N'1967176341195460608', N'1350161679034934501', N'员工兼任维护', N'User PartTime', N'UserPartTime', N'SecondaryMenu', N'systembasicmgmt/user-settings/userparttime', N'ShoppingBag', N'2', N'1', N'1', N'/api/SystemBasicMgmt/UserSettings/UserPartTime', N'', N'', N'1903486709602062336', N'2025-06-27 19:50:31.000', N'1903486709602062336', N'2025-09-14 18:45:04.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1938565603321319426', N'1967176341195460608', N'1350161679034934501', N'员工表单绑定', N'User Form Bind', N'UserFormBind', N'SecondaryMenu', N'systembasicmgmt/user-settings/userformbind', N'Management', N'3', N'1', N'1', N'/api/SystemBasicMgmt/UserSettings/UserFormBind', N'', N'', N'1903486709602062336', N'2025-06-27 19:50:31.000', N'1903486709602062336', N'2025-10-03 08:34:48.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1942199098723667968', N'1932766707219304448', N'1350161679034934501', N'币别信息维护', N'Currency Info', N'CurrencyInfo', N'SecondaryMenu', N'systembasicmgmt/system-config/currencyinfo', N'Money', N'2', N'1', N'1', N'/api/systemBasicMgmt/SystemConfig/CurrencyInfo', N'', N'', N'1903486709602062336', N'2025-07-07 20:28:44.000', N'1903486709602062336', N'2025-08-09 22:39:26.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1944738755751579648', N'1932766707219304448', N'1350161679034934501', N'汇率信息维护', N'Exchange Rate', N'ExchangeRate', N'SecondaryMenu', N'systembasicmgmt/system-config/exchangerate', N'Switch', N'3', N'1', N'1', N'/api/SystemBasicMgmt/SystemConfig/ExchangeRate', N'', N'', N'1903486709602062336', N'2025-07-14 20:40:25.000', N'1903486709602062336', N'2025-10-01 21:44:42.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1951689330179313664', N'1932766707219304448', N'1350161679034934501', N'员工操作日志', N'LogOut Info', N'LogOutInfo', N'SecondaryMenu', N'systembasicmgmt/system-config/userLoginLog', N'Tickets', N'4', N'1', N'1', N'/api/SystemBasicMgmt/SystemConfig/UserLoginLog', N'', N'', N'1903486709602062336', N'2025-08-03 00:59:31.000', N'1903486709602062336', N'2025-08-09 22:39:50.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1967176341195460608', N'0', N'1350161679034934501', N'员工相关配置', N'System UserFig', N'Userconfig', N'PrimaryMenu', N'systembasicmgmt/user-settings', N'SetUp', N'3', N'1', N'1', N'', N'/systembasicmgmt/user-settings', N'', N'1903486709602062336', N'2025-09-14 18:39:22.000', N'1903486709602062336', N'2025-10-03 09:13:24.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1968272763634454528', N'0', N'1968271760889614336', N'表单基础信息', N'Form BasicInfo', N'FormBasicInfo', N'PrimaryMenu', N'formbusiness/form-basicInfo', N'Tickets', N'1', N'1', N'1', N'', N'', N'', N'1903486709602062336', N'2025-09-17 19:16:10.000', N'1903486709602062336', N'2025-09-17 19:22:18.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1968275489407766528', N'1968272763634454528', N'1968271760889614336', N'表单组别信息', N'Form Group', N'FormGroup', N'SecondaryMenu', N'formbusiness/form-basicInfo/formgroup', N'Collection', N'2', N'1', N'1', N'/api/FormBusiness/FormBasicInfo/FormGroup', N'', N'', N'1903486709602062336', N'2025-09-17 19:27:00.000', N'1903486709602062336', N'2025-09-17 23:33:27.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1968275489407766529', N'1968272763634454528', N'1968271760889614336', N'表单类别信息', N'Form Type', N'FormType', N'SecondaryMenu', N'formbusiness/form-basicInfo/formtype', N'Postcard', N'3', N'1', N'1', N'/api/FormBusiness/FormBasicInfo/FormType', N'', N'', N'1903486709602062336', N'2025-09-17 19:27:00.000', N'1903486709602062336', N'2025-10-19 03:00:49.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1968275489407766530', N'1968272763634454528', N'1968271760889614336', N'控件信息维护', N'Control Info', N'ControlInfo', N'SecondaryMenu', N'formbusiness/form-basicInfo/controlinfo', N'Link', N'1', N'1', N'1', N'/api/FormBusiness/FormBasicInfo/ControlInfo', N'', N'', N'1903486709602062336', N'2025-09-17 19:27:00.000', N'1903486709602062336', N'2025-10-19 03:00:49.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1973378015064887296', N'1917998505360756736', N'1350161679034934501', N'国籍信息维护', N'Nationality Info', N'NationalityInfo', N'SecondaryMenu', N'systembasicmgmt/system-basicdata/nationalityinfo', N'DeleteLocation', N'5', N'1', N'1', N'/api/SystemBasicMgmt/SystemBasicData/NationalityInfo', N'', N'', N'1903486709602062336', N'2025-10-01 21:22:37.000', N'1903486709602062336', N'2025-11-09 00:47:59.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1982707658716745729', N'0', N'1982707658716745728', N'相关基础信息', N'CustMat Basic', N'CustMatBasicInfo', N'PrimaryMenu', N'custmat/custmat-basicinfo', N'List', N'1', N'1', N'1', N'', N'/custmat/custmat-basicinfo', N'', N'1903486709602062336', N'2025-10-27 15:23:10.000', N'1903486709602062336', N'2025-11-03 20:44:29.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1982707658716745730', N'1982707658716745729', N'1982707658716745728', N'客户信息维护', N'Customer Info', N'CustomerInfo', N'SecondaryMenu', N'custmat/custmat-basicinfo/customer', N'DeleteLocation', N'3', N'1', N'1', N'/api/CustMat/CustMatBasicInfo/CustomerInfo', N'', N'', N'1903486709602062336', N'2025-10-27 15:26:00.000', N'1903486709602062336', N'2025-11-02 13:02:25.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1982707658716745731', N'1982707658716745729', N'1982707658716745728', N'料号信息维护', N'PartNumber Info', N'PartNumberInfo', N'SecondaryMenu', N'custmat/custmat-basicinfo/partnumber', N'DeleteLocation', N'2', N'1', N'1', N'/api/CustMat/CustMatBasicInfo/PartNumberInfo', N'', N'', N'1903486709602062336', N'2025-10-27 15:26:00.000', N'1903486709602062336', N'2025-10-27 15:26:00.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1984848313438048256', N'1982707658716745729', N'1982707658716745728', N'厂商信息维护', N'Manufacturer Info', N'Manufacturer', N'SecondaryMenu', N'custmat/custmat-basicinfo/manufacturerinfo', N'Van', N'1', N'1', N'1', N'/api/CustMat/CustMatBasicInfo/ManufacturerInfo', N'', N'', N'1903486709602062336', N'2025-11-02 13:01:29.000', NULL, NULL)
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1988927293837414400', N'0', N'1968271760889614336', N'表单作业模块', N'Form Operate', N'FormOperate', N'PrimaryMenu', N'formbusiness/form-operate', N'Notebook', N'3', N'1', N'1', N'', N'', N'', N'1903486709602062336', N'2025-11-13 19:09:53.000', N'1903486709602062336', N'2025-11-13 19:10:07.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1988927293837414401', N'0', N'1968271760889614336', N'表单流程配置', N'Form Workflow', N'FormWorkflow', N'PrimaryMenu', N'formbusiness/form-workflow', N'Notebook', N'2', N'1', N'1', N'', N'', N'', N'1903486709602062336', N'2025-11-13 19:09:53.000', N'1903486709602062336', N'2025-11-13 19:10:07.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1988928242475732992', N'1988927293837414400', N'1968271760889614336', N'申请表单作业', N'Application Form', N'ApplicationForm', N'SecondaryMenu', N'formbusiness/form-operate/applyform', N'DocumentAdd', N'1', N'1', N'1', N'/api/FormBusiness/FormOperate/ApplyForm', N'', N'', N'1903486709602062336', N'2025-11-13 19:13:40.000', N'1903486709602062336', N'2025-11-15 09:34:45.000')
GO

INSERT INTO [Basic].[MenuInfo] ([MenuId], [ParentMenuId], [ModuleId], [MenuNameCn], [MenuNameEn], [MenuCode], [MenuType], [Path], [MenuIcon], [SortOrder], [IsEnabled], [IsVisible], [RoutePath], [Redirect], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1988928242475732993', N'1988927293837414401', N'1968271760889614336', N'表单步骤详情', N'Form Step', N'FormStep', N'SecondaryMenu', N'formbusiness/form-workflow/workflowstep', N'DocumentAdd', N'1', N'1', N'1', N'/api/FormBusiness/FormWorkFlow/WorkflowStep', N'', N'', N'1903486709602062336', N'2025-11-13 19:13:40.000', N'1903486709602062336', N'2025-11-15 09:34:45.000')
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
  [IsEnabled] int  NOT NULL,
  [IsVisible] int  NOT NULL,
  [RemarkCh] nvarchar(1000) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [RemarkEn] nvarchar(1000) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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
'MS_Description', N'是否启用：1=启用，0=不启用',
'SCHEMA', N'Basic',
'TABLE', N'ModuleInfo',
'COLUMN', N'IsEnabled'
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
INSERT INTO [Basic].[ModuleInfo] ([ModuleId], [ModuleCode], [ModuleNameCn], [ModuleNameEn], [Path], [ModuleIcon], [SortOrder], [IsEnabled], [IsVisible], [RemarkCh], [RemarkEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1350161679034934501', N'SystemBasicMgmt', N'系统基本管理', N'SystemBasic Mgmt', N'systembasicmgmt/index', N'Setting', N'1', N'1', N'1', N'系统管理模组用于统一管理系统的基本资讯、权限配置和资料字典。它支援系统参数维护、使用者和角色权限分配，以及业务字典的集中管理，提升系统的安全性与可维护性。', N'The System Management Module is used for the centralized management of the system''s basic information, permission configurations, and data dictionaries. It supports system parameter maintenance, user and role permission allocation, and centralized management of business dictionaries, thereby enhancing system security and maintainability.', N'1903486709602062336', N'2025-01-20 16:31:57.000', N'1903486709602062336', N'2025-11-03 21:36:33.000')
GO

INSERT INTO [Basic].[ModuleInfo] ([ModuleId], [ModuleCode], [ModuleNameCn], [ModuleNameEn], [Path], [ModuleIcon], [SortOrder], [IsEnabled], [IsVisible], [RemarkCh], [RemarkEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1968271760889614336', N'FormBusiness', N'表单签核业务', N'Form Business', N'formbusiness/index', N'Notebook', N'2', N'1', N'1', N'表单签核模块用于处理企业内部各类业务表单（如请假单、用印申请、采购申请等）的审批流程。', N'The Form Approval Module is designed to manage the approval workflow of various business forms (such as leave requests, stamping applications, purchase requests, etc.) within the organization.', N'1903486709602062336', N'2025-09-17 19:12:11.000', N'1903486709602062336', N'2025-10-19 01:47:57.000')
GO

INSERT INTO [Basic].[ModuleInfo] ([ModuleId], [ModuleCode], [ModuleNameCn], [ModuleNameEn], [Path], [ModuleIcon], [SortOrder], [IsEnabled], [IsVisible], [RemarkCh], [RemarkEn], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1982707658716745728', N'CustMat', N'客户生产计划', N'Customer Production Plan', N'custmat/index', N'Promotion', N'3', N'1', N'1', N'生产计划模块用于根据销售订单、库存状态、物料供应及产能情况，自动生成和调整生产计划，确保生产过程高效、有序、可控。', N'The Production Planning Module is designed to automatically generate and adjust production schedules based on sales orders, inventory status, material availability, and production capacity, ensuring an efficient, organized, and controllable manufacturing process.', N'1903486709602062336', N'2025-10-27 15:15:17.000', N'1903486709602062336', N'2025-11-03 20:48:30.000')
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
  [CreatedDate] datetime DEFAULT getdate() NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint DEFAULT NULL NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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
INSERT INTO [Basic].[RoleInfo] ([RoleId], [RoleCode], [RoleNameCn], [RoleNameEn], [Description], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'Administrator', N'管理员', N'Administrator', N'系统管理员权限', N'', N'1903486709602062336', N'2025-03-29 01:15:16.000', N'1903486709602062336', N'2025-10-19 02:19:33.000')
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
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint DEFAULT NULL NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1979881189825187840', N'1968272763634454528', N'1903486709602062336', N'2025-10-19 20:04:54.000', N'1903486709602062336', N'2025-10-19 20:04:54.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1979881189825187840', N'1968275489407766528', N'1903486709602062336', N'2025-10-19 20:04:54.000', N'1903486709602062336', N'2025-10-19 20:04:54.000')
GO

INSERT INTO [Basic].[RoleMenu] ([RoleId], [MenuId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1979881189825187840', N'1968275489407766529', N'1903486709602062336', N'2025-10-19 20:04:54.000', N'1903486709602062336', N'2025-10-19 20:04:54.000')
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
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint DEFAULT NULL NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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
INSERT INTO [Basic].[RoleModule] ([RoleId], [ModuleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1979881189825187840', N'1350161679034934501', N'1903486709602062336', N'2025-10-19 20:04:00.000', N'1903486709602062336', N'2025-10-19 20:04:00.000')
GO

INSERT INTO [Basic].[RoleModule] ([RoleId], [ModuleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1350161679034934501', N'1903486709602062336', N'2025-10-30 23:56:27.000', N'1903486709602062336', N'2025-10-30 23:56:27.000')
GO

INSERT INTO [Basic].[RoleModule] ([RoleId], [ModuleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1905670034215276544', N'1968271760889614336', N'1903486709602062336', N'2025-10-30 23:56:27.000', N'1903486709602062336', N'2025-10-30 23:56:27.000')
GO

INSERT INTO [Basic].[RoleModule] ([RoleId], [ModuleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1979881189825187840', N'1968271760889614336', N'1903486709602062336', N'2025-10-19 20:04:00.000', N'1903486709602062336', N'2025-10-19 20:04:00.000')
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
  [StartTime] datetime2(7)  NOT NULL,
  [EndTime] datetime2(7)  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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
INSERT INTO [Basic].[UserAgent] ([SubstituteUserId], [AgentUserId], [StartTime], [EndTime], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1962083859264835584', N'1969079079705645056', N'2025-10-02 16:00:00.0000000', N'2025-10-03 16:00:00.0000000', N'1903486709602062336', N'2025-10-03 19:42:06.000', N'1903486709602062336', N'2025-10-03 19:42:06.000')
GO


-- ----------------------------
-- Table structure for UserFormBind
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[UserFormBind]') AND type IN ('U'))
	DROP TABLE [Basic].[UserFormBind]
GO

CREATE TABLE [Basic].[UserFormBind] (
  [UserId] bigint  NOT NULL,
  [FormGroupTypeId] bigint  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
)
GO

ALTER TABLE [Basic].[UserFormBind] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工Id',
'SCHEMA', N'Basic',
'TABLE', N'UserFormBind',
'COLUMN', N'UserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'表单组别类型Id',
'SCHEMA', N'Basic',
'TABLE', N'UserFormBind',
'COLUMN', N'FormGroupTypeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'UserFormBind',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'UserFormBind',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'UserFormBind',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'UserFormBind',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工表单绑定表',
'SCHEMA', N'Basic',
'TABLE', N'UserFormBind'
GO


-- ----------------------------
-- Records of UserFormBind
-- ----------------------------
INSERT INTO [Basic].[UserFormBind] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1987215338470772736', N'1903486709602062336', N'2025-11-13 19:40:53.000', N'1903486709602062336', N'2025-11-13 19:40:53.000')
GO

INSERT INTO [Basic].[UserFormBind] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1987217256446300160', N'1903486709602062336', N'2025-11-13 19:40:53.000', N'1903486709602062336', N'2025-11-13 19:40:53.000')
GO

INSERT INTO [Basic].[UserFormBind] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969052085492256768', N'1903486709602062336', N'2025-11-13 19:40:53.000', N'1903486709602062336', N'2025-11-13 19:40:53.000')
GO

INSERT INTO [Basic].[UserFormBind] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969053776929230848', N'1903486709602062336', N'2025-11-13 19:40:53.000', N'1903486709602062336', N'2025-11-13 19:40:53.000')
GO

INSERT INTO [Basic].[UserFormBind] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969054482025287680', N'1903486709602062336', N'2025-11-13 19:40:53.000', N'1903486709602062336', N'2025-11-13 19:40:53.000')
GO

INSERT INTO [Basic].[UserFormBind] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969054690842906624', N'1903486709602062336', N'2025-11-13 19:40:53.000', N'1903486709602062336', N'2025-11-13 19:40:53.000')
GO

INSERT INTO [Basic].[UserFormBind] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969054813085896704', N'1903486709602062336', N'2025-11-13 19:40:53.000', N'1903486709602062336', N'2025-11-13 19:40:53.000')
GO

INSERT INTO [Basic].[UserFormBind] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969055160932110336', N'1903486709602062336', N'2025-11-13 19:40:53.000', N'1903486709602062336', N'2025-11-13 19:40:53.000')
GO

INSERT INTO [Basic].[UserFormBind] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969055351626141696', N'1903486709602062336', N'2025-11-13 19:40:53.000', N'1903486709602062336', N'2025-11-13 19:40:53.000')
GO

INSERT INTO [Basic].[UserFormBind] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969055451307970560', N'1903486709602062336', N'2025-11-13 19:40:53.000', N'1903486709602062336', N'2025-11-13 19:40:53.000')
GO

INSERT INTO [Basic].[UserFormBind] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969055549681176576', N'1903486709602062336', N'2025-11-13 19:40:53.000', N'1903486709602062336', N'2025-11-13 19:40:53.000')
GO

INSERT INTO [Basic].[UserFormBind] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969055723409248256', N'1903486709602062336', N'2025-11-13 19:40:53.000', N'1903486709602062336', N'2025-11-13 19:40:53.000')
GO

INSERT INTO [Basic].[UserFormBind] ([UserId], [FormGroupTypeId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1969055819815325696', N'1903486709602062336', N'2025-11-13 19:40:53.000', N'1903486709602062336', N'2025-11-13 19:40:53.000')
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
  [HireDate] date  NOT NULL,
  [Nationality] bigint  NOT NULL,
  [LaborId] bigint  NOT NULL,
  [Email] nvarchar(25) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [PhoneNumber] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [LoginNo] nvarchar(50) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [PassWord] nvarchar(100) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [PwdSalt] nvarchar(60) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [AvatarAddress] nvarchar(150) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [IsEmployed] int  NOT NULL,
  [IsApproval] int  NOT NULL,
  [IsRealtimeNotification] int  NOT NULL,
  [IsScheduledNotification] int  NOT NULL,
  [IsAgent] int  NOT NULL,
  [IsParttime] int  NOT NULL,
  [IsFreeze] int  NOT NULL,
  [ExpirationDays] int  NOT NULL,
  [ExpirationTime] datetime  NULL,
  [Remark] nvarchar(200) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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
'MS_Description', N'是否签核',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'IsApproval'
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
'MS_Description', N'是否冻结',
'SCHEMA', N'Basic',
'TABLE', N'UserInfo',
'COLUMN', N'IsFreeze'
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
INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1929535196076576906', N'1351602631784529920', N'E347473', N'裴小然', N'Xiaoran Pei', N'1', N'2024-07-01', N'1972220728019390464', N'1956396323422998528', N'3841510708@qq.com', N'18815384916', N'E347473', N'QQ8VvNaTsr53fRQPTOpaFd4rPLIuqPc6a8DJjM18S9E=', N'r7FNzqUAkY8TmD6mjl7UCA==', N'127.0.0.1:9000/systemadmin/20251227004216542_918e9c0a.jpg', N'1', N'1', N'1', N'1', N'0', N'1', N'1', N'180', N'2026-07-01 16:47:27.000', N'', N'1903486709602062336', N'2025-03-23 00:39:31.000', N'1903486709602062336', N'2025-12-27 00:42:31.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1961651950017712128', N'1929535196076576830', N'1351581732096180224', N'E327852', N'黃仁華', N'Paul Huang', N'1', N'2025-08-30', N'1972220728019390464', N'1956396264467861504', N'3841510708@qq.com', N'13616266055', N'E327852', N'T9ncvVo1LGia8KNoUeYR8IcuJzDeZomXWmpZpBkYalQ=', N'8aKznfX78xIhh7FjgfmpuQ==', N'http://127.0.0.1:9000/systemsadminbucket/20251003/20251003110631504.jpg', N'1', N'1', N'1', N'1', N'0', N'0', N'0', N'60', N'2025-10-29 12:47:25.000', N'', N'1903486709602062336', N'2025-08-30 12:47:25.000', N'1903486709602062336', N'2025-10-03 19:06:32.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1961653729589596160', N'1929535196076576906', N'1351601258426793984', N'E342306', N'于長洋', N'Changyang Yu', N'1', N'2025-08-30', N'1972220728019390464', N'1956396323422998528', N'3841510708@qq.com', N'15767661600', N'E342306', N'CHhX6UgNlcAUQ23sdgA6edpcvMs2WKZ24vTW97fDHzo=', N'5zj8c6aU9G3eRT+5Di9RzA==', N'http://127.0.0.1:9000/systemsadminbucket/20251003/20251003110610293.jpg', N'1', N'1', N'1', N'1', N'0', N'0', N'0', N'60', N'2025-10-29 12:54:29.000', N'', N'1903486709602062336', N'2025-08-30 12:54:29.000', N'1903486709602062336', N'2025-10-03 19:06:11.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1961654554483363840', N'1929535196076576906', N'1351601258426793984', N'E347072', N'丁甲乙', N'Darren Ding', N'1', N'2025-08-30', N'1972220728019390464', N'1956396323422998528', N'3841510708@qq.com', N'', N'E347072', N'+DMa84C3C5YcB5lh80xsiYP4jr4wLCR0Qnw348Ce45I=', N'SNeTdFinEVJLmIdGqmIoRw==', N'http://127.0.0.1:9000/systemsadminbucket/20251003/20251003110614238.jpg', N'1', N'1', N'1', N'1', N'0', N'0', N'0', N'60', N'2025-10-29 12:57:46.000', N'', N'1903486709602062336', N'2025-08-30 12:57:46.000', N'1903486709602062336', N'2025-10-03 19:06:15.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1961654824361660416', N'1929535196076576906', N'1351602631784529920', N'E348184', N'譚冰瑩', N'Ice Tan', N'0', N'2025-08-30', N'1972220728019390464', N'1956396323422998528', N'3841510708@qq.com', N'', N'E348184', N'XykdqsLFn5iOJOz9mAUYSgVwOIquXMZOQjy7D3YGrk4=', N'yulxaWM5s04AbmzH4jqJKA==', N'http://127.0.0.1:9000/systemsadminbucket/20251003/20251003110556247.jpg', N'1', N'1', N'1', N'1', N'0', N'0', N'0', N'60', N'2025-10-29 12:58:50.000', N'', N'1903486709602062336', N'2025-08-30 12:58:50.000', N'1903486709602062336', N'2025-10-03 19:05:57.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1961655127194603520', N'1929535196076576906', N'1351602771312246784', N'E346899', N'龔喆浩', N'Zhehao Gong', N'1', N'2025-08-30', N'1972220728019390464', N'1956396323422998528', N'3841510708@qq.com', N'', N'E346899', N'BQveTlaG6BgCrnM40p6bQEBVQIb2PxSYkMz1MtFBRLc=', N'0In06Dw6orFtSKCj9C+sWg==', N'http://127.0.0.1:9000/systemsadminbucket/20251003/20251003110602691.jpg', N'1', N'1', N'1', N'1', N'0', N'0', N'0', N'60', N'2025-10-29 13:00:03.000', N'', N'1903486709602062336', N'2025-08-30 13:00:03.000', N'1903486709602062336', N'2025-10-03 19:06:03.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1961846945383321600', N'1929535196076576812', N'1351584156689104896', N'E347075', N'徐奭傑', N'Marcus Hsu', N'1', N'2025-08-31', N'1972220728019390464', N'1956395974389796864', N'3841510708@qq.com', N'13801996994', N'E347075', N'F3JsyUVIMJkIgSWEgccrbNmBKMeUrKivdI9zeeQj9YE=', N'hMVY3ZCSR4TbR69vMh13vQ==', N'http://127.0.0.1:9000/systemsadminbucket/20251003/20251003110643556.jpg', N'1', N'1', N'1', N'1', N'0', N'1', N'0', N'60', N'2025-12-02 21:18:09.000', N'', N'1903486709602062336', N'2025-08-31 01:42:16.000', N'1903486709602062336', N'2025-10-03 21:18:09.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1962082837364609024', N'1929535196076576812', N'1351585319710883840', N'E347074', N'姜佑康', N'Bryan Chiang', N'1', N'2025-08-31', N'1972220728019390464', N'1956395974389796864', N'3841510708@qq.com', N'', N'E347074', N'2P2De8jBuTi2XGSs4TVLrXdbovCXLR8U1Fsd8n0aYQU=', N'aGpDJ7EWLLydadYvCuw+WA==', N'http://127.0.0.1:9000/systemsadminbucket/20251003/20251003110647994.jpg', N'1', N'1', N'1', N'1', N'0', N'0', N'0', N'60', N'2025-10-30 17:19:37.000', N'', N'1903486709602062336', N'2025-08-31 17:19:37.000', N'1903486709602062336', N'2025-10-03 19:06:49.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1962083859264835584', N'1929535196076576768', N'1351581732096180224', N'E215396', N'蔡嘉祥', N'Tsai', N'1', N'2025-08-31', N'1972220728019390464', N'1962083956962758656', N'3841510708@qq.com', N'', N'E215396', N'TRaGBVYcLJSj5zyjShWoPJHE+A6/VEL8lRMfBwbd+zk=', N'Bw8BeYTUPmy0+0CbLbuGkA==', N'127.0.0.1:9000/systemadmin/20251227004920650_5e631d5b.jpg', N'1', N'1', N'1', N'0', N'0', N'0', N'0', N'180', N'2026-06-25 02:00:40.000', N'', N'1903486709602062336', N'2025-08-31 17:23:40.000', N'1903486709602062336', N'2025-12-27 02:00:40.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1962089884348977152', N'1929535196076576831', N'1351592033860452352', N'E325994', N'王家豪', N'Sky', N'1', N'2025-08-31', N'1972220728019390464', N'1956396031587520512', N'3841510708@qq.com', N'', N'E325994', N'42+s+iA4/rQUJzgDAgJ3RoQmruX1dmlmY2rPr8rbhf8=', N'2WfiI99Eg7bsv0DJ8Ody/g==', N'http://127.0.0.1:9000/systemsadminbucket/20251003/20251003131949837.jpg', N'1', N'1', N'1', N'1', N'0', N'0', N'0', N'60', N'2025-12-02 21:23:34.000', N'', N'1903486709602062336', N'2025-08-31 17:47:37.000', N'1903486709602062336', N'2025-10-03 21:23:34.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1962090367855759360', N'1929535196076576907', N'1351600746193223680', N'E249571', N'范亚君', N'Yajun Fan', N'1', N'2025-08-31', N'1972220728019390464', N'1956396031587520512', N'3841510708@qq.com', N'', N'E249571', N'Qoa5lnSYf4iCBk/mTPwY7X6zkeyIbwAFI8sBBgXUETI=', N'/SXeGJyvj5omNv0CVM0XZQ==', N'http://127.0.0.1:9000/systemsadminbucket/20251003/20251003131934322.jpg', N'1', N'1', N'1', N'1', N'0', N'0', N'0', N'60', N'2025-12-02 21:19:35.000', N'', N'1903486709602062336', N'2025-08-31 17:49:32.000', N'1903486709602062336', N'2025-10-03 21:19:35.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1962091248886091776', N'1929535196076576876', N'1351601258426793984', N'E347377', N'喬知強', N'Zhiqiang Qiao', N'1', N'2025-08-31', N'1972220728019390464', N'1962091425965412352', N'3841510708@qq.com', N'', N'E347377', N'KGcpgw92+jrDFb0EizJO5eyD1o46RMcex9PZp8O+o2w=', N'XhUiCEvbyEFnZsTgiv5Nkw==', N'http://127.0.0.1:9000/systemsadminbucket/20251003/20251003120618339.jpg', N'1', N'1', N'1', N'1', N'0', N'1', N'0', N'60', N'2025-10-30 17:53:02.000', N'', N'1903486709602062336', N'2025-08-31 17:53:02.000', N'1903486709602062336', N'2025-10-03 20:06:23.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969079079705645056', N'1929535196076576768', N'1351582085961220096', N'ETW00331', N'黃薰鋒', N'Stein Huang', N'1', N'2025-09-20', N'1972220728019390464', N'1962084148730531840', N'3841510708@qq.com', N'', N'ETW00331', N'u3Kroho36l4NrbiqywHC7BpDViDk25GP0tJlKuZxJLY=', N'8dIffCMtw3wgzqUoxLvdCQ==', N'127.0.0.1:9000/systemadmin/20251227015121374_2dcd5a31.jpg', N'1', N'1', N'1', N'1', N'1', N'1', N'0', N'180', N'2026-06-25 01:51:29.000', N'', N'1903486709602062336', N'2025-09-20 00:40:11.000', N'1903486709602062336', N'2025-12-27 01:51:29.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969079427610578944', N'1929535196076576769', N'1351583636813512704', N'ETW00366', N'吳崇源', N'Allen Wu', N'1', N'2025-09-20', N'1972220728019390464', N'1962084148730531840', N'3841510708@qq.com', N'', N'ETW00366', N'13HrafN9ZBF01qa0ahgLd7NiwniHr9fwVg4ElIRZmZs=', N'3kRXd8MEO6lgqSi5hREy8w==', N'http://127.0.0.1:9000/systemsadminbucket/20251003/20251003110447008.jpg', N'1', N'1', N'0', N'0', N'0', N'0', N'0', N'180', N'2026-03-19 00:41:34.000', N'', N'1903486709602062336', N'2025-09-20 00:41:34.000', N'1903486709602062336', N'2025-10-03 19:04:49.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969079663670202368', N'1929535196076576769', N'1351585319710883840', N'E348464', N'鄭洋洋', N'Grace Zheng', N'0', N'2025-09-20', N'1972220728019390464', N'1962084148730531840', N'3841510708@qq.com', N'', N'E348464', N'SmKWDtGUZj4W67kJnWnk+Jp4sxjOc6SSzpRiipp7LX4=', N'4CXkDWkIIFnt6K3qse9QpQ==', N'http://127.0.0.1:9000/systemsadminbucket/20251003/20251003110456102.jpg', N'1', N'1', N'0', N'0', N'0', N'0', N'0', N'180', N'2026-03-19 00:42:30.000', N'', N'1903486709602062336', N'2025-09-20 00:42:30.000', N'1903486709602062336', N'2025-10-03 19:05:00.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969080546520862720', N'1929535196076576768', N'1351592033860452352', N'ETW00375', N'温文福', N'Max Wen', N'1', N'2025-09-20', N'1972220728019390464', N'1956372741586292736', N'3841510708@qq.com', N'', N'ETW00375', N'mCSDfai8Hlsl8ugRK0VUId8O65V6sgCOo+m6H7/BqaM=', N'HojnnQFhcme6KmAxVbHOpw==', N'http://127.0.0.1:9000/systemsadminbucket/20251003/20251003110421462.jpg', N'1', N'1', N'0', N'0', N'0', N'0', N'0', N'180', N'2026-03-19 00:46:00.000', N'', N'1903486709602062336', N'2025-09-20 00:46:00.000', N'1903486709602062336', N'2025-10-03 19:04:34.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969081656988012544', N'1929535196076576770', N'1351592574749507584', N'E340159', N'林惠峰', N'Huifeng Lin', N'0', N'2025-09-20', N'1972220728019390464', N'1969082012346224640', N'3841510708@qq.com', N'', N'E340159', N'PbHn8pd4VkvpVUWvPtfSldjbQJ04v84n4lwa8CNK3xI=', N'7ZoY7Y81jLSIVnUGfLNFgw==', N'http://127.0.0.1:9000/systemsadminbucket/20251108/20251108172541305.jpg', N'1', N'1', N'0', N'0', N'0', N'0', N'0', N'180', N'2026-05-08 01:25:48.000', N'', N'1903486709602062336', N'2025-09-20 00:50:25.000', N'1903486709602062336', N'2025-11-09 01:25:48.000')
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969083667879956480', N'1929535196076576773', N'1351581732096180224', N'ETW00327', N'吳建中', N'Jeffrey Wu', N'1', N'2025-09-20', N'1972220728019390464', N'1969082012346224640', N'3841510708@qq.com', N'', N'ETW00327', N'gNZ6W71JjJPkHu+PPxu+F54flDF+kRNyHPAzZqKqy1U=', N'gNUvAY2loNmHRUqHVlNBeg==', N'http://127.0.0.1:9000/mybucket/20250919/20250919165823803.jpg', N'1', N'0', N'0', N'0', N'0', N'0', N'0', N'180', N'2026-03-19 00:58:25.000', N'', N'1903486709602062336', N'2025-09-20 00:58:25.000', N'0', NULL)
GO

INSERT INTO [Basic].[UserInfo] ([UserId], [DepartmentId], [PositionId], [UserNo], [UserNameCn], [UserNameEn], [Gender], [HireDate], [Nationality], [LaborId], [Email], [PhoneNumber], [LoginNo], [PassWord], [PwdSalt], [AvatarAddress], [IsEmployed], [IsApproval], [IsRealtimeNotification], [IsScheduledNotification], [IsAgent], [IsParttime], [IsFreeze], [ExpirationDays], [ExpirationTime], [Remark], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969084580594061312', N'1929535196076576784', N'1351581732096180224', N'E202134', N'顧玉芬', N'Grace Gu', N'0', N'2025-09-20', N'1972220728019390464', N'1969084707446591488', N'3841510708@qq.com', N'', N'E202134', N'lozFYgTBZe/yp8b/FcZG7yTn5nWdHbktUNVt17nSxzY=', N'oNBQZxg7eZVL8BiB+I1llw==', N'http://127.0.0.1:9000/mybucket/20250919/20250919170158661.jpg', N'1', N'0', N'0', N'0', N'0', N'0', N'0', N'180', N'2026-03-19 01:02:02.000', N'', N'1903486709602062336', N'2025-09-20 01:02:02.000', N'1903486709602062336', N'2025-09-20 01:03:56.000')
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
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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

INSERT INTO [Basic].[UserLabor] ([LaborId], [LaborNameCn], [LaborNameEn], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1956395974389796864', N'IT项目经理', N'IT Project Manager', N'', N'1903486709602062336', N'2025-08-16 00:42:03.000', N'1903486709602062336', N'2025-11-03 21:31:48.000')
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


-- ----------------------------
-- Table structure for UserLock
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[UserLock]') AND type IN ('U'))
	DROP TABLE [Basic].[UserLock]
GO

CREATE TABLE [Basic].[UserLock] (
  [UserId] bigint  NOT NULL,
  [NumberErrors] int  NOT NULL,
  [CreatedDate] datetime  NOT NULL
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
'MS_Description', N'用户锁定记录',
'SCHEMA', N'Basic',
'TABLE', N'UserLock'
GO


-- ----------------------------
-- Records of UserLock
-- ----------------------------
INSERT INTO [Basic].[UserLock] ([UserId], [NumberErrors], [CreatedDate]) VALUES (N'1903486709602062336', N'5', N'2026-01-02 16:49:42.000')
GO


-- ----------------------------
-- Table structure for UserLogOut
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[UserLogOut]') AND type IN ('U'))
	DROP TABLE [Basic].[UserLogOut]
GO

CREATE TABLE [Basic].[UserLogOut] (
  [UserId] bigint  NULL,
  [IP] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [StatusId] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [LoginDate] datetime  NOT NULL
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
INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'198.18.0.1', N'LoginSuccessful', N'2025-12-27 14:30:08.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'198.18.0.1', N'LoginSuccessful', N'2025-12-27 15:42:52.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'198.18.0.1', N'LoginSuccessful', N'2025-12-27 15:11:35.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'198.18.0.1', N'LoggedOut', N'2025-12-27 15:32:39.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2025-12-30 15:00:51.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2025-12-30 16:28:28.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-01-02 10:20:45.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:20:52.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:20:53.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:20:54.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:20:54.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:20:55.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:22:15.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:22:18.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-01-02 10:22:29.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:24:28.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:24:29.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:24:29.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:24:30.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 10:24:30.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-01-02 10:25:33.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-01-02 13:05:15.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 14:52:54.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 14:52:57.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 14:52:58.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 14:52:58.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 14:52:59.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 16:47:32.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'LoginSuccessful', N'2026-01-02 16:47:39.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 16:49:42.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 16:49:43.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 16:49:44.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 16:49:44.000')
GO

INSERT INTO [Basic].[UserLogOut] ([UserId], [IP], [StatusId], [LoginDate]) VALUES (N'1903486709602062336', N'10.112.142.178', N'IncorrectPassword', N'2026-01-02 16:49:45.000')
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
  [PartTimeLaborId] bigint  NOT NULL,
  [StartTime] datetime2(7)  NOT NULL,
  [EndTime] datetime2(7)  NOT NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime DEFAULT NULL NULL
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
'MS_Description', N'兼任职业',
'SCHEMA', N'Basic',
'TABLE', N'UserPartTime',
'COLUMN', N'PartTimeLaborId'
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
INSERT INTO [Basic].[UserPartTime] ([UserId], [PartTimeDeptId], [PartTimePositionId], [PartTimeLaborId], [StartTime], [EndTime], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1962091248886091776', N'1929535196076576906', N'1351601258426793984', N'1956395917238210560', N'2025-08-30 08:00:00.0000000', N'2026-08-30 08:00:00.0000000', N'1903486709602062336', N'2025-08-31 17:08:33.000', N'1903486709602062336', N'2025-10-03 21:05:41.000')
GO

INSERT INTO [Basic].[UserPartTime] ([UserId], [PartTimeDeptId], [PartTimePositionId], [PartTimeLaborId], [StartTime], [EndTime], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1961846945383321600', N'1929535196076576812', N'1351601258426793984', N'1956396031587520512', N'2025-10-03 00:00:00.0000000', N'2026-10-03 00:00:00.0000000', N'1903486709602062336', N'2025-10-03 21:13:15.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserPartTime] ([UserId], [PartTimeDeptId], [PartTimePositionId], [PartTimeLaborId], [StartTime], [EndTime], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969079079705645056', N'1929535196076576768', N'1351581732096180224', N'1962083956962758656', N'2025-10-20 00:00:00.0000000', N'2025-10-31 00:00:00.0000000', N'1903486709602062336', N'2025-10-18 15:20:31.000', NULL, NULL)
GO


-- ----------------------------
-- Table structure for UserPosition
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[Basic].[UserPosition]') AND type IN ('U'))
	DROP TABLE [Basic].[UserPosition]
GO

CREATE TABLE [Basic].[UserPosition] (
  [PositionId] bigint  NOT NULL,
  [PositionNo] nvarchar(5) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [PositionNameCn] nvarchar(10) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [PositionNameEn] nvarchar(20) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NOT NULL,
  [PositionOrderBy] int  NULL,
  [Description] nvarchar(150) COLLATE Chinese_PRC_90_CI_AS_SC_UTF8  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint  NULL,
  [ModifiedDate] datetime  NULL
)
GO

ALTER TABLE [Basic].[UserPosition] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职级Id',
'SCHEMA', N'Basic',
'TABLE', N'UserPosition',
'COLUMN', N'PositionId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职级编码',
'SCHEMA', N'Basic',
'TABLE', N'UserPosition',
'COLUMN', N'PositionNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职级名称（中文）',
'SCHEMA', N'Basic',
'TABLE', N'UserPosition',
'COLUMN', N'PositionNameCn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职级名称（英文）',
'SCHEMA', N'Basic',
'TABLE', N'UserPosition',
'COLUMN', N'PositionNameEn'
GO

EXEC sp_addextendedproperty
'MS_Description', N'职级排序',
'SCHEMA', N'Basic',
'TABLE', N'UserPosition',
'COLUMN', N'PositionOrderBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'职级描述',
'SCHEMA', N'Basic',
'TABLE', N'UserPosition',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'Basic',
'TABLE', N'UserPosition',
'COLUMN', N'CreatedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'Basic',
'TABLE', N'UserPosition',
'COLUMN', N'CreatedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改人',
'SCHEMA', N'Basic',
'TABLE', N'UserPosition',
'COLUMN', N'ModifiedBy'
GO

EXEC sp_addextendedproperty
'MS_Description', N'修改时间',
'SCHEMA', N'Basic',
'TABLE', N'UserPosition',
'COLUMN', N'ModifiedDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'员工职级表',
'SCHEMA', N'Basic',
'TABLE', N'UserPosition'
GO


-- ----------------------------
-- Records of UserPosition
-- ----------------------------
INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351581732096180224', N'S15', N'师十五', N'Division 15', N'1', N'董事长', N'1903486709602062336', N'2025-01-23 14:28:55.000', N'1903486709602062336', N'2025-01-23 14:28:55.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351582085961220096', N'S14', N'师十四', N'Division 14', N'2', N'总经理', N'1903486709602062336', N'2025-01-23 14:29:37.000', N'1903486709602062336', N'2025-01-23 14:29:37.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351583500196642816', N'S13', N'师十三', N'Division 13', N'3', N'副总', N'1903486709602062336', N'2025-01-23 14:32:26.000', N'1903486709602062336', N'2025-01-23 14:32:26.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351583636813512704', N'S12', N'师十二', N'Division 12', N'4', N'协理', N'1903486709602062336', N'2025-01-23 14:32:42.000', N'1903486709602062336', N'2025-01-23 14:32:42.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351584014896463872', N'S11', N'师十一', N'Division 11', N'5', N'厂长', N'1903486709602062336', N'2025-01-23 14:33:27.000', N'1903486709602062336', N'2025-01-23 14:33:27.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351584156689104896', N'S10', N'师十', N'Division 10', N'6', N'资深经理', N'1903486709602062336', N'2025-01-23 14:33:44.000', N'1903486709602062336', N'2025-01-23 14:33:44.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351585319710883840', N'S09', N'师九', N'Division 9', N'7', N'经理', N'1903486709602062336', N'2025-01-23 14:36:03.000', N'1903486709602062336', N'2025-01-23 14:36:03.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351592033860452352', N'S08', N'师八', N'Division 8', N'8', N'资深副理', N'1903486709602062336', N'2025-01-23 14:49:23.000', N'1903486709602062336', N'2025-01-23 14:49:23.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351592278136717312', N'S07', N'师七', N'Division 7', N'9', N'副理', N'1903486709602062336', N'2025-01-23 14:49:52.000', N'1903486709602062336', N'2025-01-23 14:49:52.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351592574749507584', N'S06', N'师六', N'Division 6', N'10', N'专理', N'1903486709602062336', N'2025-01-23 14:50:28.000', N'1903486709602062336', N'2025-01-23 14:50:28.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351600746193223680', N'S05', N'师五', N'Division 5', N'11', N'课长/工程师', N'1903486709602062336', N'2025-01-23 15:06:42.000', N'1903486709602062336', N'2025-01-23 15:06:42.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351601258426793984', N'S04', N'师四', N'Division 4', N'12', N'副课长/工程师', N'1903486709602062336', N'2025-01-23 15:07:43.000', N'1903486709602062336', N'2025-01-23 15:07:43.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351602631784529920', N'S03', N'师三', N'Division 3', N'13', N'组长', N'1903486709602062336', N'2025-01-23 15:10:27.000', N'1903486709602062336', N'2025-01-23 15:10:27.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351602771312246784', N'S02', N'师二', N'Division 2', N'14', N'师二', N'1903486709602062336', N'2025-01-23 15:10:43.000', N'1903486709602062336', N'2025-01-23 15:10:43.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351602976027836416', N'S01', N'师一', N'Division 1', N'15', N'师一', N'1903486709602062336', N'2025-01-23 15:11:08.000', N'1903486709602062336', N'2025-01-23 15:11:08.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351604412149137408', N'Y01', N'员一', N'Member 1', N'16', N'员一', N'1903486709602062336', N'2025-01-23 15:13:59.000', N'1903486709602062336', N'2025-01-23 15:13:59.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351604819424444416', N'Y02', N'员二', N'Member 2', N'17', N'员二', N'1903486709602062336', N'2025-01-23 15:14:47.000', N'1903486709602062336', N'2025-01-23 15:14:47.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351604916346421248', N'Y03', N'员三', N'Member 3', N'18', N'员三', N'1903486709602062336', N'2025-01-23 15:14:59.000', N'1903486709602062336', N'2025-01-23 15:14:59.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351605048617992192', N'Y04', N'员四', N'Member 4', N'19', N'员四', N'1903486709602062336', N'2025-01-23 15:15:15.000', N'1903486709602062336', N'2025-01-23 15:15:15.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351605261411811328', N'Y05', N'员五', N'Member 5', N'20', N'员五', N'1903486709602062336', N'2025-01-23 15:15:40.000', N'1903486709602062336', N'2025-01-23 15:15:40.000')
GO

INSERT INTO [Basic].[UserPosition] ([PositionId], [PositionNo], [PositionNameCn], [PositionNameEn], [PositionOrderBy], [Description], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1351612928515637248', N'Y06', N'员级', N'Member level', N'21', N'员级', N'1903486709602062336', N'2025-01-23 15:30:54.000', N'1903486709602062336', N'2025-01-23 15:30:54.000')
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
  [Remarks] nvarchar(1000) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreatedBy] bigint  NOT NULL,
  [CreatedDate] datetime DEFAULT getdate() NOT NULL,
  [ModifiedBy] bigint DEFAULT NULL NULL,
  [ModifiedDate] datetime DEFAULT getdate() NULL
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
'MS_Description', N'备注',
'SCHEMA', N'Basic',
'TABLE', N'UserRole',
'COLUMN', N'Remarks'
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
INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1903486709602062336', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-03-04 16:50:11.000', N'1903486709602062336', N'2025-12-27 00:42:31.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1961651950017712128', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-08-30 12:47:26.000', N'1903486709602062336', N'2025-10-03 19:06:32.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1961653729589596160', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-08-30 12:54:29.000', N'1903486709602062336', N'2025-10-03 19:06:11.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1961654554483363840', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-08-30 12:57:46.000', N'1903486709602062336', N'2025-10-03 19:06:15.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1961654824361660416', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-08-30 12:58:50.000', N'1903486709602062336', N'2025-10-03 19:05:57.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1961655127194603520', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-08-30 13:00:03.000', N'1903486709602062336', N'2025-10-03 19:06:03.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1961846945383321600', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-08-31 01:42:16.000', N'1903486709602062336', N'2025-10-03 21:18:09.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1962082837364609024', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-08-31 17:19:37.000', N'1903486709602062336', N'2025-10-03 19:06:49.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1962083859264835584', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-08-31 17:23:40.000', N'1903486709602062336', N'2025-12-27 02:00:40.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1962089884348977152', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-08-31 17:47:37.000', N'1903486709602062336', N'2025-10-03 21:23:34.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1962090367855759360', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-08-31 17:49:32.000', N'1903486709602062336', N'2025-10-03 21:19:35.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1962091248886091776', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-08-31 17:53:02.000', N'1903486709602062336', N'2025-10-03 20:06:23.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969079079705645056', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-09-20 00:40:11.000', N'1903486709602062336', N'2025-12-27 01:51:29.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969079427610578944', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-09-20 00:41:34.000', N'1903486709602062336', N'2025-10-03 19:04:49.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969079663670202368', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-09-20 00:42:30.000', N'1903486709602062336', N'2025-10-03 19:05:00.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969080546520862720', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-09-20 00:46:00.000', N'1903486709602062336', N'2025-10-03 19:04:34.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969081656988012544', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-09-20 00:50:25.000', N'1903486709602062336', N'2025-11-09 01:25:48.000')
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969083667879956480', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-09-20 00:58:25.000', NULL, NULL)
GO

INSERT INTO [Basic].[UserRole] ([UserId], [RoleId], [Remarks], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (N'1969084580594061312', N'1905670034215276544', NULL, N'1903486709602062336', N'2025-09-20 01:02:02.000', N'1903486709602062336', N'2025-09-20 01:03:56.000')
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
-- Primary Key structure for table RoleInfo
-- ----------------------------
ALTER TABLE [Basic].[RoleInfo] ADD CONSTRAINT [PK__RoleInfo__8AFACE1A81A70B73] PRIMARY KEY CLUSTERED ([RoleId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table UserInfo
-- ----------------------------
ALTER TABLE [Basic].[UserInfo] ADD CONSTRAINT [PK__UserInfo__1788CC4C15412757] PRIMARY KEY CLUSTERED ([UserId], [UserNo])
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
-- Primary Key structure for table UserPosition
-- ----------------------------
ALTER TABLE [Basic].[UserPosition] ADD CONSTRAINT [PK__UserPosi__60BB9A7952FCAD62] PRIMARY KEY CLUSTERED ([PositionId])
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

