using System.Collections.Generic;
using System.Linq;

namespace ShootServ.Helpers
{
    public static class ModelStateExtensions
    {
        public static Dictionary<string, string> ToErrorsDictionary(this System.Web.Mvc.ModelStateDictionary modelState)
        {
            return modelState.Where(s => s.Value.Errors.Count > 0)
                .Select(
                    s =>
                        new KeyValuePair<string, string>(s.Key,
                            string.Join(";", s.Value.Errors.Select(error => error.ErrorMessage))))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}