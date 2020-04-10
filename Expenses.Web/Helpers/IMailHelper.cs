using Expenses.Common.Models;

namespace Expenses.Web.Helpers
{
	public interface IMailHelper
	{
		Response SendMail(string to, string subject, string body);
	}
}
