USE [CourtSync]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 7/23/2024 12:09:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[BookingID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[OwnerId] [int] NOT NULL,
	[SubCourtID] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[BookingDate] [date] NOT NULL,
	[TimeSlotID] [int] NOT NULL,
	[BookingType] [int] NOT NULL,
	[Amount] [decimal](10, 3) NULL,
	[Status] [int] NOT NULL,
	[PromotionCode] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CheckIn]    Script Date: 7/23/2024 12:09:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckIn](
	[CheckInID] [int] IDENTITY(1,1) NOT NULL,
	[SubCourtID] [int] NOT NULL,
	[BookingID] [int] NOT NULL,
	[CheckInTime] [datetime] NOT NULL,
	[CheckInStatus] [bit] NOT NULL,
	[UserID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CheckInID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Court]    Script Date: 7/23/2024 12:09:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Court](
	[CourtID] [int] IDENTITY(1,1) NOT NULL,
	[CourtName] [nvarchar](255) NOT NULL,
	[OwnerID] [int] NOT NULL,
	[Location] [nvarchar](255) NOT NULL,
	[Phone] [varchar](50) NULL,
	[OpeningHours] [varchar](100) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[Announcement] [nvarchar](2000) NULL,
	[PromotionID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CourtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evaluate]    Script Date: 7/23/2024 12:09:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evaluate](
	[EvaluateID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CourtID] [int] NOT NULL,
	[Rating] [int] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[EvaluateDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EvaluateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 7/23/2024 12:09:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentID] [uniqueidentifier] NOT NULL,
	[UserID] [int] NOT NULL,
	[PaymentContent] [nvarchar](max) NULL,
	[PaymentCurrency] [nvarchar](10) NULL,
	[RequiredAmount] [decimal](18, 2) NULL,
	[TotalPrice] [decimal](10, 2) NOT NULL,
	[PaymentMethod] [varchar](100) NOT NULL,
	[PaymentLanguage] [nvarchar](max) NULL,
	[PaymentStatus] [nvarchar](max) NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
	[ExpireDate] [datetime] NULL,
	[MerchantId] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 7/23/2024 12:09:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[PromotionID] [int] IDENTITY(1,1) NOT NULL,
	[PromotionCode] [varchar](50) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[Percentage] [decimal](10, 2) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[CourtID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PromotionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCourt]    Script Date: 7/23/2024 12:09:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCourt](
	[SubCourtID] [int] IDENTITY(1,1) NOT NULL,
	[CourtID] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[PricePerHour] [decimal](10, 3) NOT NULL,
	[TimeSlotID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SubCourtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeSlot]    Script Date: 7/23/2024 12:09:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSlot](
	[TimeSlotID] [int] IDENTITY(1,1) NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TimeSlotID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/23/2024 12:09:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](255) NOT NULL,
	[FullName] [varchar](100) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Phone] [varchar](50) NULL,
	[RoleType] [int] NOT NULL,
	[Otp] [nvarchar](60) NULL,
	[OtpExpiration] [datetime] NULL,
	[Verify] [int] NULL,
	[UserStatus] [int] NULL,
	[AccountBalance] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT (newid()) FOR [PaymentID]
GO
ALTER TABLE [dbo].[Promotion] ADD  DEFAULT ((0)) FOR [Percentage]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((0)) FOR [Verify]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((0)) FOR [UserStatus]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((0.00)) FOR [AccountBalance]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([OwnerId])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([PromotionCode])
REFERENCES [dbo].[Promotion] ([PromotionCode])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([SubCourtID])
REFERENCES [dbo].[SubCourt] ([SubCourtID])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([TimeSlotID])
REFERENCES [dbo].[TimeSlot] ([TimeSlotID])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[CheckIn]  WITH CHECK ADD FOREIGN KEY([BookingID])
REFERENCES [dbo].[Booking] ([BookingID])
GO
ALTER TABLE [dbo].[CheckIn]  WITH CHECK ADD FOREIGN KEY([SubCourtID])
REFERENCES [dbo].[SubCourt] ([SubCourtID])
GO
ALTER TABLE [dbo].[CheckIn]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Court]  WITH CHECK ADD FOREIGN KEY([OwnerID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Evaluate]  WITH CHECK ADD FOREIGN KEY([CourtID])
REFERENCES [dbo].[Court] ([CourtID])
GO
ALTER TABLE [dbo].[Evaluate]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD FOREIGN KEY([CourtID])
REFERENCES [dbo].[Court] ([CourtID])
GO
ALTER TABLE [dbo].[SubCourt]  WITH CHECK ADD FOREIGN KEY([CourtID])
REFERENCES [dbo].[Court] ([CourtID])
GO
ALTER TABLE [dbo].[SubCourt]  WITH CHECK ADD FOREIGN KEY([TimeSlotID])
REFERENCES [dbo].[TimeSlot] ([TimeSlotID])
GO
