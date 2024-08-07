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
-----------------------------------------------------------------------------------
-- insert table [User]
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType, Verify) values('administrator', 'admin', 'P@ssw0rd_777', 'admin@gmail.com', '0387697532', 0, 4);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType, Verify, UserStatus) values('investor999', 'Pham Quoc Duy', 'P@ssw0rd_999', 'investor777@gmail.com', '0387823694', 3, 4, 0);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType, Verify, UserStatus) values('toretto111', 'Bui Duc Trieu', 'P@ssw0rd_111', 'dominictoretto@gmail.com', '0387548251', 3, 4, 0);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType, Verify, UserStatus) values('vincenro222', 'Nguyen Phi Hung', 'P@ssw0rd_222', 'vincenro@gmail.com', '0387957473', 3, 4, 0);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType, Verify, UserStatus) values('ferrari888', 'Nguyen Trung Nam', 'P@ssw0rd_888', 'laferarri247@gmail.com', '0387123558', 3, 4, 0);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType, Verify, UserStatus) values('john_doe', 'Tran Ngoc Long', 'P@ssw0rd_666', 'mrjohn1@gmail.com', '0387123747', 1, 4, 0);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType, Verify, UserStatus) values('jane_smith', 'Tran Minh Phuc', 'P@ssw0rd_333', 'janeladdy3@gmail.com', '0387996512', 1, 4, 0);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType, Verify, UserStatus) values('maroon5', 'Nguyen Hoan', 'P@ssw0rd_3333', 'maroon5@gmail.com', '0987654321', 2, 4, 0);
insert into [User] (UserName, [FullName], [Password], Email, Phone, RoleType, Verify, UserStatus) values('caubengoan', 'Tran Minh Tien', 'P@ssw0rd_123', 'tientrangame@gmail.com', '0987372421', 2, 4, 0);
-------------------------------------------------------------------------------------
UPDATE [User] SET AccountBalance = 1000000 WHERE UserName = 'investor999';
UPDATE [User] SET AccountBalance = 1000000 WHERE UserName = 'toretto111';
UPDATE [User] SET AccountBalance = 1000000 WHERE UserName = 'vincenro222';
UPDATE [User] SET AccountBalance = 1000000 WHERE UserName = 'ferrari888';
UPDATE [User] SET AccountBalance = 1000000 WHERE UserName = 'john_doe';
UPDATE [User] SET AccountBalance = 1000000 WHERE UserName = 'jane_smith';
UPDATE [User] SET AccountBalance = 1000000 WHERE UserName = 'maroon5';
UPDATE [User] SET AccountBalance = 1000000 WHERE UserName = 'caubengoan';
---------------------------------------------
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông quận 1 - Cầu Kho', 7, N'Số 2 Nguyễn Văn Cừ, Quận 1, TP. Hồ Chí Minh', '0837401234', '05:00 - 21:00', N'37ecefc9-f37c-4554-bfee-bab686d149c4.png, 07299dd5-c4d1-4a1d-9199-55ca10f313d6.png, be92300d-b1d1-47a6-96f8-10a443aafc53.png, 7a8a89e0-2e19-4a4a-bce2-30cd666fe41f.png, 60757052-e08c-45ef-8bb7-9feed8650503.png', N'Đặt chỗ ngay bây giờ!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông Cung Văn hóa Lao động ', 6, N'Cung Văn hóa Lao Động, đường Nguyễn Thị Minh Khai, Quận 1, TP. Hồ Chí Minh', '0283930181', '05:00 - 21:00', N'ead8ebe3-3ea5-4a2f-ba70-2d6255735603.png, 88964897-34e0-4780-b6ec-e47d666b188f.png, d5f4b2dc-1694-408e-9e36-9cfacaeeb42c.png, 5de7ab5a-f3bb-4a1a-b4db-242715bb16c1.png, 2bb08f7d-92bb-4a1d-a19c-8713e61f76c3.png', N'Tham gia các giải đấu hàng tuần của chúng tôi!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông quận 2 - Bình Trưng', 7, N'41 đường số 41, Phường Bình Trưng Tây, Quận 2, Hồ Chí Minh, Việt Nam', '0837401234', '05:00 - 21:00', N'5fd9a26e-28ba-4709-a5d9-14205c1a9414.png, c1c4eb7d-2ba9-40c2-8417-ab73d0ab1d87.png, b75a129b-83ef-4074-8fc0-ac281cab6b13.png, 1cdcef60-1a9c-4528-810e-19ba6dd6297f.png, 27ee1f14-18c6-479c-ac79-79cc850557ff.png', N'Cùng nhau rèn luyện sức khỏe!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông Đông Phương', 6, N'873 đường số 47, Nguyễn Duy Trinh, Quận 2, TP Hồ Chí Minh', '0283930181', '05:00 - 21:00', N'e6e2a581-dd77-4bfb-ba12-721db48f6152.png, 5fad2a45-9497-408a-b55f-3accfdb684a6.png, 8b199773-c383-49e2-b063-5879d95f1498.png, c0a2435a-af76-47fd-bc39-2af4b5789985.png, d48d7a68-838a-4976-a510-51421d209547.png', N'Đăng ký khóa học ngay hôm nay!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông HBA Sport Center', 7, N'789 Võ Thị Sáu, Quận 3, TP. Hồ Chí Minh', '0837401234', '05:00 - 21:00', N'22ce2e8a-526f-4bcc-bf04-a58dd26a23f9.png, c3fc34de-66dc-41a1-b07e-e95e666c3e81.png, 4b851c71-dd7c-4359-b327-9ffe738d93f4.png, 45726501-7fef-4d8a-a49a-95494721ef8d.png, 65138dc8-a822-4a41-815e-75e60e93add2.png', N'Khuyến mãi đặc biệt cho thành viên mới!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông Đầu Máy Xe Lửa', 6, N'540/21 Cách Mạng Tháng 8, phường 11, Quận 3, Hồ Chí Minh', '0283930181', '05:00 - 21:00', N'8e799804-0a8b-4845-b8c2-3ec4e569a102.png, 9ee4a155-6c22-4f33-b7b8-d446dbd91206.png, ef27f72b-6cce-4b56-847d-cf88ead5aa8c.png, 5b6600da-7fd3-4a5c-8552-aea7d18c06d8.png, 66521bd4-4bd0-427c-ac1f-440fec0b5e8e.png', N'Tham gia ngay hôm nay!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông Tân Cảng', 7, N'Số 122 Đường Khánh Hội, Phường 3, Quận 4, Thành phố Hồ Chí Minh', '0837401234', '05:00 - 21:00', N'9973bfb9-a83f-4fae-9339-99f0d96ae496.png, 85217bb4-345d-449f-b8f4-ec5412cdb0c9.png, 08a99bf8-7290-417a-8147-6bdbde8d88fe.png, d2172cd5-2da2-45cc-b58e-832abbb910cb.png, 56284ce0-3fad-43ff-95a4-f94064ab8a3a.png', N'Đặt chỗ trực tuyến tiện lợi!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông Lavie', 6, N'Số 458B Nguyễn Tất Thành, Phường 18, Quận 4, thành phố Hồ Chí Minh', '0283930181', '05:00 - 21:00', N'362960d2-cab3-42c6-b592-a5b604afbad1.jpg, 727d2850-f3f2-4f01-adb8-8f9ed0118171.jpg, f1e0ea29-2a1a-4ac1-96de-186fe24c8c08.jpg, ca6b7c86-4e4f-4e87-8a96-9f6b9e77ca42.jpg, 2ac08afc-e54d-49fc-9c56-167dfe5b8a74.png', N'Tham gia giải đấu cuối tuần!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông Lê Hồng Phong', 7, N'200 Trần Hưng Đạo, Quận 5, TP. Hồ Chí Minh', '0837401234', '05:00 - 21:00', N'62fe9fe1-b11d-4662-9c45-2273ded4c866.jpg, c8334e17-e4d8-475c-a363-717c83b24313.jpg, 571eeb17-bbb3-41f6-a5ae-1ea82a67b3aa.jpg, 74fa208e-dcf0-4977-8ed6-c2460a2dcf78.jpg, 358bdc82-d8cc-4547-ae6c-70a31d901d66.png', N'Ưu đãi cho nhóm đặt sân!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông Châu Văn Liêm', 6, N'Số 235 Nguyễn Văn Cừ, phường 4, Quận 5, TP. Hồ Chí Minh', '0283930181', '05:00 - 21:00', N'ee580ddd-8059-4636-bbf8-d196093422f6.jpg, 672b5a75-4ff7-4f0d-9aee-dc604e14210b.jpg, 308b4fae-d78c-4da0-8e10-a840f73421af.jpg, 38b6f15d-1bc7-49d1-b3f4-61ef41b5d472.jpg, 533931c5-a0c5-44cf-b14e-ce7b05760470.png', N'Đăng ký thành viên để nhận nhiều ưu đãi!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông Hoàng Phi ', 7, N'477 An D. Vương, Phường 11, Quận 6, Thành phố Hồ Chí Minh', '0837401234', '05:00 - 21:00', N'd4be1ac9-d0bc-42c3-99bb-ac3ecc4343bb.jpg, 13b9b32f-362b-4d75-9cea-1b4b24807c60.jpg, 486003ba-341d-4e12-a9d5-8587f953979a.jpg, 34e36cf8-2fa3-4833-adb0-b7d670898b45.jpg, 224d5c43-cd95-4da4-94f1-828e79de6158.png', N'Chương trình giảm giá đặc biệt!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông CM Sports', 6, N'353 An D. Vương, Phường 10, Quận 6, Thành phố Hồ Chí Minh', '0283930181', '05:00 - 21:00', N'df88ecd8-e087-491a-afc8-106395f13dfc.jpg, d39b7ab3-0540-46c8-811d-7c2a163fe72a.jpg, 5b49a088-5831-4ed3-b99a-8e426b968237.jpg, 2a05e17e-4510-471f-bee0-44270ca87941.jpg, ff3cb079-59b9-4d2c-8cc1-1813b46d9b18.png', N'Đặt chỗ ngay bây giờ!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân Cầu Lông Cảng Bến Nghé', 7, N'Cảng Bến Nghé, P. Tân Thuận Đông, Quận 7, TP. Hồ Chí Minh', '0837401234', '05:00 - 21:00', N'88d8fbc8-45b2-4fe2-bf24-2c7a91a4708b.jpg, 08feefc1-14a9-4163-9731-af93ecc1cd78.jpg, d519a905-5b36-4a94-a6f6-f63c97e329f7.jpg, 8ef5c56b-4532-4034-b784-a26b0ce4be30.jpg, 716474a4-5709-4d62-adac-9102a8e221ee.png', N'Tham gia các lớp học cầu lông!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông Tân Phong', 6, N'202 Nguyễn Thị Thập, Quận 7, TP. Hồ Chí Minh', '0283930181', '05:00 - 21:00', N'e52b9b96-0d0d-477b-b893-c9a35141f99b.jpg, 3f9a2aa9-b90a-4a69-b60f-5c4fa8e63daf.jpg, c8877e11-7517-404f-b326-f212b78d75d9.jpg, 71acb557-4a06-44ac-8f2c-2b44bfd3bbe3.jpg, b129d650-1ccb-4e57-9609-ec7feeb70df2.png', N'Đặt chỗ dễ dàng và nhanh chóng!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân thể thao Phạm Thế Hiển', 7, N'150 Phạm Thế Hiển, Quận 8, TP. Hồ Chí Minh', '0837401234', '06:00 - 22:00', N'71760af0-4397-4105-b195-0d2c85b2f7ce.jpg, b0d8b1d6-25c4-4625-a4cf-4f7ff77a0fa6.png, ea20f684-8fa2-4e95-b718-cd714ac0fae9.jpg, b6f46419-c259-4836-bead-7d695c1a900d.jpg, e4627a60-29f9-4bd4-921f-a36cd844ee17.png', N'Giảm giá cho sinh viên!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông Tạ Quang Bửu', 6, N'200 Tạ Quang Bửu, Quận 8, TP. Hồ Chí Minh', '0283930181', '05:00 - 21:00', N'b1c09d71-1a8f-4318-ab51-7becb903e042.jpg, 3d904088-a8b9-4868-8e6e-155757dacc7f.jpg, b24d0966-69aa-4857-b12c-103e50f9ea12.jpg, 79e0f63a-31ca-4097-a275-17e3a6efff45.jpg, 2347aaca-e2d2-4b42-aaf4-0aa9d1e6ddd1.png', N'Tham gia câu lạc bộ cầu lông!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân thể thao Hoàng Hữu Nam', 7, N'300 Hoàng Hữu Nam, Quận 9, TP. Hồ Chí Minh', '0837401234', '05:00 - 21:00', N'92a12849-af1e-4747-bf61-ba10c12d115a.png, 3721be19-86bc-4602-8494-4a4997b5ac99.jpg, bd4e9db7-b2ad-4136-b26b-a9c44734727c.jpg, ed8c82a2-5f27-49c4-b33d-c65edafe44b4.jpg, 363bae31-baa2-4ebd-906d-c5b423f19eb8.jpg', N'Đăng ký thành viên để nhận nhiều ưu đãi!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông Long Bình', 6, N'400 Long Bình, Quận 9, TP. Hồ Chí Minh', '0283930181', '05:00 - 21:00', N'8973d278-052e-48bb-9e88-366ad8319b90.jpg, dbed457d-590a-41d2-bfeb-5443f2a41770.jpg, 9684905c-ff9d-420d-8348-ac677a57428a.jpg, 86cacfb1-74ce-45f3-863c-05821d1c122d.jpg, c79ac707-38fc-4c08-b12e-bca94271afc4.png', N'Cùng nhau rèn luyện sức khỏe!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông Kỳ Hòa', 7, N'238 đường 3 Tháng 2, Phường 12, Quận 10, TPHCM', '0837401234', '05:00 - 21:00', N'582eb20e-a6b1-4837-b973-16dad8984d8a.jpg, 18a0283d-ac75-42d0-92ae-95b98f8c1414.jpg, 2deb5f2a-a67b-463c-b078-3fccc0e7f1d2.jpg, 4827c852-b82a-4742-b2ac-3633ae9e737d.jpg, 9aeacc6f-ee8b-46e3-8c87-1b96fa1b09cb.png', N'Khuyến mãi đặc biệt cho thành viên mới!');
INSERT INTO [Court] (CourtName, OwnerID, [Location], Phone, OpeningHours, [Image], Announcement) 
VALUES (N'Sân cầu lông 291', 6, N'291 Hẻm 285 Cách Mạng Tháng Tám, Phường 12, Quận 10, Thành phố Hồ Chí Minh', '0283930181', '05:00 - 21:00', N'15aca379-e0da-4c9d-b5c6-7e0eff6aa04d.jpg, 3b77bb01-62ab-4758-92df-ce5e9756ad26.jpg, fff429d1-57c1-4293-9e0d-49cc7e3ac6d8.jpg, cd5c3ca8-46cf-46cc-bfd4-f78f4ac2f250.jpg, 930d9e16-0b22-494d-8edb-9abf148438b4.png', N'Đặt chỗ trực tuyến tiện lợi!');

--insert table [TimeSlot]
insert into [TimeSlot] (StartTime, EndTime) values('05:00:00', '07:00:00');
insert into [TimeSlot] (StartTime, EndTime) values('07:00:00', '09:00:00');
insert into [TimeSlot] (StartTime, EndTime) values('09:00:00', '11:00:00');
insert into [TimeSlot] (StartTime, EndTime) values('13:00:00', '15:00:00');
insert into [TimeSlot] (StartTime, EndTime) values('15:00:00', '17:00:00');
insert into [TimeSlot] (StartTime, EndTime) values('17:00:00', '19:00:00');
insert into [TimeSlot] (StartTime, EndTime) values('19:00:00', '21:00:00');

--insert table [SubCourt]
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ A', 157.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ A', 157.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ A', 157.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ A', 157.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ A', 157.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ A', 157.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ A', 157.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ B', 191.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ B', 191.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ B', 191.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ B', 191.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ B', 191.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ B', 191.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ B', 191.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ C', 160.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ C', 160.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ C', 160.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ C', 160.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ C', 160.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ C', 160.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ C', 160.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ D', 22.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ D', 22.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ D', 22.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ D', 22.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ D', 22.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ D', 22.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(1, N'Sân nhỏ D', 22.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ A', 30.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ A', 30.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ A', 30.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ A', 30.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ A', 30.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ A', 30.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ A', 30.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ B', 164.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ B', 164.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ B', 164.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ B', 164.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ B', 164.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ B', 164.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ B', 164.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ C', 55.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ C', 55.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ C', 55.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ C', 55.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ C', 55.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ C', 55.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ C', 55.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ D', 69.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ D', 69.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ D', 69.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ D', 69.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ D', 69.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ D', 69.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(2, N'Sân nhỏ D', 69.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ A', 70.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ A', 70.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ A', 70.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ A', 70.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ A', 70.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ A', 70.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ A', 70.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ B', 60.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ B', 60.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ B', 60.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ B', 60.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ B', 60.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ B', 60.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ B', 60.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ C', 156.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ C', 156.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ C', 156.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ C', 156.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ C', 156.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ C', 156.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ C', 156.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ D', 165.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ D', 165.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ D', 165.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ D', 165.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ D', 165.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ D', 165.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(3, N'Sân nhỏ D', 165.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ A', 75.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ A', 75.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ A', 75.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ A', 75.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ A', 75.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ A', 75.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ A', 75.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ B', 70.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ B', 70.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ B', 70.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ B', 70.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ B', 70.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ B', 70.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ B', 70.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ C', 79.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ C', 79.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ C', 79.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ C', 79.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ C', 79.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ C', 79.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ C', 79.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ D', 89.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ D', 89.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ D', 89.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ D', 89.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ D', 89.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ D', 89.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(4, N'Sân nhỏ D', 89.000, 7);


insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ A', 80.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ A', 80.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ A', 80.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ A', 80.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ A', 80.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ A', 80.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ A', 80.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ B', 45.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ B', 45.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ B', 45.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ B', 45.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ B', 45.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ B', 45.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ B', 45.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ C', 55.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ C', 55.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ C', 55.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ C', 55.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ C', 55.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ C', 55.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ C', 55.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ D', 68.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ D', 68.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ D', 68.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ D', 68.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ D', 68.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ D', 68.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(5, N'Sân nhỏ D', 68.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ A', 50.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ A', 50.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ A', 50.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ A', 50.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ A', 50.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ A', 50.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ A', 50.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ B', 60.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ B', 60.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ B', 60.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ B', 60.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ B', 60.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ B', 60.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ B', 60.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ C', 70.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ C', 70.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ C', 70.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ C', 70.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ C', 70.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ C', 70.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ C', 70.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ D', 80.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ D', 80.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ D', 80.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ D', 80.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ D', 80.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ D', 80.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(6, N'Sân nhỏ D', 80.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ A', 50.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ A', 50.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ A', 50.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ A', 50.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ A', 50.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ A', 50.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ A', 50.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ B', 80.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ B', 80.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ B', 80.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ B', 80.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ B', 80.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ B', 80.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ B', 80.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ C', 88.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ C', 88.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ C', 88.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ C', 88.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ C', 88.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ C', 88.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ C', 88.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ D', 68.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ D', 68.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ D', 68.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ D', 68.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ D', 68.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ D', 68.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(7, N'Sân nhỏ D', 68.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ A', 110.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ A', 110.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ A', 110.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ A', 110.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ A', 110.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ A', 110.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ A', 110.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ B', 92.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ B', 92.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ B', 92.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ B', 92.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ B', 92.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ B', 92.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ B', 92.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ C', 102.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ C', 102.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ C', 102.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ C', 102.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ C', 102.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ C', 102.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ C', 102.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ D', 64.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ D', 64.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ D', 64.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ D', 64.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ D', 64.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ D', 64.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(8, N'Sân nhỏ D', 64.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ A', 128.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ A', 128.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ A', 128.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ A', 128.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ A', 128.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ A', 128.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ A', 128.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ B', 126.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ B', 126.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ B', 126.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ B', 126.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ B', 126.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ B', 126.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ B', 126.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ C', 33.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ C', 33.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ C', 33.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ C', 33.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ C', 33.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ C', 33.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ C', 33.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ D', 105.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ D', 105.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ D', 105.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ D', 105.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ D', 105.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ D', 105.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(9, N'Sân nhỏ D', 105.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ A', 30.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ A', 30.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ A', 30.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ A', 30.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ A', 30.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ A', 30.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ A', 30.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ B', 142.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ B', 142.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ B', 142.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ B', 142.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ B', 142.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ B', 142.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ B', 142.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ C', 98.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ C', 98.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ C', 98.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ C', 98.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ C', 98.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ C', 98.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ C', 98.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ D', 123.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ D', 123.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ D', 123.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ D', 123.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ D', 123.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ D', 123.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(10, N'Sân nhỏ D', 123.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ A', 63.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ A', 63.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ A', 63.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ A', 63.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ A', 63.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ A', 63.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ A', 63.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ B', 20.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ B', 20.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ B', 20.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ B', 20.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ B', 20.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ B', 20.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ B', 20.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ C', 38.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ C', 38.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ C', 38.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ C', 38.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ C', 38.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ C', 38.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ C', 38.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ D', 165.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ D', 165.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ D', 165.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ D', 165.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ D', 165.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ D', 165.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(11, N'Sân nhỏ D', 165.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ A', 187.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ A', 187.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ A', 187.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ A', 187.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ A', 187.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ A', 187.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ A', 187.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ B', 171.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ B', 171.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ B', 171.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ B', 171.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ B', 171.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ B', 171.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ B', 171.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ C', 98.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ C', 98.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ C', 98.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ C', 98.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ C', 98.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ C', 98.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ C', 98.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ D', 173.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ D', 173.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ D', 173.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ D', 173.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ D', 173.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ D', 173.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(12, N'Sân nhỏ D', 173.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ A', 193.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ A', 193.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ A', 193.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ A', 193.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ A', 193.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ A', 193.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ A', 193.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ B', 89.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ B', 89.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ B', 89.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ B', 89.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ B', 89.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ B', 89.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ B', 89.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ C', 184.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ C', 184.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ C', 184.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ C', 184.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ C', 184.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ C', 184.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ C', 184.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ D', 68.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ D', 68.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ D', 68.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ D', 68.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ D', 68.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ D', 68.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(13, N'Sân nhỏ D', 68.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ A', 77.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ A', 77.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ A', 77.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ A', 77.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ A', 77.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ A', 77.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ A', 77.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ B', 173.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ B', 173.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ B', 173.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ B', 173.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ B', 173.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ B', 173.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ B', 173.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ C', 96.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ C', 96.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ C', 96.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ C', 96.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ C', 96.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ C', 96.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ C', 96.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ D', 178.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ D', 178.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ D', 178.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ D', 178.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ D', 178.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ D', 178.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(14, N'Sân nhỏ D', 178.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ A', 115.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ A', 115.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ A', 115.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ A', 115.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ A', 115.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ A', 115.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ A', 115.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ B', 39.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ B', 39.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ B', 39.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ B', 39.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ B', 39.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ B', 39.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ B', 39.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ C', 138.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ C', 138.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ C', 138.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ C', 138.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ C', 138.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ C', 138.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ C', 138.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ D', 122.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ D', 122.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ D', 122.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ D', 122.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ D', 122.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ D', 122.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(15, N'Sân nhỏ D', 122.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ A', 72.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ A', 72.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ A', 72.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ A', 72.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ A', 72.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ A', 72.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ A', 72.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ B', 60.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ B', 60.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ B', 60.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ B', 60.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ B', 60.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ B', 60.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ B', 60.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ C', 87.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ C', 87.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ C', 87.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ C', 87.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ C', 87.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ C', 87.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ C', 87.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ D', 118.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ D', 118.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ D', 118.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ D', 118.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ D', 118.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ D', 118.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(16, N'Sân nhỏ D', 118.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ A', 178.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ A', 178.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ A', 178.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ A', 178.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ A', 178.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ A', 178.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ A', 178.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ B', 21.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ B', 21.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ B', 21.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ B', 21.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ B', 21.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ B', 21.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ B', 21.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ C', 25.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ C', 25.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ C', 25.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ C', 25.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ C', 25.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ C', 25.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ C', 25.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ D', 139.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ D', 139.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ D', 139.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ D', 139.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ D', 139.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ D', 139.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(17, N'Sân nhỏ D', 139.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ A', 182.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ A', 182.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ A', 182.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ A', 182.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ A', 182.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ A', 182.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ A', 182.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ B', 123.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ B', 123.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ B', 123.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ B', 123.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ B', 123.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ B', 123.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ B', 123.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ C', 139.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ C', 139.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ C', 139.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ C', 139.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ C', 139.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ C', 139.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ C', 139.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ D', 83.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ D', 83.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ D', 83.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ D', 83.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ D', 83.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ D', 83.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(18, N'Sân nhỏ D', 83.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ A', 126.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ A', 126.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ A', 126.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ A', 126.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ A', 126.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ A', 126.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ A', 126.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ B', 130.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ B', 130.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ B', 130.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ B', 130.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ B', 130.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ B', 130.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ B', 130.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ C', 142.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ C', 142.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ C', 142.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ C', 142.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ C', 142.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ C', 142.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ C', 142.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ D', 136.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ D', 136.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ D', 136.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ D', 136.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ D', 136.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ D', 136.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(19, N'Sân nhỏ D', 136.000, 7);

insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ A', 76.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ A', 76.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ A', 76.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ A', 76.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ A', 76.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ A', 76.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ A', 76.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ B', 200.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ B', 200.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ B', 200.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ B', 200.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ B', 200.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ B', 200.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ B', 200.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ C', 104.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ C', 104.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ C', 104.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ C', 104.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ C', 104.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ C', 104.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ C', 104.000, 7);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ D', 197.000, 1);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ D', 197.000, 2);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ D', 197.000, 3);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ D', 197.000, 4);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ D', 197.000, 5);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ D', 197.000, 6);
insert into [SubCourt] (CourtID, [Name], PricePerHour, TimeSlotID) values(20, N'Sân nhỏ D', 197.000, 7);

--insert Table [Promotion]
insert into [Promotion] (PromotionCode, [Description], [Percentage], StartDate, EndDate, CourtID)
values('MSMZ4', N'Sử dụng mã MSMZ4 để được giảm giá ngay 10% khi đặt sân cầu lông! Đừng bỏ lỡ cơ hội này để thưởng thức những trận đấu thú vị cùng bạn bè và gia đình', 10, '2024-06-01', '2024-08-31', 1);
insert into [Promotion] (PromotionCode, [Description], [Percentage], StartDate, EndDate, CourtID)
values('XPAAD', N'Đặt sân cầu lông ngay hôm nay và sử dụng mã XPAAD để được giảm 20% tổng chi phí! Đây là cơ hội tuyệt vời để bạn có khoảng thời gian vui chơi và luyện tập tuyệt vời cùng bạn bè', 20, '2024-12-01', '2025-02-08', 2);

-- Insert vào bảng [Booking]
INSERT INTO [Booking] (UserID, OwnerId, SubCourtID, CreateDate, BookingDate, TimeSlotID, BookingType, Amount, [Status], PromotionCode)
VALUES (2, 1, 1, '2024-05-01 05:01:23', '2024-05-01', 1, 1, 35.000, 0, 'MSMZ4');
INSERT INTO [Booking] (UserID, OwnerId, SubCourtID, CreateDate, BookingDate, TimeSlotID, BookingType, Amount, [Status], PromotionCode)
VALUES (4, 3, 2, '2024-12-02 07:03:12', '2024-12-02', 2, 1, 55.000, 0, 'MSMZ4');

--insert Table [CheckIn]
insert into [CheckIn] (SubCourtID, BookingID, CheckInTime, CheckInStatus, UserID) values(2, 1, '2024-05-01 05:00:00', 1, 8);
insert into [CheckIn] (SubCourtID, BookingID, CheckInTime, CheckInStatus, UserID) values(1, 2, '2024-12-02 09:00:00', 0, 9);

--insert Table [Evaluate]
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(2, 1, 5, N'Những trải nghiệm tuyệt vời!', '2024-05-03 19:12:35', 'Pham Quoc Duy');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(4, 2, 3, N'Sân tốt, nhưng hơi đắt', '2024-05-07 14:32:42', 'Nguyen Phi Hung');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(3, 3, 4, N'Sân tốt, dịch vụ chu đáo', '2024-06-07 14:59:42', 'Bui Duc Trieu');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(5, 4, 3, N'Sân sạch sẽ', '2024-05-07 12:32:45', 'Nguyen Trung Nam');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(5, 5, 1, N'Thái độ nhân viên không tốt', '2024-02-07 11:11:45', 'Nguyen Trung Nam');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(4, 6, 5, N'Sân tuyệt vời, bầu không khí tuyệt vời', '2024-02-07 07:11:25', 'Nguyen Phi Hung');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(4, 7, 3, N'Sân hơi nhỏ nhưng chất lượng tốt', '2024-02-07 10:09:32', 'Nguyen Phi Hung');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(3, 8, 4, N'Sân rất đẹp và rộng rãi', '2024-07-01 10:00:00', 'Bui Duc Trieu');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(2, 9, 2, N'Dịch vụ không tốt lắm', '2024-07-02 11:00:00', 'Pham Quoc Duy');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(3, 10, 5, N'Rất hài lòng với sân', '2024-07-03 12:00:00', 'Bui Duc Trieu');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(4, 11, 3, N'Giá cả hợp lý, nhưng sân hơi xa', '2024-07-04 13:00:00', 'Nguyen Phi Hung');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(5, 12, 1, N'Không gian không thoải mái', '2024-07-05 14:00:00', 'Nguyen Trung Nam');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(2, 13, 5, N'Dịch vụ và sân đều tuyệt vời', '2024-07-06 15:00:00', 'Pham Quoc Duy');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(2, 14, 4, N'Sân sạch sẽ, dịch vụ tốt', '2024-07-07 16:00:00', 'Pham Quoc Duy');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(3, 15, 2, N'Sân hơi nhỏ và chật chội', '2024-07-08 17:00:00', 'Bui Duc Trieu');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(4, 16, 3, N'Thái độ nhân viên cần cải thiện', '2024-07-09 18:00:00', 'Nguyen Phi Hung');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(5, 17, 4, N'Giá hợp lý, dịch vụ tốt', '2024-07-10 19:00:00', 'Nguyen Trung Nam');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(5, 18, 5, N'Không gian rộng và thoáng', '2024-07-11 20:00:00', 'Nguyen Trung Nam');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(2, 19, 2, N'Nhân viên không thân thiện', '2024-07-12 21:00:00', 'Pham Quoc Duy');
insert into [Evaluate] (UserID, CourtID, Rating, Comment, EvaluateDate, CreatedBy) values(3, 20, 3, N'Sân tốt nhưng giá hơi cao', '2024-07-13 22:00:00', 'Bui Duc Trieu');


