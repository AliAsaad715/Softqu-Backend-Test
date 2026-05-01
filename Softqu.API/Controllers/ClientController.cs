using Microsoft.AspNetCore.Mvc;
using Softqu.Application.Features.Categories.DTOs;
using Softqu.Application.Features.Categories.Queries;
using Softqu.Application.Features.PopularCategories.DTOs;
using Softqu.Application.Features.PopularCategories.Queries;
using Softqu.Application.Features.SwiperSlides.DTOs;
using Softqu.Application.Features.SwiperSlides.Queries;

namespace Softqu.API.Controllers
{
    [Route("api")]
    public class ClientController : ApiClientController
    {
        // 1. GET: api/categories/:lang
        [HttpGet("categories/{lang}")]
        [ProducesResponseType(typeof(List<CategoryClientDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryTree([FromRoute] string lang)
        {
            var result = await Mediator.Send(new GetClientCategoriesTreeQuery { LanguageCode = lang });
            return HandleResult(result);
        }

        // 2. GET: api/popular-categories/:lang
        [HttpGet("popular-categories/{lang}")]
        [ProducesResponseType(typeof(List<PopularCategoryClientDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPopularCategories([FromRoute] string lang)
        {
            var result = await Mediator.Send(new GetClientPopularCategoriesQuery { LanguageCode = lang });
            return HandleResult(result);
        }

        // 3. GET: api/home-slides/:lang
        [HttpGet("home-slides/{lang}")]
        [ProducesResponseType(typeof(List<SwiperSlideClientDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHomeSlides(string lang)
        {
            var result = await Mediator.Send(new GetClientHomeSlidesQuery { LanguageCode = lang });
            return HandleResult(result);
        }
    }
}
