USE [CourtSync]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 6/28/2024 10:53:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[BookingID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[SubCourtID] [int] NOT NULL,
	[TimeSlotID] [int] NOT NULL,
	[ScheduleID] [int] NOT NULL,
	[BookingDate] [datetime] NOT NULL,
	[BookingType] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CancellationReason] [varchar](255) NULL,
	[PaymentID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CheckIn]    Script Date: 6/28/2024 10:53:27 PM ******/
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
/****** Object:  Table [dbo].[Court]    Script Date: 6/28/2024 10:53:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Court](
	[CourtID] [int] IDENTITY(1,1) NOT NULL,
	[CourtName] [varchar](255) NOT NULL,
	[CourtManagerID] [int] NOT NULL,
	[Location] [varchar](255) NOT NULL,
	[Phone] [varchar](50) NULL,
	[OpeningHours] [varchar](100) NOT NULL,
	[Image] [varchar](max) NOT NULL,
	[Announcement] [varchar](2000) NULL,
PRIMARY KEY CLUSTERED 
(
	[CourtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evaluate]    Script Date: 6/28/2024 10:53:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evaluate](
	[EvaluateID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CourtID] [int] NOT NULL,
	[Rating] [int] NOT NULL,
	[Comment] [varchar](max) NULL,
	[EvaluateDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EvaluateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 6/28/2024 10:53:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Tax] [decimal](10, 2) NOT NULL,
	[PromotionID] [int] NULL,
	[TotalPrice] [decimal](10, 2) NOT NULL,
	[RefundAmount] [decimal](10, 2) NULL,
	[PaymentMethod] [varchar](100) NOT NULL,
	[PaymentStatus] [int] NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 6/28/2024 10:53:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[PromotionID] [int] IDENTITY(1,1) NOT NULL,
	[PromotionCode] [varchar](50) NOT NULL,
	[Description] [varchar](255) NOT NULL,
	[Percentage] [decimal](10, 2) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PromotionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 6/28/2024 10:53:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[ScheduleID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[SubCourtID] [int] NOT NULL,
	[BookingDate] [date] NOT NULL,
	[TimeSlotID] [int] NOT NULL,
	[TotalHours] [decimal](5, 2) NOT NULL,
	[BookingType] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ScheduleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCourt]    Script Date: 6/28/2024 10:53:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCourt](
	[SubCourtID] [int] IDENTITY(1,1) NOT NULL,
	[CourtID] [int] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[PricePerHour] [decimal](10, 2) NOT NULL,
	[TimeSlotID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SubCourtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeSlot]    Script Date: 6/28/2024 10:53:27 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 6/28/2024 10:53:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](255) NULL,
	[FullName] [varchar](100) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[Email] [varchar](255) NULL,
	[Phone] [varchar](50) NULL,
	[RoleType] [int] NOT NULL,
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
ALTER TABLE [dbo].[Payment] ADD  DEFAULT ((0)) FOR [RefundAmount]
GO
ALTER TABLE [dbo].[Promotion] ADD  DEFAULT ((0)) FOR [Percentage]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([PaymentID])
REFERENCES [dbo].[Payment] ([PaymentID])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([ScheduleID])
REFERENCES [dbo].[Schedule] ([ScheduleID])
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
ALTER TABLE [dbo].[Court]  WITH CHECK ADD FOREIGN KEY([CourtManagerID])
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
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([PromotionID])
REFERENCES [dbo].[Promotion] ([PromotionID])
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD FOREIGN KEY([SubCourtID])
REFERENCES [dbo].[SubCourt] ([SubCourtID])
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD FOREIGN KEY([TimeSlotID])
REFERENCES [dbo].[TimeSlot] ([TimeSlotID])
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[SubCourt]  WITH CHECK ADD FOREIGN KEY([CourtID])
REFERENCES [dbo].[Court] ([CourtID])
GO
ALTER TABLE [dbo].[SubCourt]  WITH CHECK ADD FOREIGN KEY([TimeSlotID])
REFERENCES [dbo].[TimeSlot] ([TimeSlotID])
GO

-- insert table [User]
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType) values('administrator', 'admin', 'P@ssw0rd_777', 'admin@gmail.com', '0387697532', 0);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType) values('investor999', 'Pham Quoc Duy', 'P@ssw0rd_999', 'investor777@gmail.com', '0387823694', 3);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType) values('toretto111', 'Bui Duc Trieu', 'P@ssw0rd_111', 'dominictoretto@gmail.com', '0387548251', 3);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType) values('vincenro222', 'Nguyen Phi Hung', 'P@ssw0rd_222', 'vincenro@gmail.com', '0387957473', 3);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType) values('ferrari888', 'Nguyen Trung Nam', 'P@ssw0rd_888', 'laferarri247@gmail.com', '0387123558', 3);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType) values('john_doe', 'Tran Ngoc Long', 'P@ssw0rd_666', 'mrjohn1@gmail.com', '0387123747', 1);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType) values('jane_smith', 'Tran Minh Phuc', 'P@ssw0rd_333', 'janeladdy3@gmail.com', '0387996512', 1);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType) values('maroon5', 'Nguyen Hoan', 'P@ssw0rd_3333', 'maroon5@gmail.com', '0987654321', 2);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType) values('caubengoan', 'Tran Minh Tien', 'P@ssw0rd_123', 'tientrangame@gmail.com', '0987372421', 2);

-- insert table [Court]
insert into [Court] (CourtName, CourtManagerID, [Location], Phone, OpeningHours, [Image], Announcement) values('Long River Sports, Sungai Long', 7, 'Lot 1716, Sungai Long Batu 11 Cheras, 43000', '0387996512', '05:00 - 21:00', 'https://img.courtsite.my/insecure/rs:auto:1080:0:0/g:sm/aHR0cHM6Ly9maXJlYmFzZXN0b3JhZ2UuZ29vZ2xlYXBpcy5jb20vdjAvYi9jb3VydHNpdGUtdGVycmFmb3JtLmFwcHNwb3QuY29tL28vY2VudHJlSW1hZ2VzJTJGY2x2ZDcxbXp3MGpxYzA4Y3V6YWd1NzNmOCUyRnd4NmpndWVDWjFoSWpLUnV2Q3ZiR0NtalQ0QjItNjI0MmQ1OTAtMmMyOC00YTRmLThmNWItOTJmNWE1NWVjNTYzLmpwZz9hbHQ9bWVkaWEmdG9rZW49NDE3OGQ1MzUtZTQyMy00NzQ0LTkzMGItMjM1ZmQ2MzAwNmNk.webp', 'Sorry, no activities found.
Make a booking to play at Long River Sports, Sungai Long instead!');
insert into [Court] (CourtName, CourtManagerID, [Location], Phone, OpeningHours, [Image], Announcement) values('X Park Sunway City Ipoh', 7, 'No.1, Persiaran Lagun, Sunway City, 31150 Ipoh, Perak', '0387996512', '05:00 - 21:00', 'https://img.courtsite.my/insecure/rs:auto:1080:0:0/g:sm/aHR0cHM6Ly9maXJlYmFzZXN0b3JhZ2UuZ29vZ2xlYXBpcy5jb20vdjAvYi9jb3VydHNpdGUtdGVycmFmb3JtLmFwcHNwb3QuY29tL28vY2VudHJlSW1hZ2VzJTJGY2w3NDY3aGN4MXRmdDA3OGt0dWU4aWRkMiUyRm9tMmZwcXZmdWxPOGJNaDFiWXpUeUU0aXowSzMtM2Y4Yjg1NGQtNDliOS00YTRkLWE3MjctNTljOTAxOGYyNWZlLnBuZz9hbHQ9bWVkaWEmdG9rZW49NDQ4NmE3NDUtYjNkOC00ZmM2LWJmZDktYTk5OTVmM2U0NTBj.webp', 'Sorry, no activities found.
Make a booking to play at X Park Sunway City Ipoh instead!');
insert into [Court] (CourtName, CourtManagerID, [Location], Phone, OpeningHours, [Image], Announcement) values('PJS Sports Centre', 6, 'Lot 2878, Jalan PJS 7/23, Bandar Sunway, 47500 Petaling Jaya, Selangor', '0387123747', '05:00 - 21:00', 'https://img.courtsite.my/insecure/rs:auto:1080:0:0/g:sm/aHR0cHM6Ly9maXJlYmFzZXN0b3JhZ2UuZ29vZ2xlYXBpcy5jb20vdjAvYi9jb3VydHNpdGUtdGVycmFmb3JtLmFwcHNwb3QuY29tL28vY2VudHJlSW1hZ2VzJTJGY2xnYnVkY29vMGFmdjA3N3oxanYxNXMxbiUyRmNvY3VoVE56bDdXT0FnQXQ3bnR1cWl5ZmhHSjMtYzM5MDQ0YzMtYzgxOS00YzZhLTgxOGQtYzI5MTg0NDQyZjQxLmpwZz9hbHQ9bWVkaWEmdG9rZW49ZmEwZDc5YzktMGY5NC00MTYwLWFkMjMtNmNjY2I2NDQ1OGIw.webp', 'Sorry, no activities found.
Make a booking to play at PJS Sports Centre instead!');
insert into [Court] (CourtName, CourtManagerID, [Location], Phone, OpeningHours, [Image], Announcement) values('iPSS Pickleball Courts', 6, 'Island Club Sibu Jalan Teng Chin Hua, 96000 Sibu', '0387123747', '05:00 - 21:00', 'https://img.courtsite.my/insecure/rs:auto:1080:0:0/g:sm/aHR0cHM6Ly9maXJlYmFzZXN0b3JhZ2UuZ29vZ2xlYXBpcy5jb20vdjAvYi9jb3VydHNpdGUtdGVycmFmb3JtLmFwcHNwb3QuY29tL28vY2VudHJlSW1hZ2VzJTJGY2xpdTFwdWR3ODB1NDA3OHhvZW8wdHh4NiUyRnd4NmpndWVDWjFoSWpLUnV2Q3ZiR0NtalQ0QjItNTAyZDAwZWQtZGJiOS00NDBlLTg5YjYtNWYxYjYzMDcxMGUwLmpwZz9hbHQ9bWVkaWEmdG9rZW49MGM4ZDU5Y2UtNmQ0Zi00ZTQ0LWJhMDEtMGZhMTVlYjZmNDRm.webp', 'Sorry, no activities found.
Make a booking to play at iPSS Pickleball Courts instead!');
insert into [Court] (CourtName, CourtManagerID, [Location], Phone, OpeningHours, [Image], Announcement) values('Sportizza - Home of Sports Petaling Jaya', 7, 'Lot 29E, Lorong Tandang B, Off Jalan Tandang, Section 51, 46050 Petaling Jaya, Selangor', '0387996512', '05:00 - 21:00', 'https://img.courtsite.my/insecure/rs:auto:1920:0:0/g:sm/aHR0cHM6Ly9maXJlYmFzZXN0b3JhZ2UuZ29vZ2xlYXBpcy5jb20vdjAvYi9jb3VydHNpdGUtdGVycmFmb3JtLmFwcHNwb3QuY29tL28vY2VudHJlSW1hZ2VzJTJGY2wxOHYzdDdlMWJuNjA3YTlha2FlODh2MSUyRm9tMmZwcXZmdWxPOGJNaDFiWXpUeUU0aXowSzMtOGIyMjk5YTUtYTczYi00MDg4LTg1NzgtNTM2MzIxMDI4MzY0LmpwZWc_YWx0PW1lZGlhJnRva2VuPTZhM2U3NDllLTJmNDItNGNlMC1hMGRkLTY1MTg1ZWMzMjlkMw.webp', 'Sorry, no activities found.
Make a booking to play at Sportizza - Home of Sports Petaling Jaya instead!');
insert into [Court] (CourtName, CourtManagerID, [Location], Phone, OpeningHours, [Image], Announcement) values('Yak Chee Sports Complex', 6, 'Yak Chee Sports Complex, Jalan 12, Taman Bukit Kuchai, Puchong, Selangor, Malaysia', '0387123747', '05:00 - 21:00', 'https://img.courtsite.my/insecure/rs:auto:1080:0:0/g:sm/aHR0cHM6Ly9maXJlYmFzZXN0b3JhZ2UuZ29vZ2xlYXBpcy5jb20vdjAvYi9jb3VydHNpdGUtdGVycmFmb3JtLmFwcHNwb3QuY29tL28vY2VudHJlSW1hZ2VzJTJGY2x2YThhbnpsMGd0ZDA5ZjhnZHo2a2duaSUyRlBIM0lCT3dnaVdmU09nQ2pEbkNlbW1uU0JrejEtYWIwMmFjODQtMzg3YS00OTEzLWJkYjItNzNhNjcyNzI3YzQ0LmpwZz9hbHQ9bWVkaWEmdG9rZW49Yzc1OWUzM2ItY2RhZC00ZThhLWJiZjAtNzkzZmI5YjI4YmIw.webp','Sorry, no activities found.
Make a booking to play at Yak Chee Sports Complex instead!');
insert into [Court] (CourtName, CourtManagerID, [Location], Phone, OpeningHours, [Image], Announcement) values('Tunas Badminton Hall (F.K.A. True Strike Sports Centre)', 7, 'Lot PT 3962 & 3963, Jln Haruan 2, Pusat Komersial Oakland', '0387996512', '05:00 - 21:00', 'https://img.courtsite.my/insecure/rs:auto:1080:0:0/g:sm/aHR0cHM6Ly9maXJlYmFzZXN0b3JhZ2UuZ29vZ2xlYXBpcy5jb20vdjAvYi9jb3VydHNpdGUtdGVycmFmb3JtLmFwcHNwb3QuY29tL28vY2VudHJlSW1hZ2VzJTJGY2x0NTV2NXJyMGFtNDJ2ZnB4eGxsMzZyOSUyRnd4NmpndWVDWjFoSWpLUnV2Q3ZiR0NtalQ0QjItMjhmYTAyNGYtZjViOS00YzQ3LThkMjctNDZiZjA2NmQ3ZDIzLmpwZz9hbHQ9bWVkaWEmdG9rZW49MzMyMmM2ZDEtM2MxZS00OThlLThlMGEtOTY4OGU0NTY1ZDY1.webp', 'Sorry, no activities found.
Make a booking to play at Tunas Badminton Hall (F.K.A. True Strike Sports Centre) instead!')

--insert table [TimeSlot]
insert into [TimeSlot] (StartTime, EndTime) values('05:00:00', '07:00:00');
insert into [TimeSlot] (StartTime, EndTime) values('07:00:00', '09:00:00');
insert into [TimeSlot] (StartTime, EndTime) values('09:00:00', '11:00:00');
insert into [TimeSlot] (StartTime, EndTime) values('13:00:00', '15:00:00');
insert into [TimeSlot] (StartTime, EndTime) values('15:00:00', '17:00:00');
insert into [TimeSlot] (StartTime, EndTime) values('17:00:00', '19:00:00');
insert into [TimeSlot] (StartTime, EndTime) values('19:00:00', '21:00:00');

--insert table [SubCourt]
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, 'Court 1', 35, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, 'Court 1', 35, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, 'Court 1', 35, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, 'Court 1', 35, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, 'Court 1', 35, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, 'Court 1', 35, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, 'Court 1', 35, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, 'Court 2', 55, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, 'Court 2', 55, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, 'Court 2', 55, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, 'Court 2', 55, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, 'Court 2', 55, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, 'Court 2', 55, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, 'Court 2', 55, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, 'Court 1', 65, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, 'Court 1', 65, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, 'Court 1', 65, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, 'Court 1', 65, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, 'Court 1', 65, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, 'Court 1', 65, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, 'Court 1', 65, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, 'Court 2', 50, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, 'Court 2', 50, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, 'Court 2', 50, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, 'Court 2', 50, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, 'Court 2', 50, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, 'Court 2', 50, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, 'Court 2', 50, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, 'Court 1', 70, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, 'Court 1', 70, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, 'Court 1', 70, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, 'Court 1', 70, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, 'Court 1', 70, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, 'Court 1', 70, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, 'Court 1', 70, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, 'Court 2', 60, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, 'Court 2', 60, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, 'Court 2', 60, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, 'Court 2', 60, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, 'Court 2', 60, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, 'Court 2', 60, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, 'Court 2', 60, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, 'Court 1', 75, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, 'Court 1', 75, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, 'Court 1', 75, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, 'Court 1', 75, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, 'Court 1', 75, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, 'Court 1', 75, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, 'Court 1', 75, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, 'Court 2', 70, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, 'Court 2', 70, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, 'Court 2', 70, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, 'Court 2', 70, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, 'Court 2', 70, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, 'Court 2', 70, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, 'Court 2', 70, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, 'Court 1', 80, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, 'Court 1', 80, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, 'Court 1', 80, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, 'Court 1', 80, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, 'Court 1', 80, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, 'Court 1', 80, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, 'Court 1', 80, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, 'Court 2', 45, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, 'Court 2', 45, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, 'Court 2', 45, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, 'Court 2', 45, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, 'Court 2', 45, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, 'Court 2', 45, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, 'Court 2', 45, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, 'Court 1', 50, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, 'Court 1', 50, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, 'Court 1', 50, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, 'Court 1', 50, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, 'Court 1', 50, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, 'Court 1', 50, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, 'Court 1', 50, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, 'Court 2', 60, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, 'Court 2', 60, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, 'Court 2', 60, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, 'Court 2', 60, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, 'Court 2', 60, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, 'Court 2', 60, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, 'Court 2', 60, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, 'Court 1', 50, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, 'Court 1', 50, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, 'Court 1', 50, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, 'Court 1', 50, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, 'Court 1', 50, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, 'Court 1', 50, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, 'Court 1', 50, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, 'Court 2', 80, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, 'Court 2', 80, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, 'Court 2', 80, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, 'Court 2', 80, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, 'Court 2', 80, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, 'Court 2', 80, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, 'Court 2', 80, 7);

--insert Table [Schedule]
insert into [Schedule] (UserID, SubCourtID, BookingDate, TimeSlotID, TotalHours, BookingType) values(2, 1, '2024-05-01', 1, 2.00, 0);
insert into [Schedule] (UserID, SubCourtID, BookingDate, TimeSlotID, TotalHours, BookingType) values(2, 2, '2024-12-02', 3, 2.00, 0);
insert into [Schedule] (UserID, SubCourtID, BookingDate, TimeSlotID, TotalHours, BookingType) values(3, 3, '2024-08-06', 4, 2.00, 2);
insert into [Schedule] (UserID, SubCourtID, BookingDate, TimeSlotID, TotalHours, BookingType) values(8, 1, '2024-05-07', 2, 2.00, 0);
insert into [Schedule] (UserID, SubCourtID, BookingDate, TimeSlotID, TotalHours, BookingType) values(8, 4, '2024-05-01', 7, 2.00, 2);
insert into [Schedule] (UserID, SubCourtID, BookingDate, TimeSlotID, TotalHours, BookingType) values(4, 6, '2024-02-01', 4, 2.00, 1);
insert into [Schedule] (UserID, SubCourtID, BookingDate, TimeSlotID, TotalHours, BookingType) values(4, 2, '2024-07-23', 7, 2.00, 0);
insert into [Schedule] (UserID, SubCourtID, BookingDate, TimeSlotID, TotalHours, BookingType) values(5, 9, '2024-03-18', 2, 2.00, 1);
insert into [Schedule] (UserID, SubCourtID, BookingDate, TimeSlotID, TotalHours, BookingType) values(8, 7, '2024-02-04', 4, 2.00, 0);
insert into [Schedule] (UserID, SubCourtID, BookingDate, TimeSlotID, TotalHours, BookingType) values(4, 5, '2024-04-07', 1, 2.00, 2);

--insert Table [Promotion]
insert into [Promotion] (PromotionCode, [Description], [Percentage], StartDate, EndDate) values('MSMZ4', 'Use code MSMZ4 to get an instant 10% discount on your badminton court booking! Dont miss this chance to enjoy exciting matches with friends and family', 10, '2024-06-01', '2024-08-31');
insert into [Promotion] (PromotionCode, [Description], [Percentage], StartDate, EndDate) values('XPAAD', 'Book a badminton court today and use code XPAAD to get 20% off your total cost! This is a great chance to have a fantastic time playing and practicing with your friends', 20, '2024-12-01', '2025-02-08');

--insert Table [Payment]
insert into [Payment] (UserID, Tax, PromotionID, TotalPrice, RefundAmount, PaymentMethod, PaymentStatus, PaymentDate) values(2, 10, 1, 89.00, 0.00,'VNPay', 1, '2024-07-01 17:24:35');
insert into [Payment] (UserID, Tax, PromotionID, TotalPrice, RefundAmount, PaymentMethod, PaymentStatus, PaymentDate) values(4, 10, NULL, 88.00, 0.00,'VNPay', 0, '2024-08-02 06:53:23');

--insert Table [Booking]
insert into [Booking] (UserID, SubCourtID, TimeSlotID, ScheduleID, BookingDate, BookingType, [Status], CancellationReason, PaymentID) values(2, 1, 1, 1, '2024-05-01 05:01:23', 1, 1, '', 1);
insert into [Booking] (UserID, SubCourtID, TimeSlotID, ScheduleID, BookingDate, BookingType, [Status], CancellationReason, PaymentID) values(4, 2, 2, 2, '2024-12-02 07:03:12', 0, 1, '', 2);

--insert Table [CheckIn]
insert into [CheckIn] (SubCourtID, BookingID, CheckInTime, CheckInStatus, UserID) values(2, 1, '2024-05-01 05:00:00', 1, 8);
insert into [CheckIn] (SubCourtID, BookingID, CheckInTime, CheckInStatus, UserID) values(1, 2, '2024-12-02 09:00:00', 0, 9);

--insert Table [Evaluate]
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate) values(2, 1, 5, 'Great experiences!', '2024-05-03 19:12:35');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate) values(4, 2, 4, 'Good yard, but a bit expensive', '2024-05-07 14:32:42');
