namespace Building_MinimalAPIsMoviesApp.Validations
{
    public class ValidationUtilities
    {

        public static string NotEmptyMessage = "Custom Not Empty Message Error For {PropertyName}";
        public static string MaximumLengthMessage = "Custom Maximum Length Message Error For {PropertyName}";
        public static string GreaterThanOrEqualToMessage = "Custom Not Empty Message Error For {PropertyName}";
        public static string FirstLitter = "First Litter Is Uppercase Error Message";
        public static string ExistsMessage(string Name) => $"a genre with the {Name} already exists.";


        public static bool FirstLitterIsUppercase(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            var firestLitter = value[0].ToString();
            return firestLitter == firestLitter.ToUpper();
        }

    }
}
