﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace gestao_produtos.api.Results
{
    [DefaultStatusCode(StatusCodes.Status403Forbidden)]
    public class ForbiddenObjectResult : ObjectResult
    {
        public ForbiddenObjectResult([ActionResultObjectValue] object value) : base(value)
            => StatusCode = StatusCodes.Status403Forbidden;
    }
}
