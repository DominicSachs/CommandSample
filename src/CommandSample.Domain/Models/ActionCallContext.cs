using System;

namespace CommandSample.Domain.Models
{
	public class ActionCallContext
	{
		public ActionCallContext() { }

		public ActionCallContext(Guid userGuid, string email)
		{
			UserGuid = userGuid;
			Email = email;
		}

		public Guid UserGuid { get; }
		public string Email { get; set; }
	}
}