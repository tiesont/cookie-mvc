USE [YourDatabaseName]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BinaryFile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](200) NOT NULL,
	[FileData] [varbinary](max) NOT NULL,
	[Size] [bigint] NOT NULL,
	[MimeType] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_BinaryFile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Role] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](320) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[Salt] [nvarchar](200) NOT NULL,
	[DisplayName] [nvarchar](200) NOT NULL,
	[ResetToken] [varchar](300) NULL,
	[ResetTokenExpiresOn] [datetime2](7) NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_User_IsActive]  DEFAULT ((0)),
	[CreatedOn] [datetime2](7) NOT NULL CONSTRAINT [DF_User_CreatedOn]  DEFAULT (sysdatetime()),
	[UpdatedOn] [datetime2](7) NULL,
	[PasswordAttempts] [int] NOT NULL CONSTRAINT [DF_User_PasswordAttempts]  DEFAULT ((0)),
	[LastPasswordAttemptOn] [datetime2](7) NULL,
	[LockedOn] [datetime2](7) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserRole] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[RoleID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Role] ON 

GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (1, N'Registered User')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] 
([ID], [UserName], [Password], [Salt], [DisplayName], [ResetToken], [ResetTokenExpiresOn], [IsActive], [CreatedOn], [UpdatedOn], [PasswordAttempts], [LastPasswordAttemptOn], [LockedOn]) 
VALUES 
(1, N'user@example.com', N'---', N'---', N'Test User', NULL, NULL, 1, CAST(N'2015-04-19 03:08:46.0164446' AS DateTime2), CAST(N'2015-04-19 04:33:28.817' AS DateTime2), 0, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO
