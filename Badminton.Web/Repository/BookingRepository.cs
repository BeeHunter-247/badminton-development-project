using Badminton.Web.DTO.Booking;
using Badminton.Web.Enums;
using Badminton.Web.Interfaces;
using Badminton.Web.Mappers;
using Badminton.Web.Models;
using Microsoft.EntityFrameworkCore;
using static Badminton.Web.DTO.Booking.BookingDTO;

namespace Badminton.Web.Repository
{
    public class BookingRepository : IBookingRepository
    {

        private readonly CourtSyncContext _context;
        public BookingRepository(CourtSyncContext context)
        {
            _context = context;
        }

        // create
        public async Task<Booking> CreateAsync(Booking bookingModel)
        {
            if (bookingModel == null)
            {
                throw new ArgumentNullException(nameof(bookingModel));
            }

            _context.Bookings.Add(bookingModel);
            await _context.SaveChangesAsync();
            return bookingModel;
        }

        public async Task<BookingDTO> CreateBookingAsync(CreateBookingDTO createBookingDto)
        {
            var booking = createBookingDto.ToFormatBookingFromCreate();
            // Xử lý các thuộc tính còn lại của booking ở đây (ví dụ: TotalHours, Status, ...)

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking.ToFormatBookingDTO(); // Trả về BookingDTO
        }
        public async Task<BookingDTO> CreateBookingAsync(BookingDTO createBookingDto)
        {
            if (createBookingDto == null)
            {
                throw new ArgumentNullException(nameof(createBookingDto));
            }

            // Chuyển đổi BookingDTO thành Booking
            var bookingModel = BookingMapper.ToFormatBookingFromDTO(createBookingDto);

            // Thêm đối tượng Booking vào DbSet<Bookings> của context
            _context.Bookings.Add(bookingModel);

            // Lưu các thay đổi vào cơ sở dữ liệu một cách bất đồng bộ
            await _context.SaveChangesAsync();

            // Chuyển đổi Booking vừa được lưu thành BookingDTO
            var bookingDto = bookingModel.ToFormatBookingDTO();

            // Trả về đối tượng BookingDTO
            return bookingDto;
        }


        //read
        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.SubCourt)
                .Include(b => b.TimeSlot)
                .Include(b => b.Schedule)
                .Include(b => b.Promotion)
                .ToListAsync(); // Eager loading các đối tượng liên quan
        }
        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.SubCourt)
                .Include(b => b.TimeSlot)
                .Include(b => b.Schedule)
                .Include(b => b.Promotion)
                .FirstOrDefaultAsync(b => b.BookingId == id); // Eager loading các đối tượng liên quan
        }
        public async Task<List<BookingDTO>> GetAllBookingsAsync()
        {
            var bookings = await GetAllAsync();
            return bookings.Select(BookingMapper.ToFormatBookingDTO).ToList();
        }
        public async Task<BookingDTO> GetBookingByIdAsync(int id)
        {
            var booking = await GetByIdAsync(id);
            if (booking == null)
            {
                return null; // Hoặc throw new NotFoundException("Booking not found");
            }
            return booking.ToFormatBookingDTO();
        }
        public async Task<List<BookingDTO>> GetBookingsByUserIdAsync(int userId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.User)
                .Include(b => b.SubCourt)
                .Include(b => b.TimeSlot)
                .Include(b => b.Schedule)
                .Include(b => b.Promotion)
                .ToListAsync();
            return bookings.Select(BookingMapper.ToFormatBookingDTO).ToList();
        }
        public async Task<List<BookingDTO>> GetBookingsBySubCourtIdAsync(int subCourtId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.SubCourtId == subCourtId)
                .Include(b => b.User)
                .Include(b => b.SubCourt)
                .Include(b => b.TimeSlot)
                .Include(b => b.Schedule)
                .Include(b => b.Promotion)
                .ToListAsync();
            return bookings.Select(BookingMapper.ToFormatBookingDTO).ToList();
        }

        //Update
        public Task<Booking?> UpdateAsync(int id, BookingDTO bookingDTO)
        {
            throw new NotImplementedException();
        }
        public async Task<BookingDTO> UpdateBookingAsync(int id, UpdateBookingDTO updateBookingDto)
        {
            var existingBooking = await _context.Bookings.FindAsync(id);
            if (existingBooking == null)
            {
                return null;
            }

            // Cập nhật các thuộc tính của booking từ bookingDTO
            existingBooking.BookingDate = updateBookingDto.BookingDate ?? existingBooking.BookingDate;
          
            
            existingBooking.Status = updateBookingDto.Status?.GetHashCode() ?? existingBooking.Status;
            existingBooking.CancellationReason = updateBookingDto.CancellationReason ?? existingBooking.CancellationReason;
            //existingBooking.PromotionId = updateBookingDto.PromotionID ?? existingBooking.PromotionId;

            await _context.SaveChangesAsync();
            return existingBooking.ToFormatBookingDTO(); // Trả về BookingDTO sau khi cập nhật
        }
        public Task<BookingDTO> UpdateBookingAsync(int id, BookingDTO updateBookingDto)
        {
            throw new NotImplementedException();
        }
        public async Task<BookingDTO> CancelBookingAsync(int bookingId, string cancellationReason)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                return null; // Hoặc throw new NotFoundException("Booking not found");
            }

            booking.Status = (int)BookingStatus.Cancelled;
            booking.CancellationReason = cancellationReason;
            await _context.SaveChangesAsync();
            await _context.Entry(booking).ReloadAsync(); // Tải lại để có thông tin cập nhật

            return booking.ToFormatBookingDTO(); // Trả về BookingDTO sau khi cập nhật
        }

        // Delete
        public async Task<Booking?> DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return null;
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Xóa các check-in liên quan
                    _context.CheckIns.RemoveRange(_context.CheckIns.Where(ci => ci.BookingId == bookingId));
                    await _context.SaveChangesAsync();

                    // Xóa booking
                    var booking = await _context.Bookings.FindAsync(bookingId);
                    if (booking != null)
                    {
                        _context.Bookings.Remove(booking);
                        await _context.SaveChangesAsync();
                    }

                    transaction.Commit(); // Cam kết giao dịch
                }
                catch (Exception)
                {
                    transaction.Rollback(); // Hoàn tác giao dịch nếu có lỗi
                    throw; // Ném lại ngoại lệ để thông báo lỗi cho controller
                }
            }
        }


        //Kiểm tra

        public async Task<bool> BookingExists(int id)
        {
            return await _context.Bookings.AnyAsync(b => b.BookingId == id);
        }
        public async Task<bool> IsTimeSlotAvailableAsync(int subCourtId, int timeSlotId, DateOnly bookingDate)
        {
            return !await _context.Bookings.AnyAsync(b =>
                b.SubCourtId == subCourtId &&
                b.TimeSlotId == timeSlotId &&
                b.BookingDate == bookingDate);
        }

        // tính toán
        public Task<decimal> CalculateBookingPriceAsync(int subCourtId, int timeSlotId, int promotionId)
        {
            throw new NotImplementedException();
        }


    }
}
