using CommandSample.Domain.Actions.Base;
using CommandSample.Domain.Models;

namespace CommandSample.Domain.Actions
{
	public class HelloAction : AbstractAction<Nothing>
	{
		public HelloAction(ActionCallContext context, string text) : base(context)
		{
			Text = text;
		}

		public string Text { get; }
	}
}
