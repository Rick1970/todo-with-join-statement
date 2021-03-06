USE [todo]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 8/2/2016 10:56:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[categories_tasks]    Script Date: 8/2/2016 10:56:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories_tasks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[category_id] [int] NULL,
	[task_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tasks]    Script Date: 8/2/2016 10:56:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tasks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](255) NULL,
	[due_date] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[categories] ON 

INSERT [dbo].[categories] ([id], [name]) VALUES (1, N'Outside')
INSERT [dbo].[categories] ([id], [name]) VALUES (2, N'Inside')
SET IDENTITY_INSERT [dbo].[categories] OFF
SET IDENTITY_INSERT [dbo].[tasks] ON 

INSERT [dbo].[tasks] ([id], [description], [due_date]) VALUES (1, N'Walk the dog', NULL)
INSERT [dbo].[tasks] ([id], [description], [due_date]) VALUES (2, N'Water the shrubs', NULL)
INSERT [dbo].[tasks] ([id], [description], [due_date]) VALUES (3, N'Wash the dishes', NULL)
SET IDENTITY_INSERT [dbo].[tasks] OFF
