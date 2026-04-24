using MediatR;
using Softqu.Application.Features.SwiperSlides.Commands;
using Softqu.Application.Shared.Results;
using Softqu.Domain.SwiperSlide.Interfaces;

namespace Softqu.Application.Features.SwiperSlides.Handlers
{
    public class UpdateSlideHandler : IRequestHandler<UpdateSlideCommand, AppResult>
    {
        private readonly ISwiperSlideRepository _slideRepository;

        public UpdateSlideHandler(ISwiperSlideRepository repository)
        {
            _slideRepository = repository;
        }

        public async Task<AppResult> Handle(UpdateSlideCommand request, CancellationToken cancellationToken)
        {
            var swiperSlide = await _slideRepository.GetByIdAsync(request.Id);
            if (swiperSlide == null)
            {
                return new NotFound();
            }

            swiperSlide.Update(request.ImageUrl, request.CategoryId);
            await _slideRepository.SaveChangesAsync();

            return new Success();
        }
    }
}
