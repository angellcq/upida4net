﻿using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Upida.Validation;

namespace UpidaExampleAngular
{
	public class ErrorFilterAttribute : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext context)
		{
			FailResponse response;
			if (context.Exception is ValidationException)
			{
				response = (context.Exception as ValidationException).BuildFailResponse();
				if (Severity.Fatal == response.Failures.Severity)
				{
					response.Main = "You are trying to break validation !!!";
				}
			}
			else
			{
				response = new FailResponse(context.Exception.ToString());
			}

			context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, response);
		}
	}
}