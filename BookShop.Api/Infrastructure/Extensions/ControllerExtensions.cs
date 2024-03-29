﻿namespace BookShop.Api.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        public static IActionResult ReturnOkOrNotFound(this Controller controller, object model)
        {
            if(model == null)
            {
                return controller.NotFound();
            }

            return controller.Ok(model);
        }
    }
}
