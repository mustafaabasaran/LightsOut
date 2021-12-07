USE [LightsOut]
GO
/****** Object:  Table [dbo].[BoardSetting]    Script Date: 12/7/2021 9:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BoardSetting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Size] [int] NULL,
	[OnColor] [varchar](250) NULL,
	[OffColor] [varchar](250) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InitialState]    Script Date: 12/7/2021 9:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InitialState](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Row] [int] NULL,
	[Column] [int] NULL,
	[State] [tinyint] NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BoardSetting] ON 
GO
INSERT [dbo].[BoardSetting] ([Id], [Size], [OnColor], [OffColor]) VALUES (1, 5, N'#0300ff', N'#000000')
GO
SET IDENTITY_INSERT [dbo].[BoardSetting] OFF
GO
SET IDENTITY_INSERT [dbo].[InitialState] ON 
GO
INSERT [dbo].[InitialState] ([Id], [Row], [Column], [State]) VALUES (1, 0, 0, 1)
GO
INSERT [dbo].[InitialState] ([Id], [Row], [Column], [State]) VALUES (2, 0, 2, 1)
GO
INSERT [dbo].[InitialState] ([Id], [Row], [Column], [State]) VALUES (3, 2, 2, 1)
GO
INSERT [dbo].[InitialState] ([Id], [Row], [Column], [State]) VALUES (4, 3, 2, 1)
GO
INSERT [dbo].[InitialState] ([Id], [Row], [Column], [State]) VALUES (5, 4, 2, 1)
GO
SET IDENTITY_INSERT [dbo].[InitialState] OFF
GO
