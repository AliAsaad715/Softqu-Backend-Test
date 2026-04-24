using Microsoft.AspNetCore.Mvc;
using Softqu.Application.Features.SwiperSlides.Commands;
using Softqu.Application.Features.SwiperSlides.Queries;

namespace Softqu.API.Controllers
{
    [Route("api/admin/home-slides")]
    public class SwiperSlideController : ApiClientController
    {
        // 1. GET: api/admin/home-slides
        [HttpGet]
        public async Task<IActionResult> GetHomeSlidesAsync()
        {
            var result = await Mediator.Send(new GetHomeSlidesQuery());
            return HandleResult(result);
        }

        // 2. POST: api/admin/home-slides
        [HttpPost]
        public async Task<IActionResult> CreateNewSlideAsync([FromBody] CreateNewSlideCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        // 3. PUT: api/admin/home-slides
        [HttpPut]
        public async Task<IActionResult> UpdateSlideAsync([FromBody] UpdateSlideCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        // 4. PUT: api/admin/home-slides/{lang}
        [HttpPut("{lang}")]
        public async Task<IActionResult> UpdateSlideTranslationAsync([FromRoute] string lang, [FromBody] UpdateSlideTranslationCommand command)
        {
            command.LanguageCode = lang;
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        // 5. PUT: api/admin/home-slides/order
        [HttpPut("order")]
        public async Task<IActionResult> ChangeSlideOrderAsync([FromBody] ChangeSlideOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return HandleResult(result);
        }

        // 6. DELETE: api/admin/home-slides
        //[HttpDelete]
        //public async Task<IActionResult> RemoveSlideAsync([FromBody] RemoveSlideCommand command)
        //{
        //    var result = await Mediator.Send(command);
        //    return HandleResult(result);
        //}
    }
}
