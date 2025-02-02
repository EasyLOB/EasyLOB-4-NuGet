using EasyLOB.Environment;
using Newtonsoft.Json;
using System.Globalization;

namespace EasyLOB
{
    public static partial class AppHelper
    {
        #region Properties

        private static JsonSerializerSettings _jsonSettings;

        public static JsonSerializerSettings JsonSettings
        {
            get
            {
                if (_jsonSettings == null)
                {
                    _jsonSettings = new JsonSerializerSettings
                    {
                        Formatting = Formatting.None,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };
                }

                return _jsonSettings;
            }
        }

        // Culture = Language-Country ll-CC

        public static string Culture { get { return CultureInfo.CurrentCulture.Name; } }

        public static string CultureLanguage { get { return CultureInfo.CurrentCulture.Name.Substring(0, 2); } }

        public static string CultureCountry
        {
            get
            {
                string culture = CultureInfo.CurrentCulture.Name;

                return culture.Length >= 4 ? culture.Substring(3, 2) : "";
            }
        }

        #endregion Properties

        #region Methods

        public static void Log(ZOperationResult operationResult, string url, string header = null, string footer = null)
        {
            if (!operationResult.Ok)
            {
                if (string.IsNullOrEmpty(header))
                {
                    header =
                        url + System.Environment.NewLine
                        + MultiTenantHelper.Tenant.Name + System.Environment.NewLine
                        + EnvironmentHelper.Environment.UserName;
                }
                EasyLOBHelper.GetService<ILogManager>().OperationResult(operationResult, header, footer);
            }
        }

        #endregion Methods
    }
}